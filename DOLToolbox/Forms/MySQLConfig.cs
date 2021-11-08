using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DOL.Database;
using DOL.Database.Handlers;
using DOLToolbox.Services;

namespace DOLToolbox.Forms
{
    public partial class MySqlConfig : Form
    {
        public MySqlConfig()
        {
            InitializeComponent();
        }

      

        private void mysql_test_button_Click(object sender, EventArgs e)
        {
            if (mysql_test_background_worker.IsBusy)
            {
                mysql_test_label.Text = "still testing ... please wait";
                return;
            }

            mysql_test_button.Enabled = false;
            mysql_test_progressbar.Visible = true;
            mysql_test_label.ForeColor = SystemColors.ControlText;
            mysql_test_label.Text = "testing ...";
            mysql_test_background_worker.RunWorkerAsync();
        }

        private void save_config_button_Click(object sender, EventArgs e)
        {
            toolstripStatusLabelValue = "Try to save configuration ...";

            #region Loki - Create MySQL Connection String

            var dbConfig = new DbConfig();
            //Host
            if (String.IsNullOrEmpty(mysql_host_textbox.Text))
            {
                addWrongValueErrorHandler(mysql_host_textbox,
                    "The value of \"Server Address\" in \"MySQL Database settings\" is not set.");
                return;
            }
            dbConfig.SetOption("Server", mysql_host_textbox.Text);

            //Port
            if (string.IsNullOrEmpty(mysql_port_textbox.Text))
            {
                addWrongValueErrorHandler(mysql_port_textbox,
                    "The value of \"Port\" in \"MySQL Database settings\" is not allowed.");
                return;
            }
            dbConfig.SetOption("Port", Convert.ToUInt16(mysql_port_textbox.Text).ToString());

            //Database Name
            if (string.IsNullOrEmpty(mysql_database_name_textbox.Text))
            {
                addWrongValueErrorHandler(mysql_database_name_textbox,
                    "The value of \"Database Name\" in \"MySQL Database settings\" is not set.");
                return;
            }
            dbConfig.SetOption("Database", mysql_database_name_textbox.Text);

            //Username
            if (string.IsNullOrEmpty(mysql_username_textbox.Text))
            {
                addWrongValueErrorHandler(mysql_username_textbox,
                    "The value of \"Username\" in \"MySQL Database settings\" is not set.");
                return;
            }
            dbConfig.SetOption("UserID", mysql_username_textbox.Text);

            //Password
            dbConfig.SetOption("Password", mysql_password_textbox.Text);

            //Treat tiny as boolean
            dbConfig.SetOption("TreatTinyAsBoolean", "false");

            //Set generated connection string
            var t = dbConfig.ConnectionString;

            //Just for fun: Test the connection
            // mysql_test_button_Click(null, null);

            #endregion
            
            ConnectionStringService.SetString(
                dbConfig.GetValueOf("UserID"), 
                dbConfig.GetValueOf("Password"), 
                dbConfig.GetValueOf("Server"), 
                dbConfig.GetValueOf("Database"), 
                dbConfig.GetValueOf("Port"));
            Close();
        }

        #region Background Worker

        private void mysql_test_background_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var dbConfig = new DbConfig();
            dbConfig.SetOption("Server", mysql_host_textbox.Text);
            dbConfig.SetOption("Port", Convert.ToUInt32(mysql_port_textbox.Text).ToString());
            dbConfig.SetOption("Database", mysql_database_name_textbox.Text);
            dbConfig.SetOption("UserID", mysql_username_textbox.Text);
            dbConfig.SetOption("Password", mysql_password_textbox.Text);
            dbConfig.SetOption("ConnectionTimeout", "2");

            var db = new MySQLObjectDatabase(dbConfig.ConnectionString);
            var con = db.CreateConnection();
            try
            {
                con.Open();
                e.Result = "Congratulations! I am connected!";
                ;
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
            finally
            {
                con.Close();
            }
        }

        private void mysql_test_background_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result.GetType().IsSubclassOf(typeof(Exception)))
            {
                mysql_test_label.ForeColor = Color.Red;
                mysql_test_label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                mysql_test_label.Text = ((Exception)e.Result).Message;
            }
            else
            {
                mysql_test_label.ForeColor = Color.Green;
                mysql_test_label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
                mysql_test_label.Text = "Congratulations! I am connected!";
            }
            mysql_test_button.Enabled = true;
            mysql_test_progressbar.Visible = false;
        }

        #endregion

        #region Tooltip

        public string toolstripStatusLabelValue
        {
            set
            {
                if (value == null)
                {
                    toolstrip_status_label.Text = "Ready to configurate your Server.";
                    return;
                }
                toolstrip_status_label.Text = value;
            }
        }

        private void addWrongValueErrorHandler(Control recipient, string error)
        {
            wrong_data_error_handler.SetError(recipient, error);
            toolstripStatusLabelValue = error;
        }

        #endregion
    }

}