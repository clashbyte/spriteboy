using SpriteBoy.Data;
using SpriteBoy.Data.Editing.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Previews {

	/// <summary>
	/// Менеджер предустановленных превью
	/// </summary>
	public static class PreviewManager {

		/// <summary>
		/// Регистрация всех доступных превью
		/// </summary>
		public static void RegisterAll() {

			// Изображения
			Register(new string[]{
				".png", ".jpg", ".jpeg", ".gif", ".bmp"
			}, new ImagePreviewGenerator());

			// Файлы спрайтов
			Register(new string[]{
				".sbsprite", ".sbs"	
			}, new SpritePreviewGenerator());

			// Файлы моделей
			Register(new string[]{
				".s3d", ".sbmesh", ".3ds"	
			}, new ModelPreviewGenerator());

			// Файлы моделей
			Register(new string[]{
				".sbsky"
			}, new SkyboxPreviewGenerator());

			// Файлы без иконок
			Register(new string[]{
				".sbproject", ".sbmap"
			}, new GenericPreviewGenerator());

		}

		/// <summary>
		/// Регистрация одного обработчика на несколько типов
		/// </summary>
		static void Register(string[] types, PreviewGenerator pg) {
			foreach (string t in types) {
				string nt = t.ToLower();
				if (!Preview.Generators.ContainsKey(nt)) {
					Preview.Generators.Add(nt, pg);
				}
			}
		}

	}
}
