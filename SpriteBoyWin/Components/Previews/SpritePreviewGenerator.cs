using SpriteBoy.Data;
using SpriteBoy.Data.Attributes;
using SpriteBoy.Data.Editing.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Previews {
	/// <summary>
	/// Генератор для спрайтов
	/// </summary>
	[PreviewFormats(".sbsprite", ".sbs")]
	public class SpritePreviewGenerator : PreviewGenerator {

		/// <summary>
		/// Загрузка картинки из файла
		/// </summary>
		/// <param name="filename">Имя файла</param>
		/// <returns>Изображение</returns>
		public override Image Generate(string filename) {
			return null;
		}

		/// <summary>
		/// Стандартное превью картинки
		/// </summary>
		public override Preview GetProxy(string filename) {
			return StaticPreviews.SpriteFile;
		}

		/// <summary>
		/// Буллет не требуется для изображений
		/// </summary>
		public override bool ShowBulletAfterLoad(string filename) {
			return true;
		}

		/// <summary>
		/// Требуется загрузка в очередь
		/// </summary>
		public override bool PutInQueue(string filename) {
			return true;
		}

	}
}
