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
    public partial class LootTemplateSearchForm : Form
    {
        private readonly List<Mob> _mobs;
        private readonly LootTemplateService _lootTemplateService = new LootTemplateService();
        private readonly ImageService _imageService = new ImageService();

        private int _page;
        private int _pageSize = 50;
        private int _selectedIndex;
        private List<MobXLootTemplate> _allData;
        private List<MobXLootTemplate> _data;

        public event EventHandler SelectClicked;

        public LootTemplateSearchForm(List<Mob> mobs)
        {
            _mobs = mobs;
            InitializeComponent();
        }

        public LootTemplateSearchForm(List<MobXLootTemplate> allTemplates)
        {
            _allData = allTemplates;

            InitializeComponent();
        }

        private async  void ItemSearchForm_Load(object sender, EventArgs e)
        {
            if (_allData == null || _allData.Count == 0)
            {
                _allData = await _lootTemplateService.Get();
            }

            Text = $@"Dawn of Light Database Toolbox ({ConnectionStringService.ConnectionString.Server})";
            GetPage();
        }

        public class SearchModel
        {
            public string Name { get; set; }
            public int? Slot { get; set; }
        }

        private List<MobXLootTemplate> Search()
        {
            var query = _allData.AsQueryable();

            var filter = txtFilterMob.Text;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToWildcardRegex();
                _data = _allData.Where(x => Regex.IsMatch(x.LootTemplateName, filter, RegexOptions.IgnoreCase)).ToList();
            }

            return query.ToList();
        }

        private void GetPage(bool paging = false)
        {
            dataGridView1.Rows.Clear();

            if (!paging)
            {
                _data = Search();
            }

            var page = _data
                .Skip(_page * _pageSize)
                .Take(_pageSize)
                .ToList();

            var bindingList = new BindingList<MobXLootTemplate>(page);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

            SetGridColumns();

            if (dataGridView1.Rows.Count - 1 >= _selectedIndex)
            {
                dataGridView1.Rows[_selectedIndex].Selected = true;
            }
            lblPage.Text = $@"Page {_page + 1} of {Math.Ceiling(_data.Count / (decimal)_pageSize)}";
        }

        private void SetGridColumns()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MobName",
                HeaderText = @"Mob Name",
                Name = "MobName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LootTemplateName",
                HeaderText = @"Loot Template Name",
                Name = "LootTemplateName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DropCount",
                HeaderText = @"Drop Count",
                Name = "DropCount",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private MobXLootTemplate GetSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return null;
            }

            return dataGridView1.SelectedRows[0].DataBoundItem as MobXLootTemplate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var item = GetSelected();

            if (item == null)
            {
                return;
            }

            SelectClicked?.Invoke(item, e);
            Close();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _page = 0;
            _selectedIndex = 0;
            GetPage();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFilterMob.Clear();
            _selectedIndex = 0;
            _page = 0;
            GetPage();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var selected = GetSelected();
            pictureBox1.Image = null;

            if (selected == null)
            {
                return;
            }

            var mob = _mobs.FirstOrDefault(x => x.Name.Equals(selected.MobName, StringComparison.InvariantCultureIgnoreCase));
            if (mob != null)
            {
                _imageService.LoadMob(mob.Model, pictureBox1.Width, pictureBox1.Height)
                    .ContinueWith(x => pictureBox1.Image = x.Result);
            }
        }
    }
}
