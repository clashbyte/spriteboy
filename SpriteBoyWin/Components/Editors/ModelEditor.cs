using SpriteBoy.Data.Editing;
using SpriteBoy.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteBoy.Forms.Editors;
using System.Drawing;
using SpriteBoy.Data.Types;
using SpriteBoy.Engine;
using SpriteBoy.Engine.Components.Rendering;
using System.IO;
using SpriteBoy.Data.Formats;

namespace SpriteBoy.Components.Editors {

	[FileEditor(typeof(ModelForm), ".s3d", ".md3", ".md2", ".x", ".3ds")]
	public class ModelEditor : Editor {


		/// <summary>
		/// Сцена, содержащая объекты
		/// </summary>
		Scene scene;

		/// <summary>
		/// Камера
		/// </summary>
		Camera cam;

		/// <summary>
		/// Носитель для камеры, чтобы вращать её вокруг центра
		/// </summary>
		Entity camHolder;

		/// <summary>
		/// Сама модель
		/// </summary>
		Entity model;

		/// <summary>
		/// Сетка
		/// </summary>
		Entity grid;

		/// <summary>
		/// Дальность камеры
		/// </summary>
		float range;

		/// <summary>
		/// Загрузка данных
		/// </summary>
		protected override void Load() {
			UpdateTitle();
			if (scene == null) {

				scene = new Scene();

				camHolder = new Entity();
				camHolder.Position = Vec3.UnitY * 0.5f;
				camHolder.Angles = new Vec3(30, 135, 0);
				scene.Entities.Add(camHolder);

				cam = new Camera();
				scene.Camera = cam;
				cam.Parent = camHolder;

				grid = new Entity();
				grid.AddComponent(new WireGridComponent() {
					CellCount = 30,
					CellSize = 0.1f,
					GroupedCells = 5
				});
				grid.AddComponent(new WireCubeComponent() {
					WireColor = Color.Green,
					Position = Vec3.UnitY*0.5f
				});
				scene.Entities.Add(grid);
			}

			// Загрузка модели
			if (model!=null) {
				scene.Entities.Remove(model);
			}
			model = ModelLoader.FromFile(File.ProjectPath);
			scene.Entities.Add(model);

			range = ModelLoader.MaxRange * 3f;
			cam.LocalPosition = Vec3.UnitZ * -range;



		}

		/// <summary>
		/// Сохранение модели
		/// </summary>
		public override void Save() {
			
		}

		/// <summary>
		/// Обновление логики
		/// </summary>
		public override void Update() {
			float pos = -cam.LocalPosition.Z;
			if (pos != range) {
				float dst = range - pos;
				if (Math.Abs(dst) > 0.0001) {
					pos += dst * 0.3f;
				}else{
					pos += dst;
				}
				cam.LocalPosition = Vec3.UnitZ * -pos;
			}
		}

		/// <summary>
		/// Рендер сцены
		/// </summary>
		public override void Render() {
			Form.Canvas.MakeCurrent();
			scene.Render();
			Form.Canvas.Swap();
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
		/// Поворот камеры
		/// </summary>
		/// <param name="speed">Скорость поворота</param>
		public void RotateCamera(PointF speed) {
			Vec3 rot = camHolder.Angles + new Vec3(speed.Y * 0.1f, speed.X * 0.1f, 0f);
			if (Math.Abs(rot.X) > 90) {
				rot.X = Math.Sign(rot.X) * 90f;
			}
			camHolder.Angles = rot;
		}

		/// <summary>
		/// Зум камеры
		/// </summary>
		/// <param name="side">Направление</param>
		public void ZoomCamera(float side) {
			float mul = 0.8f;
			if (side<0) {
				mul = 1.2f;
			}
			for (int i = 0; i < Math.Abs(side); i++) {
				range *= mul;
			}
			if (range<0.03f) {
				range = 0.03f;
			}else if(range>80f){
				range = 80f;
			}
		}
	}
}
