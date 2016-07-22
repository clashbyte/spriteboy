namespace SpriteBoy.Forms.Specialized {
	partial class SplashForm {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
			this.checkTimer = new System.Windows.Forms.Timer(this.components);
			this.stateLabel = new SpriteBoy.Controls.NSLabel();
			this.SuspendLayout();
			// 
			// checkTimer
			// 
			this.checkTimer.Interval = 10;
			this.checkTimer.Tick += new System.EventHandler(this.checkTimer_Tick);
			// 
			// stateLabel
			// 
			this.stateLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.stateLabel.ForeColor = System.Drawing.Color.White;
			this.stateLabel.Location = new System.Drawing.Point(12, 200);
			this.stateLabel.Name = "stateLabel";
			this.stateLabel.Size = new System.Drawing.Size(226, 38);
			this.stateLabel.TabIndex = 0;
			this.stateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SplashForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(250, 250);
			this.ControlBox = false;
			this.Controls.Add(this.stateLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer checkTimer;
		private Controls.NSLabel stateLabel;
	}
}