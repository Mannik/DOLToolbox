using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using MannikToolbox.Forms;
using MannikToolbox.Services;

namespace MannikToolbox.Controls
{
    public partial class SpellControl : UserControl
    {
        private readonly SpellService _spellService;
        private readonly ModelImageService _modelImageService;
        private DBSpell _spell;

        public SpellControl()
        {
            InitializeComponent();
            _spellService = new SpellService();
            _modelImageService = new ModelImageService();
        }

        private void SpellControl_Load(object sender, EventArgs e)
        {
            //var spell = _spellService.GetSpell("0016de40-8dc3-4cd6-aefd-e34b9f720fa7");

            SetupDropdowns();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialogBox
            {
                Caption = { Text = @"Please enter Spell ID" }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                LoadSpell(dialog.Input.Text);
            }
            dialog.Dispose();
        }

        private void LoadSpell(string spellId)
        {
            if (string.IsNullOrWhiteSpace(spellId))
            {
                return;
            }

            _spell = _spellService.GetSpell(spellId);

            if (_spell == null)
            {
                return;
            }

            BindingService.BindData(_spell, this);
        }

   
      

        private void SetupDropdowns()
        {
            ComboboxService.BindRaces(_DamageType);
            ComboboxService.BindRealms(_Target);
            ComboboxService.BindGenders(_InstrumentRequirement);
        }

        private void Insertspell_Click(object sender, EventArgs e)
        {
            if (_spell == null)
            {
                return;
            }

            BindingService.SyncData(_spell, this);

            _spellService.SaveSpell(_spell);
        }
    }
}
