using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Data {

	/// <summary>
	/// Интерфейс обновляемого компонента
	/// </summary>
	interface IUpdatable {

		/// <summary>
		/// Обновление компонента
		/// </summary>
		void Update();
	}
}
