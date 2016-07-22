using SpriteBoy.Controls;
namespace SpriteBoy.Forms.Common {
	partial class MainForm {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.workSplit = new System.Windows.Forms.SplitContainer();
			this.projectTabs = new SpriteBoy.Controls.NSProjectControl();
			this.projectInspector = new SpriteBoy.Controls.NSDirectoryInspector();
			this.projectDirPanel = new System.Windows.Forms.Panel();
			this.projectDir = new SpriteBoy.Controls.NSTextBox();
			this.projectDirUp = new SpriteBoy.Controls.NSIconicButton();
			this.projectActPanel = new System.Windows.Forms.Panel();
			this.projectAdd = new SpriteBoy.Controls.NSIconicButton();
			this.projectRemove = new SpriteBoy.Controls.NSIconicButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.frameTimer = new System.Windows.Forms.Timer(this.components);
			this.nsMenuStrip1 = new SpriteBoy.Controls.NSMenuStrip();
			this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.workSplit)).BeginInit();
			this.workSplit.Panel1.SuspendLayout();
			this.workSplit.Panel2.SuspendLayout();
			this.workSplit.SuspendLayout();
			this.projectDirPanel.SuspendLayout();
			this.projectActPanel.SuspendLayout();
			this.nsMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// workSplit
			// 
			this.workSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			resources.ApplyResources(this.workSplit, "workSplit");
			this.workSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.workSplit.Name = "workSplit";
			// 
			// workSplit.Panel1
			// 
			this.workSplit.Panel1.Controls.Add(this.projectTabs);
			// 
			// workSplit.Panel2
			// 
			this.workSplit.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.workSplit.Panel2.Controls.Add(this.projectInspector);
			this.workSplit.Panel2.Controls.Add(this.projectDirPanel);
			this.workSplit.Panel2.Controls.Add(this.projectActPanel);
			this.workSplit.Panel2.Controls.Add(this.panel1);
			this.workSplit.TabStop = false;
			// 
			// projectTabs
			// 
			resources.ApplyResources(this.projectTabs, "projectTabs");
			this.projectTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.projectTabs.Name = "projectTabs";
			this.projectTabs.SelectedIndex = 0;
			this.projectTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.projectTabs.TabClose += new SpriteBoy.Controls.NSProjectControl.TabCloseEventHandler(this.projectTabs_TabClose);
			// 
			// projectInspector
			// 
			this.projectInspector.AllowDragging = true;
			resources.ApplyResources(this.projectInspector, "projectInspector");
			this.projectInspector.Name = "projectInspector";
			this.projectInspector.Offset = 0;
			this.projectInspector.SelectedEntry = null;
			this.projectInspector.MouseClick += new System.Windows.Forms.MouseEventHandler(this.projectInspector_MouseClick);
			this.projectInspector.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.projectInspector_MouseDoubleClick);
			this.projectInspector.MouseDown += new System.Windows.Forms.MouseEventHandler(this.projectInspector_MouseDown);
			// 
			// projectDirPanel
			// 
			this.projectDirPanel.Controls.Add(this.projectDir);
			this.projectDirPanel.Controls.Add(this.projectDirUp);
			resources.ApplyResources(this.projectDirPanel, "projectDirPanel");
			this.projectDirPanel.Name = "projectDirPanel";
			// 
			// projectDir
			// 
			this.projectDir.Corners.BottomLeft = false;
			this.projectDir.Corners.BottomRight = false;
			this.projectDir.Corners.TopLeft = true;
			this.projectDir.Corners.TopRight = false;
			this.projectDir.Cursor = System.Windows.Forms.Cursors.IBeam;
			resources.ApplyResources(this.projectDir, "projectDir");
			this.projectDir.MaxLength = 32767;
			this.projectDir.Multiline = false;
			this.projectDir.Name = "projectDir";
			this.projectDir.ReadOnly = true;
			this.projectDir.TabStop = false;
			this.projectDir.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.projectDir.UseSystemPasswordChar = false;
			// 
			// projectDirUp
			// 
			this.projectDirUp.Corners.BottomLeft = false;
			this.projectDirUp.Corners.BottomRight = false;
			this.projectDirUp.Corners.TopLeft = false;
			this.projectDirUp.Corners.TopRight = true;
			resources.ApplyResources(this.projectDirUp, "projectDirUp");
			this.projectDirUp.IconImage = ((System.Drawing.Image)(resources.GetObject("projectDirUp.IconImage")));
			this.projectDirUp.IconSize = new System.Drawing.Size(14, 14);
			this.projectDirUp.Large = false;
			this.projectDirUp.Name = "projectDirUp";
			this.projectDirUp.TabStop = false;
			this.projectDirUp.Click += new System.EventHandler(this.projectDirUp_Click);
			// 
			// projectActPanel
			// 
			this.projectActPanel.Controls.Add(this.projectAdd);
			this.projectActPanel.Controls.Add(this.projectRemove);
			resources.ApplyResources(this.projectActPanel, "projectActPanel");
			this.projectActPanel.Name = "projectActPanel";
			// 
			// projectAdd
			// 
			resources.ApplyResources(this.projectAdd, "projectAdd");
			this.projectAdd.Corners.BottomLeft = true;
			this.projectAdd.Corners.BottomRight = false;
			this.projectAdd.Corners.TopLeft = true;
			this.projectAdd.Corners.TopRight = false;
			this.projectAdd.IconImage = ((System.Drawing.Image)(resources.GetObject("projectAdd.IconImage")));
			this.projectAdd.IconSize = new System.Drawing.Size(14, 14);
			this.projectAdd.Large = false;
			this.projectAdd.Name = "projectAdd";
			this.projectAdd.Click += new System.EventHandler(this.projectAdd_Click);
			// 
			// projectRemove
			// 
			resources.ApplyResources(this.projectRemove, "projectRemove");
			this.projectRemove.Corners.BottomLeft = false;
			this.projectRemove.Corners.BottomRight = true;
			this.projectRemove.Corners.TopLeft = false;
			this.projectRemove.Corners.TopRight = true;
			this.projectRemove.IconImage = ((System.Drawing.Image)(resources.GetObject("projectRemove.IconImage")));
			this.projectRemove.IconSize = new System.Drawing.Size(14, 14);
			this.projectRemove.Large = false;
			this.projectRemove.Name = "projectRemove";
			this.projectRemove.Click += new System.EventHandler(this.projectRemove_Click);
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			// 
			// frameTimer
			// 
			this.frameTimer.Interval = 10;
			this.frameTimer.Tick += new System.EventHandler(this.frameTimer_Tick);
			// 
			// nsMenuStrip1
			// 
			this.nsMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
			this.nsMenuStrip1.ForeColor = System.Drawing.Color.White;
			this.nsMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem});
			resources.ApplyResources(this.nsMenuStrip1, "nsMenuStrip1");
			this.nsMenuStrip1.Name = "nsMenuStrip1";
			// 
			// projectToolStripMenuItem
			// 
			this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
			resources.ApplyResources(this.projectToolStripMenuItem, "projectToolStripMenuItem");
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.workSplit);
			this.Controls.Add(this.nsMenuStrip1);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.Activated += new System.EventHandler(this.MainForm_Activated);
			this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
			this.workSplit.Panel1.ResumeLayout(false);
			this.workSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.workSplit)).EndInit();
			this.workSplit.ResumeLayout(false);
			this.projectDirPanel.ResumeLayout(false);
			this.projectActPanel.ResumeLayout(false);
			this.nsMenuStrip1.ResumeLayout(false);
			this.nsMenuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private NSProjectControl projectTabs;
		private System.Windows.Forms.SplitContainer workSplit;
		private System.Windows.Forms.Panel projectActPanel;
		private System.Windows.Forms.Panel projectDirPanel;
		private NSTextBox projectDir;
		private NSIconicButton projectDirUp;
		private NSDirectoryInspector projectInspector;
		private System.Windows.Forms.Panel panel1;
		private NSMenuStrip nsMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
		private System.Windows.Forms.Timer frameTimer;
		private NSIconicButton projectAdd;
		private NSIconicButton projectRemove;







	}
}