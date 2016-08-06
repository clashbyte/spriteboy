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
		public virtual Frame[] Frames {
			get {
				Frame[] fr = frames;
				MeshComponent me = this;
				while (fr == null) {
					if (me.Proxy != null) {
						me = me.Proxy;
					} else {
						break;
					}
					if (me is AnimatedMeshComponent) {
						fr = (me as AnimatedMeshComponent).frames;
					}
				}
				return fr;
			}
			set {
				frames = value;
			}
		}

		/// <summary>
		/// Внутренние кадры меша
		/// </summary>
		Frame[] frames;

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
		/// Обновление анимации и рендер
		/// </summary>
		internal override void Render() {
			// Обновление переходного кадра
			if (queuedTransitionUpdate != null) {
				UpdateTransition();
				queuedTransitionUpdate = null;
			}

			// Обновление анимации
			if (queuedUpdate != null) {
				UpdateAnimation();
				queuedUpdate = null;
			}

			// Рендер
			base.Render();
		}

		/// <summary>
		/// Требуется ли обновление анимации
		/// </summary>
		internal QueuedMeshUpdate queuedUpdate;

		/// <summary>
		/// Требуется ли сохранение перехода
		/// </summary>
		internal QueuedMeshUpdate queuedTransitionUpdate;

		/// <summary>
		/// Создание копии текущего кадра для интерполяции
		/// </summary>
		protected abstract void UpdateTransition();

		/// <summary>
		/// Обновление анимации
		/// </summary>
		protected abstract void UpdateAnimation();

		/// <summary>
		/// Внутреннее запланированное обновление меша
		/// </summary>
		internal class QueuedMeshUpdate {
			/// <summary>
			/// Первый кадр
			/// </summary>
			public float FirstFrame;
			/// <summary>
			/// Последний кадр
			/// </summary>
			public float LastFrame;
			/// <summary>
			/// Текущий кадр
			/// </summary>
			public float Time;
			/// <summary>
			/// Анимация круговая
			/// </summary>
			public bool IsLooping;
		}
	}
}
