using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MannikToolbox
{
    public partial class MySqlLoginForm : Form
    {
        public MySqlLoginForm()
        {
            InitializeComponent();
        }

        private void MySqlLoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DatabaseManager.SetDatabaseConnection(txtbxHost.Text, uint.Parse(txtbxPort.Text), txtbxDB.Text, txtbxUser.Text, txtbxPwd.Text);

            try
            {
                DatabaseManager.Database.RegisterDataObject(typeof(DOL.Database.Mob));
                if (DatabaseManager.Database.GetObjectCount<DOL.Database.Mob>() >= 1)
                    MessageBox.Show("Connection Successful!");
            }
            catch { }

            Properties.Settings.Default.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Saved MySQL information.") == DialogResult.OK)
            {
                try
                {
                    Properties.Settings.Default.Username = txtbxUser.Text;
                    Properties.Settings.Default.Password = txtbxPwd.Text;
                    Properties.Settings.Default.Hostname = txtbxHost.Text;
                    Properties.Settings.Default.Port = uint.Parse(txtbxPort.Text);
                    Properties.Settings.Default.Database = txtbxDB.Text;
                    FindForm().Close();
                }
                catch (Exception g)
                {
                    MessageBox.Show(g.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
