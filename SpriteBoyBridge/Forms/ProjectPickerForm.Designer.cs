namespace SpriteBoy.Forms {
	partial class ProjectPickerForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectPickerForm));
			this.nsUpButton = new SpriteBoy.Controls.NSIconicButton();
			this.clearButton = new SpriteBoy.Controls.NSButton();
			this.fileLabel = new SpriteBoy.Controls.NSTextBox();
			this.submitButton = new SpriteBoy.Controls.NSButton();
			this.nsSeperator1 = new SpriteBoy.Controls.NSSeperator();
			this.cancelButton = new SpriteBoy.Controls.NSButton();
			this.inspector = new SpriteBoy.Controls.NSDirectoryInspector();
			this.nsLabel1 = new SpriteBoy.Controls.NSLabel();
			this.SuspendLayout();
			// 
			// nsUpButton
			// 
			resources.ApplyResources(this.nsUpButton, "nsUpButton");
			this.nsUpButton.Corners.BottomLeft = false;
			this.nsUpButton.Corners.BottomRight = false;
			this.nsUpButton.Corners.TopLeft = false;
			this.nsUpButton.Corners.TopRight = true;
			this.nsUpButton.IconImage = ((System.Drawing.Image)(resources.GetObject("nsUpButton.IconImage")));
			this.nsUpButton.IconSize = new System.Drawing.Size(14, 14);
			this.nsUpButton.Large = false;
			this.nsUpButton.Name = "nsUpButton";
			this.nsUpButton.Click += new System.EventHandler(this.nsUpButton_Click);
			// 
			// clearButton
			// 
			resources.ApplyResources(this.clearButton, "clearButton");
			this.clearButton.Name = "clearButton";
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// fileLabel
			// 
			resources.ApplyResources(this.fileLabel, "fileLabel");
			this.fileLabel.Corners.BottomLeft = false;
			this.fileLabel.Corners.BottomRight = false;
			this.fileLabel.Corners.TopLeft = true;
			this.fileLabel.Corners.TopRight = false;
			this.fileLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.fileLabel.MaxLength = 32767;
			this.fileLabel.Multiline = false;
			this.fileLabel.Name = "fileLabel";
			this.fileLabel.ReadOnly = false;
			this.fileLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.fileLabel.UseSystemPasswordChar = false;
			// 
			// submitButton
			// 
			resources.ApplyResources(this.submitButton, "submitButton");
			this.submitButton.Name = "submitButton";
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// nsSeperator1
			// 
			resources.ApplyResources(this.nsSeperator1, "nsSeperator1");
			this.nsSeperator1.Name = "nsSeperator1";
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// inspector
			// 
			resources.ApplyResources(this.inspector, "inspector");
			this.inspector.Name = "inspector";
			this.inspector.Offset = 0;
			this.inspector.SelectedEntry = null;
			this.inspector.DoubleClick += new System.EventHandler(this.submitButton_Click);
			this.inspector.MouseDown += new System.Windows.Forms.MouseEventHandler(this.inspector_MouseDown);
			// 
			// nsLabel1
			// 
			resources.ApplyResources(this.nsLabel1, "nsLabel1");
			this.nsLabel1.ForeColor = System.Drawing.Color.White;
			this.nsLabel1.Name = "nsLabel1";
			this.nsLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ProjectPickerForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ControlBox = false;
			this.Controls.Add(this.nsUpButton);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.fileLabel);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.nsSeperator1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.inspector);
			this.Controls.Add(this.nsLabel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectPickerForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.NSLabel nsLabel1;
		private Controls.NSDirectoryInspector inspector;
		private Controls.NSTextBox fileLabel;
		private Controls.NSButton cancelButton;
		private Controls.NSSeperator nsSeperator1;
		private Controls.NSButton submitButton;
		private Controls.NSButton clearButton;
		private Controls.NSIconicButton nsUpButton;
	}
}