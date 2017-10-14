using System;
using System.Windows.Forms;
using DOL.Database;

namespace MannikToolbox.Controls
{
    public partial class SpellControl : UserControl
    {
        public SpellControl()
        {
            InitializeComponent();
        }

        private void SpellControl_Load(object sender, EventArgs e)
        {
            cmbxDmgType.SelectedIndex = -1;
            cmbxInstrument.SelectedIndex = 0;
        }


        #region Spell Insert Region
        #region DefineSpellVariables
        string Spell_ID, SpellName, Description, Target, Type;
        string Message1, Message2, Message3, Message4, PackageID;
        int SpellID, ClientEffect, SpellIcon, Range, Power, DamageType, Duration, Frequency;

        int Pulse, PulsePower, Radius, RecastDelay, ResurrectHealth, ResurrectMana;
        int Concentration, LifeDrainReturn, AmnesiaChance, InstrumentRequirement, SpellGroup;
        int EffectGroup, SubSpellID, SharedTimerGroup, TooltipId;
        Boolean IsFocus = false, AllowBolt = false, IsSecondary = false, IsPrimary = false, Uninterruptible = false, MoveCast = false;
        Double CastTime, Damage, Value;
        DateTime LastTimeRowUpdated = DateTime.Now;
        string LineXSpell_ID, LinexName;
        int Level;

        #endregion
        public void GetSpellVariables()
        {
            SpellName = txtbxName.Text;
            Description = txtbxDescription.Text;
            Target = cmbxTarget.Text;
            Type = txtbxType.Text;
            Message1 = txtbxMessage1.Text; Message2 = txtbxMessage2.Text; Message3 = txtbxMessage3.Text; Message4 = txtbxMessage4.Text;
            PackageID = txtbxPackageID.Text;
            int.TryParse(txtbxSpellID.Text, out SpellID); int.TryParse(txtbxEffect.Text, out ClientEffect);
            int.TryParse(txtbxIcon.Text, out SpellIcon); int.TryParse(txtbxRange.Text, out Range);
            int.TryParse(txtbxCost.Text, out Power); int.TryParse(txtbxDuration.Text, out Duration);
            int.TryParse(txtbxFrequency.Text, out Frequency); int.TryParse(txtbxPulse.Text, out Pulse);
            int.TryParse(txtbxPulsePower.Text, out PulsePower); int.TryParse(txtbxRadius.Text, out Radius);
            int.TryParse(txtbxCooldown.Text, out RecastDelay); int.TryParse(txtbxRezHP.Text, out ResurrectHealth);
            int.TryParse(txtbxRezMana.Text, out ResurrectMana); int.TryParse(txtbxCon.Text, out Concentration);
            int.TryParse(txtbxLifedrain.Text, out LifeDrainReturn); int.TryParse(txtbxAmnesia.Text, out AmnesiaChance);
            InstrumentRequirement = cmbxInstrument.SelectedIndex; int.TryParse(txtbxSpellGroup.Text, out SpellGroup);
            int.TryParse(txtbxEffectGroup.Text, out EffectGroup); int.TryParse(txtbxSubSpell.Text, out SubSpellID);
            int.TryParse(txtbxTimer.Text, out SharedTimerGroup); int.TryParse(txtbxTooltip.Text, out TooltipId);
            if (cxFocus.Checked == true)
            {
                IsFocus = true;
            }
            if (cxBolt.Checked == true)
            {
                AllowBolt = true;
            }
            if (cxMove.Checked == true)
            {
                MoveCast = true;
            }
            if (cxInterrupt.Checked == true)
            {
                Uninterruptible = true;
            }
            if (cxPrimary.Checked == true)
            {
                IsPrimary = true;
            }
            if (cxSecondary.Checked == true)
            {
                IsSecondary = true;
            }
            double.TryParse(txtbxCastTime.Text, out CastTime);
            double.TryParse(txtbxDamage.Text, out Damage);
            double.TryParse(txtbxValue.Text, out Value);

            if (cmbxDmgType.SelectedIndex > 3)
            {
                DamageType = cmbxDmgType.SelectedIndex + 6;
            }
            else
            { DamageType = cmbxDmgType.SelectedIndex; }

            Spell_ID = SpellName + SpellID;
            int.TryParse(txtbxLevel.Text, out Level);
            LinexName = txtbxLineName.Text;
            LineXSpell_ID = Spell_ID + "_" + LinexName;
        }

