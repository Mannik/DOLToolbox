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
            //var spell = _spellService.GetSpell("0016de40-8dc3-4cd6-aefd-e34b9f720fa7");

            SetupDropdowns();
        }

        private async Task LoadSpell(string spellId)
        {
            if (string.IsNullOrWhiteSpace(spellId))
            {
                return;
            }

            _spell = await _spellService.Get(spellId);

            if (_spell == null)
            {
                return;
            }

            BindingService.BindData(_spell, this);
        }

   
      

        private void SetupDropdowns()
        {
            ComboboxService.BindWeaponDamageTypes(_DamageType);
            ComboboxService.BindTargets(Target);
            ComboboxService.BindInstrumentRequirements(_InstrumentRequirement);
        }

        private void Insertspell_Click(object sender, EventArgs e)
        {
            _spell = new DBSpell
            {
                AllowAdd = true
            };

            SyncSpell();

            _spell.ObjectId = null;
            _spell.SpellID = _spellService.GetNextSpellId();

            _spellService.Save(_spell);
            BindingService.ClearData(this);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (_spell == null)
            {
                return;
            }

            SyncSpell();
            _spellService.Save(_spell);
            BindingService.ClearData(this);
        }

        private void SyncSpell()
        {
            BindingService.SyncData(_spell, this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BindingService.ClearData(this);
        }
    }
}
