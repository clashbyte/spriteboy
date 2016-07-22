using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data {

	/// <summary>
	/// Изображение с тенью
	/// </summary>
	public class ShadowImage {

		/// <summary>
		/// Изображение
		/// </summary>
		public Image Scan {
			get {
				return img;
			}
			set {
				img = value;
				GenerateShadow();
			}
		}

		/// <summary>
		/// Тень
		/// </summary>
		public Image Shadow {
			get { return shadow; }
		}

		/// <summary>
		/// Изображение и тень
		/// </summary>
		Image img, shadow;

		/// <summary>
		/// Создание нового изображения
		/// </summary>
		/// <param name="i">Исходное изображение</param>
		public ShadowImage(Image i) {
			img = i;
			GenerateShadow();
		}

		/// <summary>
		/// Отрисовка изображения в бокс
		/// </summary>
		/// <param name="to">Габариты для отрисовки</param>
		public void Draw(Graphics g, Rectangle to, float offset = 2f) {
			float padX = to.X, padY = to.Y;
			float sizeX = img.Width, sizeY = img.Height;
			float dtX = (float)to.Width / sizeX;
			float dtY = (float)to.Height / sizeY;
			float dtl = (dtX > dtY) ? dtY : dtX;

			sizeX *= dtl;
			sizeY *= dtl;
			padX += (float)to.Width / 2f - sizeX / 2f;
			padY += (float)to.Height / 2f - sizeY / 2f;

			g.DrawImage(shadow, padX + offset, padY + offset, sizeX, sizeY);
			g.DrawImage(img, padX, padY, sizeX, sizeY);
		}

		/// <summary>
		/// Пересоздание тени
		/// </summary>
		void GenerateShadow() {
			shadow = null;
			if (img == null) {
				return;
			}

			// Делаем тень
			shadow = new Bitmap(img.Width, img.Height);
			using (Graphics g = Graphics.FromImage(shadow)) {
				ImageAttributes attr = new ImageAttributes();
				attr.SetColorMatrix(new ColorMatrix(
					new float[][] { 
						new float[] {0,  0,  0,  0, 0},        // red scaling factor of 0
						new float[] {0,  0,  0,  0, 0},        // green scaling factor of 0
						new float[] {0,  0,  0,  0, 0},        // blue scaling factor of 0
						new float[] {0,  0,  0,  0.5f, 0},        // alpha scaling factor of 1
						new float[] {0,  0,  0,  0, 1}
					}
				), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

				g.DrawImage(img,
					new Rectangle(0, 0, img.Width, img.Height),
					0, 0,
					img.Width,
					img.Height,
					GraphicsUnit.Pixel,
					attr
				);
			}
		}

	}
}
