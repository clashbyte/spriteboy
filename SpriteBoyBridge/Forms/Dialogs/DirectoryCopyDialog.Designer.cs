namespace SpriteBoy.Forms.Dialogs {
	partial class DirectoryCopyDialog {
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
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.fileNameLabel = new SpriteBoy.Controls.NSLabel();
			this.copyProgress = new SpriteBoy.Controls.NSProgressBar();
			this.label = new SpriteBoy.Controls.NSLabel();
			this.cancelButton = new SpriteBoy.Controls.NSButton();
			this.nsSeperator1 = new SpriteBoy.Controls.NSSeperator();
			this.stateTimer = new System.Windows.Forms.Timer(this.components);
			this.buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonPanel
			// 
			this.buttonPanel.Controls.Add(this.cancelButton);
			this.buttonPanel.Controls.Add(this.nsSeperator1);
			this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonPanel.Location = new System.Drawing.Point(0, 88);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(384, 50);
			this.buttonPanel.TabIndex = 4;
			// 
			// fileNameLabel
			// 
			this.fileNameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.fileNameLabel.ForeColor = System.Drawing.Color.White;
			this.fileNameLabel.Location = new System.Drawing.Point(8, 67);
			this.fileNameLabel.Name = "fileNameLabel";
			this.fileNameLabel.Size = new System.Drawing.Size(364, 16);
			this.fileNameLabel.TabIndex = 7;
			this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// copyProgress
			// 
			this.copyProgress.Location = new System.Drawing.Point(8, 37);
			this.copyProgress.Maximum = 100;
			this.copyProgress.Minimum = 0;
			this.copyProgress.Name = "copyProgress";
			this.copyProgress.Size = new System.Drawing.Size(368, 24);
			this.copyProgress.TabIndex = 6;
			this.copyProgress.Value = 50;
			// 
			// label
			// 
			this.label.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.label.ForeColor = System.Drawing.Color.White;
			this.label.Location = new System.Drawing.Point(8, 12);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(364, 27);
			this.label.TabIndex = 5;
			this.label.Text = "Copying files...";
			this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(276, 19);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(100, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// nsSeperator1
			// 
			this.nsSeperator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nsSeperator1.Location = new System.Drawing.Point(8, 3);
			this.nsSeperator1.Name = "nsSeperator1";
			this.nsSeperator1.Size = new System.Drawing.Size(368, 10);
			this.nsSeperator1.TabIndex = 0;
			this.nsSeperator1.Text = "nsSeperator1";
			// 
			// stateTimer
			// 
			this.stateTimer.Interval = 25;
			this.stateTimer.Tick += new System.EventHandler(this.stateTimer_Tick);
			// 
			// DirectoryCopyDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(384, 138);
			this.Controls.Add(this.fileNameLabel);
			this.Controls.Add(this.copyProgress);
			this.Controls.Add(this.label);
			this.Controls.Add(this.buttonPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DirectoryCopyDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Copy directory";
			this.buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.NSLabel label;
		private System.Windows.Forms.Panel buttonPanel;
		private Controls.NSButton cancelButton;
		private Controls.NSSeperator nsSeperator1;
		private Controls.NSProgressBar copyProgress;
		private Controls.NSLabel fileNameLabel;
		private System.Windows.Forms.Timer stateTimer;

	}
}