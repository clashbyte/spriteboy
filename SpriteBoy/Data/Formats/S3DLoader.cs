using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Components.Rendering;
using SpriteBoy.Engine.World;
using SpriteBoy.Files;
using System.IO;

namespace SpriteBoy.Data.Formats {
	
	/// <summary>
	/// Загрузчик для S3D-формата
	/// </summary>
	internal static class S3DLoader {

		/// <summary>
		/// Загрузка S3D-файла
		/// </summary>
		/// <param name="file">Файл</param>
		/// <returns>Загруженная модель</returns>
		public static Entity Load(string file) {

			byte[] data = FileSystem.Read(file);
			BinaryReader f = new BinaryReader(new MemoryStream(data));

			// Создание объекта
			Entity e = new Entity();

			// Поверхности
			int surfs = f.ReadUInt16();
			for (int s = 0; s < surfs; s++) {

				// Треугольники
				int verts = f.ReadUInt16() * 3;

				Vec3[] positions = new Vec3[verts];
				Vec2[] texcoords = new Vec2[verts];
				ushort[] indices = new ushort[verts];

				// Чтение вершин треугольников
				for (int t = 0; t < verts; t++) {

					Vec3 pos = new Vec3();
					pos.X = f.ReadSingle() * 0.015f;
					pos.Y = f.ReadSingle() * 0.015f;
					pos.Z = f.ReadSingle() * 0.015f;
					positions[t] = pos;
					

					Vec2 uv = new Vec2();
					uv.X = f.ReadSingle();
					uv.Y = f.ReadSingle();
					texcoords[t] = uv;
				}

				// Индексы
				for (int i = 0; i < indices.Length; i++) {
					indices[i] = (ushort)i;
				}

				// Сохранение поверхности
				StaticMeshComponent mc = new StaticMeshComponent();
				mc.Indices = indices;
				mc.Vertices = positions;
				mc.TexCoords = texcoords;
				e.AddComponent(mc);
			}

			f.Close();
			return e;
		}

	}
}
