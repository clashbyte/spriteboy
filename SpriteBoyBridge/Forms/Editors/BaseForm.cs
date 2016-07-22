using SpriteBoy.Data;
using SpriteBoy.Data.Editing;
using SpriteBoy.Data.Editing.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteBoy.Forms.Editors {

	/// <summary>
	/// Базовая форма для редактора
	/// </summary>
	public partial class BaseForm : Form {

		/// <summary>
		/// Флаги для корректной отрисовки
		/// </summary>
		protected override CreateParams CreateParams {
			get {
				var parms = base.CreateParams;
				parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
				return parms;
			}
		}

		/// <summary>
		/// Иконка окна
		/// </summary>
		public new Image Icon {
			get {
				if (icon == null) {
					return null;
				}
				return icon.Scan;
			}
			set {
				if (value == null) {
					icon = null;
					return;
				}
				Image img = value;

				// Рассчитываем размер
				float pw = 16f / (float)img.Width;
				float ph = 16f / (float)img.Height;
				float mul = (pw > ph) ? pw : ph;
				int nw = (int)((float)img.Width * mul);
				int nh = (int)((float)img.Height * mul);

				Image ret = new Bitmap(nw, nh);
				using (Graphics g = Graphics.FromImage(ret)) {
					g.CompositingMode = CompositingMode.SourceCopy;
					g.CompositingQuality = CompositingQuality.HighQuality;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.DrawImage(img, new Rectangle(0, 0, nw, nh), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
				}

				icon = new ShadowImage(ret);
			}
		}

		/// <summary>
		/// Иконка окна
		/// </summary>
		ShadowImage icon;

		/// <summary>
		/// Редактор для данной формы
		/// </summary>
		public Editor FileEditor { get; set; }

		/// <summary>
		/// Конструктор
		/// </summary>
		public BaseForm() {
			InitializeComponent();
			
		}

		/// <summary>
		/// Отрисовка иконки
		/// </summary>
		public void DrawIcon(Graphics g, int x, int y) {
			if (icon != null) {
				icon.Draw(g, new Rectangle(x, y, 16, 16), 1);
			}
		}
	}
}
