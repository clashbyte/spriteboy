using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Attributes {

	/// <summary>
	/// Объявление форматов для генераторов превьюшек
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PreviewFormats : Attribute {

		/// <summary>
		/// Доступные расширения для редактирования
		/// </summary>
		public string[] Extensions { get; private set; }

		/// <summary>
		/// Привязка генератора к указанным типам файлов
		/// </summary>
		/// <param name="extensions">Расширения</param>
		public PreviewFormats(params string[] extensions) {
			Extensions = new string[extensions.Length];
			for (int i = 0; i < extensions.Length; i++) {
				string ex = extensions[i].ToLower();
				if (!ex.StartsWith(".")) ex = "." + ex;
				Extensions[i] = ex;
			}
		}

	}
}
