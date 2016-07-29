using SpriteBoy.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteBoy.Forms.Editors {

	/// <summary>
	/// Редактор изображений
	/// </summary>
	public partial class ImageForm : BaseForm {

		NSGraphicsCanvas canvas;

		public ImageForm() {
			InitializeComponent();
			filteringCombo.Items = SharedStrings.TextureFiltering;
			wrapUCombo.Items = SharedStrings.TextureWrapMode;
			wrapVCombo.Items = SharedStrings.TextureWrapMode;
			canvas = new NSGraphicsCanvas();
		}


	}
}
