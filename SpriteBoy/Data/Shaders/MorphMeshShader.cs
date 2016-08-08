
namespace SpriteBoy.Data.Shaders {

	/// <summary>
	/// Шейдер для морфного меша
	/// </summary>
	internal class MorphMeshShader : MeshShader {

		/// <summary>
		/// Синглтон шейдера
		/// </summary>
		public static new MorphMeshShader Shader {
			get {
				if (sh == null) {
					sh = new MorphMeshShader();
				}
				return sh;
			}
		}
		static MorphMeshShader sh;
		
		/// <summary>
		/// Вершинный шейдер
		/// </summary>
		protected override string VertexProgram {
			get {
				return ShaderSources.MorphMeshVertex;
			}
		}

		/// <summary>
		/// Расположение первого буффера вершин
		/// </summary>
		public int FirstVertexBufferLocation {
			get {
				return firstVertexAttrib.Handle;
			}
		}

		/// <summary>
		/// Расположение второго буффера вершин
		/// </summary>
		public int SecondVertexBufferLocation {
			get {
				return secondVertexAttrib.Handle;
			}
		}

		/// <summary>
		/// Расположение первого буффера вершин
		/// </summary>
		public int FirstNormalBufferLocation {
			get {
				return firstNormalAttrib.Handle;
			}
		}

		/// <summary>
		/// Расположение второго буффера вершин
		/// </summary>
		public int SecondNormalBufferLocation {
			get {
				return secondNormalAttrib.Handle;
			}
		}

		/// <summary>
		/// Значение перехода между кадрами
		/// </summary>
		public float FrameDelta {
			get {
				return deltaUniform.Value;
			}
			set {
				deltaUniform.Value = value;
			}
		}

		// Скрытые буфферы и переменные
		VertexAttribute firstVertexAttrib, secondVertexAttrib;
		VertexAttribute firstNormalAttrib, secondNormalAttrib;
		FloatUniform deltaUniform;

		/// <summary>
		/// Конструктор
		/// </summary>
		public MorphMeshShader() : base() {
			if (firstVertexAttrib == null) {
				firstVertexAttrib = new VertexAttribute("inFirstPosition");
				attribs.Add(firstVertexAttrib);
			}
			if (secondVertexAttrib == null) {
				secondVertexAttrib = new VertexAttribute("inSecondPosition");
				attribs.Add(secondVertexAttrib);
			}
			if (firstNormalAttrib == null) {
				firstNormalAttrib = new VertexAttribute("inFirstNormal");
				attribs.Add(firstNormalAttrib);
			}
			if (secondNormalAttrib == null) {
				secondNormalAttrib = new VertexAttribute("inSecondNormal");
				attribs.Add(secondNormalAttrib);
			}
			if (deltaUniform == null) {
				deltaUniform = new FloatUniform("delta");
				uniforms.Add(deltaUniform);
			}
		}
	}
}
