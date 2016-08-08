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

		/// <summary>
		/// Использовать ли новый рендерер
		/// </summary>
		public static bool ShaderPipeline {
			get {
				CheckCaps();
				return shaderPipeline;
			}
		}

		// Скрытые переменные
		static bool capsLoaded;
		static Version version;
		static Version shadersVersion;
		static string gpu;
		static bool npotTextures;
		static bool shaderPipeline;

		// Проверка возможностей GL
		static void CheckCaps() {
			if (!capsLoaded) {
				
				// Определение версии GL
				version = StringToVersion(GL.GetString(StringName.Version));
				shadersVersion = StringToVersion(GL.GetString(StringName.ShadingLanguageVersion));
				gpu = GL.GetString(StringName.Renderer);
				System.Diagnostics.Debug.WriteLine("[Engine] Running on GL " + GL.GetString(StringName.Version));
				System.Diagnostics.Debug.WriteLine("[Engine] Shaders vertsion " + GL.GetString(StringName.ShadingLanguageVersion));

				// Использовать ли FFP или шейдеры
				shaderPipeline = false;
				if (version >= new Version(2, 0)) {
					if (shadersVersion >= new Version(1, 2)) {
						shaderPipeline = true;
					}
				}
				if (shaderPipeline) {
					System.Diagnostics.Debug.WriteLine("[Engine] Using modern renderer on GPU "+gpu);
				} else {
					System.Diagnostics.Debug.WriteLine("[Engine] Using legacy renderer on GPU " + gpu);
				}

				// Поиск расширений
				string[] exts = GL.GetString(StringName.Extensions).Split(' ');

				// Текстуры некратные степени двойки
				npotTextures = exts.Contains("GL_ARB_texture_non_power_of_two");
				capsLoaded = true;
			}
		}

		/// <summary>
		/// Перевод строки в класс Version
		/// </summary>
		/// <param name="txt">Строка с версией</param>
		/// <returns>Version</returns>
		static Version StringToVersion(string txt) {
			string verPart = txt.Split(' ')[0];
			string[] nums = verPart.Split('.');
			int major = int.Parse(nums[0]);
			int minor = int.Parse(nums[1]);
			return new Version(major, minor);
		}

		/// <summary>
		/// Переход к старому рендеру для слабых машин при ошибке в шейдерах
		/// </summary>
		internal static void FallbackToLegacy() {
			shaderPipeline = false;
			System.Diagnostics.Debug.WriteLine("[Engine] Requested fallback to legacy renderer");
		}
	}
}
