using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteBoy.Engine.Data;
using SpriteBoy.Data.Types;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Куб из линий
	/// </summary>
	public class WireCubeComponent : EntityComponent, IRenderable {

		/// <summary>
		/// Массив индексов для куба
		/// </summary>
		static ushort[] indexArray;

		/// <summary>
		/// Расположение относительно центра объекта
		/// </summary>
		public Vec3 Position {
			get {
				return pos;
			}
			set {
				pos = value;
				needBuffer = true;
			}
		}

		/// <summary>
		/// Размеры
		/// </summary>
		public Vec3 Size {
			get {
				return size;
			}
			set {
				size = value;
				needBuffer = true;
			}
		}

		/// <summary>
		/// Цвет линий
		/// </summary>
		public Color WireColor;

		/// <summary>
		/// Толщина линий
		/// </summary>
		public float WireWidth;

		/// <summary>
		/// Скрытые характеристики объекта
		/// </summary>
		Vec3 pos, size;

		/// <summary>
		/// Необходима перестройка буффера
		/// </summary>
		bool needBuffer;

		/// <summary>
		/// Массив вершин
		/// </summary>
		float[] vertexArray;

		/// <summary>
		/// Конструктор компонента
		/// </summary>
		public WireCubeComponent() {
			Position = Vec3.Zero;
			Size = Vec3.One;
			WireColor = Color.White;
			WireWidth = 1f;
		}

		/// <summary>
		/// Отрисовка куба
		/// </summary>
		public void Render() {
			// Генерация массивов
			if (needBuffer || vertexArray == null) {
				float halfX = size.X / 2f;
				float halfY = size.Y / 2f;
				float halfZ = size.Z / 2f;

				// Вершины
				vertexArray = new float[]{
					
					// Ближняя левая верхняя		(0)
					pos.X - halfX,
					pos.Y + halfY,
					-pos.Z + halfZ,

					// Ближняя правая верхняя		(1)
					pos.X + halfX,
					pos.Y + halfY,
					-pos.Z + halfZ,

					// Ближняя левая нижняя			(2)
					pos.X - halfX,
					pos.Y - halfY,
					-pos.Z + halfZ,

					// Ближняя правая нижняя		(3)
					pos.X + halfX,
					pos.Y - halfY,
					-pos.Z + halfZ,

					// Дальняя левая верхняя		(4)
					pos.X - halfX,
					pos.Y + halfY,
					-pos.Z - halfZ,

					// Дальняя правая верхняя		(5)
					pos.X + halfX,
					pos.Y + halfY,
					-pos.Z - halfZ,

					// Дальняя левая нижняя			(6)
					pos.X - halfX,
					pos.Y - halfY,
					-pos.Z - halfZ,

					// Дальняя правая нижняя		(7)
					pos.X + halfX,
					pos.Y - halfY,
					-pos.Z - halfZ

				};

				needBuffer = false;
			}

			// Массив индексов
			if (indexArray == null) {
				indexArray = new ushort[]{

					// Передняя часть
					0, 1,
					1, 3,
					3, 2,
					2, 0,

					// Задняя часть
					4, 5,
					5, 7,
					7, 6,
					6, 4,

					// Соединения
					0, 4,
					1, 5,
					2, 6,
					3, 7

				};
			}

			// Отрисовка куба, без текстуры
			GL.BindTexture(TextureTarget.Texture2D, 0);
			GL.LineWidth(WireWidth);
			GL.Color3(WireColor);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.VertexPointer(3, VertexPointerType.Float, 0, vertexArray);
			GL.DrawElements(BeginMode.Lines, indexArray.Length, DrawElementsType.UnsignedShort, indexArray);
			GL.DisableClientState(ArrayCap.VertexArray);
		}
	}
}
