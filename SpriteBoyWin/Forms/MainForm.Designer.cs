using SpriteBoy.Controls;
namespace SpriteBoy.Forms {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.workSplit = new System.Windows.Forms.SplitContainer();
			this.projectDirPanel = new System.Windows.Forms.Panel();
			this.projectActPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.projectTabs = new SpriteBoy.Controls.NSProjectControl();
			this.projectInspector = new SpriteBoy.Controls.NSDirectoryInspector();
			this.projectDir = new SpriteBoy.Controls.NSTextBox();
			this.projectDirUp = new SpriteBoy.Controls.NSIconicButton();
			this.nsMenuStrip1 = new SpriteBoy.Controls.NSMenuStrip();
			this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.workSplit)).BeginInit();
			this.workSplit.Panel1.SuspendLayout();
			this.workSplit.Panel2.SuspendLayout();
			this.workSplit.SuspendLayout();
			this.projectDirPanel.SuspendLayout();
			this.nsMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// workSplit
			// 
			this.workSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.workSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.workSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.workSplit.Location = new System.Drawing.Point(0, 24);
			this.workSplit.Name = "workSplit";
			// 
			// workSplit.Panel1
			// 
			this.workSplit.Panel1.Controls.Add(this.projectTabs);
			this.workSplit.Panel1MinSize = 150;
			// 
			// workSplit.Panel2
			// 
			this.workSplit.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.workSplit.Panel2.Controls.Add(this.projectInspector);
			this.workSplit.Panel2.Controls.Add(this.projectDirPanel);
			this.workSplit.Panel2.Controls.Add(this.projectActPanel);
			this.workSplit.Panel2.Controls.Add(this.panel1);
			this.workSplit.Panel2MinSize = 150;
			this.workSplit.Size = new System.Drawing.Size(800, 576);
			this.workSplit.SplitterDistance = 559;
			this.workSplit.TabIndex = 4;
			// 
			// projectDirPanel
			// 
			this.projectDirPanel.Controls.Add(this.projectDir);
			this.projectDirPanel.Controls.Add(this.projectDirUp);
			this.projectDirPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.projectDirPanel.Location = new System.Drawing.Point(0, 29);
			this.projectDirPanel.Name = "projectDirPanel";
			this.projectDirPanel.Size = new System.Drawing.Size(237, 25);
			this.projectDirPanel.TabIndex = 1;
			// 
			// projectActPanel
			// 
			this.projectActPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.projectActPanel.Location = new System.Drawing.Point(0, 0);
			this.projectActPanel.Name = "projectActPanel";
			this.projectActPanel.Size = new System.Drawing.Size(237, 29);
			this.projectActPanel.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 493);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(237, 83);
			this.panel1.TabIndex = 4;
			// 
			// projectTabs
			// 
			this.projectTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.projectTabs.ItemSize = new System.Drawing.Size(150, 25);
			this.projectTabs.Location = new System.Drawing.Point(0, 0);
			this.projectTabs.Margin = new System.Windows.Forms.Padding(0);
			this.projectTabs.Name = "projectTabs";
			this.projectTabs.Padding = new System.Drawing.Point(0, 0);
			this.projectTabs.SelectedIndex = 0;
			this.projectTabs.Size = new System.Drawing.Size(559, 576);
			this.projectTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.projectTabs.TabIndex = 3;
			this.projectTabs.TabClose += new SpriteBoy.Controls.NSProjectControl.TabCloseEventHandler(this.projectTabs_TabClose);
			// 
			// projectInspector
			// 
			this.projectInspector.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectInspector.Font = new System.Drawing.Font("Tahoma", 8F);
			this.projectInspector.Location = new System.Drawing.Point(0, 54);
			this.projectInspector.Name = "projectInspector";
			this.projectInspector.Offset = 0;
			this.projectInspector.SelectedEntry = null;
			this.projectInspector.Size = new System.Drawing.Size(237, 439);
			this.projectInspector.TabIndex = 3;
			this.projectInspector.Text = "nsDirectoryInspector1";
			this.projectInspector.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.projectInspector_MouseDoubleClick);
			// 
			// projectDir
			// 
			this.projectDir.Corners.BottomLeft = true;
			this.projectDir.Corners.BottomRight = false;
			this.projectDir.Corners.TopLeft = true;
			this.projectDir.Corners.TopRight = false;
			this.projectDir.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.projectDir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectDir.Location = new System.Drawing.Point(0, 0);
			this.projectDir.MaxLength = 32767;
			this.projectDir.Multiline = false;
			this.projectDir.Name = "projectDir";
			this.projectDir.ReadOnly = true;
			this.projectDir.Size = new System.Drawing.Size(208, 25);
			this.projectDir.TabIndex = 0;
			this.projectDir.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.projectDir.UseSystemPasswordChar = false;
			// 
			// projectDirUp
			// 
			this.projectDirUp.Corners.BottomLeft = false;
			this.projectDirUp.Corners.BottomRight = true;
			this.projectDirUp.Corners.TopLeft = false;
			this.projectDirUp.Corners.TopRight = true;
			this.projectDirUp.Dock = System.Windows.Forms.DockStyle.Right;
			this.projectDirUp.IconImage = ((System.Drawing.Image)(resources.GetObject("projectDirUp.IconImage")));
			this.projectDirUp.IconSize = new System.Drawing.Size(14, 14);
			this.projectDirUp.Large = false;
			this.projectDirUp.Location = new System.Drawing.Point(208, 0);
			this.projectDirUp.Name = "projectDirUp";
			this.projectDirUp.Size = new System.Drawing.Size(29, 25);
			this.projectDirUp.TabIndex = 1;
			this.projectDirUp.Click += new System.EventHandler(this.projectDirUp_Click);
			// 
			// nsMenuStrip1
			// 
			this.nsMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
			this.nsMenuStrip1.ForeColor = System.Drawing.Color.White;
			this.nsMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem});
			this.nsMenuStrip1.Location = new System.Drawing.Point(0, 0);
			this.nsMenuStrip1.Name = "nsMenuStrip1";
			this.nsMenuStrip1.Size = new System.Drawing.Size(800, 24);
			this.nsMenuStrip1.TabIndex = 5;
			this.nsMenuStrip1.Text = "nsMenuStrip1";
			// 
			// projectToolStripMenuItem
			// 
			this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
			this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.projectToolStripMenuItem.Text = "Project";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.workSplit);
			this.Controls.Add(this.nsMenuStrip1);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(640, 400);
			this.Name = "MainForm";
			this.workSplit.Panel1.ResumeLayout(false);
			this.workSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.workSplit)).EndInit();
			this.workSplit.ResumeLayout(false);
			this.projectDirPanel.ResumeLayout(false);
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







	}
}