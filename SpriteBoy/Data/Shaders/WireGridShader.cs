using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Shaders {

	/// <summary>
	/// Шейдер сетки
	/// </summary>
	internal class WireGridShader : ShaderBase {

		/// <summary>
		/// Синглтон шейдера
		/// </summary>
		public static WireGridShader Shader {
			get {
				if (sh == null) {
					sh = new WireGridShader();
				}
				return sh;
			}
		}
		static WireGridShader sh;

		/// <summary>
		/// Фрагментный шейдер
		/// </summary>
		protected override string FragmentProgram {
			get {
				return ShaderSources.WireGridFragment;
			}
		}

		/// <summary>
		/// Вершинный шейдер
		/// </summary>
		protected override string VertexProgram {
			get {
				return ShaderSources.WireGridVertex;
			}
		}

		/// <summary>
		/// Расположение буффера вершин
		/// </summary>
		public int VertexBufferLocation {
			get {
				return vertexAttrib.Handle;
			}
		}

		/// <summary>
		/// Расположение буффера цвета
		/// </summary>
		public int ColorBufferLocation {
			get {
				return colorAttrib.Handle;
			}
		}

		// Скрытые параметры
		static VertexAttribute vertexAttrib;
		static VertexAttribute colorAttrib;

		// Конструктор
		WireGridShader() : base() {
			if (attribs == null) {
				attribs = new List<VertexAttribute>();
			}
			if (vertexAttrib == null) {
				vertexAttrib = new VertexAttribute("inPosition");
				attribs.Add(vertexAttrib);
			}
			if (colorAttrib == null) {
				colorAttrib = new VertexAttribute("inColor");
				attribs.Add(colorAttrib);
			}
		}

	}
}
