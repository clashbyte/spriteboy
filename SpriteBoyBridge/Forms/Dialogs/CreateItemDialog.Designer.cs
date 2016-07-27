namespace SpriteBoy.Forms.Dialogs {
	partial class CreateItemDialog {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateItemDialog));
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.createButton = new SpriteBoy.Controls.NSButton();
			this.cancelButton = new SpriteBoy.Controls.NSButton();
			this.nsSeperator1 = new SpriteBoy.Controls.NSSeperator();
			this.nameBox = new SpriteBoy.Controls.NSTextBox();
			this.label = new SpriteBoy.Controls.NSLabel();
			this.buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonPanel
			// 
			this.buttonPanel.Controls.Add(this.createButton);
			this.buttonPanel.Controls.Add(this.cancelButton);
			this.buttonPanel.Controls.Add(this.nsSeperator1);
			resources.ApplyResources(this.buttonPanel, "buttonPanel");
			this.buttonPanel.Name = "buttonPanel";
			// 
			// createButton
			// 
			resources.ApplyResources(this.createButton, "createButton");
			this.createButton.Name = "createButton";
			this.createButton.Click += new System.EventHandler(this.createButton_Click);
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// nsSeperator1
			// 
			resources.ApplyResources(this.nsSeperator1, "nsSeperator1");
			this.nsSeperator1.Name = "nsSeperator1";
			// 
			// nameBox
			// 
			this.nameBox.Corners.BottomLeft = true;
			this.nameBox.Corners.BottomRight = true;
			this.nameBox.Corners.TopLeft = true;
			this.nameBox.Corners.TopRight = true;
			this.nameBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			resources.ApplyResources(this.nameBox, "nameBox");
			this.nameBox.MaxLength = 32767;
			this.nameBox.Multiline = false;
			this.nameBox.Name = "nameBox";
			this.nameBox.ReadOnly = false;
			this.nameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.nameBox.UseSystemPasswordChar = false;
			this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
			// 
			// label
			// 
			resources.ApplyResources(this.label, "label");
			this.label.ForeColor = System.Drawing.Color.White;
			this.label.Name = "label";
			this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CreateItemDialog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.label);
			this.Controls.Add(this.buttonPanel);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CreateItemDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateItemDialog_FormClosed);
			this.buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel buttonPanel;
		private Controls.NSSeperator nsSeperator1;
		private Controls.NSLabel label;
		private Controls.NSButton createButton;
		private Controls.NSButton cancelButton;
		private Controls.NSTextBox nameBox;
	}
}