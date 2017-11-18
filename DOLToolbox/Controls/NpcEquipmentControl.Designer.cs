namespace DOLToolbox.Controls
{
    partial class NpcEquipmentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClear = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this._Slot = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._TemplateID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._Effect = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this._Extension = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this._Color = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this._Model = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(807, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(84, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "New";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(3, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 14;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 32);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(236, 494);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this._Slot);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this._TemplateID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._Effect);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Controls.Add(this._Extension);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this._Color);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this._Model);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(245, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 494);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(247, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 20;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _Slot
            // 
            this._Slot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._Slot.FormattingEnabled = true;
            this._Slot.Location = new System.Drawing.Point(76, 18);
            this._Slot.Name = "_Slot";
            this._Slot.Size = new System.Drawing.Size(246, 21);
            this._Slot.TabIndex = 111;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 109;
            this.label2.Text = "Template ID";
            // 
            // _TemplateID
            // 
            this._TemplateID.Location = new System.Drawing.Point(76, 151);
            this._TemplateID.Name = "_TemplateID";
            this._TemplateID.Size = new System.Drawing.Size(246, 20);
            this._TemplateID.TabIndex = 110;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 107;
            this.label1.Text = "Slot";
            // 
            // _Effect
            // 
            this._Effect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._Effect.FormattingEnabled = true;
            this._Effect.Location = new System.Drawing.Point(76, 97);
            this._Effect.Name = "_Effect";
            this._Effect.Size = new System.Drawing.Size(246, 21);
            this._Effect.TabIndex = 106;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(35, 101);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(35, 13);
            this.label54.TabIndex = 105;
            this.label54.Text = "Effect";
            // 
            // _Extension
            // 
            this._Extension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._Extension.FormattingEnabled = true;
            this._Extension.Location = new System.Drawing.Point(76, 124);
            this._Extension.Name = "_Extension";
            this._Extension.Size = new System.Drawing.Size(246, 21);
            this._Extension.TabIndex = 104;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(17, 127);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(53, 13);
            this.label52.TabIndex = 103;
            this.label52.Text = "Extension";
            // 
            // _Color
            // 
            this._Color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._Color.FormattingEnabled = true;
            this._Color.Location = new System.Drawing.Point(76, 70);
            this._Color.Name = "_Color";
            this._Color.Size = new System.Drawing.Size(246, 21);
            this._Color.TabIndex = 102;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(39, 73);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(31, 13);
            this.label35.TabIndex = 101;
            this.label35.Text = "Color";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(34, 47);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(36, 13);
            this.label34.TabIndex = 24;
            this.label34.Text = "Model";
            // 
            // _Model
            // 
            this._Model.Location = new System.Drawing.Point(76, 44);
            this._Model.Name = "_Model";
            this._Model.Size = new System.Drawing.Size(165, 20);
            this._Model.TabIndex = 25;
            this._Model.Leave += new System.EventHandler(this._Model_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(328, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(303, 337);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(247, 177);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // NpcEquipmentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnLoad);
            this.Name = "NpcEquipmentControl";
            this.Size = new System.Drawing.Size(885, 529);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox _Model;
        private System.Windows.Forms.ComboBox _Effect;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ComboBox _Extension;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ComboBox _Color;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _TemplateID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _Slot;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}
