using SpriteBoy.Data.Editing;
using SpriteBoy.Forms;
using SpriteBoy.Forms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy {

	/// <summary>
	/// Отладочные методы
	/// </summary>
	static class DebugStuff {

		public static void AppStarted() {


			// Открытие тестового проекта
			Project.Open(System.IO.Path.GetFullPath("..\\..\\..\\Project\\MyGame.sbproject"));

			// Открытие тестового файла
			if (Project.Opened) {
				//MainForm.OpenEditor(Project.GetEntry("sky/skytest — копия (3).sbsky"));
				//MainForm.OpenEditor(Project.GetEntry("sky/skytest — копия (2).sbsky"));
				MainForm.OpenEditor(Project.GetEntry("rail.md2"));
			}
			


		}

	}
}
