using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Data;
using SpriteBoy.Engine.Pipeline;
using SpriteBoy.Data;

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

			// Сборка списка объектов
			List<EntityComponent> updateable = new List<EntityComponent>();
			foreach (Entity e in Entities) {
				updateable.AddRange(e.GetLogicalComponents());
			}

			// Обновление всех предметов
			for (int i = 0; i < times; i++) {
				if (!Paused) {
					foreach (EntityComponent e in updateable) {
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

			// Расположение камеры
			Vec3 cameraPos = Camera.Position;

			// Сборка объектов
			bool needAlphaPass = false, needBlendPass = false;
			List<RenderableGroup> opaqueGroups = new List<RenderableGroup>();
			List<RenderableGroup> alphaTestGroups = new List<RenderableGroup>();
			List<RangedRenderableGroup> alphaBlendGroups = new List<RangedRenderableGroup>();
			
			foreach (Entity e in Entities) {
				if (e.Visible) {
					Matrix4 entityMatrix = e.RenditionMatrix;
					RenderableGroup opaque = new RenderableGroup();
					RenderableGroup alphaTest = new RenderableGroup();
					RangedRenderableGroup alphaBlend = new RangedRenderableGroup();

					IEnumerable<EntityComponent> components = e.GetVisualComponents();
					foreach (EntityComponent c in components) {
						switch (c.RenditionPass) {
							case EntityComponent.TransparencyPass.AlphaTest:
								alphaTest.Components.Add(c);
								break;
							case EntityComponent.TransparencyPass.Blend:
								alphaBlend.Components.Add(c);
								break;
							default:
								opaque.Components.Add(c);
								break;
						}
					}

					if (opaque.Components.Count > 0) {
						opaque.Matrix = entityMatrix;
						opaqueGroups.Add(opaque);
					}
					if (alphaTest.Components.Count > 0) { 
						alphaTest.Matrix = entityMatrix;
						alphaTestGroups.Add(alphaTest);
						needAlphaPass = true;
					}
					if (alphaBlend.Components.Count > 0) {
						alphaBlend.Matrix = entityMatrix;
						alphaBlend.Distance = (e.Position - cameraPos).LengthSquared;
						alphaBlendGroups.Add(alphaBlend);
						needBlendPass = true;
					}
				}
			}
			

			// Непрозрачный проход
			foreach (RenderableGroup g in opaqueGroups) {
				g.Render();
			}
			
			// Альфа-проходы
			if (needAlphaPass || needBlendPass) {
				// Включение смешивания
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

				// Альфатест-проход
				if (needAlphaPass) {

					// Включение альфатеста
					if (GraphicalCaps.ShaderPipeline) {
						ShaderSystem.IsAlphaTest = true;
					} else {
						GL.Enable(EnableCap.AlphaTest);
						GL.AlphaFunc(AlphaFunction.Gequal, 0.9f);
					}

					// Отрисовка поверхностей
					foreach (RenderableGroup g in alphaTestGroups) {
						g.Render();
					}

					// Отключение альфатеста
					if (GraphicalCaps.ShaderPipeline) {
						ShaderSystem.IsAlphaTest = false;
					} else {
						GL.Disable(EnableCap.AlphaTest);
					}
				}

				// Отключение смешивания
				if (needBlendPass) {

					// Сначала - сортировка всех объектов от дальних к ближним
					alphaBlendGroups.Sort((a, b) => {
						return b.Distance.CompareTo(a.Distance);
					});

					// Отключение записи в буфер глубины
					GL.DepthMask(false);

					// Отрисовка всех поверхностей
					foreach (RangedRenderableGroup rg in alphaBlendGroups) {
						rg.Render();
					}

					// Включение буффера глубины
					GL.DepthMask(true);
				}

				// Отключение смешивания
				GL.Disable(EnableCap.Blend);
			}
			
			// Отключение состояний
			GL.Disable(EnableCap.DepthTest);
		}

		/// <summary>
		/// Группа для отрисовки
		/// </summary>
		class RenderableGroup {
			/// <summary>
			/// Матрица отрисовки
			/// </summary>
			public Matrix4 Matrix;
			/// <summary>
			/// Видимые компоненты
			/// </summary>
			public List<EntityComponent> Components;

			/// <summary>
			/// Конструктор группы
			/// </summary>
			public RenderableGroup() {
				Components = new List<EntityComponent>();
			}

			/// <summary>
			/// Отрисовка группы
			/// </summary>
			public void Render() {

				// Отправка матрицы
				if (GraphicalCaps.ShaderPipeline) {
					ShaderSystem.EntityMatrix = Matrix;
				} else {
					GL.PushMatrix();
					GL.MultMatrix(ref Matrix);
				}
				
				// Рендер
				foreach (EntityComponent c in Components) {
					RenderSingle(c);
				}

				// Выгрузка матрицы
				if (!GraphicalCaps.ShaderPipeline) {
					GL.PopMatrix();
				}
			}

			/// <summary>
			/// Рендер одного объекта
			/// </summary>
			/// <param name="c">Объект</param>
			protected virtual void RenderSingle(EntityComponent c) {
				c.Render();
			}
		}

		/// <summary>
		/// Группа отрисовки, сортируемая по удалённости
		/// </summary>
		class RangedRenderableGroup : RenderableGroup {
			/// <summary>
			/// Расстояние до камеры
			/// </summary>
			public float Distance;

			/// <summary>
			/// Отрисовка одного объекта
			/// </summary>
			/// <param name="c">Объект</param>
			protected override void RenderSingle(EntityComponent c) {
				switch (c.RenditionBlending) {
					case EntityComponent.BlendingMode.AlphaChannel:
						GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
						break;
					case EntityComponent.BlendingMode.Brightness:
						GL.BlendFunc(BlendingFactorSrc.DstColor, BlendingFactorDest.Zero);
						break;
					case EntityComponent.BlendingMode.Add:
						GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One);
						break;
					case EntityComponent.BlendingMode.Multiply:
						GL.BlendFunc(BlendingFactorSrc.DstColor, BlendingFactorDest.Zero);
						break;
					case EntityComponent.BlendingMode.ForceOpaque:
						GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.Zero);
						break;

				}
				base.RenderSingle(c);
			}
		}
	}
}
