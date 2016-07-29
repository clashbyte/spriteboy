using SpriteBoy.Data.Attributes;
using SpriteBoy.Data.Editing;
using SpriteBoy.Data.Types;
using SpriteBoy.Engine;
using SpriteBoy.Engine.Components.Rendering;
using SpriteBoy.Forms.Editors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Editors {

	/// <summary>
	/// Редактор для файлов изображений
	/// </summary>
	[FileEditor(typeof(ImageForm), ".png", ".jpg", ".jpeg", ".gif", ".bmp")]
	public class ImageEditor : Editor {

		/// <summary>
		/// Сцена для рендера
		/// </summary>
		Scene scene;

		/// <summary>
		/// Ортогональная камера
		/// </summary>
		Camera cam;

		/// <summary>
		/// Сетка
		/// </summary>
		Entity grid;

		/// <summary>
		/// Квады для обычного просмотра
		/// </summary>
		Entity singleQuad;

		/// <summary>
		/// Квады для повторяющегося просмотра
		/// </summary>
		Entity wrapQuad;

		/// <summary>
		/// Текущий зум
		/// </summary>
		float zoom;

		/// <summary>
		/// Изменяемая текстура
		/// </summary>
		Texture tex;

		/// <summary>
		/// Загрузка изображения
		/// </summary>
		protected override void Load() {
			UpdateTitle();
			if (scene == null) {

				// Создание сцены и камеры
				scene = new Scene();
				cam = new Camera();
				cam.Range = new Vec2(-5, 10);
				cam.Zoom = 6f;
				cam.Projection = Camera.ProjectionMode.Ortho;
				scene.Camera = cam;
				zoom = 6f;

				// Создание сетки
				grid = new Entity();
				grid.Position = new Vec3(-0.5f, 0.5f, 9);
				grid.Angles = new Vec3(-90, 0, 0);
				grid.AddComponent(new WireGridComponent() {
					CellCount = 40,
					CellSize = 0.1f,
					WireWidth = 0.5f
				});
				scene.Entities.Add(grid);
			}

			// Загрузка текстуры
			tex = new Texture(File.ProjectPath, Texture.LoadingMode.Instant);

			float mulX = 1f / (float)tex.Width;
			float mulY = 1f / (float)tex.Height;
			float mul = mulX < mulY ? mulX : mulY;
			float halfX = (float)tex.Width * mul / 2f;
			float halfY = (float)tex.Height * mul / 2f;

			if (singleQuad == null) {
				singleQuad = new Entity();
				singleQuad.AddComponent(new MeshComponent() {
					AlphaBlend = true
				});
				scene.Entities.Add(singleQuad);
			}
			MeshComponent singleMesh = singleQuad.GetComponent<MeshComponent>();
			singleMesh.Vertices = new Vec3[]{
				new Vec3(-halfX,  halfY, 0),
				new Vec3( halfX,  halfY, 0),
				new Vec3(-halfX, -halfY, 0),
				new Vec3( halfX, -halfY, 0),
			};
			singleMesh.TexCoords = new Vec2[]{
				new Vec2(0f, 0f),
				new Vec2(1f, 0f),
				new Vec2(0f, 1f),
				new Vec2(1f, 1f),
			};
			singleMesh.Indices = new ushort[]{
				0, 1, 2,
				1, 3, 2
			};
			singleMesh.Texture = tex;

		}

		/// <summary>
		/// Сохранение изображения
		/// </summary>
		public override void Save() {
			
		}

		/// <summary>
		/// Обновление редактора
		/// </summary>
		public override void Update() {
			if (zoom != cam.Zoom) {
				float dst = zoom - cam.Zoom;
				if (Math.Abs(dst) > 0.0001) {
					cam.Zoom += dst * 0.3f;
				} else {
					cam.Zoom += dst;
				}
			}
		}

		/// <summary>
		/// Рендер изображения
		/// </summary>
		public override void Render() {
			Form.Canvas.MakeCurrent();
			scene.Render();
			Form.Canvas.Swap();
		}

		/// <summary>
		/// Событие файла
		/// </summary>
		/// <param name="en"></param>
		/// <param name="ev"></param>
		public override void ProjectEntryEvent(Project.Entry en, Project.FileEvent ev) {
			base.ProjectEntryEvent(en, ev);
			if (closed) {
				return;
			}
		}

		/// <summary>
		/// Изменение размера кадра
		/// </summary>
		/// <param name="size"></param>
		public void ViewportChanded(Size size) {
			if (cam != null) {
				cam.Size = new Vec2(
					size.Width, size.Height
				);
			}
		}

		/// <summary>
		/// Движение камеры
		/// </summary>
		/// <param name="speed">Скорость движения</param>
		public void MoveCamera(PointF speed) {
			float mul = (1f / cam.Zoom) * (Camera.ORTHO_SIZE / (float)Form.Canvas.Height);
			cam.Position += new Vec3(-speed.X * mul, speed.Y * mul, 0f);
			grid.Position = new Vec3((float)Math.Floor(cam.Position.X)-0.5f, (float)Math.Floor(cam.Position.Y)+0.5f, 9f);
		}

		/// <summary>
		/// Зум камеры
		/// </summary>
		/// <param name="side">Направление</param>
		public void ZoomCamera(float side) {
			float mul = 0.8f;
			if (side > 0) {
				mul = 1.2f;
			}
			for (int i = 0; i < Math.Abs(side); i++) {
				zoom *= mul;
			}
			if (zoom < 4f) {
				zoom = 4f;
			} else if (zoom > 30f) {
				zoom = 30f;
			}
		}
	}
}
