#region

using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DOL.Database;
using MannikToolbox.Services;

#endregion

namespace MannikToolbox
{
    public partial class MobSearch : Form
    {
        public MobSearch()
        {
            InitializeComponent();
        }

        protected bool CheckUrlExists(string url)
        {
            if (!url.Contains("http://"))
                url = "http://" + url;
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                return false;
            }
        }

        private void MobSearch_Load(object sender, EventArgs e)
        {
            Text = $@"Mannik/Loki's Toolbox ({ConnectionStringService.ConnectionString.Server})";
            var g = DatabaseManager.Database.SelectAllObjects<Mob>();
            foreach (var m in g)
                dgd_MobSearch.Rows.Add(m.ObjectId, m.Name, m.Guild, m.Model, m.Region);
        }

        private void Dgd_MobSearch_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var model = dgd_MobSearch.SelectedRows[0].Cells[3].Value.ToString();
                if (string.IsNullOrWhiteSpace(model)) return;
                var str = dgd_MobSearch.SelectedCells[0].Value.ToString();
                lblMob_ID.Text = str;
                var baseUrl = @"http://www.dolserver.net/models/Models/mobs/" +
                              (Convert.ToInt32(model) + ".jpg");
                if (CheckUrlExists(baseUrl))
                    picNPC.ImageLocation = baseUrl;
            }
            catch
            {
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            lblMob_ID.Text = "";
            txtFilterMob.Clear();
            lblMob_ID.Text = "";
            dgd_MobSearch.Rows.Clear();
            var g = DatabaseManager.Database.SelectAllObjects<Mob>();
            foreach (var m in g)
                dgd_MobSearch.Rows.Add(m.ObjectId, m.Name, m.Guild, m.Model, m.Region);
        }

        private void BtnSelectNPC_Click(object sender, EventArgs e)
        {
            //todo Pass value to Binding 
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            lblMob_ID.Text = "";
            if (string.IsNullOrWhiteSpace(txtFilterMob.Text)) return;
            var sm = from t in DatabaseManager.Database.SelectAllObjects<Mob>()
                     where t.Name.Contains(txtFilterMob.Text)
                     select t;
            if (!sm.Any()) return;
            dgd_MobSearch.Rows.Clear();

            foreach (var m in sm)
                dgd_MobSearch.Rows.Add(m.ObjectId, m.Name, m.Guild, m.Model, m.Region);
        }
    }
}