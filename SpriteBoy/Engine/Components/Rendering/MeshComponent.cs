﻿using SpriteBoy.Data.Types;
using SpriteBoy.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace SpriteBoy.Engine.Components.Rendering {

	/// <summary>
	/// Компонент простого статичного меша
	/// </summary>
	public class MeshComponent : Component, IRenderable {

		/// <summary>
		/// Вершины
		/// </summary>
		public Vec3[] Vertices {
			get {
				if (verts!=null) {
					Vec3[] va = new Vec3[verts.Length/3];
					for (int i = 0; i < va.Length; i++) {
						int ps = i * 3;
						va[i] = new Vec3(
							verts[ps], 
							verts[ps+1],
							-verts[ps+2]
						);
					}
					return va;
				}
				return null;
			}
			set {
				if (value!=null) {
					verts = new float[value.Length*3];
					for (int i = 0; i < value.Length; i++) {
						int ps = i * 3;
						verts[ps + 0] =  value[i].X;
						verts[ps + 1] =  value[i].Y;
						verts[ps + 2] = -value[i].Z;
					}
				} else {
					verts = null;
				}
			}
		}

		/// <summary>
		/// Текстурные координаты
		/// </summary>
		public Vec2[] TexCoords {
			get {
				if (uv != null) {
					Vec2[] ca = new Vec2[uv.Length / 2];
					for (int i = 0; i < ca.Length; i++) {
						int ps = i * 2;
						ca[i] = new Vec2(
							uv[ps],
							uv[ps + 1]
						);
					}
					return ca;
				}
				return null;
			}
			set {
				if (value != null) {
					uv = new float[value.Length * 2];
					for (int i = 0; i < value.Length; i++) {
						int ps = i * 2;
						uv[ps + 0] = value[i].X;
						uv[ps + 1] = value[i].Y;
					}
				} else {
					uv = null;
				}
			}
		}

		/// <summary>
		/// Индексы
		/// </summary>
		public ushort[] Indices {
			get {
				if (indices != null) {
					ushort[] ia = new ushort[indices.Length];
					for (int i = 0; i < indices.Length; i += 3) {
						ia[i + 0] = indices[i + 2];
						ia[i + 1] = indices[i + 1];
						ia[i + 2] = indices[i + 0];
					}
					return ia;
				}
				return null;
			}
			set {
				if (value != null) {
					indices = new ushort[value.Length];
					for (int i = 0; i < indices.Length; i+=3) {
						indices[i + 0] = value[i + 2];
						indices[i + 1] = value[i + 1];
						indices[i + 2] = value[i + 0];
					}
				} else {
					indices = null;
				}
			}
		}

		/// <summary>
		/// Текстура меша
		/// </summary>
		public Texture Texture {
			get;
			set;
		}

		/// <summary>
		/// Диффузный цвет
		/// </summary>
		public Color Diffuse {
			get;
			set;
		}

		/// <summary>
		/// Включен ли альфаканал
		/// </summary>
		public bool AlphaBlend {
			get;
			set;
		}

		// Скрытые переменные
		float[] verts;
		float[] uv;
		ushort[] indices;

		/// <summary>
		/// Статический меш
		/// </summary>
		public MeshComponent() {
			Diffuse = Color.White;
			AlphaBlend = false;
		}

		/// <summary>
		/// Отрисовка меша
		/// </summary>
		public void Render() {

			// Рендер только если есть что рисовать
			if (verts != null && indices !=null) {
				if (verts.Length>0 && indices.Length>0) {

					// Установка цвета
					GL.Color3(Diffuse);

					// Если есть альфасмешивание
					if (AlphaBlend) {
						GL.Enable(EnableCap.Blend);
						GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
						GL.DepthMask(false);
					}

					// Загрузка вершин
					GL.EnableClientState(ArrayCap.VertexArray);
					GL.VertexPointer(3, VertexPointerType.Float, 0, verts);

					// Загрузка текстурных координат
					if (Texture!=null && uv!=null) {
						GL.EnableClientState(ArrayCap.TextureCoordArray);
						GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, uv);
						Texture.Bind();
					} else {
						GL.BindTexture(TextureTarget.Texture2D, 0);
					}

					// Отрисовка элементов
					GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedShort, indices);

					// Выключение состояний
					GL.DisableClientState(ArrayCap.TextureCoordArray);
					GL.DisableClientState(ArrayCap.VertexArray);

					// Отключение смешивания
					if (AlphaBlend) {
						GL.Disable(EnableCap.Blend);
						GL.DepthMask(true);
					}
				}
			}

		}
	}
}
