using System.Drawing;

namespace SpriteBoy.Data.Shaders {
	
	/// <summary>
	/// Шейдер скайбокса
	/// </summary>
	internal class SkyboxShader : ShaderBase {

		/// <summary>
		/// Синглтон шейдера
		/// </summary>
		public static SkyboxShader Shader {
			get {
				if (sh == null) {
					sh = new SkyboxShader();
				}
				return sh;
			}
		}
		static SkyboxShader sh;
		
		/// <summary>
		/// Фрагментный шейдер
		/// </summary>
		protected override string FragmentProgram {
			get {
				return ShaderSources.SkyFragment;
			}
		}

		/// <summary>
		/// Вершинный шейдер
		/// </summary>
		protected override string VertexProgram {
			get {
				return ShaderSources.SkyVertex;
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
		/// Расположение буффера текстурных координат
		/// </summary>
		public int TexCoordBufferLocation {
			get {
				return texCoordAttrib.Handle;
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
		TextureUniform textureUniform;
		ColorUniform diffuseColor;
		BoolUniform alphaPass;
		MatrixUniform textureMatrix;
		FloatUniform lightMultiplier;
		VertexAttribute vertexAttrib;
		VertexAttribute normalAttrib;
		VertexAttribute texCoordAttrib;

		// Конструктор
		protected SkyboxShader() : base() {
			if (vertexAttrib == null) {
				vertexAttrib = new VertexAttribute("inPosition");
				attribs.Add(vertexAttrib);
			}
			if (texCoordAttrib == null) {
				texCoordAttrib = new VertexAttribute("inTexCoord");
				attribs.Add(texCoordAttrib);
			}
			if (textureUniform == null) {
				textureUniform = new TextureUniform("texture");
				uniforms.Add(textureUniform);
			}
			if (diffuseColor == null) {
				diffuseColor = new ColorUniform("diffuseColor");
				uniforms.Add(diffuseColor);
			}
			if (textureMatrix == null) {
				textureMatrix = new MatrixUniform("textureMatrix");
				uniforms.Add(textureMatrix);
			}
		}

		/// <summary>
		/// Установка шейдера
		/// </summary>
		public override void Bind() {
			textureMatrix.Matrix = ShaderSystem.TextureMatrix;
			base.Bind();
		}

	}
}
