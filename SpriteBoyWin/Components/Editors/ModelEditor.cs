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
using SpriteBoy.Engine.Components.Volumes;
using SpriteBoy.Controls;
using SpriteBoy.Engine.World;
using SpriteBoy.Engine.Pipeline;

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
		Camera cam, fpsCam;

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
		Entity grid, fpsGrid;

		/// <summary>
		/// Гизмо одной клетки
		/// </summary>
		WireCubeComponent cellGizmo;

		/// <summary>
		/// Дальность камеры
		/// </summary>
		float range;

		/// <summary>
		/// Поверхности меша
		/// </summary>
		MeshComponent[] surfaces;

		/// <summary>
		/// Все тримеши для поиска пересечений
		/// </summary>
		TrimeshVolumeComponent[] volumes;

		/// <summary>
		/// Данные о поверхностях
		/// </summary>
		SurfaceData[] surfData;

		/// <summary>
		/// Луч для поиска вершин
		/// </summary>
		Ray ray;

		/// <summary>
		/// Изменяется ли поверхность кодом
		/// </summary>
		bool surfaceCodeChange;

		/// <summary>
		/// Выбранная поверхность
		/// </summary>
		int selectedSurface;

		/// <summary>
		/// Флаг, что текстуру необходимо перезагрузить
		/// </summary>
		bool forceTextureReload;

		/// <summary>
		/// Индекс сурфейса, по которому таскают текстуру
		/// </summary>
		int textureHoveredSurface;

		/// <summary>
		/// Перетаскиваемый файл
		/// </summary>
		Project.Entry draggingFile;

		/// <summary>
		/// Перетаскиваемая текстура
		/// </summary>
		Texture draggingTexture;

		/// <summary>
		/// Загрузка данных
		/// </summary>
		protected override void Load() {
			UpdateTitle();
			if (scene == null) {

				scene = new Scene();

				camHolder = new Entity();
				camHolder.Position = Vec3.UnitY * 0.3f;
				camHolder.Angles = new Vec3(30, 135, 0);
				scene.Entities.Add(camHolder);

				cam = new Camera();
				scene.Camera = cam;
				cam.Parent = camHolder;

				fpsCam = new Camera();
				fpsCam.Range = new Vec2(0.001f, 160f);
				fpsCam.Position = Vec3.Zero;

				grid = new Entity();
				grid.AddComponent(new WireGridComponent() {
					CellCount = 30,
					CellSize = 0.1f,
					GroupedCells = 5
				});
				scene.Entities.Add(grid);

				cellGizmo = new WireCubeComponent() {
					WireColor = Color.Green,
					Position = Vec3.UnitY * 0.5f
				};
				grid.AddComponent(cellGizmo);

				fpsGrid = new Entity();
				fpsGrid.AddComponent(new WireGridComponent() {
					CellCount = 60,
					CellSize = 10f,
					GroupedCells = 5
				});
				fpsGrid.Position = Vec3.UnitZ * 99;
				fpsGrid.Angles = new Vec3(90, 0, 0);
				scene.Entities.Add(fpsGrid);
			}

			// Загрузка модели
			if (model!=null) {
				scene.Entities.Remove(model);
			}
			model = ModelLoader.FromFile(File.ProjectPath);
			model.Position = Vec3.Zero;
			scene.Entities.Add(model);

			// Дистанция до модели
			range = 2f;
			cam.LocalPosition = Vec3.UnitZ * -range;
			selectedSurface = -1;
			textureHoveredSurface = -1;

			// Поиск всех поверхностей модели
			surfaces = model.GetComponents<MeshComponent>();
			volumes = new TrimeshVolumeComponent[surfaces.Length];
			for (int i = 0; i < surfaces.Length; i++) {
				volumes[i] = new TrimeshVolumeComponent() {
					Vertices = surfaces[i].Vertices,
					Indices = surfaces[i].Indices
				};
				model.AddComponent(volumes[i]);
			}

			// Данные о поверхностях
			surfData = new SurfaceData[surfaces.Length];
			for (int i = 0; i < surfaces.Length; i++) {
				surfData[i] = new SurfaceData();
			}

			// Создание списка
			int idx = 0;
			NSListView.NSListViewItem[] surfItems = new NSListView.NSListViewItem[surfaces.Length];
			foreach (MeshComponent mc in surfaces) {
				NSListView.NSListViewItem itm = new NSListView.NSListViewItem();
				itm.SubItems = new List<NSListView.NSListViewSubItem>(new NSListView.NSListViewSubItem[]{
					new NSListView.NSListViewSubItem() { Text = (idx+1).ToString() },
					new NSListView.NSListViewSubItem() { Text = "" },
					new NSListView.NSListViewSubItem() { Text = (mc.Vertices.Length).ToString() },
					new NSListView.NSListViewSubItem() { Text = (mc.Indices.Length/3).ToString() },
				});
				surfItems[idx] = itm;
				idx++;
			}
			(Form as ModelForm).surfacesList.Items = surfItems;

			// Создание луча
			ray = new Ray(new Entity[] { model });

			// Настройка интерфейса
			(Form as ModelForm).cellGizmoButton.Checked = false;
			(Form as ModelForm).firstPersonButton.Checked = false;

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
			scene.Update();
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
				fpsCam.Size = cam.Size;
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

		/// <summary>
		/// Изменён режим просмотра
		/// </summary>
		/// <param name="mode">True если нужен режим из глаз</param>
		public void ViewModeChanged(bool mode) {
			if (mode) {
				scene.Camera = fpsCam;
			}else{
				scene.Camera = cam;
			}
			grid.Visible = !mode;
			fpsGrid.Visible = mode;
		}

		/// <summary>
		/// Изменен режим отображения вспомогательного куба
		/// </summary>
		/// <param name="mode">True если отображать</param>
		public void CellGizmoChanged(bool mode) {
			cellGizmo.Enabled = mode;
		}

		/// <summary>
		/// Вывод значений для сурфейса
		/// </summary>
		/// <param name="surf">Индекс сурфейса</param>
		public void ShowSurfaceParams(int surf) {
			surfaceCodeChange = true;
			selectedSurface = surf;
			ModelForm mf = Form as ModelForm;
			mf.textureFile.File = surfData[surf].file;
			mf.surfaceColorButton.SelectedColor = surfData[surf].tint;
			mf.surfaceOpaque.Checked = surfData[surf].opaque;
			mf.surfaceUnlit.Checked = surfData[surf].unlit;
			surfaceCodeChange = false;
		}

		/// <summary>
		/// Изменена текстура меша
		/// </summary>
		public void SurfaceTextureChanged(Project.Entry file) {
			if (surfData!=null && !surfaceCodeChange) {
				if (file != null) {
					bool needTex = forceTextureReload;
					if (!needTex) {
						needTex = file != surfData[selectedSurface].file;
					}
					if (needTex) {
						surfData[selectedSurface].file = file;
						surfData[selectedSurface].tex = new Texture(surfData[selectedSurface].file.ProjectPath, Texture.LoadingMode.Queued);
						surfData[selectedSurface].tex.ApplyMetaConfig(file.Meta);
					}
				} else {
					surfData[selectedSurface].file = null;
					surfData[selectedSurface].tex = null;
				}
				ApplySurfaceData();
				UpdateSurfaceItemText(selectedSurface, file);
				Saved = false;
			}
		}

		/// <summary>
		/// Изменены флаги поверхности
		/// </summary>
		/// <param name="isOpaque"></param>
		/// <param name="isUnlit"></param>
		public void SurfaceFlagsChanged(bool isOpaque, bool isUnlit) {
			if (surfData!=null && !surfaceCodeChange) {
				surfData[selectedSurface].opaque = isOpaque;
				surfData[selectedSurface].unlit = isUnlit;
				ApplySurfaceData();
				Saved = false;
			}
		}

		/// <summary>
		/// Вхождение перетаскивания текстуры
		/// </summary>
		public void TextureDragEntered(Project.Entry file) {
			draggingFile = file;
			draggingTexture = new Texture(file.ProjectPath, Texture.LoadingMode.Queued);
		}

		/// <summary>
		/// Перетаскивание текстуры
		/// </summary>
		public bool TextureDragOver(int x, int y) {
			int sid = GetSurfaceUnderCoords(x, y);
			if (sid != textureHoveredSurface) {
				for (int i = 0; i < surfaces.Length; i++) {
					if (i == sid) {
						surfaces[i].Texture = draggingTexture;
					} else {
						surfaces[i].Texture = surfData[i].tex;
					}
				}
				textureHoveredSurface = sid;
			}
			return sid>=0;
		}

		/// <summary>
		/// Перетаскивание текстуры отменено
		/// </summary>
		public void TextureDragCanceled() {
			draggingTexture = null;
			draggingFile = null;
			textureHoveredSurface = -1;
		}

		/// <summary>
		/// Текстура сброшена
		/// </summary>
		public void TextureDropped() {
			if (textureHoveredSurface >= 0) {
				surfData[textureHoveredSurface].file = draggingFile;
				surfData[textureHoveredSurface].tex = draggingTexture;
				surfaces[textureHoveredSurface].Texture = draggingTexture;
				UpdateSurfaceItemText(textureHoveredSurface, draggingFile);
				if (textureHoveredSurface == selectedSurface) {
					surfaceCodeChange = true;
					(Form as ModelForm).textureFile.File = draggingFile;
					surfaceCodeChange = false;
				}
				textureHoveredSurface = -1;
			}
		}

		/// <summary>
		/// Применение поверхностных данных
		/// </summary>
		void ApplySurfaceData() {
			for (int i = 0; i < surfData.Length; i++) {
				surfaces[i].Texture = surfData[i].tex;
				surfaces[i].Diffuse = surfData[i].tint;
				surfaces[i].AlphaBlend = !surfData[i].opaque;
			}
		}

		/// <summary>
		/// Обновление данных для записи в списке
		/// </summary>
		/// <param name="id">Индекс поверхности</param>
		/// <param name="file">Файл</param>
		void UpdateSurfaceItemText(int id, Project.Entry file) {
			string label = "(none)";
			if (file != null) {
				label = System.IO.Path.GetFileNameWithoutExtension(file.ProjectPath);
				if (file.Parent != Project.BaseDir) {
					label = file.Parent.Name + "/" + label;
				}
			}
			(Form as ModelForm).surfacesList.Items[id].SubItems[1].Text = label;
			(Form as ModelForm).surfacesList.Invalidate();
		}

		/// <summary>
		/// Поиск поверхности под указанными координатами
		/// </summary>
		/// <param name="x">Координата X</param>
		/// <param name="y">Координата Y</param>
		/// <returns>Индекс поверхности или -1</returns>
		public int GetSurfaceUnderCoords(int x, int y) {
			int idx = -1;
			ray.FromCamera(cam, x, y);
			Ray.HitInfo hi = ray.Cast();
			if (hi != null) {
				for (int i = 0; i < volumes.Length; i++) {
					if (hi.Volume == volumes[i]) {
						idx = i;
						break;
					}
				}
			}
			return idx;
		}

		/// <summary>
		/// Данные поверхности
		/// </summary>
		class SurfaceData {
			/// <summary>
			/// Файл текстуры
			/// </summary>
			public Project.Entry file = null;
			/// <summary>
			/// Текстура меша
			/// </summary>
			public Texture tex = null;
			/// <summary>
			/// Цвет поверхности
			/// </summary>
			public Color tint = Color.White;
			/// <summary>
			/// Непрозрачный
			/// </summary>
			public bool opaque = true;
			/// <summary>
			/// Всегда яркий
			/// </summary>
			public bool unlit = false;

			/// <summary>
			/// Переход цвета при выборе
			/// </summary>
			public float colorTrans = 0f;
		}
	}
}
