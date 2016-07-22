using SpriteBoy.Controls;
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
	/// Окно с сообщением
	/// </summary>
	public partial class MessageDialog : Form {

		/// <summary>
		/// Иконка для отрисовки
		/// </summary>
		ShadowImage boxIcon;

		/// <summary>
		/// Размеры текста
		/// </summary>
		SizeF textSize;

		/// <summary>
		/// Текст
		/// </summary>
		string labelText;

		/// <summary>
		/// Позиция текста по вертикали
		/// </summary>
		int textLocation;

		/// <summary>
		/// Результаты кнопок
		/// </summary>
		DialogResult[] buttonResults;

		/// <summary>
		/// Флаг, что состояние установлено перед закрытием
		/// </summary>
		bool stateSet;

		/// <summary>
		/// Конструктор диалога
		/// </summary>
		MessageDialog(string caption, string text, string[] buttons, DialogResult[] results, ShadowImage icon) {
			InitializeComponent();
			Font = new Font("Tahoma", 10f);

			Text = caption;
			boxIcon = icon;
			buttonResults = results;
			labelText = text;

			// Создаём графику
			Graphics g = Graphics.FromImage(new Bitmap(1, 1));
			textSize = g.MeasureString(text, Font, 300);
			Size nsz = ClientSize;
			nsz.Height = 50 + (int)textSize.Height;
			textLocation = 30;
			if (nsz.Height < 120) {
				nsz.Height = 120;
				textLocation = 60 - (int)(textSize.Height/2f);
			}
			nsz.Height += buttonPanel.Height;
			ClientSize = nsz;

			// Создаём кнопки
			int ps = ClientSize.Width - 8 - 100;
			for (int i = buttons.Length-1; i >= 0; i--) {
				NSButton btn = new NSButton();
				btn.Text = buttons[i];
				btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
				btn.Tag = i;
				btn.Location = new Point(ps, 19);
				btn.Size = new Size(100, 23);
				btn.Click += ButtonClicked;
				buttonPanel.Controls.Add(btn);
				ps -= 108;
			}
			
		}

		/// <summary>
		/// Отрисовка формы
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e) {
			//base.OnPaint(e);

			Graphics g = e.Graphics;
			g.Clear(Color.FromArgb(50, 50, 50));

			// Иконка
			boxIcon.Draw(g, new Rectangle(30, 30, 60, 60), 2);

			// Текст
			g.DrawString(labelText, Font, Brushes.Black, new RectangleF(121f, textLocation+1, textSize.Width, textSize.Height));
			g.DrawString(labelText, Font, Brushes.White, new RectangleF(120f, textLocation, textSize.Width, textSize.Height));
			
		}

		/// <summary>
		/// Вывод окна с сообщением
		/// </summary>
		/// <param name="caption">Заголовок</param>
		/// <param name="text">Текст</param>
		/// <param name="buttonGroup">Тип кнопок</param>
		/// <param name="iconType">Тип иконки</param>
		/// <returns>Состояние окна</returns>
		public static DialogResult Show(string caption, string text, MessageBoxButtons buttonGroup = MessageBoxButtons.OK, MessageBoxIcon iconType = MessageBoxIcon.Information) {

			// Данные для вывода
			string[] buttons;
			DialogResult[] results;
			ShadowImage icon;

			// Выбор кнопок
			switch (buttonGroup) {
				case MessageBoxButtons.AbortRetryIgnore:
					buttons = new string[]{
						ControlStrings.DialogAbortButton, ControlStrings.DialogRetryButton, ControlStrings.DialogIgnoreButton
					};
					results = new DialogResult[]{
						DialogResult.Abort, DialogResult.Retry, DialogResult.Ignore
					};
					break;
				case MessageBoxButtons.OKCancel:
					buttons = new string[]{
						ControlStrings.DialogOKButton, ControlStrings.DialogCancelButton
					};
					results = new DialogResult[]{
						DialogResult.OK, DialogResult.Cancel
					};
					break;
				case MessageBoxButtons.RetryCancel:
					buttons = new string[]{
						ControlStrings.DialogRetryButton, ControlStrings.DialogCancelButton
					};
					results = new DialogResult[]{
						DialogResult.Retry, DialogResult.Cancel
					};
					break;
				case MessageBoxButtons.YesNo:
					buttons = new string[]{
						ControlStrings.DialogYesButton, ControlStrings.DialogNoButton
					};
					results = new DialogResult[]{
						DialogResult.Yes, DialogResult.No
					};
					break;
				case MessageBoxButtons.YesNoCancel:
					buttons = new string[]{
						ControlStrings.DialogYesButton, ControlStrings.DialogNoButton, ControlStrings.DialogCancelButton
					};
					results = new DialogResult[]{
						DialogResult.Yes, DialogResult.No, DialogResult.Cancel
					};
					break;
				default:
					buttons = new string[]{
						ControlStrings.DialogOKButton
					};
					results = new DialogResult[]{
						DialogResult.OK
					};
					break;
			}

			// Выбор иконки
			switch (iconType) {
				case MessageBoxIcon.Asterisk:
					icon = new ShadowImage(ControlImages.dialoginfo);
					System.Media.SystemSounds.Asterisk.Play();
					break;
				case MessageBoxIcon.Error:
					icon = new ShadowImage(ControlImages.dialogerror);
					System.Media.SystemSounds.Hand.Play();
					break;
				case MessageBoxIcon.Exclamation:
					icon = new ShadowImage(ControlImages.dialogwarning);
					System.Media.SystemSounds.Exclamation.Play();
					break;
				case MessageBoxIcon.Question:
					icon = new ShadowImage(ControlImages.dialogquestion);
					System.Media.SystemSounds.Question.Play();
					break;
				default:
					icon = new ShadowImage(ControlImages.dialogok);
					System.Media.SystemSounds.Asterisk.Play();
					break;
			}

			// Открываем диалог
			MessageDialog d = new MessageDialog(caption, text, buttons, results, icon);
			return d.ShowDialog();
		}

		/// <summary>
		/// Вывод полностью собственного окна
		/// </summary>
		/// <param name="caption">Заголовок</param>
		/// <param name="text">Текст</param>
		/// <param name="buttons">Массив имён кнопок</param>
		/// <param name="results">Массив результатов кнопок</param>
		/// <param name="icon">Иконка</param>
		/// <returns>Состояние окна</returns>
		public static DialogResult ShowCustom(string caption, string text, string[] buttons, DialogResult[] results, ShadowImage icon) {
			MessageDialog d = new MessageDialog(caption, text, buttons, results, icon);
			return d.ShowDialog();
		}

		/// <summary>
		/// Форма закрыта
		/// </summary>
		/// <param name="e"></param>
		protected override void OnFormClosed(FormClosedEventArgs e) {
			if (!stateSet) {
				DialogResult = System.Windows.Forms.DialogResult.None;
			}
			Close();
		}

		/// <summary>
		/// Одна из кнопок нажата
		/// </summary>
		void ButtonClicked(object sender, EventArgs e) {
			stateSet = true;
			DialogResult = buttonResults[(int)(sender as NSButton).Tag];
			Close();
		}
		

	}
}
