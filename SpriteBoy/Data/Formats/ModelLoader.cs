using SpriteBoy.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Formats {

	/// <summary>
	/// Основной загрузчик моделей
	/// </summary>
	public static class ModelLoader {

		/// <summary>
		/// Максимальная дальность до модели
		/// </summary>
		public static float MaxRange {
			get;
			internal set;
		}

		/// <summary>
		/// Загрузка модели из файла
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <returns>Объект со всей загруженной информацией</returns>
		public static Entity FromFile(string name) {
			switch (System.IO.Path.GetExtension(name).ToLower()) {
				case ".s3d":
					return S3DLoader.Load(name);

				case ".md2":
					return MD2Loader.Load(name);

				case ".md3":
					return S3DLoader.Load(name);

			}
			return null;
		}




	}
}
