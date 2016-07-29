using SpriteBoy.Data.Attributes;
using SpriteBoy.Data.Editing;
using SpriteBoy.Forms.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteBoy.Components.Editors {

	[FileEditor(typeof(ModelConverterForm), ".md3", ".md2", ".x", ".3ds")]
	public class ModelConverter : Editor {

		protected override void Load() {
			UpdateTitle();
			Saved = true;
		}

		public override void Save() {
			Saved = true;
		}
	}
}
