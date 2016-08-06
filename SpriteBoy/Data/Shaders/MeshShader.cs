using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Shaders {

	/// <summary>
	/// Базовый шейдер для меша
	/// </summary>
	internal class MeshShader : ShaderBase {

		/// <summary>
		/// Синглтон шейдера
		/// </summary>
		public static MeshShader Shader {
			get {
				if (sh == null) {
					sh = new MeshShader();
				}
				return sh;
			}
		}
		static MeshShader sh;
		
		/// <summary>
		/// Фрагментный шейдер
		/// </summary>
		protected override string FragmentProgram {
			get {
				return ShaderSources.MeshFragment;
			}
		}

		/// <summary>
		/// Вершинный шейдер
		/// </summary>
		protected override string VertexProgram {
			get {
				return ShaderSources.MeshVertex;
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
		/// Расположение буффера нормалей
		/// </summary>
		public int NormalBufferLocation {
			get {
				return normalAttrib.Handle;
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

		/// <summary>
		/// Основной цвет
		/// </summary>
		public float LightMultiplier {
			get {
				return lightMultiplier.Value;
			}
			set {
				lightMultiplier.Value = value;
			}
		}

		/// <summary>
		/// Основной цвет
		/// </summary>
		public bool AlphaTestPass {
			get {
				return alphaPass.Value;
			}
			set {
				alphaPass.Value = value;
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
		protected MeshShader() : base() {
			if (vertexAttrib == null) {
				vertexAttrib = new VertexAttribute("inPosition");
				attribs.Add(vertexAttrib);
			}
			if (normalAttrib == null) {
				normalAttrib = new VertexAttribute("inNormal");
				attribs.Add(normalAttrib);
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
			if (alphaPass == null) {
				alphaPass = new BoolUniform("discardPass");
				uniforms.Add(alphaPass);
			}
			if (lightMultiplier == null) {
				lightMultiplier = new FloatUniform("lightMult");
				uniforms.Add(lightMultiplier);
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
			alphaPass.Value = ShaderSystem.IsAlphaTest;
			base.Bind();
		}
	}
}
