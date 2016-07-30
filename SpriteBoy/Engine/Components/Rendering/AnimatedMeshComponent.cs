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
		public Frame CurrentFrame {
			get;
			private set;
		}

		/// <summary>
		/// Переходной кадр
		/// </summary>
		public Frame TransitionFrame {
			get;
			private set;
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
		/// Сохранение переходного кадра из текущего
		/// </summary>
		internal abstract void CreateTransionFrame();

		/// <summary>
		/// Установка текущего кадра
		/// </summary>
		/// <param name="f1">Первый кадр</param>
		/// <param name="f2">Второй кадр</param>
		/// <param name="delta">Интерполяция между кадрами</param>
		internal abstract void SetFrame(Frame f1, Frame f2, float delta);

	}
}
