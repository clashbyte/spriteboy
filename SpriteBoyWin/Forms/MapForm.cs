using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteBoy.Controls;

namespace SpriteBoy.Forms {
	public partial class MapForm : BaseForm {



		public MapForm() {
			InitializeComponent();
			DoubleBuffered = false;

			NSGraphicsCanvas gc = new NSGraphicsCanvas();
			gc.Dock = DockStyle.Fill;
			gc.Paint += RenderCallback;
			gc.Visible = true;
			Controls.Add(gc);
		}


		void RenderCallback(object sender, PaintEventArgs peArg) {
			NSGraphicsCanvas gc = (NSGraphicsCanvas)sender;
			gc.MakeCurrent();

			//OpenTK.Graphics.OpenGL.GL.ClearColor(Color.FromArgb(40, 40, 40));
			//OpenTK.Graphics.OpenGL.GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit);

			//gc.SwapBuffers();
			gc.Swap();
		}




	}
}
