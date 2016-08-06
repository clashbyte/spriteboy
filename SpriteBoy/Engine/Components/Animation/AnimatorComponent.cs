using SpriteBoy.Engine.Components.Rendering;
using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Components.Animation {

	/// <summary>
	/// Аниматор для меша
	/// </summary>
	public class AnimatorComponent : EntityComponent, IUpdatable {

		/// <summary>
		/// Количество кадров
		/// </summary>
		public int FrameCount {
			get {
				return (int)Math.Ceiling(MaxTime);
			}
		}

		/// <summary>
		/// Максимальное время
		/// </summary>
		public float MaxTime {
			get {
				AnimatedMeshComponent[] ms = Parent.GetComponents<AnimatedMeshComponent>();
				float sz = 0;
				foreach (AnimatedMeshComponent m in ms) {
					float l = m.AnimationLength;
					if (l > sz) {
						sz = l;
					}
				}
				return sz;
			}
		}

		/// <summary>
		/// Режим анимации
		/// </summary>
		public AnimationMode Mode {
			get;
			private set;
		}

		/// <summary>
		/// Играется ли анимация
		/// </summary>
		public bool Playing {
			get;
			private set;
		}

		/// <summary>
		/// Первый кадр
		/// </summary>
		public int FirstFrame {
			get;
			private set;
		}

		/// <summary>
		/// Последний кадр
		/// </summary>
		public int LastFrame {
			get;
			private set;
		}

		/// <summary>
		/// Скорость проигрывания
		/// </summary>
		public float Speed {
			get {
				return speed;
			}
			set {
				speed = value;
			}
		}

		/// <summary>
		/// Идёт переход
		/// </summary>
		public bool IsTransition {
			get;
			private set;
		}

		/// <summary>
		/// Текущее время
		/// </summary>
		public float Time {
			get {
				if (IsTransition) {
					return -time / transLength;
				}
				if (Speed > 0) {
					return (float)FirstFrame + time;
 				}
				return (float)LastFrame - time;
			}
		}
		
		/// <summary>
		/// Текущее время
		/// </summary>
		float time;

		/// <summary>
		/// Первый кадр
		/// </summary>
		float firstFrame;

		/// <summary>
		/// Последний кадр
		/// </summary>
		float lastFrame;

		/// <summary>
		/// Скорость проигрывания
		/// </summary>
		float speed;

		/// <summary>
		/// Длина перехода
		/// </summary>
		float transLength;

		/// <summary>
		/// Анимированные меши
		/// </summary>
		AnimatedMeshComponent[] meshes;

		/// <summary>
		/// Обновление анимации
		/// </summary>
		internal override void Update() {
			if (Playing) {

				// Рассчёт текущих значений
				float currentTime = 0f;
				if (IsTransition) {
					currentTime = -time / transLength;
				} else {
					if (speed < 0) {
						currentTime = lastFrame - time;
					} else {
						currentTime = firstFrame + time;
					}
				}

				// Установка анимации
				foreach (AnimatedMeshComponent mc in meshes) {
					AnimatedMeshComponent.QueuedMeshUpdate qu = new AnimatedMeshComponent.QueuedMeshUpdate();
					qu.FirstFrame = firstFrame;
					qu.LastFrame = lastFrame;
					qu.IsLooping = Mode == AnimationMode.Loop;
					qu.Time = currentTime;
					mc.queuedUpdate = qu;
				}

				// Обвновление логики
				if (IsTransition) {
					time += 1f;
					if (time > 0) {
						IsTransition = false;
					}
				}else{
					if (speed == 0 || firstFrame == lastFrame) {
						Playing = false;
					} else {
						time += Math.Abs(speed);
						if (time > lastFrame - firstFrame) {
							switch (Mode) {
								case AnimationMode.OneShot:
									time = lastFrame;
									Playing = false;
									break;
								case AnimationMode.Loop:
									time = time % (lastFrame - firstFrame + 1f);
									break;
								case AnimationMode.PingPongLoop:
									time = time % (lastFrame - firstFrame);
									speed *= -1;
									break;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Установка кадра без анимирования 
		/// </summary>
		/// <param name="frame">Время для установки</param>
		public void StopWithFrame(float frame) {
			Stop();
			if (meshes == null) {
				meshes = Parent.GetComponents<AnimatedMeshComponent>();
			}
			if (meshes != null) {
				foreach (AnimatedMeshComponent m in meshes) {
					AnimatedMeshComponent.QueuedMeshUpdate qu = new AnimatedMeshComponent.QueuedMeshUpdate();
					qu.FirstFrame = 0;
					qu.LastFrame = MaxTime;
					qu.IsLooping = false;
					qu.Time = 0;
					if (qu.LastFrame > 0) {
						qu.Time = frame % qu.LastFrame;
					}
					m.queuedUpdate = qu;
				}
			}
		}

		/// <summary>
		/// Остановка анимации
		/// </summary>
		public void Stop() {
			float currentTime = 0f;
			if (IsTransition) {
				currentTime = time / transLength;
			} else {
				if (speed < 0) {
					currentTime = lastFrame - time;
				} else {
					currentTime = firstFrame + time;
				}
			}
			if (meshes!=null) {
				foreach (AnimatedMeshComponent m in meshes) {
					AnimatedMeshComponent.QueuedMeshUpdate qu = new AnimatedMeshComponent.QueuedMeshUpdate();
					qu.FirstFrame = firstFrame;
					qu.LastFrame = lastFrame;
					qu.IsLooping = Mode == AnimationMode.Loop;
					qu.Time = currentTime;
					m.queuedUpdate = qu;
				}
			}
			
			Playing = false;
		}

		/// <summary>
		/// Анимирование меша
		/// </summary>
		/// <param name="from">Начальное время</param>
		/// <param name="to">Конечное время</param>
		/// <param name="mode">Режим анимации</param>
		/// <param name="transition">Количество переходных кадров</param>
		public void Animate(float from, float to, float spd = 1f, AnimationMode mode = AnimationMode.Loop, float transition = 0f) {
			meshes = Parent.GetComponents<AnimatedMeshComponent>();
			if (meshes!=null) {

				// Установка общих значений
				float oldFirstFrame = firstFrame;
				float oldLastFrame = lastFrame;
				float currentTime = 0f;
				if (IsTransition) {
					currentTime = time / transLength;
				} else {
					if (speed < 0) {
						currentTime = oldLastFrame - time;
					} else {
						currentTime = oldFirstFrame + time;
					}
				}
				bool allowRepeat = Mode == AnimationMode.Loop;

				speed = spd;
				firstFrame = from;
				lastFrame = to;
				Mode = mode;
				IsTransition = false;
				if (firstFrame>lastFrame) {
					float d = firstFrame;
					firstFrame = lastFrame;
					lastFrame = d;
				}
				float maxFrame = MaxTime;
				if (firstFrame < 0) {
					firstFrame = 0;
				}
				if (lastFrame > maxFrame) {
					lastFrame = maxFrame;
				}

				if (transition > 0) {
					time = -transition;
					transLength = transition;
					foreach (AnimatedMeshComponent m in meshes) {
						AnimatedMeshComponent.QueuedMeshUpdate qu = new AnimatedMeshComponent.QueuedMeshUpdate();
						qu.FirstFrame = oldFirstFrame;
						qu.LastFrame = oldLastFrame;
						qu.IsLooping = allowRepeat;
						qu.Time = currentTime;
						m.queuedTransitionUpdate = qu;
					}
					IsTransition = true;
				}else{
					time = 0f;
				}
				Playing = true;
			}
		}

		/// <summary>
		/// Тип анимации
		/// </summary>
		public enum AnimationMode {
			/// <summary>
			/// Только в одну сторону
			/// </summary>
			OneShot,
			/// <summary>
			/// Повтор
			/// </summary>
			Loop,
			/// <summary>
			/// Повтор туда-обратно
			/// </summary>
			PingPongLoop
		}
	}
}
