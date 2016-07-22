using SpriteBoy.Data.Editing.Graphics;
using SpriteBoy.Forms;
using SpriteBoy.Forms.Common;
using SpriteBoy.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Editing {

	/// <summary>
	/// Класс проекта
	/// </summary>
	public static class Project {

		/// <summary>
		/// Позволить ли удаление файлов
		/// </summary>
		const bool ALLOW_REMOVING = false;

		/// <summary>
		/// Основная директория проекта
		/// </summary>
		public static Dir BaseDir { get; private set; }

		/// <summary>
		/// Рабочая папка проекта
		/// </summary>
		public static string Folder { get; private set; }

		/// <summary>
		/// Открыт ли проект
		/// </summary>
		public static bool Opened { get; private set; }

		/// <summary>
		/// Имя проекта
		/// </summary>
		public static string Name { get; private set; }


		/// <summary>
		/// Создание нового проекта по указанному пути
		/// </summary>
		public static void Create() {

		}

		/// <summary>
		/// Открытие проекта
		/// </summary>
		/// <param name="projf">Путь до файла .sbproject</param>
		public static void Open(string projf) {
			if (Opened) {
				Close();
			}
			

			// Открытие каталогов
			Folder = Path.GetDirectoryName(projf);
			BaseDir = OpenDirRecursively("");

			// Проект успено открыт
			MainForm.ProjectOpened();
			Opened = true;
		}

		/// <summary>
		/// Закрытие проекта
		/// </summary>
		public static void Close() {
			
			Opened = false;
		}

		/// <summary>
		/// Создание нового файла
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static bool CreateEntry(string name, Project.Dir parent) {

			

			return true;
		}

		/// <summary>
		/// Создание новой папки
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static Dir CreateDir(string name, Dir parent) {
			List<Dir> dl = new List<Dir>(parent.Dirs);
			foreach (Dir d in dl) {
				if (d.ShortName.ToLower() == name.ToLower()) {
					return null;
				}
			}

			// Создание папки
			Dir dir = new Dir();
			dir.Name = parent != BaseDir ? parent.Name + "/" + name : name;
			dir.Entries = new Entry[0];
			dir.Dirs = new Dir[0];
			dir.Parent = parent;
			dl.Add(dir);
			parent.Dirs = dl.ToArray();

			// Создание настоящей папки
			Directory.CreateDirectory(dir.FullPath);

			MainForm.ProjectDirEvent(dir, FileEvent.Created);
			return dir;
		}

		/// <summary>
		/// Изменение имени файла
		/// </summary>
		/// <param name="e">Файл для переименовывания</param>
		/// <param name="newName">Новое имя</param>
		/// <returns>True если файл переименован</returns>
		public static bool RenameItem(Project.Entry e, string newName) {
			if (newName.IndexOfAny(Path.GetInvalidFileNameChars())>=0) {
				return false;
			}
			if (newName != e.Name) {
				
			}

			MainForm.ProjectEntryEvent(e, FileEvent.Renamed);
			return true;
		}

		/// <summary>
		/// Изменение имени директории
		/// </summary>
		/// <param name="d">Директория для переименовывания</param>
		/// <param name="newName">Новое имя</param>
		/// <returns>True если директория переименована</returns>
		public static bool RenameItem(Project.Dir d, string newName) {
			if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) {
				return false;
			}
			if (newName != d.ShortName) {

			}

			MainForm.ProjectDirEvent(d, FileEvent.Renamed);
			return true;
		}

		/// <summary>
		/// Удаление файла
		/// </summary>
		/// <returns>True если файл удалён</returns>
		public static bool DeleteEntry(Project.Entry e) {
			if (MessageDialog.Show(ControlStrings.DeleteFileTitle, ControlStrings.DeleteFileText.Replace("%FILE%", "\n"+e.Name), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
				if (ALLOW_REMOVING) {
					File.Delete(e.FullPath);
				}
				e.Deleted = true;
				List<Project.Entry> files = new List<Entry>(e.Parent.Entries);
				if (files.Contains(e)) {
					files.Remove(e);
				}
				e.Parent.Entries = files.ToArray();

				MainForm.ProjectEntryEvent(e, FileEvent.Deleted);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Удаление директории
		/// </summary>
		/// <param name="d">Директория</param>
		/// <returns>True если директория удалена</returns>
		public static bool DeleteDir(Project.Dir d) {
			if (d == BaseDir) {
				return false;
			}
			if (MessageDialog.Show(ControlStrings.DeleteFolderTitle, ControlStrings.DeleteFolderText.Replace("%FOLDER%", "\n" + d.ShortName), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
				DeleteDirRecursive(d);
				List<Project.Dir> dirs = new List<Dir>(d.Parent.Dirs);
				if (dirs.Contains(d)) {
					dirs.Remove(d);
				}
				d.Parent.Dirs = dirs.ToArray();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Рекурсивное удаление директории
		/// </summary>
		static void DeleteDirRecursive(Project.Dir d) {
			
			// Удаление файлов
			foreach (Project.Entry en in d.Entries) {

				// Флажок того что файл удалён
				en.Deleted = true;

				// Удаление файла
				if (ALLOW_REMOVING) {
					File.Delete(en.FullPath);
				}
				MainForm.ProjectEntryEvent(en, FileEvent.Deleted);
			}

			// Удаление подпапок
			foreach (Project.Dir dr in d.Dirs) {
				DeleteDirRecursive(dr);
			}

			// Удаление директории
			d.Deleted = true;
			if (ALLOW_REMOVING) {
				Directory.Delete(d.Name);
			}
			MainForm.ProjectDirEvent(d, FileEvent.Deleted);
		}

		/// <summary>
		/// Можно ли изменять данный файл в инспекторе
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static bool LegalFile(object d) {
			if (d is Project.Entry) {
				Project.Entry e = d as Project.Entry;
				if (e.Name.EndsWith(".sbproject")) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Получение файла из проекта по его относительному пути
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <returns>Файл или null</returns>
		public static Project.Entry GetEntry(string name) {
			// Поиск папки
			if (name.Length == 0) {
				return null;
			}
			string[] parts = name.Split(new char[]{'/', '\\'});
			Project.Dir d = Project.BaseDir;
			for (int i = 0; i < parts.Length-1; i++) {
				bool found = false;
				string n = parts[i].ToLower();
				foreach (Project.Dir dn in d.Dirs) {
					if (n == dn.Name.ToLower()) {
						d = dn;
						found = true;
						break;
					}
				}
				if (!found) {
					return null;
				}
			}

			// Поиск файла
			string fn = parts[parts.Length-1].ToLower();
			foreach (Project.Entry en in d.Entries) {
				if (fn == en.Name.ToLower()) {
					return en;
				}
			}

			return null;
		}

		/// <summary>
		/// Получение директории из проекта по ее относительному пути
		/// </summary>
		/// <param name="name">Имя директории</param>
		/// <returns>Директория или null</returns>
		public static Project.Dir GetDir(string name) {
			// Поиск папки
			if (name.Length == 0) {
				return null;
			}
			string[] parts = name.Split(new char[] { '/', '\\' });
			Project.Dir d = Project.BaseDir;
			for (int i = 0; i < parts.Length - 1; i++) {
				bool found = false;
				string n = parts[i].ToLower();
				foreach (Project.Dir dn in d.Dirs) {
					if (n == dn.Name.ToLower()) {
						d = dn;
						found = true;
						break;
					}
				}
				if (!found) {
					return null;
				}
			}
			return d;
		}

		/// <summary>
		/// Открытие папки рекурсивно
		/// </summary>
		/// <returns>Директория</returns>
		static Dir OpenDirRecursively(string path) {
			Dir dir = new Dir();
			dir.Name = path;

			// Открытие вложенных папок
			string[] dirNames = Directory.GetDirectories(Path.Combine(Folder, path));
			List<Dir> dirList = new List<Dir>();
			foreach (string dn in dirNames) {
				string pd = path;
				if (pd!="") {
					pd += "/";
				}
				Dir dr = OpenDirRecursively(pd+Path.GetFileName(dn));
				dr.Parent = dir;
				dirList.Add(dr);
			}
			dir.Dirs = dirList.ToArray();

			// Открытие файлов
			string[] fileNames = Directory.GetFiles(Path.Combine(Folder, path), "*.*");
			List<Entry> fileList = new List<Entry>();
			foreach (string fn in fileNames) {
				// Чтение файла
				if (((File.GetAttributes(fn) & FileAttributes.Hidden) != FileAttributes.Hidden)) {
					Entry en = new Entry();
					en.Name = Path.GetFileName(fn);
					en.Parent = dir;
					fileList.Add(en);
				}
			}
			dir.Entries = fileList.ToArray();

			// Возврат
			return dir;
		}

		/// <summary>
		/// Одна папка проекта
		/// </summary>
		public class Dir {

			/// <summary>
			/// Имя папки
			/// </summary>
			public string Name {
				get;
				internal set;
			}

			/// <summary>
			/// Родительская директория
			/// </summary>
			public Dir Parent {
				get;
				internal set;
			}

			/// <summary>
			/// Список папок
			/// </summary>
			public Dir[] Dirs {
				get;
				internal set;
			}

			/// <summary>
			/// Список файлов
			/// </summary>
			public Entry[] Entries {
				get;
				internal set;
			}

			/// <summary>
			/// Флаг что эта директория удалена
			/// </summary>
			public bool Deleted {
				get;
				internal set;
			}

			/// <summary>
			/// Имя папки без пути
			/// </summary>
			public string ShortName {
				get {
					return Path.GetFileName(Name);
				}
			}

			/// <summary>
			/// Полный путь до папки
			/// </summary>
			public string FullPath {
				get {
					return Path.Combine(Project.Folder, Name).Replace('\\', '/');
				}
			}

			/// <summary>
			/// Список всех использованных имён в директории
			/// </summary>
			public string[] UsedNames {
				get {
					List<string> names = new List<string>();
					foreach (Dir d in Dirs) {
						names.Add(d.ShortName.ToLower());
					}
					foreach (Entry e in Entries) {
						names.Add(e.Name.ToLower());
					}
					return names.ToArray();
				}
			}

		}

		/// <summary>
		/// Один файл проекта
		/// </summary>
		public class Entry {

			/// <summary>
			/// Имя файла
			/// </summary>
			public string Name {
				get;
				internal set;
			}

			/// <summary>
			/// Изображение-превью
			/// </summary>
			public Preview Icon;

			/// <summary>
			/// Родительский каталог
			/// </summary>
			public Dir Parent {
				get;
				internal set;
			}

			/// <summary>
			/// Файл удалён
			/// </summary>
			public bool Deleted {
				get;
				internal set;
			}

			/// <summary>
			/// Получение проектного пути до файла
			/// </summary>
			public string ProjectPath {
				get {
					return Path.Combine(Parent.Name, Name).Replace('\\', '/');
				}
			}

			/// <summary>
			/// Полный путь до файла
			/// </summary>
			public string FullPath {
				get {
					return Path.Combine(Project.Folder, ProjectPath).Replace('\\', '/');
				}
			}
		}

		/// <summary>
		/// Перетаскиваемый файл инпектора
		/// </summary>
		public class DraggingEntry {

			/// <summary>
			/// Сам файл
			/// </summary>
			public Entry File;

		}

		/// <summary>
		/// Тип события с файлом
		/// </summary>
		public enum FileEvent {
			/// <summary>
			/// Файл создан
			/// </summary>
			Created,
			/// <summary>
			/// Файл изменён
			/// </summary>
			Modified,
			/// <summary>
			/// Файл переименован
			/// </summary>
			Renamed,
			/// <summary>
			/// Файл удалён
			/// </summary>
			Deleted
		}
	}
}
