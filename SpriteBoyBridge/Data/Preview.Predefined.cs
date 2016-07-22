using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data {

	/// <summary>
	/// Изображение предпросмотра
	/// </summary>
	public partial class Preview {

		/// <summary>
		/// Превью папки
		/// </summary>
		public static Preview FolderIcon {
			get {
				if (folderPreview == null) {
					GenerateDefault();
				}
				return folderPreview;
			}
		}

		/// <summary>
		/// Превью неизвестного файла
		/// </summary>
		public static Preview FileIcon {
			get {
				if (filePreview == null) {
					GenerateDefault();
				}
				return filePreview;
			}
		}

		/// <summary>
		/// Превью незагруженного изображения
		/// </summary>
		public static Preview ImageFile {
			get {
				if (imagePreview == null) {
					GenerateDefault();
				}
				return imagePreview;
			}
		}

		/// <summary>
		/// Превью незагруженой модели
		/// </summary>
		public static Preview ModelFile {
			get {
				if (modelPreview == null) {
					GenerateDefault();
				}
				return modelPreview;
			}
		}

		/// <summary>
		/// Превью незагруженого скайбокса
		/// </summary>
		public static Preview SkyboxFile {
			get {
				if (skyPreview == null) {
					GenerateDefault();
				}
				return skyPreview;
			}
		}

		/// <summary>
		/// Превью незагруженого спрайта
		/// </summary>
		public static Preview SpriteFile {
			get {
				if (spritePreview == null) {
					GenerateDefault();
				}
				return spritePreview;
			}
		}

		/// <summary>
		/// Превью карты
		/// </summary>
		public static Preview MapFile {
			get {
				if (mapPreview == null) {
					GenerateDefault();
				}
				return mapPreview;
			}
		}

		/// <summary>
		/// Превью незагруженой анимации
		/// </summary>
		public static Preview AnimationFile {
			get {
				if (animationPreview == null) {
					GenerateDefault();
				}
				return animationPreview;
			}
		}

		/// <summary>
		/// Превью проекта
		/// </summary>
		public static Preview ProjectFile {
			get {
				if (projectPreview == null) {
					GenerateDefault();
				}
				return projectPreview;
			}
		}

		/// <summary>
		/// Ссылки на стандартные превьюшки
		/// </summary>
		static Preview folderPreview, filePreview, imagePreview, modelPreview, skyPreview, spritePreview, mapPreview, animationPreview, projectPreview;


		/// <summary>
		/// Генерация стандартных превьюшек
		/// </summary>
		static void GenerateDefault() {
			folderPreview		= new Preview(SpriteBoy.InspectorIcons.Folder);
			imagePreview		= new Preview(SpriteBoy.InspectorIcons.Image);
			filePreview			= new Preview(SpriteBoy.InspectorIcons.File);
			modelPreview		= new Preview(SpriteBoy.InspectorIcons.Model);
			skyPreview			= new Preview(SpriteBoy.InspectorIcons.Skybox);
			spritePreview		= new Preview(SpriteBoy.InspectorIcons.Sprite);
			mapPreview			= new Preview(SpriteBoy.InspectorIcons.Map);
			animationPreview	= new Preview(SpriteBoy.InspectorIcons.Animation);
			projectPreview		= new Preview(SpriteBoy.InspectorIcons.Project);
		}
	}
}
