using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteBoy.Data;
using System.Drawing;
using SpriteBoy.Data.Editing.Graphics;
using SpriteBoy.Data.Attributes;

namespace SpriteBoy.Components.Previews {

	/// <summary>
	/// Генератор для файлов без графического представления
	/// </summary>
	[PreviewFormats(".sbproject", ".sbmap")]
	public class GenericPreviewGenerator : PreviewGenerator {

		/// <summary>
		/// Загрузка не требуется
		/// </summary>
		public override Image Generate(string filename) {
			return null;
		}

		/// <summary>
		/// Выборка из стандартных превьюшек
		/// </summary>
		public override Preview GetProxy(string filename) {
			switch (System.IO.Path.GetExtension(filename).ToLower()) {

				// Проект
				case ".sbproject":
					return StaticPreviews.ProjectFile;

				// Карта
				case ".sbmap":
					return StaticPreviews.MapFile;


				default:
					return Preview.FileIcon;
			}
		}

		/// <summary>
		/// Буллет не требуется для изображений
		/// </summary>
		public override bool ShowBulletAfterLoad(string filename) {
			return false;
		}

		/// <summary>
		/// Требуется загрузка в очередь
		/// </summary>
		public override bool PutInQueue(string filename) {
			return false;
		}

	}
}
