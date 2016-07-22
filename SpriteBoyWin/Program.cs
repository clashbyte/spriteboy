using SpriteBoy.Data.Editing;
using SpriteBoy.Data.Editing.Graphics;
using SpriteBoy.Files;
using SpriteBoy.Forms.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpriteBoy {

	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			// Вывод сплэша загрузки
			SplashForm.Label = SharedStrings.AppStartupState;
			SplashForm.Open();


			// Регистрация обработчиков иконок
			SplashForm.Label = SharedStrings.AppPreviewRegisterState;
			Preview.Register();

			// Регистрация редакторов
			SplashForm.Label = SharedStrings.AppEditorRegisterState;
			Editor.Register();

			// Установка драйвера файловой системы
			FileSystem.Driver = new ProjectFileDriver();

			// Регистрация всех мыслимых и немыслимых хвостов у формы
			Forms.CallbackManager.RegisterAll();

			// Запуск формы
			SplashForm.Label = SharedStrings.AppCreatingUIState;
			Application.Run(new Forms.Common.MainForm());
			 

			//Application.Run(new Forms.TestForm());
		}
	}
}
