using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace SpriteBoy.Data {

	/// <summary>
	/// Система рендеринга шейдерами
	/// </summary>
	static class ShaderSystem {

		/// <summary>
		/// Текущая матрица проекции кадра
		/// </summary>
		public static Matrix4 ProjectionMatrix;

		/// <summary>
		/// Матрица камеры
		/// </summary>
		public static Matrix4 CameraMatrix;

		/// <summary>
		/// Матрица объекта
		/// </summary>
		public static Matrix4 EntityMatrix;

		/// <summary>
		/// Текстурная матрица
		/// </summary>
		public static Matrix4 TextureMatrix;

		/// <summary>
		/// Включен ли альфатест на данном проходе
		/// </summary>
		public static bool IsAlphaTest;

	}
}
