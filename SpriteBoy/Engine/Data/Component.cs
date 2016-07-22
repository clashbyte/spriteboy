using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Data {

	/// <summary>
	/// Некоторый компонент от энтити - может быть чем угодно
	/// </summary>
	public abstract class Component {

		/// <summary>
		/// Включен ли компонент
		/// </summary>
		public bool Enabled;

		/// <summary>
		/// Конструктор компонента
		/// </summary>
		public Component() {
			Enabled = true;
		}

	}
}
