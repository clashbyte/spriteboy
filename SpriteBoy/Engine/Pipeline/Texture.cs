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
using SpriteBoy.Data;
using OpenTK;

namespace SpriteBoy.Engine.Pipeline {

	/// <summary>
	/// Текстурный объект
	/// </summary>
	public class Texture : IDisposable {

		/// <summary>
		/// Пустая текстура
		/// </summary>
		static int emptyGLTexture;

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
			get {
				if (IsReleased || tex == null) {
					return LoadingState.Empty;
				} else {
					return (LoadingState)((byte)tex.State);
				}
			}
		}

		/// <summary>
		/// Режим прозрачности текстуры
		/// </summary>
		public TransparencyMode Transparency {
			get {
				if (tex != null) {
					return (TransparencyMode)((byte)tex.Transparency);
				}
				return TransparencyMode.Opaque;
			}
		}

		/// <summary>
		/// Освобождена ли текстура
		/// </summary>
		public bool IsReleased {
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
			get {
				if (tex != null) {
					return tex.Width;
				}
				return 0;
			}
		}

		/// <summary>
		/// Высота текстуры
		/// </summary>
		public int Height {
			get {
				if (tex != null) {
					return tex.Height;
				}
				return 0;
			}
		}

		/// <summary>
		/// Текстура в кеше
		/// </summary>
		TextureCache.CacheEntry tex;

		/// <summary>
		/// Загрузка текстуры
		/// </summary>
		/// <param name="file">Имя файла</param>
		/// <param name="loadMode">Режим загрузки</param>
		public Texture(string file, LoadingMode loadMode = LoadingMode.Instant) {
			Link = file;
			WrapHorizontal = WrapMode.Repeat;
			WrapVertical = WrapMode.Repeat;
			tex = TextureCache.Get(file, loadMode == LoadingMode.Instant);
			tex.IncrementReference();
		}

		/// <summary>
		/// Удаление текстуры
		/// </summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Внутреннее освобожение ресурсов
		/// </summary>
		void Dispose(bool disposing) {
			if (!IsReleased) {
				if (tex != null) {
					tex.DecrementReference();
					tex = null;
				}
				IsReleased = true;
			}
		}

		/// <summary>
		/// Деструктор
		/// </summary>
		~Texture() {
			Dispose(false);
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
		/// Освобождение текстуры
		/// </summary>
		public void Release() {
			
		}

		/// <summary>
		/// Установка текстуры
		/// </summary>
		internal void Bind() {
			bool bindable = false;
			if (State != LoadingState.Empty) {
				if (tex.GLTex != 0) {
					bindable = true;
				}
			}

			// Если есть что установить в конвейер
			if (bindable) {

				// Установка в конвейер
				tex.Bind();

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
					BindEmpty();
				}
			}
		}

		/// <summary>
		/// Установка однопиксельной непрозрачной текстуры в конвейер
		/// </summary>
		internal static void BindEmpty() {
			if (GraphicalCaps.ShaderPipeline) {
				if (emptyGLTexture == 0) {
					emptyGLTexture = GL.GenTexture();
					GL.BindTexture(TextureTarget.Texture2D, emptyGLTexture);
					GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Four, 1, 1, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, new byte[]{255,255,255,255});
				} else {
					GL.BindTexture(TextureTarget.Texture2D, emptyGLTexture);
				}
				ShaderSystem.TextureMatrix = Matrix4.Identity;
			} else {
				GL.BindTexture(TextureTarget.Texture2D, 0);
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
			/// Текстура пуста
			/// </summary>
			Empty	= 5
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

		/// <summary>
		/// Тип прозрачности текстуры
		/// </summary>
		public enum TransparencyMode : byte {
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
