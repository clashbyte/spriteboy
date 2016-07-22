using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteBoy.Controls;
using SpriteBoy.Data;

namespace SpriteBoy.Forms {

	/// <summary>
	/// Основное окно редактора
	/// </summary>
	public partial class MainForm : Form {

		/// <summary>
		/// Список открытых редакторов
		/// </summary>
		public static BaseForm[] Editors { get; private set; }

		/// <summary>
		/// Синглтон-ссылка на окно
		/// </summary>
		public static MainForm W { get; private set; }

		/// <summary>
		/// Конструктор с заданием синглтона
		/// </summary>
		public MainForm() {
			InitializeComponent();
			W = this;

			// Разворот в полный экран на втором мониторе
			#if DEBUG
			if (Screen.AllScreens.Length>1) {
				foreach (Screen s in Screen.AllScreens) {
					if (!s.Primary) {
						StartPosition = FormStartPosition.Manual;
						WindowState = FormWindowState.Normal;
						Location = s.Bounds.Location;
						WindowState = FormWindowState.Maximized;
						break;
					}
				}
			}
			#endif


			// Обработчик загрузки превьюшек
			Preview.PreviewsReady += Preview_PreviewsReady;

			AddEditor(new DashboardForm());
			AddEditor(new MapForm());

			Data.Project.Open("D:\\NewEngine\\Project");
			PopulateManager(Project.BaseDir);
		}

		/// <summary>
		/// Событие загрузки изображений
		/// </summary>
		/// <param name="e"></param>
		void Preview_PreviewsReady(Events.Data.PreviewReadyEventArgs e) {
			projectInspector.Invalidate();
		}

		/// <summary>
		/// Двойной щелчок по файлу в инспекторе
		/// </summary>
		private void projectInspector_MouseDoubleClick(object sender, MouseEventArgs e) {
			NSDirectoryInspector.Entry en = projectInspector.SelectedEntry;
			if (en != null) {
				if (en.IsDirectory) {
					Project.Dir dr = en.Tag as Project.Dir;
					if (dr != null) {
						PopulateManager(dr);
					}
				} else {
					
					// TODO: Открытие файла

				}
			}
		}

		/// <summary>
		/// Переход на уровень вверх в инспекторе
		/// </summary>
		private void projectDirUp_Click(object sender, EventArgs e) {
			Project.Dir dr = projectInspector.Tag as Project.Dir;
			if (dr != null) {
				if (dr.Parent != null) {
					PopulateManager(dr.Parent);
				}
			}
		}

		/// <summary>
		/// Добавление нового редактора
		/// </summary>
		/// <param name="editor">Редактор</param>
		public static void AddEditor(BaseForm editor) {
			if (Editors != null) {
				if (Editors.Contains(editor)) {
					return;
				}
			}

			TabPage p = new TabPage(editor.Name);
			p.Tag = (object)editor;

			editor.FormBorderStyle = FormBorderStyle.None;
			editor.Dock = DockStyle.Fill;
			editor.TopLevel = false;
			editor.TextChanged += delegate(object sender, EventArgs e) {
				p.Text = ((BaseForm)sender).Text;
			};
			p.Controls.Add(editor);
			editor.Show();
			W.projectTabs.AddTab(p, true);

			List<BaseForm> edlist = Editors != null ? Editors.ToList() : new List<BaseForm>();
			edlist.Add(editor);
			Editors = edlist.ToArray();
		}

		/// <summary>
		/// Закрытие редактора
		/// </summary>
		/// <param name="editor">Редактор</param>
		public static void RemoveEditor(BaseForm editor) {
			if (Editors != null) {
				if (!Editors.Contains(editor)) {
					return;
				}
			}

			TabPage p = GetPageForEditor(editor);
			if (p == null) {
				return;
			}

		}

		/// <summary>
		/// Заполнение менеджера проекта
		/// </summary>
		/// <param name="dir">Директория для открытия</param>
		static void PopulateManager(Project.Dir dir) {

			// Очистка
			W.projectInspector.Entries.Clear();
			W.projectInspector.Tag = (object)dir;

			// Путь до папки
			string path = "Проект"+dir.Name.Substring(Project.BaseDir.Name.Length).Replace("\\", "/");
			if (!path.EndsWith("/")) {
				path += "/";
			}
			W.projectDir.Text = path;

			// Заполнение папками
			foreach (Project.Dir d in dir.Dirs) {
				W.projectInspector.Entries.Add(new NSDirectoryInspector.Entry(){
					IsDirectory = true,
					Name = System.IO.Path.GetFileName(d.Name),
					Tag = (object)d
				});
			}

			// Заполнение файлами
			foreach (Project.Entry e in dir.Entries) {
				if (e.Icon == null) {
					e.Icon = Preview.Get(System.IO.Path.Combine(dir.Name, e.Name));
				}
				W.projectInspector.Entries.Add(new NSDirectoryInspector.Entry() {
					IsDirectory = false,
					Name = System.IO.Path.GetFileNameWithoutExtension(e.Name),
					Icon = e.Icon,
					Tag = (object)e
				});
			}

		}

		/// <summary>
		/// Поиск страницы указанного 
		/// </summary>
		/// <param name="f"></param>
		/// <returns></returns>
		static TabPage GetPageForEditor(BaseForm f) {
			if (W==null) {
				return null;
			}

			// Поиск редактора
			foreach (TabPage p in W.projectTabs.TabPages) {
				if ((BaseForm)p.Tag == f) {
					return p;
				}
			}
			return null;
		}

		/// <summary>
		/// Закрытие таба
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void projectTabs_TabClose(object sender, NSProjectControl.TabCloseEventArgs e) {
			TabPage p = e.Page;
			SpriteBoy.Events.Forms.EditorCloseEventArgs ev = new Events.Forms.EditorCloseEventArgs() {
				Cancel = false
			};
			BaseForm f = (BaseForm)p.Tag;
			e.Cancel = !f.AllowClose();
		}

	}
}
