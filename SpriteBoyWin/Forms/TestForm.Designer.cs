namespace SpriteBoy.Forms {
	partial class TestForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.nsColorPicker1 = new SpriteBoy.Controls.NSColorPicker();
			this.SuspendLayout();
			// 
			// nsColorPicker1
			// 
			this.nsColorPicker1.Location = new System.Drawing.Point(12, 12);
			this.nsColorPicker1.Name = "nsColorPicker1";
			this.nsColorPicker1.SelectedColor = System.Drawing.Color.White;
			this.nsColorPicker1.Size = new System.Drawing.Size(200, 200);
			this.nsColorPicker1.TabIndex = 0;
			this.nsColorPicker1.Text = "nsColorPicker1";
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(488, 365);
			this.Controls.Add(this.nsColorPicker1);
			this.Name = "TestForm";
			this.Text = "TestForm";
			this.Load += new System.EventHandler(this.TestForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.NSColorPicker nsColorPicker1;
	}
}