using SpriteBoy.Data.Editing;
using SpriteBoy.Forms.Common;
using SpriteBoy.Forms.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Forms {

	/// <summary>
	/// Менеджер сообщений от основного окна
	/// </summary>
	public static class CallbackManager {

		public static void RegisterAll() {

			MainForm.WindowLoad += MainForm_WindowLoad;
			MainForm.FrameTick += MainForm_FrameTick;
		}

		static void MainForm_FrameTick(object sender, EventArgs e) {
			GameEngine.Update();
		}

		static void MainForm_WindowLoad(object sender, EventArgs e) {
			DebugStuff.AppStarted();
			
	
			// Скрываем форму загрузки
			SplashForm.Dismiss();
		}


	}
}
