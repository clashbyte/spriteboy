using OpenTK.Graphics.OpenGL;
using SpriteBoy.Data.Rendering;
using SpriteBoy.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Components.Volumes {
	
	/// <summary>
	/// Объем из треугольников
	/// </summary>
	public class TrimeshVolumeComponent : VolumeComponent {

		/// <summary>
		/// Вершины
		/// </summary>
		public Vec3[] Vertices {
			get;
			set;
		}

		/// <summary>
		/// Индексы
		/// </summary>
		public ushort[] Indices {
			get;
			set;
		}

		/// <summary>
		/// Поиск пересечений с лучом
		/// </summary>
		/// <param name="rayPos">Начало луча</param>
		/// <param name="rayDir">Направление луча</param>
		/// <param name="rayLength">Длина луча</param>
		/// <param name="pos">Место пересечения</param>
		/// <param name="normal">Нормаль пересечения</param>
		/// <returns>True если есть пересечение</returns>
		internal override bool RayHit(Vec3 rayPos, Vec3 rayDir, float rayLength, out Vec3 pos, out Vec3 normal) {

			// Если есть с чем пересекаться
			if (Indices != null && Vertices != null) {
				if (Indices.Length > 0) {

					float range = float.MaxValue;
					bool hit = false;
					Vec3 hpos = Vec3.Zero, hnorm = Vec3.Zero;
					for (int i = 0; i < Indices.Length; i+=3) {
						Vec3 v0 = Vertices[Indices[i+0]];
						Vec3 v1 = Vertices[Indices[i+1]];
						Vec3 v2 = Vertices[Indices[i+2]];
						Vec3 hp, hn;
						if (Intersections.RayTriangle(rayPos, rayDir, v0, v1, v2, false, out hp, out hn)) {
							float d = (hp - rayPos).LengthSquared;
							if (d < range) {
								range = d;
								hpos = hp;
								hnorm = hn;
								hit = true;
							}
						}
					}

					range = (float)Math.Sqrt(range);
					if (hit && range <= rayLength) {
						pos = hpos;
						normal = hnorm;
						return true;
					}
				}
			}

			// Пересечения нет
			pos = Vec3.Zero;
			normal = Vec3.UnitZ;
			return false;
		}

		/// <summary>
		/// Отладочная отрисовка
		/// </summary>
		protected override void RenderDebug() {
			if (!Enabled) {
				return;
			}
			if (Vertices!=null && Indices != null) {
				GL.Color3(System.Drawing.Color.Yellow);
				GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
				GL.LineWidth(2f);
				GL.Disable(EnableCap.CullFace);
	
				GL.Begin(BeginMode.Triangles);
				foreach (ushort idx in Indices) {
					GL.Vertex3(Vertices[idx].X, Vertices[idx].Y, -Vertices[idx].Z);
				}
				GL.End();

				GL.Enable(EnableCap.CullFace);
				GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
			}
		}
	}
}
