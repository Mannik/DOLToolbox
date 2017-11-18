using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Forms;
using DOLToolbox.Services;

namespace DOLToolbox.Controls
{
    public partial class NpcTemplateControl : UserControl
    {
        private readonly NpcTemplateService _npcTemplateService = new NpcTemplateService();
        private readonly ToolTip _toolTip;
        private DBNpcTemplate _template;
        private Dictionary<int, string> _raceResists;

        public NpcTemplateControl()
        {
            InitializeComponent();

            _toolTip = new ToolTip
            {
                AutoPopDelay = 10000,
                InitialDelay = 500,
                ReshowDelay = 500,
                ShowAlways = true
            };
        }

        private async void NpcTemplateControl_Load(object sender, System.EventArgs e)
        {
            await FillRaceResists();
            await SetupDropdowns();
        }

        private async Task FillRaceResists()
        {
            await Task.Run(() =>
            {
                _raceResists = DatabaseManager.Database.SelectAllObjects<Race>()
                    .ToDictionary(x => x.ID,
                        x =>
                            $"Crush: {x.ResistCrush}, Slash: {x.ResistSlash}, Thrust: {x.ResistThrust}\nBody: {x.ResistBody}, Cold: {x.ResistCold}, Energy: {x.ResistEnergy}\nHeat: {x.ResistHeat}, Matter: {x.ResistMatter}, Spirit: {x.ResistSpirit}\nNatural: {x.ResistNatural}");
            });
        }

        private async Task SetupDropdowns()
        {
            await ComboboxService.BindMobRaces(_Race);
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
                MessageBox.Show($@"Object with ObjectId: {id} not found", @"Object not found");
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
                Caption = { Text = @"Please enter object id" }
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
            BindingService.ClearData(this);
        }

        private void _Race_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selected = _Race.SelectedItem as ComboboxService.SelectItemModel;
            if (_raceResists != null && selected?.Id != null && _raceResists.ContainsKey(selected.Id.Value))
            {
                _toolTip.SetToolTip(label30, _raceResists[selected.Id.Value]);
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            var dialogResult = MessageBox.Show(@"You are about to create a new NPC Template record.\nAre you sure this is what you want to do?", @"Insert new NPC Template", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes)
            {
                return;
            }
            _template = new DBNpcTemplate();
            BindingService.SyncData(_template, this);
            SyncFlags();
            SyncWeaponSlots();

            _template.ObjectId = null;
            _npcTemplateService.Save(_template);
            BindingService.ClearData(this);
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            Clear();
            _template = null;
        }

        private void Clear()
        {
            BindingService.ClearData(this);
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            if (_template == null)
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

            _npcTemplateService.Delete(_template);
            Clear();
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            var search = new NpcTemplateSearchForm();

            search.SelectClicked += (o, args) =>
            {
                if (!(o is DBNpcTemplate item))
                {
                    return;
                }

                LoadItem(item.ObjectId);

            };

            search.ShowDialog(this);
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            var modelViewer = new ModelViewerSearchForm(ModelViewerSearchForm.ModelType.Mob);

            modelViewer.SelectClicked += (o, args) =>
            {
                _Model.Text = $@"{_Model.Text};{o.ToString()}".Trim(';');
            };

            modelViewer.Show(this);
        }
    }
}
