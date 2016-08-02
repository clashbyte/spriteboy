using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Компонент простого статичного меша
	/// </summary>
	public class StaticMeshComponent : MeshComponent {

		/// <summary>
		/// Статический меш
		/// </summary>
		public StaticMeshComponent() {
			Diffuse = Color.White;
		}

		
	}
}
