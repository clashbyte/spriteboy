using SpriteBoy.Data.Editing.Graphics;
using SpriteBoy.Files;
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
			if (!File.Exists(projf)) {
				MessageDialog.Show(ControlStrings.ProjectNotFoundTitle, ControlStrings.ProjectNotFoundText.Replace("%PROJECT%", Path.GetFileNameWithoutExtension(projf)), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
				return;
			}

			// Открытие каталогов
			Folder = Path.GetDirectoryName(projf);
			BaseDir = RecursiveOpenDir("");

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
		/// Сохранение состояния всех файлов и папок
		/// </summary>
		public static void CacheState() {
			if (!Opened) {
				return;
			}
			RecursiveSaveDirState(BaseDir);
		}

		/// <summary>
		/// Проверка состояния файлов и папок - после разворота приложения
		/// </summary>
		public static void ValidateState() {
			if (!Opened) {
				return;
			}
			RecursiveValidateDirState(BaseDir);
		}

		/// <summary>
		/// Создание нового файла
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <param name="parent">Родительский каталог</param>
		/// <returns></returns>
		public static Entry CreateEntry(string name, Project.Dir parent) {

			// Поиск похожего
			List<Entry> fl = new List<Entry>(parent.Entries);
			foreach (Entry f in fl) {
				if (f.Name.ToLower() == name.ToLower()) {
					return null;
				}
			}
			
			// Запись на диск
			File.Create(parent.FullPath+"/"+name).Close();

			// Создание файла
			Entry e = new Entry();
			e.Name = name;
			e.Parent = parent;
			e.Icon = Preview.Get(e.FullPath);
			fl.Add(e);
			fl.Sort((a, b) => {
				return a.Name.CompareTo(b.Name);
			});
			parent.Entries = fl.ToArray();
			MainForm.ProjectEntryEvent(e, FileEvent.Created);
			return e;
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
				string oldfull = e.FullPath;
				byte[] meta = null;
				if (newName.ToLower() != e.Name.ToLower()) {
					meta = e.Meta;
					e.Meta = null;
				}
				e.Name = newName;
				e.Icon = Preview.Get(e.FullPath);
				if (meta!=null) {
					e.Meta = meta;
				}
				File.Move(oldfull, e.FullPath);
				MainForm.ProjectEntryEvent(e, FileEvent.Renamed);
			}
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
				string oldfull = d.FullPath;
				d.Name = d.Parent == BaseDir ? newName : d.Parent.Name + "/" + newName;
				Directory.Move(oldfull, d.FullPath);
				RecursiveNotifyDir(d, FileEvent.Renamed);
			}
			return true;
		}

		/// <summary>
		/// Копирование файла в другую папку
		/// </summary>
		/// <param name="e">Файл для копирования</param>
		/// <param name="parent">Папка</param>
		/// <returns>Скопированный файл</returns>
		public static Entry CopyEntry(Entry en, Dir parent) {

			// Подбор имени
			string fileName = Path.GetFileNameWithoutExtension(en.Name);
			string ext = Path.GetExtension(en.Name);
			string folder = en.Parent.FullPath;
			while (File.Exists(folder + "/" + fileName + ext)) {
				fileName += " - Copy";
			}

			// Копирование файла
			File.Copy(en.FullPath, folder + "/" + fileName + ext);

			// Создание записи
			Entry e = new Entry();
			e.Name = fileName + ext;
			e.Parent = parent;
			e.Meta = en.Meta;
			e.Icon = Preview.Get(e.FullPath);
			List<Entry> fl = parent.Entries.ToList();
			fl.Add(e);
			fl.Sort((a, b) => {
				return a.Name.CompareTo(b.Name);
			});
			parent.Entries = fl.ToArray();
			MainForm.ProjectEntryEvent(e, FileEvent.Created);
			return e;
		}

		/// <summary>
		/// Копирование директории
		/// </summary>
		/// <param name="dr">Директория для копирования</param>
		/// <param name="parent">Новая родительская директория</param>
		/// <returns>Скопированная директория</returns>
		public static Dir CopyDir(Dir dr, Dir parent) {

			// Создание диалога
			DirectoryCopyDialog cd = new DirectoryCopyDialog(dr, parent);
			cd.ShowDialog();

			// Возврат скопированной папки
			return cd.CopiedDir;
		}

		/// <summary>
		/// Удаление файла
		/// </summary>
		/// <returns>True если файл удалён</returns>
		public static bool DeleteEntry(Project.Entry e) {

			// Удаление записи
			File.Delete(e.FullPath);
			e.Deleted = true;
			List<Project.Entry> files = new List<Entry>(e.Parent.Entries);
			if (files.Contains(e)) {
				files.Remove(e);
			}
			e.Parent.Entries = files.ToArray();

			// Событие окна
			MainForm.ProjectEntryEvent(e, FileEvent.Deleted);
			return true;
		}

		/// <summary>
		/// Удаление директории
		/// </summary>
		/// <param name="d">Директория</param>
		/// <returns>True если директория удалена</returns>
		public static bool DeleteDir(Project.Dir d) {

			// Нельзя удалить корневую папку проекта
			if (d == BaseDir) {
				return false;
			}

			// Рекурсивное удаление папки
			RecursiveDeleteDir(d);
			List<Project.Dir> dirs = new List<Dir>(d.Parent.Dirs);
			if (dirs.Contains(d)) {
				dirs.Remove(d);
			}
			d.Parent.Dirs = dirs.ToArray();
			return true;
		}

		

		/// <summary>
		/// Можно ли изменять данный файл в инспекторе
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static bool OperableFile(object d) {
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
		/// Оповещение изменений файла
		/// </summary>
		/// <param name="e">Изменённый файл</param>
		public static void Notify(Entry e) {
			MainForm.ProjectEntryEvent(e, FileEvent.Modified);
		}

		/// <summary>
		/// Открытие папки рекурсивно
		/// </summary>
		/// <returns>Директория</returns>
		static Dir RecursiveOpenDir(string path) {
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
				Dir dr = RecursiveOpenDir(pd + Path.GetFileName(dn));
				dr.Parent = dir;
				dirList.Add(dr);
			}
			dirList.Sort((a, b) => {
				return a.ShortName.CompareTo(b.ShortName);
			});
			dir.Dirs = dirList.ToArray();

			// Открытие файлов
			string[] fileNames = Directory.GetFiles(Path.Combine(Folder, path), "*.*");
			List<Entry> fileList = new List<Entry>();
			foreach (string fn in fileNames) {
				// Чтение файла
				if (((File.GetAttributes(fn) & FileAttributes.Hidden) != FileAttributes.Hidden) && fn.ToLower() != MetaFile.META_FILE_NAME) {
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
		/// Нотификация директории
		/// </summary>
		/// <param name="dir">Директория</param>
		/// <param name="ev">Тип события</param>
		static void RecursiveNotifyDir(Dir dir, FileEvent ev) {

			// Подпапки
			foreach (Dir d in dir.Dirs) {
				RecursiveNotifyDir(d, ev);
			}

			// Файлы
			foreach (Entry e in dir.Entries) {
				MainForm.ProjectEntryEvent(e, ev);
				if (ev == FileEvent.Deleted) {
					e.Deleted = true;
				}
			}

			// Сама папка
			MainForm.ProjectDirEvent(dir, ev);
			if (ev == FileEvent.Deleted) {
				dir.Deleted = true;
			}
		}

		/// <summary>
		/// Сохранение состояния папки
		/// </summary>
		static void RecursiveSaveDirState(Dir dir) {

			// Сохранение самой папки
			dir.creationTime = Directory.GetCreationTime(dir.FullPath);
			dir.writeTime = Directory.GetLastWriteTime(dir.FullPath);

			// Сохранение файлов
			foreach (Entry e in dir.Entries) {
				e.creationTime = File.GetCreationTime(e.FullPath);
				e.writeTime = File.GetLastWriteTime(e.FullPath);
			}

			// Сохранение вложенных папок
			foreach (Dir d in dir.Dirs) {
				RecursiveSaveDirState(d);
			}
		}

		/// <summary>
		/// Сверка состояния папки
		/// </summary>
		static void RecursiveValidateDirState(Dir dir) {

			// Сверяем папки
			List<Dir> allDirs = new List<Dir>();
			string[] incDirs = Directory.GetDirectories(dir.FullPath, "*", SearchOption.TopDirectoryOnly);
			string[] lowerDirs = new string[incDirs.Length];
			bool[] foundDirs = new bool[lowerDirs.Length];
			for (int i = 0; i < incDirs.Length; i++) {
				lowerDirs[i] = Path.GetFileName(incDirs[i]).ToLower();
			}

			// Проверяем старые папки на удаление
			foreach (Dir d in dir.Dirs) {
				string name = d.ShortName.ToLower();
				if (lowerDirs.Contains(name)) {
					foundDirs[Array.IndexOf<string>(lowerDirs, name)] = true;
					allDirs.Add(d);
					RecursiveValidateDirState(d);
				} else {
					RecursiveNotifyDir(d, FileEvent.Deleted);
				}
			}

			// Проверка новых директорий
			for (int i = 0; i < foundDirs.Length; i++) {
				if (!foundDirs[i] && lowerDirs[i] != "." && lowerDirs[i] != "..") {
					Dir d = new Dir();
					d.Parent = dir;
					d.Name = d.Parent != BaseDir ? d.Parent.Name + "/" + Path.GetFileName(incDirs[i]) : Path.GetFileName(incDirs[i]);
					allDirs.Add(d);
					RecursiveValidateDirState(d);
				}
			}

			// Сохранение директорий
			allDirs.Sort((a, b) => {
				return a.ShortName.CompareTo(b.ShortName);
			});
			dir.Dirs = allDirs.ToArray();

			// Проверка файлов
			List<Entry> allEntries = new List<Entry>();
			string[] incFiles = Directory.GetFiles(dir.FullPath, "*", SearchOption.TopDirectoryOnly);
			string[] lowerFiles = new string[incFiles.Length];
			bool[] foundFiles = new bool[lowerFiles.Length];
			for (int i = 0; i < incFiles.Length; i++) {
				lowerFiles[i] = Path.GetFileName(incFiles[i]).ToLower();
			}

			// Проверяем старые файлы на удаление
			foreach (Entry e in dir.Entries) {
				string name = e.Name.ToLower();
				if (lowerFiles.Contains(name)) {
					foundFiles[Array.IndexOf<string>(lowerFiles, name)] = true;
					allEntries.Add(e);

					DateTime fcrTime = File.GetCreationTime(e.FullPath);
					DateTime fwrTime = File.GetLastWriteTime(e.FullPath);
					if (fcrTime > e.creationTime || fwrTime > e.writeTime) {
						MainForm.ProjectEntryEvent(e, FileEvent.Modified);
					}
				} else {
					e.Deleted = true;
					MainForm.ProjectEntryEvent(e, FileEvent.Deleted);
				}
			}

			// Проверка новых файлов
			for (int i = 0; i < foundFiles.Length; i++) {
				if (!foundFiles[i] && ((File.GetAttributes(incFiles[i]) & FileAttributes.Hidden) != FileAttributes.Hidden) && lowerFiles[i] != MetaFile.META_FILE_NAME) {
					Entry e = new Entry();
					e.Name = System.IO.Path.GetFileName(incFiles[i]);
					e.Parent = dir;
					allEntries.Add(e);
					MainForm.ProjectEntryEvent(e, FileEvent.Created);
				}
			}

			// Сохранение файлов
			allEntries.Sort((a, b) => {
				return a.Name.CompareTo(b.Name);
			});
			dir.Entries = allEntries.ToArray();

			// Проверка директории
			DateTime crTime = Directory.GetCreationTime(dir.FullPath);
			DateTime wrTime = Directory.GetLastWriteTime(dir.FullPath);
			if (crTime > dir.creationTime) {
				MainForm.ProjectDirEvent(dir, FileEvent.Created);
			} else if (wrTime > dir.writeTime) {
				MainForm.ProjectDirEvent(dir, FileEvent.Modified);
			}
		}


		/// <summary>
		/// Рекурсивное удаление директории
		/// </summary>
		static void RecursiveDeleteDir(Project.Dir d) {

			// Удаление файлов
			foreach (Project.Entry en in d.Entries) {

				// Флажок того что файл удалён
				en.Deleted = true;

				// Удаление файла
				try {
					File.Delete(en.FullPath);
				} catch (Exception) { }
				MainForm.ProjectEntryEvent(en, FileEvent.Deleted);
			}

			// Удаление подпапок
			foreach (Project.Dir dr in d.Dirs) {
				RecursiveDeleteDir(dr);
			}

			// Удаление директории
			d.Deleted = true;
			try {
				Directory.Delete(d.FullPath);
			} catch (Exception) {
			}
			MainForm.ProjectDirEvent(d, FileEvent.Deleted);
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
			/// Метаданные
			/// </summary>
			internal MetaFile Meta {
				get {
					if (meta==null) {
						meta = new MetaFile(this);
					}
					return meta;
				}
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

			/// <summary>
			/// Конструктор
			/// </summary>
			public Dir() {
				Entries = new Entry[0];
				Dirs = new Dir[0];
			}

			/// <summary>
			/// Время создания директории
			/// </summary>
			internal DateTime creationTime;

			/// <summary>
			/// Время последней записи в директории
			/// </summary>
			internal DateTime writeTime;

			/// <summary>
			/// Метаданные для папки
			/// </summary>
			private MetaFile meta;
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
			/// Получение метаданных для файла
			/// </summary>
			public byte[] Meta {
				get {
					if (Parent!=null) {
						return Parent.Meta[Name];
					}
					return null;
				}
				set {
					if (Parent!=null) {
						Parent.Meta[Name] = value;
					}
				}
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

			/// <summary>
			/// Время создания файла
			/// </summary>
			internal DateTime creationTime;

			/// <summary>
			/// Время последней записи в файл
			/// </summary>
			internal DateTime writeTime;
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
