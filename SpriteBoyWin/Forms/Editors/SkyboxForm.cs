using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteBoy.Forms;
using SpriteBoy.Data;
using SpriteBoy.Controls;
using SpriteBoy.Components.Editors;
using SpriteBoy.Engine;
using SpriteBoy.Data.Editing;

namespace SpriteBoy.Forms.Editors {

	/// <summary>
	/// Форма для редактирования неба
	/// </summary>
	public partial class SkyboxForm : BaseForm {

		/// <summary>
		/// Поворот мышью
		/// </summary>
		bool mouseLook = false;

		/// <summary>
		/// Клавиша мыши, которой начат просмотр
		/// </summary>
		MouseButtons mouseButton;

		/// <summary>
		/// Место клика
		/// </summary>
		Point clickOrigin = Point.Empty;

		/// <summary>
		/// Предыдущее расположение мыши
		/// </summary>
		Point lastPos = Point.Empty;

		/// <summary>
		/// Конструктор
		/// </summary>
		public SkyboxForm() {
			InitializeComponent();

			CreateCanvas();
			Canvas.MouseDown += Canvas_MouseDown;
			Canvas.MouseUp += Canvas_MouseUp;
			Canvas.MouseMove += Canvas_MouseMove;
		}

		void Canvas_MouseDown(object sender, MouseEventArgs e) {
			if (!mouseLook) {
				mouseLook = true;
				mouseButton = e.Button;
				Canvas.LockMouse = true;
				Cursor.Hide();
			}
		}

		private void Canvas_MouseUp(object sender, MouseEventArgs e) {
			if (mouseLook && mouseButton == e.Button) {
				mouseLook = false;
				Canvas.LockMouse = false;
				Cursor.Show();
			}
		}

		void Canvas_MouseMove(object sender, MouseEventArgs e) {
			if (mouseLook) {
				PointF spd = Canvas.MouseSpeed;
				spd.X *= 0.7f;
				spd.Y *= 0.7f;
				(FileEditor as SkyboxEditor).RotateCamera(spd);
			}
		}

		private void SkyboxForm_Resize(object sender, EventArgs e) {
			(FileEditor as SkyboxEditor).ViewportChanded(Canvas.ClientSize);
		}

		/// <summary>
		/// Изменен файл текстуры
		/// </summary>
		private void skyTexture_FileChanged(object sender, EventArgs e) {
			Skybox.Side side = Skybox.Side.Front;
			Project.Entry file = frontSkyTexture.File;
			NSFileDropControl s = sender as NSFileDropControl;

			if (s == topSkyTexture) {
				file = topSkyTexture.File;
				side = Skybox.Side.Top;
			} else if (s == bottomSkyTexture){
				file = bottomSkyTexture.File;
				side = Skybox.Side.Bottom;
			} else if (s == leftSkyTexture){
				file = leftSkyTexture.File;
				side = Skybox.Side.Left;
			} else if (s == rightSkyTexture) {
				file = rightSkyTexture.File;
				side = Skybox.Side.Right;
			} else if (s == backSkyTexture) {
				file = backSkyTexture.File;
				side = Skybox.Side.Back;
			}

			(FileEditor as SkyboxEditor).TextureChanged(side, file);
		}

		/// <summary>
		/// Нажата кнопка видимости гизмо
		/// </summary>
		private void skyGizmoButton_CheckedChanged(object sender) {
			(FileEditor as SkyboxEditor).GizmoChanged(skyGizmoButton.Checked);
		}
	}
}
