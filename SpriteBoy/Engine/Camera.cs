using OpenTK;
using SpriteBoy.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Data;

namespace SpriteBoy.Engine {
	
	/// <summary>
	/// Камера для отрисовки мира
	/// </summary>
	public class Camera : Entity {

		/// <summary>
		/// Матрица проекции
		/// </summary>
		Matrix4 proj = Matrix4.Identity;

		/// <summary>
		/// Матрица для скайбокса
		/// </summary>
		Matrix4 skymat = Matrix4.Identity;

		/// <summary>
		/// Размеры экрана
		/// </summary>
		Vector2 size = Vector2.One;

		/// <summary>
		/// Дальность камеры
		/// </summary>
		Vector2 range = new Vector2(0.03f, 300f);

		/// <summary>
		/// Режим проекции камеры
		/// </summary>
		CameraProjectionMode mode = CameraProjectionMode.Perspective;

		/// <summary>
		/// Отношение ширины экрана к высоте
		/// </summary>
		float aspect = 1f;

		/// <summary>
		/// Флаг, нужна ли матрица
		/// </summary>
		bool needMatrix = false;

		/// <summary>
		/// Размеры окна камеры
		/// </summary>
		public Vec2 Size {
			get {
				return new Vec2(size.X, size.Y);
			}
			set {
				size = new Vector2(value.X, value.Y);
				needMatrix = true;
			}
		}

		/// <summary>
		/// Дальность камеры
		/// </summary>
		public Vec2 Range {
			get {
				return new Vec2(range.X, range.Y);
			}
			set {
				range = new Vector2(value.X, value.Y);
				needMatrix = true;
			}
		}

		/// <summary>
		/// Режим проекции камеры
		/// </summary>
		public CameraProjectionMode ProjectionMode {
			get {
				return mode;
			}
			set {
				mode = value;
				needMatrix = true;
			}
		}

		/// <summary>
		/// Настройка камеры перед рендером кадра
		/// </summary>
		public void Setup() {
			if (needMatrix) {
				RebuildProjection();
				needMatrix = false;
			}
			
			// Загрузка данныз
			ShaderSystem.ProjectionMatrix = proj;
			GL.Viewport(0, 0, (int)size.X, (int)size.Y);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref proj);
		}

		/// <summary>
		/// Загрузка матрицы неба
		/// </summary>
		public void LoadSkyMatrix() {
			ShaderSystem.CameraMatrix = skymat;
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref skymat);
		}

		/// <summary>
		/// Загрузка обычной матрицы
		/// </summary>
		public void LoadMatrix() {
			ShaderSystem.CameraMatrix = invmat;
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref invmat);
		}

		/// <summary>
		/// Перестройка матрицы
		/// </summary>
		void RebuildProjection() {
			aspect = size.X / size.Y;
			if (mode == CameraProjectionMode.Perspective) {
				proj = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 3f, aspect, range.X, range.Y);
			}else{
				proj = Matrix4.CreatePerspectiveOffCenter(0, size.X, size.Y, 0, range.X, range.Y);
			}
		}

		/// <summary>
		/// Перестройка матрицы
		/// </summary>
		protected override void RebuildMatrix(){
			base.RebuildMatrix();
			skymat = invmat.ClearTranslation();
		}

		/// <summary>
		/// Режимы проекции камеры
		/// </summary>
		public enum CameraProjectionMode {

			/// <summary>
			/// Ортогональная проекция
			/// </summary>
			Ortho,

			/// <summary>
			/// Перспективная проекция
			/// </summary>
			Perspective

		}
	}
}
