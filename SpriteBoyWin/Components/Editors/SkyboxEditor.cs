using SpriteBoy.Data;
using SpriteBoy.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SpriteBoy.Engine;
using SpriteBoy.Data.Editing;
using SpriteBoy.Data.Attributes;
using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Components.Rendering;
using SpriteBoy.Files;
using System.IO;
using System.Windows.Forms;
using SpriteBoy.Forms.Editors;

namespace SpriteBoy.Components.Editors {
	
	/// <summary>
	/// Редактор файлов неба
	/// </summary>
	[FileEditor(typeof(SkyboxForm), ".sbsky")]
	public class SkyboxEditor : Editor {

		/// <summary>
		/// Какие типы файлов может создавать
		/// </summary>
		public override Editor.FileCreator[] CreatingFiles {
			get {
				return new FileCreator[]{
					new FileCreator(){
						Name = SharedStrings.SkyFileVisualName,
						Icon = SharedImages.SkyFile,
						Extension = ".sbsky",
						Order = 20
					}
				};
			}
		}

		/// <summary>
		/// Сцена, содержащая объекты
		/// </summary>
		Scene scene;

		/// <summary>
		/// Камера
		/// </summary>
		Camera cam;

		/// <summary>
		/// Коробка
		/// </summary>
		Skybox sky;

		/// <summary>
		/// Коробка с направляющими неба
		/// </summary>
		Entity wireGuides;


		/// <summary>
		/// Загрузка и инициализация
		/// </summary>
		protected override void Load() {

			// Создание сцены
			scene = new Scene();

			// Камера
			cam = new Camera();
			scene.Camera = cam;

			// Создание скайбокса
			sky = new Skybox();
			scene.Sky = sky;

			// Направляющие
			WireCubeComponent c = new WireCubeComponent();
			c.WireColor = Color.Lime;
			c.WireWidth = 3f;
			wireGuides = new Entity();
			wireGuides.AddComponent(c);
			wireGuides.Position = Vec3.Zero;
			scene.Entities.Add(wireGuides);

			// Загрузка данных
			Title = System.IO.Path.GetFileNameWithoutExtension(File.Name) + " - " + Form.Text;

			ChunkedFile cf = new ChunkedFile(File.FullPath);
			if (cf.Root.Name == "Skybox") {
				// Конвертация формы
				SkyboxForm frm = Form as SkyboxForm;

				// Поиск чанка с текстурами
				ChunkedFile.KeyValueChunk textureChunk = (ChunkedFile.KeyValueChunk)cf.Root.GetChunk("Texture");
				if (textureChunk != null) {
					frm.topSkyTexture.File = Project.GetEntry(textureChunk.Values["Top"]);
					frm.bottomSkyTexture.File = Project.GetEntry(textureChunk.Values["Bottom"]);
					frm.leftSkyTexture.File = Project.GetEntry(textureChunk.Values["Left"]);
					frm.rightSkyTexture.File = Project.GetEntry(textureChunk.Values["Right"]);
					frm.frontSkyTexture.File = Project.GetEntry(textureChunk.Values["Front"]);
					frm.backSkyTexture.File = Project.GetEntry(textureChunk.Values["Back"]);
				}

				// Получение чанка для редактора
				ChunkedFile.DataChunk editorChunk = (ChunkedFile.DataChunk)cf.Root.GetChunk("Editor");
				if (editorChunk != null) {
					MemoryStream ms = new MemoryStream(editorChunk.Data);
					BinaryReader br = new BinaryReader(ms);
					float cx = br.ReadSingle();
					float cy = br.ReadSingle();
					cam.Angles = new Vec3(cx, cy, 0f);
					(Form as SkyboxForm).skyGizmoButton.Checked = br.ReadBoolean();
					br.Close();
				}
			}
			Saved = true;
			
		}

		/// <summary>
		/// Сохранение файла
		/// </summary>
		public override void Save() {
			ChunkedFile cf = new ChunkedFile();
			cf.Root.Name = "Skybox";

			// Запись текстур
			ChunkedFile.KeyValueChunk textureChunk = new ChunkedFile.KeyValueChunk("Texture");
			textureChunk.Values.Add("Front",	sky[Skybox.Side.Front] != null ?	sky[Skybox.Side.Front].Link : "");
			textureChunk.Values.Add("Right",	sky[Skybox.Side.Right] != null ?	sky[Skybox.Side.Right].Link : "");
			textureChunk.Values.Add("Back",		sky[Skybox.Side.Back] != null ?		sky[Skybox.Side.Back].Link : "");
			textureChunk.Values.Add("Left",		sky[Skybox.Side.Left] != null ?		sky[Skybox.Side.Left].Link : "");
			textureChunk.Values.Add("Top",		sky[Skybox.Side.Top] != null ?		sky[Skybox.Side.Top].Link : "");
			textureChunk.Values.Add("Bottom",	sky[Skybox.Side.Bottom] != null ?	sky[Skybox.Side.Bottom].Link : "");
			cf.Root.Children.Add(textureChunk);

			// Запись данных редактора
			ChunkedFile.DataChunk editorChunk = new ChunkedFile.DataChunk("Editor");
			MemoryStream ms = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(ms);
			bw.Write((float)cam.Angles.X);
			bw.Write((float)cam.Angles.Y);
			bw.Write((bool)(Form as SkyboxForm).skyGizmoButton.Checked);
			bw.Close();
			editorChunk.Data = ms.ToArray();
			cf.Root.Children.Add(editorChunk);

			// Сохранение
			cf.Save(File.FullPath);
			if (File.Icon!=null) {
				File.Icon.Refresh();
			}
			Saved = true;
		}


		public override void GotFocus() {
			
		}

		public override void LostFocus() {
			
		}

		/// <summary>
		/// Обновление объектов сцены
		/// </summary>
		public override void Update() {
			
		}

		/// <summary>
		/// Рендер сцены
		/// </summary>
		public override void Render() {
			(Form as SkyboxForm).canvas.MakeCurrent();
			scene.Render();
			(Form as SkyboxForm).canvas.Swap();
		}

		/// <summary>
		/// Изменение размера кадра
		/// </summary>
		/// <param name="size"></param>
		public void ViewportChanded(Size size) {
			if (cam!=null) {
				cam.Size = new Vec2(
					size.Width, size.Height
				);
			}
		}

		/// <summary>
		/// Текстура изменена
		/// </summary>
		/// <param name="side">Сторона</param>
		/// <param name="file">Имя файла</param>
		public void TextureChanged(Skybox.Side side, Project.Entry file) {
			if (file!=null) {
				bool needTex = true;
				if (sky[side]!=null) {
					if (sky[side].Link == file.ProjectPath) {
						needTex = false;
					}
				}
				if (needTex) {
					sky[side] = new Texture(file.ProjectPath, Texture.LoadingMode.Queued);
				}
			} else {
				sky[side] = null;
			}

			Saved = false;
		}

		/// <summary>
		/// Изменена видимость гизмо
		/// </summary>
		/// <param name="state">Состояние</param>
		public void GizmoChanged(bool state) {
			wireGuides.Visible = state;
			Saved = false;
		}

		/// <summary>
		/// Поворот камеры
		/// </summary>
		/// <param name="speed">Скорость поворота</param>
		public void RotateCamera(PointF speed) {
			Vec3 rot = cam.Angles + new Vec3(speed.Y * 0.1f, speed.X * 0.1f, 0f);
			if (Math.Abs(rot.X)>90) {
				rot.X = Math.Sign(rot.X) * 90f;
			}
			cam.Angles = rot;
		}
	}
}
