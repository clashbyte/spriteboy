using SpriteBoy.Components.Editors;
using SpriteBoy.Controls;
using SpriteBoy.Data.Editing;
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
		/// Забрасываемая текстура
		/// </summary>
		Project.Entry dragTexture = null;

		/// <summary>
		/// Клавиша перетаскивания
		/// </summary>
		MouseButtons dragButton;

		/// <summary>
		/// Конструктор
		/// </summary>
		public ModelForm() {
			InitializeComponent();

			CreateCanvas();
			Canvas.AllowDrop = true;
			Canvas.MouseDown += Canvas_MouseDown;
			Canvas.MouseUp += Canvas_MouseUp;
			Canvas.MouseMove += Canvas_MouseMove;
			Canvas.MouseWheel += Canvas_MouseWheel;
			Canvas.QueryContinueDrag += Canvas_QueryContinueDrag;
			Canvas.DragEnter += Canvas_DragEnter;
			Canvas.DragOver += Canvas_DragOver;
			Canvas.DragLeave += Canvas_DragLeave;
			Canvas.DragDrop += Canvas_DragDrop;
		}

		/// <summary>
		/// Начало перетаскивания на канвас
		/// </summary>
		void Canvas_QueryContinueDrag(object sender, QueryContinueDragEventArgs e) {
			if (dragTexture != null) {
				e.Action = DragAction.Continue;
			} else {
				e.Action = DragAction.Cancel;
			}
		}

		/// <summary>
		/// Вхождение перетаскивания
		/// </summary>
		void Canvas_DragEnter(object sender, DragEventArgs e) {
			e.Effect = DragDropEffects.None;
			dragTexture = null;
			if (e.Data.GetDataPresent(typeof(Project.DraggingEntry))) {
				Project.DraggingEntry de = (Project.DraggingEntry)e.Data.GetData(typeof(Project.DraggingEntry));
				if (textureFile.FileSupported(de.File.Name)) {
					dragTexture = de.File;
				}
			}
			if (dragTexture != null) {
				(FileEditor as ModelEditor).TextureDragEntered(dragTexture);
				Point p = Canvas.PointToClient(new Point(e.X, e.Y));
				e.Effect = (FileEditor as ModelEditor).TextureDragOver(p.X, p.Y) ? DragDropEffects.Link : DragDropEffects.None;
			}
		}

		/// <summary>
		/// Перетаскивание над контролом
		/// </summary>
		void Canvas_DragOver(object sender, DragEventArgs e) {
			if (dragTexture != null) {
				Point p = Canvas.PointToClient(new Point(e.X, e.Y));
				e.Effect = (FileEditor as ModelEditor).TextureDragOver(p.X, p.Y) ? DragDropEffects.Link : DragDropEffects.None;
			}
		}

		/// <summary>
		/// Перетаскивание вышло за границы контрола
		/// </summary>
		void Canvas_DragLeave(object sender, EventArgs e) {
			if (dragTexture != null) {
				(FileEditor as ModelEditor).TextureDragCanceled();
			}
			dragTexture = null;
		}

		/// <summary>
		/// Перетащен файл
		/// </summary>
		void Canvas_DragDrop(object sender, DragEventArgs e) {
			e.Effect = DragDropEffects.Link;
			if (dragTexture != null) {
				Point p = Canvas.PointToClient(new Point(e.X, e.Y));
				e.Effect = (FileEditor as ModelEditor).TextureDragOver(p.X, p.Y) ? DragDropEffects.Link : DragDropEffects.None;
				(FileEditor as ModelEditor).TextureDropped();
			}
			dragTexture = null;
		}



		


		/// <summary>
		/// Мышь нажата
		/// </summary>
		void Canvas_MouseDown(object sender, MouseEventArgs e) {
			bool allowDrag = e.Button == System.Windows.Forms.MouseButtons.Right;
			if (!allowDrag) {
				if (e.Button == System.Windows.Forms.MouseButtons.Left) {
					int surf = (FileEditor as ModelEditor).GetSurfaceUnderCoords(e.Location.X, e.Location.Y);
					if (surf == -1 || propertyTabs.SelectedTab != surfacePage) {
						allowDrag = true;
					} else {
						surfacesList.SelectedItem = surfacesList.Items[surf];
					}
				}
			}
			if (firstPersonButton.Checked) {
				allowDrag = false;
			}
			if (!mouseLook && allowDrag) {
				mouseLook = true;
				dragButton = e.Button;
				Canvas.LockMouse = true;
				Cursor.Hide();
			}
		}

		/// <summary>
		/// Мышь отпущена
		/// </summary>
		private void Canvas_MouseUp(object sender, MouseEventArgs e) {
			if (mouseLook && e.Button == dragButton) {
				mouseLook = false;
				dragButton = System.Windows.Forms.MouseButtons.None;
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
			} else {
				//(FileEditor as ModelEditor).MouseMove(e.Location);
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
			AdjustSurfaceColumns();
		}

		/// <summary>
		/// Режим просмотра изменен
		/// </summary>
		private void cellGizmoButton_CheckedChanged(object sender) {
			(FileEditor as ModelEditor).CellGizmoChanged(cellGizmoButton.Checked);
		}

		/// <summary>
		/// Режим просмотра изменен
		/// </summary>
		private void firstPersonButton_CheckedChanged(object sender) {
			(FileEditor as ModelEditor).ViewModeChanged(firstPersonButton.Checked);
		}

		/// <summary>
		/// Изменение выбранного элемента списка сурфейсов
		/// </summary>
		private void surfacesList_SelectedItemChanged(object sender) {
			if (surfacesList.SelectedItem != null) {
				(FileEditor as ModelEditor).ShowSurfaceParams(Array.IndexOf(surfacesList.Items, surfacesList.SelectedItem));
				surfaceTexPanel.Visible = true;
			} else {
				surfaceTexPanel.Visible = false;
			}
			AdjustSurfaceColumns();
		}

		/// <summary>
		/// Изменена текстура поверхности
		/// </summary>
		private void textureFile_FileChanged(object sender, EventArgs e) {
			(FileEditor as ModelEditor).SurfaceTextureChanged(textureFile.File);
		}

		/// <summary>
		/// Изменены флаги поверхности
		/// </summary>
		private void surfaceFlags_CheckedChanged(object sender) {
			(FileEditor as ModelEditor).SurfaceFlagsChanged(surfaceOpaque.Checked, surfaceUnlit.Checked);
		}

		/// <summary>
		/// Перестройка колонок списка поверхностей
		/// </summary>
		private void AdjustSurfaceColumns() {
			if (surfacesList != null) {
				surfacesList.Columns[1].Width = surfacesList.Width - 160;
				if (surfacesList.Columns[1].Width < 1) {
					surfacesList.Columns[1].Width = 1;
				}
				surfacesList.Invalidate();
			}
		}

	}
}
