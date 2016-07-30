﻿using SpriteBoy.Data.Types;
using SpriteBoy.Engine;
using SpriteBoy.Engine.Components.Rendering;
using SpriteBoy.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Formats {

	/// <summary>
	/// Загрузка модели MD2
	/// </summary>
	internal static class MD2Loader {

		/// <summary>
		/// Загрузка MD2 из файла
		/// </summary>
		/// <param name="fname">Имя файла</param>
		/// <returns>Собранный меш</returns>
		public static Entity Load(string fname) {

			// Загрузка модели
			MorphMeshComponent me = ReadModel(fname);

			// Создание объекта
			Entity e = new Entity();
			e.AddComponent(new MorphMeshComponent() {
				Proxy = me,
				Texture = new Texture("skin_ss.png")
			});

			// Возврат
			return e;
		}

		/// <summary>
		/// Чтение модели с диска, если она не закеширована
		/// </summary>
		/// <param name="fname">Имя файла</param>
		/// <returns>Кешированная модель</returns>
		static MorphMeshComponent ReadModel(string fname) {

			// Открытие файла
			byte[] data = FileSystem.Read(fname);
			BinaryReader f = new BinaryReader(new MemoryStream(data));

			// Чтение заголовка
			string fourcc = new string(f.ReadChars(4));
			int version = f.ReadInt32();
			if (fourcc != "IDP2" || version != 8) {
				f.Close();
				return null;
			}

			// Размер текстуры и одного кадра
			float skinWidth = f.ReadInt32();
			float skinHeight = f.ReadInt32();
			int frameSize = f.ReadInt32();

			// Чтение размеров и отступов
			f.BaseStream.Position += 4;
			int vertexNum = f.ReadInt32();
			int texCoordNum = f.ReadInt32();
			int trisNum = f.ReadInt32();
			f.BaseStream.Position += 4;
			int frameNum = f.ReadInt32();

			f.BaseStream.Position += 4;
			int texCoordOffset = f.ReadInt32();
			int trisOffset = f.ReadInt32();
			int frameOffset = f.ReadInt32();

			// Чтение текстурных координат
			Vec2[] texCoords = new Vec2[texCoordNum];
			f.BaseStream.Position = texCoordOffset;
			for (int i = 0; i < texCoordNum; i++) {
				texCoords[i].X = (float)f.ReadInt16() / skinWidth;
				texCoords[i].Y = (float)f.ReadInt16() / skinHeight;
			}

			// Чтение треугольников
			ushort[] indices = new ushort[trisNum * 3];
			ushort[] coordAssoc = new ushort[trisNum * 3];
			f.BaseStream.Position = trisOffset;
			for (int i = 0; i < trisNum; i++) {
				int tri = i * 3;
				indices[tri + 0] = f.ReadUInt16();
				indices[tri + 1] = f.ReadUInt16();
				indices[tri + 2] = f.ReadUInt16();
				coordAssoc[tri + 0] = f.ReadUInt16();
				coordAssoc[tri + 1] = f.ReadUInt16();
				coordAssoc[tri + 2] = f.ReadUInt16();
			}

			// Чтение кадров
			Vec3[][] verts = new Vec3[frameNum][];
			Vec3[][] norms = new Vec3[frameNum][];
			f.BaseStream.Position = frameOffset;
			for (int fr = 0; fr < frameNum; fr++) {

				// Создание массивов
				verts[fr] = new Vec3[vertexNum];
				norms[fr] = new Vec3[vertexNum];

				// Размер и позиция
				float sx, sy, sz, ox, oy, oz;
				sx = f.ReadSingle();
				sy = f.ReadSingle();
				sz = f.ReadSingle();
				ox = f.ReadSingle();
				oy = f.ReadSingle();
				oz = f.ReadSingle();

				// Имя кадра
				f.BaseStream.Position += 16;

				// Чтение вершин
				for (int vr = 0; vr < vertexNum; vr++) {

					// Чтение вершины
					float px, py, pz;
					px = (float)f.ReadByte() * sx + ox;
					py = (float)f.ReadByte() * sy + oy;
					pz = (float)f.ReadByte() * sz + oz;
					verts[fr][vr] = new Vec3(-py, pz, px) * 0.015f;

					// Чтение нормали
					byte nidx = f.ReadByte();
					norms[fr][vr] = Vec3.Zero;
				}
			}
			f.Close();

			// Теперь, из-за того, что имеет место использование
			// одних и тех же вершин, но с разными текстурными
			// координатами, требуется создать копии для каждой
			// вершины под свои координаты

			// Поиск всех использованных текстурных координат каждой вершиной
			int totalVerts = 0;
			List<int>[] usedCoords = new List<int>[vertexNum];
			for (int i = 0; i < indices.Length; i++) {
				if (usedCoords[indices[i]]==null) {
					usedCoords[indices[i]] = new List<int>();
				}
				if (!usedCoords[indices[i]].Contains(coordAssoc[i])) {
					usedCoords[indices[i]].Add(coordAssoc[i]);
					totalVerts++;
				}
			}
			
			// Заполнение массива ассоциаций вершин и текстурных координат
			Vec2[] coords = new Vec2[totalVerts];
			int[] vertexAssoc = new int[totalVerts];
			int assocIndex = 0;
			int vertexIndex = 0;
			foreach (List<int> il in usedCoords) {
				for (int i = 0; i < il.Count; i++) {
					vertexAssoc[assocIndex + i] = vertexIndex;
					coords[assocIndex + i] = texCoords[il[i]];
				}
				assocIndex += il.Count;
				vertexIndex++;
			}

			// Перестройка массива индексов
			for (int i = 0; i < indices.Length; i++) {
				int idx = 0;
				int tex = coordAssoc[i];
				foreach (List<int> ac in usedCoords) {
					if (ac.Contains(tex)) {
						idx += ac.IndexOf(tex);
						break;
					}
					idx += ac.Count;
				}
				indices[i] = (ushort)idx;
			}

			// Перестройка кадров
			MorphMeshComponent.Frame[] morphFrames = new MorphMeshComponent.Frame[frameNum];
			for (int fr = 0; fr < frameNum; fr++) {

				// Переассоциация вершин
				Vec3[] frameVerts = new Vec3[totalVerts];
				Vec3[] frameNorms = new Vec3[totalVerts];
				for (int vr = 0; vr < totalVerts; vr++) {
					frameVerts[vr] = verts[fr][vertexAssoc[vr]];
					frameNorms[vr] = norms[fr][vertexAssoc[vr]];
				}

				// Создание кадра
				MorphMeshComponent.MorphFrame frame = new MorphMeshComponent.MorphFrame();
				frame.Vertices = frameVerts;
				frame.Normals = frameNorms;
				morphFrames[fr] = frame;
			}

			// Создание компонента
			MorphMeshComponent mmesh = new MorphMeshComponent();
			mmesh.TexCoords = coords;
			mmesh.Indices = indices;
			mmesh.Frames = morphFrames;

			return mmesh;
		}

	}
}
