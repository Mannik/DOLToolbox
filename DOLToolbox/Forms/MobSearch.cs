using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Extensions;
using DOLToolbox.Services;

namespace DOLToolbox.Forms
{
    public partial class MobSearch : Form
    {
        private int _page;
        private int _pageSize = 50;
        private List<Mob> _data;
        private IList<Mob> _allData;
        private readonly ImageService _modelImageService = new ImageService();

        public event EventHandler SelectNpcClicked;

        public MobSearch()
        {
            InitializeComponent();
        }
        public MobSearch(IList<Mob> data)
        {
            InitializeComponent();
            _allData = data;
        }

        private async void MobSearch_Load(object sender, EventArgs e)
        {
            Text = $@"Dawn of Light Database Toolbox ({ConnectionStringService.ConnectionString.Server})";

            if (_allData == null || !_allData.Any())
            {
                var loading = new LoadingForm
                {
                    ProgressText = { Text = @"Loading: Mobs" }
                };
                loading.Show();

                _allData = await Task.Run(() => DatabaseManager.Database.SelectAllObjects<Mob>());

                loading.Close();
            }

            GetPage();
        }

        private void Dgd_MobSearch_SelectionChanged(object sender, EventArgs e)
        {
            var selected = GetSelected();
            if (selected == null)
            {
                return;
            }
            
            lblMob_ID.Text = selected.ObjectId;
            _modelImageService.LoadMob(selected.Model, picNPC.Width, picNPC.Height)
                .ContinueWith(x => picNPC.Image = x.Result);
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
            var selected = GetSelected();
            if (selected == null)
            {
                return;
            }

            SelectNpcClicked?.Invoke(selected, e);
            Close();
        }

        private Mob GetSelected()
        {
            if (dgd_MobSearch.SelectedRows.Count < 1)
            {
                return null;
            }

            return dgd_MobSearch.SelectedRows[0].DataBoundItem as Mob;
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
                    .Where(x =>
                        string.IsNullOrWhiteSpace(filter) ||
                        Regex.IsMatch(x.Name, txtFilterMob.Text.ToWildcardRegex(), RegexOptions.IgnoreCase))
                    .ToList();

            var page = _data
                .Skip(_page * _pageSize)
                .Take(_pageSize)
                .ToList();


            var bindingList = new BindingList<Mob>(page);
            var source = new BindingSource(bindingList, null);
            dgd_MobSearch.DataSource = source;

            SetGridColumns();

            lblPage.Text = $@"Page {_page + 1} of {Math.Ceiling(_data.Count / (decimal) _pageSize)}";
        }

        private void SetGridColumns()
        {
            dgd_MobSearch.Columns.Clear();

            dgd_MobSearch.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = @"Name",
                Name = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgd_MobSearch.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Guild",
                HeaderText = @"Guild",
                Name = "Guild",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgd_MobSearch.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Region",
                HeaderText = @"Region",
                Name = "Region",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
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

        private void MobSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            _allData = null;
            _data = null;
        }
    }
}