using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Data.Types;

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
		float[] vertexBuffer;
		byte[] colorBuffer;

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
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.ColorArray);

			GL.VertexPointer(3, VertexPointerType.Float, 0, vertexBuffer);
			GL.ColorPointer(3, ColorPointerType.UnsignedByte, 0, colorBuffer);

			GL.DrawArrays(BeginMode.Lines, 0, vertexBuffer.Length/3);

			GL.DisableClientState(ArrayCap.ColorArray);
			GL.DisableClientState(ArrayCap.VertexArray);
		}

		/// <summary>
		/// Перестроение буфферов
		/// </summary>
		void RebuildBuffer() {

			int count = (cellCount * 2 + 1) * 4;
			vertexBuffer = new float[count*3];
			colorBuffer = new byte[count*3];

			float maxPos = (float)cellCount * cellSize;
			int idx = 0;
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
					vertexBuffer[idx + 0] = ps * mul;
					vertexBuffer[idx + 2] = maxPos;
					vertexBuffer[idx + 3] = ps * mul;
					vertexBuffer[idx + 5] = -maxPos;

					vertexBuffer[idx + 6] = maxPos;
					vertexBuffer[idx + 8] = ps * mul;
					vertexBuffer[idx + 9] = -maxPos;
					vertexBuffer[idx + 11] = ps * mul;

					// Цвета
					for (int cl = 0; cl < 12; cl += 3) {
						colorBuffer[idx + cl + 0] = isAccent ? accentColor.R : mainColor.R;
						colorBuffer[idx + cl + 1] = isAccent ? accentColor.G : mainColor.G;
						colorBuffer[idx + cl + 2] = isAccent ? accentColor.B : mainColor.B;
					}

					idx += 12;
				}
			}

		}
	}
}
