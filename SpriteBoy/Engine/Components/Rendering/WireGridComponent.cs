using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Pipeline;
using SpriteBoy.Data.Shaders;
using SpriteBoy.Data;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Сетка
	/// </summary>
	public class WireGridComponent : EntityComponent, IRenderable {

		/// <summary>
		/// Основной цвет
		/// </summary>
		public Color WireMainColor {
			get {
				return mainColor;
			}
			set {
				mainColor = value;
				needBuffer = true;
			}
		}

		/// <summary>
		/// Цвет акцентных линий
		/// </summary>
		public Color WireAccentColor {
			get {
				return accentColor;
			}
			set {
				accentColor = value;
				needBuffer = true;
			}
		}

		/// <summary>
		/// Толщина сетки
		/// </summary>
		public float WireWidth {
			get;
			set;
		}

		/// <summary>
		/// Количество клеток с каждой стороны
		/// </summary>
		public int CellCount {
			get {
				return cellCount;
			}
			set {
				if (value != cellCount) {
					cellCount = value;
					needBuffer = true;
					RebuildParentCull();
				}
			}
		}

		/// <summary>
		/// Размер одной клетки
		/// </summary>
		public float CellSize {
			get {
				return cellSize;
			}
			set {
				if (cellSize != value) {
					cellSize = value;
					needBuffer = true;
					RebuildParentCull();
				}
			}
		}

		/// <summary>
		/// Размер одной клетки
		/// </summary>
		public int GroupedCells {
			get {
				return groupedCells;
			}
			set {
				if (groupedCells != value) {
					groupedCells = value;
					needBuffer = true;
				}
			}
		}

		/// <summary>
		/// Скрытое количество клеток
		/// </summary>
		int cellCount;

		/// <summary>
		/// Скрытый размер клетки
		/// </summary>
		float cellSize;

		/// <summary>
		/// Скрытое количество клеток в группе
		/// </summary>
		int groupedCells;

		/// <summary>
		/// Цвета линий
		/// </summary>
		Color mainColor, accentColor;

		/// <summary>
		/// Требуется ли перестройка буффера
		/// </summary>
		bool needBuffer;

		/// <summary>
		/// Сфера отсечения
		/// </summary>
		CullSphere cull;

		// Скрытые буфферы
		float[] vertexArray;
		float[] colorArray;
		
		// Скрытые буфферы
		int vertexBuffer;
		int colorBuffer;

		/// <summary>
		/// Конструктор
		/// </summary>
		public WireGridComponent() {
			cellSize = 1f;
			cellCount = 30;
			groupedCells = 10;
			WireWidth = 0.5f;
			accentColor = Color.FromArgb(80, 80, 80);
			mainColor = Color.FromArgb(60, 60, 60);
			cull = new CullSphere();
			needBuffer = true;
		}

		/// <summary>
		/// Коробка для отсечения
		/// </summary>
		internal override CullBox GetCullingBox() {
			Vec3 sz = new Vec3(1f, 0f, 1f) * cellCount * cellSize;
			return new CullBox() { 
				Max = sz,
				Min = -sz
			};
		}

		/// <summary>
		/// Отрисовка
		/// </summary>
		internal override void Render() {
			if (needBuffer) {
				RebuildBuffer();
				needBuffer = false;
			}

			GL.LineWidth(WireWidth);
			GL.BindTexture(TextureTarget.Texture2D, 0);

			if (GraphicalCaps.ShaderPipeline) {

				ShaderSystem.CheckVertexBuffer(ref vertexBuffer, vertexArray, BufferUsageHint.StaticDraw);
				ShaderSystem.CheckVertexBuffer(ref colorBuffer, colorArray, BufferUsageHint.StaticDraw);

				WireGridShader shader = WireGridShader.Shader;
				shader.Bind();
				GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
				GL.VertexAttribPointer(shader.VertexBufferLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
				GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);
				GL.VertexAttribPointer(shader.ColorBufferLocation, 4, VertexAttribPointerType.Float, false, 0, 0);
				GL.DrawArrays(BeginMode.Lines, 0, vertexArray.Length / 3);
				shader.Unbind();
			} else {
				GL.EnableClientState(ArrayCap.VertexArray);
				GL.EnableClientState(ArrayCap.ColorArray);
				GL.VertexPointer(3, VertexPointerType.Float, 0, vertexArray);
				GL.ColorPointer(4, ColorPointerType.Float, 0, colorArray);
				GL.DrawArrays(BeginMode.Lines, 0, vertexArray.Length / 3);
				GL.DisableClientState(ArrayCap.ColorArray);
				GL.DisableClientState(ArrayCap.VertexArray);
			}
		}

		/// <summary>
		/// Перестроение буфферов
		/// </summary>
		void RebuildBuffer() {

			if (GraphicalCaps.ShaderPipeline) {
				GL.DeleteBuffer(vertexBuffer);
				GL.DeleteBuffer(colorBuffer);
			}

			int count = (cellCount * 2 + 1) * 4;
			vertexArray = new float[count*3];
			colorArray = new float[count*4];

			float maxPos = (float)cellCount * cellSize;
			int idx = 0;
			int cidx = 0;
			for (int i = 0; i <= cellCount; i++) {

				bool isAccent = false;
				if (groupedCells > 0) {
					isAccent = (i % groupedCells) == 0;
				}
				for (int d = 0; d < 2; d++ ) {
					if (d==1 && i == 0) {
						break;
					}

					float mul = d == 1 ? -1f : 1f;

					// Вершинный буффер
					float ps = (float)i * cellSize;
					vertexArray[idx + 0] = ps * mul;
					vertexArray[idx + 2] = maxPos;
					vertexArray[idx + 3] = ps * mul;
					vertexArray[idx + 5] = -maxPos;

					vertexArray[idx + 6] = maxPos;
					vertexArray[idx + 8] = ps * mul;
					vertexArray[idx + 9] = -maxPos;
					vertexArray[idx + 11] = ps * mul;

					// Цвета
					for (int cl = 0; cl < 16; cl += 4) {
						colorArray[cidx + cl + 0] = isAccent ? (float)accentColor.R / 255f : (float)mainColor.R / 255f;
						colorArray[cidx + cl + 1] = isAccent ? (float)accentColor.G / 255f : (float)mainColor.G / 255f;
						colorArray[cidx + cl + 2] = isAccent ? (float)accentColor.B / 255f : (float)mainColor.B / 255f;
						colorArray[cidx + cl + 3] = isAccent ? (float)accentColor.A / 255f : (float)mainColor.A / 255f;
					}

					idx += 12;
					cidx += 16;
				}
			}

		}
	}
}
