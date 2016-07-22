using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Editing.Graphics {

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
		/// Ссылки на стандартные превьюшки
		/// </summary>
		static Preview folderPreview, filePreview;


		/// <summary>
		/// Генерация стандартных превьюшек
		/// </summary>
		static void GenerateDefault() {
			folderPreview		= new Preview(SpriteBoy.InspectorIcons.Folder);
			filePreview			= new Preview(SpriteBoy.InspectorIcons.File);
		}
	}
}
