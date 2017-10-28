﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using MannikToolbox.Services;

namespace MannikToolbox.Forms
{
    public partial class ItemSearchForm : Form
    {
        private readonly ModelImageService _modelImageService = new ModelImageService();
        private readonly ItemService _itemService = new ItemService();

        private int _page;
        private int _pageSize = 50;
        private int _selectedIndex;
        private List<ItemTemplate> _allData;
        private List<ItemTemplate> _data;

        public event EventHandler SelectClicked;

        public ItemSearchForm()
        {
            InitializeComponent();
        }

        public ItemSearchForm(List<ItemTemplate> allItems)
        {
            _allData = allItems;

            InitializeComponent();
        }

        private async  void ItemSearchForm_Load(object sender, EventArgs e)
        {
            if (_allData == null || _allData.Count == 0)
            {
                _allData = await _itemService.GetItems();
            }

            Text = $@"Dawn of Light Database Toolbox ({ConnectionStringService.ConnectionString.Server})";
            GetPage();
        }

        private void GetPage(bool paging = false)
        {
            dataGridView1.Rows.Clear();

            if (!paging)
            {
                var filter = txtFilterMob.Text?.ToLower();
                _data = _allData
                    .Where(x => string.IsNullOrWhiteSpace(filter) || x.Name.ToLower().Contains(filter))
                    .ToList();
            }

            var page = _data
                .Skip(_page * _pageSize)
                .Take(_pageSize)
                .ToList();

            var bindingList = new BindingList<ItemTemplate>(page);
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
                DataPropertyName = "Name",
                HeaderText = @"Name",
                Name = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Level",
                HeaderText = @"Level",
                Name = "Level",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private ItemTemplate GetSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return null;
            }

            return dataGridView1.SelectedRows[0].DataBoundItem as ItemTemplate;
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


            if (selected == null)
            {
                return;
            }
            
            pictureBox1.Image = _modelImageService.LoadItem(selected.Model);
        }
    }
}
