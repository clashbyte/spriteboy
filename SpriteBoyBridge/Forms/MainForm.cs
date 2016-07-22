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
using System.Reflection;
using SpriteBoy.Data.Editing.Graphics;
using SpriteBoy.Data.Editing;

namespace SpriteBoy.Forms {

	/// <summary>
	/// Основное окно редактора
	/// </summary>
	public partial class MainForm : Form {

		/// <summary>
		/// Заголовок окна
		/// </summary>
		static readonly string AppTitle = "SpriteBoy Editor v" + Assembly.GetExecutingAssembly().GetName().Version;

		/// <summary>
		/// Список открытых редакторов
		/// </summary>
		public static BaseForm[] Editors { get; private set; }

		/// <summary>
		/// Синглтон-ссылка на окно
		/// </summary>
		public static MainForm W { get; private set; }

		/// <summary>
		/// Событие загрузки окна
		/// </summary>
		public static event EventHandler WindowLoad;

		/// <summary>
		/// Один кадр движка
		/// </summary>
		public static event EventHandler FrameTick;

		/// <summary>
		/// Конструктор с заданием синглтона
		/// </summary>
		public MainForm() {
			InitializeComponent();
			W = this;
			UpdateHeader();
			WindowState = FormWindowState.Maximized;

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

			if (WindowLoad != null) {
				WindowLoad(null, EventArgs.Empty);
			}

			frameTimer.Start();
		}
		

		/// <summary>
		/// Обновление заголовка
		/// </summary>
		public static void UpdateHeader() {
			if (W!=null) {

			}
		}

		/// <summary>
		/// Загружен проект
		/// </summary>
		public static void ProjectOpened() {
			if (W != null) {
				PopulateManager(Project.BaseDir);
				W.projectTabs.TabPages.Clear();
			}
		}

		/// <summary>
		/// Открытие редактора для файла
		/// </summary>
		/// <param name="e">Файл</param>
		public static void OpenEditor(Project.Entry e) {
			if (W != null) {
				bool found = false;
				if (Editors != null) {
					foreach (BaseForm frm in Editors) {
						if (frm.FileEditor.File == e) {
							W.projectTabs.SelectedTab = frm.Tag as TabPage;
							found = true;
							break;
						}
					}
				}

				if (!found) {
					Editor ed = Editor.Create(e);
					if (ed != null) {
						TabPage p = new TabPage(ed.Title);
						p.Tag = (object)ed.Form;
						ed.Form.Tag = (object)p;

						ed.Form.FormBorderStyle = FormBorderStyle.None;
						ed.Form.Dock = DockStyle.Fill;
						ed.Form.TopLevel = false;
						ed.Form.TextChanged += delegate(object sender, EventArgs ea) {
							p.Text = ((BaseForm)sender).Text;
						};
						p.Controls.Add(ed.Form);
						ed.Form.Show();
						W.projectTabs.AddTab(p, true);

						List<BaseForm> edlist = Editors != null ? Editors.ToList() : new List<BaseForm>();
						edlist.Add(ed.Form);
						Editors = edlist.ToArray();
					} else {
						MessageDialog.Show(ControlStrings.EditorNotFoundTitle, ControlStrings.EditorNotFoundText.Replace("%FILE%", "\n\n" + e.Name + "\n\n"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		/// <summary>
		/// Закрытие редактора
		/// </summary>
		/// <param name="e">Редактор который необходимо закрыть</param>
		public static bool CloseEditor(Editor e, bool force = false) {
			if (Editors != null) {
				if (!Editors.Contains(e.Form)) {
					return true;
				}
			}
			bool close = true;
			if (!force) {
				close = e.AllowClose();
			}

			if (close) {
				TabPage tp = null;
				foreach (TabPage t in W.projectTabs.TabPages) {
					if (tp.Tag == (object)e.Form) {
						tp = t;
						break;
					}
				}
				if (tp!=null) {
					W.projectTabs.RemoveTab(tp); 
					List<BaseForm> edlist = Editors == null ? new List<BaseForm>() : Editors.ToList();
					if (edlist.Contains(e.Form)) {
						edlist.Remove(e.Form);
					}
					Editors = edlist.ToArray();
				}
			}
			return close;
		}

		/// <summary>
		/// Событие изменения содержимого табов
		/// </summary>
		public static void TabsChanged() {
			if (W != null) {
				W.projectTabs.Invalidate();
			}
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
			e.Cancel = !f.FileEditor.AllowClose();
			if (!e.Cancel) {
				if (Editors != null) {
					if (Editors.Length != 0) {
						List<BaseForm> edlist = Editors.ToList();
						edlist.Remove(e.Page.Tag as BaseForm);
						Editors = edlist.ToArray();
					}
				}
			}
		}

		/// <summary>
		/// Обновление кадра
		/// </summary>
		private void frameTimer_Tick(object sender, EventArgs e) {
			if (FrameTick!=null) {
				FrameTick(null, EventArgs.Empty);
			}
			if (projectTabs.SelectedTab != null) {
				Editor ed = (projectTabs.SelectedTab.Tag as BaseForm).FileEditor;
				if (ed != null) {
					ed.Update();
					ed.Render();
				}
			}
		}

		/// <summary>
		/// Обработка нажатия абстрактной клавиши
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.S) == Keys.S) {
				// Сохраняем всё
				if (Editors!=null) {
					foreach (BaseForm f in Editors) {
						if (!f.FileEditor.Saved) {
							f.FileEditor.Save();
						}
					}
				}
			} else {
				return base.ProcessCmdKey(ref msg, keyData);
			}
			return false;
		}
	}
}
