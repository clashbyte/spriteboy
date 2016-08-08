using System.Drawing;

namespace SpriteBoy.Data.Shaders {

	/// <summary>
	/// Шейдер для куба
	/// </summary>
	internal class WireCubeShader : ShaderBase {

		/// <summary>
		/// Синглтон шейдера
		/// </summary>
		public static WireCubeShader Shader {
			get {
				if (sh == null) {
					sh = new WireCubeShader();
				}
				return sh;
			}
		}
		static WireCubeShader sh;

		/// <summary>
		/// Фрагментный шейдер
		/// </summary>
		protected override string FragmentProgram {
			get {
				return ShaderSources.WireCubeFragment;
			}
		}

		/// <summary>
		/// Вершинный шейдер
		/// </summary>
		protected override string VertexProgram {
			get {
				return ShaderSources.WireCubeVertex;
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
		/// Основной цвет
		/// </summary>
		public Color DiffuseColor {
			get {
				return diffuseColor.Color;
			}
			set {
				diffuseColor.Color = value;
			}
		}

		// Скрытые параметры
		static VertexAttribute vertexAttrib;
		static ColorUniform diffuseColor;

		/// <summary>
		/// Конструктор
		/// </summary>
		WireCubeShader() : base() {
			if (vertexAttrib==null) {
				vertexAttrib = new VertexAttribute("inPosition");
				attribs.Add(vertexAttrib);
			}
			if (diffuseColor==null) {
				diffuseColor = new ColorUniform("diffuseColor");
				uniforms.Add(diffuseColor);
			}
		}

	}
}
