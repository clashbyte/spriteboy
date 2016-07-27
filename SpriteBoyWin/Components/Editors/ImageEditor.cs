using SpriteBoy.Data.Attributes;
using SpriteBoy.Data.Editing;
using SpriteBoy.Forms.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Editors {

	/// <summary>
	/// Редактор для файлов изображений
	/// </summary>
	[FileEditor(typeof(ImageForm), ".png", ".jpg", ".jpeg", ".gif", ".bmp")]
	public class ImageEditor : Editor {

		/// <summary>
		/// Загрузка изображения
		/// </summary>
		protected override void Load() {
			UpdateTitle();

		}

		/// <summary>
		/// Сохранение изображения
		/// </summary>
		public override void Save() {
			
		}

		/// <summary>
		/// Рендер изображения
		/// </summary>
		public override void Render() {
			
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
	}
}
