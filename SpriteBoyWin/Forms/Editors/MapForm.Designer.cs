using SpriteBoy.Controls;
namespace SpriteBoy.Forms.Editors {
	partial class MapForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.nsTabControl1 = new NSTabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.nsRadioIconicButton5 = new NSRadioIconicButton();
			this.nsRadioIconicButton4 = new NSRadioIconicButton();
			this.nsRadioIconicButton3 = new NSRadioIconicButton();
			this.nsRadioIconicButton2 = new NSRadioIconicButton();
			this.nsRadioIconicButton1 = new NSRadioIconicButton();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.nsTabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(807, 25);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.nsTabControl1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(45, 354);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(762, 200);
			this.panel2.TabIndex = 1;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.nsRadioIconicButton5);
			this.panel3.Controls.Add(this.nsRadioIconicButton4);
			this.panel3.Controls.Add(this.nsRadioIconicButton3);
			this.panel3.Controls.Add(this.nsRadioIconicButton2);
			this.panel3.Controls.Add(this.nsRadioIconicButton1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.Location = new System.Drawing.Point(0, 25);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(45, 529);
			this.panel3.TabIndex = 2;
			// 
			// nsTabControl1
			// 
			this.nsTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.nsTabControl1.Controls.Add(this.tabPage1);
			this.nsTabControl1.Controls.Add(this.tabPage2);
			this.nsTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nsTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.nsTabControl1.ItemSize = new System.Drawing.Size(28, 115);
			this.nsTabControl1.Location = new System.Drawing.Point(0, 0);
			this.nsTabControl1.Multiline = true;
			this.nsTabControl1.Name = "nsTabControl1";
			this.nsTabControl1.SelectedIndex = 0;
			this.nsTabControl1.Size = new System.Drawing.Size(762, 200);
			this.nsTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.nsTabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.tabPage1.Location = new System.Drawing.Point(119, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(639, 192);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.tabPage2.Location = new System.Drawing.Point(119, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(639, 192);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// nsRadioIconicButton5
			// 
			this.nsRadioIconicButton5.Checked = false;
			this.nsRadioIconicButton5.Corners.BottomLeft = true;
			this.nsRadioIconicButton5.Corners.BottomRight = true;
			this.nsRadioIconicButton5.Corners.TopLeft = false;
			this.nsRadioIconicButton5.Corners.TopRight = false;
			this.nsRadioIconicButton5.IconImage = ((System.Drawing.Image)(resources.GetObject("nsRadioIconicButton5.IconImage")));
			this.nsRadioIconicButton5.IconSize = new System.Drawing.Size(28, 28);
			this.nsRadioIconicButton5.Large = true;
			this.nsRadioIconicButton5.Location = new System.Drawing.Point(0, 174);
			this.nsRadioIconicButton5.Name = "nsRadioIconicButton5";
			this.nsRadioIconicButton5.Size = new System.Drawing.Size(42, 42);
			this.nsRadioIconicButton5.TabIndex = 4;
			// 
			// nsRadioIconicButton4
			// 
			this.nsRadioIconicButton4.Checked = false;
			this.nsRadioIconicButton4.Corners.BottomLeft = false;
			this.nsRadioIconicButton4.Corners.BottomRight = false;
			this.nsRadioIconicButton4.Corners.TopLeft = false;
			this.nsRadioIconicButton4.Corners.TopRight = false;
			this.nsRadioIconicButton4.IconImage = ((System.Drawing.Image)(resources.GetObject("nsRadioIconicButton4.IconImage")));
			this.nsRadioIconicButton4.IconSize = new System.Drawing.Size(30, 30);
			this.nsRadioIconicButton4.Large = true;
			this.nsRadioIconicButton4.Location = new System.Drawing.Point(0, 132);
			this.nsRadioIconicButton4.Name = "nsRadioIconicButton4";
			this.nsRadioIconicButton4.Size = new System.Drawing.Size(42, 42);
			this.nsRadioIconicButton4.TabIndex = 3;
			// 
			// nsRadioIconicButton3
			// 
			this.nsRadioIconicButton3.Checked = false;
			this.nsRadioIconicButton3.Corners.BottomLeft = false;
			this.nsRadioIconicButton3.Corners.BottomRight = false;
			this.nsRadioIconicButton3.Corners.TopLeft = true;
			this.nsRadioIconicButton3.Corners.TopRight = true;
			this.nsRadioIconicButton3.IconImage = ((System.Drawing.Image)(resources.GetObject("nsRadioIconicButton3.IconImage")));
			this.nsRadioIconicButton3.IconSize = new System.Drawing.Size(28, 28);
			this.nsRadioIconicButton3.Large = true;
			this.nsRadioIconicButton3.Location = new System.Drawing.Point(0, 90);
			this.nsRadioIconicButton3.Name = "nsRadioIconicButton3";
			this.nsRadioIconicButton3.Size = new System.Drawing.Size(42, 42);
			this.nsRadioIconicButton3.TabIndex = 2;
			// 
			// nsRadioIconicButton2
			// 
			this.nsRadioIconicButton2.Checked = false;
			this.nsRadioIconicButton2.Corners.BottomLeft = true;
			this.nsRadioIconicButton2.Corners.BottomRight = true;
			this.nsRadioIconicButton2.Corners.TopLeft = false;
			this.nsRadioIconicButton2.Corners.TopRight = false;
			this.nsRadioIconicButton2.IconImage = ((System.Drawing.Image)(resources.GetObject("nsRadioIconicButton2.IconImage")));
			this.nsRadioIconicButton2.IconSize = new System.Drawing.Size(28, 28);
			this.nsRadioIconicButton2.Large = true;
			this.nsRadioIconicButton2.Location = new System.Drawing.Point(0, 42);
			this.nsRadioIconicButton2.Name = "nsRadioIconicButton2";
			this.nsRadioIconicButton2.Size = new System.Drawing.Size(42, 42);
			this.nsRadioIconicButton2.TabIndex = 1;
			// 
			// nsRadioIconicButton1
			// 
			this.nsRadioIconicButton1.Checked = true;
			this.nsRadioIconicButton1.Corners.BottomLeft = false;
			this.nsRadioIconicButton1.Corners.BottomRight = false;
			this.nsRadioIconicButton1.Corners.TopLeft = true;
			this.nsRadioIconicButton1.Corners.TopRight = true;
			this.nsRadioIconicButton1.IconImage = ((System.Drawing.Image)(resources.GetObject("nsRadioIconicButton1.IconImage")));
			this.nsRadioIconicButton1.IconSize = new System.Drawing.Size(26, 26);
			this.nsRadioIconicButton1.Large = true;
			this.nsRadioIconicButton1.Location = new System.Drawing.Point(0, 0);
			this.nsRadioIconicButton1.Name = "nsRadioIconicButton1";
			this.nsRadioIconicButton1.Size = new System.Drawing.Size(42, 42);
			this.nsRadioIconicButton1.TabIndex = 0;
			// 
			// MapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(807, 554);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MapForm";
			this.Text = "MapForm";
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.nsTabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private NSRadioIconicButton nsRadioIconicButton1;
		private NSRadioIconicButton nsRadioIconicButton2;
		private NSTabControl nsTabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private NSRadioIconicButton nsRadioIconicButton3;
		private NSRadioIconicButton nsRadioIconicButton5;
		private NSRadioIconicButton nsRadioIconicButton4;
	}
}