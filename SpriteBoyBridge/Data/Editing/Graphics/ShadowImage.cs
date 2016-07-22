using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Editing.Graphics {

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
		public void Draw(System.Drawing.Graphics g, Rectangle to, float offset = 2f) {
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
			using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(shadow)) {
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

		/// <summary>
		/// Создание изображения из уже существующего
		/// </summary>
		/// <param name="img">Изображение</param>
		/// <param name="size">Размер</param>
		/// <param name="offset">Отступ тени</param>
		/// <returns>Изображение с тенью указанного размера</returns>
		public static Image CompiledFromImage(Image img, int size, int offset) {
			Bitmap b = new Bitmap(size, size);
			size -= offset;
			using(System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b)){

				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

				// Тень
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
					new Rectangle(offset, offset, size, size),
					0, 0,
					img.Width,
					img.Height,
					GraphicsUnit.Pixel,
					attr
				);

				// Изображение
				g.DrawImage(img,
					new Rectangle(0, 0, size, size),
					0, 0,
					img.Width,
					img.Height,
					GraphicsUnit.Pixel
				);

			}
			return b;
		}

	}
}
