using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Types {

	/// <summary>
	/// Коробка для отсечения
	/// </summary>
	public class CullBox {

		/// <summary>
		/// Минимальная точка коробки
		/// </summary>
		public Vec3 Min {
			get;
			set;
		}

		/// <summary>
		/// Максимальная точка коробки
		/// </summary>
		public Vec3 Max {
			get;
			set;
		}

		/// <summary>
		/// Конвертация в сферу
		/// </summary>
		/// <returns>Сфера</returns>
		public CullSphere ToSphere() {
			return new CullSphere() {
				Position = (Max + Min) / 2f,
				Radius = (Max - Min).Length / 2f
			};
		}

		/// <summary>
		/// Боксы для отсечения
		/// </summary>
		/// <param name="boxes"></param>
		/// <returns></returns>
		public static CullBox FromBoxes(CullBox[] boxes) {
			Vec3 min = Vec3.One * float.MaxValue, max = Vec3.One * float.MinValue;
			foreach (CullBox bb in boxes) {
				if (bb.Min.X < min.X) min.X = bb.Min.X;
				if (bb.Min.Y < min.Y) min.Y = bb.Min.Y;
				if (bb.Min.Z < min.Z) min.Z = bb.Min.Z;
				if (bb.Max.X > max.X) max.X = bb.Max.X;
				if (bb.Max.Y > max.Y) max.Y = bb.Max.Y;
				if (bb.Max.Z > max.Z) max.Z = bb.Max.Z;
			}
			return new CullBox() {
				Min = min,
				Max = max
			};
		}
	}
}
