using SpriteBoy.Data.Types;
using SpriteBoy.Engine.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Engine.Data {

	/// <summary>
	/// Некоторый компонент от энтити - может быть чем угодно
	/// </summary>
	public abstract class EntityComponent {

		/// <summary>
		/// Включен ли компонент
		/// </summary>
		public bool Enabled;

		/// <summary>
		/// Родитель компонента
		/// </summary>
		public Entity Parent {
			get;
			internal set;
		}

		/// <summary>
		/// Конструктор компонента
		/// </summary>
		public EntityComponent() {
			Enabled = true;
		}

		
		/// <summary>
		/// Отрисовка компонента
		/// </summary>
		internal virtual void Render() { }

		/// <summary>
		/// Обновление компонента
		/// </summary>
		internal virtual void Update() { }

		/// <summary>
		/// Получение бокса для отсечения
		/// </summary>
		/// <returns></returns>
		internal virtual CullBox GetCullingBox() { return null; }

		/// <summary>
		/// Перестройка родительского отсекающего объема
		/// </summary>
		internal void RebuildParentCull() {
			if (Parent!=null) {
				Parent.needCullRebuild = true;
			}
		}
	}
}
