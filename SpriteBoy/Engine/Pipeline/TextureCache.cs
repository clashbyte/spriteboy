using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Pipeline {

	/// <summary>
	/// Кеш текстур
	/// </summary>
	public static class TextureCache {

		/// <summary>
		/// Загруженные текстуры
		/// </summary>
		static ConcurrentDictionary<string, CacheEntry> textures = new ConcurrentDictionary<string, CacheEntry>();

		/// <summary>
		/// Текстуры на загрузку
		/// </summary>
		static ConcurrentQueue<CacheEntry> loadQueue = new ConcurrentQueue<CacheEntry>();

		/// <summary>
		/// Текстуры на отправку
		/// </summary>
		static ConcurrentQueue<CacheEntry> sendQueue = new ConcurrentQueue<CacheEntry>();

		/// <summary>
		/// Обновление кеша текстур
		/// </summary>
		internal static void Update() {

		}


		/// <summary>
		/// Одна запись в списке текстур
		/// </summary>
		internal class CacheEntry {

			/// <summary>
			/// Количество использований
			/// </summary>
			public int UseCount {
				get;
				set;
			}

			/// <summary>
			/// GL-текстура
			/// </summary>
			public int GLTex {
				get;
				set;
			}

			/// <summary>
			/// Скан-данные текстуры
			/// </summary>
			public byte[] Data {
				get;
				set;
			}

			/// <summary>
			/// Состояние текстуры
			/// </summary>
			public EntryState State {
				get;
				set;
			}
			
		}

		/// <summary>
		/// Состояние текстуры
		/// </summary>
		internal enum EntryState {
			/// <summary>
			/// Текстура ожидает чтение с диска
			/// </summary>
			Waiting = 0,
			/// <summary>
			/// Текстура читается с диска
			/// </summary>
			Reading = 1,
			/// <summary>
			/// Текстура прочитана, но не отправлена в видеопамять
			/// </summary>
			NotSent = 2,
			/// <summary>
			/// Текстура отправляется на видеокарту
			/// </summary>
			Sending = 3,
			/// <summary>
			/// Текстура отправлена и готова к отрисовке
			/// </summary>
			Complete = 4
		}
	}
}
