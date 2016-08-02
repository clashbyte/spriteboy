using OpenTK;
using SpriteBoy.Data.Types;
using SpriteBoy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteBoy.Engine.Data;
using OpenTK.Graphics.OpenGL;
using SpriteBoy.Data.Rendering;
using SpriteBoy.Engine.Components.Rendering;
using SpriteBoy.Engine.Components.Volumes;

namespace SpriteBoy.Engine.World {
	
	/// <summary>
	/// Базовый объект игрового мира
	/// </summary>
	public class Entity {

		/// <summary>
		/// Скрытая матрица
		/// </summary>
		protected Matrix4 mat = Matrix4.Identity;

		/// <summary>
		/// Инвертированная матрица
		/// </summary>
		protected Matrix4 invmat = Matrix4.Identity;

		/// <summary>
		/// Родитель
		/// </summary>
		protected Entity parent;

		/// <summary>
		/// Расположение
		/// </summary>
		protected Vector3 pos = Vector3.Zero;

		/// <summary>
		/// Поворот
		/// </summary>
		protected Vector3 rot = Vector3.Zero;

		/// <summary>
		/// Виден ли объект
		/// </summary>
		protected bool visible = true;

		/// <summary>
		/// Компоненты объекта
		/// </summary>
		protected List<EntityComponent> components;

		/// <summary>
		/// Требуется перестройка сферы отсечения
		/// </summary>
		internal bool needCullRebuild;

		/// <summary>
		/// Сфера отсечения
		/// </summary>
		protected CullSphere cullSphere = new CullSphere();

		/// <summary>
		/// Создание объекта
		/// </summary>
		public Entity() {
			Children = new List<Entity>();
			components = new List<EntityComponent>();
		}

		/// <summary>
		/// Радиус объекта - используется в редакторе
		/// </summary>
		public float Radius {
			get {
				if (needCullRebuild) {
					RebuildCullSphere();
				}
				return cullSphere.Radius;
			}
		}

		/// <summary>
		/// Виден ли объект
		/// </summary>
		public bool Visible {
			get {
				Entity d = this;
				while (d!=null) {
					if (!d.visible) {
						return false;
					}
					d = d.parent;
				}
				return true;
			}
			set {
				visible = value;
			}
		}

		/// <summary>
		/// Получение расположения
		/// </summary>
		public Vec3 Position {
			get {
				Vector3 vt = pos;
				if(parent != null){
					vt = Vector3.TransformPosition(vt, parent.invmat);
				}
				return new Vec3(
					vt.X, vt.Y, -vt.Z
				);
			}
			set {
				pos = new Vector3(
					value.X, value.Y, -value.Z
				);
				if (parent != null) {
					pos = Vector3.TransformPosition(pos, parent.mat);
				}
				RebuildMatrix();
			}
		}

		/// <summary>
		/// Получение локального расположения
		/// </summary>
		public Vec3 LocalPosition {
			get {
				return new Vec3(
					pos.X, pos.Y, -pos.Z
				);
			}
			set {
				pos = new Vector3(
					value.X, value.Y, -value.Z
				);
				RebuildMatrix();
			}
		}

		/// <summary>
		/// Получение поворота
		/// </summary>
		public Vec3 Angles {
			get {
				Vector3 vt = rot;
				if (parent != null) {
					vt = ModifyAngles(pos, parent.invmat);
				}
				return new Vec3(
					-vt.X, -vt.Y, vt.Z
				);
			}
			set {
				rot = new Vector3(
					-value.X, -value.Y, value.Z
				);
				if (parent != null) {
					rot = ModifyAngles(pos, parent.mat);
				}
				RebuildMatrix();
			}
		}

		/// <summary>
		/// Получение локального расположения
		/// </summary>
		public Vec3 LocalAngles {
			get {
				return new Vec3(
					-rot.X, -rot.Y, rot.Z
				);
			}
			set {
				rot = new Vector3(
					-value.X, -value.Y, value.Z
				);
				RebuildMatrix();
			}
		}

		/// <summary>
		/// Дочерние объекты
		/// </summary>
		public List<Entity> Children {
			get; private set;
		}

