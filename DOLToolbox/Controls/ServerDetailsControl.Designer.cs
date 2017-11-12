namespace DOLToolbox.Controls
{
    partial class ServerDetailsControl
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
            this.lblCPUUsage = new System.Windows.Forms.Label();
            this.ServerIP = new System.Windows.Forms.Label();
            this.lblAccounts = new System.Windows.Forms.Label();
            this.lblChrCreated = new System.Windows.Forms.Label();
            this.lblBugReports = new System.Windows.Forms.Label();
            this.lblAppeals = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCPUUsage
            // 
            this.lblCPUUsage.AutoSize = true;
            this.lblCPUUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPUUsage.Location = new System.Drawing.Point(3, 494);
            this.lblCPUUsage.Name = "lblCPUUsage";
            this.lblCPUUsage.Size = new System.Drawing.Size(93, 17);
            this.lblCPUUsage.TabIndex = 13;
            this.lblCPUUsage.Text = "CPU Usage =";
            // 
            // ServerIP
            // 
            this.ServerIP.AutoSize = true;
            this.ServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerIP.Location = new System.Drawing.Point(3, 10);
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Size = new System.Drawing.Size(131, 17);
            this.ServerIP.TabIndex = 10;
            this.ServerIP.Text = "Database Server IP";
            // 
            // lblAccounts
            // 
            this.lblAccounts.AutoSize = true;
            this.lblAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccounts.Location = new System.Drawing.Point(3, 38);
            this.lblAccounts.Name = "lblAccounts";
            this.lblAccounts.Size = new System.Drawing.Size(66, 17);
            this.lblAccounts.TabIndex = 8;
            this.lblAccounts.Text = "Accounts";
            // 
            // lblChrCreated
            // 
            this.lblChrCreated.AutoSize = true;
            this.lblChrCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChrCreated.Location = new System.Drawing.Point(3, 66);
            this.lblChrCreated.Name = "lblChrCreated";
            this.lblChrCreated.Size = new System.Drawing.Size(131, 17);
            this.lblChrCreated.TabIndex = 9;
            this.lblChrCreated.Text = "Characters Created";
            // 
            // lblBugReports
            // 
            this.lblBugReports.AutoSize = true;
            this.lblBugReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBugReports.Location = new System.Drawing.Point(3, 94);
            this.lblBugReports.Name = "lblBugReports";
            this.lblBugReports.Size = new System.Drawing.Size(87, 17);
            this.lblBugReports.TabIndex = 12;
            this.lblBugReports.Text = "Bug Reports";
            // 
            // lblAppeals
            // 
            this.lblAppeals.AutoSize = true;
            this.lblAppeals.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppeals.Location = new System.Drawing.Point(3, 122);
            this.lblAppeals.Name = "lblAppeals";
            this.lblAppeals.Size = new System.Drawing.Size(119, 17);
            this.lblAppeals.TabIndex = 11;
            this.lblAppeals.Text = "Pending Appeals ";
            // 
            // ServerDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCPUUsage);
            this.Controls.Add(this.ServerIP);
            this.Controls.Add(this.lblAccounts);
            this.Controls.Add(this.lblChrCreated);
            this.Controls.Add(this.lblBugReports);
            this.Controls.Add(this.lblAppeals);
            this.Name = "ServerDetailsControl";
            this.Size = new System.Drawing.Size(885, 529);
            this.Load += new System.EventHandler(this.ServerDetailsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCPUUsage;
        private System.Windows.Forms.Label ServerIP;
        private System.Windows.Forms.Label lblAccounts;
        private System.Windows.Forms.Label lblChrCreated;
        private System.Windows.Forms.Label lblBugReports;
        private System.Windows.Forms.Label lblAppeals;
    }
}
