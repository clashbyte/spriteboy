using SpriteBoy.Data.Editing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SpriteBoy.Forms.Dialogs {
	public partial class DirectoryCopyDialog : Form {

		/// <summary>
		/// Копированная директория
		/// </summary>
		public Project.Dir CopiedDir {
			get;
			private set;
		}

		/// <summary>
		/// Внутренние переменные для копирования
		/// </summary>
		Project.Dir copyDir, newParent;

		/// <summary>
		/// Количество файлов
		/// </summary>
		int numFiles;

		/// <summary>
		/// Количество скопированных файлов
		/// </summary>
		int filesCopied;

		/// <summary>
		/// Отмена копирования
		/// </summary>
		bool abortCopy;

		/// <summary>
		/// Текущее имя файла
		/// </summary>
		string currentFileName;

		/// <summary>
		/// Копирующий поток
		/// </summary>
		Thread copyThread;

		/// <summary>
		/// Закрывается ли форма сама
		/// </summary>
		bool selfClosing;

		/// <summary>
		/// Поток закончил работу
		/// </summary>
		bool threadComplete;

		/// <summary>
		/// Создание копирующего диалога
		/// </summary>
		public DirectoryCopyDialog(Project.Dir dir, Project.Dir parent) {
			InitializeComponent();
			copyDir = dir;
			newParent = parent;
		}

		/// <summary>
		/// Показ окна
		/// </summary>
		protected override void OnShown(EventArgs e) {
			base.OnShown(e);
			copyThread = new Thread(ThreadedWork);
			copyThread.Priority = ThreadPriority.BelowNormal;
			copyThread.IsBackground = true;
			copyThread.Start();
			stateTimer.Start();
		}

		/// <summary>
		/// Закрытие формы
		/// </summary>
		protected override void OnFormClosing(FormClosingEventArgs e) {
			base.OnFormClosing(e);
			if (!selfClosing) {
				if (!abortCopy) {
					abortCopy = true;
					cancelButton.Enabled = false;
				}
				e.Cancel = true;
			}else{
				e.Cancel = false;
			}
		}

		/// <summary>
		/// Отмена копирования
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e) {
			if (!abortCopy) {
				abortCopy = true;
				cancelButton.Enabled = false;
			}
		}

		/// <summary>
		/// Тик таймера состояния
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stateTimer_Tick(object sender, EventArgs e) {
			if (threadComplete) {
				copyProgress.Value = copyProgress.Maximum;
				stateTimer.Stop();
				selfClosing = true;
				Close();
			} else {
				if (copyProgress.Maximum != numFiles) {
					copyProgress.Maximum = numFiles;
				}
				if (copyProgress.Value != filesCopied) {
					copyProgress.Value = filesCopied;
				}
				if (fileNameLabel.Text != currentFileName) {
					fileNameLabel.Text = currentFileName;
				}
			}
			
		}

		/// <summary>
		/// Фоновая поточная работа
		/// </summary>
		void ThreadedWork() {
			
			// Сканирование файлов для копирования
			RecursiveCalculateFiles(copyDir);

			// Копирование папки
			CopiedDir = RecursiveCopyDir(copyDir, newParent);
			List<Project.Dir> dirs = newParent.Dirs.ToList();
			dirs.Add(CopiedDir);
			dirs.Sort((a, b) => {
				return a.ShortName.CompareTo(b.ShortName);
			});
			newParent.Dirs = dirs.ToArray();

			// Копирование закончено
			threadComplete = true;
		}

		/// <summary>
		/// Рекурсивный подсчёт файлов
		/// </summary>
		/// <param name="d"></param>
		void RecursiveCalculateFiles(Project.Dir d) {
			numFiles += d.Entries.Length;
			foreach (Project.Dir cd in d.Dirs) {
				RecursiveCalculateFiles(cd);
			}
		}

		/// <summary>
		/// Рекурсивное копирование директории
		/// </summary>
		/// <param name="cd">Директория</param>
		/// <param name="parent">Новый родитель</param>
		/// <returns>Скопированная директория</returns>
		Project.Dir RecursiveCopyDir(Project.Dir cd, Project.Dir parent) {

			// Подбор имени папки
			string dirName = cd.ShortName;
			while (Directory.Exists(parent.FullPath + "/" + dirName)) {
				dirName += " - Copy";
			}
			
			// Создание папки
			Directory.CreateDirectory(parent.FullPath + "/" + dirName);

			// Создание объекта папки
			Project.Dir d = new Project.Dir();
			d.Parent = parent;
			d.Name = parent != Project.BaseDir ? parent.Name + "/" + dirName : dirName;

			// Внутренние части
			List<Project.Dir> dirs = new List<Project.Dir>();
			List<Project.Entry> entries = new List<Project.Entry>();

			if (!abortCopy) {
				
				// Копирование файлов
				foreach (Project.Entry e in cd.Entries) {

					// Создание записи
					currentFileName = e.Name;
					filesCopied++;
					Project.Entry ce = new Project.Entry();
					ce.Name = e.Name;
					ce.Parent = d;
					entries.Add(ce);

					// Копирование файла
					File.Copy(e.FullPath, ce.FullPath);

					// Выход если отмена
					if (abortCopy) {
						break;
					}
				}

				// Копирование папок
				foreach (Project.Dir dr in cd.Dirs) {
					dirs.Add(RecursiveCopyDir(dr, d));

					// Выход если отмена
					if (abortCopy) {
						break;
					}
				}

			}
			d.Dirs = dirs.ToArray();
			d.Entries = entries.ToArray();

			// Сохранение папки
			return d;
		}

	}
}
