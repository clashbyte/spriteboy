using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace SpriteBoy.Engine {

	/// <summary>
	/// Сцена, содержащая объекты
	/// </summary>
	public class Scene {

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
		/// Конструктор сцены
		/// </summary>
		public Scene() {
			Entities = new List<Entity>();
			BackColor = Color.FromArgb(40, 40, 40);
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

			GL.Disable(EnableCap.DepthTest);
		}

	}
}
