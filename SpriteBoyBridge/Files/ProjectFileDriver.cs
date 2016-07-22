using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SpriteBoy.Data.Editing;

namespace SpriteBoy.Files {

	/// <summary>
	/// Драйвер файлов для проекта
	/// </summary>
	public class ProjectFileDriver : FileSystemDriver {

		/// <summary>
		/// Существует ли файл
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <returns>True если файл существует</returns>
		public override bool DirExist(string name) {
			return Directory.Exists(Path.Combine(Project.Folder, name));
		}

		/// <summary>
		/// Существует ли директория
		/// </summary>
		/// <param name="name">Имя директории</param>
		/// <returns>True если директория существует</returns>
		public override bool FileExist(string name) {
			return File.Exists(Path.Combine(Project.Folder, name));
		}

		/// <summary>
		/// Чтение файла
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <returns>Содержимое в виде массива</returns>
		public override byte[] Read(string name) {
			return File.ReadAllBytes(Path.Combine(Project.Folder, name));
		}

		/// <summary>
		/// Запись данных в файл
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <param name="data">Данные для записи</param>
		public override void Write(string name, byte[] data) {
			File.WriteAllBytes(Path.Combine(Project.Folder, name), data);
		}

	}
}
