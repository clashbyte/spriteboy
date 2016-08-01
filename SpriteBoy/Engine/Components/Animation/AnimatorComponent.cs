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
		/// Анимированные меши
		/// </summary>
		AnimatedMeshComponent[] meshes;

		/// <summary>
		/// Режим анимации
		/// </summary>
		AnimationMode loopmode;

		/// <summary>
		/// Анимируется ли меш
		/// </summary>
		bool playing;

		/// <summary>
		/// Происходит переход
		/// </summary>
		bool trans;

		/// <summary>
		/// Время перехода
		/// </summary>
		float transTime;

		/// <summary>
		/// Начало и конец отрезка анимации
		/// </summary>
		float start, end;

		/// <summary>
		/// Скорость проигрывания
		/// </summary>
		float speed;

		/// <summary>
		/// Текущее время
		/// </summary>
		float time;

		/// <summary>
		/// Обновление анимации
		/// </summary>
		internal override void Update() {
			if (playing) {

				// Кадры для смешивания
				AnimatedMeshComponent.Frame fr1 = null, fr2 = null;
				float delta = 0f;

				// Установка анимации
				foreach (AnimatedMeshComponent mc in meshes) {

					if (trans) {
						delta = (transTime + time) / transTime;
					}else{
						float t = 0;
						if (speed < 0) {
							t = end - time;

						} else {
							t = start + time;
							fr1 = SearchFrameBackward(mc, t);
							fr2 = SearchFrameForward(mc, t);
						}
						if (fr1 != null && fr2 != null) {
							if (fr1.Time != fr2.Time) {
								delta = (t - fr1.Time) / (fr2.Time - fr1.Time);
							}
						}
					}
					
					// Применение кадров
					if (fr1 != null && fr2 != null) {
						mc.CurrentFrame = mc.InterpolateFrame(fr1, fr2, delta);
					}else if(fr1 != null){
						mc.CurrentFrame = mc.InterpolateFrame(fr1, fr1, delta);
					}else if(fr2 != null){
						mc.CurrentFrame = mc.InterpolateFrame(fr2, fr2, delta);
					}
				}

				// Обвновление логики
				if (trans) {

				}else{
					time += Math.Abs(speed);
					if (time > end - start) {
						switch (loopmode) {
							case AnimationMode.OneShot:
								time = end;
								playing = false;
								break;
							case AnimationMode.Loop:
								time = time % (end - start);
								break;
							case AnimationMode.PingPongLoop:
								time = time % (end - start);
								speed *= -1;
								break;
						}
					}
				}

			}
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
				speed = spd;
				start = from;
				end = to;
				loopmode = mode;

				if (transition > 0) {
					time = -transition;
					transTime = transition;
					foreach (AnimatedMeshComponent m in meshes) {
						m.SnapshotTransition();
					}
					trans = true;
				}else{
					time = 0f;
				}
				playing = true;
			}
		}

		/// <summary>
		/// Поиск кадра впереди
		/// </summary>
		/// <param name="m">Меш</param>
		/// <param name="time">Время</param>
		/// <param name="allowLoop">Разрешить поиск с другой стороны</param>
		/// <returns>Кадр</returns>
		AnimatedMeshComponent.Frame SearchFrameForward(AnimatedMeshComponent m, float time) {
			AnimatedMeshComponent.Frame[] frames = m.Frames;
			if (frames!=null && frames.Length>0) {
				// Поиск после
				for (int i = 0; i < frames.Length; i++) {
					if (frames[i].Time > time && frames[i].Time <= end) {
						return frames[i];
					}
				}

				// Поиск сзади
				if (loopmode == AnimationMode.Loop) {
					for (int i = 0; i < frames.Length; i++) {
						if (frames[i].Time >= start) {
							return frames[i];
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Поиск кадра сзади
		/// </summary>
		/// <param name="m">Меш</param>
		/// <param name="time">Время</param>
		/// <param name="allowLoop">Разрешить поиск с другой стороны</param>
		/// <returns>Кадр</returns>
		AnimatedMeshComponent.Frame SearchFrameBackward(AnimatedMeshComponent m, float time) {
			AnimatedMeshComponent.Frame[] frames = m.Frames;
			if (frames != null && frames.Length > 0) {
				// Поиск после
				for (int i = frames.Length-1; i >= 0; i--) {
					if (frames[i].Time < time && frames[i].Time >= start) {
						return frames[i];
					}
				}

				// Поиск сзади
				if (loopmode == AnimationMode.Loop) {
					for (int i = frames.Length - 1; i >= 0; i--) {
						if (frames[i].Time <= end) {
							return frames[i];
						}
					}
				}
			}
			return null;
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
