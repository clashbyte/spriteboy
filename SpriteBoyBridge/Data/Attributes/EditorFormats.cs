using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Attributes {

	/// <summary>
	/// Редактор контента для указанных расширений
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class EditorFormats : Attribute {

		/// <summary>
		/// Доступные расширения для редактирования
		/// </summary>
		public string[] Extensions { get; private set; }

		/// <summary>
		/// Форма для редактирования
		/// </summary>
		public Type Form { get; private set; }

		/// <summary>
		/// Привязка редактора к указанным типам файлов
		/// </summary>
		/// <param name="form">Форма</param>
		/// <param name="extensions">Расширения</param>
		public EditorFormats(Type form, params string[] extensions) {
			Form = form;
			Extensions = new string[extensions.Length];
			for (int i = 0; i < extensions.Length; i++) {
				string ex = extensions[i].ToLower();
				if (!ex.StartsWith(".")) ex = "." + ex;
				Extensions[i] = ex;
			}
		}

	}
}
