using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using MannikToolbox.Forms;
using MannikToolbox.Services;

namespace MannikToolbox.Controls
{
    public partial class ItemTemplateControl : UserControl
    {
        private readonly ItemService _itemService;
        private readonly ModelImageService _modelImageService;
        private ItemTemplate _item;

        public ItemTemplateControl()
        {
            InitializeComponent();
            _itemService = new ItemService();
            _modelImageService = new ModelImageService();
        }
        private void ItemTemplateControl_Load(object sender, EventArgs e)
        {
            //var item = _itemService.GetItem("0016de40-8dc3-4cd6-aefd-e34b9f720fa7");

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
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return;
            }

            _item = _itemService.GetItem(itemId);

            if (_item == null)
            {
                return;
            }

            pictureBox1.Image = _modelImageService.LoadItem(_item.Model);

            BindingService.BindData(_item, this);

        }

        private void itemSave_Click(object sender, EventArgs e)
        {
            if (_item == null)
            {
                return;
            }

            BindingService.SyncData(_item, this);
            _itemService.SaveItem(_item);
        }

        private void _Model_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

            if (int.TryParse(_Model.Text, out var modelId))
            {
                pictureBox1.Image = _modelImageService.LoadItem(modelId);
            }
        }

        private void itemSearch_Click(object sender, EventArgs e)
        {
            var itemsearch = new MobSearch();

            itemsearch.SelectNpcClicked += (o, args) =>
            {
                LoadItem(o.ToString());
            };

            itemsearch.ShowDialog(this);
        }

        private void SetupDropdowns()
        {
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

        }

      
        #region BonusCatagory Code
        private void Bonus1Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus1Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus1Type);
            }
            else if (Bonus1Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus1Type);
            }
        }

        private void Bonus2Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus2Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus2Type);
            }
            else if (Bonus2Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus2Type);
            }
        }

        private void Bonus3Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus3Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus3Type);
            }
            else if (Bonus3Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus3Type);
            }
        }

        private void Bonus4Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus4Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus4Type);
            }
            else if (Bonus4Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus4Type);
            }
        }

        private void Bonus5Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus5Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus5Type);
            }
            else if (Bonus5Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus5Type);
            }
        }

        private void Bonus6Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus6Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus6Type);
            }
            else if (Bonus6Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus6Type);
            }
        }

        private void Bonus7Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus7Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus7Type);
            }
            else if (Bonus7Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus7Type);
            }
        }

        private void Bonus8Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus8Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus8Type);
            }
            else if (Bonus8Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus8Type);
            }
        }

        private void Bonus9Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus9Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus9Type);
            }
            else if (Bonus9Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus9Type);
            }
        }

        private void Bonus10Catagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Bonus10Catagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_Bonus10Type);
            }
            else if (Bonus10Catagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_Bonus10Type);
            }
        }

        private void ExtraBonusCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ExtraBonusCatagory.SelectedIndex == 1)
            {
                ComboboxService.BindBonusAll(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 2)
            {
                ComboboxService.BindBonusStats(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 3)
            {
                ComboboxService.BindBonusStatCap(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 4)
            {
                ComboboxService.BindBonusResists(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 5)
            {
                ComboboxService.BindBonusResistCap(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 6)
            {
                ComboboxService.BindBonusSkills(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 7)
            {
                ComboboxService.BindBonusFocus(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 8)
            {
                ComboboxService.BindBonusTOA(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 9)
            {
                ComboboxService.BindBonusOther(_ExtraBonusType);
            }
            else if (ExtraBonusCatagory.SelectedIndex == 10)
            {
                ComboboxService.BindBonusMythical(_ExtraBonusType);
            }
        }
        #endregion

    }
}
