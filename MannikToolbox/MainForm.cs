
#region Change Log
// MySQL Connection builder created : Loki 
// Some Error Handling added : Loki
// Check for Unique SpellID before trying to insert : Loki
// Removed Default value 0 From textboxes as they are allowed NULL : Loki
#endregion

#region

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DOL.Database;
using MannikToolbox.Properties;
using MySql.Data.MySqlClient;

#endregion

namespace MannikToolbox
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ConfigExist()
        {
            // todo: allow users to change connection details!

            #region Create connection Details : Loki

            if (!File.Exists(Application.StartupPath + "AppConfig.xml"))
            {
                var result = MessageBox.Show(@"No config Create now!", @"Create Config", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                {
                    new MySqlConfig().ShowDialog(this);
                }
                if (result == DialogResult.No)
                {
                    MessageBox.Show(@"Sorry, you must create Connection Details!.");
                    Application.ExitThread();
                }
            }
            else
            {
                var xmlDoc = XDocument.Load(Application.StartupPath + "AppConfig.xml");
                var selectors = from elements in xmlDoc.Elements("root").Elements("Server")
                    .Elements("DBConnectionString")
                                select elements;
                foreach (var element in selectors)
                {
                    var str = element.Value;
                    var ext = str.Substring(0, str.LastIndexOf(";", StringComparison.Ordinal));
                    var csb = new MySqlConnectionStringBuilder(ext);
                    Settings.Default.Username = csb.UserID;
                    Settings.Default.Password = csb.Password;
                    Settings.Default.Hostname = csb.Server;
                    Settings.Default.Database = csb.Database;
                    Settings.Default.Port = csb.Port;
                    DatabaseManager.SetDatabaseConnection(Settings.Default.Hostname, Settings.Default.Port,
                        Settings.Default.Database, Settings.Default.Username, Settings.Default.Password);
                }
            }

            #endregion
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigExist();
            cmbxDmgType.SelectedIndex = 0;
            cmbxInstrument.SelectedIndex = 0;
            txtbxName.Focus();
        }

        #region global

        private string password = Settings.Default.Password;
        private string username = Settings.Default.Username;
        private string db = Settings.Default.Database;
        private uint port = Settings.Default.Port;
        private string host = Settings.Default.Hostname;

        #endregion

        #region Spell Insert Region

        #region DefineSpellVariables

        private string Spell_ID, SpellName, Description, Target, Type;
        private string Message1, Message2, Message3, Message4, PackageID;
        private int SpellID, ClientEffect, SpellIcon, Range, Power, DamageType, Duration, Frequency;
        private int Pulse, PulsePower, Radius, RecastDelay, ResurrectHealth, ResurrectMana;
        private int Concentration, LifeDrainReturn, AmnesiaChance, InstrumentRequirement, SpellGroup;
        private int EffectGroup, SubSpellID, SharedTimerGroup, TooltipId;
        private Boolean IsFocus, AllowBolt, IsSecondary, IsPrimary, Uninterruptible, MoveCast;
        private Double CastTime, Damage, Value;
        private DateTime LastTimeRowUpdated = DateTime.Now;
        private string LineXSpell_ID, LinexName;
        private int Level;

        #endregion

        public void GetSpellVariables()
        {
            SpellName = txtbxName.Text;
            Description = txtbxDescription.Text;
            Target = cmbxTarget.Text;
            Type = txtbxType.Text;
            Message1 = txtbxMessage1.Text;
            Message2 = txtbxMessage2.Text;
            Message3 = txtbxMessage3.Text;
            Message4 = txtbxMessage4.Text;
            PackageID = txtbxPackageID.Text;
            int.TryParse(txtbxSpellID.Text, out SpellID);
            int.TryParse(txtbxEffect.Text, out ClientEffect);
            int.TryParse(txtbxIcon.Text, out SpellIcon);
            int.TryParse(txtbxRange.Text, out Range);
            int.TryParse(txtbxCost.Text, out Power);
            int.TryParse(txtbxDuration.Text, out Duration);
            int.TryParse(txtbxFrequency.Text, out Frequency);
            int.TryParse(txtbxPulse.Text, out Pulse);
            int.TryParse(txtbxPulsePower.Text, out PulsePower);
            int.TryParse(txtbxRadius.Text, out Radius);
            int.TryParse(txtbxCooldown.Text, out RecastDelay);
            int.TryParse(txtbxRezHP.Text, out ResurrectHealth);
            int.TryParse(txtbxRezMana.Text, out ResurrectMana);
            int.TryParse(txtbxCon.Text, out Concentration);
            int.TryParse(txtbxLifedrain.Text, out LifeDrainReturn);
            int.TryParse(txtbxAmnesia.Text, out AmnesiaChance);
            InstrumentRequirement = cmbxInstrument.SelectedIndex;
            int.TryParse(txtbxSpellGroup.Text, out SpellGroup);
            int.TryParse(txtbxEffectGroup.Text, out EffectGroup);
            int.TryParse(txtbxSubSpell.Text, out SubSpellID);
            int.TryParse(txtbxTimer.Text, out SharedTimerGroup);
            int.TryParse(txtbxTooltip.Text, out TooltipId);
            if (cxFocus.Checked)
            {
                IsFocus = true;
            }
            if (cxBolt.Checked)
            {
                AllowBolt = true;
            }
            if (cxMove.Checked)
            {
                MoveCast = true;
            }
            if (cxInterrupt.Checked)
            {
                Uninterruptible = true;
            }
            if (cxPrimary.Checked)
            {
                IsPrimary = true;
            }
            if (cxSecondary.Checked)
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
            {
                DamageType = cmbxDmgType.SelectedIndex;
            }

            Spell_ID = (SpellName + SpellID);
            int.TryParse(txtbxLevel.Text, out Level);
            LinexName = txtbxLineName.Text;
            LineXSpell_ID = Spell_ID + "_" + LinexName;
        }

        public void ClearUI()
        {
            Action<Control.ControlCollection> func = null;

            func = controls =>
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

        private void btnInsertSpell_Click(object sender, EventArgs e)
        {
            // todo: Ensure only numerical values can be inserted into int textboxes, also prevent paste.
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
                DatabaseManager.SetDatabaseConnection(Settings.Default.Hostname, Settings.Default.Port,
                    Settings.Default.Database, Settings.Default.Username, Settings.Default.Password);
                var spell = new DBSpell();
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

                MessageBox.Show(@"Spell added to the database!");
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cxLinexSpell.Checked)
            {
                try
                {
                    GetSpellVariables();
                    DatabaseManager.SetDatabaseConnection(Settings.Default.Hostname, Settings.Default.Port,
                        Settings.Default.Database, Settings.Default.Username, Settings.Default.Password);
                    var line = new DBLineXSpell();
                    line.Level = Level;
                    line.LineName = LinexName;
                    line.SpellID = SpellID;
                }
                catch (Exception g)
                {
                    MessageBox.Show(g.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Item Insert Region

        //todo

        #endregion

        #region Quest Insert Region

        //todo

        #endregion

        #region NPCTemplate Insert Region

        //todo

        #endregion

        #region Input Validation

        //todo

        #endregion
    }
}