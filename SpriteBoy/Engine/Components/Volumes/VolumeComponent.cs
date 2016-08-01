using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Components.Volumes {

	/// <summary>
	/// Класс объема для проверки пересечения с лучом
	/// </summary>
	public abstract class VolumeComponent : EntityComponent, IUpdatable, IRenderable {

		/// <summary>
		/// Проверка на пересечение с лучом
		/// </summary>
		/// <param name="pos">Позиция найденного пересечения</param>
		/// <param name="normal">Нормаль найденного пересечения</param>
		/// <returns>True если луч пересек хоть что-то</returns>
		internal abstract bool RayHit(Vec3 rayPos, Vec3 rayDir, float rayLength, out Vec3 pos, out Vec3 normal);

		/// <summary>
		/// Отладочная отрисовка
		/// </summary>
		internal override void Render() {
			if (GameEngine.DebugMode) {
				RenderDebug();
			}
		}

		/// <summary>
		/// Процедура отладочной отрисовки
		/// </summary>
		protected virtual void RenderDebug() { }
	}
}
