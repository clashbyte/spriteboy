namespace SpriteBoy.Forms.Editors {
	partial class ModelConverterForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelConverterForm));
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader4 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader5 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			SpriteBoy.Controls.NSListView.NSListViewColumnHeader nsListViewColumnHeader6 = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader();
			this.nsLabel1 = new SpriteBoy.Controls.NSLabel();
			this.convertButton = new SpriteBoy.Controls.NSButton();
			this.openButton = new SpriteBoy.Controls.NSButton();
			this.progressList = new SpriteBoy.Controls.NSListView();
			this.SuspendLayout();
			// 
			// nsLabel1
			// 
			this.nsLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nsLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.nsLabel1.ForeColor = System.Drawing.Color.White;
			this.nsLabel1.Location = new System.Drawing.Point(12, 12);
			this.nsLabel1.MaximumSize = new System.Drawing.Size(600, 59);
			this.nsLabel1.Name = "nsLabel1";
			this.nsLabel1.Size = new System.Drawing.Size(482, 59);
			this.nsLabel1.TabIndex = 0;
			this.nsLabel1.Text = resources.GetString("nsLabel1.Text");
			this.nsLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// convertButton
			// 
			this.convertButton.Location = new System.Drawing.Point(12, 77);
			this.convertButton.Name = "convertButton";
			this.convertButton.Size = new System.Drawing.Size(120, 23);
			this.convertButton.TabIndex = 1;
			this.convertButton.Text = "Convert";
			// 
			// openButton
			// 
			this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.openButton.Location = new System.Drawing.Point(314, 77);
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(180, 23);
			this.openButton.TabIndex = 2;
			this.openButton.Text = "Open converted";
			// 
			// progressList
			// 
			this.progressList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			nsListViewColumnHeader4.Text = null;
			nsListViewColumnHeader4.Width = 20;
			nsListViewColumnHeader5.Text = null;
			nsListViewColumnHeader5.Width = 100;
			nsListViewColumnHeader6.Text = null;
			nsListViewColumnHeader6.Width = 999;
			this.progressList.Columns = new SpriteBoy.Controls.NSListView.NSListViewColumnHeader[] {
        nsListViewColumnHeader4,
        nsListViewColumnHeader5,
        nsListViewColumnHeader6};
			this.progressList.Items = new SpriteBoy.Controls.NSListView.NSListViewItem[0];
			this.progressList.Location = new System.Drawing.Point(12, 106);
			this.progressList.MultiSelect = true;
			this.progressList.Name = "progressList";
			this.progressList.Size = new System.Drawing.Size(482, 278);
			this.progressList.TabIndex = 3;
			this.progressList.Text = "nsListView1";
			// 
			// ModelConverterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(506, 396);
			this.Controls.Add(this.progressList);
			this.Controls.Add(this.openButton);
			this.Controls.Add(this.convertButton);
			this.Controls.Add(this.nsLabel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
			this.Name = "ModelConverterForm";
			this.Text = "Model Importer";
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.NSLabel nsLabel1;
		private Controls.NSButton convertButton;
		private Controls.NSButton openButton;
		private Controls.NSListView progressList;
	}
}