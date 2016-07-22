using SpriteBoy.Data.Attributes;
using SpriteBoy.Events.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Threading;

namespace SpriteBoy.Data.Editing.Graphics {
	
	/// <summary>
	/// Изображение предпросмотра
	/// </summary>
	public partial class Preview {

		/// <summary>
		/// Список генераторов для указанного расширения файлов
		/// </summary>
		static Dictionary<string, Type> Generators = new Dictionary<string, Type>();

		/// <summary>
		/// Делегат для отслеживания завершения загрузки иконок
		/// </summary>
		public delegate void PreviewsReadyEventHandler(PreviewReadyEventArgs e);

		/// <summary>
		/// Событие загрузки новых превьюшек
		/// </summary>
		public static event PreviewsReadyEventHandler PreviewsReady;

		/// <summary>
		/// Очередь для загрузки изображений
		/// </summary>
		static ConcurrentQueue<Preview> loadingQueue = new ConcurrentQueue<Preview>();

		/// <summary>
		/// Очередь готовых иконок
		/// </summary>
		static ConcurrentQueue<Preview> readyQueue = new ConcurrentQueue<Preview>();

		/// <summary>
		/// Таймер для создания событий загрузки
		/// </summary>
		static System.Timers.Timer readyMessageTimer;

		/// <summary>
		/// Фоновый поток для загрузки изображений
		/// </summary>
		static Thread loadingThread;

		/// <summary>
		/// Имя файла
		/// </summary>
		public string FilePath { get; private set; }

		/// <summary>
		/// Флаг о готовности иконок
		/// </summary>
		public bool Ready { get; private set; }

		/// <summary>
		/// Прокси-превью - откуда брать картинки если основная не загружена
		/// </summary>
		public Preview Proxy { get; private set; }

		/// <summary>
		/// Показывать ли прокси-картинку в качестве буллета
		/// </summary>
		public bool ProxyBullet { get; private set; }

		/// <summary>
		/// Большая иконка
		/// </summary>
		public ShadowImage LargeIcon {
			get {
				if (largeIcon != null && Ready) {
					return largeIcon;
				} else {
					return (Proxy != null) ? Proxy.largeIcon : null;
				}
			}
		}

		/// <summary>
		/// Маленькая иконка
		/// </summary>
		public ShadowImage SmallIcon {
			get {
				if (smallIcon != null && Ready) {
					return smallIcon;
				} else {
					return (Proxy != null) ? Proxy.smallIcon : null;
				}
			}
		}

		/// <summary>
		/// Иконка-буллет
		/// </summary>
		public ShadowImage BulletIcon {
			get {
				if (bulletIcon != null && Ready) {
					return bulletIcon;
				} else {
					return (Proxy != null) ? Proxy.bulletIcon : null;
				}
			}
		}

		/// <summary>
		/// Скрытые изображения
		/// </summary>
		ShadowImage largeIcon, smallIcon, bulletIcon;

		/// <summary>
		/// Генератор для указанного файла
		/// </summary>
		PreviewGenerator generator;

		/// <summary>
		/// Создание нового превью
		/// </summary>
		/// <param name="fname">Имя файла</param>
		/// <param name="gen">Тип генератора для превью</param>
		Preview(string fname, Type gen) {
			generator = Activator.CreateInstance(gen) as PreviewGenerator;
			FilePath = fname;
			Proxy = generator.GetProxy(fname);
			ProxyBullet = generator.ShowBulletAfterLoad(fname);

			Refresh();
			
		}

		/// <summary>
		/// Создание превью из готового изображения
		/// </summary>
		/// <param name="img">Изображение</param>
		Preview(Image img) {
			BuildImages(img, true);
		}

		/// <summary>
		/// Пересоздание иконки
		/// </summary>
		public void Refresh() {
			if (generator.PutInQueue(FilePath)) {
				loadingQueue.Enqueue(this);
				if (loadingThread == null) {
					StartLoader();
				}
			} else {
				Image img = generator.Generate(FilePath);
				if (img != null) {
					BuildImages(CropTransparent(img as Bitmap));
				}
			}
		}

		/// <summary>
		/// Генерация превьюшек из изображения
		/// </summary>
		/// <param name="img">Изображение</param>
		void BuildImages(Image img, bool filter = false) {
			largeIcon = new ShadowImage(ScaleProportional(img, 128, filter));
			smallIcon = new ShadowImage(ScaleProportional(largeIcon.Scan, 64, filter));
			bulletIcon = new ShadowImage(ScaleProportional(smallIcon.Scan, 24, filter));
			Ready = true;
		}

