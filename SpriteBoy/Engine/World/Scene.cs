using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using SpriteBoy.Data.Types;

namespace SpriteBoy.Engine.World {

	/// <summary>
	/// Сцена, содержащая объекты
	/// </summary>
	public class Scene {

		/// <summary>
		/// Количество миллисекунд на кадр
		/// </summary>
		const int FRAME_TICKS = 16;

		/// <summary>
		/// Список всех объектов сцены
		/// </summary>
		public List<Entity> Entities {
			get;
			private set;
		}

		/// <summary>
		/// Скайбокс для отрисовки
		/// </summary>
		public Skybox Sky { get; set; }

		/// <summary>
		/// Камера для отрисовки сцены
		/// </summary>
		public Camera Camera { get; set; }

		/// <summary>
		/// Фоновый цвет сцены
		/// </summary>
		public Color BackColor { get; set; }

		/// <summary>
		/// Обновление находится в режиме паузы
		/// </summary>
		public bool Paused { get; set; }

		/// <summary>
		/// Время последнего обновления сцены
		/// </summary>
		protected int LastUpdateTime;

		/// <summary>
		/// Конструктор сцены
		/// </summary>
		public Scene() {
			Entities = new List<Entity>();
			BackColor = Color.FromArgb(40, 40, 40);
		}

		/// <summary>
		/// Обновление логики
		/// </summary>
		public void Update() {
			// Количество тиков для обновления
			int times = 1;
			if (LastUpdateTime == 0) {
				LastUpdateTime = Environment.TickCount - FRAME_TICKS;
			} else {
				times = (Environment.TickCount - LastUpdateTime) / FRAME_TICKS;
			}

			// Обновление всех предметов
			for (int i = 0; i < times; i++) {
				if (!Paused) {
					foreach (Entity e in Entities) {
						e.Update();
					}
				}
				LastUpdateTime += FRAME_TICKS;
			}
		}

		/// <summary>
		/// Отрисовка всех объектов для каждой камеры
		/// </summary>
		public void Render() {

			// Очистка сцены
			GL.ClearColor(BackColor);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			
			// Параметры
			GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Back);

			// Отрисовка камеры
			if (Camera!=null) {
				Camera.Setup();
				if (Sky!=null) {
					Camera.LoadSkyMatrix();
					Sky.Render();
				}
				Camera.LoadMatrix();
				GL.Enable(EnableCap.DepthTest);
				GL.DepthFunc(DepthFunction.Lequal);
			}

			// Отрисовка всех предметов
			foreach (Entity e in Entities) {
				if (e.Visible) {
					e.Render();
				}
			}

			// Отключение состояний
			GL.Disable(EnableCap.DepthTest);
		}

	}
}