        public void ClearUI()
        {

            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else if (control is CheckBox)
                        (control as CheckBox).Checked = false;
                    else if (control is ComboBox)
                        (control as ComboBox).SelectedIndex = 0;
                    else
                        func(control.Controls);
            };

            func(Controls);
        }



        private void btnInsertSpell_Click_1(object sender, EventArgs e)
        {

            #region Some basic Error Checking : Loki

            if (String.IsNullOrEmpty(txtbxName.Text))
            {
                MessageBox.Show(@"Name cannot be blank! ");
                txtbxName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtbxSpellID.Text))
            {
                MessageBox.Show(@"SpellID cannot be blank! ");
                txtbxSpellID.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtbxEffect.Text))
            {
                MessageBox.Show(@"Effect cannot be blank! ");
                txtbxEffect.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtbxIcon.Text))
            {
                MessageBox.Show(@"Icon cannot be blank! ");
                txtbxIcon.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtbxDescription.Text))
            {
                MessageBox.Show(@"Description cannot be blank! ");
                txtbxDescription.Focus();
                return;
            }

            if (String.IsNullOrEmpty(cmbxTarget.Text))
            {
                MessageBox.Show(@"Target cannot be blank! ");
                cmbxTarget.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtbxTooltip.Text))
            {
                MessageBox.Show(@"Tooltip cannot be blank! ");
                txtbxTooltip.Focus();
                return;
            }
            #endregion
            // todo : Needs testing 
            #region SpellID is Unique so lets check : Loki
            var inv = DatabaseManager.Database.SelectObjects<DBSpell>("SpellID = '" + SpellID + "'");
            if (inv.Count > 0)
            {
                MessageBox.Show(@"This SpellID already exist in the Database!");
                txtbxSpellID.Clear();
                txtbxSpellID.Focus();
                return;
            }
            #endregion

            try
            {
                GetSpellVariables();
                DatabaseManager.SetDatabaseConnection();
                DBSpell spell = new DBSpell();
                spell.AllowAdd = true;
                spell.AllowBolt = AllowBolt;
                spell.AmnesiaChance = AmnesiaChance;
                spell.CastTime = CastTime;
                spell.ClientEffect = ClientEffect;
                spell.Concentration = Concentration;
                spell.Damage = Damage;
                spell.DamageType = DamageType;
                spell.Description = Description;
                spell.Duration = Duration;
                spell.EffectGroup = EffectGroup;
                spell.Frequency = Frequency;
                spell.Icon = SpellIcon;
                spell.InstrumentRequirement = InstrumentRequirement;
                spell.IsFocus = IsFocus;
                spell.IsPrimary = IsPrimary;
                spell.IsSecondary = IsSecondary;
                spell.LifeDrainReturn = LifeDrainReturn;
                spell.Message1 = Message1;
                spell.Message2 = Message2;
                spell.Message3 = Message3;
                spell.Message4 = Message4;
                spell.MoveCast = MoveCast;
                spell.Name = SpellName;
                spell.PackageID = PackageID;
                spell.Power = Power;
                spell.Pulse = Pulse;
                spell.PulsePower = PulsePower;
                spell.Radius = Radius;
                spell.Range = Range;
                spell.RecastDelay = RecastDelay;
                spell.ResurrectHealth = ResurrectHealth;
                spell.ResurrectMana = ResurrectMana;
                spell.SharedTimerGroup = SharedTimerGroup;
                spell.SpellGroup = SpellGroup;
                spell.SpellID = SpellID;
                spell.SubSpellID = SubSpellID;
                spell.Target = Target;
                spell.Type = Type;
                spell.Uninterruptible = Uninterruptible;
                spell.Value = Value;
                DatabaseManager.Database.AddObject(spell);
                MessageBox.Show("Spell added to the database!");
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cxLinexSpell.Checked == true)
            {
                try
                {
                    GetSpellVariables();
                    DatabaseManager.SetDatabaseConnection();
                    DBLineXSpell line = new DBLineXSpell();
                    line.Level = Level;
                    line.LineName = LinexName;
                    line.SpellID = SpellID;
                }
                catch (Exception g)
                {
                    MessageBox.Show(g.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

    }
}
