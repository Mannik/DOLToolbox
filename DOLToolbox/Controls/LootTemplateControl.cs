using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Forms;
using DOLToolbox.Models;
using DOLToolbox.Services;

namespace DOLToolbox.Controls
{
    public partial class LootTemplateControl : UserControl
    {
        private readonly LootTemplateService _lootTemplateService = new LootTemplateService();
        private readonly ItemService _itemService = new ItemService();
        private readonly ImageService _modelImageService = new ImageService();
        
        private List<ItemTemplate> _items;
        private LootTemplateModel _model;
        private List<Mob> _mobs;

        public LootTemplateControl()
        {
            InitializeComponent();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {

            var dialog = new InputDialogBox
            {
                Caption = { Text = @"Please enter MobXLootTemplate ID" }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                await LoadTemplate(dialog.Input.Text);
            }

            dialog.Dispose();
        }

        private async Task LoadTemplate(string templateId)
        {
            if (string.IsNullOrWhiteSpace(templateId))
            {
                return;
            }

            Clear();
            _model = await _lootTemplateService.Get(templateId);

            if (_model == null)
            {
                MessageBox.Show($@"Object with ObjectId: {templateId} not found", @"Object not found");
                return;
            }

            BindingService.BindData(_model.MobXLootTemplate, this);
            _MobXLootTemplate_ID.Text = _model.MobXLootTemplate.ObjectId;

            var mob = _mobs.FirstOrDefault(x => _model.MobXLootTemplate.MobName == x.Name);
            if (mob != null)
            {
                await _modelImageService.LoadMob(mob.Model, pictureBox2.Width, pictureBox2.Height)
                    .ContinueWith(x => _modelImageService.AttachImage(pictureBox2, x));
            }

            var bindingList = new BindingList<LootTemplate>(_model.LootTemplates.OrderBy(x => x.ItemTemplateID).ToList());
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            SetGridColumns();
        }

        private LootTemplate GetSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return null;
            }

            return dataGridView1.SelectedRows[0].DataBoundItem as LootTemplate;
        }

        private void SetGridColumns()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemTemplateID",
                HeaderText = @"ItemTemplateID",
                Name = "ItemTemplateID",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Chance",
                HeaderText = @"Chance",
                Name = "Chance",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Count",
                HeaderText = @"Count",
                Name = "Count",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void Clear()
        {
            _model = null;
            BindingService.ClearData(this);
            dataGridView1.DataSource = null;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            _Chance.Text = null;
            _Count.Text = null;
            _ItemTemplateID.Text = null;

            var selected = GetSelected();

            if (selected == null)
            {
                return;
            }

            BindingService.BindData(selected, this);

            var item = _items.FirstOrDefault(x => x.Id_nb == selected.ItemTemplateID);

            if (item?.Model != null)
            {
                _modelImageService.LoadItem(item.Model, pictureBox1.Width, pictureBox1.Height)
                    .ContinueWith(x => _modelImageService.AttachImage(pictureBox1, x));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var search = new ItemSearchForm(_items)
            {
                TopMost = true
            };

            search.SelectClicked += (o, args) =>
            {
                if (!(o is ItemTemplate item))
                {
                    return;
                }

                _ItemTemplateID.Text = item.Id_nb;
                _modelImageService.LoadItem(item.Model, pictureBox1.Width, pictureBox1.Height)
                    .ContinueWith(x => _modelImageService.AttachImage(pictureBox1, x));
            };

            search.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var search = new LootTemplateSearchForm(_mobs);

            search.SelectClicked += async (o, args) =>
            {
                if (!(o is MobXLootTemplate item))
                {
                    return;
                }

                await LoadTemplate(item.ObjectId);

            };

            search.ShowDialog(this);
        }

        private void LootTemplateControl_Leave(object sender, EventArgs e)
        {
            _items = null;
            _mobs = null;
        }

        private async void LootTemplateControl_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                return;
            }

            var loading = new LoadingForm
            {
                ProgressText = { Text = @"Loading: Mobs" }
            };
            loading.Show();

            BindingService.ToggleEnabled(this);
            _items = await _itemService.GetItems();
            _mobs = await Task.Run(() => DatabaseManager.Database.SelectAllObjects<Mob>().ToList());
            BindingService.ToggleEnabled(this);

            loading.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var search = new MobSearch(_mobs)
            {
                TopMost = true
            };

            search.SelectNpcClicked += (o, args) =>
            {
                if (!(o is Mob mob))
                {
                    return;
                }

                _MobName.Text = mob.Name;
                _modelImageService.LoadMob(mob.Model, pictureBox2.Width, pictureBox2.Height)
                    .ContinueWith(x => _modelImageService.AttachImage(pictureBox2, x));
            };

            search.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _LootTemplateName.Text = _MobName.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        // Template Remove
        private async void button7_Click(object sender, EventArgs e)
        {
            if (_model == null)
            {
                return;
            }

            var confirmResult = MessageBox.Show(@"Are you sure to delete the selected object",
                @"Confirm Delete!!",
                MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            await _lootTemplateService.Remove(_model.MobXLootTemplate);
            Clear();
        }

        //Template Save
        private async void button10_Click(object sender, EventArgs e)
        {
            var model = _model?.MobXLootTemplate ?? new MobXLootTemplate();

            BindingService.SyncData(model, this);

            BindingService.ToggleEnabled(this);
            var objectId = await _lootTemplateService.Save(model);
            await LoadTemplate(objectId);
            BindingService.ToggleEnabled(this);
        }

        // Item Remove
        private async void button8_Click(object sender, EventArgs e)
        {
            var model = GetSelected();
            if (_model?.MobXLootTemplate == null || model == null)
            {
                return;
            }

            var confirmResult = MessageBox.Show(@"Are you sure to delete the selected object",
                @"Confirm Delete!!",
                MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            BindingService.ToggleEnabled(this);
            await _lootTemplateService.Remove(model);
            await LoadTemplate(_model.MobXLootTemplate.ObjectId);
            BindingService.ToggleEnabled(this);
        }

        // Item Save
        private async void button9_Click(object sender, EventArgs e)
        {
            var model = GetSelected();
            if (_model?.MobXLootTemplate == null || model == null)
            {
                return;
            }

            BindingService.SyncData(model, this);

            BindingService.ToggleEnabled(this);
            await _lootTemplateService.Save(model);
            await LoadTemplate(_model.MobXLootTemplate.ObjectId);
            BindingService.ToggleEnabled(this);
        }

        // Item Add
        private async void button3_Click(object sender, EventArgs e)
        {
            if (_model?.MobXLootTemplate == null)
            {
                return;
            }

            var model = new LootTemplate
            {
                TemplateName = _model.MobXLootTemplate.LootTemplateName
            };

            BindingService.SyncData(model, this);

            BindingService.ToggleEnabled(this);
            await _lootTemplateService.Save(model);
            await LoadTemplate(_model.MobXLootTemplate.ObjectId);
            BindingService.ToggleEnabled(this);
        }
    }
}
