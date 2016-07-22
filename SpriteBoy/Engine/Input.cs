using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Input;
using System.Drawing;

namespace SpriteBoy.Engine {

	/// <summary>
	/// Ввод пользователя
	/// </summary>
	public static class Input {

		/// <summary>
		/// Скорость мыши
		/// </summary>
		public static PointF MouseSpeed { get; private set; }

		/// <summary>
		/// Предыдущее состояние мыши
		/// </summary>
		static MouseState oldState;

		/// <summary>
		/// Обновление мыши и клавиатуры
		/// </summary>
		public static void Update() {
			
			MouseState s = Mouse.GetState();
			if (s != oldState) {
				MouseSpeed = new PointF(
					s.X - oldState.X, s.Y - oldState.Y
				);

				oldState = s;
			}
			
		}

	}
}
