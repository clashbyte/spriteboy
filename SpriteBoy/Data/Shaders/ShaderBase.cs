using OpenTK;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Engine.Pipeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Shaders {

	/// <summary>
	/// Базовый шейдер
	/// </summary>
	internal abstract class ShaderBase {

		/// <summary>
		/// Вершинная программа (фрагментный шейдер)
		/// </summary>
		protected virtual string VertexProgram {
			get {
				return "";
			}
		}

		/// <summary>
		/// Фрагментная программа (пиксельный шейдер)
		/// </summary>
		protected virtual string FragmentProgram {
			get {
				return "";
			}
		}

		/// <summary>
		/// Юниформы
		/// </summary>
		protected List<Uniform> uniforms;

		/// <summary>
		/// Аттрибуты
		/// </summary>
		protected List<VertexAttribute> attribs;

		/// <summary>
		/// Индекс GL-шейдера
		/// </summary>
		protected int GLProgram = -1;

		/// <summary>
		/// Матрицы объекта
		/// </summary>
		protected MatrixUniform projectionMatrix, cameraMatrix, entityMatrix;

		/// <summary>
		/// Конструктор программы
		/// </summary>
		protected ShaderBase() {
			if (uniforms==null) {
				uniforms = new List<Uniform>();
				projectionMatrix = new MatrixUniform("projectionMatrix");
				cameraMatrix = new MatrixUniform("cameraMatrix");
				entityMatrix = new MatrixUniform("entityMatrix");
				uniforms.AddRange(new MatrixUniform[]{
					cameraMatrix, entityMatrix, projectionMatrix
				});
			}
			if (attribs==null) {
				attribs = new List<VertexAttribute>();
			}
		}

		/// <summary>
		/// Компиляция шейдера
		/// </summary>
		void Compile() {
			
			// Инициализация шейдеров
			int frpr = GL.CreateShader(ShaderType.FragmentShader);
			int vrpr = GL.CreateShader(ShaderType.VertexShader);
			int compstatus = 0;

			// Отправка исходных данных шейдеров
			GL.ShaderSource(frpr, FragmentProgram);
			GL.ShaderSource(vrpr, VertexProgram);

			// Компиляция фрагментного шейдера
			GL.CompileShader(frpr);
			GL.GetShader(frpr, ShaderParameter.CompileStatus, out compstatus);
			if (compstatus == 0) {
				System.Diagnostics.Debug.WriteLine("[Shader] Fragment program compiling error:\n" + GL.GetShaderInfoLog(frpr));
				GLProgram = -2;
				return;
			}

			// Компиляция вершинного шейдера
			GL.CompileShader(vrpr);
			GL.GetShader(vrpr, ShaderParameter.CompileStatus, out compstatus);
			if (compstatus == 0) {
				System.Diagnostics.Debug.WriteLine("[Shader] Vertex program compiling error:\n" + GL.GetShaderInfoLog(vrpr));
				GLProgram = -2;
				return;
			}

			// Создание программы и присоединение шейдеров
			GLProgram = GL.CreateProgram();
			GL.AttachShader(GLProgram, frpr);
			GL.AttachShader(GLProgram, vrpr);

			// Линковка шейдерной программы
			int lnkstat = 0;
			GL.LinkProgram(GLProgram);
			GL.GetProgram(GLProgram, ProgramParameter.LinkStatus, out lnkstat);
			if (lnkstat == 0) {
				System.Diagnostics.Debug.WriteLine("[Shader] Shader program linking error:\n" + GL.GetProgramInfoLog(GLProgram));
				GLProgram = -2;
				return;
			}

			// Поиск юниформов
			GL.UseProgram(GLProgram);
			if (uniforms != null) {
				foreach (Uniform u in uniforms) {
					u.Seek(GLProgram);
				}
			}

			// Поиск аттрибутов
			if (attribs != null) {
				foreach (VertexAttribute v in attribs) {
					v.Seek(GLProgram);
				}
			}
			GL.UseProgram(0);
			
		}

		/// <summary>
		/// Установка шейдера
		/// </summary>
		public virtual void Bind() {
			bool allow = GLProgram > -2;
			if (allow && GLProgram == -1) {
				Compile();
				if (GLProgram == -2) allow = false;
			}

			if (allow) {
				projectionMatrix.Matrix = ShaderSystem.ProjectionMatrix;
				cameraMatrix.Matrix = ShaderSystem.CameraMatrix;
				entityMatrix.Matrix = ShaderSystem.EntityMatrix;
				GL.UseProgram(GLProgram);
				if (uniforms != null) {
					foreach (Uniform u in uniforms) {
						u.Bind(GLProgram);
					}
				}
				if (attribs != null) {
					foreach (VertexAttribute v in attribs) {
						if(v.Handle > -1) GL.EnableVertexAttribArray(v.Handle);
					}
				}
			} else {
				GraphicalCaps.FallbackToLegacy();
				GL.UseProgram(0);
			}
		}

		/// <summary>
		/// Отключение шейдера
		/// </summary>
		public void Unbind() {
			GL.UseProgram(0);
			if (attribs != null) {
				foreach (VertexAttribute v in attribs) {
					GL.DisableVertexAttribArray(v.Handle);
				}
			}
			if (GLProgram == -2) {
				GraphicalCaps.FallbackToLegacy();
			}
		}

		/// <summary>
		/// База для параметров шейдера
		/// </summary>
		protected internal abstract class Uniform {
			/// <summary>
			/// Имя юниформа
			/// </summary>
			public string Name;

			/// <summary>
			/// Ссылка на юниформ
			/// </summary>
			protected int Handle = -1;

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя юниформа</param>
			public Uniform(string name) {
				Name = name;
			}

			/// <summary>
			/// Поиск юниформа
			/// </summary>
			public void Seek(int program) {
				if (Handle == -1) {
					Handle = GL.GetUniformLocation(program, Name);
				}
			}

			/// <summary>
			/// Установка юниформа
			/// </summary>
			public abstract void Bind(int program);
		}

		/// <summary>
		/// Текстурный юниформ
		/// </summary>
		protected internal class TextureUniform : Uniform {
			/// <summary>
			/// Слой, с которого брать текстуру
			/// </summary>
			public int Layer;

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя юниформа</param>
			public TextureUniform(string name) : base(name) { }

			/// <summary>
			/// Установка шейдера
			/// </summary>
			public override void Bind(int program) {
				GL.Uniform1(Handle, Layer);
			}
		}

		/// <summary>
		/// Текстурный юниформ
		/// </summary>
		protected internal class BoolUniform : Uniform {
			/// <summary>
			/// Булево значение
			/// </summary>
			public bool Value;

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя юниформа</param>
			public BoolUniform(string name) : base(name) { }

			/// <summary>
			/// Установка шейдера
			/// </summary>
			public override void Bind(int program) {
				GL.Uniform1(Handle, Value ? 1 : 0);
			}
		}

		/// <summary>
		/// Цветовой юниформ
		/// </summary>
		protected internal class ColorUniform : Uniform {
			/// <summary>
			/// Цвет
			/// </summary>
			public Color Color;

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя юниформа</param>
			public ColorUniform(string name) : base(name) { }

			/// <summary>
			/// Установка цвета
			/// </summary>
			/// <param name="program">Программа</param>
			public override void Bind(int program) {
				GL.Uniform4(Handle, Color);
			}
		}

		/// <summary>
		/// Дробный юниформ
		/// </summary>
		protected internal class FloatUniform : Uniform {
			/// <summary>
			/// Число
			/// </summary>
			public float Value;

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя юниформа</param>
			public FloatUniform(string name) : base(name) { }

			/// <summary>
			/// Установка числа
			/// </summary>
			/// <param name="program">Программа</param>
			public override void Bind(int program) {
				GL.Uniform1(Handle, Value);
			}
		}

		/// <summary>
		/// Векторный юниформ
		/// </summary>
		protected internal class VectorUniform : Uniform {
			/// <summary>
			/// Вектор
			/// </summary>
			public Vector3 Vector;

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя юниформа</param>
			public VectorUniform(string name) : base(name) { }

			/// <summary>
			/// Установка цвета
			/// </summary>
			/// <param name="program">Программа</param>
			public override void Bind(int program) {
				GL.Uniform3(Handle, Vector);
			}
		}

		/// <summary>
		/// Матричный юниформ
		/// </summary>
		protected internal class MatrixUniform : Uniform {
			/// <summary>
			/// Матрица для юниформа
			/// </summary>
			public Matrix4 Matrix;

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя юниформа</param>
			public MatrixUniform(string name) : base(name) { }

			/// <summary>
			/// Установка матрицы
			/// </summary>
			public override void Bind(int program) {
				GL.UniformMatrix4(Handle, false, ref Matrix);
			}
		}

		/// <summary>
		/// Юниформ матричных массивов
		/// </summary>
		protected internal class MatrixArrayUniform : Uniform {
			/// <summary>
			/// Матрицы для юниформа
			/// </summary>
			public Matrix4[] Matrices {
				get {
					if (matrixArray!=null) {
						Matrix4[] ma = new Matrix4[matrixCount];
						for (int i = 0; i < matrixCount; i++) {
							int off = i * 16;
							Matrix4 m = new Matrix4();
							for (int column = 0; column < 4; column++) {
								for (int row = 0; row < 4; row++) {
									m[row, column] = matrixArray[off + column * 4 + row];
								}
							}
							ma[i] = m;
						}
						return ma;
					}
					return null;
				}
				set {
					if (value != null) {
						matrixCount = value.Length;
						matrixArray = new float[value.Length * 16];
						for (int i = 0; i < value.Length; i++) {
							Matrix4 m = value[i];
							int off = i * 16;
							for (int column = 0; column < 4; column++) {
								for (int row = 0; row < 4; row++) {
									matrixArray[off + column * 4 + row] = m[row, column];
								}
							}
						}
					} else {
						matrixCount = 0;
						matrixArray = null;
					}
				}
			}

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя юниформа</param>
			public MatrixArrayUniform(string name) : base(name) { }

			/// <summary>
			/// Скрытые объединенные матрицы
			/// </summary>
			float[] matrixArray;

			/// <summary>
			/// Количество матриц
			/// </summary>
			int matrixCount;

			/// <summary>
			/// Установка юниформа
			/// </summary>
			/// <param name="program"></param>
			public override void Bind(int program) {
				if (matrixArray!=null && matrixCount > 0) {
					GL.UniformMatrix4(Handle, matrixCount, false, matrixArray);
				}
			}
		}

		/// <summary>
		/// Вершинный атрибут
		/// </summary>
		protected internal class VertexAttribute {
			/// <summary>
			/// Имя юниформа
			/// </summary>
			public string Name;

			/// <summary>
			/// Ссылка на юниформ
			/// </summary>
			public int Handle {
				get;
				protected set;
			}

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя аттрибута</param>
			public VertexAttribute(string name) {
				Name = name;
				Handle = -1;
			}

			/// <summary>
			/// Поиск атрибута
			/// </summary>
			/// <param name="program">Индекс программы</param>
			public void Seek(int program) {
				if (Handle == -1) {
					Handle = GL.GetAttribLocation(program, Name);
				}
			}
		}
	}
}
