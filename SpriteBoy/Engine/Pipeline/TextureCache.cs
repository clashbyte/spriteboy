using OpenTK;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Files;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using SpriteBoy.Data;

namespace SpriteBoy.Engine.Pipeline {

	/// <summary>
	/// Кеш текстур
	/// </summary>
	internal static class TextureCache {

		/// <summary>
		/// Количество потоков для чтения с диска
		/// </summary>
		const int LOADING_THREADS = 2;

		/// <summary>
		/// Количество отправляемых текстур за кадр
		/// </summary>
		const int SENDS_PER_FRAME = 15;

		/// <summary>
		/// Количество очищаемых текстур за кадр
		/// </summary>
		const int RELEASES_PER_FRAME = 60;

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
		/// Текстуры на очистку
		/// </summary>
		static ConcurrentBag<int> releaseQueue = new ConcurrentBag<int>();
		
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
			while (!sendQueue.IsEmpty) {

				CacheEntry ce = null;
				if (sendQueue.TryDequeue(out ce)) {
					ce.SendToGL();
				}

				sendQuota--;
				if (sendQuota == 0) {
					break;
				}
			}

			// Проверка текстур 
			List<string> namesToRemove = new List<string>();
			foreach (KeyValuePair<string, CacheEntry> e in textures) {
				if (e.Value.UseCount == 0) {
					if ((DateTime.Now - e.Value.LastUseLostTime).Milliseconds > LOST_TEXTURE_LIFETIME) {
						System.Diagnostics.Debug.WriteLine("[TexCache] Released texture " + e.Key);
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
		/// Получение текстуры
		/// </summary>
		/// <param name="name">Имя файла в проектной файловой системе</param>
		internal static CacheEntry Get(string name, bool instant) {
			name = name.ToLower();
			if (!textures.ContainsKey(name)) {
				CacheEntry ce = new CacheEntry(name);
				if (instant) {
					ce.LoadNow();
				} else {
					ce.AddToLoadingQueue();
				}
				textures.TryAdd(name, ce);
			}
			return textures[name];
		}

		/// <summary>
		/// Проверка и запуск потоков
		/// </summary>
		static void CheckThreads() {
			if (loadingThreads == null) {
				loadingThreads = new Thread[LOADING_THREADS];
				for (int i = 0; i < LOADING_THREADS; i++) {
					Thread t = new Thread(ThreadedLoading);
					t.Priority = ThreadPriority.BelowNormal;
					t.IsBackground = true;
					t.Start();
				}
			}
		}

		/// <summary>
		/// Потоковая загрузка текстур
		/// </summary>
		static void ThreadedLoading() {
			while (true) {
				if (!loadQueue.IsEmpty) {
					CacheEntry t = null;
					if (loadQueue.TryDequeue(out t)) {
						t.ReadData();
						if (t.State != EntryState.Empty) {
							sendQueue.Enqueue(t);
						}
					}
					Thread.Sleep(0);
				} else {
					Thread.Sleep(50);
				}
			}
		}

		/// <summary>
		/// Одна запись в списке текстур
		/// </summary>
		internal class CacheEntry {

			/// <summary>
			/// Имя файла текстуры
			/// </summary>
			public string FileName {
				get;
				private set;
			}

			/// <summary>
			/// Количество использований
			/// </summary>
			public int UseCount {
				get;
				private set;
			}

			/// <summary>
			/// Время освобождение
			/// </summary>
			public DateTime LastUseLostTime {
				get;
				private set;
			}

			/// <summary>
			/// GL-текстура
			/// </summary>
			public int GLTex {
				get;
				private set;
			}

			/// <summary>
			/// Формат данных
			/// </summary>
			public OpenTK.Graphics.OpenGL.PixelFormat DataFormat {
				get;
				private set;
			}

			/// <summary>
			/// Скан-данные текстуры
			/// </summary>
			public byte[] Data {
				get;
				private set;
			}

			/// <summary>
			/// Режим прозрачности текстуры
			/// </summary>
			public TransparencyMode Transparency {
				get;
				private set;
			}

			/// <summary>
			/// Состояние текстуры
			/// </summary>
			public EntryState State {
				get;
				private set;
			}

			/// <summary>
			/// Ширина
			/// </summary>
			public int Width {
				get;
				private set;
			}

			/// <summary>
			/// Высота
			/// </summary>
			public int Height {
				get;
				private set;
			}

			/// <summary>
			/// Вычисленная ширина (если видеокарта не поддерживет NPOT)
			/// </summary>
			public int ComputedWidth {
				get;
				private set;
			}

			/// <summary>
			/// Вычисленная высота (если видеокарта не поддерживает NPOT)
			/// </summary>
			public int ComputedHeight {
				get;
				private set;
			}

			/// <summary>
			/// Горизонтальный множитель (если видеокарта не поддерживает NPOT)
			/// </summary>
			public float HorizontalDelta {
				get;
				private set;
			}

			/// <summary>
			/// Вертикальный множитель (если видеокарта не поддерживает NPOT)
			/// </summary>
			public float VerticalDelta {
				get;
				private set;
			}

			/// <summary>
			/// Текстурная матрица (если видеокарта не поддерживает NPOT)
			/// </summary>
			public Matrix4 TextureMatrix {
				get;
				private set;
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
			/// Создание записи
			/// </summary>
			/// <param name="file">Имя файла</param>
			public CacheEntry(string file) {
				State = EntryState.Empty;
				FileName = file;
				TextureMatrix = Matrix4.Identity;
			}

			/// <summary>
			/// Загрузка сейчас же
			/// </summary>
			public void LoadNow() {
				ReadData();
				SendToGL();
			}

			/// <summary>
			/// Добавление текстуры в очередь загрузки
			/// </summary>
			public void AddToLoadingQueue() {
				if (State != EntryState.Reading && State != EntryState.Waiting && !loadQueue.Contains(this)) {
					loadQueue.Enqueue(this);
					CheckThreads();
				}
			}

			/// <summary>
			/// Чтение данных с диска
			/// </summary>
			public void ReadData() {
				State = EntryState.Reading;

				// Чтение байт-массива
				if (FileSystem.FileExist(FileName)) {

					// Загрузка картинки
					byte[] data = FileSystem.Read(FileName);
					byte[] scan = null;
					Bitmap bmp = Bitmap.FromStream(new MemoryStream(data)) as Bitmap;
					Width = bmp.Width;
					Height = bmp.Height;

					// Если текстуры, некратные двойке, не поддерживаются
					if (!GraphicalCaps.NonPowerOfTwoTextures) {
						ComputedWidth = Pow2(Width);
						ComputedHeight = Pow2(Height);
						if (Width != ComputedWidth || Height != ComputedHeight) {
							Bitmap pr = new Bitmap(ComputedWidth, ComputedHeight);
							using (Graphics g = Graphics.FromImage(pr)) {
								g.DrawImage(bmp, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, Width, Height), GraphicsUnit.Pixel);
							}
							bmp.Dispose();
							bmp = pr;
						}
						HorizontalDelta = (float)Width / (float)ComputedWidth;
						VerticalDelta = (float)Height / (float)ComputedHeight;
						TextureMatrix = Matrix4.CreateScale(HorizontalDelta, VerticalDelta, 1f);
					} else {
						ComputedWidth = Width;
						ComputedHeight = Height;
						HorizontalDelta = 1f;
						VerticalDelta = 1f;
						TextureMatrix = Matrix4.Identity;
					}

					// Превращение картинки в скан
					BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					scan = new byte[Math.Abs(bd.Stride * bd.Height)];
					Marshal.Copy(bd.Scan0, scan, 0, scan.Length);
					bmp.UnlockBits(bd);
					bmp.Dispose();
					

					// Сохранение параметров скана
					DataFormat = OpenTK.Graphics.OpenGL.PixelFormat.Bgra;
					Data = scan;

					// Определение прозрачности по скану
					Transparency = TransparencyMode.Opaque;
					for (int ty = 0; ty < Height; ty++) {
						for (int tx = 0; tx < Width; tx++) {
							byte a = scan[(ty * ComputedWidth + tx) * 4 + 3];
							if (a < 255) {
								Transparency = TransparencyMode.AlphaCut;
								if (a > 0) {
									Transparency = TransparencyMode.AlphaFull;
									break;
								}
							}
						}
					}

					// Текстура готова к отправке
					State = EntryState.NotSent;
				} else {
					State = EntryState.Empty;
				}
			}

			/// <summary>
			/// Отправка текстуры на видеокарту
			/// </summary>
			public void SendToGL() {
				State = EntryState.Sending;
				GL.Enable(EnableCap.Texture2D);

				GLTex = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D, GLTex);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Four, ComputedWidth, ComputedHeight, 0, DataFormat, PixelType.UnsignedByte, Data);
				GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvModeCombine.Modulate);
				GL.BindTexture(TextureTarget.Texture2D, 0);

				Data = null;

				State = EntryState.Complete;
			}

			/// <summary>
			/// Удаление текстуры
			/// </summary>
			public void Release() {
				if (GLTex != 0) {
					if (GL.IsTexture(GLTex)) {
						GL.DeleteTexture(GLTex);
					}
					GLTex = 0;
				}
				if (Data != null) {
					Data = null;
				}
				State = EntryState.Empty;
			}

			/// <summary>
			/// Включение текстуры
			/// </summary>
			public void Bind() {

				// Включение в конвейер
				GL.BindTexture(TextureTarget.Texture2D, GLTex);
				
				// Загрузка матрицы
				ShaderSystem.TextureMatrix = TextureMatrix;
				GL.MatrixMode(MatrixMode.Texture);
				Matrix4 mat = TextureMatrix;
				GL.LoadMatrix(ref mat);
				GL.MatrixMode(MatrixMode.Modelview);
				

			}

			/// <summary>
			/// Поиск наибольшего числа, кратного двойке
			/// </summary>
			/// <param name="num">Число</param>
			/// <returns>Ближайшее кратное двойке</returns>
			int Pow2(int num) {
				int d = 1;
				while (d<num) {
					d *= 2;
				}
				return d;
			}
		}

		/// <summary>
		/// Состояние текстуры
		/// </summary>
		internal enum EntryState : byte {
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
			/// Текстура очищена или не загружена
			/// </summary>
			Empty = 5
		}

		/// <summary>
		/// Тип прозрачности текстуры
		/// </summary>
		internal enum TransparencyMode : byte {
			/// <summary>
			/// Полностью непрозрачное изображение
			/// </summary>
			Opaque = 0,
			/// <summary>
			/// Однобитная прозрачность, есть-нет
			/// </summary>
			AlphaCut = 1,
			/// <summary>
			/// Интерполированная прозрачность
			/// </summary>
			AlphaFull = 2
		}
	}
}
