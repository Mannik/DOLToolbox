using System.Collections;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;
using MannikToolbox.Forms;
using MannikToolbox.Services;

namespace MannikToolbox.Controls
{
    public partial class NpcTemplateControl : UserControl
    {
        private readonly NpcTemplateService _npcTemplateService = new NpcTemplateService();
        private DBNpcTemplate _template;

        public NpcTemplateControl()
        {
            InitializeComponent();
            SetupDropdowns();
        }

        private void SetupDropdowns()
        {
            ComboboxService.BindRaces(_Race);
            ComboboxService.BindGenders(_Gender);
            ComboboxService.BindWeaponDamageTypes(_MeleeDamageType);
            ComboboxService.BindBodyTypes(_BodyType);
        }

        private void BindFlags()
        {
            var flagsArray = new BitArray(new[] { (int)_template.Flags }).Cast<bool>().ToArray();

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
            _template.Flags = (ushort)array[0];
        }

        private void BindWeaponSlots()
        {
            switch (_template.VisibleWeaponSlots)
            {
                case 16:
                    _WpnSlotsSB.Checked = true;
                    break;
                case 34:
                    _WpnSlots2H.Checked = true;
                    break;
                case 51:
                    _WpnSlotsRange.Checked = true;
                    break;
                case 240:
                    _WpnSlots1H.Checked = true;
                    break;
                default:
                    _WpnSlotsNone.Checked = true;
                    break;
            }
        }

        private void SyncWeaponSlots()
        {
            if (_WpnSlotsSB.Checked)
            {
                _template.VisibleWeaponSlots = 16;
                return;
            }

            if (_WpnSlots2H.Checked)
            {
                _template.VisibleWeaponSlots = 34;
                return;
            }

            if (_WpnSlotsRange.Checked)
            {
                _template.VisibleWeaponSlots = 51;
                return;
            }

            if (_WpnSlots1H.Checked)
            {
                _template.VisibleWeaponSlots = 240;
                return;
            }

            _template.VisibleWeaponSlots = 255;
        }

        private void LoadItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            _template = _npcTemplateService.Get(id);

            if (_template == null)
            {
                return;
            }

            BindingService.BindData(_template, this);
            BindFlags();
            BindWeaponSlots();
        }

        private void btnLoad_Click(object sender, System.EventArgs e)
        {
            var dialog = new InputDialogBox
            {
                Caption = { Text = @"Please enter Mob ID" }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                LoadItem(dialog.Input.Text);
            }
            dialog.Dispose();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (_template == null)
            {
                return;
            }

            BindingService.SyncData(_template, this);
            SyncFlags();
            SyncWeaponSlots();
            _npcTemplateService.Save(_template);
        }
    }
}
