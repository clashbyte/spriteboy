using SpriteBoy.Data.Attributes;
using SpriteBoy.Forms;
using SpriteBoy.Forms.Common;
using SpriteBoy.Forms.Dialogs;
using SpriteBoy.Forms.Editors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SpriteBoy.Data.Editing {

	/// <summary>
	/// Базовый класс редактора
	/// </summary>
	public abstract class Editor {

		/// <summary>
		/// Ассоциации типов файлов с редакторами
		/// </summary>
		static Dictionary<string, Type> Associations;

		/// <summary>
		/// Ассоциации редакторов и их окон
		/// </summary>
		static Dictionary<Type, Type> ContentForms;

		/// <summary>
		/// Типы создаваемых файлов
		/// </summary>
		public static Editor.FileCreator[] CreatableList {
			get;
			private set;
		}

		/// <summary>
		/// Типы файлов, создаваемых этим типом редактора
		/// </summary>
		public virtual FileCreator[] CreatingFiles {
			get {
				return null;
			}
		}

		/// <summary>
		/// Открытие редактора
		/// </summary>
		public BaseForm Form { get; protected set; }

		/// <summary>
		/// Имя файла
		/// </summary>
		public Project.Entry File { get; protected set; }

		/// <summary>
		/// Заголовок окна
		/// </summary>
		public string Title {
			get {
				return title;
			}
			protected set {
				if (title != value) {
					title = value;
					MainForm.TabsChanged();
				}
			}
		}

		/// <summary>
		/// Сохранён ли файл
		/// </summary>
		public bool Saved {
			get {
				return saved;
			}
			protected set {
				if (value != saved) {
					saved = value;
					MainForm.TabsChanged();
				}
			} 
		}

		/// <summary>
		/// Заголовок окна (скрытый)
		/// </summary>
		string title;

		/// <summary>
		/// Сохранён ли проект (скрыто)
		/// </summary>
		bool saved;

		/// <summary>
		/// Закрыт ли редактор
		/// </summary>
		protected bool closed;

		/// <summary>
		/// Сохраняется ли файл
		/// </summary>
		protected bool saving;

		/// <summary>
		/// Создание из файла
		/// </summary>
		/// <param name="file">Путь до файла</param>
		public static Editor Create(Project.Entry entry) {
			string ext = System.IO.Path.GetExtension(entry.Name).ToLower();
			if (Associations!=null) {
				if (Associations.ContainsKey(ext)) {

					// Создание редактора
					Type t = Associations[ext];
					Editor e = Activator.CreateInstance(t) as Editor;
					e.File = entry;

					// Создание окна
					Type ft = ContentForms[t];
					e.Form = Activator.CreateInstance(ft) as BaseForm;
					e.Form.FileEditor = e;

					// Загрузка
					e.Load();
					return e;

				}
			}
			return null;
		}

		/// <summary>
		/// Закрытие вкладки
		/// </summary>
		/// <returns>True если вкладку можно закрыть</returns>
		public bool AllowClose() {
			if (saved) {
				return true;
			}
			System.Windows.Forms.DialogResult dr = Forms.Dialogs.MessageDialog.Show(
				ControlStrings.TabCloseTitle,
				ControlStrings.TabCloseText,
				System.Windows.Forms.MessageBoxButtons.YesNoCancel,
				System.Windows.Forms.MessageBoxIcon.Question
			);
			if (dr == System.Windows.Forms.DialogResult.Yes) {
				Save();
			}
			return dr != System.Windows.Forms.DialogResult.Cancel;
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
		public virtual void Update() { }

		/// <summary>
		/// Отрисовка кадра
		/// </summary>
		public virtual void Render() { }

		/// <summary>
		/// Получил фокус
		/// </summary>
		public virtual void GotFocus() { }

		/// <summary>
		/// Потерял фокус
		/// </summary>
		public virtual void LostFocus() { }

		/// <summary>
		/// Событие изменения файла проекта
		/// </summary>
		public virtual void ProjectEntryEvent(Project.Entry en, Project.FileEvent ev) {
			if (en == File) {
				switch (ev) {
					case Project.FileEvent.Created:
					case Project.FileEvent.Modified:
						if (!saving) {
							MainForm.FocusEditor(this);
							DialogResult dr = MessageDialog.Show(ControlStrings.TabReloadTitle, ControlStrings.TabReloadText, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							if (dr == DialogResult.Yes) {
								Load();
								Saved = true;
							} else {
								Saved = false;
							}
						}
						break;
					case Project.FileEvent.Renamed:
						UpdateTitle();
						break;
					case Project.FileEvent.Deleted:
						MainForm.CloseEditor(this, true);
						closed = true;
						break;
				}
			}
		}

		/// <summary>
		/// Событие изменения директории проекта
		/// </summary>
		public virtual void ProjectDirEvent(Project.Dir dr, Project.FileEvent ev) {

		}

		/// <summary>
		/// Обновление тайтла при изменении файла
		/// </summary>
		protected void UpdateTitle() {
			Title = System.IO.Path.GetFileNameWithoutExtension(File.Name) + " - " + Form.Text;
		}

		/// <summary>
		/// Ассоциация расширений с редактором
		/// </summary>
		public static void Register() {
			Associations = new Dictionary<string, Type>();
			ContentForms = new Dictionary<Type, Type>();
			List<FileCreator> creators = new List<FileCreator>();

			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				foreach (Type type in assembly.GetTypes()) {
					
					// Поиск редакторов
					object[] attribs = type.GetCustomAttributes(typeof(FileEditor), false);
					if (attribs != null && attribs.Length > 0) {
						FileEditor ce = attribs[0] as FileEditor;

						// Сохранение формы
						ContentForms.Add(type, ce.Form);

						// Привязка к расширениям
						foreach (string ex in ce.Extensions) {
							if (!Associations.ContainsKey(ex)) {
								Associations.Add(ex, type);
							}
						}
					}

					// Поиск создаваемых файлов
					if (type.IsSubclassOf(typeof(Editor))) {
						object obj = Activator.CreateInstance(type);
						FileCreator[] fc = (FileCreator[])type.GetProperty("CreatingFiles").GetValue(obj, null);
						if (fc != null && fc.Length > 0) {
							creators.AddRange(fc);
						}
					}
				}
			}
			creators.Sort((x, y) => x.Order.CompareTo(y.Order));
			CreatableList = creators.ToArray();
		}

		/// <summary>
		/// Описание редактора и типа файла
		/// </summary>
		public class FileCreator {
			/// <summary>
			/// Имя файла
			/// </summary>
			public string Name {
				get;
				set;
			}
			/// <summary>
			/// Расширение
			/// </summary>
			public string Extension {
				get;
				set;
			}
			/// <summary>
			/// Иконка
			/// </summary>
			public Image Icon {
				get;
				set;
			}
			/// <summary>
			/// Номер для сортировки
			/// </summary>
			public int Order {
				get;
				set;
			}
		}
	}
}
