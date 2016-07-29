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

namespace SpriteBoy.Components.Editors {

	[FileEditor(typeof(ModelForm), ".s3d")]
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
				camHolder.Position = Vec3.Zero;
				camHolder.Angles = new Vec3(30, 45, 0);
				scene.Entities.Add(camHolder);

				cam = new Camera();
				scene.Camera = cam;
				cam.Parent = camHolder;

				model = new Entity();
				model.AddComponent(new WireGridComponent());
				scene.Entities.Add(model);
			}


			BinaryReader f = new BinaryReader(new FileStream(File.FullPath, FileMode.Open, FileAccess.Read));
			f.BaseStream.Position = 0;
			int surfs = f.ReadUInt16();
			for (int s = 0; s < surfs; s++) {
				int tris = f.ReadUInt16();

				Vec3[] positions = new Vec3[tris*3];
				ushort[] indices = new ushort[tris*3];

				for (int t = 0; t < tris*3; t++) {

					Vec3 pos = new Vec3();
					pos.X = f.ReadSingle() * 0.05f;
					pos.Y = f.ReadSingle() * 0.05f;
					pos.Z = f.ReadSingle() * 0.05f;
					positions[t] = pos;

					Vec2 uv = new Vec2();
					uv.X = f.ReadSingle();
					uv.Y = f.ReadSingle();
				}

				for (int i = 0; i < indices.Length; i++) {
					indices[i] = (ushort)i;
				}

				MeshComponent mc = new MeshComponent();
				mc.Indices = indices;
				mc.Vertices = positions;
				model.AddComponent(mc);
			}


			range = 50f;
			cam.LocalPosition = Vec3.UnitZ * -range;

			f.Close();


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
			if (range<0.1f) {
				range = 0.1f;
			}else if(range>250f){
				range = 250f;
			}
		}
	}
}
