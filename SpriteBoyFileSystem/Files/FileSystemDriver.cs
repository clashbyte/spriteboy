using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Files {

	/// <summary>
	/// Драйвер файловой системы
	/// </summary>
	public abstract class FileSystemDriver {

		/// <summary>
		/// Существует ли файл
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <returns>True если файл существует</returns>
		public abstract bool FileExist(string name);

		/// <summary>
		/// Существует ли каталог
		/// </summary>
		/// <param name="name">Имя директории</param>
		/// <returns>True если директория существует</returns>
		public abstract bool DirExist(string name);

		/// <summary>
		/// Чтение файла
		/// </summary>
		/// <param name="name">Путь до файла</param>
		/// <returns>Массив с данными из файла</returns>
		public abstract byte[] Read(string name);

		/// <summary>
		/// Запись файла
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <param name="data">Данные</param>
		public abstract void Write(string name, byte[] data);
	}
}
