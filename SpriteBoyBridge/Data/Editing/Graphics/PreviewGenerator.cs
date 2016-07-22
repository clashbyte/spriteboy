using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpriteBoy.Data.Editing.Graphics {

	/// <summary>
	/// Абстрактный класс генератора превьюшек
	/// </summary>
	public abstract class PreviewGenerator {

		/// <summary>
		/// Генерация предпросмотра иконки с помощью данного генератора
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public abstract Image Generate(string filename);

		/// <summary>
		/// Получение прокси для указанного типа файла
		/// </summary>
		/// <param name="filename">Имя файла</param>
		/// <returns>Превью буллета</returns>
		public abstract Preview GetProxy(string filename);

		/// <summary>
		/// Показывать ли буллет после загрузки основной картинки
		/// </summary>
		/// <param name="filename">Имя файла</param>
		/// <returns>True если буллет требуется</returns>
		public abstract bool ShowBulletAfterLoad(string filename);

		/// <summary>
		/// Делать ли картинку в фоне
		/// </summary>
		/// <param name="filename">Имя файла</param>
		/// <returns>True если необходима генерация в фоновом потоке</returns>
		public abstract bool PutInQueue(string filename);

	}
}
