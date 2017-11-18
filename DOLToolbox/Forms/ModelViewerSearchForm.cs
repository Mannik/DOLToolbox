using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DOL.Util.DOLModelViewer;
using DOLToolbox.Services;

namespace DOLToolbox.Forms
{
    public partial class ModelViewerSearchForm : Form
    {
        private readonly ImageService _imageService = new ImageService();
        public event EventHandler SelectClicked;

        public enum ModelType
        {
            Item, Mob
        }

        private ModelType _type = ModelType.Item;

        public ModelViewerSearchForm()
        {
            InitializeComponent();
        }


        public ModelViewerSearchForm(ModelType type)
        {
            InitializeComponent();
            _type = type;

            if (_type == ModelType.Mob)
            {
                rdMob.Checked = true;
            }
            else
            {
                rdItem.Checked = true;
            }
        }

        private void ModelViewerSearchForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
            SetTypes();
        }

        private void SetTypes()
        {
            cboType.Items.Clear();

            if (_type == ModelType.Item)
            {
                cboType.Items.Add("");
                cboType.Items.Add("Armor");
                cboType.Items.Add("Weapon");
                cboType.Items.Add("Siege");
                cboType.Items.Add("Housing");
                cboType.Items.Add("World");
                cboType.Items.Add("Inventory");
                cboType.Items.Add("Other");
                return;
            }

            cboType.Items.Add("");
            cboType.Items.Add("Biped");
            cboType.Items.Add("Female");
            cboType.Items.Add("Vampiir");
            cboType.Items.Add("Demon");
            cboType.Items.Add("Animal");
            cboType.Items.Add("Other");
        }

        private void LoadGrid()
        {
            if (_type == ModelType.Item)
            {
                LoadItems();
                return;
            }

            LoadMobs();
        }

        private void LoadItems()
        {
            var items = ModelViewerService.Viewer.GetItems().AsQueryable();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                items = items.Where(x => 
                    x.Name.IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    x.Category.IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(cboType.SelectedItem?.ToString()))
            {
                var type = cboType.SelectedItem.ToString();
                
                switch (type)
                {
                    case "Armor":
                        items = items.Where(x => x.IsArmor);
                        break;
                    case "Weapon":
                        items = items.Where(x => x.IsWeapon);
                        break;
                    case "Siege":
                        items = items.Where(x => x.IsSiegeWeapon);
                        break;
                    case "Housing":
                        items = items.Where(x => x.IsHousingItem);
                        break;
                    case "World":
                        items = items.Where(x => x.IsWorldObject);
                        break;
                    case "Inventory":
                        items = items.Where(x => x.IsInventory);
                        break;
                    case "Other":
                        items = items.Where(x => x.IsOther);
                        break;
                }
            }

            var bindingList = new BindingList<Item>(items.ToList());
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

            SetGridColumns();
        }

        private void LoadMobs()
        {
            var mobs = ModelViewerService.Viewer.GetMobs().AsQueryable();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                mobs = mobs.Where(x =>
                    x.Name.IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(cboType.SelectedItem?.ToString()))
            {
                var type = cboType.SelectedItem.ToString();

                switch (type)
                {
                    case "Biped":
                        mobs = mobs.Where(x => x.IsBiped);
                        break;
                    case "Female":
                        mobs = mobs.Where(x => x.IsFemale);
                        break;
                    case "Vampiir":
                        mobs = mobs.Where(x => x.IsVampiir);
                        break;
                    case "Demon":
                        mobs = mobs.Where(x => x.IsDemon);
                        break;
                    case "Animal":
                        mobs = mobs.Where(x => x.IsAnimal);
                        break;
                    case "Other":
                        mobs = mobs.Where(x => x.IsOther);
                        break;
                }
            }
            var bindingList = new BindingList<Mob>(mobs.ToList());
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;

            SetGridColumns();
        }

        private void SetGridColumns()
        {
            dataGridView1.Columns.Clear();

            if (_type == ModelType.Item)
            {
                SetItemGridColumns();
                return;
            }

            SetMobGridColumns();
        }

        private void SetMobGridColumns()
        {
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ModelId",
                HeaderText = @"ModelId",
                Name = "ModelId",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = @"Name",
                Name = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsBiped",
                HeaderText = @"IsBiped",
                Name = "IsBiped",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsFemale",
                HeaderText = @"IsFemale",
                Name = "IsFemale",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsVampiir",
                HeaderText = @"IsVampiir",
                Name = "IsVampiir",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsDemon",
                HeaderText = @"IsDemon",
                Name = "IsDemon",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsAnimal",
                HeaderText = @"IsAnimal",
                Name = "IsAnimal",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsOther",
                HeaderText = @"IsOther",
                Name = "IsOther",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "MountId",
                HeaderText = @"MountId",
                Name = "MountId",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void SetItemGridColumns()
        {
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ModelId",
                HeaderText = @"ModelId",
                Name = "ModelId",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = @"Name",
                Name = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Category",
                HeaderText = @"Category",
                Name = "Category",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsArmor",
                HeaderText = @"IsArmor",
                Name = "IsArmor",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsWeapon",
                HeaderText = @"IsWeapon",
                Name = "IsWeapon",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "WeaponHandName",
                HeaderText = @"WeaponHand",
                Name = "WeaponHandName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsSiegeWeapon",
                HeaderText = @"IsSiegeWeapon",
                Name = "IsSiegeWeapon",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsHousingItem",
                HeaderText = @"IsHousingItem",
                Name = "IsHousingItem",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsWorldObject",
                HeaderText = @"IsWorldObject",
                Name = "IsWorldObject",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsInventory",
                HeaderText = @"IsInventory",
                Name = "IsInventory",
                Width = 20
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsOther",
                HeaderText = @"IsOther",
                Name = "IsOther",
                Width = 20
            });
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rb))
            {
                return;
            }

            _type = !rb.Checked ? ModelType.Item : ModelType.Mob;

            Clear();
            SetTypes();
            LoadGrid();
        }

        private void Clear()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            cboType.SelectedIndex = -1;
            txtSearch.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var selected = GetSelected();

            if (selected == null)
            {
                return;
            }

            if (_type == ModelType.Item)
            {
                _imageService.LoadItem(selected.ModelId, pictureBox1.Width, pictureBox1.Height)
                    .ContinueWith(x => pictureBox1.Image = x.Result);
            }
            else
            {
                _imageService.LoadMob(selected.ModelId, pictureBox1.Width, pictureBox1.Height)
                    .ContinueWith(x => pictureBox1.Image = x.Result);
            }
        }

        private BaseDataObject GetSelected()
        {

            if (dataGridView1.SelectedRows.Count < 1)
            {
                return null;
            }

            return dataGridView1.SelectedRows[0].DataBoundItem as BaseDataObject;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selected = GetSelected();

            if (selected == null)
            {
                return;
            }

            SelectClicked?.Invoke(selected.ModelId, e);
            Close();
        }
    }
}
