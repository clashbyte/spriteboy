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
		public float AnimationLength {
			get {
				float ln = 0;
				Frame[] fr = Frames;
				if (fr!=null) {
					foreach (Frame f in fr) {
						ln += f.Time;
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
		/// Текущее состояние меша
		/// </summary>
		public virtual Frame CurrentFrame {
			get;
			internal set;
		}

		/// <summary>
		/// Переходной кадр
		/// </summary>
		public virtual Frame TransitionFrame {
			get;
			internal set;
		}

		/// <summary>
		/// Кадр меша
		/// </summary>
		public abstract class Frame {

			/// <summary>
			/// Время кадра
			/// </summary>
			public float Time {
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
