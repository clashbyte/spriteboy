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

		WireCubeComponent morphDebug;

		/// <summary>
		/// Вершины - только чтение
		/// </summary>
		public override Vec3[] Vertices {
			get {
				if (vertices==null) {
					Frame[] f = Frames;
					if (f!=null) {
						CurrentFrame = InterpolateFrame(f[0], f[0], 0);
						SetupVertexData();
					}
				}
				return base.Vertices;
			}
		}

		/// <summary>
		/// Нормали - только чтение
		/// </summary>
		public override Vec3[] Normals {
			get {
				return base.Normals;
			}
		}

		/// <summary>
		/// Текущий кадр
		/// </summary>
		public override AnimatedMeshComponent.Frame CurrentFrame {
			get {
				return base.CurrentFrame;
			}
			internal set {
				base.CurrentFrame = value;
				MorphFrame cf = (MorphFrame)base.CurrentFrame;
				if (cf != null) {
					Vec3 max = Vec3.One * float.MinValue, min = Vec3.One * float.MaxValue;
					for (int i = 0; i < cf.verts.Length; i += 3) {
						if (cf.verts[i] > max.X) {
							max.X = cf.verts[i];
						}
						if (cf.verts[i+1] > max.Y) {
							max.Y = cf.verts[i+1];
						}
						if (-cf.verts[i+2] > max.Z) {
							max.Z = -cf.verts[i+2];
						}
						if (cf.verts[i] < min.X) {
							min.X = cf.verts[i];
						}
						if (cf.verts[i+1] < min.Y) {
							min.Y = cf.verts[i+1];
						}
						if (-cf.verts[i+2] < min.Z) {
							min.Z = -cf.verts[i+2];
						}
					}
					cull.Min = min;
					cull.Max = max;
					RebuildParentCull();
				}
				
			}
		}

		public MorphMeshComponent() {
			morphDebug = new WireCubeComponent() {
				WireWidth = 2f,
				WireColor = System.Drawing.Color.Red
			};
		}

		protected override void AfterRender() {
			//morphDebug.Render();
		}

		/// <summary>
		/// Установка вершинных данных в меш
		/// </summary>
		protected override void SetupVertexData() {
			vertices = (CurrentFrame as MorphFrame).verts;
			normals = (CurrentFrame as MorphFrame).normals;
		}

		/// <summary>
		/// Интерполирование двух кадров
		/// </summary>
		/// <param name="f1">Первый кадр</param>
		/// <param name="f2">Второй кадр</param>
		/// <param name="delta">Значение интерполяции</param>
		internal override Frame InterpolateFrame(Frame f1, Frame f2, float delta) {
			MorphFrame mf1 = (MorphFrame)f1;
			MorphFrame mf2 = (MorphFrame)f2;
			MorphFrame mf = new MorphFrame();
			mf.verts = new float[mf1.verts.Length];
			mf.normals = new float[mf1.normals.Length];

			// Интерполяция данных
			float p1, p2;
			for (int i = 0; i < mf.verts.Length; i++) {
				
				// Вершина
				p1 = mf1.verts[i];
				p2 = mf2.verts[i];
				mf.verts[i] = p1 + (p2 - p1) * delta;

				// Нормаль
				p1 = mf1.normals[i];
				p2 = mf2.normals[i];
				mf.normals[i] = p1 + (p2 - p1) * delta;

			}

			// Сохранение временного кадра
			return mf;
		}

		/// <summary>
		/// Создание переходного кадра
		/// </summary>
		internal override void SnapshotTransition() {
			MorphFrame mf = new MorphFrame();
			if (CurrentFrame!=null) {
				mf.verts = (CurrentFrame as MorphFrame).verts;
				mf.normals = (CurrentFrame as MorphFrame).normals;
			}
			TransitionFrame = mf;
		}

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
	}
}
