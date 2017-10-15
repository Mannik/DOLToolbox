using System;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using DOL.GS;
using MannikToolbox.Forms;
using MannikToolbox.Services;

namespace MannikToolbox.Controls
{
    public partial class MobControl : UserControl
    {
        private readonly MobService _mobService;
        private Mob _mob;

        public MobControl()
        {
            InitializeComponent();
            _mobService = new MobService();
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
                _mob = _mobService.GetMob(dialog.Input.Text);

                if (_mob == null)
                {
                    return;
                }

                BindingService.BindData(_mob, this);
            }
            dialog.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_mob == null)
            {
                return;
            }

            BindingService.SyncData(_mob, this);
            _mobService.SaveMob(_mob);
        }

        private void SetupDropdowns()
        {
            var races = Enum.GetValues(typeof(eRace))
                .Cast<byte>()
                .Distinct()
                .Select(x => (int)x)
                .ToDictionary(x => x, x => Enum.GetName(typeof(eRace), x));
            _Race.DataSource = new BindingSource(races, null);
            _Race.DisplayMember = "Value";
            _Race.ValueMember = "Key";

        }
    }
}
