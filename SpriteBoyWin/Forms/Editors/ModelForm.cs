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

			CreateCanvas();
			Canvas.MouseDown += Canvas_MouseDown;
			Canvas.MouseUp += Canvas_MouseUp;
			Canvas.MouseMove += Canvas_MouseMove;
			Canvas.MouseWheel += Canvas_MouseWheel;
		}

		/// <summary>
		/// Мышь нажата
		/// </summary>
		void Canvas_MouseDown(object sender, MouseEventArgs e) {
			if (!mouseLook) {
				mouseLook = true;
				mouseButton = e.Button;
				Canvas.LockMouse = true;
				Cursor.Hide();
			}
		}

		/// <summary>
		/// Мышь отпущена
		/// </summary>
		private void Canvas_MouseUp(object sender, MouseEventArgs e) {
			if (mouseLook && mouseButton == e.Button) {
				mouseLook = false;
				Canvas.LockMouse = false;
				Cursor.Show();
			}
		}

		/// <summary>
		/// Мышь двигается
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Canvas_MouseMove(object sender, MouseEventArgs e) {
			if (mouseLook) {
				PointF spd = Canvas.MouseSpeed;
				spd.X *= 0.7f;
				spd.Y *= 0.7f;
				(FileEditor as ModelEditor).RotateCamera(spd);
			}
		}

		/// <summary>
		/// Колесо мыши
		/// </summary>
		void Canvas_MouseWheel(object sender, MouseEventArgs e) {
			(FileEditor as ModelEditor).ZoomCamera(e.Delta/120);
		}

		protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);
			if (Canvas!=null) {
				(FileEditor as ModelEditor).ViewportChanded(Canvas.ClientSize);
			}
			if (surfacesList != null) {
				surfacesList.Columns[1].Width = surfacesList.Width - 160;
				if (surfacesList.Columns[1].Width<1) {
					surfacesList.Columns[1].Width = 1;
				}
				surfacesList.Invalidate();
			}
		}


	}
}
