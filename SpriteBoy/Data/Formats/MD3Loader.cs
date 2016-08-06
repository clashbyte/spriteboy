using SpriteBoy.Data.Types;
using SpriteBoy.Engine;
using SpriteBoy.Engine.Components.Animation;
using SpriteBoy.Engine.Components.Rendering;
using SpriteBoy.Engine.World;
using SpriteBoy.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Formats {

	/// <summary>
	/// Загрузка модели MD3
	/// </summary>
	internal static class MD3Loader {

		/// <summary>
		/// Множитель для вершин
		/// </summary>
		const float VERTEX_SCALE = (1f / 64f) * 0.015f;

		/// <summary>
		/// Множитель для байтов нормалей
		/// </summary>
		const float NORMAL_MULT = 3.14f * 2f / 255f;

		/// <summary>
		/// Загруженные файлы
		/// </summary>
		static Dictionary<string, CachedEntry> loaded = new Dictionary<string, CachedEntry>();

		/// <summary>
		/// Загрузка MD3 из файла
		/// </summary>
		/// <param name="fname">Имя файла</param>
		/// <returns>Собранный меш</returns>
		public static Entity Load(string fname) {

			// Загрузка модели
			string f = fname.ToLower();
			if (!loaded.ContainsKey(f)) {
				loaded.Add(f, ReadModel(fname));
			}
			CachedEntry entry = loaded[f];

			// Создание объекта
			Entity e = new Entity();
			if (entry.Surfaces != null) {
				foreach (MorphMeshComponent m in entry.Surfaces) {
					e.AddComponent(new MorphMeshComponent() {
						Proxy = m
					});
				}
			}
			e.AddComponent(new AnimatorComponent());

			// Возврат
			return e;
		}

		/// <summary>
		/// Чтение модели с диска, если она не закеширована
		/// </summary>
		/// <param name="fname">Имя файла</param>
		/// <returns>Кешированная модель</returns>
		static CachedEntry ReadModel(string fname) {

			// Открытие файла
			byte[] data = FileSystem.Read(fname);
			BinaryReader f = new BinaryReader(new MemoryStream(data));

			// Чтение заголовка
			string fourcc = new string(f.ReadChars(4));
			int version = f.ReadInt32();
			if (fourcc != "IDP3" || version != 15) {
				f.Close();
				return null;
			}

			// Пропуск имени модели и флагов
			f.BaseStream.Position += 72;

			// Значения количества
			int tagNum = f.ReadInt32();
			int surfaceNum = f.ReadInt32();

			// Пропуск скинов
			f.BaseStream.Position += 8;
			
			// Отступы в файле
			int tagOffset = f.ReadInt32();
			int surfaceOffset = f.ReadInt32();

			// Кешированная запись
			CachedEntry entry = new CachedEntry();

			// Массивы мешей
			entry.Surfaces = new MorphMeshComponent[surfaceNum];

			// Чтение поверхностей
			f.BaseStream.Position = surfaceOffset;
			for (int surfIndex = 0; surfIndex < surfaceNum; surfIndex++) {

				// Пропуск ненужных данных
				f.BaseStream.Position += 72;

				// Чтение размеров
				int frameNum = f.ReadInt32();
				int shaderNum = f.ReadInt32();
				int vertexNum = f.ReadInt32();
				int triangleNum = f.ReadInt32();
				
				// Пропуск отступов и шейдеров
				f.BaseStream.Position += 20;
				f.BaseStream.Position += 68 * shaderNum;

				// Индексы
				ushort[] indices = new ushort[triangleNum * 3];
				for (int i = 0; i < indices.Length; i++) {
					indices[i] = (ushort)f.ReadInt32();
				}

				// Текстурные координаты
				Vec2[] texCoords = new Vec2[vertexNum];
				for (int i = 0; i < texCoords.Length; i++) {
					texCoords[i].X = f.ReadSingle();
					texCoords[i].Y = f.ReadSingle();
				}

				// Кадры
				MorphMeshComponent.MorphFrame[] frames = new MorphMeshComponent.MorphFrame[frameNum];
				for (int frameIndex = 0; frameIndex < frameNum; frameIndex++) {

					// Необходимые массивы
					Vec3[] verts = new Vec3[vertexNum];
					Vec3[] norms = new Vec3[vertexNum];

					// Чтение вершин и нормалей
					for (int i = 0; i < vertexNum; i++) {

						// Позиция
						float px, py, pz, nx, ny, nz;
						px = (float)f.ReadInt16() * VERTEX_SCALE;
						py = (float)f.ReadInt16() * VERTEX_SCALE;
						pz = (float)f.ReadInt16() * VERTEX_SCALE;
						verts[i] = new Vec3(-py, pz, px);

						// Нормаль
						float lng = (float)f.ReadByte() * NORMAL_MULT;
						float lat = (float)f.ReadByte() * NORMAL_MULT;
						nx = (float)(Math.Cos(lat) * Math.Sin(lng));
						ny = (float)(Math.Sin(lat) * Math.Sin(lng));
						nz = (float)(Math.Cos(lng));
						norms[i] = new Vec3(-ny, nz, nx);
					}

					// Сохранение кадра
					MorphMeshComponent.MorphFrame frame = new MorphMeshComponent.MorphFrame();
					frame.Time = frameIndex;
					frame.Vertices = verts;
					frame.Normals = norms;
					frames[frameIndex] = frame;
				}

				// Поверхность
				MorphMeshComponent surface = new MorphMeshComponent();
				surface.Frames = frames;
				surface.TexCoords = texCoords;
				surface.Indices = indices;
				entry.Surfaces[surfIndex] = surface;
			}

			return entry;
		}


		/// <summary>
		/// Поверхности
		/// </summary>
		class CachedEntry {
			/// <summary>
			/// Поверхности
			/// </summary>
			public MorphMeshComponent[] Surfaces;

		}
	}
}
