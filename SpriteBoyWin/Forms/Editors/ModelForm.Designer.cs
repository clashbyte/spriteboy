﻿namespace SpriteBoy.Forms.Editors {
	partial class ModelForm {
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
			SpriteBoy.Controls.NSLabel nsLabel3;
			SpriteBoy.Controls.NSLabel nsLabel5;
			SpriteBoy.Controls.NSLabel nsLabel4;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelForm));
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader1 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader2 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader3 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader4 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			this.toolPanel = new System.Windows.Forms.Panel();
			this.toolCursor = new SpriteBoy.Controls.NSRadioIconicButton();
			this.utilPanel = new System.Windows.Forms.Panel();
			this.firstPersonButton = new SpriteBoy.Controls.NSCheckboxIconicButton();
			this.cellGizmoButton = new SpriteBoy.Controls.NSCheckboxIconicButton();
			this.propertyTabs = new SpriteBoy.Controls.NSTabControl();
			this.surfacePage = new System.Windows.Forms.TabPage();
			this.surfacesList = new SpriteBoy.Controls.NSListView();
			this.surfaceTexPanel = new System.Windows.Forms.Panel();
			this.surfaceBlending = new SpriteBoy.Controls.NSComboBox();
			this.surfaceUnlit = new SpriteBoy.Controls.NSOnOffBox();
			this.surfaceColorButton = new SpriteBoy.Controls.NSColorPickerButton();
			this.textureFile = new SpriteBoy.Controls.NSFileDropControl();
			this.animationPage = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.nsAnimationView1 = new SpriteBoy.Controls.NSAnimationView();
			this.panel3 = new System.Windows.Forms.Panel();
			this.nsIconicButton2 = new SpriteBoy.Controls.NSIconicButton();
			this.nsIconicButton1 = new SpriteBoy.Controls.NSIconicButton();
			this.animationStop = new SpriteBoy.Controls.NSIconicButton();
			this.animationPlay = new SpriteBoy.Controls.NSCheckboxIconicButton();
			this.nsListView1 = new SpriteBoy.Controls.NSListView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.jointsPage = new System.Windows.Forms.TabPage();
			nsLabel1 = new SpriteBoy.Controls.NSLabel();
			nsLabel3 = new SpriteBoy.Controls.NSLabel();
			nsLabel5 = new SpriteBoy.Controls.NSLabel();
			nsLabel4 = new SpriteBoy.Controls.NSLabel();
			this.toolPanel.SuspendLayout();
			this.utilPanel.SuspendLayout();
			this.propertyTabs.SuspendLayout();
			this.surfacePage.SuspendLayout();
			this.surfaceTexPanel.SuspendLayout();
			this.animationPage.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// nsLabel1
			// 
			nsLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel1.ForeColor = System.Drawing.Color.White;
			nsLabel1.Location = new System.Drawing.Point(6, 0);
			nsLabel1.Name = "nsLabel1";
			nsLabel1.Size = new System.Drawing.Size(100, 20);
			nsLabel1.TabIndex = 1;
			nsLabel1.Text = "Texture";
			nsLabel1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel3
			// 
			nsLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel3.ForeColor = System.Drawing.Color.White;
			nsLabel3.Location = new System.Drawing.Point(119, 51);
			nsLabel3.Name = "nsLabel3";
			nsLabel3.Size = new System.Drawing.Size(57, 25);
			nsLabel3.TabIndex = 4;
			nsLabel3.Text = "Color:";
			nsLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nsLabel5
			// 
			nsLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel5.ForeColor = System.Drawing.Color.White;
			nsLabel5.Location = new System.Drawing.Point(119, 82);
			nsLabel5.Name = "nsLabel5";
			nsLabel5.Size = new System.Drawing.Size(57, 24);
			nsLabel5.TabIndex = 8;
			nsLabel5.Text = "Unlit:";
			nsLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nsLabel4
			// 
			nsLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel4.ForeColor = System.Drawing.Color.White;
			nsLabel4.Location = new System.Drawing.Point(119, 0);
			nsLabel4.Name = "nsLabel4";
			nsLabel4.Size = new System.Drawing.Size(121, 20);
			nsLabel4.TabIndex = 9;
			nsLabel4.Text = "Blending mode:";
			nsLabel4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// toolPanel
			// 
			this.toolPanel.Controls.Add(this.toolCursor);
			this.toolPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.toolPanel.Location = new System.Drawing.Point(0, 25);
			this.toolPanel.Name = "toolPanel";
			this.toolPanel.Size = new System.Drawing.Size(45, 342);
			this.toolPanel.TabIndex = 5;
			// 
			// toolCursor
			// 
			this.toolCursor.Checked = true;
			this.toolCursor.Corners.BottomLeft = true;
			this.toolCursor.Corners.BottomRight = true;
			this.toolCursor.Corners.TopLeft = true;
			this.toolCursor.Corners.TopRight = true;
			this.toolCursor.IconImage = ((System.Drawing.Image)(resources.GetObject("toolCursor.IconImage")));
			this.toolCursor.IconSize = new System.Drawing.Size(26, 26);
			this.toolCursor.Large = true;
			this.toolCursor.Location = new System.Drawing.Point(0, 0);
			this.toolCursor.Name = "toolCursor";
			this.toolCursor.Size = new System.Drawing.Size(42, 42);
			this.toolCursor.TabIndex = 0;
			// 
			// utilPanel
			// 
			this.utilPanel.Controls.Add(this.firstPersonButton);
			this.utilPanel.Controls.Add(this.cellGizmoButton);
			this.utilPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.utilPanel.Location = new System.Drawing.Point(0, 0);
			this.utilPanel.Name = "utilPanel";
			this.utilPanel.Size = new System.Drawing.Size(726, 25);
			this.utilPanel.TabIndex = 6;
			// 
			// firstPersonButton
			// 
			this.firstPersonButton.Checked = false;
			this.firstPersonButton.Corners.BottomLeft = false;
			this.firstPersonButton.Corners.BottomRight = true;
			this.firstPersonButton.Corners.TopLeft = false;
			this.firstPersonButton.Corners.TopRight = true;
			this.firstPersonButton.IconImage = ((System.Drawing.Image)(resources.GetObject("firstPersonButton.IconImage")));
			this.firstPersonButton.IconSize = new System.Drawing.Size(16, 16);
			this.firstPersonButton.Large = false;
			this.firstPersonButton.Location = new System.Drawing.Point(143, 1);
			this.firstPersonButton.Name = "firstPersonButton";
			this.firstPersonButton.Size = new System.Drawing.Size(100, 23);
			this.firstPersonButton.TabIndex = 3;
			this.firstPersonButton.Text = "From eyes";
			this.firstPersonButton.CheckedChanged += new SpriteBoy.Controls.NSCheckboxIconicButton.CheckedChangedEventHandler(this.firstPersonButton_CheckedChanged);
			// 
			// cellGizmoButton
			// 
			this.cellGizmoButton.Checked = false;
			this.cellGizmoButton.Corners.BottomLeft = true;
			this.cellGizmoButton.Corners.BottomRight = false;
			this.cellGizmoButton.Corners.TopLeft = true;
			this.cellGizmoButton.Corners.TopRight = false;
			this.cellGizmoButton.IconImage = ((System.Drawing.Image)(resources.GetObject("cellGizmoButton.IconImage")));
			this.cellGizmoButton.IconSize = new System.Drawing.Size(14, 14);
			this.cellGizmoButton.Large = false;
			this.cellGizmoButton.Location = new System.Drawing.Point(44, 1);
			this.cellGizmoButton.Name = "cellGizmoButton";
			this.cellGizmoButton.Size = new System.Drawing.Size(100, 23);
			this.cellGizmoButton.TabIndex = 2;
			this.cellGizmoButton.Text = "Cell size";
			this.cellGizmoButton.CheckedChanged += new SpriteBoy.Controls.NSCheckboxIconicButton.CheckedChangedEventHandler(this.cellGizmoButton_CheckedChanged);
			// 
			// propertyTabs
			// 
			this.propertyTabs.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.propertyTabs.Controls.Add(this.surfacePage);
			this.propertyTabs.Controls.Add(this.animationPage);
			this.propertyTabs.Controls.Add(this.jointsPage);
			this.propertyTabs.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.propertyTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.propertyTabs.ItemSize = new System.Drawing.Size(28, 115);
			this.propertyTabs.Location = new System.Drawing.Point(0, 367);
			this.propertyTabs.Multiline = true;
			this.propertyTabs.Name = "propertyTabs";
			this.propertyTabs.SelectedIndex = 0;
			this.propertyTabs.Size = new System.Drawing.Size(726, 150);
			this.propertyTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.propertyTabs.TabIndex = 7;
			// 
			// surfacePage
			// 
			this.surfacePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.surfacePage.Controls.Add(this.surfacesList);
			this.surfacePage.Controls.Add(this.surfaceTexPanel);
			this.surfacePage.Location = new System.Drawing.Point(119, 4);
			this.surfacePage.Name = "surfacePage";
			this.surfacePage.Padding = new System.Windows.Forms.Padding(3);
			this.surfacePage.Size = new System.Drawing.Size(603, 142);
			this.surfacePage.TabIndex = 0;
			this.surfacePage.Text = "Surfaces";
			// 
			// surfacesList
			// 
			nsListViewColumnHeader1.Text = "№";
			nsListViewColumnHeader1.Width = 30;
			nsListViewColumnHeader2.Text = "Texture";
			nsListViewColumnHeader2.Width = 100;
			nsListViewColumnHeader3.Text = "Verts";
			nsListViewColumnHeader3.Width = 50;
			nsListViewColumnHeader4.Text = "Tris";
			nsListViewColumnHeader4.Width = 50;
			this.surfacesList.Columns = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader[] {
        nsListViewColumnHeader1,
        nsListViewColumnHeader2,
        nsListViewColumnHeader3,
        nsListViewColumnHeader4};
			this.surfacesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.surfacesList.Items = new SpriteBoy.Controls.NSListView.NSListViewItem[0];
			this.surfacesList.Location = new System.Drawing.Point(3, 3);
			this.surfacesList.MultiSelect = false;
			this.surfacesList.Name = "surfacesList";
			this.surfacesList.SelectedItem = null;
			this.surfacesList.SelectedItems = new SpriteBoy.Controls.NSListView.NSListViewItem[0];
			this.surfacesList.Size = new System.Drawing.Size(354, 136);
			this.surfacesList.TabIndex = 0;
			this.surfacesList.SelectedItemChanged += new SpriteBoy.Controls.NSListView.SelectedItemChangedEventHandler(this.surfacesList_SelectedItemChanged);
			// 
			// surfaceTexPanel
			// 
			this.surfaceTexPanel.Controls.Add(this.surfaceBlending);
			this.surfaceTexPanel.Controls.Add(nsLabel4);
			this.surfaceTexPanel.Controls.Add(nsLabel5);
			this.surfaceTexPanel.Controls.Add(this.surfaceUnlit);
			this.surfaceTexPanel.Controls.Add(nsLabel3);
			this.surfaceTexPanel.Controls.Add(this.surfaceColorButton);
			this.surfaceTexPanel.Controls.Add(nsLabel1);
			this.surfaceTexPanel.Controls.Add(this.textureFile);
			this.surfaceTexPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.surfaceTexPanel.Location = new System.Drawing.Point(357, 3);
			this.surfaceTexPanel.Name = "surfaceTexPanel";
			this.surfaceTexPanel.Size = new System.Drawing.Size(243, 136);
			this.surfaceTexPanel.TabIndex = 1;
			this.surfaceTexPanel.Visible = false;
			// 
			// surfaceBlending
			// 
			this.surfaceBlending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.surfaceBlending.ForeColor = System.Drawing.Color.White;
			this.surfaceBlending.Items = "Alpha\nBrightness\nAdd\nMultiply\nOpaque";
			this.surfaceBlending.Location = new System.Drawing.Point(118, 22);
			this.surfaceBlending.Name = "surfaceBlending";
			this.surfaceBlending.SelectedIndex = 0;
			this.surfaceBlending.Size = new System.Drawing.Size(120, 23);
			this.surfaceBlending.TabIndex = 10;
			this.surfaceBlending.IndexChanged += new SpriteBoy.Controls.NSComboBox.SelectionChangedEventHandler(this.surfaceBlending_IndexChanged);
			// 
			// surfaceUnlit
			// 
			this.surfaceUnlit.Checked = true;
			this.surfaceUnlit.Location = new System.Drawing.Point(182, 82);
			this.surfaceUnlit.MaximumSize = new System.Drawing.Size(56, 24);
			this.surfaceUnlit.MinimumSize = new System.Drawing.Size(56, 24);
			this.surfaceUnlit.Name = "surfaceUnlit";
			this.surfaceUnlit.Size = new System.Drawing.Size(56, 24);
			this.surfaceUnlit.TabIndex = 7;
			this.surfaceUnlit.Text = "nsOnOffBox1";
			this.surfaceUnlit.CheckedChanged += new SpriteBoy.Controls.NSOnOffBox.CheckedChangedEventHandler(this.surfaceFlags_CheckedChanged);
			// 
			// surfaceColorButton
			// 
			this.surfaceColorButton.Location = new System.Drawing.Point(181, 51);
			this.surfaceColorButton.Name = "surfaceColorButton";
			this.surfaceColorButton.SelectedColor = System.Drawing.Color.White;
			this.surfaceColorButton.Size = new System.Drawing.Size(57, 25);
			this.surfaceColorButton.TabIndex = 3;
			this.surfaceColorButton.Text = "nsColorPickerButton1";
			// 
			// textureFile
			// 
			this.textureFile.AllowDrop = true;
			this.textureFile.AllowedTypes = ".png|.jpg|.jpeg|.gif|.bmp";
			this.textureFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.textureFile.File = null;
			this.textureFile.Font = new System.Drawing.Font("Tahoma", 8F);
			this.textureFile.Location = new System.Drawing.Point(6, 22);
			this.textureFile.Name = "textureFile";
			this.textureFile.Size = new System.Drawing.Size(100, 111);
			this.textureFile.TabIndex = 0;
			this.textureFile.Text = "nsFileDropControl1";
			this.textureFile.FileChanged += new System.EventHandler(this.textureFile_FileChanged);
			// 
			// animationPage
			// 
			this.animationPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.animationPage.Controls.Add(this.panel2);
			this.animationPage.Controls.Add(this.panel1);
			this.animationPage.Location = new System.Drawing.Point(119, 4);
			this.animationPage.Name = "animationPage";
			this.animationPage.Padding = new System.Windows.Forms.Padding(3);
			this.animationPage.Size = new System.Drawing.Size(603, 142);
			this.animationPage.TabIndex = 1;
			this.animationPage.Text = "Animation";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.nsAnimationView1);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.nsListView1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(477, 136);
			this.panel2.TabIndex = 2;
			// 
			// nsAnimationView1
			// 
			this.nsAnimationView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.nsAnimationView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nsAnimationView1.Location = new System.Drawing.Point(0, 0);
			this.nsAnimationView1.Name = "nsAnimationView1";
			this.nsAnimationView1.Size = new System.Drawing.Size(277, 103);
			this.nsAnimationView1.TabIndex = 0;
			this.nsAnimationView1.Text = "nsAnimationView1";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.nsIconicButton2);
			this.panel3.Controls.Add(this.nsIconicButton1);
			this.panel3.Controls.Add(this.animationStop);
			this.panel3.Controls.Add(this.animationPlay);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 103);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(277, 33);
			this.panel3.TabIndex = 2;
			// 
			// nsIconicButton2
			// 
			this.nsIconicButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nsIconicButton2.Corners.BottomLeft = true;
			this.nsIconicButton2.Corners.BottomRight = false;
			this.nsIconicButton2.Corners.TopLeft = true;
			this.nsIconicButton2.Corners.TopRight = false;
			this.nsIconicButton2.IconImage = null;
			this.nsIconicButton2.IconSize = new System.Drawing.Size(0, 0);
			this.nsIconicButton2.Large = false;
			this.nsIconicButton2.Location = new System.Drawing.Point(225, 5);
			this.nsIconicButton2.Name = "nsIconicButton2";
			this.nsIconicButton2.Size = new System.Drawing.Size(25, 25);
			this.nsIconicButton2.TabIndex = 3;
			// 
			// nsIconicButton1
			// 
			this.nsIconicButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nsIconicButton1.Corners.BottomLeft = false;
			this.nsIconicButton1.Corners.BottomRight = true;
			this.nsIconicButton1.Corners.TopLeft = false;
			this.nsIconicButton1.Corners.TopRight = true;
			this.nsIconicButton1.IconImage = null;
			this.nsIconicButton1.IconSize = new System.Drawing.Size(0, 0);
			this.nsIconicButton1.Large = false;
			this.nsIconicButton1.Location = new System.Drawing.Point(249, 5);
			this.nsIconicButton1.Name = "nsIconicButton1";
			this.nsIconicButton1.Size = new System.Drawing.Size(25, 25);
			this.nsIconicButton1.TabIndex = 2;
			// 
			// animationStop
			// 
			this.animationStop.Corners.BottomLeft = false;
			this.animationStop.Corners.BottomRight = true;
			this.animationStop.Corners.TopLeft = false;
			this.animationStop.Corners.TopRight = true;
			this.animationStop.IconImage = null;
			this.animationStop.IconSize = new System.Drawing.Size(0, 0);
			this.animationStop.Large = true;
			this.animationStop.Location = new System.Drawing.Point(37, 3);
			this.animationStop.Name = "animationStop";
			this.animationStop.Size = new System.Drawing.Size(35, 30);
			this.animationStop.TabIndex = 1;
			// 
			// animationPlay
			// 
			this.animationPlay.Checked = false;
			this.animationPlay.Corners.BottomLeft = true;
			this.animationPlay.Corners.BottomRight = false;
			this.animationPlay.Corners.TopLeft = true;
			this.animationPlay.Corners.TopRight = false;
			this.animationPlay.IconImage = null;
			this.animationPlay.IconSize = new System.Drawing.Size(0, 0);
			this.animationPlay.Large = true;
			this.animationPlay.Location = new System.Drawing.Point(3, 3);
			this.animationPlay.Name = "animationPlay";
			this.animationPlay.Size = new System.Drawing.Size(35, 30);
			this.animationPlay.TabIndex = 0;
			// 
			// nsListView1
			// 
			this.nsListView1.Columns = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader[0];
			this.nsListView1.Dock = System.Windows.Forms.DockStyle.Right;
			this.nsListView1.Items = new SpriteBoy.Controls.NSListView.NSListViewItem[0];
			this.nsListView1.Location = new System.Drawing.Point(277, 0);
			this.nsListView1.MultiSelect = true;
			this.nsListView1.Name = "nsListView1";
			this.nsListView1.SelectedItem = null;
			this.nsListView1.SelectedItems = new SpriteBoy.Controls.NSListView.NSListViewItem[0];
			this.nsListView1.Size = new System.Drawing.Size(200, 136);
			this.nsListView1.TabIndex = 1;
			this.nsListView1.Text = "nsListView1";
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(480, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(120, 136);
			this.panel1.TabIndex = 1;
			// 
			// jointsPage
			// 
			this.jointsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.jointsPage.Location = new System.Drawing.Point(119, 4);
			this.jointsPage.Name = "jointsPage";
			this.jointsPage.Size = new System.Drawing.Size(603, 142);
			this.jointsPage.TabIndex = 2;
			this.jointsPage.Text = "Mounts";
			// 
			// ModelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(726, 517);
			this.Controls.Add(this.toolPanel);
			this.Controls.Add(this.utilPanel);
			this.Controls.Add(this.propertyTabs);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
			this.Name = "ModelForm";
			this.Text = "Model";
			this.toolPanel.ResumeLayout(false);
			this.utilPanel.ResumeLayout(false);
			this.propertyTabs.ResumeLayout(false);
			this.surfacePage.ResumeLayout(false);
			this.surfaceTexPanel.ResumeLayout(false);
			this.animationPage.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel toolPanel;
		private Controls.NSRadioIconicButton toolCursor;
		private System.Windows.Forms.Panel utilPanel;
		private Controls.NSTabControl propertyTabs;
		private System.Windows.Forms.TabPage surfacePage;
		private System.Windows.Forms.TabPage animationPage;
		private System.Windows.Forms.TabPage jointsPage;
		public Controls.NSListView surfacesList;
		public System.Windows.Forms.Panel surfaceTexPanel;
		public Controls.NSFileDropControl textureFile;
		public Controls.NSColorPickerButton surfaceColorButton;
		public Controls.NSCheckboxIconicButton cellGizmoButton;
		public Controls.NSCheckboxIconicButton firstPersonButton;
		public Controls.NSOnOffBox surfaceUnlit;
		public Controls.NSComboBox surfaceBlending;
		private System.Windows.Forms.Panel panel2;
		private Controls.NSAnimationView nsAnimationView1;
		private System.Windows.Forms.Panel panel3;
		private Controls.NSListView nsListView1;
		private System.Windows.Forms.Panel panel1;
		private Controls.NSIconicButton animationStop;
		private Controls.NSCheckboxIconicButton animationPlay;
		private Controls.NSIconicButton nsIconicButton2;
		private Controls.NSIconicButton nsIconicButton1;
	}
}