﻿using SpriteBoy.Controls;
using SpriteBoy.Data.Editing;
using SpriteBoy.Data.Editing.Graphics;
using SpriteBoy.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteBoy.Forms.Common {

	/// <summary>
	/// Часть формы, отвечающая за работу с инспектором
	/// </summary>
	partial class MainForm {

		/// <summary>
		/// Копированная папка
		/// </summary>
		Project.Dir copyingDir;

		/// <summary>
		/// Копированный файл
		/// </summary>
		Project.Entry copyingEntry;

		/// <summary>
		/// Расположение попапа инспектора
		/// </summary>
		Point inspectorPopupLocation;

		/// <summary>
		/// Флаг, что следующий ивент надо будет проигнорировать
		/// </summary>
		bool localFileEvent;

		/// <summary>
		/// Двойной щелчок по файлу в инспекторе
		/// </summary>
		private void projectInspector_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				NSDirectoryInspector.Entry en = projectInspector.SelectedEntry;
				if (en != null) {
					if (en.IsDirectory) {
						Project.Dir dr = en.Tag as Project.Dir;
						if (dr != null) {
							PopulateManager(dr);
						}
					} else {
						OpenEditor(en.Tag as Project.Entry);
					}
				}
			}
		}

		/// <summary>
		/// Смена выбранного элемента инспектора
		/// </summary>
		private void projectInspector_SelectionChanged(object sender) {
			projectFileInfo.File = projectInspector.SelectedEntry;
		}

		/// <summary>
		/// Клик по файлу в инспекторе
		/// </summary>
		private void projectInspector_MouseClick(object sender, MouseEventArgs e) {
			NSDirectoryInspector.Entry en = projectInspector.SelectedEntry;
			bool free = false;
			if (en != null) {
				free = Project.OperableFile(en.Tag);
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right) {

				NSContextMenu cm = new NSContextMenu();
				if (en != null) {
					// Открытие файла
					cm.Items.Add(ControlStrings.InspectorContextOpen, ShadowImage.CompiledFromImage(InspectorIcons.MenuOpen, 16, 1), (sndr, args) => {

						if (en != null) {
							if (en.IsDirectory) {
								Project.Dir dr = en.Tag as Project.Dir;
								if (dr != null) {
									PopulateManager(dr);
								}
							} else {
								OpenEditor(en.Tag as Project.Entry);
							}
						}
					});
				}

				// Создание файла
				ToolStripMenuItem crItem = new ToolStripMenuItem(ControlStrings.InspectorContextCreate, ShadowImage.CompiledFromImage(InspectorIcons.MenuAdd, 16, 1));
				crItem.DropDown = new NSContextMenu();
				AddCreationMenuItems(crItem.DropDown.Items);
				cm.Items.Add(crItem);

				// Разделитель
				cm.Items.Add(new ToolStripSeparator());

				if (en != null) {
					// Смена имени
					cm.Items.Add(new ToolStripMenuItem(ControlStrings.InspectorContextRename, ShadowImage.CompiledFromImage(InspectorIcons.MenuRename, 16, 1), (sndr, args) => {
						string[] used;
						string ext = "";
						if (en.IsDirectory) {
							used = (en.Tag as Project.Dir).UsedNames;
						}else{
							used = (en.Tag as Project.Entry).Parent.UsedNames;
							ext = System.IO.Path.GetExtension((en.Tag as Project.Entry).Name);
						}

						RenameItemDialog rd = new RenameItemDialog(en.Name);
						rd.SpecifiedName = en.Name;
						rd.Extension = ext;
						rd.ExistingNames = used;
						needProjectRescan = false;
						if (rd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
							if (en.IsDirectory) {
								Project.RenameItem(en.Tag as Project.Dir, rd.SpecifiedName);
							} else {
								Project.RenameItem(en.Tag as Project.Entry, rd.SpecifiedName);
							}
						}

					}) {
						Enabled = free
					});

					// Копирование
					cm.Items.Add(new ToolStripMenuItem(ControlStrings.InspectorContextCopy, ShadowImage.CompiledFromImage(InspectorIcons.MenuCopy, 16, 1), (sndr, args) => {
						if (en != null) {
							copyingDir = null;
							copyingEntry = null;
							if (en.IsDirectory) {
								copyingDir = en.Tag as Project.Dir;
							} else {
								copyingEntry = en.Tag as Project.Entry;
							}
						}
					}) {
						Enabled = free
					});
				}

				// Вставка
				ToolStripMenuItem ps = new ToolStripMenuItem(ControlStrings.InspectorContextPaste, ShadowImage.CompiledFromImage(InspectorIcons.MenuPaste, 16, 1), (sndr, args) => {
					Project.Dir bd = (Project.Dir)projectInspector.Tag;
					localFileEvent = true;
					needProjectRescan = false;
					if (copyingDir != null) {
						Project.Dir cd = Project.CopyDir(copyingDir, bd);
						NSDirectoryInspector.Entry fen = new NSDirectoryInspector.Entry();
						fen.IsDirectory = true;
						fen.Name = cd.ShortName;
						fen.Tag = (object)cd;
						projectInspector.Entries.Add(fen);
						projectInspector.SelectedEntry = fen;
					} else if(copyingEntry != null) {
						Project.Entry ce = Project.CopyEntry(copyingEntry, bd);
						NSDirectoryInspector.Entry fen = new NSDirectoryInspector.Entry();
						fen.IsDirectory = false;
						fen.Name = System.IO.Path.GetFileNameWithoutExtension(ce.Name);
						fen.Icon = ce.Icon;
						fen.Tag = (object)ce;
						projectInspector.Entries.Add(fen);
						projectInspector.SelectedEntry = fen;
					}
					localFileEvent = false;
				});
				ps.Enabled = copyingDir != null || copyingEntry != null;
				cm.Items.Add(ps);

				if (en != null) {
					// Удаление
					cm.Items.Add(new ToolStripMenuItem(ControlStrings.InspectorContextDelete, ShadowImage.CompiledFromImage(InspectorIcons.MenuDelete, 16, 1), (sndr, args) => {
						if (en != null) {
							RemoveEntry(en);
						}
					}) {
						Enabled = free
					});
				}

				cm.Show(inspectorPopupLocation);
			}
		}

		/// <summary>
		/// Мышь нажата в инспекторе
		/// </summary>
		private void projectInspector_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Right) {
				inspectorPopupLocation = Cursor.Position;
			} else if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				bool removable = false;
				if (projectInspector.SelectedEntry != null) {
					removable = Project.OperableFile(projectInspector.SelectedEntry.Tag);
				}
				projectRemove.Enabled = removable;
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
		/// Добавление нового энтри в проект
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void projectAdd_Click(object sender, EventArgs e) {
			NSContextMenu cm = new NSContextMenu();
			AddCreationMenuItems(cm.Items);
			cm.Show(projectAdd, new Point(0, projectAdd.Height));
		}

		/// <summary>
		/// Удаление чего то
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void projectRemove_Click(object sender, EventArgs e) {
			NSDirectoryInspector.Entry en = projectInspector.SelectedEntry;
			if (en != null) {
				RemoveEntry(en);
			}
		}

		/// <summary>
		/// Создание меню создаваемых файлов
		/// </summary>
		/// <param name="col">Список для добавления</param>
		private void AddCreationMenuItems(ToolStripItemCollection col) {

			// Папка
			col.Add(ControlStrings.InspectorContextFolder, ShadowImage.CompiledFromImage(InspectorIcons.Folder, 16, 1), (sndr, args) => {
				Project.Dir d = projectInspector.Tag as Project.Dir;
				CreateItemDialog dlg = new CreateItemDialog(ControlStrings.InspectorContextFolder);
				dlg.ExistingNames = d.UsedNames;
				DialogResult dr = dlg.ShowDialog();
				if (dr == System.Windows.Forms.DialogResult.OK) {
					localFileEvent = true;
					NSDirectoryInspector.Entry en = new NSDirectoryInspector.Entry();
					en.IsDirectory = true;
					en.Name = dlg.SpecifiedName;
					en.Tag = Project.CreateDir(dlg.SpecifiedName, d);
					projectInspector.Entries.Add(en);
					projectInspector.SelectedEntry = en;
					localFileEvent = false;
				}
			});

			if (Editor.CreatableList.Length > 0) {
				col.Add(new ToolStripSeparator());
				foreach (Editor.FileCreator fc in Editor.CreatableList) {
					col.Add(fc.Name, ShadowImage.CompiledFromImage(fc.Icon, 16, 1), (sndr, args) => {
						Project.Dir cd = projectInspector.Tag as Project.Dir;
						Editor.FileCreator f = fc;
						if (cd != null) {
							CreateItemDialog dlg = new CreateItemDialog(fc.Name);
							dlg.Extension = f.Extension;
							dlg.ExistingNames = cd.UsedNames;
							if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
								localFileEvent = true;
								Project.Entry ce = Project.CreateEntry(dlg.SpecifiedName, cd);
								NSDirectoryInspector.Entry en = new NSDirectoryInspector.Entry();
								en.IsDirectory = false;
								en.Name = System.IO.Path.GetFileNameWithoutExtension(dlg.SpecifiedName);
								en.Icon = ce.Icon;
								en.Tag = (object)ce;
								projectInspector.Entries.Add(en);
								projectInspector.SelectedEntry = en;
								localFileEvent = false;
							}
						}
					});
				}
			}


		}

		/// <summary>
		/// Удаление записи
		/// </summary>
		/// <param name="en">Элемент в инспекторе</param>
		void RemoveEntry(NSDirectoryInspector.Entry en) {
			if (en.IsDirectory) {
				if (MessageDialog.Show(ControlStrings.DeleteFolderTitle, ControlStrings.DeleteFolderText.Replace("%FOLDER%", en.Name), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
					localFileEvent = true;
					Project.DeleteDir(en.Tag as Project.Dir);
					projectInspector.SelectedEntry = null;
					projectInspector.Entries.Remove(en);
					localFileEvent = false;
					projectRemove.Enabled = false;
				}
			} else {
				if (MessageDialog.Show(ControlStrings.DeleteFileTitle, ControlStrings.DeleteFileText.Replace("%FILE%", en.Name), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
					localFileEvent = true;
					Project.DeleteEntry(en.Tag as Project.Entry);
					projectInspector.SelectedEntry = null;
					projectInspector.Entries.Remove(en);
					localFileEvent = false;
					projectRemove.Enabled = false;
				}
			}
		}

		/// <summary>
		/// Заполнение менеджера проекта
		/// </summary>
		/// <param name="dir">Директория для открытия</param>
		protected static void PopulateManager(Project.Dir dir) {

			// Очистка
			W.projectInspector.Entries.Clear();
			W.projectInspector.Tag = (object)dir;

			// Путь до папки
			W.projectDir.Text = dir.Name;

			// Заполнение папками
			foreach (Project.Dir d in dir.Dirs) {
				W.projectInspector.Entries.Add(new NSDirectoryInspector.Entry() {
					IsDirectory = true,
					Name = System.IO.Path.GetFileName(d.Name),
					Tag = (object)d
				});
			}

			// Заполнение файлами
			foreach (Project.Entry e in dir.Entries) {
				if (e.Icon == null) {
					e.Icon = Preview.Get(e.FullPath);
				}
				W.projectInspector.Entries.Add(new NSDirectoryInspector.Entry() {
					IsDirectory = false,
					Name = System.IO.Path.GetFileNameWithoutExtension(e.Name),
					Icon = e.Icon,
					Tag = (object)e
				});
			}

			W.projectRemove.Enabled = false;
		}
	}
}
