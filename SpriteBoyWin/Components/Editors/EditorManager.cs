using SpriteBoy.Data;
using SpriteBoy.Data.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Editors {

	/// <summary>
	/// Менеджер редакторов
	/// </summary>
	public class EditorManager {

		/// <summary>
		/// Регистрация всех редакторов
		/// </summary>
		public static void RegisterAll() {

			// Редактор скайбоксов
			Editor.Register(new string[]{
				".sbsky"
			}, typeof(SkyboxEditor));

			
			

		}


	}
}
