namespace DOLToolbox.Forms
{
    partial class ModelViewerSearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelViewerSearchForm));
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.rdMob = new System.Windows.Forms.RadioButton();
			this.rdItem = new System.Windows.Forms.RadioButton();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(12, 39);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(1181, 766);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
			// 
			// rdMob
			// 
			this.rdMob.AutoSize = true;
			this.rdMob.Location = new System.Drawing.Point(60, 13);
			this.rdMob.Name = "rdMob";
			this.rdMob.Size = new System.Drawing.Size(51, 17);
			this.rdMob.TabIndex = 1;
			this.rdMob.Text = "Mobs";
			this.rdMob.UseVisualStyleBackColor = true;
			this.rdMob.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// rdItem
			// 
			this.rdItem.AutoSize = true;
			this.rdItem.Checked = true;
			this.rdItem.Location = new System.Drawing.Point(4, 13);
			this.rdItem.Name = "rdItem";
			this.rdItem.Size = new System.Drawing.Size(50, 17);
			this.rdItem.TabIndex = 2;
			this.rdItem.TabStop = true;
			this.rdItem.Text = "Items";
			this.rdItem.UseVisualStyleBackColor = true;
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(158, 12);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(272, 20);
			this.txtSearch.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(117, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Search";
			// 
			// cboType
			// 
			this.cboType.FormattingEnabled = true;
			this.cboType.Location = new System.Drawing.Point(436, 12);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(133, 21);
			this.cboType.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(575, 10);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Search";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(1199, 39);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(234, 302);
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(1339, 782);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(94, 23);
			this.button2.TabIndex = 8;
			this.button2.Text = "Use Selected";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// ModelViewerSearchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1445, 817);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cboType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.rdItem);
			this.Controls.Add(this.rdMob);
			this.Controls.Add(this.dataGridView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ModelViewerSearchForm";
			this.Text = "Model Viewer Search Form";
			this.Load += new System.EventHandler(this.ModelViewerSearchForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton rdMob;
        private System.Windows.Forms.RadioButton rdItem;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
    }
}