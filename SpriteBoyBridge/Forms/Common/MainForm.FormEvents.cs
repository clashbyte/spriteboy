using SpriteBoy.Data.Editing;
using SpriteBoy.Forms.Editors;
using SpriteBoy.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteBoy.Data.Editing.Graphics;
using System.IO;

namespace SpriteBoy.Forms.Common {
	
	/// <summary>
	/// Части формы, отвечающие за события окна
	/// </summary>
	partial class MainForm  {

		/// <summary>
		/// Нужен ли рескан проекта при запуске
		/// </summary>
		static bool needProjectRescan;

		/// <summary>
		/// К форме вернулся фокус
		/// </summary>
		private void MainForm_Activated(object sender, EventArgs e) {
			if (needProjectRescan) {
				Project.ValidateState();
			}
			needProjectRescan = true;
		}

		/// <summary>
		/// Форма потеряла фокус
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Deactivate(object sender, EventArgs e) {
			Project.CacheState();
		}

		/// <summary>
		/// Событие загрузки изображений
		/// </summary>
		/// <param name="e"></param>
		void Preview_PreviewsReady(Events.Data.PreviewReadyEventArgs e) {
			projectInspector.Invalidate();
		}



		/// <summary>
		/// Событие, связанное с файлом проекта
		/// </summary>
		public static void ProjectEntryEvent(Project.Entry e, Project.FileEvent ev) {
			System.Diagnostics.Debug.WriteLine("File " + e.ProjectPath + " " + ev.ToString());
			if (W != null) {

				// Обновление редакторов
				if (Editors!=null) {
					foreach (BaseForm b in Editors) {
						needProjectRescan = false;
						b.FileEditor.ProjectEntryEvent(e, ev);
					}
				}

				// Обновление инспектора
				if (!W.localFileEvent) {
					Project.Dir cd = W.projectInspector.Tag as Project.Dir;
					switch (ev) {
						case Project.FileEvent.Created:
							if (e.Parent == cd) {
								if (e.Icon == null) {
									e.Icon = Preview.Get(e.FullPath);
								}
								W.projectInspector.Entries.Add(new NSDirectoryInspector.Entry() {
									IsDirectory = false,
									Tag = (object)e,
									Name = Path.GetFileNameWithoutExtension(e.Name),
									Icon = e.Icon
								});
								W.projectInspector.Invalidate();
							}
							break;
						case Project.FileEvent.Modified:
						case Project.FileEvent.Renamed:
							if (e.Parent == cd) {
								object oe = (object)e;
								if (ev == Project.FileEvent.Modified) {
									e.Icon = Preview.Get(e.FullPath);
								}
								foreach (NSDirectoryInspector.Entry en in W.projectInspector.Entries) {
									if (en.Tag == oe) {
										en.Name = Path.GetFileNameWithoutExtension(e.Name);
										en.Icon = e.Icon;
										W.projectInspector.Invalidate();
										break;
									}
								}
							}
							break;
						case Project.FileEvent.Deleted:
							if (e.Parent == cd) {
								object oe = (object)e;
								foreach (NSDirectoryInspector.Entry en in W.projectInspector.Entries) {
									if (en.Tag == oe) {
										W.projectInspector.Entries.Remove(en);
										W.projectInspector.Invalidate();
										break;
									}
								}
							}
							break;
					}
				}
			}
		}

		/// <summary>
		/// Событие, связанное с директорией проекта
		/// </summary>
		public static void ProjectDirEvent(Project.Dir d, Project.FileEvent ev) {
			System.Diagnostics.Debug.WriteLine("Dir " + d.Name + " " + ev.ToString());
			if (W != null) {

				// Обновление редакторов
				if (Editors != null) {
					foreach (BaseForm b in Editors) {
						needProjectRescan = false;
						b.FileEditor.ProjectDirEvent(d, ev);
					}
				}

				// Обновление инспектора
				if (!W.localFileEvent) {
					Project.Dir cd = W.projectInspector.Tag as Project.Dir;
					switch (ev) {
						case Project.FileEvent.Created:
							if (d.Parent == cd) {
								W.projectInspector.Entries.Add(new NSDirectoryInspector.Entry(){
									IsDirectory = true,
									Tag = (object)d,
									Name = d.ShortName
								});
								W.projectInspector.Invalidate();
							}
							break;
						case Project.FileEvent.Modified:
						case Project.FileEvent.Renamed:
							if (d.Parent == cd) {
								object od = (object)d;
								foreach (NSDirectoryInspector.Entry en in W.projectInspector.Entries) {
									if (en.Tag == od) {
										en.Name = d.ShortName;
										W.projectInspector.Invalidate();
										break;
									}
								}
							}
							break;
						case Project.FileEvent.Deleted:
							if (d == cd) {
								PopulateManager(d.Parent);
							}else if(d.Parent == cd){
								object od = (object)d;
								foreach (NSDirectoryInspector.Entry en in W.projectInspector.Entries) {
									if (en.Tag == od) {
										W.projectInspector.Entries.Remove(en);
										W.projectInspector.Invalidate();
										break;
									}
								}
							}
							break;
					}
				}
			}
		}
	}
}
