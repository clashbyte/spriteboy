using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using SpriteBoy.Engine.Pipeline;
using SpriteBoy.Data.Shaders;
using OpenTK.Graphics;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Геометрический компонент
	/// </summary>
	public class MeshComponent : EntityComponent, IRenderable {

		/// <summary>
		/// Вершины
		/// </summary>
		public virtual Vec3[] Vertices {
			get {
				float[] verts = SearchVertices();
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
					vertices = new float[value.Length * 3];
					Vec3 max = Vec3.One * float.MinValue, min = Vec3.One * float.MaxValue;
					for (int i = 0; i < value.Length; i++) {
						int ps = i * 3;
						vertices[ps + 0] = value[i].X;
						vertices[ps + 1] = value[i].Y;
						vertices[ps + 2] = -value[i].Z;
						if (value[i].X > max.X) {
							max.X = value[i].X;
						}
						if (value[i].Y > max.Y) {
							max.Y = value[i].Y;
						}
						if (value[i].Z > max.Z) {
							max.Z = value[i].Z;
						}
						if (value[i].X < min.X) {
							min.X = value[i].X;
						}
						if (value[i].Y < min.Y) {
							min.Y = value[i].Y;
						}
						if (value[i].Z < min.Z) {
							min.Z = value[i].Z;
						}
					}
					cull.Min = min;
					cull.Max = max;
					RebuildParentCull();
					vertexCount = vertices.Length;
				} else {
					vertices = null;
					vertexCount = 0;
				}
				needVertexBuffer = true;
			}
		}

		/// <summary>
		/// Нормали
		/// </summary>
		public virtual Vec3[] Normals {
			get {
				float[] norms = SearchNormals();
				if (norms != null) {
					Vec3[] na = new Vec3[norms.Length / 3];
					for (int i = 0; i < na.Length; i++) {
						int ps = i * 3;
						na[i] = new Vec3(
							norms[ps],
							norms[ps + 1],
							-norms[ps + 2]
						);
					}
					return na;
				} else {

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
					normals = null;
				}
				needNormalBuffer = true;
			}
		}


		/// <summary>
		/// Текстурные координаты
		/// </summary>
		public virtual Vec2[] TexCoords {
			get {
				float[] tcrd = SearchTexCoords();
				if (tcrd != null) {
					Vec2[] ca = new Vec2[tcrd.Length / 2];
					for (int i = 0; i < ca.Length; i++) {
						int ps = i * 2;
						ca[i] = new Vec2(
							tcrd[ps],
							tcrd[ps + 1]
						);
					}
					return ca;
				}
				return null;
			}
			set {
				if (value != null) {
					uv = new float[value.Length * 2];
					for (int i = 0; i < value.Length; i++) {
						int ps = i * 2;
						uv[ps + 0] = value[i].X;
						uv[ps + 1] = value[i].Y;
					}
				} else {
					uv = null;
				}
				needTexCoordBuffer = true;
			}
		}

		/// <summary>
		/// Индексы
		/// </summary>
		public virtual ushort[] Indices {
			get {
				ushort[] idxs = SearchIndices();
				if (idxs != null) {
					ushort[] ia = new ushort[idxs.Length];
					for (int i = 0; i < idxs.Length; i += 3) {
						ia[i + 0] = idxs[i + 2];
						ia[i + 1] = idxs[i + 1];
						ia[i + 2] = idxs[i + 0];
					}
					return ia;
				}
				return null;
			}
			set {
				if (value != null) {
					indices = new ushort[value.Length];
					for (int i = 0; i < indices.Length; i += 3) {
						indices[i + 0] = value[i + 2];
						indices[i + 1] = value[i + 1];
						indices[i + 2] = value[i + 0];
					}
					indexCount = indices.Length;
				} else {
					indices = null;
					indexCount = 0;
				}
				needIndexBuffer = true;
				needVertexBuffer = true;
				needNormalBuffer = true;
				needTexCoordBuffer = true;
			}
		}

		/// <summary>
		/// Текстура меша
		/// </summary>
		public Texture Texture {
			get;
			set;
		}

		/// <summary>
		/// Диффузный цвет
		/// </summary>
		public Color Diffuse {
			get;
			set;
		}

		/// <summary>
		/// Неосвещённый меш
		/// </summary>
		public bool Unlit {
			get;
			set;
		}

		/// <summary>
		/// Режим смешивания
		/// </summary>
		public BlendingMode Blending {
			get;
			set;
		}

		/// <summary>
		/// Прокси-меш, содержащий геометрические данные
		/// </summary>
		public MeshComponent Proxy {
			get;
			set;
		}

		/// <summary>
		/// Режим смешивания при отрисовке
		/// </summary>
		internal override EntityComponent.BlendingMode RenditionBlending {
			get {
				return Blending;
			}
		}

		/// <summary>
		/// Выборка, в какой из проходов рендера включить данный меш
		/// </summary>
		internal override EntityComponent.TransparencyPass RenditionPass {
			get {
				if (Diffuse.A < 255) {
					return TransparencyPass.Blend;
				}
				switch (Blending) {
					case BlendingMode.AlphaChannel:
						if (Texture!=null) {
							if (Texture.Transparency == Pipeline.Texture.TransparencyMode.AlphaFull) {
								return TransparencyPass.Blend;
							} else if (Texture.Transparency == Pipeline.Texture.TransparencyMode.AlphaCut) {
								return TransparencyPass.AlphaTest;
							}
						}
						break;
					case BlendingMode.Brightness:
					case BlendingMode.Add:
					case BlendingMode.Multiply:
						return TransparencyPass.Blend;
				}
				return TransparencyPass.Opaque;
			}
		}

		// Множитель для визуального затенения
		protected const float LIGHT_MULT = 0.8f;

		// Скрытые переменные
		protected float[] vertices;
		protected float[] normals;
		protected float[] uv;
		protected ushort[] indices;
		protected CullBox cull;

		// Количество вершин и индексов
		protected int vertexCount;
		protected int indexCount;

		// Буфферы для нового рендера
		protected int vertexBuffer;
		protected int normalBuffer;
		protected int textureBuffer;
		protected int indexBuffer;

		// Флаги для перестройки буфферов
		protected bool needVertexBuffer;
		protected bool needNormalBuffer;
		protected bool needTexCoordBuffer;
		protected bool needIndexBuffer;

		/// <summary>
		/// Статический меш
		/// </summary>
		public MeshComponent() {
			Diffuse = Color.White;
			Blending = BlendingMode.AlphaChannel;
			cull = new CullBox();
		}

		/// <summary>
		/// Отрисовка меша
		/// </summary>
		internal override void Render() {
			if (GraphicalCaps.ShaderPipeline) {
				ModernRender();
			} else {
				LegacyRender();
			}
		}

		/// <summary>
		/// Устаревший рендерер
		/// </summary>
		protected virtual void LegacyRender() {

			// Поиск вершин и индексов
			float[] va = SearchVertices();
			ushort[] ia = SearchIndices();

			// Рендер только если есть что рисовать
			if (va != null && ia != null) {
				if (va.Length > 0 && ia.Length > 0) {

					// Установка цвета и освещения, если требуется
					if (!Unlit) {
						GL.ShadeModel(ShadingModel.Smooth);
						GL.Enable(EnableCap.Lighting);
						GL.Enable(EnableCap.Light0);
						GL.LightModel(LightModelParameter.LightModelAmbient, new float[] { LIGHT_MULT, LIGHT_MULT, LIGHT_MULT, 1f });
						GL.Light(LightName.Light0, LightParameter.Position, new OpenTK.Vector4(0f, 1f, 0f, 1f));
						GL.Enable(EnableCap.ColorMaterial);
						GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.AmbientAndDiffuse);
					}
					GL.Color3(Diffuse);

					// Загрузка вершин
					GL.EnableClientState(ArrayCap.VertexArray);
					GL.VertexPointer(3, VertexPointerType.Float, 0, va);

					// Загрузка нормалей
					float[] na = SearchNormals();
					if (na != null) {
						GL.EnableClientState(ArrayCap.NormalArray);
						GL.NormalPointer(NormalPointerType.Float, 0, na);
					}

					// Загрузка текстурных координат
					float[] ta = null;
					if (Texture != null) {
						ta = SearchTexCoords();
					}
					if (Texture != null && ta != null) {
						GL.EnableClientState(ArrayCap.TextureCoordArray);
						GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, ta);
						Texture.Bind();
					} else {
						GL.BindTexture(TextureTarget.Texture2D, 0);
					}

					// Отрисовка элементов
					GL.DrawElements(BeginMode.Triangles, ia.Length, DrawElementsType.UnsignedShort, ia);

					// Выключение состояний
					GL.DisableClientState(ArrayCap.ColorArray);
					GL.DisableClientState(ArrayCap.NormalArray);
					GL.DisableClientState(ArrayCap.TextureCoordArray);
					GL.DisableClientState(ArrayCap.VertexArray);
					GL.Disable(EnableCap.Light0);
					GL.Disable(EnableCap.Lighting);

				}
			}
		}

		/// <summary>
		/// Новый рендер
		/// </summary>
		protected virtual void ModernRender() {

			// Проверка буфферов
			CheckBuffers();

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

				// Отрисовка
				MeshShader shader = MeshShader.Shader;
				shader.DiffuseColor = Diffuse;
				shader.LightMultiplier = Unlit ? 1f : LIGHT_MULT;
				shader.Bind();
				GL.BindBuffer(BufferTarget.ArrayBuffer, SearchVertexBuffer());
				GL.VertexAttribPointer(shader.VertexBufferLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
				GL.BindBuffer(BufferTarget.ArrayBuffer, SearchNormalBuffer());
				GL.VertexAttribPointer(shader.NormalBufferLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
				GL.BindBuffer(BufferTarget.ArrayBuffer, SearchTexCoordBuffer());
				GL.VertexAttribPointer(shader.TexCoordBufferLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, SearchIndexBuffer());
				GL.DrawElements(BeginMode.Triangles, iCount, DrawElementsType.UnsignedShort, 0);
				shader.Unbind();
			}

		}

		/// <summary>
		/// Проверка буфферов
		/// </summary>
		protected virtual void CheckBuffers() {

			// Индексный буффер
			if (needIndexBuffer) {
				ushort[] ia = SearchIndices();
				if (ia == null) {
					ia = new ushort[0];
				}
				if (indexBuffer == 0) {
					indexBuffer = GL.GenBuffer();
				}
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
				GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(ia.Length * 2), ia, BufferUsageHint.StaticDraw);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
				needIndexBuffer = false;
			}

			// Вершинный буффер
			if (needVertexBuffer) {
				float[] va = SearchVertices();
				if (va == null) {
					va = new float[vertexCount * 3];
				}
				if (vertexBuffer == 0) {
					vertexBuffer = GL.GenBuffer();
				}
				GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(va.Length * 4), va, BufferUsageHint.StaticDraw);
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
				needVertexBuffer = false;
			}

			// Буффер нормалей
			if (needNormalBuffer) {
				float[] na = SearchNormals();
				if (na == null) {
					na = new float[vertexCount * 3];
				}
				if (normalBuffer == 0) {
					normalBuffer = GL.GenBuffer();
				}
				GL.BindBuffer(BufferTarget.ArrayBuffer, normalBuffer);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(na.Length * 4), na, BufferUsageHint.StaticDraw);
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
				needNormalBuffer = false;
			}

			// Текстурный буффер
			if (needTexCoordBuffer) {
				float[] ta = SearchTexCoords();
				if (ta == null) {
					ta = new float[vertexCount * 2];
				}
				if (textureBuffer == 0) {
					textureBuffer = GL.GenBuffer();
				}
				GL.BindBuffer(BufferTarget.ArrayBuffer, textureBuffer);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(ta.Length * 4), ta, BufferUsageHint.StaticDraw);
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
				needTexCoordBuffer = false;
			}

			// Проверка родителя
			if (Proxy != null) {
				Proxy.CheckBuffers();
			}
		}

		/// <summary>
		/// Получение сферы отсечения
		/// </summary>
		/// <returns>Сфера</returns>
		internal override CullBox GetCullingBox() {
			return cull;
		}

		/// <summary>
		/// Поиск вершин среди прокси-объектов
		/// </summary>
		/// <returns>Массив вершин</returns>
		protected float[] SearchVertices() {
			MeshComponent mc = this;
			float[] va = vertices;
			while (va == null) {
				if (mc.Proxy!=null) {
					mc = mc.Proxy;
				}else{
					break;
				}
				va = mc.vertices;
			}
			return va;
		}

		/// <summary>
		/// Поиск нормалей среди прокси-объектов
		/// </summary>
		/// <returns>Массив нормалей</returns>
		protected float[] SearchNormals() {
			MeshComponent mc = this;
			float[] na = normals;
			while (na == null) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				na = mc.normals;
			}
			return na;
		}

		/// <summary>
		/// Поиск текстурных координат среди прокси-объектов
		/// </summary>
		/// <returns>Массив текстурных координат</returns>
		protected float[] SearchTexCoords() {
			MeshComponent mc = this;
			float[] ta = uv;
			while (ta == null) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				ta = mc.uv;
			}
			return ta;
		}

		/// <summary>
		/// Поиск индексов треугольников
		/// </summary>
		/// <returns></returns>
		protected ushort[] SearchIndices() {
			MeshComponent mc = this;
			ushort[] ia = indices;
			while (ia == null) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				ia = mc.indices;
			}
			return ia;
		}

		/// <summary>
		/// Поиск вершинного буффера
		/// </summary>
		/// <returns>Индекс вершинного буффера</returns>
		protected int SearchVertexBuffer() {
			MeshComponent mc = this;
			int buffer = vertexBuffer;
			while (buffer == 0) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				buffer = mc.vertexBuffer;
			}
			return buffer;
		}

		/// <summary>
		/// Поиск буффера нормалей
		/// </summary>
		/// <returns>Индекс буффера нормалей</returns>
		protected int SearchNormalBuffer() {
			MeshComponent mc = this;
			int buffer = normalBuffer;
			while (buffer == 0) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				buffer = mc.normalBuffer;
			}
			return buffer;
		}

		/// <summary>
		/// Поиск буффера текстурных координат
		/// </summary>
		/// <returns>Индекс буффера текстурных координат</returns>
		protected int SearchTexCoordBuffer() {
			MeshComponent mc = this;
			int buffer = textureBuffer;
			while (buffer == 0) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				buffer = mc.textureBuffer;
			}
			return buffer;
		}

		/// <summary>
		/// Поиск индексного буффера
		/// </summary>
		/// <returns>Индексный буффер</returns>
		protected int SearchIndexBuffer() {
			MeshComponent mc = this;
			int buffer = indexBuffer;
			while (buffer == 0) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				buffer = mc.indexBuffer;
			}
			return buffer;
		}

		/// <summary>
		/// Поиск количества вершин
		/// </summary>
		/// <returns>Количество вершин</returns>
		protected int GetVertexCount() {
			MeshComponent mc = this;
			int count = vertexCount;
			while (count == 0) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				count = mc.vertexCount;
			}
			return count;
		}

		/// <summary>
		/// Поиск количества индексов
		/// </summary>
		/// <returns>Количество индексов</returns>
		protected int GetIndexCount() {
			MeshComponent mc = this;
			int count = indexCount;
			while (count == 0) {
				if (mc.Proxy != null) {
					mc = mc.Proxy;
				} else {
					break;
				}
				count = mc.indexCount;
			}
			return count;
		}
	}
}
