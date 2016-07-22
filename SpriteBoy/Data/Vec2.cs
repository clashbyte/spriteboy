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
	public struct Vec2 : IEquatable<Vec2> {

		/// <summary>
		/// Значение оси X
		/// </summary>
		public float X;

		/// <summary>
		/// Значение оси Y
		/// </summary>
		public float Y;


		/// <summary>
		/// Конструктор с одинаковым значением
		/// </summary>
		/// <param name="value">Значение для каждой из компонент</param>
		public Vec2(float value) {
			X = value;
			Y = value;
		}

		/// <summary>
		/// Конструктор со значениями компонент
		/// </summary>
		/// <param name="x">Значение компоненты X</param>
		/// <param name="y">Значение компоненты Y</param>
		public Vec2(float x, float y) {
			X = x;
			Y = y;
		}

		/// <summary>
		/// Конструктор из исходного Vec2
		/// </summary>
		/// <param name="v">Vec2 для копирования данных</param>
		public Vec2(Vec2 v) {
			X = v.X;
			Y = v.Y;
		}

		/// <summary>
		/// Получение длины вектора
		/// </summary>
		/// <seealso cref="LengthSquared"/>
		public float Length {
			get {
				return (float)Math.Sqrt(X * X + Y * Y);
			}
		}

		/// <summary>
		/// Получение квадрата длины вектора (работает гораздо быстрее простого получения длины)
		/// </summary>
		public float LengthSquared {
			get {
				return X * X + Y * Y;
			}
		}

		/// <summary>
		/// Нормализация вектора
		/// </summary>
		public void Normalize() {
			float scale = 1.0f / Length;
			X *= scale;
			Y *= scale;
		}

		/// <summary>
		/// Скалярное произведение
		/// </summary>
		/// <param name="to">Второй вектор</param>
		/// <returns>Значение скалярного произведения</returns>
		public float Dot(Vec2 to) {
			return X * to.X + Y * to.Y;
		}

		/// <summary>
		/// Смешивание двух векторов
		/// </summary>
		/// <param name="to">Второй вектор</param>
		/// <param name="blend">Уровень смешивания (0-1)</param>
		/// <returns>Смешаный вектор</returns>
		public Vec2 Lerp(Vec2 to, float blend) {
			return new Vec2(
				blend * (to.X - X) + X,
				blend * (to.Y - Y) + Y
			);
		}

		/// <summary>
		/// Вектор с единицей в X-компоненте
		/// </summary>
		public static readonly Vec2 UnitX = new Vec2(1, 0);

		/// <summary>
		/// Вектор с единицей в Y-компоненте
		/// </summary>
		public static readonly Vec2 UnitY = new Vec2(0, 1);

		/// <summary>
		/// Нулевой вектор
		/// </summary>
		public static readonly Vec2 Zero = new Vec2(0);

		/// <summary>
		/// Единичный вектор
		/// </summary>
		public static readonly Vec2 One = new Vec2(1);

		/// <summary>
		/// Сложение двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>Сумма векторов</returns>
		public static Vec2 operator +(Vec2 left, Vec2 right) {
			left.X += right.X;
			left.Y += right.Y;
			return left;
		}

		/// <summary>
		/// Вычитание двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>Разность векторов</returns>
		public static Vec2 operator -(Vec2 left, Vec2 right) {
			left.X -= right.X;
			left.Y -= right.Y;
			return left;
		}

		/// <summary>
		/// Смена знака вектора
		/// </summary>
		/// <param name="v">Вектор</param>
		/// <returns>Инвертированный вектор</returns>
		public static Vec2 operator -(Vec2 v) {
			v.X = -v.X;
			v.Y = -v.Y;
			return v;
		}

		/// <summary>
		/// Умножение вектора на скаляр
		/// </summary>
		/// <param name="v">Вектор</param>
		/// <param name="scalar">Скаляр</param>
		/// <returns>Умноженный вектор</returns>
		public static Vec2 operator *(Vec2 v, float scalar) {
			v.X *= scalar;
			v.Y *= scalar;
			return v;
		}

		/// <summary>
		/// Умножение двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>Произведение векторов</returns>
		public static Vec2 operator *(Vec2 left, Vec2 right) {
			left.X *= right.X;
			left.Y *= right.Y;
			return left;
		}

		/// <summary>
		/// Деление двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>Частное векторов</returns>
		public static Vec2 operator /(Vec2 left, Vec2 right) {
			left.X /= right.X;
			left.Y /= right.Y;
			return left;
		}

		/// <summary>
		/// Деление вектора на скаляр
		/// </summary>
		/// <param name="v">Вектор</param>
		/// <param name="scalar">Скаляр</param>
		/// <returns>Умноженный вектор</returns>
		public static Vec2 operator /(Vec2 v, float scalar) {
			scalar = 1f / scalar;
			v.X *= scalar;
			v.Y *= scalar;
			return v;
		}

		/// <summary>
		/// Сравнение двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>True если векторы равны</returns>
		public static bool operator ==(Vec2 left, Vec2 right) {
			return left.X == right.X && left.Y == right.Y;
		}

		/// <summary>
		/// Сравнение двух векторов
		/// </summary>
		/// <param name="left">Первый вектор</param>
		/// <param name="right">Второй вектор</param>
		/// <returns>True если векторы не равны</returns>
		public static bool operator !=(Vec2 left, Vec2 right) {
			return left.X != right.X || left.Y != right.Y;
		}

		/// <summary>
		/// Приведение к строковому виду
		/// </summary>
		/// <returns>Строковое представление</returns>
		public override string ToString() {
			return String.Format("({0}, {1})", X, Y);
		}

		/// <summary>
		/// Получение уникального хэшкода вектора
		/// </summary>
		/// <returns>Код</returns>
		public override int GetHashCode() {
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		/// <summary>
		/// Сравнение вектора с объектом
		/// </summary>
		/// <param name="obj">Объект</param>
		/// <returns>True если вектора одинаковы</returns>
		public override bool Equals(object obj) {
			if (!(obj is Vec2)) {
				return false;
			}
			return Equals((Vec2)obj);
		}

		/// <summary>
		/// Сравнение с другим вектором
		/// </summary>
		/// <param name="v">Другой вектор</param>
		/// <returns>True если вектора равны</returns>
		public bool Equals(Vec2 v) {
			return v.X == X && v.Y == Y;
		}

	}
}
