using OpenTK;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Data;
using SpriteBoy.Data.Shaders;
using SpriteBoy.Engine.Data;
using SpriteBoy.Engine.Pipeline;
using System;
using System.Drawing;

namespace SpriteBoy.Engine.World {

	/// <summary>
	/// Коробка с текстурами неба
	/// </summary>
	public class Skybox : IRenderable {

		/// <summary>
		/// Текстуры
		/// </summary>
		Texture[] textures;

		// Скрытые массивы для рендера
		static float[] vertices;
		static float[] coords;
		static ushort[] indices;

		// Матрицы для каждой стороны
		static Matrix4[] matrices;

		// Вершинные буфферы
		static int vertexBuffer;
		static int texCoordBuffer;
		static int indexBuffer;

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
			Diffuse = Color.White;
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
		/// Цвет диффуза
		/// </summary>
		public Color Diffuse {
			get;
			set;
		}

		/// <summary>
		/// Отрисовка скайбокса
		/// </summary>
		internal void Render() {

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

				// Проверка буфферов
				if (GraphicalCaps.ShaderPipeline) {

					// Индексный буффер
					if (indexBuffer == 0) {
						indexBuffer = GL.GenBuffer();
						GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
						GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Length * 2), indices, BufferUsageHint.StaticDraw);
						GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
					}

					// Вершинный буффер
					if (vertexBuffer == 0) {
						vertexBuffer = GL.GenBuffer();
						GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
						GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * 4), vertices, BufferUsageHint.StaticDraw);
						GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
					}

					// Координатный буффер
					if (texCoordBuffer == 0) {
						texCoordBuffer = GL.GenBuffer();
						GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordBuffer);
						GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(coords.Length * 4), coords, BufferUsageHint.StaticDraw);
						GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
					}
				}
			}

			// Установка данных
			if (GraphicalCaps.ShaderPipeline) {

				// Включение шейдера
				SkyboxShader shader = SkyboxShader.Shader;
				shader.DiffuseColor = Diffuse;
				shader.Bind();

				// Включение буфферов
				GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
				GL.VertexAttribPointer(shader.VertexBufferLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
				GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordBuffer);
				GL.VertexAttribPointer(shader.TexCoordBufferLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);

				for (int i = 0; i < 6; i++) {
					if (textures[i] != null) {
						ShaderSystem.EntityMatrix = matrices[i];
						textures[i].Bind();
						shader.Bind();
						GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedShort, 0);
					}
				}

				// Отключение шейдера
				shader.Unbind();

			} else {
				// Включение массивов
				GL.EnableClientState(ArrayCap.VertexArray);
				GL.EnableClientState(ArrayCap.TextureCoordArray);

				GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);
				GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, coords);

				// Отрисовка сторон
				GL.Color3(Diffuse);
				for (int i = 0; i < 6; i++) {
					if (textures[i] != null) {
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
