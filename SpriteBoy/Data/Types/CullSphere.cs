using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Types {
	
	/// <summary>
	/// Сфера отсечения
	/// </summary>
	public class CullSphere {

		/// <summary>
		/// Расположение сферы
		/// </summary>
		public Vec3 Position {
			get;
			set;
		}

		/// <summary>
		/// Радиус сферы
		/// </summary>
		public float Radius {
			get;
			set;
		}

	}
}
