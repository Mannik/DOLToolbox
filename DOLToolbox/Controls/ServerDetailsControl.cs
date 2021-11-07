using System;
using System.Diagnostics;
using System.Windows.Forms;
using DOL.Database;
using DOL.Database.Handlers;
using DOLToolbox.Services;

namespace DOLToolbox.Controls
{
    public partial class ServerDetailsControl : UserControl
    {
        private readonly Timer _timer = new Timer();
        private readonly PerformanceCounter _cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        public ServerDetailsControl()
        {
            InitializeComponent();
        }

        private void ServerDetailsControl_Load(object sender, EventArgs e)
        {
            _timer.Interval = 1000;
            _timer.Tick += TimerOnTick;
            _timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (!Visible)
            {
                return;
            }
            var connectionString = ConnectionStringService.DbConfig.ConnectionString;
            var db = new MySQLObjectDatabase(connectionString);
            var testConnection = db.CreateConnection();

            try
            {
                testConnection.Open();
            }
            catch (Exception)
            {
                // swallow db connection issues
            }
            finally
            {
                if (testConnection.State == System.Data.ConnectionState.Open)
                {
                    lblAccounts.Text = @"Accounts Created = " + DatabaseManager.Database.GetObjectCount<Account>();
                    lblChrCreated.Text = @"Characters Created = " + DatabaseManager.Database.GetObjectCount<DOLCharacters>();
                    lblBugReports.Text = @"Bug Reports = " + DatabaseManager.Database.GetObjectCount<BugReport>();
                    lblAppeals.Text = @"Pending Appeals = " + DatabaseManager.Database.GetObjectCount<BugReport>();
                    testConnection.Close();
                }
                else
                {
                    lblAccounts.Text = @"Not connected to database";
                    lblChrCreated.Text = string.Empty;
                    lblBugReports.Text = string.Empty;
                    lblAppeals.Text = string.Empty;
                }
            }

            lblCPUUsage.Text = @"CPU Usage = " + (int)_cpu.NextValue() + @"%";
        }
    }
}
