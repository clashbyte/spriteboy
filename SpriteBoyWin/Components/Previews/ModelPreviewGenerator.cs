using SpriteBoy.Data;
using SpriteBoy.Data.Attributes;
using SpriteBoy.Data.Editing.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Previews {
	/// <summary>
	/// Генератор для спрайтов
	/// </summary>
	[PreviewFormats(".s3d", ".sbmesh", ".3ds", ".obj", ".md3", ".md2")]
	public class ModelPreviewGenerator : PreviewGenerator {

		/// <summary>
		/// Загрузка картинки из файла
		/// </summary>
		/// <param name="filename">Имя файла</param>
		/// <returns>Изображение</returns>
		public override Image Generate(string filename) {
			string ext = System.IO.Path.GetExtension(filename).ToLower();
			switch (ext) {

				case ".s3d":
					return ExtractS3DPreview(filename);

				default:
					return null;
			}
		}

		/// <summary>
		/// Стандартное превью картинки
		/// </summary>
		public override Preview GetProxy(string filename) {
			return StaticPreviews.ModelFile;
		}

		/// <summary>
		/// Буллет не требуется для изображений
		/// </summary>
		public override bool ShowBulletAfterLoad(string filename) {
			return true;
		}

		/// <summary>
		/// Требуется загрузка в очередь
		/// </summary>
		public override bool PutInQueue(string filename) {
			return true;
		}

		/// <summary>
		/// Раскодирование превью для S3D-файла
		/// </summary>
		Image ExtractS3DPreview(string file) {
			byte[] imageData = null;
			BinaryReader f = new BinaryReader(new FileStream(file, FileMode.Open, FileAccess.Read));
			
			// Поиск расположения превью
			f.BaseStream.Position = f.BaseStream.Length - 8;
			f.BaseStream.Position = f.ReadUInt32();

			// Чтение PNG-данных
			int previewSize = f.ReadInt32();
			if (previewSize > 0) {
				imageData = f.ReadBytes(previewSize);
			}
			f.Close();

			// Загрузка изображения
			if (imageData!=null) {
				try {
					return Image.FromStream(new MemoryStream(imageData));
				} catch (Exception) {
					return null;
				}
			}
			return null;
		}
	}
}
