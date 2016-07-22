using SpriteBoy.Data.Editing;
using SpriteBoy.Forms.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Forms.Common {
	
	/// <summary>
	/// Части формы, отвечающие за события окна
	/// </summary>
	public partial class MainForm  {

		/// <summary>
		/// К форме вернулся фокус
		/// </summary>
		private void MainForm_Activated(object sender, EventArgs e) {
			System.Diagnostics.Debug.WriteLine("Focused");
		}

		/// <summary>
		/// Форма потеряла фокус
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Deactivate(object sender, EventArgs e) {

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
						b.FileEditor.ProjectEntryEvent(e, ev);
					}
				}

				// Обновление инспектора
				if (!W.localFileEvent) {
					

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
						b.FileEditor.ProjectDirEvent(d, ev);
					}
				}

				// Обновление инспектора
				if (!W.localFileEvent) {


				}
			}
		}
	}
}
