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
	/// Генератор для всех типов картинок
	/// </summary>
	[PreviewFormats(".png", ".jpg", ".jpeg", ".gif", ".bmp")]
	public class ImagePreviewGenerator : PreviewGenerator {

		/// <summary>
		/// Загрузка картинки из файла
		/// </summary>
		/// <param name="filename">Имя файла</param>
		/// <returns>Изображение</returns>
		public override Image Generate(string filename) {
			if (System.IO.File.Exists(filename)) {
				return Image.FromFile(filename);
			}
			return null;
		}

		/// <summary>
		/// Стандартное превью картинки
		/// </summary>
		public override Preview GetProxy(string filename) {
			return StaticPreviews.ImageFile;
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
			return true;
		}

	}
}
