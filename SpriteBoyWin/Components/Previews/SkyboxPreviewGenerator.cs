using SpriteBoy.Data;
using SpriteBoy.Data.Attributes;
using SpriteBoy.Data.Editing.Graphics;
using SpriteBoy.Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Previews {
	
	/// <summary>
	/// Генератор для скайбоксов
	/// </summary>
	[PreviewFormats(".sbsky")]
	class SkyboxPreviewGenerator : PreviewGenerator {

		static readonly string[] sideNames = new string[]{
			"Front", "Left", "Right", "Back", "Up", "Down"
		};

		/// <summary>
		/// Загрузка картинки из файла
		/// </summary>
		/// <param name="filename">Имя файла</param>
		/// <returns>Изображение</returns>
		public override Image Generate(string filename) {
			Image img = null;
			ChunkedFile cf = new ChunkedFile(filename);
			if (cf.Root.Name == "Skybox") {

				// Ищем чанк с текстурами
				ChunkedFile.KeyValueChunk k = (ChunkedFile.KeyValueChunk)cf.Root.GetChunk("Texture");
				if (k != null) {
					foreach (string sn in sideNames) {
						if (k.Values.ContainsKey(sn)) {
							string fn = k.Values[sn];
							if (FileSystem.FileExist(fn)) {
								img = Image.FromStream(new MemoryStream(FileSystem.Read(fn)));
								break;
							}
						}
					}
				}
				
			}

			// TODO: Добавить затемнение картинки цветом скайбокса


			return img;
		}

		/// <summary>
		/// Стандартное превью картинки
		/// </summary>
		public override Preview GetProxy(string filename) {
			return StaticPreviews.SkyFile;
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

	}
}
