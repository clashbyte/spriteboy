using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteBoy.Engine.Data;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace SpriteBoy.Engine {

	/// <summary>
	/// Коробка с текстурами неба
	/// </summary>
	public class Skybox : IRenderable {

		/// <summary>
		/// Текстуры
		/// </summary>
		Texture[] textures;

		/// <summary>
		/// Вершины
		/// </summary>
		static float[] vertices;

		/// <summary>
		/// Текстурные координаты
		/// </summary>
		static float[] coords;

		/// <summary>
		/// Индексы
		/// </summary>
		static ushort[] indices;

		/// <summary>
		/// Матрицы для каждой стороны
		/// </summary>
		static Matrix4[] matrices;

		/// <summary>
		/// Создание нового скайбокса
		/// </summary>
		public Skybox() {
			textures = new Texture[6];
			if (matrices == null) {
				matrices = new Matrix4[6];
				matrices[0] = Matrix4.Identity;
				matrices[1] = Matrix4.CreateRotationY(-MathHelper.PiOver2);
				matrices[2] = Matrix4.CreateRotationY(-MathHelper.Pi);
				matrices[3] = Matrix4.CreateRotationY(-MathHelper.Pi - MathHelper.PiOver2);
				matrices[4] = Matrix4.CreateRotationX(MathHelper.PiOver2);
				matrices[5] = Matrix4.CreateRotationX(-MathHelper.PiOver2);
			}
		}

		/// <summary>
		/// Установка текстуры определенной стороне скайбокса
		/// </summary>
		/// <param name="side">Сторона</param>
		/// <returns>Текстура на этой стороне</returns>
		public Texture this[Side side] {
			get {
				return textures[(int)side];
			}
			set {
				textures[(int)side] = value;
			}
		}

		/// <summary>
		/// Отрисовка скайбокса
		/// </summary>
		public void Render() {

			// Создание массивов
			if (vertices == null || coords == null || indices == null) {

				// Вершины
				vertices = new float[] {
					-5f,  5f, -5f,
					 5f,  5f, -5f,
					-5f, -5f, -5f,
					 5f, -5f, -5f
				};

				// Текстурные координаты
				coords = new float[] {
					0f, 0f,
					1f, 0f,
					0f, 1f,
					1f, 1f
				};

				// Индексы
				indices = new ushort[] {
					2, 1, 0,
					2, 3, 1
				};
			}

			// Установка данных
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);

			GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);
			GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, coords);

			// Отрисовка сторон
			GL.Color3(Color.White);
			for (int i = 0; i < 6; i++) {
				if (textures[i]!=null) {
					textures[i].Bind();
					GL.PushMatrix();
					GL.MultMatrix(ref matrices[i]);
					GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedShort, indices);
					GL.PopMatrix();
				}
			}

			// Отключение массивов
			GL.DisableClientState(ArrayCap.TextureCoordArray);
			GL.DisableClientState(ArrayCap.VertexArray);
		}

		/// <summary>
		/// Сторона скайбокса
		/// </summary>
		public enum Side : byte {
			/// <summary>
			/// Z+
			/// </summary>
			Front	= 0,
			/// <summary>
			/// X+
			/// </summary>
			Right	= 1,
			/// <summary>
			/// Z-
			/// </summary>
			Back	= 2,
			/// <summary>
			/// X-
			/// </summary>
			Left	= 3,
			/// <summary>
			/// Y+
			/// </summary>
			Top		= 4,
			/// <summary>
			/// Y-
			/// </summary>
			Bottom	= 5
		}

	}
}