		/// <summary>
		/// Родитель
		/// </summary>
		public Entity Parent {
			get {
				return parent;
			}
			set {
				if (parent != null) {
					pos = Vector3.TransformPosition(pos, parent.invmat);
					parent.Children.Remove(this);
					parent = null;
				}
				if (value!=null) {
					parent = value;
					parent.Children.Add(this);
				}
			}
		}

		/// <summary>
		/// Матрица - используется рендером
		/// </summary>
		internal Matrix4 RenditionMatrix {
			get {
				return mat;
			}
		}

		/// <summary>
		/// Добавление компонента к объекту
		/// </summary>
		/// <param name="c">Новый компонент</param>
		public void AddComponent(EntityComponent c) {
			c.Parent = this;
			if (c is IRenderable) {
				needCullRebuild = true;
			}
			components.Add(c);
		}

		/// <summary>
		/// Удаление компонента
		/// </summary>
		/// <param name="c">Компонент для удаления</param>
		public void RemoveComponent(EntityComponent c) {
			if (components.Contains(c)) {
				c.Parent = null;
				if (c is IRenderable) {
					needCullRebuild = true;
				}
				components.Remove(c);
			}
		}

		/// <summary>
		/// Получение указанного компонента
		/// </summary>
		/// <typeparam name="T">Тип компонента</typeparam>
		/// <param name="index">Индекс</param>
		/// <returns>Компонент или null</returns>
		public T GetComponent<T>(int index = 0) where T : EntityComponent {
			foreach (EntityComponent c in components) {
				if (c is T) {
					if (index>0) {
						index--;
					} else {
						return (T)c;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Получение всех компонентов указанного типа
		/// </summary>
		/// <typeparam name="T">Тип компонента</typeparam>
		/// <returns>Список компонентов</returns>
		public T[] GetComponents<T>() where T : EntityComponent {
			List<T> rc = new List<T>();
			foreach (EntityComponent c in components) {
				if (c is T) {
					rc.Add((T)c);
				}
			}
			return rc.ToArray();
		}

		/// <summary>
		/// Отдача всех обновляемых объектов
		/// </summary>
		internal IEnumerable<EntityComponent> GetLogicalComponents() {
			List<EntityComponent> cl = new List<EntityComponent>();
			foreach (EntityComponent c in components) {
				if (c is IUpdatable && c.Enabled) {
					cl.Add(c);
				}
			}
			return cl;
		}

		/// <summary>
		/// Отдача всех видимых компонентов
		/// </summary>
		internal IEnumerable<EntityComponent> GetVisualComponents() {

			// Сборка компонентов
			List<EntityComponent> cl = new List<EntityComponent>();

			// Перестройка сферы отсечения
			if (needCullRebuild) {
				RebuildCullSphere();
			}

			// Если не отсечен по сфере
			if (Frustum.Contains(cullSphere.Position, cullSphere.Radius)) {

				// Поиск видимых компонентов
				foreach (EntityComponent c in components) {
					if (c is IRenderable && c.Enabled) {
						cl.Add(c);
					}
				}
			}

			return cl;
		}

		/// <summary>
		/// Проброс луча через объект
		/// </summary>
		/// <param name="pos">Расположение луча</param>
		/// <param name="dir">Направление луча</param>
		/// <param name="hitPos">Место пересечения</param>
		/// <param name="hitNormal">Нормаль пересечения</param>
		/// <param name="hitVolume">Пересеченный объект</param>
		/// <returns>True если есть пересечение</returns>
		internal bool RayCast(Vec3 pos, Vec3 dir, float rayLength, out Vec3 hitPos, out Vec3 hitNormal, out VolumeComponent hitVolume) {

			// Перестроение основной сферы
			if (needCullRebuild) {
				RebuildCullSphere();
			}

			// Пересечение с основной сферой
			if (Intersections.RaySphere(pos, dir, cullSphere.Position, cullSphere.Radius)) {
				float range = float.MaxValue;
				Vec3 norm = Vec3.Zero;
				Vec3 hitp = Vec3.Zero;
				VolumeComponent hvol = null;

				// Пересечение с волюмами
				Vec3 tfpos = PointToLocal(pos);
				Vec3 tfnrm = VectorToLocal(dir);
				VolumeComponent[] volumes = GetComponents<VolumeComponent>();
				if (volumes!=null && volumes.Length >0) {
					foreach (VolumeComponent v in volumes) {
						if (v.Enabled) {
							Vec3 hp, hn;
							if (v.RayHit(tfpos, tfnrm, rayLength, out hp, out hn)) {
								float dst = (hp - tfpos).Length;
								if (dst < range) {
									range = dst;
									hitp = hp;
									norm = hn;
									hvol = v;
								}
							}
						}
					}
				}
				
				// Возврат
				if (hvol != null && range <= rayLength) {
					hitPos = PointToWorld(hitp);
					hitNormal = VectorToWorld(norm);
					hitVolume = hvol;
					return true;
				}
			}

			// Нет пересечения
			hitPos = Vec3.Zero;
			hitNormal = Vec3.Zero;
			hitVolume = null;
			return false;
		}

		/// <summary>
		/// Перевод точки из глобальной позиции в локальную
		/// </summary>
		/// <param name="point">Позиция</param>
		/// <returns>Позиция в локальных координатах</returns>
		public Vec3 PointToLocal(Vec3 point) {
			Vector3 v = Vector3.TransformPosition(new Vector3(point.X, point.Y, -point.Z), invmat);
			return new Vec3(v.X, v.Y, -v.Z);
		}

		/// <summary>
		/// Перевод точки из локальной позиции в глобальную
		/// </summary>
		/// <param name="point">Позиция</param>
		/// <returns>Позиция в глобальных координатах</returns>
		public Vec3 PointToWorld(Vec3 point) {
			Vector3 v = Vector3.TransformPosition(new Vector3(point.X, point.Y, -point.Z), mat);
			return new Vec3(v.X, v.Y, -v.Z);
		}

		/// <summary>
		/// Перевод вектора из глобального направления в локальное
		/// </summary>
		/// <param name="vec">Вектор</param>
		/// <returns>Вектор в локальном направлении</returns>
		public Vec3 VectorToLocal(Vec3 vec) {
			Vector3 v = Vector3.TransformVector(new Vector3(vec.X, vec.Y, -vec.Z), invmat);
			return new Vec3(v.X, v.Y, -v.Z);
		}

		/// <summary>
		/// Перевод вектора из локального направления в глобальное
		/// </summary>
		/// <param name="vec">Вектор</param>
		/// <returns>Вектор в глобальном направлении</returns>
		public Vec3 VectorToWorld(Vec3 vec) {
			Vector3 v = Vector3.TransformVector(new Vector3(vec.X, vec.Y, -vec.Z), mat);
			return new Vec3(v.X, v.Y, -v.Z);
		}

		/// <summary>
		/// Перестройка матрицы
		/// </summary>
		protected virtual void RebuildMatrix() {

			// Построение матрицы
			mat = Matrix4.CreateFromQuaternion(rot.ToQuaternion()) * Matrix4.CreateTranslation(pos);
			if (parent != null) {
				mat *= parent.mat;
			}
			invmat = mat.Inverted();

			// Обновление детей
			foreach (Entity e in Children) {
				e.RebuildMatrix();
			}
		}

		/// <summary>
		/// Перестройка сферы отсечения
		/// </summary>
		protected virtual void RebuildCullSphere() {
			List<CullBox> culls = new List<CullBox>();
			foreach (EntityComponent c in components) {
				if (c is IRenderable) {
					CullBox cb = c.GetCullingBox();
					if (cb!=null) {
						culls.Add(cb);
					}
				}
			}
			cullSphere = CullBox.FromBoxes(culls.ToArray()).ToSphere();
			cullSphere.Position = PointToWorld(cullSphere.Position);
			needCullRebuild = false;
		}

		/// <summary>
		/// Умножение углов эйлера на матрицу
		/// </summary>
		Vector3 ModifyAngles(Vector3 r, Matrix4 m) {
			return (r.ToQuaternion() * m.ExtractRotation()).ToEuler();
		}
		
	}
}
