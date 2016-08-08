using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace SpriteBoy.Data {

	/// <summary>
	/// Система рендеринга шейдерами
	/// </summary>
	internal static class ShaderSystem {

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

		/// <summary>
		/// Проверка буффера на существование, и загрузка данных
		/// </summary>
		/// <param name="buffer">Индекс для проверки</param>
		public static void CheckVertexBuffer(ref int buffer, float[] data, BufferUsageHint hint = BufferUsageHint.StaticDraw) {
			if (!GL.IsBuffer(buffer)) {
				buffer = GL.GenBuffer();
				GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length*4), data, hint);
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			}
		}

		/// <summary>
		/// Проверка индексного буффера на существование
		/// </summary>
		/// <param name="buffer">Индекс для проверки</param>
		/// <param name="data">Данные для отправки, если буффер не существует</param>
		public static void CheckIndexBuffer(ref int buffer, ushort[] data, BufferUsageHint hint = BufferUsageHint.StaticDraw) {
			if (!GL.IsBuffer(buffer)) {
				buffer = GL.GenBuffer();
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffer);
				GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(data.Length * 2), data, hint);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
			}
		}

	}
}
