using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data {
	
	/// <summary>
	/// Расширения для TKшных векторов
	/// </summary>
	static class DataExtensions {

		/// <summary>
		/// Преобразование из вектора в кватернион
		/// </summary>
		/// <param name="euler">Углы Эйлера</param>
		/// <returns>Кватернион</returns>
		public static Quaternion ToQuaternion(this Vector3 euler) {
			float pitch = MathHelper.DegreesToRadians(euler.X);
			float yaw = MathHelper.DegreesToRadians(euler.Y);
			float roll = MathHelper.DegreesToRadians(euler.Z);
			float rollOver2 = roll * 0.5f;
			float sinRollOver2 = (float)Math.Sin((double)rollOver2);
			float cosRollOver2 = (float)Math.Cos((double)rollOver2);
			float pitchOver2 = pitch * 0.5f;
			float sinPitchOver2 = (float)Math.Sin((double)pitchOver2);
			float cosPitchOver2 = (float)Math.Cos((double)pitchOver2);
			float yawOver2 = yaw * 0.5f;
			float sinYawOver2 = (float)Math.Sin((double)yawOver2);
			float cosYawOver2 = (float)Math.Cos((double)yawOver2);
			Quaternion result = new Quaternion();
			result.W = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
			result.X = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
			result.Y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
			result.Z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
			return result;
		}

		/// <summary>
		/// Преобразование кватерниона в углы Эйлепа
		/// </summary>
		/// <param name="q1">Кватернион</param>
		/// <returns>Вектор с углами</returns>
		public static Vector3 ToEuler(this Quaternion q1) {
			float sqw = q1.W * q1.W;
			float sqx = q1.X * q1.X;
			float sqy = q1.Y * q1.Y;
			float sqz = q1.Z * q1.Z;
			float unit = sqx + sqy + sqz + sqw;
			float test = q1.X * q1.W - q1.Y * q1.Z;
			Vector3 v;

			if (test > 0.4995f * unit) {
				v.Y = 2f * (float)Math.Atan2(q1.Y, q1.X);
				v.X = (float)Math.PI / 2f;
				v.Z = 0;
				return NormalizeAngles(v);
			}
			if (test < -0.4995f * unit) {
				v.Y = -2f * (float)Math.Atan2(q1.Y, q1.X);
				v.X = -(float)Math.PI / 2;
				v.Z = 0;
				return NormalizeAngles(v);
			}
			Quaternion q = new Quaternion(q1.Z, q1.X, q1.Y, q1.W);
			v.Y = (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (q.Z * q.Z + q.W * q.W));
			v.X = (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y));
			v.Z = (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (q.Y * q.Y + q.Z * q.Z));
			return NormalizeAngles(v);
		}

		static Vector3 NormalizeAngles(Vector3 angles) {
			angles.X = NormalizeAngle(angles.X);
			angles.Y = NormalizeAngle(angles.Y);
			angles.Z = NormalizeAngle(angles.Z);
			return angles;
		}

		static float NormalizeAngle(float angle) {
			angle = MathHelper.RadiansToDegrees(angle);
			while (angle > 360)
				angle -= 360;
			while (angle < 0)
				angle += 360;
			return angle;
		}

	}
}
