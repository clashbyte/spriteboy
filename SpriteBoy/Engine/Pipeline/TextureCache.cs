using OpenTK;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenTK.Graphics.OpenGL;

namespace SpriteBoy.Engine.Pipeline {

	/// <summary>
	/// Кеш текстур
	/// </summary>
	public static class TextureCache {

		/// <summary>
		/// Количество потоков для чтения с диска
		/// </summary>
		const int LOADING_THREADS = 2;

		/// <summary>
		/// Количество отправляемых текстур за кадр
		/// </summary>
		const int SENDS_PER_FRAME = 15;

		/// <summary>
		/// Время жизни потерянной текстуры
		/// </summary>
		const int LOST_TEXTURE_LIFETIME = 3000;

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
		/// Потоки для чтения с диска
		/// </summary>
		static Thread[] loadingThreads;

		/// <summary>
		/// Обновление кеша текстур
		/// </summary>
		internal static void Update() {

			// Отправка текстур
			int sendQuota = SENDS_PER_FRAME;

			// Проверка текстур 
			List<string> namesToRemove = new List<string>();
			foreach (KeyValuePair<string, CacheEntry> e in textures) {
				if (e.Value.UseCount == 0) {
					if ((DateTime.Now - e.Value.LastUseLostTime).Milliseconds > LOST_TEXTURE_LIFETIME) {
						e.Value.Release();
						namesToRemove.Add(e.Key);
					}
				}
			}
			
			// Удаление текстур
			foreach (string nm in namesToRemove) {
				CacheEntry ce;
				textures.TryRemove(nm, out ce);
			}
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
			/// Время освобождение
			/// </summary>
			public DateTime LastUseLostTime {
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

			/// <summary>
			/// Ширина
			/// </summary>
			public int Width {
				get;
				set;
			}

			/// <summary>
			/// Высота
			/// </summary>
			public int Height {
				get;
				set;
			}

			/// <summary>
			/// Вычисленная ширина (если видеокарта не поддерживет NPOT)
			/// </summary>
			public int ComputedWidth {
				get;
				set;
			}

			/// <summary>
			/// Вычисленная высота (если видеокарта не поддерживает NPOT)
			/// </summary>
			public int ComputedHeight {
				get;
				set;
			}

			/// <summary>
			/// Горизонтальный множитель (если видеокарта не поддерживает NPOT)
			/// </summary>
			public float HorizontalDelta {
				get;
				set;
			}

			/// <summary>
			/// Вертикальный множитель (если видеокарта не поддерживает NPOT)
			/// </summary>
			public float VerticalDelta {
				get;
				set;
			}

			/// <summary>
			/// Текстурная матрица (если видеокарта не поддерживает NPOT)
			/// </summary>
			public Matrix4 TextureMatrix {
				get;
				set;
			}

			/// <summary>
			/// Обьявление о новой ссылке на текстуру
			/// </summary>
			public void IncrementReference() {
				UseCount++;
			}

			/// <summary>
			/// Объявление об отписке от текстуры
			/// </summary>
			public void DecrementReference() {
				UseCount--;
				if (UseCount == 0) {
					LastUseLostTime = DateTime.Now;
				}
			}

			/// <summary>
			/// Отправка текстуры на видеокарту
			/// </summary>
			public void Send {

			}

			/// <summary>
			/// Удаление текстуры
			/// </summary>
			public void Release() {

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
			Complete = 4,
			/// <summary>
			/// Текстура очищена для экономии памяти
			/// </summary>
			Released = 5
		}
	}
}
