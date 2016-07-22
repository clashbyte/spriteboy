using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SpriteBoy.Forms.Specialized {

	/// <summary>
	/// Форма загрузки редактора
	/// </summary>
	public partial class SplashForm : Form {

		/// <summary>
		/// Текст состояния
		/// </summary>
		public static string Label {
			get {
				return label;
			}
			set {
				label = value;
			}
		}

		static string label;

		/// <summary>
		/// Флаг, что форму необходимо закрыть
		/// </summary>
		static bool closeFlag;

		/// <summary>
		/// Поток обработчика сообщений от формы
		/// </summary>
		static Thread pumpThread;

		/// <summary>
		/// Открытая форма
		/// </summary>
		static SplashForm form;

		/// <summary>
		/// Конструктор формы
		/// </summary>
		SplashForm() {
			InitializeComponent();
			form = this;
			checkTimer.Start();
			stateLabel.Text = Label;
		}

		/// <summary>
		/// Открытие формы
		/// </summary>
		public static void Open() {
			closeFlag = false;
			pumpThread = new Thread(() => {
				Application.Run(new SplashForm());
			});
			pumpThread.Priority = ThreadPriority.AboveNormal;
			pumpThread.IsBackground = false;
			pumpThread.Start();
		}

		/// <summary>
		/// Закрытие формы
		/// </summary>
		public static void Dismiss() {
			closeFlag = true;
		}

		/// <summary>
		/// Шаг таймера проверки состояния
		/// </summary>
		private void checkTimer_Tick(object sender, EventArgs e) {
			if (label!=stateLabel.Text) {
				stateLabel.Text = label;
				stateLabel.Invalidate();
			}
			if (closeFlag) {
				form.Close();
				checkTimer.Stop();
			}
		}

	}
}
