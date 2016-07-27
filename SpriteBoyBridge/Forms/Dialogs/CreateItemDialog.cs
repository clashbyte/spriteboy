using SpriteBoy.Data.Editing.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteBoy.Forms.Dialogs {

	/// <summary>
	/// Окно выбора имени нового файла
	/// </summary>
	public partial class CreateItemDialog : Form {

		/// <summary>
		/// Иконка
		/// </summary>
		static ShadowImage icon;

		/// <summary>
		/// Существующие имена файлов
		/// </summary>
		public string[] ExistingNames {
			get {
				return existingNames;
			}
			set {
				if (value == null) {
					existingNames = new string[0];
				} else {
					existingNames = value;
				}
			}
		}

		/// <summary>
		/// Расширение файла
		/// </summary>
		public string Extension {
			get;
			set;
		}

		/// <summary>
		/// Выбранное имя
		/// </summary>
		public string SpecifiedName {
			get;
			private set;
		}

		/// <summary>
		/// Существующие имена
		/// </summary>
		string[] existingNames;

		/// <summary>
		/// Текст ошибки
		/// </summary>
		string errorText = "";

		/// <summary>
		/// Есть ли ошибка
		/// </summary>
		bool hasError = false;

		/// <summary>
		/// Форма закрыта кнопкой
		/// </summary>
		bool selfExit;

		/// <summary>
		/// Создание окна
		/// </summary>
		public CreateItemDialog(string title) {
			InitializeComponent();
			Text = title;
			createButton.Enabled = false;
			existingNames = new string[0];
			hasError = false;
		}

		/// <summary>
		/// Отрисовка формы
		/// </summary>
		protected override void OnPaint(PaintEventArgs e) {
			Graphics g = e.Graphics;
			g.Clear(Color.FromArgb(50, 50, 50));

			if (hasError) {
				if (icon == null) {
					icon = new ShadowImage(ControlImages.dialogerror);
				}
				icon.Draw(g, new Rectangle(12, 72, 24, 24), 1);
				g.DrawString(errorText, Font, Brushes.Black, 43, 78);
				g.DrawString(errorText, Font, Brushes.White, 42, 77);
			}
		}

		/// <summary>
		/// Файл изменён
		/// </summary>
		private void nameBox_TextChanged(object sender, EventArgs e) {
			string txt = nameBox.Text;
			hasError = false;
			if (txt=="") {
				createButton.Enabled = false;
				Invalidate();
				return;
			}
			txt = txt.ToLower();

			if (txt.IndexOfAny(System.IO.Path.GetInvalidFileNameChars())>=0 || txt.IndexOf('.')>=0) {
				hasError = true;
				errorText = ControlStrings.FileNameIncorrect;
			}
			if (Extension!=null) {
				txt += Extension.ToLower();
			}
			if(existingNames.Contains(txt) && !hasError){
				hasError = true;
				errorText = ControlStrings.FileNameExists;
			}

			createButton.Enabled = !hasError;
			Invalidate();
		}

		/// <summary>
		/// Кнопка "Создать" нажата
		/// </summary>
		private void createButton_Click(object sender, EventArgs e) {
			if (hasError || nameBox.Text == "") {
				return;
			}
			DialogResult = System.Windows.Forms.DialogResult.OK;
			string txt = nameBox.Text;
			if (Extension!=null) {
				txt += Extension;
			}
			SpecifiedName = txt;
			selfExit = true;
			Close();
		}

		/// <summary>
		/// Нажата кнопка "Отмена"
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e) {
			Close();
		}

		/// <summary>
		/// Закрытие формы
		/// </summary>
		private void CreateItemDialog_FormClosed(object sender, FormClosedEventArgs e) {
			if (!selfExit) {
				DialogResult = System.Windows.Forms.DialogResult.Cancel;
			}
		}

		/// <summary>
		/// Нажатие клавиши
		/// </summary>
		private void nameBox_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				if (createButton.Enabled) {
					if (hasError || nameBox.Text == "") {
						return;
					}
					DialogResult = System.Windows.Forms.DialogResult.OK;
					string txt = nameBox.Text;
					if (Extension != null) {
						txt += Extension;
					}
					SpecifiedName = txt;
					selfExit = true;
					Close();
				}
			}else if(e.KeyCode == Keys.Escape) {
				Close();
			}
		}

		/// <summary>
		/// Обработка нажатия абстрактной клавиши
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			if (keyData.HasFlag(Keys.Enter)) {
				if (createButton.Enabled) {
					if (hasError || nameBox.Text == "") {
						return false;
					}
					DialogResult = System.Windows.Forms.DialogResult.OK;
					string txt = nameBox.Text;
					if (Extension != null) {
						txt += Extension;
					}
					SpecifiedName = txt;
					selfExit = true;
					Close();
				}
			} else if(keyData.HasFlag(Keys.Escape)) {
				Close();
			} else {
				return base.ProcessCmdKey(ref msg, keyData);
			}
			return false;
		}
	}
}
