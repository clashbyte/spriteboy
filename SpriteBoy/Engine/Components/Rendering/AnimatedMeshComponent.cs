using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Абстрактный класс анимируемого меша
	/// </summary>
	public abstract class AnimatedMeshComponent : MeshComponent {

		/// <summary>
		/// Длина анимации
		/// </summary>
		public int AnimationLength {
			get {
				int ln = 0;
				Frame[] fr = Frames;
				if (fr!=null) {
					foreach (Frame f in fr) {
						if (f.Time > ln) {
							ln = f.Time;
						}
					}
				}
				return ln;
			}
		}

		/// <summary>
		/// Кадры меша
		/// </summary>
		public Frame[] Frames {
			get {
				Frame[] fr = frames;
				MeshComponent me = this;
				while (fr == null) {
					if (me.Proxy != null) {
						me = me.Proxy;
					}
					if (me is AnimatedMeshComponent) {
						fr = (me as AnimatedMeshComponent).frames;
					}
				}
				return fr;
			}
			set {
				frames = value;
				CurrentFrame = null;
			}
		}

		/// <summary>
		/// Внутренние кадры меша
		/// </summary>
		Frame[] frames;

		/// <summary>
		/// Необходимо сделать переходящий буффер
		/// </summary>
		internal bool needTransitionCache;
		/// <summary>
		/// Необходимо пересчитать данные
		/// </summary>
		internal bool frameRefresh;
		/// <summary>
		/// Переходное значение буффера
		/// </summary>
		internal float frameDelta;
		/// <summary>
		/// Первый кадр (-1 - интерполяция)
		/// </summary>
		protected int frameOne;
		/// <summary>
		/// Второй кадр
		/// </summary>
		protected int frameTwo;

		/// <summary>
		/// Текущее состояние меша
		/// </summary>
		internal virtual Frame CurrentFrame {
			get;
			set;
		}

		/// <summary>
		/// Переходной кадр
		/// </summary>
		internal virtual Frame TransitionFrame {
			get;
			set;
		}

		/// <summary>
		/// Кадр меша
		/// </summary>
		public abstract class Frame {

			/// <summary>
			/// Время кадра
			/// </summary>
			public int Time {
				get;
				set;
			}

		}

		/// <summary>
		/// Установка необходимых вершинных данных в самом начале отрисовки
		/// </summary>
		protected virtual void SetupVertexData() { }

		/// <summary>
		/// Отрисовка меша
		/// </summary>
		internal override void Render() {

			// Установка текущего кадра, если он пуст
			if (CurrentFrame == null) {
				Frame[] frm = Frames;
				if (frm!=null) {
					CurrentFrame = InterpolateFrame(frm[0], frm[0], 0);
				}
			}

			// Установка вершин
			SetupVertexData();

			// Отрисовка
			base.Render();
		}

		/// <summary>
		/// Интерполяция кадров
		/// </summary>
		/// <param name="f1">Первый кадр</param>
		/// <param name="f2">Второй кадр</param>
		/// <param name="delta">Интерполяция между кадрами</param>
		internal abstract Frame InterpolateFrame(Frame f1, Frame f2, float delta);

		/// <summary>
		/// Создание копии текущего кадра для интерполяции
		/// </summary>
		internal abstract void SnapshotTransition();

	}
}
