namespace SpriteBoy.Forms.Editors {
	partial class ImageForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageForm));
			this.toolPanel = new System.Windows.Forms.Panel();
			this.toolLook = new SpriteBoy.Controls.NSRadioIconicButton();
			this.nsTabControl1 = new SpriteBoy.Controls.NSTabControl();
			this.paramsTab = new System.Windows.Forms.TabPage();
			this.nsLabel3 = new SpriteBoy.Controls.NSLabel();
			this.wrapVCombo = new SpriteBoy.Controls.NSComboBox();
			this.nsLabel2 = new SpriteBoy.Controls.NSLabel();
			this.wrapUCombo = new SpriteBoy.Controls.NSComboBox();
			this.nsLabel1 = new SpriteBoy.Controls.NSLabel();
			this.filteringCombo = new SpriteBoy.Controls.NSComboBox();
			this.utilPanel = new System.Windows.Forms.Panel();
			this.toolPanel.SuspendLayout();
			this.nsTabControl1.SuspendLayout();
			this.paramsTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolPanel
			// 
			this.toolPanel.Controls.Add(this.toolLook);
			this.toolPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.toolPanel.Location = new System.Drawing.Point(0, 25);
			this.toolPanel.Name = "toolPanel";
			this.toolPanel.Size = new System.Drawing.Size(45, 395);
			this.toolPanel.TabIndex = 6;
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
			// nsTabControl1
			// 
			this.nsTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.nsTabControl1.Controls.Add(this.paramsTab);
			this.nsTabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.nsTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.nsTabControl1.ItemSize = new System.Drawing.Size(28, 115);
			this.nsTabControl1.Location = new System.Drawing.Point(0, 420);
			this.nsTabControl1.Multiline = true;
			this.nsTabControl1.Name = "nsTabControl1";
			this.nsTabControl1.SelectedIndex = 0;
			this.nsTabControl1.Size = new System.Drawing.Size(719, 101);
			this.nsTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.nsTabControl1.TabIndex = 5;
			// 
			// paramsTab
			// 
			this.paramsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.paramsTab.Controls.Add(this.nsLabel3);
			this.paramsTab.Controls.Add(this.wrapVCombo);
			this.paramsTab.Controls.Add(this.nsLabel2);
			this.paramsTab.Controls.Add(this.wrapUCombo);
			this.paramsTab.Controls.Add(this.nsLabel1);
			this.paramsTab.Controls.Add(this.filteringCombo);
			this.paramsTab.Location = new System.Drawing.Point(119, 4);
			this.paramsTab.Name = "paramsTab";
			this.paramsTab.Size = new System.Drawing.Size(596, 93);
			this.paramsTab.TabIndex = 2;
			this.paramsTab.Text = "Parameters";
			// 
			// nsLabel3
			// 
			this.nsLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.nsLabel3.ForeColor = System.Drawing.Color.White;
			this.nsLabel3.Location = new System.Drawing.Point(6, 60);
			this.nsLabel3.Name = "nsLabel3";
			this.nsLabel3.Size = new System.Drawing.Size(133, 21);
			this.nsLabel3.TabIndex = 5;
			this.nsLabel3.Text = "Vertical wrapping";
			this.nsLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// wrapVCombo
			// 
			this.wrapVCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.wrapVCombo.ForeColor = System.Drawing.Color.White;
			this.wrapVCombo.Items = "";
			this.wrapVCombo.Location = new System.Drawing.Point(145, 60);
			this.wrapVCombo.Name = "wrapVCombo";
			this.wrapVCombo.SelectedIndex = 0;
			this.wrapVCombo.Size = new System.Drawing.Size(200, 21);
			this.wrapVCombo.TabIndex = 4;
			// 
			// nsLabel2
			// 
			this.nsLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.nsLabel2.ForeColor = System.Drawing.Color.White;
			this.nsLabel2.Location = new System.Drawing.Point(6, 33);
			this.nsLabel2.Name = "nsLabel2";
			this.nsLabel2.Size = new System.Drawing.Size(133, 21);
			this.nsLabel2.TabIndex = 3;
			this.nsLabel2.Text = "Horizontal wrapping";
			this.nsLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// wrapUCombo
			// 
			this.wrapUCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.wrapUCombo.ForeColor = System.Drawing.Color.White;
			this.wrapUCombo.Items = "";
			this.wrapUCombo.Location = new System.Drawing.Point(145, 33);
			this.wrapUCombo.Name = "wrapUCombo";
			this.wrapUCombo.SelectedIndex = 0;
			this.wrapUCombo.Size = new System.Drawing.Size(200, 21);
			this.wrapUCombo.TabIndex = 2;
			// 
			// nsLabel1
			// 
			this.nsLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.nsLabel1.ForeColor = System.Drawing.Color.White;
			this.nsLabel1.Location = new System.Drawing.Point(6, 6);
			this.nsLabel1.Name = "nsLabel1";
			this.nsLabel1.Size = new System.Drawing.Size(133, 21);
			this.nsLabel1.TabIndex = 1;
			this.nsLabel1.Text = "Filtering";
			this.nsLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// filteringCombo
			// 
			this.filteringCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.filteringCombo.ForeColor = System.Drawing.Color.White;
			this.filteringCombo.Items = "";
			this.filteringCombo.Location = new System.Drawing.Point(145, 6);
			this.filteringCombo.Name = "filteringCombo";
			this.filteringCombo.SelectedIndex = 0;
			this.filteringCombo.Size = new System.Drawing.Size(200, 21);
			this.filteringCombo.TabIndex = 0;
			// 
			// utilPanel
			// 
			this.utilPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.utilPanel.Location = new System.Drawing.Point(0, 0);
			this.utilPanel.Name = "utilPanel";
			this.utilPanel.Size = new System.Drawing.Size(719, 25);
			this.utilPanel.TabIndex = 7;
			// 
			// ImageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(719, 521);
			this.Controls.Add(this.toolPanel);
			this.Controls.Add(this.nsTabControl1);
			this.Controls.Add(this.utilPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
			this.Name = "ImageForm";
			this.Text = "Image";
			this.toolPanel.ResumeLayout(false);
			this.nsTabControl1.ResumeLayout(false);
			this.paramsTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel toolPanel;
		private Controls.NSRadioIconicButton toolLook;
		private Controls.NSTabControl nsTabControl1;
		private System.Windows.Forms.TabPage paramsTab;
		private System.Windows.Forms.Panel utilPanel;
		private Controls.NSLabel nsLabel1;
		private Controls.NSComboBox filteringCombo;
		private Controls.NSLabel nsLabel3;
		private Controls.NSComboBox wrapVCombo;
		private Controls.NSLabel nsLabel2;
		private Controls.NSComboBox wrapUCombo;
	}
}