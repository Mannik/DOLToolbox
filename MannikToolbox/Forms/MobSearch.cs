using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using DOL.GS;
using MannikToolbox.Services;

namespace MannikToolbox.Forms
{
    public partial class MobSearch : Form
    {
        private int _page = 0;
        private int _pageSize = 50;
        private List<Mob> _data;
        private IList<Mob> _allData = DatabaseManager.Database.SelectAllObjects<Mob>();
        private readonly ModelImageService _modelImageService = new ModelImageService();

        public event EventHandler SelectNpcClicked;

        public MobSearch()
        {
            InitializeComponent();
        }

        private void MobSearch_Load(object sender, EventArgs e)
        {
            Text = $@"Mannik/Loki's Toolbox ({ConnectionStringService.ConnectionString.Server})";
            GetPage();
        }

        private void Dgd_MobSearch_SelectionChanged(object sender, EventArgs e)
        {
            if (dgd_MobSearch.SelectedRows.Count == 0 || dgd_MobSearch.SelectedRows[0].Cells.Count == 0)
            {
                return;
            }
            
            try
            {
                var mobId = dgd_MobSearch.SelectedRows[0].Cells[0].Value.ToString();
                var mob = _allData.First(x => x.ObjectId == mobId);
                lblMob_ID.Text = mobId;
                picNPC.Image = _modelImageService.LoadMob(mob.Model);
            }
            catch
            {
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            lblMob_ID.Text = "";
            txtFilterMob.Clear();
            _page = 0;
            GetPage();
        }

        private void BtnSelectNPC_Click(object sender, EventArgs e)
        {
            if (dgd_MobSearch.SelectedRows.Count == 0 || dgd_MobSearch.SelectedRows[0].Cells.Count == 0)
            {
                return;
            }

            var mobId = dgd_MobSearch.SelectedRows[0].Cells[0].Value.ToString();

            SelectNpcClicked?.Invoke(mobId, e);
            Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            _page = 0;
            lblMob_ID.Text = "";
            GetPage();
        }

        private void GetPage(bool paging = false)
        {
            var filter = txtFilterMob.Text?.ToLower();
            dgd_MobSearch.Rows.Clear();
            _data = paging
                ? _data
                : _allData
                    .Where(x => string.IsNullOrWhiteSpace(filter) || x.Name.ToLower().Contains(filter))
                    .ToList();

            _data
                .Skip(_page * _pageSize)
                .Take(_pageSize)
                .ForEach(x => dgd_MobSearch.Rows.Add(x.ObjectId, x.Name, x.Guild, x.Model, x.Region));

            lblPage.Text = $@"Page {_page + 1} of {Math.Ceiling(_data.Count / (decimal) _pageSize)}";
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_page == 0)
            {
                return;
            }

            _page = _page - 1;
            GetPage(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (_page == 0)
            {
                return;
            }

            _page = 0;
            GetPage(true);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var totalPages = Math.Ceiling(_data.Count / (decimal) _pageSize);

            if (_page == totalPages - 1)
            {
                return;
            }

            _page = _page + 1;
            GetPage(true);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            var totalPages = (int)Math.Ceiling(_data.Count / (decimal)_pageSize);

            if (_page == totalPages - 1)
            {
                return;
            }

            _page = totalPages - 1;
            GetPage(true);
        }
    }
}