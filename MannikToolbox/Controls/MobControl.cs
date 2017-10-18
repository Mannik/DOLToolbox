using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using MannikToolbox.Forms;
using MannikToolbox.Services;

namespace MannikToolbox.Controls
{
    public partial class MobControl : UserControl
    {
        private readonly MobService _mobService;
        private readonly ModelImageService _modelImageService;
        private Mob _mob;

        public MobControl()
        {
            InitializeComponent();
            _mobService = new MobService();
            _modelImageService = new ModelImageService();
        }

        private void MobControl_Load(object sender, EventArgs e)
        {
            //var mob = _mobService.GetMob("0016de40-8dc3-4cd6-aefd-e34b9f720fa7");

            SetupDropdowns();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialogBox
            {
                Caption = {Text = @"Please enter Mob ID"}
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                LoadMob(dialog.Input.Text);
            }
            dialog.Dispose();
        }

        private void LoadMob(string mobId)
        {
            if (string.IsNullOrWhiteSpace(mobId))
            {
                return;
            }

            _mob = _mobService.GetMob(mobId);

            if (_mob == null)
            {
                return;
            }

            pictureBox1.Image = _modelImageService.LoadMob(_mob.Model);

            BindingService.BindData(_mob, this);
            BindFlags();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_mob == null)
            {
                return;
            }

            BindingService.SyncData(_mob, this);
            SyncFlags();
            _mobService.SaveMob(_mob);
        }

        private void _Model_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

            if (int.TryParse(_Model.Text, out var modelId))
            {
                pictureBox1.Image = _modelImageService.LoadMob(modelId);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mobsearch = new MobSearch();

            mobsearch.SelectNpcClicked += (o, args) =>
            {
                LoadMob(o.ToString());
            };
            
            mobsearch.Show(this);
        }

        private void SetupDropdowns()
        {
            ComboboxService.BindRaces(_Race);
            ComboboxService.BindRealms(_Realm);
            ComboboxService.BindGenders(_Gender);
            ComboboxService.BindRegions(_Region);
            ComboboxService.BindWeaponDamageTypes(_MeleeDamageType);
            ComboboxService.BindBodyTypes(_BodyType);
        }

        private void BindFlags()
        {
            var flagsArray = new BitArray(new[] { (int)_mob.Flags }).Cast<bool>().ToArray();

            _FlagsGhost.Checked = flagsArray[0];
            _FlagsStealth.Checked = flagsArray[1];
            _FlagsHideName.Checked = flagsArray[2];
            _FlagsNoTarget.Checked = flagsArray[3];
            _FlagsPeace.Checked = flagsArray[4];
            _FlagsFlying.Checked = flagsArray[5];
            _FlagsTorch.Checked = flagsArray[6];
            _FlagsStatue.Checked = flagsArray[7];
            _FlagsSwimming.Checked = flagsArray[8];
        }

        private void SyncFlags()
        {
            var boolArray = new[]
            {
                _FlagsGhost.Checked,
                _FlagsStealth.Checked,
                _FlagsHideName.Checked,
                _FlagsNoTarget.Checked,
                _FlagsPeace.Checked,
                _FlagsFlying.Checked,
                _FlagsTorch.Checked,
                _FlagsStatue.Checked,
                _FlagsSwimming.Checked
            };
            var flagsArray = new BitArray(boolArray);


            int[] array = new int[1];
            flagsArray.CopyTo(array, 0);
            _mob.Flags = (uint)array[0];
        }
    }
}
