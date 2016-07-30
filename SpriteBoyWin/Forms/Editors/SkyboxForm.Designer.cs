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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkyboxForm));
			SpriteBoy.Controls.NSLabel nsLabel2;
			SpriteBoy.Controls.NSLabel nsLabel3;
			SpriteBoy.Controls.NSLabel nsLabel4;
			SpriteBoy.Controls.NSLabel nsLabel5;
			SpriteBoy.Controls.NSLabel nsLabel6;
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
			resources.ApplyResources(nsLabel1, "nsLabel1");
			nsLabel1.ForeColor = System.Drawing.Color.White;
			nsLabel1.Name = "nsLabel1";
			nsLabel1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel2
			// 
			resources.ApplyResources(nsLabel2, "nsLabel2");
			nsLabel2.ForeColor = System.Drawing.Color.White;
			nsLabel2.Name = "nsLabel2";
			nsLabel2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel3
			// 
			resources.ApplyResources(nsLabel3, "nsLabel3");
			nsLabel3.ForeColor = System.Drawing.Color.White;
			nsLabel3.Name = "nsLabel3";
			nsLabel3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel4
			// 
			resources.ApplyResources(nsLabel4, "nsLabel4");
			nsLabel4.ForeColor = System.Drawing.Color.White;
			nsLabel4.Name = "nsLabel4";
			nsLabel4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel5
			// 
			resources.ApplyResources(nsLabel5, "nsLabel5");
			nsLabel5.ForeColor = System.Drawing.Color.White;
			nsLabel5.Name = "nsLabel5";
			nsLabel5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel6
			// 
			resources.ApplyResources(nsLabel6, "nsLabel6");
			nsLabel6.ForeColor = System.Drawing.Color.White;
			nsLabel6.Name = "nsLabel6";
			nsLabel6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsTabControl1
			// 
			resources.ApplyResources(this.nsTabControl1, "nsTabControl1");
			this.nsTabControl1.Controls.Add(this.texturesTab);
			this.nsTabControl1.Controls.Add(this.lightingTab);
			this.nsTabControl1.Controls.Add(this.weatherTab);
			this.nsTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.nsTabControl1.Multiline = true;
			this.nsTabControl1.Name = "nsTabControl1";
			this.nsTabControl1.SelectedIndex = 0;
			this.nsTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			// 
			// texturesTab
			// 
			resources.ApplyResources(this.texturesTab, "texturesTab");
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
			this.texturesTab.Name = "texturesTab";
			// 
			// backSkyTexture
			// 
			resources.ApplyResources(this.backSkyTexture, "backSkyTexture");
			this.backSkyTexture.AllowDrop = true;
			this.backSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.backSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.backSkyTexture.File = null;
			this.backSkyTexture.Name = "backSkyTexture";
			this.backSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// frontSkyTexture
			// 
			resources.ApplyResources(this.frontSkyTexture, "frontSkyTexture");
			this.frontSkyTexture.AllowDrop = true;
			this.frontSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.frontSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.frontSkyTexture.File = null;
			this.frontSkyTexture.Name = "frontSkyTexture";
			this.frontSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// bottomSkyTexture
			// 
			resources.ApplyResources(this.bottomSkyTexture, "bottomSkyTexture");
			this.bottomSkyTexture.AllowDrop = true;
			this.bottomSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.bottomSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.bottomSkyTexture.File = null;
			this.bottomSkyTexture.Name = "bottomSkyTexture";
			this.bottomSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// topSkyTexture
			// 
			resources.ApplyResources(this.topSkyTexture, "topSkyTexture");
			this.topSkyTexture.AllowDrop = true;
			this.topSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.topSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.topSkyTexture.File = null;
			this.topSkyTexture.Name = "topSkyTexture";
			this.topSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// leftSkyTexture
			// 
			resources.ApplyResources(this.leftSkyTexture, "leftSkyTexture");
			this.leftSkyTexture.AllowDrop = true;
			this.leftSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.leftSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.leftSkyTexture.File = null;
			this.leftSkyTexture.Name = "leftSkyTexture";
			this.leftSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// rightSkyTexture
			// 
			resources.ApplyResources(this.rightSkyTexture, "rightSkyTexture");
			this.rightSkyTexture.AllowDrop = true;
			this.rightSkyTexture.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.rightSkyTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.rightSkyTexture.File = null;
			this.rightSkyTexture.Name = "rightSkyTexture";
			this.rightSkyTexture.FileChanged += new System.EventHandler(this.skyTexture_FileChanged);
			// 
			// lightingTab
			// 
			resources.ApplyResources(this.lightingTab, "lightingTab");
			this.lightingTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.lightingTab.Controls.Add(this.nsLabel8);
			this.lightingTab.Name = "lightingTab";
			// 
			// nsLabel8
			// 
			resources.ApplyResources(this.nsLabel8, "nsLabel8");
			this.nsLabel8.ForeColor = System.Drawing.Color.Gray;
			this.nsLabel8.Name = "nsLabel8";
			this.nsLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// weatherTab
			// 
			resources.ApplyResources(this.weatherTab, "weatherTab");
			this.weatherTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.weatherTab.Controls.Add(this.nsLabel7);
			this.weatherTab.Name = "weatherTab";
			// 
			// nsLabel7
			// 
			resources.ApplyResources(this.nsLabel7, "nsLabel7");
			this.nsLabel7.ForeColor = System.Drawing.Color.Gray;
			this.nsLabel7.Name = "nsLabel7";
			this.nsLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// toolPanel
			// 
			resources.ApplyResources(this.toolPanel, "toolPanel");
			this.toolPanel.Controls.Add(this.toolLook);
			this.toolPanel.Name = "toolPanel";
			// 
			// toolLook
			// 
			resources.ApplyResources(this.toolLook, "toolLook");
			this.toolLook.Checked = true;
			this.toolLook.Corners.BottomLeft = true;
			this.toolLook.Corners.BottomRight = true;
			this.toolLook.Corners.TopLeft = true;
			this.toolLook.Corners.TopRight = true;
			this.toolLook.IconImage = ((System.Drawing.Image)(resources.GetObject("toolLook.IconImage")));
			this.toolLook.IconSize = new System.Drawing.Size(26, 26);
			this.toolLook.Large = true;
			this.toolLook.Name = "toolLook";
			// 
			// utilPanel
			// 
			resources.ApplyResources(this.utilPanel, "utilPanel");
			this.utilPanel.Controls.Add(this.skyGizmoButton);
			this.utilPanel.Name = "utilPanel";
			// 
			// skyGizmoButton
			// 
			resources.ApplyResources(this.skyGizmoButton, "skyGizmoButton");
			this.skyGizmoButton.Checked = true;
			this.skyGizmoButton.Corners.BottomLeft = true;
			this.skyGizmoButton.Corners.BottomRight = true;
			this.skyGizmoButton.Corners.TopLeft = true;
			this.skyGizmoButton.Corners.TopRight = true;
			this.skyGizmoButton.IconImage = ((System.Drawing.Image)(resources.GetObject("skyGizmoButton.IconImage")));
			this.skyGizmoButton.IconSize = new System.Drawing.Size(14, 14);
			this.skyGizmoButton.Large = false;
			this.skyGizmoButton.Name = "skyGizmoButton";
			this.skyGizmoButton.CheckedChanged += new SpriteBoy.Controls.NSCheckboxIconicButton.CheckedChangedEventHandler(this.skyGizmoButton_CheckedChanged);
			// 
			// SkyboxForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.Controls.Add(this.toolPanel);
			this.Controls.Add(this.nsTabControl1);
			this.Controls.Add(this.utilPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
			this.Name = "SkyboxForm";
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