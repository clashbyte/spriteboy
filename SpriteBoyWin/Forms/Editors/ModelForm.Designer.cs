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
			SpriteBoy.Controls.NSLabel nsLabel2;
			SpriteBoy.Controls.NSLabel nsLabel3;
			SpriteBoy.Controls.NSLabel nsLabel4;
			SpriteBoy.Controls.NSLabel nsLabel5;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelForm));
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader17 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader18 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader19 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader20 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			this.toolPanel = new System.Windows.Forms.Panel();
			this.toolCursor = new SpriteBoy.Controls.NSRadioIconicButton();
			this.utilPanel = new System.Windows.Forms.Panel();
			this.propertyTabs = new SpriteBoy.Controls.NSTabControl();
			this.surfacePage = new System.Windows.Forms.TabPage();
			this.surfacesList = new SpriteBoy.Controls.NSListView();
			this.surfaceTexPanel = new System.Windows.Forms.Panel();
			this.surfaceUnlit = new SpriteBoy.Controls.NSOnOffBox();
			this.surfaceOpaque = new SpriteBoy.Controls.NSOnOffBox();
			this.surfaceColorButton = new SpriteBoy.Controls.NSColorPickerButton();
			this.textureFile = new SpriteBoy.Controls.NSFileDropControl();
			this.animationPage = new System.Windows.Forms.TabPage();
			this.jointsPage = new System.Windows.Forms.TabPage();
			this.cellGizmoButton = new SpriteBoy.Controls.NSCheckboxIconicButton();
			this.firstPersonButton = new SpriteBoy.Controls.NSCheckboxIconicButton();
			nsLabel1 = new SpriteBoy.Controls.NSLabel();
			nsLabel2 = new SpriteBoy.Controls.NSLabel();
			nsLabel3 = new SpriteBoy.Controls.NSLabel();
			nsLabel4 = new SpriteBoy.Controls.NSLabel();
			nsLabel5 = new SpriteBoy.Controls.NSLabel();
			this.toolPanel.SuspendLayout();
			this.utilPanel.SuspendLayout();
			this.propertyTabs.SuspendLayout();
			this.surfacePage.SuspendLayout();
			this.surfaceTexPanel.SuspendLayout();
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
			// nsLabel2
			// 
			nsLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel2.ForeColor = System.Drawing.Color.White;
			nsLabel2.Location = new System.Drawing.Point(118, 0);
			nsLabel2.Name = "nsLabel2";
			nsLabel2.Size = new System.Drawing.Size(125, 20);
			nsLabel2.TabIndex = 2;
			nsLabel2.Text = "Parameters";
			nsLabel2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nsLabel3
			// 
			nsLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel3.ForeColor = System.Drawing.Color.White;
			nsLabel3.Location = new System.Drawing.Point(118, 26);
			nsLabel3.Name = "nsLabel3";
			nsLabel3.Size = new System.Drawing.Size(57, 25);
			nsLabel3.TabIndex = 4;
			nsLabel3.Text = "Color:";
			nsLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nsLabel4
			// 
			nsLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel4.ForeColor = System.Drawing.Color.White;
			nsLabel4.Location = new System.Drawing.Point(118, 57);
			nsLabel4.Name = "nsLabel4";
			nsLabel4.Size = new System.Drawing.Size(57, 24);
			nsLabel4.TabIndex = 6;
			nsLabel4.Text = "Opaque:";
			nsLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nsLabel5
			// 
			nsLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F);
			nsLabel5.ForeColor = System.Drawing.Color.White;
			nsLabel5.Location = new System.Drawing.Point(118, 87);
			nsLabel5.Name = "nsLabel5";
			nsLabel5.Size = new System.Drawing.Size(57, 24);
			nsLabel5.TabIndex = 8;
			nsLabel5.Text = "Unlit:";
			nsLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			nsListViewColumnHeader17.Text = "№";
			nsListViewColumnHeader17.Width = 30;
			nsListViewColumnHeader18.Text = "Texture";
			nsListViewColumnHeader18.Width = 100;
			nsListViewColumnHeader19.Text = "Verts";
			nsListViewColumnHeader19.Width = 50;
			nsListViewColumnHeader20.Text = "Tris";
			nsListViewColumnHeader20.Width = 50;
			this.surfacesList.Columns = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader[] {
        nsListViewColumnHeader17,
        nsListViewColumnHeader18,
        nsListViewColumnHeader19,
        nsListViewColumnHeader20};
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
			this.surfaceTexPanel.Controls.Add(nsLabel5);
			this.surfaceTexPanel.Controls.Add(this.surfaceUnlit);
			this.surfaceTexPanel.Controls.Add(nsLabel4);
			this.surfaceTexPanel.Controls.Add(this.surfaceOpaque);
			this.surfaceTexPanel.Controls.Add(nsLabel3);
			this.surfaceTexPanel.Controls.Add(this.surfaceColorButton);
			this.surfaceTexPanel.Controls.Add(nsLabel2);
			this.surfaceTexPanel.Controls.Add(nsLabel1);
			this.surfaceTexPanel.Controls.Add(this.textureFile);
			this.surfaceTexPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.surfaceTexPanel.Location = new System.Drawing.Point(357, 3);
			this.surfaceTexPanel.Name = "surfaceTexPanel";
			this.surfaceTexPanel.Size = new System.Drawing.Size(243, 136);
			this.surfaceTexPanel.TabIndex = 1;
			this.surfaceTexPanel.Visible = false;
			// 
			// surfaceUnlit
			// 
			this.surfaceUnlit.Checked = true;
			this.surfaceUnlit.Location = new System.Drawing.Point(181, 87);
			this.surfaceUnlit.MaximumSize = new System.Drawing.Size(56, 24);
			this.surfaceUnlit.MinimumSize = new System.Drawing.Size(56, 24);
			this.surfaceUnlit.Name = "surfaceUnlit";
			this.surfaceUnlit.Size = new System.Drawing.Size(56, 24);
			this.surfaceUnlit.TabIndex = 7;
			this.surfaceUnlit.Text = "nsOnOffBox1";
			this.surfaceUnlit.CheckedChanged += new SpriteBoy.Controls.NSOnOffBox.CheckedChangedEventHandler(this.surfaceFlags_CheckedChanged);
			// 
			// surfaceOpaque
			// 
			this.surfaceOpaque.Checked = true;
			this.surfaceOpaque.Location = new System.Drawing.Point(181, 57);
			this.surfaceOpaque.MaximumSize = new System.Drawing.Size(56, 24);
			this.surfaceOpaque.MinimumSize = new System.Drawing.Size(56, 24);
			this.surfaceOpaque.Name = "surfaceOpaque";
			this.surfaceOpaque.Size = new System.Drawing.Size(56, 24);
			this.surfaceOpaque.TabIndex = 5;
			this.surfaceOpaque.Text = "nsOnOffBox1";
			this.surfaceOpaque.CheckedChanged += new SpriteBoy.Controls.NSOnOffBox.CheckedChangedEventHandler(this.surfaceFlags_CheckedChanged);
			// 
			// surfaceColorButton
			// 
			this.surfaceColorButton.Location = new System.Drawing.Point(181, 26);
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
			this.animationPage.Location = new System.Drawing.Point(119, 4);
			this.animationPage.Name = "animationPage";
			this.animationPage.Padding = new System.Windows.Forms.Padding(3);
			this.animationPage.Size = new System.Drawing.Size(603, 142);
			this.animationPage.TabIndex = 1;
			this.animationPage.Text = "Animation";
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
		public Controls.NSOnOffBox surfaceUnlit;
		public Controls.NSOnOffBox surfaceOpaque;
		public Controls.NSColorPickerButton surfaceColorButton;
		public Controls.NSCheckboxIconicButton cellGizmoButton;
		public Controls.NSCheckboxIconicButton firstPersonButton;
	}
}