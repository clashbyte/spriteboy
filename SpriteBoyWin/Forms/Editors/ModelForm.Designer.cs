namespace SpriteBoy.Forms.Editors {
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
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader25 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader26 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader27 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader28 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader22 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader23 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader24 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
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
			this.animationTracker = new SpriteBoy.Controls.NSAnimationView();
			this.panel3 = new System.Windows.Forms.Panel();
			this.animationAdd = new SpriteBoy.Controls.NSIconicButton();
			this.animationRemove = new SpriteBoy.Controls.NSIconicButton();
			this.animationStop = new SpriteBoy.Controls.NSIconicButton();
			this.animationPlay = new SpriteBoy.Controls.NSCheckboxIconicButton();
			this.animationList = new SpriteBoy.Controls.NSListView();
			this.animationProps = new System.Windows.Forms.Panel();
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
			nsListViewColumnHeader25.Text = "№";
			nsListViewColumnHeader25.Width = 30;
			nsListViewColumnHeader26.Text = "Texture";
			nsListViewColumnHeader26.Width = 100;
			nsListViewColumnHeader27.Text = "Verts";
			nsListViewColumnHeader27.Width = 50;
			nsListViewColumnHeader28.Text = "Tris";
			nsListViewColumnHeader28.Width = 50;
			this.surfacesList.Columns = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader[] {
        nsListViewColumnHeader25,
        nsListViewColumnHeader26,
        nsListViewColumnHeader27,
        nsListViewColumnHeader28};
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
			this.animationPage.Controls.Add(this.animationProps);
			this.animationPage.Location = new System.Drawing.Point(119, 4);
			this.animationPage.Name = "animationPage";
			this.animationPage.Padding = new System.Windows.Forms.Padding(3);
			this.animationPage.Size = new System.Drawing.Size(603, 142);
			this.animationPage.TabIndex = 1;
			this.animationPage.Text = "Animation";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.animationTracker);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.animationList);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(453, 136);
			this.panel2.TabIndex = 2;
			// 
			// animationTracker
			// 
			this.animationTracker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.animationTracker.Dock = System.Windows.Forms.DockStyle.Fill;
			this.animationTracker.Length = 60;
			this.animationTracker.Location = new System.Drawing.Point(0, 0);
			this.animationTracker.MarkerPosition = -1F;
			this.animationTracker.Name = "animationTracker";
			this.animationTracker.PointKeys = null;
			this.animationTracker.Size = new System.Drawing.Size(253, 103);
			this.animationTracker.TabIndex = 0;
			this.animationTracker.Text = "nsAnimationView1";
			this.animationTracker.FrameChanged += new SpriteBoy.Controls.NSAnimationView.FrameChangedEventHandler(this.animationTracker_FrameChanged);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.animationAdd);
			this.panel3.Controls.Add(this.animationRemove);
			this.panel3.Controls.Add(this.animationStop);
			this.panel3.Controls.Add(this.animationPlay);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 103);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(253, 33);
			this.panel3.TabIndex = 2;
			// 
			// animationAdd
			// 
			this.animationAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.animationAdd.Corners.BottomLeft = true;
			this.animationAdd.Corners.BottomRight = false;
			this.animationAdd.Corners.TopLeft = true;
			this.animationAdd.Corners.TopRight = false;
			this.animationAdd.IconImage = ((System.Drawing.Image)(resources.GetObject("animationAdd.IconImage")));
			this.animationAdd.IconSize = new System.Drawing.Size(13, 13);
			this.animationAdd.Large = false;
			this.animationAdd.Location = new System.Drawing.Point(201, 5);
			this.animationAdd.Name = "animationAdd";
			this.animationAdd.Size = new System.Drawing.Size(25, 25);
			this.animationAdd.TabIndex = 3;
			// 
			// animationRemove
			// 
			this.animationRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.animationRemove.Corners.BottomLeft = false;
			this.animationRemove.Corners.BottomRight = true;
			this.animationRemove.Corners.TopLeft = false;
			this.animationRemove.Corners.TopRight = true;
			this.animationRemove.Enabled = false;
			this.animationRemove.IconImage = ((System.Drawing.Image)(resources.GetObject("animationRemove.IconImage")));
			this.animationRemove.IconSize = new System.Drawing.Size(13, 13);
			this.animationRemove.Large = false;
			this.animationRemove.Location = new System.Drawing.Point(225, 5);
			this.animationRemove.Name = "animationRemove";
			this.animationRemove.Size = new System.Drawing.Size(25, 25);
			this.animationRemove.TabIndex = 2;
			// 
			// animationStop
			// 
			this.animationStop.Corners.BottomLeft = false;
			this.animationStop.Corners.BottomRight = true;
			this.animationStop.Corners.TopLeft = false;
			this.animationStop.Corners.TopRight = true;
			this.animationStop.IconImage = ((System.Drawing.Image)(resources.GetObject("animationStop.IconImage")));
			this.animationStop.IconSize = new System.Drawing.Size(17, 17);
			this.animationStop.Large = true;
			this.animationStop.Location = new System.Drawing.Point(37, 3);
			this.animationStop.Name = "animationStop";
			this.animationStop.Size = new System.Drawing.Size(35, 30);
			this.animationStop.TabIndex = 1;
			this.animationStop.Click += new System.EventHandler(this.animationStop_Click);
			// 
			// animationPlay
			// 
			this.animationPlay.Checked = false;
			this.animationPlay.Corners.BottomLeft = true;
			this.animationPlay.Corners.BottomRight = false;
			this.animationPlay.Corners.TopLeft = true;
			this.animationPlay.Corners.TopRight = false;
			this.animationPlay.IconImage = ((System.Drawing.Image)(resources.GetObject("animationPlay.IconImage")));
			this.animationPlay.IconSize = new System.Drawing.Size(17, 17);
			this.animationPlay.Large = true;
			this.animationPlay.Location = new System.Drawing.Point(3, 3);
			this.animationPlay.Name = "animationPlay";
			this.animationPlay.Size = new System.Drawing.Size(35, 30);
			this.animationPlay.TabIndex = 0;
			// 
			// animationList
			// 
			nsListViewColumnHeader22.Text = "Name";
			nsListViewColumnHeader22.Width = 102;
			nsListViewColumnHeader23.Text = "Start";
			nsListViewColumnHeader23.Width = 40;
			nsListViewColumnHeader24.Text = "End";
			nsListViewColumnHeader24.Width = 40;
			this.animationList.Columns = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader[] {
        nsListViewColumnHeader22,
        nsListViewColumnHeader23,
        nsListViewColumnHeader24};
			this.animationList.Dock = System.Windows.Forms.DockStyle.Right;
			this.animationList.Items = new SpriteBoy.Controls.NSListView.NSListViewItem[0];
			this.animationList.Location = new System.Drawing.Point(253, 0);
			this.animationList.MultiSelect = true;
			this.animationList.Name = "animationList";
			this.animationList.SelectedItem = null;
			this.animationList.SelectedItems = new SpriteBoy.Controls.NSListView.NSListViewItem[0];
			this.animationList.Size = new System.Drawing.Size(200, 136);
			this.animationList.TabIndex = 1;
			this.animationList.Text = "nsListView1";
			// 
			// animationProps
			// 
			this.animationProps.Dock = System.Windows.Forms.DockStyle.Right;
			this.animationProps.Location = new System.Drawing.Point(456, 3);
			this.animationProps.Name = "animationProps";
			this.animationProps.Size = new System.Drawing.Size(144, 136);
			this.animationProps.TabIndex = 1;
			this.animationProps.Visible = false;
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
		public Controls.NSListView surfacesList;
		public System.Windows.Forms.Panel surfaceTexPanel;
		public Controls.NSFileDropControl textureFile;
		public Controls.NSColorPickerButton surfaceColorButton;
		public Controls.NSCheckboxIconicButton cellGizmoButton;
		public Controls.NSCheckboxIconicButton firstPersonButton;
		public Controls.NSOnOffBox surfaceUnlit;
		public Controls.NSComboBox surfaceBlending;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		public Controls.NSTabControl propertyTabs;
		public System.Windows.Forms.TabPage surfacePage;
		public System.Windows.Forms.TabPage animationPage;
		public System.Windows.Forms.TabPage jointsPage;
		public Controls.NSAnimationView animationTracker;
		public Controls.NSListView animationList;
		public System.Windows.Forms.Panel animationProps;
		public Controls.NSIconicButton animationStop;
		public Controls.NSCheckboxIconicButton animationPlay;
		public Controls.NSIconicButton animationAdd;
		public Controls.NSIconicButton animationRemove;
	}
}