using SpriteBoy.Components.Editors;
using SpriteBoy.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteBoy.Forms.Editors {

	/// <summary>
	/// Окно редактора моделей
	/// </summary>
	public partial class ModelForm : BaseForm {

		/// <summary>
		/// Холст для рисования
		/// </summary>
		public NSGraphicsCanvas canvas;

		/// <summary>
		/// Поворот мышью
		/// </summary>
		bool mouseLook = false;

		/// <summary>
		/// Клавиша мыши, которой начат просмотр
		/// </summary>
		MouseButtons mouseButton;

		/// <summary>
		/// Конструктор
		/// </summary>
		public ModelForm() {
			InitializeComponent();
			canvas = new NSGraphicsCanvas();
			canvas.Dock = DockStyle.Fill;
			canvas.Visible = true;
			canvas.MouseDown += canvas_MouseDown;
			canvas.MouseUp += canvas_MouseUp;
			canvas.MouseMove += canvas_MouseMove;
			canvas.MouseWheel += canvas_MouseWheel;
			Controls.Add(canvas);
			canvas.BringToFront();
		}

		/// <summary>
		/// Мышь нажата
		/// </summary>
		void canvas_MouseDown(object sender, MouseEventArgs e) {
			if (!mouseLook) {
				mouseLook = true;
				mouseButton = e.Button;
				canvas.LockMouse = true;
				Cursor.Hide();
			}
		}

		/// <summary>
		/// Мышь отпущена
		/// </summary>
		private void canvas_MouseUp(object sender, MouseEventArgs e) {
			if (mouseLook && mouseButton == e.Button) {
				mouseLook = false;
				canvas.LockMouse = false;
				Cursor.Show();
			}
		}

		/// <summary>
		/// Мышь двигается
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void canvas_MouseMove(object sender, MouseEventArgs e) {
			if (mouseLook) {
				PointF spd = canvas.MouseSpeed;
				spd.X *= 0.7f;
				spd.Y *= 0.7f;
				(FileEditor as ModelEditor).RotateCamera(spd);
			}
		}

		/// <summary>
		/// Колесо мыши
		/// </summary>
		void canvas_MouseWheel(object sender, MouseEventArgs e) {
			(FileEditor as ModelEditor).ZoomCamera(e.Delta/120);
		}

		protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);
			if (canvas!=null) {
				(FileEditor as ModelEditor).ViewportChanded(canvas.ClientSize);
			}
			if (surfacesList != null) {
				surfacesList.Columns[1].Width = surfacesList.Width - 160;
				if (surfacesList.Columns[1].Width<1) {
					surfacesList.Columns[1].Width = 1;
				}
				surfacesList.InvalidateColumns();
			}
		}


	}
}
