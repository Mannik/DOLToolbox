using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Forms;
using DOLToolbox.Services;

namespace DOLToolbox.Controls
{
    public partial class SpellControl : UserControl
    {
        private readonly SpellService _spellService;
        private DBSpell _spell;

        public SpellControl()
        {
            InitializeComponent();
            _spellService = new SpellService();
        }

        private void SpellControl_Load(object sender, EventArgs e)
        {
            SetupDropdowns();
        }

        private async Task LoadSpell(string spellId)
        {
            Clear();

            if (string.IsNullOrWhiteSpace(spellId))
            {
                return;
            }

            _spell = await _spellService.Get(spellId);

            if (_spell == null)
            {
                MessageBox.Show($@"Object with ObjectId: {spellId} not found", @"Object not found");
                return;
            }

            BindingService.BindData(_spell, this);
        }

        private void SetupDropdowns()
        {
            ComboboxService.BindWeaponDamageTypes(_DamageType);
            ComboboxService.BindTargets(_Target);
            ComboboxService.BindInstrumentRequirements(_InstrumentRequirement);
        }

        private async void Insertspell_Click(object sender, EventArgs e)
        {
            _spell = new DBSpell
            {
                AllowAdd = true
            };

            SyncSpell();
            
            _spell.SpellID = _spellService.GetNextSpellId();
            
            var spellId = _spellService.Save(_spell);
            await LoadSpell(spellId);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialogBox
            {
                Caption = { Text = @"Please enter Spell ID" }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                await LoadSpell(dialog.Input.Text);
            }
            dialog.Dispose();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (_spell == null)
            {
                return;
            }

            SyncSpell();

            var spellId = _spellService.Save(_spell);
            await LoadSpell(spellId);
        }

        private void SyncSpell()
        {
            BindingService.SyncData(_spell, this);
        }

        private void Clear()
        {
            _spell = null;
            BindingService.ClearData(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_spell == null)
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

            _spellService.Delete(_spell);
            Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var search = new SpellSearchForm();

            search.SelectClicked += async (o, args) =>
            {
                if (!(o is DBSpell item))
                {
                    return;
                }

                await LoadSpell(item.ObjectId);

            };

            search.ShowDialog(this);
        }
    }
}
