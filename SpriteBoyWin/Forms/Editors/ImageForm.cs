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
	/// Редактор изображений
	/// </summary>
	public partial class ImageForm : BaseForm {

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
		public ImageForm() {
			InitializeComponent();
			filteringCombo.Items = SharedStrings.TextureFiltering;
			wrapUCombo.Items = SharedStrings.TextureWrapMode;
			wrapVCombo.Items = SharedStrings.TextureWrapMode;

			CreateCanvas();
			Canvas.MouseDown += canvas_MouseDown;
			Canvas.MouseUp += canvas_MouseUp;
			Canvas.MouseMove += canvas_MouseMove;
			Canvas.MouseWheel += canvas_MouseWheel;
		}


		/// <summary>
		/// Мышь нажата
		/// </summary>
		void canvas_MouseDown(object sender, MouseEventArgs e) {
			if (!mouseLook) {
				mouseLook = true;
				mouseButton = e.Button;
			}
		}

		/// <summary>
		/// Мышь отпущена
		/// </summary>
		private void canvas_MouseUp(object sender, MouseEventArgs e) {
			if (mouseLook && mouseButton == e.Button) {
				mouseLook = false;
			}
		}

		/// <summary>
		/// Мышь двигается
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void canvas_MouseMove(object sender, MouseEventArgs e) {
			if (mouseLook) {
				PointF spd = Canvas.MouseSpeed;
				(FileEditor as ImageEditor).MoveCamera(spd);
			}
		}

		/// <summary>
		/// Колесо мыши
		/// </summary>
		void canvas_MouseWheel(object sender, MouseEventArgs e) {
			(FileEditor as ImageEditor).ZoomCamera(e.Delta / 120);
		}

		/// <summary>
		/// Изменение размера
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);
			if (Canvas != null) {
				(FileEditor as ImageEditor).ViewportChanded(Canvas.ClientSize);
			}
		}


	}
}
