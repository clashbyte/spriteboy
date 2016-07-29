using SpriteBoy.Data.Editing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Files {

	/// <summary>
	/// Класс, хранящий метаданные о конкретных файлах
	/// </summary>
	internal class MetaFile {

		/// <summary>
		/// Имя файла метаданных
		/// </summary>
		public const string META_FILE_NAME = ".sbmeta";

		/// <summary>
		/// Родительский каталог
		/// </summary>
		public Project.Dir Parent {
			get;
			private set;
		}

		/// <summary>
		/// Данные о файлах
		/// </summary>
		Dictionary<string, byte[]> fileData;

		/// <summary>
		/// Загрузка метаданных
		/// </summary>
		/// <param name="dir"></param>
		public MetaFile(Project.Dir dir) {
			LoadFile();
		}

		/// <summary>
		/// Есть ли данные для указанного файла
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <returns>True если данные имеются</returns>
		public bool HasData(string name) {
			if (fileData!=null) {
				return fileData.ContainsKey(name.ToLower());
			}
			return false;
		}

		/// <summary>
		/// Чтение и запись данных о файле
		/// </summary>
		/// <param name="name">Имя файла</param>
		/// <returns>Массив байтов или null</returns>
		public byte[] this[string name] {
			get {
				if (fileData!=null) {
					name = name.ToLower();
					if (fileData.ContainsKey(name)) {
						return fileData[name];
					}
				}
				return null;
			}
			set {
				if (fileData==null) {
					if (value == null) {
						return;
					}
					fileData = new Dictionary<string, byte[]>();
				}
				name = name.ToLower();
				if (value != null) {
					if (fileData.ContainsKey(name)) {
						fileData[name] = value;
					} else {
						fileData.Add(name, value);
					}
				}else{
					if (fileData.ContainsKey(name)) {
						fileData.Remove(name);
					}
				}
				SaveFile();
			}
		}

		/// <summary>
		/// Загрузка метаданных
		/// </summary>
		void LoadFile() {
			string fname = Parent.FullPath + "/" + META_FILE_NAME;
			if (File.Exists(fname)) {

				// Чтение чанкового файла
				ChunkedFile cf = new ChunkedFile(fname);
				if (!cf.Root.Name.StartsWith("SBMeta")) {
					return;
				}

				// Чанки с данными
				fileData = new Dictionary<string, byte[]>();
				foreach (ChunkedFile.Chunk ch in cf.Root.Children) {
					ChunkedFile.ContainerChunk cont = ch as ChunkedFile.ContainerChunk;

					// Чтение имени и данных
					string name = Encoding.Unicode.GetString((cont.Children[0] as ChunkedFile.DataChunk).Data);
					byte[] data = (cont.Children[1] as ChunkedFile.DataChunk).Data;

					// Сохранение
					fileData.Add(name.ToLower(), data);
				}
			}
		}

		/// <summary>
		/// Сохранение файла
		/// </summary>
		void SaveFile() {
			if (fileData != null) {
				string fname = Parent.FullPath + "/" + META_FILE_NAME;
				if (File.Exists(fname)) {
					File.Delete(fname);
				}

				// Выход если файлов нет
				if (fileData.Count==0) {
					return;
				}

				// Запись данных
				ChunkedFile ch = new ChunkedFile();
				ch.Root.Name = "SBMeta1";

				// Сохранение файлов
				foreach (KeyValuePair<string, byte[]> b in fileData) {
					ChunkedFile.ContainerChunk cont = new ChunkedFile.ContainerChunk();

					// Запись имени и файла
					cont.Children.AddRange(new ChunkedFile.Chunk[]{
						new ChunkedFile.DataChunk("FileNam", Encoding.Unicode.GetBytes(b.Key)),
						new ChunkedFile.DataChunk("RawData", b.Value)
					});

					ch.Root.Children.Add(cont);
				}

				// Запись файла
				ch.Save(fname);
				File.SetAttributes(fname, FileAttributes.Hidden);
			}
		}
	}
}
