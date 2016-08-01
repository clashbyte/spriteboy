using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using SpriteBoy.Engine.Pipeline;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Геометрический компонент
	/// </summary>
	public abstract class MeshComponent : EntityComponent, IRenderable {

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
				} else {
					vertices = null;
				}
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
				} else {
					indices = null;
				}
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
		/// Включен ли альфаканал
		/// </summary>
		public bool AlphaBlend {
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

		// Скрытые переменные
		protected float[] vertices;
		protected float[] normals;
		protected float[] uv;
		protected ushort[] indices;
		protected CullBox cull;

		/// <summary>
		/// Статический меш
		/// </summary>
		public MeshComponent() {
			Diffuse = Color.White;
			AlphaBlend = false;
			cull = new CullBox();
		}

		/// <summary>
		/// Метод, вызываемый до рендера
		/// </summary>
		protected virtual void BeforeRender() { }

		/// <summary>
		/// Метод, вызываемый после рендера
		/// </summary>
		protected virtual void AfterRender() { }

		/// <summary>
		/// Отрисовка меша
		/// </summary>
		internal override void Render() {

			// Не рисуем если меш отключен
			if (!Enabled) {
				return;
			}

			// Поиск вершин и индексов
			float[] va = SearchVertices();
			ushort[] ia = SearchIndices();

			// Рендер только если есть что рисовать
			if (va != null && ia != null) {
				if (va.Length > 0 && ia.Length > 0) {

					// Установка данных
					BeforeRender();

					// Установка цвета
					GL.Color3(Diffuse);

					// Если есть альфасмешивание
					if (AlphaBlend) {
						GL.Enable(EnableCap.Blend);
						GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
						GL.DepthMask(false);
					}

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

					// Отключение смешивания
					if (AlphaBlend) {
						GL.Disable(EnableCap.Blend);
						GL.DepthMask(true);
					}

					// Отключение значений
					AfterRender();
				}
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
		float[] SearchVertices() {
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
		float[] SearchNormals() {
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
		float[] SearchTexCoords() {
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
		ushort[] SearchIndices() {
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
	}
}
