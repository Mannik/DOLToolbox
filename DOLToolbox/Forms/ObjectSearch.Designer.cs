namespace DOLToolbox.Forms
{
    partial class ObjectSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lblObject_ID = new System.Windows.Forms.Label();
            this.btnSelectNPC = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtFilterObject = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgd_ObjectSearch = new System.Windows.Forms.DataGridView();
            this.MobID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NPCName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NPCRegionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgd_ObjectSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(91, 812);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(37, 13);
            this.lblPage.TabIndex = 24;
            this.lblPage.Text = "Pages";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(224, 807);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(27, 23);
            this.btnNext.TabIndex = 23;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(46, 807);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "<<";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(191, 807);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(27, 23);
            this.btnLast.TabIndex = 22;
            this.btnLast.Text = ">>";
            this.btnLast.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(15, 807);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(25, 23);
            this.btnPrevious.TabIndex = 20;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // lblObject_ID
            // 
            this.lblObject_ID.AutoSize = true;
            this.lblObject_ID.Location = new System.Drawing.Point(666, 790);
            this.lblObject_ID.Name = "lblObject_ID";
            this.lblObject_ID.Size = new System.Drawing.Size(202, 13);
            this.lblObject_ID.TabIndex = 19;
            this.lblObject_ID.Text = "fd83f37b-e43a-412d-9d60-550a9bbf0449";
            // 
            // btnSelectNPC
            // 
            this.btnSelectNPC.Location = new System.Drawing.Point(642, 806);
            this.btnSelectNPC.Name = "btnSelectNPC";
            this.btnSelectNPC.Size = new System.Drawing.Size(248, 23);
            this.btnSelectNPC.TabIndex = 18;
            this.btnSelectNPC.Text = "Select this NPC";
            this.btnSelectNPC.UseVisualStyleBackColor = true;
            this.btnSelectNPC.Click += new System.EventHandler(this.btnSelectNPC_Click_1);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(482, 807);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(48, 23);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // txtFilterObject
            // 
            this.txtFilterObject.Location = new System.Drawing.Point(257, 809);
            this.txtFilterObject.Name = "txtFilterObject";
            this.txtFilterObject.Size = new System.Drawing.Size(151, 20);
            this.txtFilterObject.TabIndex = 16;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(414, 807);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // dgd_ObjectSearch
            // 
            this.dgd_ObjectSearch.AllowUserToOrderColumns = true;
            this.dgd_ObjectSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgd_ObjectSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgd_ObjectSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MobID,
            this.NPCName,
            this.Model,
            this.NPCRegionID});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgd_ObjectSearch.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgd_ObjectSearch.Location = new System.Drawing.Point(12, 12);
            this.dgd_ObjectSearch.MultiSelect = false;
            this.dgd_ObjectSearch.Name = "dgd_ObjectSearch";
            this.dgd_ObjectSearch.ReadOnly = true;
            this.dgd_ObjectSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgd_ObjectSearch.Size = new System.Drawing.Size(518, 789);
            this.dgd_ObjectSearch.TabIndex = 13;
            // 
            // MobID
            // 
            this.MobID.FillWeight = 5.076142F;
            this.MobID.HeaderText = "Mob_ID";
            this.MobID.MinimumWidth = 2;
            this.MobID.Name = "MobID";
            this.MobID.ReadOnly = true;
            this.MobID.Visible = false;
            // 
            // NPCName
            // 
            this.NPCName.FillWeight = 123.731F;
            this.NPCName.HeaderText = "Name";
            this.NPCName.Name = "NPCName";
            this.NPCName.ReadOnly = true;
            // 
            // Model
            // 
            this.Model.FillWeight = 60F;
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            // 
            // NPCRegionID
            // 
            this.NPCRegionID.FillWeight = 60F;
            this.NPCRegionID.HeaderText = "Region ID";
            this.NPCRegionID.Name = "NPCRegionID";
            this.NPCRegionID.ReadOnly = true;
            // 
            // ObjectSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 862);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.lblObject_ID);
            this.Controls.Add(this.btnSelectNPC);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtFilterObject);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgd_ObjectSearch);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjectSearch";
            this.Text = "Object Search Form";
            this.Load += new System.EventHandler(this.ObjectSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgd_ObjectSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblObject_ID;
        private System.Windows.Forms.Button btnSelectNPC;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtFilterObject;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgd_ObjectSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPCRegionID;
    }
}