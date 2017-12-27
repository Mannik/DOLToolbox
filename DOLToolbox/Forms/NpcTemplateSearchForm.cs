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
    public partial class NpcTemplateSearchForm : Form
    {
        private readonly NpcTemplateService _npcTemplateService = new NpcTemplateService();
        private readonly ImageService _imageService = new ImageService();
        
        private List<DBNpcTemplate> _allData;
        private List<DBNpcTemplate> _data;
        private int _page;
        private int _pageSize = 50;
        private int _selectedIndex;

        public event EventHandler SelectClicked;

        public NpcTemplateSearchForm()
        {
            InitializeComponent();
        }

        private async void SpellSearchForm_Load(object sender, EventArgs e)
        {
            var loading = new LoadingForm
            {
                ProgressText = { Text = @"Loading: Templates" }
            };
            loading.Show();

            _allData = await _npcTemplateService.Get();

            loading.Close();
            GetPage();
        }
        private void GetPage(bool paging = false)
        {
            dataGridView1.Rows.Clear();

            if (!paging)
            {
                var filter = txtFilter.Text;
                if (string.IsNullOrWhiteSpace(filter))
                {
                    _data = _allData.ToList();
                }
                else
                {
                    filter = filter.ToWildcardRegex();
                    _data = _allData.Where(x => Regex.IsMatch(x.Name, filter, RegexOptions.IgnoreCase)).ToList();
                }

            }

            var page = _data
                .Skip(_page * _pageSize)
                .Take(_pageSize)
                .ToList();

            var bindingList = new BindingList<DBNpcTemplate>(page);
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
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TemplateId",
                HeaderText = @"TemplateId",
                Name = "TemplateId",
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ClassType",
                HeaderText = @"ClassType",
                Name = "ClassType",
            });
			foreach (DataGridViewColumn column in dataGridView1.Columns)
			{
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			}
		}

        private DBNpcTemplate GetSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return null;
            }

            return dataGridView1.SelectedRows[0].DataBoundItem as DBNpcTemplate;
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

        // First
        private void button3_Click(object sender, EventArgs e)
        {
            if (_page == 0)
            {
                return;
            }

            _page = 0;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _page = 0;
            _selectedIndex = 0;
            GetPage();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFilter.Clear();
            _selectedIndex = 0;
            _page = 0;
            GetPage();
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

        private void SpellSearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _allData = null;
            _data = null;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var selected = GetSelected();
            if (selected == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(selected.Model))
            {
                var modelId = new String(selected.Model.TakeWhile(Char.IsDigit).ToArray());

                if(int.TryParse(modelId, out int model))
                {
                    _imageService.LoadMob(model, pictureBox1.Width, pictureBox1.Height)
                        .ContinueWith(x => _imageService.AttachImage(pictureBox1, x));
                }
            }
        }
    }
}
