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
			this.optionTabs = new SpriteBoy.Controls.NSTabControl();
			this.paramsTab = new System.Windows.Forms.TabPage();
			this.nsLabel3 = new SpriteBoy.Controls.NSLabel();
			this.wrapVCombo = new SpriteBoy.Controls.NSComboBox();
			this.nsLabel2 = new SpriteBoy.Controls.NSLabel();
			this.wrapUCombo = new SpriteBoy.Controls.NSComboBox();
			this.nsLabel1 = new SpriteBoy.Controls.NSLabel();
			this.filteringCombo = new SpriteBoy.Controls.NSComboBox();
			this.utilPanel = new System.Windows.Forms.Panel();
			this.imageTileButton = new SpriteBoy.Controls.NSCheckboxIconicButton();
			this.toolPanel.SuspendLayout();
			this.optionTabs.SuspendLayout();
			this.paramsTab.SuspendLayout();
			this.utilPanel.SuspendLayout();
			this.SuspendLayout();
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
			// optionTabs
			// 
			resources.ApplyResources(this.optionTabs, "optionTabs");
			this.optionTabs.Controls.Add(this.paramsTab);
			this.optionTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.optionTabs.Multiline = true;
			this.optionTabs.Name = "optionTabs";
			this.optionTabs.SelectedIndex = 0;
			this.optionTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			// 
			// paramsTab
			// 
			resources.ApplyResources(this.paramsTab, "paramsTab");
			this.paramsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.paramsTab.Controls.Add(this.nsLabel3);
			this.paramsTab.Controls.Add(this.wrapVCombo);
			this.paramsTab.Controls.Add(this.nsLabel2);
			this.paramsTab.Controls.Add(this.wrapUCombo);
			this.paramsTab.Controls.Add(this.nsLabel1);
			this.paramsTab.Controls.Add(this.filteringCombo);
			this.paramsTab.Name = "paramsTab";
			// 
			// nsLabel3
			// 
			resources.ApplyResources(this.nsLabel3, "nsLabel3");
			this.nsLabel3.ForeColor = System.Drawing.Color.White;
			this.nsLabel3.Name = "nsLabel3";
			this.nsLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// wrapVCombo
			// 
			resources.ApplyResources(this.wrapVCombo, "wrapVCombo");
			this.wrapVCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.wrapVCombo.ForeColor = System.Drawing.Color.White;
			this.wrapVCombo.Items = "";
			this.wrapVCombo.Name = "wrapVCombo";
			this.wrapVCombo.SelectedIndex = 0;
			this.wrapVCombo.IndexChanged += new SpriteBoy.Controls.NSComboBox.SelectionChangedEventHandler(this.wrapVCombo_IndexChanged);
			// 
			// nsLabel2
			// 
			resources.ApplyResources(this.nsLabel2, "nsLabel2");
			this.nsLabel2.ForeColor = System.Drawing.Color.White;
			this.nsLabel2.Name = "nsLabel2";
			this.nsLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// wrapUCombo
			// 
			resources.ApplyResources(this.wrapUCombo, "wrapUCombo");
			this.wrapUCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.wrapUCombo.ForeColor = System.Drawing.Color.White;
			this.wrapUCombo.Items = "";
			this.wrapUCombo.Name = "wrapUCombo";
			this.wrapUCombo.SelectedIndex = 0;
			this.wrapUCombo.IndexChanged += new SpriteBoy.Controls.NSComboBox.SelectionChangedEventHandler(this.wrapUCombo_IndexChanged);
			// 
			// nsLabel1
			// 
			resources.ApplyResources(this.nsLabel1, "nsLabel1");
			this.nsLabel1.ForeColor = System.Drawing.Color.White;
			this.nsLabel1.Name = "nsLabel1";
			this.nsLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// filteringCombo
			// 
			resources.ApplyResources(this.filteringCombo, "filteringCombo");
			this.filteringCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.filteringCombo.ForeColor = System.Drawing.Color.White;
			this.filteringCombo.Items = "";
			this.filteringCombo.Name = "filteringCombo";
			this.filteringCombo.SelectedIndex = 0;
			this.filteringCombo.IndexChanged += new SpriteBoy.Controls.NSComboBox.SelectionChangedEventHandler(this.filteringCombo_IndexChanged);
			// 
			// utilPanel
			// 
			resources.ApplyResources(this.utilPanel, "utilPanel");
			this.utilPanel.Controls.Add(this.imageTileButton);
			this.utilPanel.Name = "utilPanel";
			// 
			// imageTileButton
			// 
			resources.ApplyResources(this.imageTileButton, "imageTileButton");
			this.imageTileButton.Checked = false;
			this.imageTileButton.Corners.BottomLeft = true;
			this.imageTileButton.Corners.BottomRight = true;
			this.imageTileButton.Corners.TopLeft = true;
			this.imageTileButton.Corners.TopRight = true;
			this.imageTileButton.IconImage = ((System.Drawing.Image)(resources.GetObject("imageTileButton.IconImage")));
			this.imageTileButton.IconSize = new System.Drawing.Size(14, 14);
			this.imageTileButton.Large = false;
			this.imageTileButton.Name = "imageTileButton";
			this.imageTileButton.CheckedChanged += new SpriteBoy.Controls.NSCheckboxIconicButton.CheckedChangedEventHandler(this.imageTileButton_CheckedChanged);
			// 
			// ImageForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.Controls.Add(this.toolPanel);
			this.Controls.Add(this.optionTabs);
			this.Controls.Add(this.utilPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
			this.Name = "ImageForm";
			this.toolPanel.ResumeLayout(false);
			this.optionTabs.ResumeLayout(false);
			this.paramsTab.ResumeLayout(false);
			this.utilPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel toolPanel;
		private Controls.NSRadioIconicButton toolLook;
		private Controls.NSTabControl optionTabs;
		private System.Windows.Forms.TabPage paramsTab;
		private System.Windows.Forms.Panel utilPanel;
		private Controls.NSLabel nsLabel1;
		private Controls.NSLabel nsLabel3;
		private Controls.NSLabel nsLabel2;
		public Controls.NSCheckboxIconicButton imageTileButton;
		public Controls.NSComboBox filteringCombo;
		public Controls.NSComboBox wrapVCombo;
		public Controls.NSComboBox wrapUCombo;
	}
}