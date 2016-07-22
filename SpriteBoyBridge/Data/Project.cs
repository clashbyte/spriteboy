using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data {

	/// <summary>
	/// Класс проекта
	/// </summary>
	public static class Project {

		/// <summary>
		/// Основная директория проекта
		/// </summary>
		public static Dir BaseDir { get; private set; }

		/// <summary>
		/// Открыт ли проект
		/// </summary>
		public static bool Opened { get; private set; }

		/// <summary>
		/// Создание нового проекта по указанному пути
		/// </summary>
		public static void Create() {

		}

		/// <summary>
		/// Открытие проекта
		/// </summary>
		/// <param name="dir">Путь до директории с файлом .sbproj</param>
		public static void Open(string dir) {
			
			

			// Открытие каталогов
			BaseDir = OpenDirRecursively(dir);
		}

		/// <summary>
		/// Открытие папки рекурсивно
		/// </summary>
		/// <returns>Директория</returns>
		static Dir OpenDirRecursively(string path) {
			Dir dir = new Dir();
			dir.Name = path;

			// Открытие вложенных папок
			string[] dirNames = Directory.GetDirectories(path);
			List<Dir> dirList = new List<Dir>();
			foreach (string dn in dirNames) {
				Dir dr = OpenDirRecursively(dn);
				dr.Parent = dir;
				dirList.Add(dr);
			}
			dir.Dirs = dirList.ToArray();

			// Открытие файлов
			string[] fileNames = Directory.GetFiles(path, "*.*");
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
			public string Name;

			/// <summary>
			/// Родительская директория
			/// </summary>
			public Dir Parent;

			/// <summary>
			/// Список папок
			/// </summary>
			public Dir[] Dirs;

			/// <summary>
			/// Список файлов
			/// </summary>
			public Entry[] Entries;

		}

		/// <summary>
		/// Один файл проекта
		/// </summary>
		public class Entry {

			/// <summary>
			/// Имя файла
			/// </summary>
			public string Name;

			/// <summary>
			/// Изображение-превью
			/// </summary>
			public Preview Icon;

			/// <summary>
			/// Родительский каталог
			/// </summary>
			public Dir Parent;

			/// <summary>
			/// Полный путь до файла
			/// </summary>
			public string FullPath {
				get {
					return System.IO.Path.Combine(Parent.Name, Name);
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

	}
}
