using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpriteBoy.Files {

	/// <summary>
	/// Расширения для файловой системы
	/// </summary>
	public static class FileExtensions {

		/// <summary>
		/// Запись строки с префиксным размером
		/// </summary>
		/// <param name="f">Поток для записи</param>
		/// <param name="s">Строка</param>
		public static void WritePrefixedString(this BinaryWriter f, string s) {
			f.Write((UInt32)s.Length);
			f.Write(s.ToCharArray());
		}

		/// <summary>
		/// Чтение строки с префиксным размером
		/// </summary>
		/// <param name="f">Поток для чтения</param>
		/// <returns>Строка</returns>
		public static string ReadPrefixedString(this BinaryReader f) {
			uint sz = f.ReadUInt32();
			string s = "";
			if (sz>0) {
				s = new string(f.ReadChars((int)sz));
			}
			return s;
		}

		/// <summary>
		/// Запись строки с константным размером
		/// </summary>
		/// <param name="f">Поток для записи</param>
		/// <param name="s">Строка</param>
		/// <param name="ln">Длина строки в символах</param>
		public static void WriteConstantString(this BinaryWriter f, string s, int ln) {
			if (s.Length > ln) {
				s = s.Substring(0, ln);
			} else if (s.Length < ln) {
				s = s.PadRight(ln, '\0');
			}
			f.Write(s.ToCharArray());
		}

		/// <summary>
		/// Чтение строки с константным размером
		/// </summary>
		/// <param name="f">Поток для чтения</param>
		/// <returns>Строка</returns>
		public static string ReadConstantString(this BinaryReader f, int ln) {
			string l = new string(f.ReadChars(ln));
			if (l.Contains('\0')) {
				l = l.Substring(0, l.IndexOf('\0'));
			}
			return l;
		}

		
	}
}
