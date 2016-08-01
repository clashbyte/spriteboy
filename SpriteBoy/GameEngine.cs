using SpriteBoy.Engine;
using SpriteBoy.Engine.Pipeline;

namespace SpriteBoy {
	
	/// <summary>
	/// Основные функции и хуки движка
	/// </summary>
	public class GameEngine {

		/// <summary>
		/// Отладочный режим
		/// </summary>
		public static bool DebugMode = false;

		/// <summary>
		/// Обновление переменных движка
		/// </summary>
		public static void Update() {

			// Текстуры
			Texture.UpdateQueued();

			// Ввод
			Input.Update();

		}
	}
}
