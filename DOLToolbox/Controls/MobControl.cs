﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Forms;
using DOLToolbox.Services;

namespace DOLToolbox.Controls
{
    public partial class MobControl : UserControl
    {
        private readonly MobService _mobService;
        private readonly ImageService _modelImageService;
        private Mob _mob;
        private Dictionary<int, string> _raceResists;

        public MobControl()
        {
            InitializeComponent();
            _mobService = new MobService();
            _modelImageService = new ImageService();
        }

        private async void MobControl_Load(object sender, EventArgs e)
        {
            await FillRaceResists();
            await SetupDropdowns();
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
                return;

            _mob = _mobService.GetMob(mobId);

            if (_mob == null)
            {
                MessageBox.Show($@"Object with ObjectId: {mobId} not found", @"Object not found");
                return;
            }
            
            _modelImageService.LoadMob(_mob.Model, pictureBox1.Width, pictureBox1.Height)
                .ContinueWith(x => _modelImageService.AttachImage(pictureBox1, x));

            BindingService.BindData(_mob, this);
            BindFlags();
            BindWeaponSlots();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_mob == null)
            {
                _mob = new Mob
                {
                    ObjectId = null
                };
            }

            try
            {
                BindingService.SyncData(_mob, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            SyncFlags();
            SyncWeaponSlots();
            _mobService.SaveMob(_mob);
            BindingService.ClearData(this);
        }

        private void _Model_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

            if (int.TryParse(_Model.Text, out var modelId))
            {
                _modelImageService.LoadMob(modelId, pictureBox1.Width, pictureBox1.Height)
                    .ContinueWith(x => _modelImageService.AttachImage(pictureBox1, x));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mobsearch = new MobSearch();

            mobsearch.SelectNpcClicked += (o, args) => { LoadMob(((Mob)o).ObjectId); };

            mobsearch.ShowDialog(this);
        }

        private async Task SetupDropdowns()
        {
            await ComboboxService.BindMobRaces(_Race);
            ComboboxService.BindRealms(_Realm);
            ComboboxService.BindGenders(_Gender);
            ComboboxService.BindRegions(_Region);
            ComboboxService.BindWeaponDamageTypes(_MeleeDamageType);
            ComboboxService.BindBodyTypes(_BodyType);
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

        private void BindFlags()
        {
            var flagsArray = new BitArray(new[] {(int) _mob.Flags}).Cast<bool>().ToArray();

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


            var array = new int[1];
            flagsArray.CopyTo(array, 0);
            _mob.Flags = (uint) array[0];
        }

        private void BindWeaponSlots()
        {
            switch (_mob.VisibleWeaponSlots)
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
                _mob.VisibleWeaponSlots = 16;
                return;
            }

            if (_WpnSlots2H.Checked)
            {
                _mob.VisibleWeaponSlots = 34;
                return;
            }

            if (_WpnSlotsRange.Checked)
            {
                _mob.VisibleWeaponSlots = 51;
                return;
            }

            if (_WpnSlots1H.Checked)
            {
                _mob.VisibleWeaponSlots = 240;
                return;
            }

            _mob.VisibleWeaponSlots = 255;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ClearMob();
        }

        private void ClearMob()
        {
            _mob = null;
            pictureBox1.Image = null;
            BindingService.ClearData(this);
        }

        private void _Race_DrawItem(object sender, DrawItemEventArgs e)
        {
            if(e.Index == -1)
            {
                return;
            }

            e.DrawBackground();
            using (var br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(_Race.GetItemText(_Race.Items[e.Index]), e.Font, br, e.Bounds);
            }

            if ((e.State & DrawItemState.Selected) != DrawItemState.Selected) return;
            var selected = _Race.SelectedItem as ComboboxService.SelectItemModel;
            if (_raceResists != null && selected?.Id != null && _raceResists.ContainsKey(selected.Id.Value))
            {
                toolTip1.Show(_raceResists[selected.Id.Value], _Race, e.Bounds.Right, e.Bounds.Bottom);
            }
            else
            {
                toolTip1.Hide(_Race);
            }

            e.DrawFocusRectangle();
        }

        private void _Race_DropDownClosed(object sender, EventArgs e) => toolTip1.Hide(_Race);

        private void button_click_delete_mob(object sender, EventArgs e)
        {
            if (_mob == null)
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

            _mobService.DeleteMob(_mob);
            ClearMob();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var modelViewer = new ModelViewerSearchForm(ModelViewerSearchForm.ModelType.Mob);

            modelViewer.SelectClicked += (o, args) => { _Model.Text = o.ToString(); };

            modelViewer.Show(this);
        }
    }
}