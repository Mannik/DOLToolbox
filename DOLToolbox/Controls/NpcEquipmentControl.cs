using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Forms;
using DOLToolbox.Services;

namespace DOLToolbox.Controls
{
    public partial class NpcEquipmentControl : UserControl
    {
        private readonly NpcEquipmentService _npcEquipmentService = new NpcEquipmentService();
        private readonly ImageService _imageService = new ImageService();
        private readonly ItemService _itemService = new ItemService();
        
        private readonly List<ComboboxService.SelectItemModel> _equipmentSlots =
            new List<ComboboxService.SelectItemModel>
            {
                new ComboboxService.SelectItemModel(10, "Right Hand"),
                new ComboboxService.SelectItemModel(11, "Left Hand"),
                new ComboboxService.SelectItemModel(12, "Two Hand"),
                new ComboboxService.SelectItemModel(13, "Ranged"),
                new ComboboxService.SelectItemModel(21, "Helm"),
                new ComboboxService.SelectItemModel(22, "Hands"),
                new ComboboxService.SelectItemModel(23, "Feet"),
                new ComboboxService.SelectItemModel(25, "Torso"),
                new ComboboxService.SelectItemModel(26, "Cloak"),
                new ComboboxService.SelectItemModel(27, "Legs"),
                new ComboboxService.SelectItemModel(28, "Arms")
            };

        private List<NPCEquipment> _equipment;
        private NPCEquipment _selected;
        private string _templateId;
        private List<ItemTemplate> _items;

        public NpcEquipmentControl()
        {
            InitializeComponent();
            BindDropdowns();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialogBox
            {
                Caption = { Text = @"Please enter NPC Equipment ID" }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                LoadItems(dialog.Input.Text);
            }

            dialog.Dispose();
        }

        public class EquipmentGridModel
        {
            public string Value { get; set; }
            public bool DoesExist { get; set; }
            public NPCEquipment Item { get; set; }
            public int? SlotId { get; set; }
        }

        private void LoadItems(string templateId)
        {
            if (string.IsNullOrWhiteSpace(templateId))
            {
                return;
            }

            _equipment = _npcEquipmentService.Get(templateId);

            if (_equipment == null || !_equipment.Any())
            {
                MessageBox.Show($@"Object with ObjectId: {templateId} not found", @"Object not found");
                return;
            }

            _templateId = _equipment.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.TemplateID))?.TemplateID;

            var items =
                (from slot in _equipmentSlots
                    join item in _equipment on slot.Id equals item.Slot into tmpItem
                    from item in tmpItem.DefaultIfEmpty()
                    select new EquipmentGridModel
                    {
                        SlotId = slot.Id,
                        Value = slot.Value,
                        DoesExist = item != null,
                        Item = item
                    })
                .ToList();

            var bindingList = new BindingList<EquipmentGridModel>(items);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

            SetGridColumns();
        }

        private void SetGridColumns()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Value",
                HeaderText = @"Slot",
                Name = "Value",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DoesExist",
                HeaderText = @"Exists",
                Name = "DoesExist",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            BindingService.ClearData(this);
            pictureBox1.Image = null;
            _TemplateID.Text = _templateId;

            var selected = GetSelected();

            if (selected == null)
            {
                _selected = null;
                return;
            }

            groupBox1.Text = selected.Value;
            _Slot.SelectedItem = _Slot.Items.Cast<ComboboxService.SelectItemModel>().First(x => x.Id == selected.SlotId);

            if (selected.Item == null)
            {
                _selected = null;
                return;
            }

            _selected = selected.Item;
            BindingService.BindData(selected.Item, this);
            _imageService.LoadItem(selected.Item.Model, pictureBox1.Width, pictureBox1.Height)
                .ContinueWith(x => pictureBox1.Image = x.Result);
        }

        private EquipmentGridModel GetSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return null;
            }

            return dataGridView1.SelectedRows[0].DataBoundItem as EquipmentGridModel;
        }

        private void BindDropdowns()
        {
            ComboboxService.BindColors(_Color);
            ComboboxService.BindItemExtension(_Extension);
            ComboboxService.BindItemEffect(_Effect);
            ComboboxService.BindItemSlot(_Slot);
        }

        private void Clear()
        {
            BindingService.ClearData(this);
            pictureBox1.Image = null;
            _templateId = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var modelViewer = new ModelViewerSearchForm(ModelViewerSearchForm.ModelType.Item);

            modelViewer.SelectClicked += (o, args) =>
            {
                _Model.Text = o.ToString();
                _imageService.LoadItem((int)o, pictureBox1.Width, pictureBox1.Height)
                    .ContinueWith(x => pictureBox1.Image = x.Result);
            };

            modelViewer.Show(this);
        }

        private async void NpcEquipmentControl_Load(object sender, EventArgs e)
        {
            _items =  await _itemService.GetItems();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_selected == null)
            {
                _selected = new NPCEquipment
                {
                    TemplateID = _templateId,
                    ObjectId = null
                };
                _equipment.Add(_selected);
            }

            BindingService.SyncData(_selected, this);

            // template id has changed
            if (!string.IsNullOrWhiteSpace(_selected.TemplateID) && _selected.TemplateID != _templateId)
            {
                _equipment.ForEach(x =>
                {
                    x.TemplateID = _selected.TemplateID;
                    _npcEquipmentService.Save(x);
                });

                LoadItems(_selected.TemplateID);
                return;
            }

            var templateId = _npcEquipmentService.Save(_selected);
            LoadItems(templateId);
        }

        private void _Model_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(_Model.Text, out int modelId))
            {
                _imageService.LoadItem(modelId, pictureBox1.Width, pictureBox1.Height)
                    .ContinueWith(x => pictureBox1.Image = x.Result);
            }
        }
    }
}
