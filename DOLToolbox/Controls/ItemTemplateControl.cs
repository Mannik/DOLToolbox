using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Forms;
using DOLToolbox.Services;

namespace DOLToolbox.Controls
{
    public partial class ItemTemplateControl : UserControl
    {
        private readonly ItemService _itemService = new ItemService();
        private readonly ImageService _modelImageService = new ImageService();
        private ItemTemplate _item;

        public ItemTemplateControl()
        {
            InitializeComponent();
        }

        public ItemTemplateControl(string itemId)
        {
            InitializeComponent();

            LoadItem(itemId);
        }

        private void ItemTemplateControl_Load(object sender, EventArgs e)
        {
            SetupDropdowns();
        }

        private void itemLoad_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialogBox
            {
                Caption = { Text = @"Please enter Item ID" }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                LoadItem(dialog.Input.Text);
            }
            dialog.Dispose();
        }

        private void LoadItem(string itemId)
        {
            Clear();

            if (string.IsNullOrWhiteSpace(itemId))
            {
                return;
            }

            _item = _itemService.GetItem(itemId);


            if (_item == null)
            {
                MessageBox.Show($@"Object with ObjectId: {itemId} not found", @"Object not found");
                return;
            }

            _modelImageService.LoadItem(_item.Model, pictureBox1.Width, pictureBox1.Height)
                .ContinueWith(x => _modelImageService.AttachImage(pictureBox1, x));

            BindingService.BindData(_item, this);

        }

        private void SetupDropdowns()
        {
            ComboboxService.BindItemExtension(_Extension);
            ComboboxService.BindItemEffect(_Effect);
            ComboboxService.BindRealms(_Realm);
            ComboboxService.BindWeaponDamageTypes(_Type_Damage);
            ComboboxService.BindObjectType(_Object_Type);
            ComboboxService.BindBonusCatagory(Bonus1Catagory);
            ComboboxService.BindBonusCatagory(Bonus2Catagory);
            ComboboxService.BindBonusCatagory(Bonus3Catagory);
            ComboboxService.BindBonusCatagory(Bonus4Catagory);
            ComboboxService.BindBonusCatagory(Bonus5Catagory);
            ComboboxService.BindBonusCatagory(Bonus6Catagory);
            ComboboxService.BindBonusCatagory(Bonus7Catagory);
            ComboboxService.BindBonusCatagory(Bonus8Catagory);
            ComboboxService.BindBonusCatagory(Bonus9Catagory);
            ComboboxService.BindBonusCatagory(Bonus10Catagory);
            ComboboxService.BindBonusCatagory(ExtraBonusCatagory);
            ComboboxService.BindColors(_Color);
            ComboboxService.BindItemSlot(_Item_Type);

            _Object_Type.AutoCompleteCustomSource.AddRange(_Object_Type.Items.Cast<ComboboxService.SelectItemModel>().Select(x => x.Value).ToArray());
        }

        private void TypeBinder(string prefix)
        {
            var categoryField = GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(x => x.Name == $@"{prefix}Catagory");

            var bonusField = GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(x => x.Name == $@"_{prefix}Type");

            var category = categoryField.GetValue(this) as ComboBox;
            var type = bonusField.GetValue(this) as ComboBox;

            if (type == null || category == null)
            {
                return;
            }

            switch (category.SelectedIndex)
            {
                case 0:
                    ComboboxService.BindBonusAll(type);
                    break;
                case 1:
                    ComboboxService.BindBonusStats(type);
                    break;
                case 2:
                    ComboboxService.BindBonusStatCap(type);
                    break;
                case 3:
                    ComboboxService.BindBonusResists(type);
                    break;
                case 4:
                    ComboboxService.BindBonusResistCap(type);
                    break;
                case 5:
                    ComboboxService.BindBonusSkills(type);
                    break;
                case 6:
                    ComboboxService.BindBonusFocus(type);
                    break;
                case 7:
                    ComboboxService.BindBonusToa(type);
                    break;
                case 8:
                    ComboboxService.BindBonusOther(type);
                    break;
                case 9:
                    ComboboxService.BindBonusMythical(type);
                    break;
            }
        }
        
        private void Bonus1Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus1");
        }

        private void Bonus2Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus2");
        }

        private void Bonus3Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus3");
        }

        private void Bonus4Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus4");
        }

        private void Bonus5Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus5");
        }

        private void Bonus6Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus6");
        }

        private void Bonus7Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus7");
        }

        private void Bonus8Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus8");
        }

        private void Bonus9Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus9");
        }

        private void Bonus10Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("Bonus10");
        }

        private void ExtraBonusCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeBinder("ExtraBonus");
        }

        private void itemSearch_Click_1(object sender, EventArgs e)
        {
            var search = new ItemSearchForm();

            search.SelectClicked += (o, args) =>
            {
                if (!(o is ItemTemplate item))
                {
                    return;
                }

                LoadItem(item.ObjectId);

            };

            search.ShowDialog(this);
        }

        private void itemSave_Click_1(object sender, EventArgs e)
        {
            string id;
            if (_item == null)
            {
                _item = new ItemTemplate();
                id = _item.Id_nb;
            }
            else
            {
                id = _item.Id_nb;
            }

            try
            {
                _item.AllowUpdate = true;
                BindingService.SyncData(_item, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (id != _item.Id_nb && !_itemService.UpdateId(id, _item.Id_nb, _item.ObjectId))
            {
                MessageBox.Show(@"Unfortunately Id_nb cannot be changed. Please update manually if needed.");
                return;
            }

            var itemId = _itemService.SaveItem(_item);
            LoadItem(itemId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            BindingService.ClearData(this);
            _item = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_item == null)
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

            _itemService.Delete(_item);
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var modelViewer = new ModelViewerSearchForm(ModelViewerSearchForm.ModelType.Item);

            modelViewer.SelectClicked += (o, args) => { _Model.Text = o.ToString(); };

            modelViewer.Show(this);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var modelViewer = new ModelViewerSearchForm(ModelViewerSearchForm.ModelType.Item);

            modelViewer.SelectClicked += (o, args) => { _Model.Text = o.ToString(); };

            modelViewer.Show(this);
        }
    }
}
