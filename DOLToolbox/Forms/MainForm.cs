using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOLToolbox.Controls;
using DOLToolbox.Services;
using MySql.Data.MySqlClient;

namespace DOLToolbox.Forms
{
    public partial class MainForm : Form
    {
	    public MainForm()
        {
			InitializeComponent();
        }

		private async void MainForm_Load(object sender, EventArgs e)
		{
            // do not open app until db connection is valid
		    bool isDbConnected = false;
		    while (!isDbConnected)
		    {
		        MySqlConnectionStringBuilder sb = ConnectionStringService.ConnectionString;
		        MySqlConnection testConnection = new MySqlConnection(sb.ConnectionString);

		        try
		        {
		            testConnection.Open();
		        }
		        catch (MySqlException ex)
		        {
		            var response = MessageBox.Show($@"DB connection error: {ex.Message}", @"DB Connection Error", MessageBoxButtons.RetryCancel);

		            if (response == DialogResult.Cancel)
		            {
		                Application.Exit();
		                return;
		            }

		            new MySqlConfig().ShowDialog(this);
		        }

		        isDbConnected = testConnection.State == System.Data.ConnectionState.Open;
		    }

            var loading = new LoadingForm();
			//prevent clicking behind berfore connections made : Loki
			ToolboxTabControl.Enabled = false;
			loading.Show();
		    var progress = new Progress<int>(percent =>
		    {
		        loading.ProgressBar.Value = percent;

		        var item = percent / (100 / DatabaseManager.RegisteredObjects.Length);

		        if (item < DatabaseManager.RegisteredObjects.Length)
		        {
		            var type = DatabaseManager.RegisteredObjects[item].Name;
		            loading.ProgressText.Text = $@"Loading: {type}";
		        }
		    });

            // doing this to start loading the db
            await Task.Run(() =>
            {
                DatabaseManager.SetDatabaseConnection(progress);
            });

            loading.ProgressText.Text = @"Loading: Model Viewer";
		    await Task.Run(() => new ModelViewerService());

            loading.Close();
            ToolboxTabControl.Enabled = true;
            Text = $@"Dawn of Light Database Toolbox ({ConnectionStringService.ConnectionString.Server})";
		    LoadTabForms();
		}
        
        private void LoadTabForms()
        {
            UserControl control = new SpellControl { Dock = DockStyle.Fill };
            tabSpell.Controls.Add(control);

            control = new MobControl { Dock = DockStyle.Fill };
            tabMob.Controls.Add(control);

            control = new ServerDetailsControl { Dock = DockStyle.Fill };
            tabServerDetails.Controls.Add(control);

            control = new NpcTemplateControl { Dock = DockStyle.Fill };
            tabNPCTemplate.Controls.Add(control);

            control = new ItemTemplateControl { Dock = DockStyle.Fill };
            tabItem.Controls.Add(control);

            control = new MerchantItemsControl { Dock = DockStyle.Fill };
            tabMerchantItems.Controls.Add(control);

            control = new NpcEquipmentControl { Dock = DockStyle.Fill };
            tabNpcEquipment.Controls.Add(control);

            control = new LootTemplateControl { Dock = DockStyle.Fill };
            tabLootTemplate.Controls.Add(control);
        }

		private void Menu_DB_Click(object sender, EventArgs e)
        {
			new MySqlConfig().ShowDialog(this);
			Application.Restart();
			Environment.Exit(0);
		}
    }
}