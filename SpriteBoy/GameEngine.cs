using SpriteBoy.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy {
	
	
	public class GameEngine {

		/// <summary>
		/// Обновление переменных движка
		/// </summary>
		public static void Update() {

			// Текстуры
			Texture.UpdateQueued();

			// Ввод
			Input.Update();

		}
	}
}
