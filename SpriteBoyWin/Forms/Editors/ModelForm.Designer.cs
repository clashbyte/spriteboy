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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelForm));
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader1 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader2 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader3 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader4 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			this.toolPanel = new System.Windows.Forms.Panel();
			this.toolLook = new SpriteBoy.Controls.NSRadioIconicButton();
			this.utilPanel = new System.Windows.Forms.Panel();
			this.nsTabControl1 = new SpriteBoy.Controls.NSTabControl();
			this.surfacePage = new System.Windows.Forms.TabPage();
			this.animationPage = new System.Windows.Forms.TabPage();
			this.jointsPage = new System.Windows.Forms.TabPage();
			this.surfacesList = new SpriteBoy.Controls.NSListView();
			this.toolPanel.SuspendLayout();
			this.nsTabControl1.SuspendLayout();
			this.surfacePage.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolPanel
			// 
			this.toolPanel.Controls.Add(this.toolLook);
			this.toolPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.toolPanel.Location = new System.Drawing.Point(0, 25);
			this.toolPanel.Name = "toolPanel";
			this.toolPanel.Size = new System.Drawing.Size(45, 342);
			this.toolPanel.TabIndex = 5;
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
			this.utilPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.utilPanel.Location = new System.Drawing.Point(0, 0);
			this.utilPanel.Name = "utilPanel";
			this.utilPanel.Size = new System.Drawing.Size(726, 25);
			this.utilPanel.TabIndex = 6;
			// 
			// nsTabControl1
			// 
			this.nsTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.nsTabControl1.Controls.Add(this.surfacePage);
			this.nsTabControl1.Controls.Add(this.animationPage);
			this.nsTabControl1.Controls.Add(this.jointsPage);
			this.nsTabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.nsTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.nsTabControl1.ItemSize = new System.Drawing.Size(28, 115);
			this.nsTabControl1.Location = new System.Drawing.Point(0, 367);
			this.nsTabControl1.Multiline = true;
			this.nsTabControl1.Name = "nsTabControl1";
			this.nsTabControl1.SelectedIndex = 0;
			this.nsTabControl1.Size = new System.Drawing.Size(726, 150);
			this.nsTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.nsTabControl1.TabIndex = 7;
			// 
			// surfacePage
			// 
			this.surfacePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.surfacePage.Controls.Add(this.surfacesList);
			this.surfacePage.Location = new System.Drawing.Point(119, 4);
			this.surfacePage.Name = "surfacePage";
			this.surfacePage.Padding = new System.Windows.Forms.Padding(3);
			this.surfacePage.Size = new System.Drawing.Size(603, 142);
			this.surfacePage.TabIndex = 0;
			this.surfacePage.Text = "Surfaces";
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
			// surfacesList
			// 
			this.surfacesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			nsListViewColumnHeader1.Text = "№";
			nsListViewColumnHeader1.Width = 30;
			nsListViewColumnHeader2.Text = "Name";
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
			this.surfacesList.Items = new SpriteBoy.Controls.NSListView.NSListViewItem[0];
			this.surfacesList.Location = new System.Drawing.Point(3, 0);
			this.surfacesList.MultiSelect = false;
			this.surfacesList.Name = "surfacesList";
			this.surfacesList.Size = new System.Drawing.Size(297, 142);
			this.surfacesList.TabIndex = 0;
			this.surfacesList.Text = "nsListView1";
			// 
			// ModelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(726, 517);
			this.Controls.Add(this.toolPanel);
			this.Controls.Add(this.utilPanel);
			this.Controls.Add(this.nsTabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
			this.Name = "ModelForm";
			this.Text = "Model";
			this.toolPanel.ResumeLayout(false);
			this.nsTabControl1.ResumeLayout(false);
			this.surfacePage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel toolPanel;
		private Controls.NSRadioIconicButton toolLook;
		private System.Windows.Forms.Panel utilPanel;
		private Controls.NSTabControl nsTabControl1;
		private System.Windows.Forms.TabPage surfacePage;
		private System.Windows.Forms.TabPage animationPage;
		private System.Windows.Forms.TabPage jointsPage;
		private Controls.NSListView surfacesList;
	}
}