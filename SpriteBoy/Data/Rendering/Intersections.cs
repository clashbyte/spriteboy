using SpriteBoy.Data.Types;
using System;

namespace SpriteBoy.Data.Rendering {

	/// <summary>
	/// Библиотека функций для поиска пересечений
	/// </summary>
	internal static class Intersections {

		/// <summary>
		/// Пересекается ли луч со сферой
		/// </summary>
		/// <param name="rayPos">Позиция начала луча</param>
		/// <param name="rayDir">Направление луча</param>
		/// <param name="spherePos">Расположение сферы</param>
		/// <param name="sphereRad">Радиус сферы</param>
		/// <returns>True если есть пересечение</returns>
		public static bool RaySphere(Vec3 rayPos, Vec3 rayDir, Vec3 spherePos, float sphereRad) {
			Vec3 q = spherePos - rayPos;
			float c = q.Length;
			float v = q.Dot(rayDir);
			if ((sphereRad * sphereRad - (c*c - v*v))<0f) {
				return false;
			}
			return true;
		}

		/// <summary>
		/// Пересекается ли луч с треугольником
		/// </summary>
		/// <param name="rayPos">Позиция начала луча</param>
		/// <param name="rayDir">Направление луча</param>
		/// <param name="v0">Первая вершина</param>
		/// <param name="v1">Вторая вершина</param>
		/// <param name="v2">Третья вершина</param>
		/// <param name="twoSided">Учитывать обе стороны</param>
		/// <param name="hitPoint">Место пересечения</param>
		/// <param name="hitNormal">Нормаль пересечения</param>
		/// <returns>True если есть пересечение</returns>
		public static bool RayTriangle(Vec3 rayPos, Vec3 rayDir, Vec3 v0, Vec3 v1, Vec3 v2, bool twoSided, out Vec3 hitPoint, out Vec3 hitNormal) {
			Vec3 tv1 = v1 - v0;
			Vec3 tv2 = v2 - v0;
			Vec3 pv = rayDir.Cross(tv2);
			float det = tv1.Dot(pv);

			float pp = det;
			if (twoSided) pp = Math.Abs(det);
			if (pp < float.Epsilon) {
				hitPoint = Vec3.Zero;
				hitNormal = Vec3.Zero;
				return false;
			}

			float invDet = 1f / det;

			Vec3 tv = rayPos - v0;
			float u = tv.Dot(pv) * invDet;
			if (u < 0 || u > 1) {
				hitPoint = Vec3.Zero;
				hitNormal = Vec3.Zero;
				return false;
			}

			Vec3 qv = tv.Cross(tv1);
			float v = rayDir.Dot(qv) * invDet;
			if (v < 0 || u + v > 1) {
				hitPoint = Vec3.Zero;
				hitNormal = Vec3.Zero;
				return false;
			}

			float w = 1f - (u + v);
			hitPoint = v0 * u + v1 * v + v2 * w;
			hitNormal = (v1 - v0).Cross(v2 - v0);
			if (pp < 0) hitNormal *= -1f;
			hitNormal.Normalize();
			return true;
		}

	}
}
