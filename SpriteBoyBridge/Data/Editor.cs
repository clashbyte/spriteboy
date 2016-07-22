using SpriteBoy.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data {

	/// <summary>
	/// Базовый класс редактора
	/// </summary>
	public abstract class Editor {

		/// <summary>
		/// Ассоциации типов файлов с редакторами
		/// </summary>
		public static Dictionary<string, Type> Associations { get; private set; }

		/// <summary>
		/// Открытие редактора
		/// </summary>
		public BaseForm Form { get; private set; }

		/// <summary>
		/// Имя файла
		/// </summary>
		public string FileName { get; private set; }

		/// <summary>
		/// Сохранён ли файл
		/// </summary>
		public bool Saved { get; private set; }

		/// <summary>
		/// Тип формы для редактора
		/// </summary>
		protected abstract Type FormType { get; }

		/// <summary>
		/// Создание из файла
		/// </summary>
		/// <param name="file">Путь до файла</param>
		public static Editor Create(string file) {
			string ext = System.IO.Path.GetExtension(file).ToLower();
			if (Associations!=null) {
				if (Associations.ContainsKey(ext)) {

					// Создание редактора
					Type t = Associations[ext];
					Editor e = Activator.CreateInstance(t) as Editor;
					e.FileName = file;
					e.CreateAssocForm();
					e.Load();
					return e;

				}
			}
			return null;
		}

		/// <summary>
		/// Создание связанной формы
		/// </summary>
		void CreateAssocForm() {
			if (FormType != null) {
				Form = Activator.CreateInstance(FormType) as BaseForm;
				Form.FileEditor = this;
			}
		}

		/// <summary>
		/// Загрузка файла при открытии редактора
		/// </summary>
		protected abstract void Load();

		/// <summary>
		/// Сохранение данных файла
		/// </summary>
		public abstract void Save();

		/// <summary>
		/// Обновление кадра
		/// </summary>
		public abstract void Update();

		/// <summary>
		/// Отрисовка кадра
		/// </summary>
		public abstract void Render();

		/// <summary>
		/// Получил фокус
		/// </summary>
		public abstract void GotFocus();

		/// <summary>
		/// Потерял фокус
		/// </summary>
		public abstract void LostFocus();

		/// <summary>
		/// Ассоциация расширений с редактором
		/// </summary>
		/// <param name="exts">Массив расширений</param>
		/// <param name="editor">Тип редактора, которым эти файлы открывать</param>
		public static void Register(string[] exts, Type editor) {
			if (exts!=null) {
				if (Associations == null) {
					Associations = new Dictionary<string, Type>();
				}
				foreach (string ext in exts) {
					if (!Associations.ContainsKey(ext)) {
						Associations.Add(ext, editor);
					}
				}
			}
		}
	}
}
