using OpenTK.Graphics.OpenGL;
using SpriteBoy.Data;
using SpriteBoy.Data.Shaders;
using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Data;
using SpriteBoy.Engine.Pipeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Компонент повершинно-анимированного меша
	/// </summary>
	public class MorphMeshComponent : AnimatedMeshComponent {

		/// <summary>
		/// Кадры анимации
		/// </summary>
		public override AnimatedMeshComponent.Frame[] Frames {
			get {
				return base.Frames;
			}
			set {
				base.Frames = value;
				if (value != null) {
					RebuildCullingSphere();
					queuedUpdate = new QueuedMeshUpdate() {
						FirstFrame = 0f,
						LastFrame = 999f,
						Time = 0f,
						IsLooping = true
					};
					if (GraphicalCaps.ShaderPipeline) {
						vertexCount = (value[0] as MorphFrame).verts.Length;
					}
				}
			}
		}

		/// <summary>
		/// Вершины - только чтение
		/// </summary>
		public override Vec3[] Vertices {
			get {
				if (vertices == null) {
					Frame[] frm = Frames;
					if (frm != null) {
						MorphFrame mf = InterpolateFrame((MorphFrame)frm[0], (MorphFrame)frm[0], 0f);
						queuedUpdate = new QueuedMeshUpdate() {
							FirstFrame = 0f,
							LastFrame = 1f,
							IsLooping = false,
							Time = 0
						};
						vertices = mf.verts;
					}
				}
				return base.Vertices;
			}
		}

		/// <summary>
		/// Нормали - только чтение
		/// </summary>
		public override Vec3[] Normals {
			get {
				if (normals == null) {
					Frame[] frm = Frames;
					if (frm != null) {
						MorphFrame mf = InterpolateFrame((MorphFrame)frm[0], (MorphFrame)frm[0], 0f);
						queuedUpdate = new QueuedMeshUpdate() {
							FirstFrame = 0f,
							LastFrame = 1f,
							IsLooping = false,
							Time = 0
						};
						normals = mf.normals;
					}
				}
				return base.Normals;
			}
		}

		/// <summary>
		/// Текущий кадр
		/// </summary>
		MorphFrame currentFrame;
		/// <summary>
		/// Переходный кадр
		/// </summary>
		MorphFrame transitionFrame;

		/// <summary>
		/// Первый и второй буфферы позиций вершин
		/// </summary>
		int firstVertexBuffer, secondVertexBuffer;
		/// <summary>
		/// Первый и второй буфферы нормалей вершин
		/// </summary>
		int firstNormalBuffer, secondNormalBuffer;

		/// <summary>
		/// Переход между двумя буфферами
		/// </summary>
		float bufferInterpolation;

		/// <summary>
		/// Предыдущие использованные кадры - снижают нагрузку на пайплайн
		/// </summary>
		float firstFrameUsed = -64f, secondFrameUsed = -64f;

		/// <summary>
		/// Конструктор
		/// </summary>
		public MorphMeshComponent() : base() {
			cull = null;
		}
		
		/// <summary>
		/// Новый рендерер
		/// </summary>
		protected override void ModernRender() {

			// Проверка буфферов
			needVertexBuffer = false;
			needNormalBuffer = false;
			CheckBuffers();

			// Количество вершин и индексов
			int vCount = GetVertexCount();
			int iCount = GetIndexCount();

			// Рендер
			if (iCount > 0 && vCount > 0) {

				// Установка текстуры
				if (Texture != null) {
					Texture.Bind();
				} else {
					Texture.BindEmpty();
				}

				// Шейдер
				MorphMeshShader shader = MorphMeshShader.Shader;
				shader.DiffuseColor = Diffuse;
				shader.FrameDelta = bufferInterpolation;
				shader.LightMultiplier = Unlit ? 1f : LIGHT_MULT;
				shader.Bind();

				// Вершины
				GL.BindBuffer(BufferTarget.ArrayBuffer, firstVertexBuffer);
				GL.VertexAttribPointer(shader.FirstVertexBufferLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
				GL.BindBuffer(BufferTarget.ArrayBuffer, secondVertexBuffer);
				GL.VertexAttribPointer(shader.SecondVertexBufferLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
				
				// Нормали
				GL.BindBuffer(BufferTarget.ArrayBuffer, firstNormalBuffer);
				GL.VertexAttribPointer(shader.FirstNormalBufferLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
				GL.BindBuffer(BufferTarget.ArrayBuffer, secondNormalBuffer);
				GL.VertexAttribPointer(shader.SecondNormalBufferLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
				
				// Текстурные координаты
				GL.BindBuffer(BufferTarget.ArrayBuffer, SearchTexCoordBuffer());
				GL.VertexAttribPointer(shader.TexCoordBufferLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
				
				// Индексы и отрисовка
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, SearchIndexBuffer());
				GL.DrawElements(BeginMode.Triangles, iCount, DrawElementsType.UnsignedShort, 0);
				shader.Unbind();
			}

		}

		/// <summary>
		/// Получение бокса для отсечения
		/// </summary>
		/// <returns>Коробка</returns>
		internal override CullBox GetCullingBox() {
			if (cull == null) {
				CullBox cb = null;
				MeshComponent mc = this;
				while (cb == null) {
					if (mc.Proxy != null) {
						mc = mc.Proxy;
					} else {
						break;
					}
					cb = mc.GetCullingBox();
				}
			}
			return cull;
		}

		/// <summary>
		/// Интерполирование двух кадров
		/// </summary>
		/// <param name="mf1">Первый кадр</param>
		/// <param name="mf2">Второй кадр</param>
		/// <param name="delta">Значение интерполяции</param>
		MorphFrame InterpolateFrame(MorphFrame mf1, MorphFrame mf2, float delta) {
			MorphFrame mf = new MorphFrame();
			mf.verts = new float[mf1.verts.Length];
			mf.normals = new float[mf1.normals.Length];

			// Интерполяция данных
			float p1, p2;
			for (int i = 0; i < mf.verts.Length; i++) {
				
				// Вершина
				p1 = mf1.verts[i];
				p2 = mf2.verts[i];
				mf.verts[i] = p1 + (p2 - p1) * delta;

				// Нормаль
				p1 = mf1.normals[i];
				p2 = mf2.normals[i];
				mf.normals[i] = p1 + (p2 - p1) * delta;
			}

			// Сохранение временного кадра
			return mf;
		}

		/// <summary>
		/// Обновление анимации
		/// </summary>
		protected override void UpdateAnimation() {
			Frame[] frms = Frames;
			if (frms != null) {
				QueuedMeshUpdate q = queuedUpdate;
				MorphFrame f1, f2;
				float d = 0;
				if (q.Time < 0) {
					f1 = transitionFrame;
					f2 = GetFrameForward(q.Time, q.IsLooping, q.FirstFrame, q.LastFrame);
					d = 1f + q.Time;
				} else {
					f1 = GetFrameBackward(q.Time, q.IsLooping, q.FirstFrame, q.LastFrame);
					f2 = GetFrameForward(q.Time, q.IsLooping, q.FirstFrame, q.LastFrame);
					if (f2 == null) {
						f2 = f1;
					}else if(f1 == null) {
						f1 = f2;
					} else {
						if (f1.Time != f2.Time) {
							d = (q.Time - f1.Time) / (f2.Time - f1.Time);
						}
					}
					if (d < 0) {
						d = 1f + d;
					}
				}
				if (GraphicalCaps.ShaderPipeline) {
					UpdateBuffers(f1, f2, d);
				} else {
					currentFrame = InterpolateFrame(f1, f2, d);
					vertices = currentFrame.verts;
					normals = currentFrame.normals;
				}
			}
		}

		/// <summary>
		/// Создание переходного кадра
		/// </summary>
		protected override void UpdateTransition() {
			Frame[] frms = Frames;
			if (frms != null) {
				QueuedMeshUpdate q = queuedTransitionUpdate;
				MorphFrame f1, f2;
				float d = 0;
				if (q.Time<0) {
					if (transitionFrame == null) {
						// WTF
						transitionFrame = GetFrameForward(q.FirstFrame, true, q.FirstFrame, q.LastFrame);
					}
					f1 = transitionFrame;
					f2 = GetFrameForward(q.Time, q.IsLooping, q.FirstFrame, q.LastFrame);
					d = 1f + q.Time;
				}else{
					f1 = GetFrameBackward(q.Time, q.IsLooping, q.FirstFrame, q.LastFrame);
					f2 = GetFrameForward(q.Time, q.IsLooping, q.FirstFrame, q.LastFrame);
					if (f2 == null) {
						f2 = f1;
					} else if (f1 == null) {
						f1 = f2;
					} else {
						if (f1.Time != f2.Time) {
							d = (q.Time - f1.Time) / (f2.Time - f1.Time);
						}
					}
					if (d < 0) {
						d = 1f + d;
					}
				}
				transitionFrame = InterpolateFrame(f1, f2, d);
			}
		}

		/// <summary>
		/// Поиск кадра вперед
		/// </summary>
		/// <param name="time">Время</param>
		/// <param name="allowLoop">Разрешить ли поиск с начала</param>
		/// <returns>Кадр</returns>
		MorphFrame GetFrameForward(float time, bool allowLoop, float minTime, float maxTime) {
			Frame[] frms = Frames;
			Frame fr = null;
			foreach (Frame f in frms) {
				if (f.Time > maxTime) {
					break;
				}
				if (f.Time > time) {
					fr = f;
					break;
				}
			}
			if (fr == null && allowLoop) {
				foreach (Frame f in frms) {
					if (f.Time >= minTime) {
						fr = f;
						break;
					}
				}
			}
			return (MorphFrame)fr;
		}

		/// <summary>
		/// Поиск кадра назад
		/// </summary>
		/// <param name="time">Время</param>
		/// <param name="allowLoop">Разрешить ли поиск с конца</param>
		/// <returns>Кадр</returns>
		MorphFrame GetFrameBackward(float time, bool allowLoop, float minTime, float maxTime) {
			List<Frame> fl = new List<Frame>(Frames);
			fl.Reverse();
			Frame[] frms = fl.ToArray();

			Frame fr = null;
			foreach (Frame f in frms) {
				if (f.Time < minTime) {
					break;
				}
				if (f.Time <= time) {
					fr = f;
					break;
				}
			}
			if (fr == null && allowLoop) {
				foreach (Frame f in frms) {
					if (f.Time <= minTime) {
						fr = f;
						break;
					}
				}
			}
			return (MorphFrame)fr;
		}

		/// <summary>
		/// Обновление состояния буфферов
		/// </summary>
		void UpdateBuffers(MorphFrame f1, MorphFrame f2, float d) {
			bool needFirstBuffer = false;
			bool needSecondBuffer = false;
			if (firstFrameUsed != f1.Time) {
				needFirstBuffer = true;
				firstFrameUsed = f1.Time;
			}
			if (secondFrameUsed != f2.Time) {
				needSecondBuffer = true;
				secondFrameUsed = f2.Time;
			}

			if (needFirstBuffer) {
				if (firstVertexBuffer == 0) firstVertexBuffer = GL.GenBuffer();
				if (firstNormalBuffer == 0) firstNormalBuffer = GL.GenBuffer();
				GL.BindBuffer(BufferTarget.ArrayBuffer, firstVertexBuffer);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(f1.verts.Length * 4), f1.verts, BufferUsageHint.StreamDraw);
				GL.BindBuffer(BufferTarget.ArrayBuffer, firstNormalBuffer);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(f1.normals.Length * 4), f1.normals, BufferUsageHint.StreamDraw);
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			}

			if (needSecondBuffer) {
				if (secondVertexBuffer == 0) secondVertexBuffer = GL.GenBuffer();
				if (secondNormalBuffer == 0) secondNormalBuffer = GL.GenBuffer();
				GL.BindBuffer(BufferTarget.ArrayBuffer, secondVertexBuffer);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(f2.verts.Length * 4), f2.verts, BufferUsageHint.StreamDraw);
				GL.BindBuffer(BufferTarget.ArrayBuffer, secondNormalBuffer);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(f2.normals.Length * 4), f2.normals, BufferUsageHint.StreamDraw);
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			}

			bufferInterpolation = d;
		}

		/// <summary>
		/// Изменение сферы отсечения
		/// </summary>
		void RebuildCullingSphere() {
			Frame[] frames = Frames;
			if (frames != null) {
				Vec3 max = Vec3.One * float.MinValue, min = Vec3.One * float.MaxValue;
				foreach (Frame f in frames) {
					MorphFrame cf = (MorphFrame)f;
					if (cf != null) {
						for (int i = 0; i < cf.verts.Length; i += 3) {
							if (cf.verts[i] > max.X) {
								max.X = cf.verts[i];
							}
							if (cf.verts[i + 1] > max.Y) {
								max.Y = cf.verts[i + 1];
							}
							if (-cf.verts[i + 2] > max.Z) {
								max.Z = -cf.verts[i + 2];
							}
							if (cf.verts[i] < min.X) {
								min.X = cf.verts[i];
							}
							if (cf.verts[i + 1] < min.Y) {
								min.Y = cf.verts[i + 1];
							}
							if (-cf.verts[i + 2] < min.Z) {
								min.Z = -cf.verts[i + 2];
							}
						}
						cull = new CullBox();
						cull.Min = min;
						cull.Max = max;
					}
				}
				RebuildParentCull();
			}
		}

		/// <summary>
		/// Морфный кадр анимации
		/// </summary>
		public class MorphFrame : AnimatedMeshComponent.Frame {

			/// <summary>
			/// Вершины
			/// </summary>
			public Vec3[] Vertices {
				get {
					if (verts != null) {
						Vec3[] va = new Vec3[verts.Length / 3];
						for (int i = 0; i < va.Length; i++) {
							int ps = i * 3;
							va[i] = new Vec3(
								verts[ps],
								verts[ps + 1],
								-verts[ps + 2]
							);
						}
						return va;
					}
					return null;
				}
				set {
					if (value != null) {
						verts = new float[value.Length * 3];
						for (int i = 0; i < value.Length; i++) {
							int ps = i * 3;
							verts[ps + 0] = value[i].X;
							verts[ps + 1] = value[i].Y;
							verts[ps + 2] = -value[i].Z;
						}
					} else {
						verts = null;
					}
				}
			}

			/// <summary>
			/// Нормали
			/// </summary>
			public Vec3[] Normals {
				get {
					if (normals != null) {
						Vec3[] na = new Vec3[normals.Length / 3];
						for (int i = 0; i < na.Length; i++) {
							int ps = i * 3;
							na[i] = new Vec3(
								normals[ps],
								normals[ps + 1],
								-normals[ps + 2]
							);
						}
						return na;
					}
					return null;
				}
				set {
					if (value != null) {
						normals = new float[value.Length * 3];
						for (int i = 0; i < value.Length; i++) {
							int ps = i * 3;
							normals[ps + 0] = value[i].X;
							normals[ps + 1] = value[i].Y;
							normals[ps + 2] = -value[i].Z;
						}
					} else {
						verts = null;
					}
				}
			}

			// Скрытые переменные
			internal float[] verts;
			internal float[] normals;
		}
	}
}
