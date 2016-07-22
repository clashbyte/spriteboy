using SpriteBoy.Data.Editing.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Previews {

	/// <summary>
	/// Статичные превью
	/// </summary>
	public static class StaticPreviews {

		/// <summary>
		/// Файл неба
		/// </summary>
		public static Preview SkyFile {
			get {
				if (skyFile == null) {
					skyFile = Preview.FromImage(SharedImages.SkyFile);
				}
				return skyFile;
			}
		}

		/// <summary>
		/// Файл анимации
		/// </summary>
		public static Preview AnimFile {
			get {
				if (animFile == null) {
					animFile = Preview.FromImage(SharedImages.AnimFile);
				}
				return animFile;
			}
		}

		/// <summary>
		/// Файл неба
		/// </summary>
		public static Preview ImageFile {
			get {
				if (imageFile == null) {
					imageFile = Preview.FromImage(SharedImages.ImageFile);
				}
				return imageFile;
			}
		}

		/// <summary>
		/// Файл карты
		/// </summary>
		public static Preview MapFile {
			get {
				if (mapFile == null) {
					mapFile = Preview.FromImage(SharedImages.MapFile);
				}
				return mapFile;
			}
		}

		/// <summary>
		/// Файл модели
		/// </summary>
		public static Preview ModelFile {
			get {
				if (modelFile == null) {
					modelFile = Preview.FromImage(SharedImages.ModelFile);
				}
				return modelFile;
			}
		}

		/// <summary>
		/// Файл проекта
		/// </summary>
		public static Preview ProjectFile {
			get {
				if (projectFile == null) {
					projectFile = Preview.FromImage(SharedImages.ProjectFile);
				}
				return projectFile;
			}
		}

		/// <summary>
		/// Файл спрайта
		/// </summary>
		public static Preview SpriteFile {
			get {
				if (spriteFile == null) {
					spriteFile = Preview.FromImage(SharedImages.SpriteFile);
				}
				return spriteFile;
			}
		}

		/// <summary>
		/// Скрытые изображения
		/// </summary>
		static Preview animFile, imageFile, mapFile, modelFile, projectFile, skyFile, spriteFile;

	}
}
