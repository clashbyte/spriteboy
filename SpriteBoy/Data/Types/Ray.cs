using SpriteBoy.Engine;
using SpriteBoy.Engine.Components.Volumes;
using SpriteBoy.Engine.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Types {

	/// <summary>
	/// Луч для поиска объемов
	/// </summary>
	public class Ray {

		/// <summary>
		/// Расположение луча
		/// </summary>
		public Vec3 Position {
			get;
			set;
		}

		/// <summary>
		/// Направление луча
		/// </summary>
		public Vec3 Direction {
			get;
			set;
		}

		/// <summary>
		/// Длина луча
		/// </summary>
		public float Length {
			get;
			set;
		}

		/// <summary>
		/// Список объектов для проверки
		/// </summary>
		public Entity[] Entities {
			get;
			set;
		}

		/// <summary>
		/// Сцена, откуда собрать объекты
		/// </summary>
		public Scene Scene {
			get;
			set;
		}

		/// <summary>
		/// Создание пустого луча
		/// </summary>
		public Ray() : this(Vec3.Zero, Vec3.Zero, -1f, (Entity[])null) { }

		/// <summary>
		/// Создание луча со списком объектов
		/// </summary>
		/// <param name="entities">Список объектов для проверки</param>
		public Ray(Entity[] entities) : this(Vec3.Zero, Vec3.Zero, entities) { }

		/// <summary>
		/// Создание луча со списком объектов сцены
		/// </summary>
		/// <param name="scene">Сцена для сбора объектов</param>
		public Ray(Scene scene) : this(Vec3.Zero, Vec3.Zero, scene) { }

		/// <summary>
		/// Создание луча с координатами
		/// </summary>
		/// <param name="position">Позиция</param>
		/// <param name="direction">Направление</param>
		public Ray(Vec3 position, Vec3 direction) : this(position, direction, -1f, (Entity[])null) { }

		/// <summary>
		/// Создание луча с координатами и длиной
		/// </summary>
		/// <param name="position">Позиция</param>
		/// <param name="direction">Направление</param>
		public Ray(Vec3 position, Vec3 direction, float length) : this(position, direction, length, (Entity[])null) { }

		/// <summary>
		/// Создание луча с координатами и списком объектов
		/// </summary>
		/// <param name="position">Позиция</param>
		/// <param name="direction">Направление</param>
		/// <param name="entities">Список объектов для проверки</param>
		public Ray(Vec3 position, Vec3 direction, Entity[] entities) : this(position, direction, -1f, entities) { }

		/// <summary>
		/// Создание луча с координатами и сценой
		/// </summary>
		/// <param name="position">Позиция</param>
		/// <param name="direction">Направление</param>
		/// <param name="scene">Сцена для сбора объектов</param>
		public Ray(Vec3 position, Vec3 direction, Scene scene) : this(position, direction, -1f, scene) { }

		/// <summary>
		/// Создание луча
		/// </summary>
		/// <param name="position">Позиция</param>
		/// <param name="direction">Направление</param>
		/// <param name="length">Длина</param>
		/// <param name="entities">Список объектов для проверки</param>
		public Ray(Vec3 position, Vec3 direction, float length, Entity[] entities) {
			Position = position;
			Direction = direction;
			Length = length;
			Entities = entities;
		}

		/// <summary>
		/// Создание луча
		/// </summary>
		/// <param name="position">Позиция</param>
		/// <param name="direction">Направление</param>
		/// <param name="length">Длина</param>
		/// <param name="scene">Сцена для сбора объектов</param>
		public Ray(Vec3 position, Vec3 direction, float length, Scene scene) {
			Position = position;
			Direction = direction;
			Length = length;
			Scene = scene;
		}

		/// <summary>
		/// Создание луча из камеры
		/// </summary>
		/// <param name="cam">Камера</param>
		/// <param name="x">X-координата экрана</param>
		/// <param name="y">Y-координата экрана</param>
		/// <param name="length">Длина луча</param>
		/// <param name="entities">Список объектов для проверки</param>
		public Ray(Camera cam, float x, float y, float length, Entity[] entities) {
			FromCamera(cam, x, y);
			Length = length;
			Entities = entities;
		}

		/// <summary>
		/// Создание луча из камеры
		/// </summary>
		/// <param name="cam">Камера</param>
		/// <param name="x">X-координата экрана</param>
		/// <param name="y">Y-координата экрана</param>
		/// <param name="length">Длина луча</param>
		/// <param name="scene">Сцена для сбора объектов</param>
		public Ray(Camera cam, float x, float y, float length, Scene scene) {
			FromCamera(cam, x, y);
			Length = length;
			Scene = scene;
		}

		/// <summary>
		/// Подстройка луча под камеру
		/// </summary>
		/// <param name="cam">Камера</param>
		/// <param name="x">X-координата экрана</param>
		/// <param name="y">Y-координата экрана</param>
		public void FromCamera(Camera cam, float x, float y) {
			Position = cam.ScreenToPoint(new Vec3(x, y, 0f));
			Direction = cam.ScreenToPoint(new Vec3(x, y, 1f)) - Position;
		}

		/// <summary>
		/// Запуск луча
		/// </summary>
		/// <returns>Первое столкновение</returns>
		public HitInfo Cast() {
			return InternalCast(GetEntities());
		}

		/// <summary>
		/// Поиск всех пересечений
		/// </summary>
		/// <returns>Список всех пересечений</returns>
		public HitInfo[] CastAll() {
			return InternalCastAll(GetEntities());
		}

		/// <summary>
		/// Сбор всех объектов для проверки
		/// </summary>
		/// <returns>Массив объектов</returns>
		Entity[] GetEntities() {
			if (Entities!=null) {
				return Entities;
			}
			if (Scene != null) {
				return Scene.Entities.ToArray();
			}
			return new Entity[0];
		}

		/// <summary>
		/// Пересечение с ближайшим объектом
		/// </summary>
		/// <param name="entities">Список объектов</param>
		/// <returns></returns>
		HitInfo InternalCast(Entity[] entities) {
			HitInfo[] hi = InternalCastAll(entities);
			if (hi.Length>0) {
				return hi[0];
			}
			return null;
		}

		/// <summary>
		/// Пересечение со всеми доступными объектами
		/// </summary>
		/// <param name="entities">Список объектов</param>
		/// <returns>Все пересечения</returns>
		HitInfo[] InternalCastAll(Entity[] entities) {
			float ln = Length;
			if (ln<=0) {
				ln = float.MaxValue;
			}
			List<HitInfo> hlist = new List<HitInfo>();
			if (entities!=null) {
				foreach (Entity e in entities) {
					if (e.Visible) {
						Vec3 hp, hn;
						VolumeComponent hvol;
						if (e.RayCast(Position, Direction, ln, out hp, out hn, out hvol)) {
							hlist.Add(new HitInfo() {
								Position = hp,
								Normal = hn,
								Volume = hvol,
								Entity = e,
								dist = (hp - Position).Length
							});
						}
					}
				}
			}
			if (hlist.Count>0) {
				hlist.Sort((a, b) => {
					return a.dist.CompareTo(b.dist);
				});
			}
			return hlist.ToArray();
		}

		/// <summary>
		/// Пересечение
		/// </summary>
		public class HitInfo {

			/// <summary>
			/// Глобальное место пересечения
			/// </summary>
			public Vec3 Position {
				get;
				internal set;
			}

			/// <summary>
			/// Глобальная нормаль пересечения
			/// </summary>
			public Vec3 Normal {
				get;
				internal set;
			}

			/// <summary>
			/// Объем
			/// </summary>
			public VolumeComponent Volume {
				get;
				internal set;
			}

			/// <summary>
			/// Объект
			/// </summary>
			public Entity Entity {
				get;
				internal set;
			}

			/// <summary>
			/// Расстояние пересечения от центра луча
			/// </summary>
			internal float dist;
		}
	}
}
