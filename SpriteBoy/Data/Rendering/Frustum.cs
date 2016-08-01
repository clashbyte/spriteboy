using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using SpriteBoy.Data.Types;
using OpenTK.Graphics.OpenGL;

namespace SpriteBoy.Data.Rendering {

	/// <summary>
	/// Усеченная пирамида обзора камеры
	/// </summary>
	internal static class Frustum {

		/// <summary>
		/// Данные сторон
		/// </summary>
		static Vector4[] frustum;

		/// <summary>
		/// Установка фрустума
		/// </summary>
		/// <param name="projMat">Матрица проекции</param>
		/// <param name="modlMat">Модельная матрица</param>
		public static void Setup(Matrix4 projMat, Matrix4 modlMat) {

			// Матрица отсечения
			Matrix4 c = modlMat * projMat;
			if (frustum == null) {
				frustum = new Vector4[6];
			}
			c.Transpose();

			// Правая поверхность
			frustum[0] = new Vector4(
				c.Column0.W - c.Column0.X,
				c.Column1.W - c.Column1.X,
				c.Column2.W - c.Column2.X,
				c.Column3.W - c.Column3.X
			);

			// Левая поверхность
			frustum[1] = new Vector4(
				c.Column0.W + c.Column0.X,
				c.Column1.W + c.Column1.X,
				c.Column2.W + c.Column2.X,
				c.Column3.W + c.Column3.X
			);

			// Нижняя поверхность
			frustum[2] = new Vector4(
				c.Column0.W - c.Column0.Y,
				c.Column1.W - c.Column1.Y,
				c.Column2.W - c.Column2.Y,
				c.Column3.W - c.Column3.Y
			);

			// Верхняя поверхность
			frustum[3] = new Vector4(
				c.Column0.W + c.Column0.Y,
				c.Column1.W + c.Column1.Y,
				c.Column2.W + c.Column2.Y,
				c.Column3.W + c.Column3.Y
			);

			// Дальняя поверхность
			frustum[4] = new Vector4(
				c.Column0.W - c.Column0.Z,
				c.Column1.W - c.Column1.Z,
				c.Column2.W - c.Column2.Z,
				c.Column3.W - c.Column3.Z
			);

			// Ближняя поверхность
			frustum[5] = new Vector4(
				c.Column0.W + c.Column0.Z,
				c.Column1.W + c.Column1.Z,
				c.Column2.W + c.Column2.Z,
				c.Column3.W + c.Column3.Z
			);

			// Нормализация
			for (int i = 0; i < 6; i++) {
				frustum[i] /= frustum[i].Xyz.Length;
			}
		}

		/// <summary>
		/// Видна ли сфера 
		/// </summary>
		/// <param name="pos">Позиция</param>
		/// <param name="radius">Радиус</param>
		public static bool Contains(Vec3 pos, float radius) {
			for (int p = 0; p < 6; p++) {
				if ((frustum[p].X * pos.X + frustum[p].Y * pos.Y + frustum[p].Z * -pos.Z + frustum[p].W) <= -radius) {
					return false;
				}
			}
			return true;
		}
	}
}
