namespace SpriteBoy.Forms.Dialogs {
	partial class MessageDialog {
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
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.nsSeperator1 = new SpriteBoy.Controls.NSSeperator();
			this.buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonPanel
			// 
			this.buttonPanel.Controls.Add(this.nsSeperator1);
			this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonPanel.Location = new System.Drawing.Point(0, 161);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(434, 50);
			this.buttonPanel.TabIndex = 1;
			// 
			// nsSeperator1
			// 
			this.nsSeperator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nsSeperator1.Location = new System.Drawing.Point(8, 3);
			this.nsSeperator1.Name = "nsSeperator1";
			this.nsSeperator1.Size = new System.Drawing.Size(418, 10);
			this.nsSeperator1.TabIndex = 0;
			this.nsSeperator1.Text = "nsSeperator1";
			// 
			// MessageDialog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(434, 211);
			this.Controls.Add(this.buttonPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MessageDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MessageDialog";
			this.buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.NSSeperator nsSeperator1;
		private System.Windows.Forms.Panel buttonPanel;
	}
}