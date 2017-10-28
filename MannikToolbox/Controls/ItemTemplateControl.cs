﻿using System;
using System.Linq;
using System.Reflection;
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
                case 1:
                    ComboboxService.BindBonusAll(type);
                    break;
                case 2:
                    ComboboxService.BindBonusStats(type);
                    break;
                case 3:
                    ComboboxService.BindBonusStatCap(type);
                    break;
                case 4:
                    ComboboxService.BindBonusResists(type);
                    break;
                case 5:
                    ComboboxService.BindBonusResistCap(type);
                    break;
                case 6:
                    ComboboxService.BindBonusSkills(type);
                    break;
                case 7:
                    ComboboxService.BindBonusFocus(type);
                    break;
                case 8:
                    ComboboxService.BindBonusToa(type);
                    break;
                case 9:
                    ComboboxService.BindBonusOther(type);
                    break;
                case 10:
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
    }
}