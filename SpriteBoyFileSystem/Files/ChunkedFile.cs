using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Files {
	
	/// <summary>
	/// Файл, состоящий из чанков
	/// </summary>
	public class ChunkedFile {

		/// <summary>
		/// Имя файла
		/// </summary>
		public string FileName {
			get;
			set;
		}

		/// <summary>
		/// Основной чанк-контейнер
		/// </summary>
		public ContainerChunk Root {
			get;
			private set;
		}

		/// <summary>
		/// Создание пустого файла
		/// </summary>
		public ChunkedFile() {
			Root = new ContainerChunk();
		}

		/// <summary>
		/// Чтение файла
		/// </summary>
		/// <param name="path">Полный путь до файла</param>
		public ChunkedFile(string path) {
			FileName = path;
			BinaryReader f = new BinaryReader(new MemoryStream(FileSystem.Read(path)));

			// Читаем основной чанк
			if (f.BaseStream.Length >= 12) {
				string name = f.ReadConstantString(7);
				f.BaseStream.Position++;
				uint sz = f.ReadUInt32();
				Root = new ContainerChunk(name);
				Root.Read(f, sz);
			} else {
				Root = new ContainerChunk("");
			}
			
			f.Close();
		}

		/// <summary>
		/// Запись в файл
		/// </summary>
		public void Save(string path) {
			MemoryStream ms = new MemoryStream();
			BinaryWriter f = new BinaryWriter(ms);

			f.WriteConstantString(Root.Name, 7);
			f.Write((byte)ChunkType.ContainerChunk);
			f.Write((uint)Root.Size);
			Root.Write(f);
			f.Close();

			FileSystem.Write(path, ms.ToArray());
		}

		/// <summary>
		/// Запись в файл без указания пути
		/// </summary>
		public void Save() {
			Save(FileName);
		}

		/// <summary>
		/// Абстрактный чанк
		/// </summary>
		public abstract class Chunk {
			
			/// <summary>
			/// Имя чанка
			/// </summary>
			public string Name {
				set {
					if (value.Length>7) {
						throw new ArgumentException("Maximal name size of 7 bytes exceeded");
					}
					_name = value;
				}
				get {
					return _name;
				}
			}

			/// <summary>
			/// Размер чанка в байтах
			/// </summary>
			public abstract uint Size { get; }

			/// <summary>
			/// Скрытое имя чанка
			/// </summary>
			string _name;

			/// <summary>
			/// Запись чанка в файл
			/// </summary>
			/// <param name="f">Поток для записи</param>
			public abstract void Write(BinaryWriter f);

			/// <summary>
			/// Чтение чанка
			/// </summary>
			public abstract void Read(BinaryReader f, uint size);

			/// <summary>
			/// Запись заголовка
			/// </summary>
			/// <param name="f">Поток для записи</param>
			/// <param name="type">Идентификатор типа чанка</param>
			protected void WriteHeader(BinaryWriter f, ChunkType type) {
				f.WriteConstantString(Name, 7);		// 7 байт на строку имени чанка
				f.Write((byte)type);				// 1 байт на тип чанка
				f.Write((uint)Size);				// 4 байта на размер данных чанка
			}
		}

		/// <summary>
		/// Чанк-контейнер для других чанков
		/// </summary>
		public class ContainerChunk : Chunk {

			/// <summary>
			/// Зависимые чанки
			/// </summary>
			public List<Chunk> Children { get; private set; }

			/// <summary>
			/// Размер чанка
			/// </summary>
			public override uint Size {
				get {
					uint sz = 0;
					if (Children!=null) {
						foreach (Chunk ch in Children) {
							sz += ch.Size + 12;
						}
					}
					return sz;
				}
			}

			/// <summary>
			/// Конструктор чанка
			/// </summary>
			public ContainerChunk() : this("", null) { }

			/// <summary>
			/// Конструктор чанка с именем
			/// </summary>
			/// <param name="name">Имя чанка</param>
			public ContainerChunk(string name) : this(name, null) { }

			/// <summary>
			/// Конструктор чанка с именем и вложенными чанками
			/// </summary>
			/// <param name="name">Имя чанка</param>
			/// <param name="children">Вложенные чанки</param>
			public ContainerChunk(string name, IEnumerable<Chunk> children) {
				Name = name;
				Children = new List<Chunk>();
				if (children!=null) {
					foreach (Chunk ch in children) {
						Children.Add(ch);
					}
				}
			}

			/// <summary>
			/// Запись чанка в файл
			/// </summary>
			/// <param name="f">Поток куда писать</param>
			public override void Write(BinaryWriter f) {
				foreach (Chunk c in Children) {

					// Тип вложенного чанка
					ChunkType t = ChunkType.ContainerChunk;
					if (c is DataChunk) {
						t = ChunkType.RawDataChunk;
					}else if(c is KeyValueChunk) {
						t = ChunkType.KeyValueChunk;
					}

					// Запись чанка
					f.WriteConstantString(c.Name, 7);		// 7 байт на строку имени чанка
					f.Write((byte)t);					// 1 байт на тип чанка
					f.Write((uint)c.Size);				// 4 байта на размер данных чанка

					// Запись данных чанка
					c.Write(f);
				}
			}

			/// <summary>
			/// Чтение чанка из файла
			/// </summary>
			/// <param name="f">Поток откуда читать</param>
			public override void Read(BinaryReader f, uint size) {
				Children.Clear();
				uint quota = 0;
				while (quota < size) {
					Chunk c;

					// Чтение данных
					string nm		= f.ReadConstantString(7);
					ChunkType ct	= (ChunkType)f.ReadByte();
					uint sz			= f.ReadUInt32();

					// Создание чанка
					if (ct == ChunkType.RawDataChunk) {
						c = new DataChunk(nm);
					} else if (ct == ChunkType.KeyValueChunk) {
						c = new KeyValueChunk(nm);
					}else{
						c = new ContainerChunk(nm);
					}

					// Читаем содержимое
					c.Read(f, sz);
					Children.Add(c);

					// Увеличиваем квоту
					quota += 12 + c.Size;
				}

			}

			/// <summary>
			/// Поиск вложенного чанка по имени
			/// </summary>
			/// <param name="name">Имя чанка</param>
			/// <returns>Чанк или null</returns>
			public Chunk GetChunk(string name) {
				name = name.ToLower();
				foreach (Chunk c in Children) {
					if (c.Name.ToLower()==name) {
						return c;
					}
				}
				return null;
			}

		}

		/// <summary>
		/// Чанк, содержащий пары Ключ-Значение
		/// </summary>
		public class KeyValueChunk : Chunk {

			/// <summary>
			/// Ключи и значения
			/// </summary>
			public Dictionary<string, string> Values { get; private set; }

			/// <summary>
			/// Размер чанка в байтах
			/// </summary>
			public override uint Size {
				get {
					uint sz = 0;
					if (Values!=null) {
						foreach (KeyValuePair<string, string> e in Values) {
							sz += 8 + (uint)e.Key.Length + (uint)e.Value.Length;
						}
					}
					return sz;
				}
			}

			/// <summary>
			/// Конструктор чанка
			/// </summary>
			public KeyValueChunk() : this("", null) { }

			/// <summary>
			/// Конструктор чанка с именем
			/// </summary>
			/// <param name="name">Имя чанка</param>
			public KeyValueChunk(string name) : this(name, null) { }

			/// <summary>
			/// Конструктор чанка с именем и парами значений
			/// </summary>
			/// <param name="name">Имя чанка</param>
			/// <param name="values">Данные "ключ-значение"</param>
			public KeyValueChunk(string name, IDictionary<string, string> values) {
				Name = name;
				Values = new Dictionary<string, string>();
				if (values != null) {
					foreach (KeyValuePair<string, string> d in values) {
						Values.Add(d.Key, d.Value);
					}
				}
			}

			/// <summary>
			/// Запись чанка в файл
			/// </summary>
			/// <param name="f">Поток куда писать</param>
			public override void Write(BinaryWriter f) {
				foreach (KeyValuePair<string, string> d in Values) {
					f.WritePrefixedString(d.Key);
					f.WritePrefixedString(d.Value);
				}
			}

			/// <summary>
			/// Чтение чанка из файла
			/// </summary>
			/// <param name="f">Поток откуда читать</param>
			public override void Read(BinaryReader f, uint size) {
				Values.Clear();
				uint quota = 0;
				while (quota < size) {

					// Читаем ключ и значение
					string k = f.ReadPrefixedString();
					string v = f.ReadPrefixedString();
					Values.Add(k, v);

					// Увеличиваем квоту
					quota += 8 + (uint)k.Length + (uint)v.Length;
				}
			}

		}

		/// <summary>
		/// Чанк, содержащий байтовые данные
		/// </summary>
		public class DataChunk : Chunk {

			/// <summary>
			/// Массив данных
			/// </summary>
			public byte[] Data { get; set; }

			/// <summary>
			/// Размер чанка в байтах
			/// </summary>
			public override uint Size {
				get {
					uint sz = 0;
					if (Data != null) {
						sz += (uint)Data.Length;
					}
					return sz;
				}
			}

			/// <summary>
			/// Конструктор чанка
			/// </summary>
			public DataChunk() : this("", null) { }

			/// <summary>
			/// Конструктор чанка с именем
			/// </summary>
			/// <param name="name">Имя чанка</param>
			public DataChunk(string name) : this(name, null) { }

			/// <summary>
			/// Конструктор чанка с именем и данными
			/// </summary>
			/// <param name="name">Имя чанка</param>
			/// <param name="values">Данные в виде массива байт</param>
			public DataChunk(string name, byte[] data) {
				Name = name;
				Data = data;
			}

			/// <summary>
			/// Запись чанка в файл
			/// </summary>
			/// <param name="f">Поток куда писать</param>
			public override void Write(BinaryWriter f) {
				if (Data!=null) {
					f.Write(Data);
				}
			}

			/// <summary>
			/// Чтение чанка из файла
			/// </summary>
			/// <param name="f">Поток откуда читать</param>
			public override void Read(BinaryReader f, uint size) {
				if (size>0) {
					Data = f.ReadBytes((int)size);
				}
			}

		}

		public enum ChunkType : byte {
			/// <summary>
			/// Чанк-контейнер
			/// </summary>
			ContainerChunk		= 0,
			/// <summary>
			/// Чанк с данными
			/// </summary>
			RawDataChunk		= 1,
			/// <summary>
			/// Чанк типа ключ-значение
			/// </summary>
			KeyValueChunk		= 2
		}

	}
}
