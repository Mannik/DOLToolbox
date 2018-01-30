namespace DOLToolbox.Forms
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.ToolboxTabControl = new System.Windows.Forms.TabControl();
			this.tabSpell = new System.Windows.Forms.TabPage();
			this.tabItem = new System.Windows.Forms.TabPage();
			this.tabNPCTemplate = new System.Windows.Forms.TabPage();
			this.tabQuest = new System.Windows.Forms.TabPage();
			this.tabMob = new System.Windows.Forms.TabPage();
			this.tabMerchantItems = new System.Windows.Forms.TabPage();
			this.tabNpcEquipment = new System.Windows.Forms.TabPage();
			this.tabLootTemplate = new System.Windows.Forms.TabPage();
			this.tabServerDetails = new System.Windows.Forms.TabPage();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_New = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_DB = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolboxTabControl.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ToolboxTabControl
			// 
			this.ToolboxTabControl.Controls.Add(this.tabSpell);
			this.ToolboxTabControl.Controls.Add(this.tabItem);
			this.ToolboxTabControl.Controls.Add(this.tabNPCTemplate);
			this.ToolboxTabControl.Controls.Add(this.tabQuest);
			this.ToolboxTabControl.Controls.Add(this.tabMob);
			this.ToolboxTabControl.Controls.Add(this.tabMerchantItems);
			this.ToolboxTabControl.Controls.Add(this.tabNpcEquipment);
			this.ToolboxTabControl.Controls.Add(this.tabLootTemplate);
			this.ToolboxTabControl.Controls.Add(this.tabServerDetails);
			this.ToolboxTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ToolboxTabControl.Location = new System.Drawing.Point(0, 24);
			this.ToolboxTabControl.Name = "ToolboxTabControl";
			this.ToolboxTabControl.SelectedIndex = 0;
			this.ToolboxTabControl.Size = new System.Drawing.Size(893, 555);
			this.ToolboxTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.ToolboxTabControl.TabIndex = 3;
			// 
			// tabSpell
			// 
			this.tabSpell.BackColor = System.Drawing.Color.LightGray;
			this.tabSpell.Location = new System.Drawing.Point(4, 22);
			this.tabSpell.Name = "tabSpell";
			this.tabSpell.Padding = new System.Windows.Forms.Padding(3);
			this.tabSpell.Size = new System.Drawing.Size(885, 529);
			this.tabSpell.TabIndex = 0;
			this.tabSpell.Text = "Spell";
			// 
			// tabItem
			// 
			this.tabItem.BackColor = System.Drawing.Color.LightGray;
			this.tabItem.Location = new System.Drawing.Point(4, 22);
			this.tabItem.Name = "tabItem";
			this.tabItem.Padding = new System.Windows.Forms.Padding(3);
			this.tabItem.Size = new System.Drawing.Size(885, 529);
			this.tabItem.TabIndex = 1;
			this.tabItem.Text = "Item";
			// 
			// tabNPCTemplate
			// 
			this.tabNPCTemplate.BackColor = System.Drawing.Color.LightGray;
			this.tabNPCTemplate.Location = new System.Drawing.Point(4, 22);
			this.tabNPCTemplate.Name = "tabNPCTemplate";
			this.tabNPCTemplate.Size = new System.Drawing.Size(885, 529);
			this.tabNPCTemplate.TabIndex = 2;
			this.tabNPCTemplate.Text = "Npctemplate";
			// 
			// tabQuest
			// 
			this.tabQuest.BackColor = System.Drawing.Color.LightGray;
			this.tabQuest.Location = new System.Drawing.Point(4, 22);
			this.tabQuest.Name = "tabQuest";
			this.tabQuest.Size = new System.Drawing.Size(885, 529);
			this.tabQuest.TabIndex = 3;
			this.tabQuest.Text = "Quest";
			// 
			// tabMob
			// 
			this.tabMob.Location = new System.Drawing.Point(4, 22);
			this.tabMob.Name = "tabMob";
			this.tabMob.Padding = new System.Windows.Forms.Padding(3);
			this.tabMob.Size = new System.Drawing.Size(885, 529);
			this.tabMob.TabIndex = 4;
			this.tabMob.Text = "Mob";
			this.tabMob.UseVisualStyleBackColor = true;
			// 
			// tabMerchantItems
			// 
			this.tabMerchantItems.Location = new System.Drawing.Point(4, 22);
			this.tabMerchantItems.Name = "tabMerchantItems";
			this.tabMerchantItems.Size = new System.Drawing.Size(885, 529);
			this.tabMerchantItems.TabIndex = 6;
			this.tabMerchantItems.Text = "Merchant Items";
			this.tabMerchantItems.UseVisualStyleBackColor = true;
			// 
			// tabNpcEquipment
			// 
			this.tabNpcEquipment.Location = new System.Drawing.Point(4, 22);
			this.tabNpcEquipment.Name = "tabNpcEquipment";
			this.tabNpcEquipment.Size = new System.Drawing.Size(885, 529);
			this.tabNpcEquipment.TabIndex = 7;
			this.tabNpcEquipment.Text = "NPC Equipment";
			this.tabNpcEquipment.UseVisualStyleBackColor = true;
			// 
			// tabLootTemplate
			// 
			this.tabLootTemplate.Location = new System.Drawing.Point(4, 22);
			this.tabLootTemplate.Name = "tabLootTemplate";
			this.tabLootTemplate.Size = new System.Drawing.Size(885, 529);
			this.tabLootTemplate.TabIndex = 8;
			this.tabLootTemplate.Text = "Loot Template";
			this.tabLootTemplate.UseVisualStyleBackColor = true;
			// 
			// tabServerDetails
			// 
			this.tabServerDetails.BackColor = System.Drawing.Color.White;
			this.tabServerDetails.Location = new System.Drawing.Point(4, 22);
			this.tabServerDetails.Name = "tabServerDetails";
			this.tabServerDetails.Size = new System.Drawing.Size(885, 529);
			this.tabServerDetails.TabIndex = 0;
			this.tabServerDetails.Text = "Server Details";
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.Gray;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(893, 24);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_New});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// Menu_New
			// 
			this.Menu_New.Name = "Menu_New";
			this.Menu_New.Size = new System.Drawing.Size(98, 22);
			this.Menu_New.Text = "New";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_DB});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// Menu_DB
			// 
			this.Menu_DB.Name = "Menu_DB";
			this.Menu_DB.Size = new System.Drawing.Size(221, 22);
			this.Menu_DB.Text = "Change MySQL Credentials ";
			this.Menu_DB.Click += new System.EventHandler(this.Menu_DB_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(893, 579);
			this.Controls.Add(this.ToolboxTabControl);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(909, 618);
			this.MinimumSize = new System.Drawing.Size(909, 618);
			this.Name = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ToolboxTabControl.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl ToolboxTabControl;
        private System.Windows.Forms.TabPage tabSpell;
        private System.Windows.Forms.TabPage tabItem;
        private System.Windows.Forms.TabPage tabNPCTemplate;
        private System.Windows.Forms.TabPage tabQuest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_New;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_DB;
		private System.Windows.Forms.TabPage tabServerDetails;
        private System.Windows.Forms.TabPage tabMob;
        private System.Windows.Forms.TabPage tabMerchantItems;
        private System.Windows.Forms.TabPage tabNpcEquipment;
        private System.Windows.Forms.TabPage tabLootTemplate;
    }
}

