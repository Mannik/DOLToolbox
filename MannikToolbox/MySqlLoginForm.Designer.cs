namespace MannikToolbox
{
    partial class MySqlLoginForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtbxUser = new System.Windows.Forms.TextBox();
            this.txtbxPwd = new System.Windows.Forms.TextBox();
            this.txtbxHost = new System.Windows.Forms.TextBox();
            this.txtbxPort = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtbxDB = new System.Windows.Forms.TextBox();
            this.lblDB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(261, 138);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(29, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "User";
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Location = new System.Drawing.Point(13, 45);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(53, 13);
            this.lblPwd.TabIndex = 2;
            this.lblPwd.Text = "Password";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(13, 76);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(29, 13);
            this.lblHost.TabIndex = 3;
            this.lblHost.Text = "Host";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(13, 110);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "Port";
            // 
            // txtbxUser
            // 
            this.txtbxUser.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MannikToolbox.Properties.Settings.Default, "User", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtbxUser.Location = new System.Drawing.Point(72, 10);
            this.txtbxUser.Name = "txtbxUser";
            this.txtbxUser.Size = new System.Drawing.Size(100, 20);
            this.txtbxUser.TabIndex = 5;
            this.txtbxUser.Text = global::MannikToolbox.Properties.Settings.Default.Username;
            // 
            // txtbxPwd
            // 
            this.txtbxPwd.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MannikToolbox.Properties.Settings.Default, "Pwd", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtbxPwd.Location = new System.Drawing.Point(72, 42);
            this.txtbxPwd.Name = "txtbxPwd";
            this.txtbxPwd.PasswordChar = '*';
            this.txtbxPwd.Size = new System.Drawing.Size(100, 20);
            this.txtbxPwd.TabIndex = 6;
            this.txtbxPwd.Text = global::MannikToolbox.Properties.Settings.Default.Password;
            // 
            // txtbxHost
            // 
            this.txtbxHost.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MannikToolbox.Properties.Settings.Default, "Host", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtbxHost.Location = new System.Drawing.Point(72, 73);
            this.txtbxHost.Name = "txtbxHost";
            this.txtbxHost.Size = new System.Drawing.Size(100, 20);
            this.txtbxHost.TabIndex = 7;
            this.txtbxHost.Text = global::MannikToolbox.Properties.Settings.Default.Hostname;
            // 
            // txtbxPort
            // 
            this.txtbxPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MannikToolbox.Properties.Settings.Default, "Port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtbxPort.Location = new System.Drawing.Point(72, 107);
            this.txtbxPort.Name = "txtbxPort";
            this.txtbxPort.Size = new System.Drawing.Size(100, 20);
            this.txtbxPort.TabIndex = 8;
            this.txtbxPort.Text = "3306";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(261, 66);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 37);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "Test Connection";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtbxDB
            // 
            this.txtbxDB.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MannikToolbox.Properties.Settings.Default, "Database", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtbxDB.Location = new System.Drawing.Point(72, 138);
            this.txtbxDB.Name = "txtbxDB";
            this.txtbxDB.Size = new System.Drawing.Size(100, 20);
            this.txtbxDB.TabIndex = 11;
            this.txtbxDB.Text = global::MannikToolbox.Properties.Settings.Default.Database;
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(13, 141);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(53, 13);
            this.lblDB.TabIndex = 10;
            this.lblDB.Text = "Database";
            // 
            // MySqlLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 173);
            this.Controls.Add(this.txtbxDB);
            this.Controls.Add(this.lblDB);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtbxPort);
            this.Controls.Add(this.txtbxHost);
            this.Controls.Add(this.txtbxPwd);
            this.Controls.Add(this.txtbxUser);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.lblPwd);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnSave);
            this.Name = "MySqlLoginForm";
            this.Text = "DBConnectionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtbxUser;
        private System.Windows.Forms.TextBox txtbxPwd;
        private System.Windows.Forms.TextBox txtbxHost;
        private System.Windows.Forms.TextBox txtbxPort;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtbxDB;
        private System.Windows.Forms.Label lblDB;
    }
}