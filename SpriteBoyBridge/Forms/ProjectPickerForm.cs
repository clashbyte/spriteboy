using SpriteBoy.Controls;
using SpriteBoy.Data.Editing;
using SpriteBoy.Data.Editing.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteBoy.Forms {

	/// <summary>
	/// Форма для выбора файла
	/// </summary>
	partial class ProjectPickerForm : Form {

		/// <summary>
		/// Дроппер для данного пикера
		/// </summary>
		public NSFileDropControl Dropper {
			get;
			set;
		}

		/// <summary>
		/// Текущий каталог
		/// </summary>
		Project.Dir currentDir;

		/// <summary>
		/// Предыдущий файл
		/// </summary>
		Project.Entry prevFile;

		/// <summary>
		/// Конструктор формы
		/// </summary>
		public ProjectPickerForm() {
			InitializeComponent();
			
		}


		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(30,30,30)), 0, 0, Width-1, Height-1);
		}

		protected override void OnDeactivate(EventArgs e) {
			base.OnDeactivate(e);
			if (Dropper!=null && DialogResult != System.Windows.Forms.DialogResult.OK) {
				Dropper.File = prevFile;
			}
			Close();
		}

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);
			prevFile = Dropper.File;
			if (Dropper.File!=null) {
				currentDir = Dropper.File.Parent;
			}else{
				currentDir = Project.BaseDir;
			}
			Populate(true);
			Preview.PreviewsReady += Preview_PreviewsReady;
			Focus();
		}

		void Preview_PreviewsReady(Events.Data.PreviewReadyEventArgs e) {
			inspector.Invalidate();
		}

		protected override void OnClosed(EventArgs e) {
			base.OnClosed(e);
			Preview.PreviewsReady -= Preview_PreviewsReady;
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			Close();
		}

		private void submitButton_Click(object sender, EventArgs e) {
			if (inspector.SelectedEntry != null) {
				if (inspector.SelectedEntry.IsDirectory) {
					currentDir = inspector.SelectedEntry.Tag as Project.Dir;
					inspector.SelectedEntry = null;
					submitButton.Enabled = false;
					Populate();
				} else {
					DialogResult = System.Windows.Forms.DialogResult.OK;
					Dropper.File = inspector.SelectedEntry.Tag as Project.Entry;
					Close();
				}
			}
		}

		private void clearButton_Click(object sender, EventArgs e) {
			DialogResult = System.Windows.Forms.DialogResult.OK;
			if (Dropper != null) {
				Dropper.File = null;
			}
			Close();
		}

		private void nsUpButton_Click(object sender, EventArgs e) {
			if (currentDir.Parent != null) {
				currentDir = currentDir.Parent;
				Populate();
			}
		}

		private void inspector_MouseDown(object sender, MouseEventArgs e) {
			submitButton.Enabled = inspector.SelectedEntry != null;
			if (inspector.SelectedEntry!=null) {
				if (Dropper != null && !inspector.SelectedEntry.IsDirectory) {
					Dropper.File = (Project.Entry)inspector.SelectedEntry.Tag;
				}
			}
		}

		private void Populate(bool select = false) {

			// Очистка
			inspector.Entries.Clear();

			// Путь до папки
			fileLabel.Text = currentDir.Name;
			submitButton.Enabled = false;

			// Заполнение папками
			foreach (Project.Dir d in currentDir.Dirs) {
				inspector.Entries.Add(new NSDirectoryInspector.Entry() {
					IsDirectory = true,
					Name = System.IO.Path.GetFileName(d.Name),
					Tag = (object)d
				});
			}

			// Заполнение файлами
			foreach (Project.Entry e in currentDir.Entries) {
				if (Dropper.FileSupported(e.Name)) {
					if (e.Icon == null) {
						e.Icon = Preview.Get(e.FullPath);
					}
					inspector.Entries.Add(new NSDirectoryInspector.Entry() {
						IsDirectory = false,
						Name = System.IO.Path.GetFileNameWithoutExtension(e.Name),
						Icon = e.Icon,
						Tag = (object)e
					});
					if (select && prevFile == e) {
						inspector.SelectedEntry = inspector.Entries[inspector.Entries.Count - 1];
						submitButton.Enabled = true;
					}
				}
			}

		}
	}
}
