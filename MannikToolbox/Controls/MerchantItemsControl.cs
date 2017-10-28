using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using DOL.GS;
using MannikToolbox.Forms;
using MannikToolbox.Services;

namespace MannikToolbox.Controls
{
    public partial class MerchantItemsControl : UserControl
    {
        private readonly MerchantItemService _merchantItemService;
        private readonly ItemService _itemService;
        private readonly ModelImageService _modelImageService;

        private List<MerchantItem> _merchantItems;
        private List<ItemTemplate> _items;
        private PageItemModel _selected;
        private int _page;
        private int _selectedIndex;

        public MerchantItemsControl()
        {
            InitializeComponent();

            _merchantItemService = new MerchantItemService();
            _itemService = new ItemService();
            _modelImageService =  new ModelImageService();
        }

        private async void MerchantItemsControl_Load(object sender, EventArgs e)
        {
            var loading = new LoadingForm
            {
                ProgressText = {Text = @"Loading: Items"}
            };
            loading.Show();

            _items = await _itemService.GetItems();

            loading.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialogBox
            {
                Caption = { Text = @"Please enter Merchant Item Template ID" }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                _page = 0;
                LoadItems(dialog.Input.Text);
            }
            dialog.Dispose();
        }

        private void LoadItems(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            _merchantItems = _merchantItemService.Get(id);

            if (_merchantItems == null || _merchantItems.Count == 0)
            {
                return;
            }

            LoadPage();
        }

        private class PageItemModel
        {
            public string Name { get; set; }
            public long Value { get; set; }
            public int Level { get; set; }
            public int Position { get; set; }
            public MerchantItem MerchantItem { get; set; }
            public ItemTemplate Item { get; set; }
            public bool IsItemMissing { get; set; }
        }

        private void LoadPage()
        {
            txtItemListId.Text = _merchantItems.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ItemListID))?.ItemListID;

            var page =
                (from merchItem in _merchantItems.Where(x => x.PageNumber == _page)
                    join item in _items on merchItem.ItemTemplateID equals item.Id_nb into tmpItem
                    from item in tmpItem
                    select new PageItemModel
                    {
                        Name = item?.Name ?? merchItem.ItemTemplateID,
                        Value = item?.Price ?? 0,
                        Level = item?.Level ?? 0,
                        IsItemMissing = item == null,
                        Position = merchItem.SlotPosition + 1,
                        MerchantItem = merchItem,
                        Item = item
                    })
                .OrderBy(x => x.Position)
                .ToList();

            var bindingList = new BindingList<PageItemModel>(page);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

            SetGridColumns();
            lblPage.Text = $@"Page {_page + 1} of 5";

            if(dataGridView1.Rows.Count  -1 >= _selectedIndex)
            {
                dataGridView1.Rows[_selectedIndex].Selected = true;
            }
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

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Value",
                HeaderText = @"Value",
                Name = "Value",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Position",
                HeaderText = @"Position",
                Name = "Position",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            _merchantItems.Clear();
            _page = 0;
            _selectedIndex = 0;
            pictureBox1.Image = null;
            txtItemListId.Text = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_page == 0)
            {
                return;
            }

            _page -= 1;
            LoadPage();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_page == 4)
            {
                return;
            }

            _page += 1;
            LoadPage();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelected();
            if (selectedItem == null)
            {
                return;
            }

            var prevItem = _merchantItems
                .FirstOrDefault(x =>
                    x.PageNumber == selectedItem.MerchantItem.PageNumber &&
                    x.SlotPosition == selectedItem.MerchantItem.SlotPosition - 1);

            if (prevItem == null)
            {
                return;
            }

            button6.Enabled = false;
            _selectedIndex = dataGridView1.SelectedRows[0].Index - 1;
            prevItem.SlotPosition += 1;
            selectedItem.MerchantItem.SlotPosition -= 1;

            var templateId = _merchantItemService.Save(_merchantItems);
            _merchantItemService.Get(templateId);

            LoadPage();
            button6.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelected();
            if (selectedItem == null)
            {
                return;
            }
            
            var nextItem = _merchantItems
                .FirstOrDefault(x =>
                    x.PageNumber == selectedItem.MerchantItem.PageNumber &&
                    x.SlotPosition == selectedItem.MerchantItem.SlotPosition + 1);

            if (nextItem == null)
            {
                return;
            }

            button7.Enabled = false;
            _selectedIndex = dataGridView1.SelectedRows[0].Index + 1;
            nextItem.SlotPosition -= 1;
            selectedItem.MerchantItem.SlotPosition += 1;

            var templateId = _merchantItemService.Save(_merchantItems);
            _merchantItemService.Get(templateId);

            LoadPage();
            button7.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelected();
            if (selectedItem == null)
            {
                return;
            }

            button8.Enabled = false;
            var confirmResult = MessageBox.Show(@"Are you sure to delete the selected item",
                @"Confirm Delete!!",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _merchantItems.Remove(selectedItem.MerchantItem);

                _merchantItems
                    .Where(x => x.PageNumber == selectedItem.MerchantItem.PageNumber &&
                                x.SlotPosition > selectedItem.MerchantItem.SlotPosition)
                    .ForEach(x => x.SlotPosition -= 1);

                var templateId = _merchantItemService.Save(_merchantItems);
                _merchantItemService.Get(templateId);
                LoadPage();
            }
            button7.Enabled = true;
        }

        private PageItemModel GetSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return null;
            }

            return dataGridView1.SelectedRows[0].DataBoundItem as PageItemModel;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            _selected = GetSelected();


            if (_selected?.Item?.Model != null)
            {
                pictureBox1.Image = _modelImageService.LoadItem(_selected.Item.Model);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GetSelected() == null)
            {
                return;
            }

            var search = new ItemSearchForm(_items)
            {
                TopMost = true
            };

            search.SelectClicked += (o, args) =>
            {
                var selected = GetSelected();

                if (selected == null)
                {
                    return;
                }

                if (!(o is ItemTemplate item))
                {
                    return;
                }

                selected.MerchantItem.ItemTemplateID = item.Id_nb;

                var templateId = _merchantItemService.Save(_merchantItems);
                _merchantItemService.Get(templateId);

                LoadPage();

            };

            search.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var search = new ItemSearchForm(_items);

            search.SelectClicked += (o, args) =>
            {
                if (!(o is ItemTemplate item))
                {
                    return;
                }

                var slot = 0;
                if (_merchantItems != null && _merchantItems.Count(x => x.PageNumber == _page) > 0)
                {
                    slot = _merchantItems.Where(x => x.PageNumber == _page).Max(x => x.SlotPosition) + 1;
                }
                else
                {
                    _merchantItems = new List<MerchantItem>();
                }

                var merchantItem = new MerchantItem
                {
                    ItemTemplateID = item.Id_nb,
                    PageNumber = _page,
                    SlotPosition = slot,
                    
                };
                _merchantItems.Add(merchantItem);

                var templateId = _merchantItemService.Save(_merchantItems);
                _merchantItemService.Get(templateId);

                LoadPage();

            };

            search.ShowDialog(this);
        }
    }
}
