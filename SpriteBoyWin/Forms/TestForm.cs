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
	public partial class TestForm : Form {
		public TestForm() {
			InitializeComponent();

			NSAnimationView.PointKey[] keys = new NSAnimationView.PointKey[15];
			for (int i = 0; i < keys.Length; i++) {
				keys[i] = new NSAnimationView.PointKey(i+4);
			}

			nsAnimationView1.PointKeys = keys;
		}

		private void TestForm_Load(object sender, EventArgs e) {

			
			

			//Close();
		}
	}
}
