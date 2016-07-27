using OpenTK;
using SpriteBoy.Data.Types;
using SpriteBoy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteBoy.Engine.Data;
using OpenTK.Graphics.OpenGL;

namespace SpriteBoy.Engine {
	
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
		protected List<Component> components;

		/// <summary>
		/// Создание объекта
		/// </summary>
		public Entity() {
			Children = new List<Entity>();
			components = new List<Component>();
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
		/// Добавление компонента к объекту
		/// </summary>
		/// <param name="c">Новый компонент</param>
		public void AddComponent(Component c) {
			components.Add(c);
		}

		/// <summary>
		/// Удаление компонента
		/// </summary>
		/// <param name="c">Компонент для удаления</param>
		public void RemoveComponent(Component c) {
			if (components.Contains(c)) {
				components.Remove(c);
			}
		}

		/// <summary>
		/// Получение указанного компонента
		/// </summary>
		/// <typeparam name="T">Тип компонента</typeparam>
		/// <param name="index">Индекс</param>
		/// <returns>Компонент или null</returns>
		public T GetComponent<T>(int index = 0) where T : Component {
			foreach (Component c in components) {
				if (c is T) {
					if (index>0) {
						index--;
					} else {
						return (T)c;
					}
				}
			}
			return default(T);
		}

		/// <summary>
		/// Отрисовка объекта
		/// </summary>
		public void Render() {
			GL.PushMatrix();
			GL.MultMatrix(ref mat);
			ShaderSystem.EntityMatrix = mat;

			foreach (Component c in components) {
				if (c is IRenderable) {
					(c as IRenderable).Render();
				}
			}
			GL.PopMatrix();
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
		/// Умножение углов эйлера на матрицу
		/// </summary>
		Vector3 ModifyAngles(Vector3 r, Matrix4 m) {
			return (r.ToQuaternion() * m.ExtractRotation()).ToEuler();
		}
		
	}
}
