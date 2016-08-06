using OpenTK;
using SpriteBoy.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Data;
using SpriteBoy.Data.Rendering;
using System.Drawing.Drawing2D;
using SpriteBoy.Engine.Pipeline;

namespace SpriteBoy.Engine.World {
	
	/// <summary>
	/// Камера для отрисовки мира
	/// </summary>
	public class Camera : Entity {

		/// <summary>
		/// Размер ортогональной проекции
		/// </summary>
		public const float ORTHO_SIZE = 10f;

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
		ProjectionMode mode = ProjectionMode.Perspective;

		/// <summary>
		/// Отношение ширины экрана к высоте
		/// </summary>
		float aspect = 1f;

		/// <summary>
		/// Увеличение камеры
		/// </summary>
		float zoom = 1f;

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
		/// Увеличение камеры
		/// </summary>
		public float Zoom {
			get {
				return zoom;
			}
			set {
				zoom = value;
				if (zoom<0.00001f) {
					zoom = 0.00001f;
				}
				needMatrix = true;
			}
		}

		/// <summary>
		/// Режим проекции камеры
		/// </summary>
		public ProjectionMode Projection {
			get {
				return mode;
			}
			set {
				mode = value;
				needMatrix = true;
			}
		}

		/// <summary>
		/// Перевод точки в экранные координаты
		/// </summary>
		/// <param name="point">Точка в глобальных координатах</param>
		/// <returns></returns>
		public Vec2 PointToScreen(Vec3 point) {
			if (needMatrix) {
				RebuildProjection();
			}
			Vector4 tv = Vector4.Transform(new Vector4(point.X, point.Y, -point.Z, 1f), invmat * proj);
			if (tv.W < -float.Epsilon || tv.W > float.Epsilon) {
				tv /= tv.W;
			}
			return new Vec2(
				(tv.X + 1f) * 0.5f * size.X,
				(-tv.Y + 1f) * 0.5f * size.Y
			);
		}

		/// <summary>
		/// Перевод экранных координат в точку
		/// </summary>
		/// <param name="sx">X-координата</param>
		/// <param name="sy">Y-координата</param>
		/// <returns>Точка в мировых координатах</returns>
		public Vec3 ScreenToPoint(float sx, float sy) {
			return ScreenToPoint(new Vec3(sx, sy, 0));
		}

		/// <summary>
		/// Перевод точки из экранных координат в мировые
		/// </summary>
		/// <param name="p">Точка для перевода</param>
		/// <returns>Точка в мировых координатах</returns>
		internal Vec3 ScreenToPoint(Vec3 p) {
			if (needMatrix) {
				RebuildProjection();
			}
			Vector4 vec;
			vec.X = 2.0f * p.X / (float)size.X - 1;
			vec.Y = -(2.0f * p.Y / (float)size.Y - 1);
			vec.Z = p.Z;
			vec.W = 1.0f;
			vec = Vector4.Transform(vec, (invmat * proj).Inverted());
			if (vec.W > float.Epsilon || vec.W < float.Epsilon) {
				vec.X /= vec.W;
				vec.Y /= vec.W;
				vec.Z /= vec.W;
			}
			return new Vec3(vec.X, vec.Y, -vec.Z);
		}

		/// <summary>
		/// Настройка камеры перед рендером кадра
		/// </summary>
		internal void Setup() {
			if (needMatrix) {
				RebuildProjection();
			}
			
			// Загрузка данных
			GL.Viewport(0, 0, (int)size.X, (int)size.Y);
			if (GraphicalCaps.ShaderPipeline) {
				ShaderSystem.ProjectionMatrix = proj;
			} else {
				GL.MatrixMode(MatrixMode.Projection);
				GL.LoadMatrix(ref proj);
			}
		}

		/// <summary>
		/// Загрузка матрицы неба
		/// </summary>
		internal void LoadSkyMatrix() {
			if (GraphicalCaps.ShaderPipeline) {
				ShaderSystem.CameraMatrix = skymat;
			} else {
				GL.MatrixMode(MatrixMode.Modelview);
				GL.LoadMatrix(ref skymat);
			}
		}

		/// <summary>
		/// Загрузка обычной матрицы
		/// </summary>
		internal void LoadMatrix() {
			if (GraphicalCaps.ShaderPipeline) {
				ShaderSystem.CameraMatrix = invmat;
			} else {
				GL.MatrixMode(MatrixMode.Modelview);
				GL.LoadMatrix(ref invmat);
			}

			// Настройка фрустума
			Frustum.Setup(proj, invmat);
		}

		/// <summary>
		/// Перестройка матрицы
		/// </summary>
		void RebuildProjection() {
			aspect = size.X / size.Y;
			switch (mode) {
				case ProjectionMode.Ortho:
					proj = Matrix4.CreateOrthographic(aspect * ORTHO_SIZE * (1f / zoom), ORTHO_SIZE * (1f / zoom), range.X, range.Y);
					break;
				case ProjectionMode.CanvasOrtho:
					proj = Matrix4.CreateOrthographicOffCenter(0, size.X, size.Y, 0, range.X, range.Y);
					break;
				case ProjectionMode.Perspective:
					proj = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 3f * (1f / zoom), aspect, range.X, range.Y);
					break;
			}
			needMatrix = false;
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
		public enum ProjectionMode {

			/// <summary>
			/// Ортогональная проекция
			/// </summary>
			Ortho,

			/// <summary>
			/// Ортогональная проекция с попиксельной точностью
			/// </summary>
			CanvasOrtho,

			/// <summary>
			/// Перспективная проекция
			/// </summary>
			Perspective

		}
	}
}
