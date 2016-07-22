namespace SpriteBoy.Forms.Editors {
	partial class SkyboxForm {
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
			SpriteBoy.Controls.NSLabel nsLabel1;
			SpriteBoy.Controls.NSLabel nsLabel2;
			SpriteBoy.Controls.NSLabel nsLabel3;
			SpriteBoy.Controls.NSLabel nsLabel4;
			SpriteBoy.Controls.NSLabel nsLabel5;
			SpriteBoy.Controls.NSLabel nsLabel6;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkyboxForm));
			this.nsTabControl1 = new SpriteBoy.Controls.NSTabControl();
			this.texturesTab = new System.Windows.Forms.TabPage();
			this.backSkyTexture = new SpriteBoy.Controls.NSFileDropControl();
			this.frontSkyTexture = new SpriteBoy.Controls.NSFileDropControl();
			this.bottomSkyTexture = new SpriteBoy.Controls.NSFileDropControl();
			this.topSkyTexture = new SpriteBoy.Controls.NSFileDropControl();
			this.leftSkyTexture = new SpriteBoy.Controls.NSFileDropControl();
			this.rightSkyTexture = new SpriteBoy.Controls.NSFileDropControl();
			this.lightingTab = new System.Windows.Forms.TabPage();
			this.nsLabel8 = new SpriteBoy.Controls.NSLabel();
			this.weatherTab = new System.Windows.Forms.TabPage();
			this.nsLabel7 = new SpriteBoy.Controls.NSLabel();
			this.toolPanel = new System.Windows.Forms.Panel();
			this.toolLook = new SpriteBoy.Controls.NSRadioIconicButton();
			this.utilPanel = new System.Windows.Forms.Panel();
			this.skyGizmoButton = new SpriteBoy.Controls.NSCheckboxIconicButton();
			nsLabel1 = new SpriteBoy.Controls.NSLabel();
			nsLabel2 = new SpriteBoy.Controls.NSLabel();
			nsLabel3 = new SpriteBoy.Controls.NSLabel();
			nsLabel4 = new SpriteBoy.Controls.NSLabel();
			nsLabel5 = new SpriteBoy.Controls.NSLabel();
			nsLabel6 = new SpriteBoy.Controls.NSLabel();
			this.nsTabControl1.SuspendLayout();
			this.texturesTab.SuspendLayout();
			this.lightingTab.SuspendLayout();
			this.weatherTab.SuspendLayout();
			this.toolPanel.SuspendLayout();
			this.utilPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// nsLabel1
			// 
			nsLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel1.ForeColor = System.Drawing.Color.White;
			nsLabel1.Location = new System.Drawing.Point(6, 3);
			nsLabel1.Name = "nsLabel1";
			nsLabel1.Size = new System.Drawing.Size(90, 20);
			nsLabel1.TabIndex = 0;
			nsLabel1.Text = "Right (X+)";
			nsLabel1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel2
			// 
			nsLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel2.ForeColor = System.Drawing.Color.White;
			nsLabel2.Location = new System.Drawing.Point(102, 3);
			nsLabel2.Name = "nsLabel2";
			nsLabel2.Size = new System.Drawing.Size(90, 20);
			nsLabel2.TabIndex = 2;
			nsLabel2.Text = "Left (X-)";
			nsLabel2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel3
			// 
			nsLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel3.ForeColor = System.Drawing.Color.White;
			nsLabel3.Location = new System.Drawing.Point(198, 3);
			nsLabel3.Name = "nsLabel3";
			nsLabel3.Size = new System.Drawing.Size(90, 20);
			nsLabel3.TabIndex = 4;
			nsLabel3.Text = "Top (Y+)";
			nsLabel3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel4
			// 
			nsLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel4.ForeColor = System.Drawing.Color.White;
			nsLabel4.Location = new System.Drawing.Point(294, 3);
			nsLabel4.Name = "nsLabel4";
			nsLabel4.Size = new System.Drawing.Size(90, 20);
			nsLabel4.TabIndex = 6;
			nsLabel4.Text = "Bottom (Y-)";
			nsLabel4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel5
			// 
			nsLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel5.ForeColor = System.Drawing.Color.White;
			nsLabel5.Location = new System.Drawing.Point(390, 3);
			nsLabel5.Name = "nsLabel5";
			nsLabel5.Size = new System.Drawing.Size(90, 20);
			nsLabel5.TabIndex = 8;
			nsLabel5.Text = "Front (Z+)";
			nsLabel5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel6
			// 
			nsLabel6.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel6.ForeColor = System.Drawing.Color.White;
			nsLabel6.Location = new System.Drawing.Point(486, 3);
			nsLabel6.Name = "nsLabel6";
			nsLabel6.Size = new System.Drawing.Size(90, 20);
			nsLabel6.TabIndex = 10;
			nsLabel6.Text = "Back (Z-)";
			nsLabel6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsTabControl1
			// 
			this.nsTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.nsTabControl1.Controls.Add(this.texturesTab);
			this.nsTabControl1.Controls.Add(this.lightingTab);
			this.nsTabControl1.Controls.Add(this.weatherTab);
			this.nsTabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.nsTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.nsTabControl1.ItemSize = new System.Drawing.Size(28, 115);
			this.nsTabControl1.Location = new System.Drawing.Point(0, 346);
			this.nsTabControl1.Multiline = true;
			this.nsTabControl1.Name = "nsTabControl1";
			this.nsTabControl1.SelectedIndex = 0;
			this.nsTabControl1.Size = new System.Drawing.Size(771, 150);
			this.nsTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.nsTabControl1.TabIndex = 1;
			// 
			// texturesTab
			// 
			this.texturesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.texturesTab.Controls.Add(this.backSkyTexture);
			this.texturesTab.Controls.Add(nsLabel6);
			this.texturesTab.Controls.Add(this.frontSkyTexture);
			this.texturesTab.Controls.Add(nsLabel5);
			this.texturesTab.Controls.Add(this.bottomSkyTexture);
			this.texturesTab.Controls.Add(nsLabel4);
			this.texturesTab.Controls.Add(this.topSkyTexture);
			this.texturesTab.Controls.Add(nsLabel3);
			this.texturesTab.Controls.Add(this.leftSkyTexture);
			this.texturesTab.Controls.Add(nsLabel2);
			this.texturesTab.Controls.Add(this.rightSkyTexture);
			this.texturesTab.Controls.Add(nsLabel1);
			this.texturesTab.Location = new System.Drawing.Point(119, 4);
			this.texturesTab.Name = "texturesTab";
			this.texturesTab.Size = new System.Drawing.Size(648, 142);
			this.texturesTab.TabIndex = 2;
			this.texturesTab.Text = "Textures";
			// 
			// backSkyTexture
			// 
			this.backSkyTexture.AllowDrop = true;
			this.backSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.backSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.backSkyTexture.File = null;
			this.backSkyTexture.Font = new System.Drawing.Font("Tahoma", 8F);
			this.backSkyTexture.Location = new System.Drawing.Point(486, 29);
			this.backSkyTexture.Name = "backSkyTexture";
			this.backSkyTexture.Size = new System.Drawing.Size(90, 105);
			this.backSkyTexture.TabIndex = 11;
			this.backSkyTexture.Text = "backSkyTexture";
			this.backSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// frontSkyTexture
			// 
			this.frontSkyTexture.AllowDrop = true;
			this.frontSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.frontSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.frontSkyTexture.File = null;
			this.frontSkyTexture.Font = new System.Drawing.Font("Tahoma", 8F);
			this.frontSkyTexture.Location = new System.Drawing.Point(390, 29);
			this.frontSkyTexture.Name = "frontSkyTexture";
			this.frontSkyTexture.Size = new System.Drawing.Size(90, 105);
			this.frontSkyTexture.TabIndex = 9;
			this.frontSkyTexture.Text = "frontSkyTexture";
			this.frontSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// bottomSkyTexture
			// 
			this.bottomSkyTexture.AllowDrop = true;
			this.bottomSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.bottomSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.bottomSkyTexture.File = null;
			this.bottomSkyTexture.Font = new System.Drawing.Font("Tahoma", 8F);
			this.bottomSkyTexture.Location = new System.Drawing.Point(294, 29);
			this.bottomSkyTexture.Name = "bottomSkyTexture";
			this.bottomSkyTexture.Size = new System.Drawing.Size(90, 105);
			this.bottomSkyTexture.TabIndex = 7;
			this.bottomSkyTexture.Text = "bottomSkyTexture";
			this.bottomSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// topSkyTexture
			// 
			this.topSkyTexture.AllowDrop = true;
			this.topSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.topSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.topSkyTexture.File = null;
			this.topSkyTexture.Font = new System.Drawing.Font("Tahoma", 8F);
			this.topSkyTexture.Location = new System.Drawing.Point(198, 29);
			this.topSkyTexture.Name = "topSkyTexture";
			this.topSkyTexture.Size = new System.Drawing.Size(90, 105);
			this.topSkyTexture.TabIndex = 5;
			this.topSkyTexture.Text = "topSkyTexture";
			this.topSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// leftSkyTexture
			// 
			this.leftSkyTexture.AllowDrop = true;
			this.leftSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.leftSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.leftSkyTexture.File = null;
			this.leftSkyTexture.Font = new System.Drawing.Font("Tahoma", 8F);
			this.leftSkyTexture.Location = new System.Drawing.Point(102, 29);
			this.leftSkyTexture.Name = "leftSkyTexture";
			this.leftSkyTexture.Size = new System.Drawing.Size(90, 105);
			this.leftSkyTexture.TabIndex = 3;
			this.leftSkyTexture.Text = "leftSkyTexture";
			this.leftSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// rightSkyTexture
			// 
			this.rightSkyTexture.AllowDrop = true;
			this.rightSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.rightSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.rightSkyTexture.File = null;
			this.rightSkyTexture.Font = new System.Drawing.Font("Tahoma", 8F);
			this.rightSkyTexture.Location = new System.Drawing.Point(6, 29);
			this.rightSkyTexture.Name = "rightSkyTexture";
			this.rightSkyTexture.Size = new System.Drawing.Size(90, 105);
			this.rightSkyTexture.TabIndex = 1;
			this.rightSkyTexture.Text = "rightSkyTexture";
			this.rightSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// lightingTab
			// 
			this.lightingTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.lightingTab.Controls.Add(this.nsLabel8);
			this.lightingTab.Location = new System.Drawing.Point(119, 4);
			this.lightingTab.Name = "lightingTab";
			this.lightingTab.Padding = new System.Windows.Forms.Padding(3);
			this.lightingTab.Size = new System.Drawing.Size(648, 142);
			this.lightingTab.TabIndex = 0;
			this.lightingTab.Text = "Lighting";
			// 
			// nsLabel8
			// 
			this.nsLabel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nsLabel8.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.nsLabel8.ForeColor = System.Drawing.Color.Gray;
			this.nsLabel8.Location = new System.Drawing.Point(3, 3);
			this.nsLabel8.Name = "nsLabel8";
			this.nsLabel8.Size = new System.Drawing.Size(642, 136);
			this.nsLabel8.TabIndex = 1;
			this.nsLabel8.Text = "Not implemented yet";
			this.nsLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// weatherTab
			// 
			this.weatherTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.weatherTab.Controls.Add(this.nsLabel7);
			this.weatherTab.Location = new System.Drawing.Point(119, 4);
			this.weatherTab.Name = "weatherTab";
			this.weatherTab.Padding = new System.Windows.Forms.Padding(3);
			this.weatherTab.Size = new System.Drawing.Size(648, 142);
			this.weatherTab.TabIndex = 1;
			this.weatherTab.Text = "Weather";
			// 
			// nsLabel7
			// 
			this.nsLabel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nsLabel7.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.nsLabel7.ForeColor = System.Drawing.Color.Gray;
			this.nsLabel7.Location = new System.Drawing.Point(3, 3);
			this.nsLabel7.Name = "nsLabel7";
			this.nsLabel7.Size = new System.Drawing.Size(642, 136);
			this.nsLabel7.TabIndex = 0;
			this.nsLabel7.Text = "Not implemented yet";
			this.nsLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// toolPanel
			// 
			this.toolPanel.Controls.Add(this.toolLook);
			this.toolPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.toolPanel.Location = new System.Drawing.Point(0, 25);
			this.toolPanel.Name = "toolPanel";
			this.toolPanel.Size = new System.Drawing.Size(45, 321);
			this.toolPanel.TabIndex = 3;
			// 
			// toolLook
			// 
			this.toolLook.Checked = true;
			this.toolLook.Corners.BottomLeft = true;
			this.toolLook.Corners.BottomRight = true;
			this.toolLook.Corners.TopLeft = true;
			this.toolLook.Corners.TopRight = true;
			this.toolLook.IconImage = ((System.Drawing.Image)(resources.GetObject("toolLook.IconImage")));
			this.toolLook.IconSize = new System.Drawing.Size(26, 26);
			this.toolLook.Large = true;
			this.toolLook.Location = new System.Drawing.Point(0, 0);
			this.toolLook.Name = "toolLook";
			this.toolLook.Size = new System.Drawing.Size(42, 42);
			this.toolLook.TabIndex = 0;
			// 
			// utilPanel
			// 
			this.utilPanel.Controls.Add(this.skyGizmoButton);
			this.utilPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.utilPanel.Location = new System.Drawing.Point(0, 0);
			this.utilPanel.Name = "utilPanel";
			this.utilPanel.Size = new System.Drawing.Size(771, 25);
			this.utilPanel.TabIndex = 4;
			// 
			// skyGizmoButton
			// 
			this.skyGizmoButton.Checked = true;
			this.skyGizmoButton.Corners.BottomLeft = true;
			this.skyGizmoButton.Corners.BottomRight = true;
			this.skyGizmoButton.Corners.TopLeft = true;
			this.skyGizmoButton.Corners.TopRight = true;
			this.skyGizmoButton.IconImage = ((System.Drawing.Image)(resources.GetObject("skyGizmoButton.IconImage")));
			this.skyGizmoButton.IconSize = new System.Drawing.Size(14, 14);
			this.skyGizmoButton.Large = false;
			this.skyGizmoButton.Location = new System.Drawing.Point(44, 1);
			this.skyGizmoButton.Name = "skyGizmoButton";
			this.skyGizmoButton.Size = new System.Drawing.Size(23, 23);
			this.skyGizmoButton.TabIndex = 1;
			this.skyGizmoButton.CheckedChanged += new SpriteBoy.Controls.NSCheckboxIconicButton.CheckedChangedEventHandler(this.skyGizmoButton_CheckedChanged);
			// 
			// SkyboxForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(771, 496);
			this.Controls.Add(this.toolPanel);
			this.Controls.Add(this.nsTabControl1);
			this.Controls.Add(this.utilPanel);
			this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
			this.Name = "SkyboxForm";
			this.Text = "Skybox";
			this.Resize += new System.EventHandler(this.SkyboxForm_Resize);
			this.nsTabControl1.ResumeLayout(false);
			this.texturesTab.ResumeLayout(false);
			this.lightingTab.ResumeLayout(false);
			this.weatherTab.ResumeLayout(false);
			this.toolPanel.ResumeLayout(false);
			this.utilPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.NSTabControl nsTabControl1;
		private System.Windows.Forms.TabPage lightingTab;
		private System.Windows.Forms.TabPage weatherTab;
		private System.Windows.Forms.TabPage texturesTab;
		private System.Windows.Forms.Panel toolPanel;
		private Controls.NSRadioIconicButton toolLook;
		private System.Windows.Forms.Panel utilPanel;
		public Controls.NSCheckboxIconicButton skyGizmoButton;
		private Controls.NSLabel nsLabel7;
		private Controls.NSLabel nsLabel8;
		public Controls.NSFileDropControl rightSkyTexture;
		public Controls.NSFileDropControl backSkyTexture;
		public Controls.NSFileDropControl frontSkyTexture;
		public Controls.NSFileDropControl bottomSkyTexture;
		public Controls.NSFileDropControl topSkyTexture;
		public Controls.NSFileDropControl leftSkyTexture;
	}
}