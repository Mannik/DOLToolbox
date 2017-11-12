using DOLToolbox.Properties;

namespace DOLToolbox.Forms
{
    partial class MySqlConfig
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
			this.components = new System.ComponentModel.Container();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolstrip_status_label = new System.Windows.Forms.ToolStripStatusLabel();
			this.save_config_button = new System.Windows.Forms.Button();
			this.mysql_groupbox = new System.Windows.Forms.GroupBox();
			this.mysql_username_textbox = new System.Windows.Forms.TextBox();
			this.mysql_database_name_textbox = new System.Windows.Forms.TextBox();
			this.mysql_port_textbox = new System.Windows.Forms.TextBox();
			this.mysql_host_textbox = new System.Windows.Forms.TextBox();
			this.mysql_test_progressbar = new System.Windows.Forms.ProgressBar();
			this.mysql_says_label = new System.Windows.Forms.Label();
			this.mysql_password_textbox = new System.Windows.Forms.TextBox();
			this.mysql_test_label = new System.Windows.Forms.Label();
			this.mysql_test_button = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.mysql_test_background_worker = new System.ComponentModel.BackgroundWorker();
			this.wrong_data_error_handler = new System.Windows.Forms.ErrorProvider(this.components);
			this.statusStrip1.SuspendLayout();
			this.mysql_groupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.wrong_data_error_handler)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstrip_status_label});
			this.statusStrip1.Location = new System.Drawing.Point(0, 296);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(434, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 10;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolstrip_status_label
			// 
			this.toolstrip_status_label.Name = "toolstrip_status_label";
			this.toolstrip_status_label.Size = new System.Drawing.Size(181, 17);
			this.toolstrip_status_label.Text = "Ready to configurate your server.";
			// 
			// save_config_button
			// 
			this.save_config_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.save_config_button.Image = Resources.save_config_button_Image;
			this.save_config_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.save_config_button.Location = new System.Drawing.Point(278, 254);
			this.save_config_button.Name = "save_config_button";
			this.save_config_button.Size = new System.Drawing.Size(121, 23);
			this.save_config_button.TabIndex = 11;
			this.save_config_button.Text = "Save configuration";
			this.save_config_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.save_config_button.UseVisualStyleBackColor = true;
			this.save_config_button.Click += new System.EventHandler(this.save_config_button_Click);
			// 
			// mysql_groupbox
			// 
			this.mysql_groupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mysql_groupbox.Controls.Add(this.mysql_username_textbox);
			this.mysql_groupbox.Controls.Add(this.mysql_database_name_textbox);
			this.mysql_groupbox.Controls.Add(this.mysql_port_textbox);
			this.mysql_groupbox.Controls.Add(this.mysql_host_textbox);
			this.mysql_groupbox.Controls.Add(this.mysql_test_progressbar);
			this.mysql_groupbox.Controls.Add(this.mysql_says_label);
			this.mysql_groupbox.Controls.Add(this.mysql_password_textbox);
			this.mysql_groupbox.Controls.Add(this.mysql_test_label);
			this.mysql_groupbox.Controls.Add(this.mysql_test_button);
			this.mysql_groupbox.Controls.Add(this.label14);
			this.mysql_groupbox.Controls.Add(this.label13);
			this.mysql_groupbox.Controls.Add(this.label12);
			this.mysql_groupbox.Controls.Add(this.label11);
			this.mysql_groupbox.Controls.Add(this.label10);
			this.mysql_groupbox.Location = new System.Drawing.Point(12, 6);
			this.mysql_groupbox.Name = "mysql_groupbox";
			this.mysql_groupbox.Size = new System.Drawing.Size(387, 235);
			this.mysql_groupbox.TabIndex = 8;
			this.mysql_groupbox.TabStop = false;
			this.mysql_groupbox.Text = "MySQL Database settings";
			// 
			// mysql_username_textbox
			// 
			this.mysql_username_textbox.Location = new System.Drawing.Point(102, 102);
			this.mysql_username_textbox.Name = "mysql_username_textbox";
			this.mysql_username_textbox.Size = new System.Drawing.Size(260, 20);
			this.mysql_username_textbox.TabIndex = 4;
			this.mysql_username_textbox.Text = "dol";
			// 
			// mysql_database_name_textbox
			// 
			this.mysql_database_name_textbox.Location = new System.Drawing.Point(102, 76);
			this.mysql_database_name_textbox.Name = "mysql_database_name_textbox";
			this.mysql_database_name_textbox.Size = new System.Drawing.Size(260, 20);
			this.mysql_database_name_textbox.TabIndex = 3;
			this.mysql_database_name_textbox.Text = "dol";
			// 
			// mysql_port_textbox
			// 
			this.mysql_port_textbox.Location = new System.Drawing.Point(102, 50);
			this.mysql_port_textbox.Name = "mysql_port_textbox";
			this.mysql_port_textbox.Size = new System.Drawing.Size(61, 20);
			this.mysql_port_textbox.TabIndex = 2;
			this.mysql_port_textbox.Text = "3306";
			// 
			// mysql_host_textbox
			// 
			this.mysql_host_textbox.Location = new System.Drawing.Point(102, 24);
			this.mysql_host_textbox.Name = "mysql_host_textbox";
			this.mysql_host_textbox.Size = new System.Drawing.Size(260, 20);
			this.mysql_host_textbox.TabIndex = 1;
			this.mysql_host_textbox.Text = "localhost";
			// 
			// mysql_test_progressbar
			// 
			this.mysql_test_progressbar.Location = new System.Drawing.Point(217, 158);
			this.mysql_test_progressbar.Name = "mysql_test_progressbar";
			this.mysql_test_progressbar.Size = new System.Drawing.Size(97, 15);
			this.mysql_test_progressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.mysql_test_progressbar.TabIndex = 24;
			this.mysql_test_progressbar.Visible = false;
			// 
			// mysql_says_label
			// 
			this.mysql_says_label.AutoSize = true;
			this.mysql_says_label.Location = new System.Drawing.Point(6, 185);
			this.mysql_says_label.Name = "mysql_says_label";
			this.mysql_says_label.Size = new System.Drawing.Size(69, 13);
			this.mysql_says_label.TabIndex = 23;
			this.mysql_says_label.Text = "MySQL says:";
			// 
			// mysql_password_textbox
			// 
			this.mysql_password_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mysql_password_textbox.Location = new System.Drawing.Point(102, 128);
			this.mysql_password_textbox.Name = "mysql_password_textbox";
			this.mysql_password_textbox.PasswordChar = '*';
			this.mysql_password_textbox.Size = new System.Drawing.Size(260, 20);
			this.mysql_password_textbox.TabIndex = 5;
			// 
			// mysql_test_label
			// 
			this.mysql_test_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mysql_test_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mysql_test_label.ForeColor = System.Drawing.SystemColors.ControlText;
			this.mysql_test_label.Location = new System.Drawing.Point(99, 185);
			this.mysql_test_label.Name = "mysql_test_label";
			this.mysql_test_label.Size = new System.Drawing.Size(282, 47);
			this.mysql_test_label.TabIndex = 11;
			this.mysql_test_label.Text = "configure me ...";
			// 
			// mysql_test_button
			// 
			this.mysql_test_button.Image = Resources.mysql_test_button_Image;
			this.mysql_test_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.mysql_test_button.Location = new System.Drawing.Point(99, 154);
			this.mysql_test_button.Name = "mysql_test_button";
			this.mysql_test_button.Size = new System.Drawing.Size(109, 23);
			this.mysql_test_button.TabIndex = 6;
			this.mysql_test_button.Text = "Test connection";
			this.mysql_test_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.mysql_test_button.UseVisualStyleBackColor = true;
			this.mysql_test_button.Click += new System.EventHandler(this.mysql_test_button_Click);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 131);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(56, 13);
			this.label14.TabIndex = 8;
			this.label14.Text = "Password:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(6, 105);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(58, 13);
			this.label13.TabIndex = 6;
			this.label13.Text = "Username:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 79);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(87, 13);
			this.label12.TabIndex = 4;
			this.label12.Text = "Database Name:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 53);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(29, 13);
			this.label11.TabIndex = 2;
			this.label11.Text = "Port:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 27);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(82, 13);
			this.label10.TabIndex = 0;
			this.label10.Text = "Server Address:";
			// 
			// mysql_test_background_worker
			// 
			this.mysql_test_background_worker.WorkerReportsProgress = true;
			this.mysql_test_background_worker.WorkerSupportsCancellation = true;
			this.mysql_test_background_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mysql_test_background_worker_DoWork);
			this.mysql_test_background_worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mysql_test_background_worker_RunWorkerCompleted);
			// 
			// wrong_data_error_handler
			// 
			this.wrong_data_error_handler.ContainerControl = this;
			// 
			// MySqlConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 318);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.save_config_button);
			this.Controls.Add(this.mysql_groupbox);
			this.MaximumSize = new System.Drawing.Size(450, 357);
			this.MinimumSize = new System.Drawing.Size(450, 357);
			this.Name = "MySqlConfig";
			this.ShowIcon = false;
			this.Text = "MySQLConfig";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.mysql_groupbox.ResumeLayout(false);
			this.mysql_groupbox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.wrong_data_error_handler)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolstrip_status_label;
        private System.Windows.Forms.Button save_config_button;
        private System.Windows.Forms.GroupBox mysql_groupbox;
        private System.Windows.Forms.TextBox mysql_username_textbox;
        private System.Windows.Forms.TextBox mysql_database_name_textbox;
        private System.Windows.Forms.TextBox mysql_port_textbox;
        private System.Windows.Forms.TextBox mysql_host_textbox;
        private System.Windows.Forms.ProgressBar mysql_test_progressbar;
        private System.Windows.Forms.Label mysql_says_label;
        private System.Windows.Forms.TextBox mysql_password_textbox;
        private System.Windows.Forms.Label mysql_test_label;
        private System.Windows.Forms.Button mysql_test_button;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.ComponentModel.BackgroundWorker mysql_test_background_worker;
        private System.Windows.Forms.ErrorProvider wrong_data_error_handler;
    }
}