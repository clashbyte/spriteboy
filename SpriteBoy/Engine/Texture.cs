using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Files;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SpriteBoy.Engine {

	/// <summary>
	/// Текстурный объект
	/// </summary>
	public class Texture {

		/// <summary>
		/// Количество потоков для загрузки текстур
		/// </summary>
		const int THREAD_COUNT = 2;

		/// <summary>
		/// Текстур на загрузку в один кадр
		/// </summary>
		const int UPLOADS_PER_FRAME = 10;

		/// <summary>
		/// Очередь на чтение с диска
		/// </summary>
		static ConcurrentQueue<Texture> readingQueue;

		/// <summary>
		/// Очередь на отправку в видеопамять
		/// </summary>
		static ConcurrentQueue<Texture> sendingQueue;

		/// <summary>
		/// Потоки для загрузки текстуры
		/// </summary>
		static Thread[] loadingThreads;

		/// <summary>
		/// Имя файла во внутренней файловой системе
		/// </summary>
		public string Link {
			get;
			private set;
		}

		/// <summary>
		/// Состояние загрузки текстуры
		/// </summary>
		public LoadingState State {
			get;
			private set;
		}

		/// <summary>
		/// Какой текстурой заменить, если текущая ещё не загружена
		/// </summary>
		public Texture Proxy {
			get;
			set;
		}

		/// <summary>
		/// Повторение текстуры по горизонтали
		/// </summary>
		public WrapMode WrapHorizontal {
			get;
			set;
		}

		/// <summary>
		/// Повторение текстуры по вертикали
		/// </summary>
		public WrapMode WrapVertical {
			get;
			set;
		}

		/// <summary>
		/// Фильтрация текстуры
		/// </summary>
		public FilterMode Filtering {
			get;
			set;
		}

		/// <summary>
		/// Ширина текстуры
		/// </summary>
		public int Width {
			get;
			private set;
		}

		/// <summary>
		/// Высота текстуры
		/// </summary>
		public int Height {
			get;
			private set;
		}

		/// <summary>
		/// Промежуточные несжатые данные из файла
		/// </summary>
		byte[] scanData;

		

		/// <summary>
		/// "Имя" (индекс) GL-текстуры
		/// </summary>
		int texHandle;

		/// <summary>
		/// Тип пикселя
		/// </summary>
		OpenTK.Graphics.OpenGL.PixelFormat pixelFormat;




		/// <summary>
		/// Загрузка текстуры
		/// </summary>
		/// <param name="file">Имя файла</param>
		/// <param name="loadMode">Режим загрузки</param>
		public Texture(string file, LoadingMode loadMode = LoadingMode.Instant) {
			Link = file;
			WrapHorizontal = WrapMode.Repeat;
			WrapVertical = WrapMode.Repeat;
			if (loadMode == LoadingMode.Instant) {
				// Нужно загрузить прямо сейчас
				ReadData();
				SendToVRAM();
			} else {
				// Нужно добавить в список "на потом"
				PrepareThreads();
				readingQueue.Enqueue(this);
			}
		}

		/// <summary>
		/// Установка настроек из метаданных
		/// </summary>
		public void ApplyMetaConfig(byte[] meta) {
			if (meta != null) {
				BinaryReader f = new BinaryReader(new MemoryStream(meta));
				Filtering = (FilterMode)f.ReadByte();
				WrapHorizontal = (WrapMode)f.ReadByte();
				WrapVertical = (WrapMode)f.ReadByte();
				f.Close();
			}
		}

		/// <summary>
		/// Установка текстуры
		/// </summary>
		public void Bind() {
			if (State == LoadingState.Complete) {

				// Установка в конвейер
				GL.BindTexture(TextureTarget.Texture2D, texHandle);

				// Фильтрация
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				switch (Filtering) {
					case FilterMode.Disabled:
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
						break;
					case FilterMode.Enabled:
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
						break;
				}

				// Повторение по горизонтали
				switch (WrapHorizontal) {
					case WrapMode.Clamp:
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
						break;
					case WrapMode.Repeat:
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
						break;
					case WrapMode.Mirror:
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
						break;
				}

				// Повторение по вертикали
				switch (WrapVertical) {
					case WrapMode.Clamp:
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
						break;
					case WrapMode.Repeat:
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
						break;
					case WrapMode.Mirror:
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);
						break;
				}

			} else {
				if (Proxy != null) {
					Proxy.Bind();
				} else {
					GL.BindTexture(TextureTarget.Texture2D, 0);
				}
			}
		}

		/// <summary>
		/// Чтение данных
		/// </summary>
		void ReadData() {
			State = LoadingState.Reading;

			// Чтение байт-массива
			if (FileSystem.FileExist(Link)) {

				// Загрузка картинки
				byte[] data = FileSystem.Read(Link);
				Bitmap bmp = Bitmap.FromStream(new MemoryStream(data)) as Bitmap;
				Width = bmp.Width;
				Height = bmp.Height;

				// Превращение картинки в скан
				BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				scanData = new byte[Math.Abs(bd.Stride * bd.Height)];
				Marshal.Copy(bd.Scan0, scanData, 0, scanData.Length);
				bmp.UnlockBits(bd);
				bmp.Dispose();

				// Сохранение параметров скана
				pixelFormat = OpenTK.Graphics.OpenGL.PixelFormat.Bgra;

				// Текстура готова к отправке
				State = LoadingState.NotSent;
			} else {
				State = LoadingState.Destroyed;
			}
		}

		/// <summary>
		/// Отправка в видеопамять
		/// </summary>
		void SendToVRAM() {
			State = LoadingState.Sending;

			// Отправка текстуры
			texHandle = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, texHandle);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Four, Width, Height, 0, pixelFormat, PixelType.UnsignedByte, scanData);

			GL.BindTexture(TextureTarget.Texture2D, 0);
			scanData = null;

			State = LoadingState.Complete;
		}


		/// <summary>
		/// Запуск потоков
		/// </summary>
		static void PrepareThreads() {
			if (readingQueue == null) {
				readingQueue = new ConcurrentQueue<Texture>();
			}
			if (sendingQueue == null) {
				sendingQueue = new ConcurrentQueue<Texture>();
			}
			if (loadingThreads == null) {
				loadingThreads = new Thread[THREAD_COUNT];
				for (int i = 0; i < THREAD_COUNT; i++) {
					Thread t = new Thread(ThreadedLoading);
					t.Priority = ThreadPriority.BelowNormal;
					t.IsBackground = true;
					t.Start();
				}
			}
		}

		/// <summary>
		/// Процедура для потоковой загрузки данных
		/// </summary>
		static void ThreadedLoading() {
			while(true){
				if (!readingQueue.IsEmpty) {
					Texture t = null;
					if (readingQueue.TryDequeue(out t)) {
						if (t.State != LoadingState.Destroyed) {
							t.ReadData();
							sendingQueue.Enqueue(t);
						}
					}
					Thread.Sleep(0);
				}else{
					Thread.Sleep(50);
				}
			}
		}

		/// <summary>
		/// Отправка всех неотправленных текстур
		/// </summary>
		public static void UpdateQueued() {
			if (sendingQueue != null) {
				int quota = UPLOADS_PER_FRAME;
				while (!sendingQueue.IsEmpty) {

					Texture t = null;
					if (sendingQueue.TryDequeue(out t)) {
						if (t.State != LoadingState.Destroyed) {
							t.SendToVRAM();
						} else {
							t.scanData = null;
						}
					}

					quota--;
					if (quota <= 0) {
						break;
					}
				}
			}
		}

		/// <summary>
		/// Режим загрузки текстуры
		/// </summary>
		public enum LoadingMode : byte {
			/// <summary>
			/// Текстура загрузится сразу при вызове конструктора
			/// </summary>
			Instant		= 0,
			/// <summary>
			/// Текстура загрузится фоновым потоком
			/// </summary>
			Queued		= 1
		}

		/// <summary>
		/// Состояние загрузки текстуры
		/// </summary>
		public enum LoadingState : byte {
			/// <summary>
			/// Текстура ожидает чтение с диска
			/// </summary>
			Waiting		= 0,
			/// <summary>
			/// Текстура читается с диска
			/// </summary>
			Reading		= 1,
			/// <summary>
			/// Текстура прочитана, но не отправлена в видеопамять
			/// </summary>
			NotSent		= 2,
			/// <summary>
			/// Текстура отправляется на видеокарту
			/// </summary>
			Sending		= 3,
			/// <summary>
			/// Текстура отправлена и готова к отрисовке
			/// </summary>
			Complete	= 4,
			/// <summary>
			/// Текстура уничтожена и должна быть выгружена отовсюду
			/// </summary>
			Destroyed	= 5
		}

		/// <summary>
		/// Маппинг текстуры за пределами [0-1]
		/// </summary>
		public enum WrapMode : byte {
			/// <summary>
			/// Не повторять
			/// </summary>
			Clamp		= 0,
			/// <summary>
			/// Повторять
			/// </summary>
			Repeat		= 1,
			/// <summary>
			/// Повторять с отражением на кадом шаге
			/// </summary>
			Mirror		= 2
		}

		/// <summary>
		/// Режим фильтрации
		/// </summary>
		public enum FilterMode : byte {
			/// <summary>
			/// Текстура не сглаживается
			/// </summary>
			Disabled	= 0,
			/// <summary>
			/// Текстура сглаживается
			/// </summary>
			Enabled		= 1
		}
	}
}
