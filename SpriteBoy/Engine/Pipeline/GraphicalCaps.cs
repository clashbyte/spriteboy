using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Pipeline {

	/// <summary>
	/// Графические возможности видеокарты
	/// </summary>
	public static class GraphicalCaps {

		/// <summary>
		/// Версия OpenGL
		/// </summary>
		public static Version GLVersion {
			get {
				CheckCaps();
				return version;
			}
		}

		/// <summary>
		/// Поддержка текстур, не кратных двум
		/// </summary>
		public static bool NonPowerOfTwoTextures {
			get {
				CheckCaps();
				return npotTextures;
			}
		}

		public static bool ShaderPipeline {
			get {
				CheckCaps();
				return shaderPipeline;
			}
		}

		// Скрытые переменные
		static bool capsLoaded;
		static Version version;
		static bool npotTextures;
		static bool shaderPipeline;

		// Проверка возможностей GL
		static void CheckCaps() {
			if (!capsLoaded) {
				//version = new Version(GL.GetString(StringName.Version));
				System.Diagnostics.Debug.WriteLine("[Engine] Running on GL " + GL.GetString(StringName.Version));


				// Использовать ли FFP или шейдеры
				shaderPipeline = false;

				// Поиск расширений
				string[] exts = GL.GetString(StringName.Extensions).Split(' ');

				// Текстуры некратные степени двойки
				npotTextures = exts.Contains("GL_ARB_texture_non_power_of_two");
				capsLoaded = true;
			}
		}

	}
}