		/// <summary>
		/// Обрезка прозрачной части изображения
		/// </summary>
		/// <param name="img">Изображение</param>
		/// <returns>Обрезанное изображение</returns>
		static Image CropTransparent(Bitmap img) {
			
			// Первый этап - поиск непрозрачной части
			int minX = img.Width;
			int minY = img.Height;
			int maxX = 0;
			int maxY = 0;
			for (int y = 0; y < img.Height; y++) {
				for (int x = 0; x < img.Width; x++) {
					if (img.GetPixel(x, y).A > 0) {
						if (x > maxX) maxX = x;
						if (y > maxY) maxY = y;
						if (x < minX) minX = x;
						if (y < minY) minY = y;
					}
				}
			}

			// Второй этап - создание изображения
			Rectangle rect = new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
			Bitmap bm = new Bitmap(rect.Width, rect.Height);
			using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm)) {
				g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
			}
			return bm;
		}

		/// <summary>
		/// Пропорциональное изменение размера
		/// </summary>
		/// <param name="img">Изображение</param>
		/// <param name="size">Ширина и высота</param>
		/// <returns>Новая подогнанная иконка</returns>
		Image ScaleProportional(Image img, int size, bool filter = false) {
			// Рассчитываем размер
			float pw = (float)size / (float)img.Width;
			float ph = (float)size / (float)img.Height;
			float mul = (pw > ph) ? pw : ph;
			int nw = (int)((float)img.Width * mul);
			int nh = (int)((float)img.Height * mul);

			// Обрабатываем маленькие картинки
			if (!filter && img.Width < size && img.Height < size) {
				int pow = 2;
				while (img.Width * pow < size && img.Height * pow < size) {
					pow++;
				}
				Image pg = new Bitmap(img.Width * pow, img.Height * pow);
				using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(pg)) {
					g.InterpolationMode = InterpolationMode.NearestNeighbor;
					g.DrawImage(img, new Rectangle(0, 0, pg.Width, pg.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
				}
				img = pg;
			}

			Image ret = new Bitmap(nw, nh);
			using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ret)) {
				g.CompositingMode = CompositingMode.SourceCopy;
				g.CompositingQuality = CompositingQuality.HighQuality;
				g.InterpolationMode = InterpolationMode.Low;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.PixelOffsetMode = PixelOffsetMode.HighQuality;
				g.DrawImage(img, new Rectangle(0, 0, nw, nh), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
			}
			return ret;
		}

		/// <summary>
		/// Получение превью
		/// </summary>
		/// <param name="fname">Имя файла</param>
		/// <returns>Превью</returns>
		public static Preview Get(string fname) {
			Preview p;
			string ext = System.IO.Path.GetExtension(fname).ToLower();
			if (Generators.ContainsKey(ext)) {
				p = new Preview(fname, Generators[ext]);
			}else{
				p = FileIcon;
			}
			return p;
		}

		/// <summary>
		/// Создание превью из изображения
		/// </summary>
		/// <param name="img">Изображение</param>
		/// <returns>Превью для данной картинки</returns>
		public static Preview FromImage(Image img) {
			return new Preview(img);
		}

		/// <summary>
		/// Ассоциация расширений с редактором
		/// </summary>
		public static void Register() {
			Generators = new Dictionary<string, Type>();

			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				foreach (Type type in assembly.GetTypes()) {
					object[] attribs = type.GetCustomAttributes(typeof(PreviewFormats), false);
					if (attribs != null && attribs.Length > 0) {
						PreviewFormats ce = attribs[0] as PreviewFormats;

						// Привязка к расширениям
						foreach (string ex in ce.Extensions) {
							if (!Generators.ContainsKey(ex)) {
								Generators.Add(ex, type);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Запуск загрузчика
		/// </summary>
		static void StartLoader() {

			// Поток-загрузчик
			loadingThread = new Thread(() => {
				while (true) {
					int quota = 5;
					while (!loadingQueue.IsEmpty) {

						// Загрузка изображения
						Preview p;
						if (loadingQueue.TryDequeue(out p)) {
							Image bmp = p.generator.Generate(p.FilePath);
							if (bmp!=null) {
								bmp = CropTransparent((Bitmap)bmp);
								p.BuildImages(bmp);
							}
							readyQueue.Enqueue(p);
						}

						// Уменьшение квоты загрузки
						quota--;
						if (quota==0) {
							break;
						}
						Thread.Sleep(0);
					}

					Thread.Sleep(10);
				}
			});
			loadingThread.IsBackground = true;
			loadingThread.Priority = ThreadPriority.BelowNormal;
			loadingThread.Start();

			// Таймер
			readyMessageTimer = new System.Timers.Timer(100);
			readyMessageTimer.AutoReset = true;
			readyMessageTimer.Elapsed += (sender, args) => {
				if (PreviewsReady != null) {
					if (!readyQueue.IsEmpty) {
						List<Preview> pl = new List<Preview>();
						while (!readyQueue.IsEmpty) {
							Preview p;
							if (readyQueue.TryDequeue(out p)) {
								pl.Add(p);
							}
						}
						PreviewsReady(new PreviewReadyEventArgs() {
							ReadyPreviews = pl.ToArray()
						});
					}
				}
			};
			readyMessageTimer.Start();
		}
	}

}
