using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Компонент повершинно-анимированного меша
	/// </summary>
	public class MorphMeshComponent : AnimatedMeshComponent {

		/// <summary>
		/// Морфный кадр анимации
		/// </summary>
		public class MorphFrame : AnimatedMeshComponent.Frame {

			/// <summary>
			/// Вершины
			/// </summary>
			public Vec3[] Vertices {
				get {
					if (verts != null) {
						Vec3[] va = new Vec3[verts.Length / 3];
						for (int i = 0; i < va.Length; i++) {
							int ps = i * 3;
							va[i] = new Vec3(
								verts[ps],
								verts[ps + 1],
								-verts[ps + 2]
							);
						}
						return va;
					}
					return null;
				}
				set {
					if (value != null) {
						verts = new float[value.Length * 3];
						for (int i = 0; i < value.Length; i++) {
							int ps = i * 3;
							verts[ps + 0] = value[i].X;
							verts[ps + 1] = value[i].Y;
							verts[ps + 2] = -value[i].Z;
						}
					} else {
						verts = null;
					}
				}
			}

			/// <summary>
			/// Нормали
			/// </summary>
			public Vec3[] Normals {
				get {
					if (normals != null) {
						Vec3[] na = new Vec3[normals.Length / 3];
						for (int i = 0; i < na.Length; i++) {
							int ps = i * 3;
							na[i] = new Vec3(
								normals[ps],
								normals[ps + 1],
								-normals[ps + 2]
							);
						}
						return na;
					}
					return null;
				}
				set {
					if (value != null) {
						normals = new float[value.Length * 3];
						for (int i = 0; i < value.Length; i++) {
							int ps = i * 3;
							normals[ps + 0] = value[i].X;
							normals[ps + 1] = value[i].Y;
							normals[ps + 2] = -value[i].Z;
						}
					} else {
						verts = null;
					}
				}
			}

			// Скрытые переменные
			internal float[] verts;
			internal float[] normals;
		}

		/// <summary>
		/// Сохранение текущего состояния как переходного кадра
		/// </summary>
		internal override void CreateTransionFrame() {
			
		}

		/// <summary>
		/// Интерполирование двух кадров
		/// </summary>
		/// <param name="f1">Первый кадр</param>
		/// <param name="f2">Второй кадр</param>
		/// <param name="delta">Значение интерполяции</param>
		internal override void SetFrame(Frame f1, Frame f2, float delta) {
			
		}
	}
}
