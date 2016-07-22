using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Files {

	/// <summary>
	/// Статичный класс, обеспечивает работу с файлами через драйверы
	/// </summary>
	public static class FileSystem {

		/// <summary>
		/// Драйвер, через который работает система
		/// </summary>
		public static FileSystemDriver Driver;

		/// <summary>
		/// Существует ли файл
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <returns>True если файл существует</returns>
		public static bool FileExist(string name) {
			CheckForDriver();
			return Driver.FileExist(name);
		}

		/// <summary>
		/// Существует ли каталог
		/// </summary>
		/// <param name="name">Имя директории</param>
		/// <returns>True если директория существует</returns>
		public static bool DirExist(string name) {
			CheckForDriver();
			return Driver.DirExist(name);
		}

		/// <summary>
		/// Чтение файла
		/// </summary>
		/// <param name="name">Путь до файла</param>
		/// <returns>Массив с данными из файла</returns>
		public static byte[] Read(string name) {
			CheckForDriver();
			return Driver.Read(name);
		}

		/// <summary>
		/// Запись файла
		/// </summary>
		/// <param name="name">Путь до файла</param>
		/// <param name="data">Данные для записи</param>
		public static void Write(string name, byte[] data) {
			CheckForDriver();
			Driver.Write(name, data);
		}

		/// <summary>
		/// Проверка на драйвер файловой системы
		/// </summary>
		static void CheckForDriver() {
			if (Driver == null) {
				throw new Exception("Filesystem driver didn't initialized!");
			}
		}
	}
}
