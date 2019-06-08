using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Extensions;
using DOLToolbox.Services;


namespace DOLToolbox.Forms
{
    public partial class ObjectSearch : Form
    {

        private int _page;
        private int _pageSize = 50;
        private List<WorldObject> _data;
        private IList<WorldObject> _allData;
        private readonly ObjectService _objectService = new ObjectService();

        public event EventHandler SelectNpcClicked;

        public ObjectSearch()
        {
            InitializeComponent();
        }
        public ObjectSearch(IList<WorldObject> data)
        {
            InitializeComponent();
            _allData = data;
        }

        private async void ObjectSearch_Load(object sender, EventArgs e)
        {
            if (_allData == null || !_allData.Any())
            {
                var loading = new LoadingForm
                {
                    ProgressText = { Text = @"Loading: World Obects" }
                };
                loading.Show();

                _allData = await _objectService.GetObjects();

                loading.Close();
            }

            GetPage();
        }

        private void Dgd_ObjectSearch_SelectionChanged(object sender, EventArgs e)
        {
            var selected = GetSelected();
            if (selected == null)
            {
                return;
            }

            lblObject_ID.Text = selected.ObjectId;

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            lblObject_ID.Text = "";
            txtFilterObject.Clear();
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

        private WorldObject GetSelected()
        {
            if (dgd_ObjectSearch.SelectedRows.Count < 1)
            {
                return null;
            }

            return dgd_ObjectSearch.SelectedRows[0].DataBoundItem as WorldObject;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            _page = 0;
            lblObject_ID.Text = "";
            GetPage();
        }

        private void GetPage(bool paging = false)
        {
            var filter = txtFilterObject.Text?.ToLower();
            dgd_ObjectSearch.Rows.Clear();
            _data = paging
                ? _data
                : _allData
                    .Where(x =>
                        string.IsNullOrWhiteSpace(filter) ||
                        Regex.IsMatch(x.Name, txtFilterObject.Text.ToWildcardRegex(), RegexOptions.IgnoreCase))
                    .ToList();

            var page = _data
                .Skip(_page * _pageSize)
                .Take(_pageSize)
                .ToList();


            var bindingList = new BindingList<WorldObject>(page);
            var source = new BindingSource(bindingList, null);
            dgd_ObjectSearch.DataSource = source;

            SetGridColumns();

            lblPage.Text = $@"Page {_page + 1} of {Math.Ceiling(_data.Count / (decimal)_pageSize)}";
        }

        private void SetGridColumns()
        {
            dgd_ObjectSearch.Columns.Clear();

            dgd_ObjectSearch.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = @"Name",
                Name = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgd_ObjectSearch.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Guild",
                HeaderText = @"Guild",
                Name = "Guild",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgd_ObjectSearch.Columns.Add(new DataGridViewTextBoxColumn
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
            var totalPages = Math.Ceiling(_data.Count / (decimal)_pageSize);

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

        private void ObjectSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            _allData = null;
            _data = null;
        }

        private void btnSelectNPC_Click_1(object sender, EventArgs e)
        {
            var selected = GetSelected();
            if (selected == null)
            {
                return;
            }

            SelectNpcClicked?.Invoke(selected, e);
            Close();
        }
    }
}

