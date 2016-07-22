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
	public partial class RenameItemDialog : Form {

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
			set;
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
		public RenameItemDialog(string title) {
			InitializeComponent();
			Text = title;
			renameButton.Enabled = false;
			existingNames = new string[0];
			hasError = false;
		}

		protected override void OnShown(EventArgs e) {
			nameBox.Text = SpecifiedName;
			base.OnShown(e);
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
				renameButton.Enabled = false;
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
			if(existingNames.Contains(txt) && !hasError && txt != SpecifiedName.ToLower()){
				hasError = true;
				errorText = ControlStrings.FileNameExists;
			}

			renameButton.Enabled = !hasError;
			Invalidate();
		}

		/// <summary>
		/// Кнопка "Переименовать" нажата
		/// </summary>
		private void renameButton_Click(object sender, EventArgs e) {
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
	}
}
