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
			this.nsAnimationView1 = new SpriteBoy.Controls.NSAnimationView();
			this.SuspendLayout();
			// 
			// nsAnimationView1
			// 
			this.nsAnimationView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nsAnimationView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.nsAnimationView1.Length = 80;
			this.nsAnimationView1.Location = new System.Drawing.Point(12, 12);
			this.nsAnimationView1.MarkerPosition = 4F;
			this.nsAnimationView1.Name = "nsAnimationView1";
			this.nsAnimationView1.PointKeys = null;
			this.nsAnimationView1.Size = new System.Drawing.Size(414, 143);
			this.nsAnimationView1.TabIndex = 0;
			this.nsAnimationView1.Text = "nsAnimationView1";
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(488, 365);
			this.Controls.Add(this.nsAnimationView1);
			this.Name = "TestForm";
			this.Text = "TestForm";
			this.Load += new System.EventHandler(this.TestForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.NSAnimationView nsAnimationView1;

	}
}