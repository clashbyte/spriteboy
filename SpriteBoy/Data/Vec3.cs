using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SpriteBoy.Data {
	
	/// <summary>
	/// Трёхмерный вектор
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vec3 : IEquatable<Vec3> {

		/// <summary>
		/// Значение оси X
		/// </summary>
		public float X;

		/// <summary>
		/// Значение оси Y
		/// </summary>
		public float Y;

		/// <summary>
		/// Значение оси Z
		/// </summary>
		public float Z;


		/// <summary>
        /// Конструктор с одинаковым значением
        /// </summary>
        /// <param name="value">Значение для каждой из компонент</param>
        public Vec3(float value) {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>
        /// Конструктор со значениями компонент
        /// </summary>
        /// <param name="x">Значение компоненты X</param>
		/// <param name="y">Значение компоненты Y</param>
		/// <param name="z">Значение компоненты Z</param>
        public Vec3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

		/*
        /// <summary>
        /// Constructs a new Vector3 from the given Vector2.
        /// </summary>
        /// <param name="v">The Vector2 to copy components from.</param>
        public Vec3(Vector2 v) {
            X = v.X;
            Y = v.Y;
            Z = 0.0f;
        }
		 */

        /// <summary>
        /// Конструктор из исходного Vec3
        /// </summary>
        /// <param name="v">Vec3 для копирования данных</param>
        public Vec3(Vec3 v) {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

		/// <summary>
		/// Получение длины вектора
		/// </summary>
		/// <seealso cref="LengthSquared"/>
		public float Length {
			get {
				return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
			}
		}

		/// <summary>
		/// Получение квадрата длины вектора (работает гораздо быстрее простого получения длины)
		/// </summary>
		public float LengthSquared {
			get {
				return X * X + Y * Y + Z * Z;
			}
		}

		/// <summary>
		/// Нормализация вектора
		/// </summary>
		public void Normalize() {
			float scale = 1.0f / Length;
			X *= scale;
			Y *= scale;
			Z *= scale;
		}

		/// <summary>
		/// Скалярное произведение
		/// </summary>
		/// <param name="to">Второй вектор</param>
		/// <returns>Значение скалярного произведения</returns>
		public float Dot(Vec3 to) {
			return X * to.X + Y * to.Y + Z * to.Z;
		}

		/// <summary>
		/// Перекрестное произведение
		/// </summary>
		/// <param name="to">Второй вектор</param>
		/// <returns>Значение перекрестного произведения</returns>
		public Vec3 Cross(Vec3 to) {
			return new Vec3(Y * to.Z - Z * to.Y,
				Z * to.X - X * to.Z,
				X * to.Y - Y * to.X);
		}

		/// <summary>
		/// Смешивание двух векторов
		/// </summary>
		/// <param name="to">Второй вектор</param>
		/// <param name="blend">Уровень смешивания (0-1)</param>
		/// <returns>Смешаный вектор</returns>
		public Vec3 Lerp(Vec3 to, float blend) {
			return new Vec3(
				blend * (to.X - X) + X,
				blend * (to.Y - Y) + Y,
				blend * (to.Z - Z) + Z
			);
		}

		/// <summary>
		/// Вектор с единицей в X-компоненте
		/// </summary>
		public static readonly Vec3 UnitX = new Vec3(1, 0, 0);

		/// <summary>
		/// Вектор с единицей в Y-компоненте
		/// </summary>
		public static readonly Vec3 UnitY = new Vec3(0, 1, 0);

		/// <summary>
		/// Вектор с единицей в Z-компоненте
		/// </summary>
		public static readonly Vec3 UnitZ = new Vec3(0, 0, 1);

		/// <summary>
		/// Нулевой вектор
		/// </summary>
		public static readonly Vec3 Zero = new Vec3(0);

		/// <summary>
		/// Единичный вектор
		/// </summary>
		public static readonly Vec3 One = new Vec3(1);

		/// <summary>
		/// Сложение двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>Сумма векторов</returns>
		public static Vec3 operator +(Vec3 left, Vec3 right) {
			left.X += right.X;
			left.Y += right.Y;
			left.Z += right.Z;
			return left;
		}

		/// <summary>
		/// Вычитание двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>Разность векторов</returns>
		public static Vec3 operator -(Vec3 left, Vec3 right) {
			left.X -= right.X;
			left.Y -= right.Y;
			left.Z -= right.Z;
			return left;
		}

		/// <summary>
		/// Смена знака вектора
		/// </summary>
		/// <param name="v">Вектор</param>
		/// <returns>Инвертированный вектор</returns>
		public static Vec3 operator -(Vec3 v) {
			v.X = -v.X;
			v.Y = -v.Y;
			v.Z = -v.Z;
			return v;
		}

		/// <summary>
		/// Умножение вектора на скаляр
		/// </summary>
		/// <param name="v">Вектор</param>
		/// <param name="scalar">Скаляр</param>
		/// <returns>Умноженный вектор</returns>
		public static Vec3 operator *(Vec3 v, float scalar) {
			v.X *= scalar;
			v.Y *= scalar;
			v.Z *= scalar;
			return v;
		}

		/// <summary>
		/// Умножение двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>Произведение векторов</returns>
		public static Vec3 operator *(Vec3 left, Vec3 right) {
			left.X *= right.X;
			left.Y *= right.Y;
			left.Z *= right.Z;
			return left;
		}

		/// <summary>
		/// Деление двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>Частное векторов</returns>
		public static Vec3 operator /(Vec3 left, Vec3 right) {
			left.X /= right.X;
			left.Y /= right.Y;
			left.Z /= right.Z;
			return left;
		}

		/// <summary>
		/// Деление вектора на скаляр
		/// </summary>
		/// <param name="v">Вектор</param>
		/// <param name="scalar">Скаляр</param>
		/// <returns>Умноженный вектор</returns>
		public static Vec3 operator /(Vec3 v, float scalar) {
			scalar = 1f / scalar;
			v.X *= scalar;
			v.Y *= scalar;
			v.Z *= scalar;
			return v;
		}

		/// <summary>
		/// Сравнение двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>True если векторы равны</returns>
		public static bool operator ==(Vec3 left, Vec3 right) {
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		/// <summary>
		/// Сравнение двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>True если векторы не равны</returns>
		public static bool operator !=(Vec3 left, Vec3 right) {
			return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
		}

		/// <summary>
		/// Приведение к строковому виду
		/// </summary>
		/// <returns>Строковое представление</returns>
		public override string ToString() {
			return String.Format("({0}, {1}, {2})", X, Y, Z);
		}

		/// <summary>
		/// Получение уникального хэшкода вектора
		/// </summary>
		/// <returns>Код</returns>
		public override int GetHashCode() {
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		/// <summary>
		/// Сравнение вектора с объектом
		/// </summary>
		/// <param name="obj">Объект</param>
		/// <returns>True если вектора одинаковы</returns>
		public override bool Equals(object obj) {
			if (!(obj is Vec3)) {
				return false;
			}
			return Equals((Vec3)obj);
		}

		/// <summary>
		/// Сравнение с другим вектором
		/// </summary>
		/// <param name="v">Другой вектор</param>
		/// <returns>True если вектора равны</returns>
		public bool Equals(Vec3 v) {
			return v.X == X && v.Y == Y && v.Z == Z;
		}

	}
}
