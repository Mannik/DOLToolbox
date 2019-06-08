using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Forms;
using DOLToolbox.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DOLToolbox.Controls
{
    public partial class RewardQuestControl : UserControl
    {
        private readonly RewardQuestService _questService;
        private DBDQRewardQ _quest;

        string goalType, questGoals, goalRepeatNo, goalTargetName, goalTargetText, collectItemTemplate, goalStepNo;
        string optionalRewardItemTemplates = "", finalRewardItemTemplates = "";
        string allowedClasses;
        string xloc, yloc, zoneId;
        string finishNPC;
        int stepCount;

        // using this to determine if quest can be saved back to original ID
        private bool LoadedQuest { get; set; } = false;

        // goal info
        public Dictionary<int, string> goaltype_dictionary;
        public Dictionary<int, string> goaltext_dictionary;
        public Dictionary<int, string> questgoalsformatted_dictionary;
        public Dictionary<int, string> goalrepeatno_dictionary;
        public Dictionary<int, string> goaltargetname_dictionary;
        public Dictionary<int, string> goaltargettext_dictionary;
        public Dictionary<int, string> colitem_dictionary;
        public Dictionary<int, string> goalstepno_dictionary;
        public Dictionary<int, string> xloc_dictionary;
        public Dictionary<int, string> yloc_dictionary;
        public Dictionary<int, string> zoneid_dictionary;
        // item rewards
        public Dictionary<int, string> opt_dictionary;
        public Dictionary<int, string> fin_dictionary;
        // quest restrictions
        private Dictionary<int, string> allClasses;

        public RewardQuestControl()
        {
            InitializeComponent();
            _questService = new RewardQuestService();

            opt_dictionary = new Dictionary<int, string>();
            fin_dictionary = new Dictionary<int, string>();
            //advtext_dictionary = new Dictionary<int, string>();
            colitem_dictionary = new Dictionary<int, string>();
            goalrepeatno_dictionary = new Dictionary<int, string>();
            goaltargetname_dictionary = new Dictionary<int, string>();
            goaltargettext_dictionary = new Dictionary<int, string>();
            goaltext_dictionary = new Dictionary<int, string>();
            questgoalsformatted_dictionary = new Dictionary<int, string>();
            goaltype_dictionary = new Dictionary<int, string>();
            goalstepno_dictionary = new Dictionary<int, string>();
            xloc_dictionary = new Dictionary<int, string>();
            yloc_dictionary = new Dictionary<int, string>();
            zoneid_dictionary = new Dictionary<int, string>();
            allClasses = new Dictionary<int, string>();

            PopulateClassDictionary();
        }

        // display all the quests currently in the database
        private void questSearch_Click(object sender, EventArgs e)
        {
            var search = new RewardQuestSearchForm();

            search.SelectClicked += async (o, args) =>
            {
                if (!(o is DBDQRewardQ item))
                {
                    return;
                }

                await LoadQuest(item.ID.ToString());

            };

            search.ShowDialog(this);
        }

        // load a quest from an ID
        private async void questLoad_Click(object sender, EventArgs e)
        {
            var dialog = new InputDialogBox
            {
                Caption = { Text = @"Please enter Quest ID" }
            };

            if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.Input.Text))
            {
                await LoadQuest(dialog.Input.Text);
            }
            dialog.Dispose();
        }

        private async Task LoadQuest(string questId)
        {
            Clear();

            if (string.IsNullOrWhiteSpace(questId))
            {
                return;
            }

            _quest = await _questService.Get(questId);

            if (_quest == null)
            {
                MessageBox.Show($@"Object with ObjectId: {questId} not found", @"Object not found");
                return;
            }

            BindingService.BindData(_quest, this);
            // need to grab the serialized data and turn it back to usable form            
            DeserializeData(_quest);

        }

        /// <summary>
        /// convert database format kill bandit;2|talk to NPC;3 to usable format
        /// </summary>        
        private void DeserializeData(DBDQRewardQ quest)
        {
            if (_quest == null)
            {
                return;
            }

            try
            {
                questGoals = quest.QuestGoals;
                goalType = quest.GoalType;
                goalRepeatNo = quest.GoalRepeatNo;
                goalTargetName = quest.GoalTargetName;
                goalTargetText = quest.GoalTargetText;
                collectItemTemplate = quest.CollectItemTemplate;
                string rawOptRewards = quest.OptionalRewardItemTemplates;
                string numOptChoices = "1";
                //string numOptChoices = string.IsNullOrWhiteSpace(rawOptRewards) ? "1" : rawOptRewards.Substring(0, 1), optionalRewardItemTemplates = rawOptRewards.Substring(1);
                if (!string.IsNullOrWhiteSpace(rawOptRewards))
                {
                    numOptChoices = rawOptRewards.Substring(0, 1);
                    optionalRewardItemTemplates = rawOptRewards.Substring(1);
                }
                finalRewardItemTemplates = quest.FinalRewardItemTemplates;
                allowedClasses = quest.AllowedClasses;
                xloc = quest.XOffset;
                yloc = quest.YOffset;
                zoneId = quest.ZoneID;

                StringBuilder gtype = new StringBuilder(goalType);
                gtype.Replace("2", "Search");
                gtype.Replace("3", "Kill");
                gtype.Replace("5", "InteractFinish");
                gtype.Replace("6", "InteractWhisper");
                gtype.Replace("7", "InteractDeliver");
                gtype.Replace("4", "Interact");
                goalType = gtype.ToString();
                string[] splitGoalType = goalType.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitGoalType, goaltype_dictionary);
                _GoalType.Text = goaltype_dictionary[1];

                string[] splitGoalRepeatNo = goalRepeatNo.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitGoalRepeatNo, goalrepeatno_dictionary);
                _GoalRepeatNo.Text = goalrepeatno_dictionary[1];

                string[] splitgoalTargetName = goalTargetName.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitgoalTargetName, goaltargetname_dictionary);
                _GoalTargetName.Text = goaltargetname_dictionary[1];

                string[] splitgoalTargetText = goalTargetText.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitgoalTargetText, goaltargettext_dictionary);
                _GoalTargetText.Text = goaltargettext_dictionary[1];

                string[] splitcollectItemTemplate = collectItemTemplate.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitcollectItemTemplate, colitem_dictionary);
                _CollectItemTempate.Text = colitem_dictionary[1];

                string[] splitOptionalRewards = optionalRewardItemTemplates.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitOptionalRewards, opt_dictionary);
                _OptionalReward.Text = opt_dictionary[1];
                int.TryParse(numOptChoices, out int choices);
                OptRewardUpDown.Value = choices;

                string[] splitFinalRewards = finalRewardItemTemplates.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitFinalRewards, fin_dictionary);
                _FinalReward.Text = fin_dictionary[1];

                string[] splitXloc = xloc.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitXloc, xloc_dictionary);
                _XOffset.Text = xloc_dictionary[1];

                string[] splitYloc = yloc.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitYloc, yloc_dictionary);
                _YOffset.Text = yloc_dictionary[1];

                string[] splitZoneId = zoneId.Split(new string[] { "|" }, StringSplitOptions.None);
                StringToDictionary(splitZoneId, zoneid_dictionary);
                _ZoneID.Text = zoneid_dictionary[1];

                var splitGoals = questGoals.Split('|');
                for (int i = 1; i < splitGoals.Length + 1; i++)
                {
                    var groupItems = splitGoals[i - 1].Split(';');
                    goaltext_dictionary[i] = groupItems[0];
                    goalstepno_dictionary[i] = groupItems[1];
                }
                _QuestGoals.Text = goaltext_dictionary[1];
                GoalStepNo.Text = goalstepno_dictionary[1];

                string[] splitAllowedClasses;
                if (!string.IsNullOrWhiteSpace(allowedClasses))
                {
                    splitAllowedClasses = allowedClasses.Split(new string[] { "|" }, StringSplitOptions.None);

                    for (int i = 0; i < splitAllowedClasses.Length; i++)
                    {
                        int.TryParse(splitAllowedClasses[i], out int result);
                        splitAllowedClasses[i] = allClasses[result];
                    }
                    for (int i = 0; i < splitAllowedClasses.Length; i++)
                    {
                        for (int j = 0; j < listClasses.Items.Count; j++)
                        {
                            string cls = listClasses.Items[j].ToString();
                            if (cls == splitAllowedClasses[i])
                            {
                                listClasses.SetSelected(j, true);
                                continue;
                            }
                        }
                    }
                }

                LoadedQuest = true;
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error while deserializing data! Quest was not loaded completely - Errors in database format.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // convert database string entries to dictionary for quest goal usage
        private void StringToDictionary(string[] str, Dictionary<int, string> dict)
        {
            if (str == null)
            {
                return;
            }

            for (int i = 0; i < str.Length; i++)
            {
                dict.Add(i + 1, str[i]);
            }
        }

        // attempt to save the current quest
        private void questSave_Click(object sender, EventArgs e)
        {
            int goalNum = int.Parse(GoalNumber.Text);
            int optNum = int.Parse(optNumber.Text);
            int optChoices = (int)OptRewardUpDown.Value;
            int finNum = int.Parse(finNumber.Text);

            foreach (KeyValuePair<int, string> kpv in goaltext_dictionary)
            {
                questgoalsformatted_dictionary.Remove(kpv.Key);
                questgoalsformatted_dictionary.Add(kpv.Key, goaltext_dictionary[kpv.Key] + ";" + goalstepno_dictionary[kpv.Key]);
            }

            // this needs to always be done, incase user edits a step that has already been added 
            //if (!goaltype_dictionary.ContainsKey(goalNum)) //Adds step data to the dictionary on last step if the forward/back button has not been pressed yet

            goaltype_dictionary.Remove(goalNum);
            goaltype_dictionary.Add(goalNum, _GoalType.Text);
            goalstepno_dictionary.Remove(goalNum);
            goalstepno_dictionary.Add(goalNum, GoalStepNo.Text);

            // safety to prevent bad quests
            if (!CheckForInteractFinishGoal())
            {
                MessageBox.Show("The last step MUST be of goaltype 'InteractFinish'. Check your quests goals/steps.", @"Quest Incomplete!");
                return;
            }

            goaltext_dictionary.Remove(goalNum);
            goaltext_dictionary.Add(goalNum, _QuestGoals.Text);
            questgoalsformatted_dictionary.Remove(goalNum);
            questgoalsformatted_dictionary.Add(goalNum, _QuestGoals.Text + ";" + GoalStepNo.Text);
            goaltargetname_dictionary.Remove(goalNum);
            goaltargetname_dictionary.Add(goalNum, _GoalTargetName.Text);
            colitem_dictionary.Remove(goalNum);
            colitem_dictionary.Add(goalNum, _CollectItemTempate.Text);
            goalrepeatno_dictionary.Remove(goalNum);
            goalrepeatno_dictionary.Add(goalNum, _GoalRepeatNo.Text);
            xloc_dictionary.Remove(goalNum);
            xloc_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_XOffset.Text) ? "0" : _XOffset.Text);
            yloc_dictionary.Remove(goalNum);
            yloc_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_YOffset.Text) ? "0" : _YOffset.Text);
            zoneid_dictionary.Remove(goalNum);
            zoneid_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_ZoneID.Text) ? "0" : _ZoneID.Text);
            goaltargettext_dictionary.Remove(goalNum);
            goaltargettext_dictionary.Add(goalNum, _GoalTargetText.Text);

            opt_dictionary.Remove(optNum); // do this incase it was edited without pressing forward/back
            if (!string.IsNullOrWhiteSpace(_OptionalReward.Text)) //!opt_dictionary.ContainsKey(optNum) && 
            {
                opt_dictionary.Add(optNum, _OptionalReward.Text);
            }
            fin_dictionary.Remove(finNum); // do this incase it was edited without pressing forward/back
            if (!string.IsNullOrWhiteSpace(_FinalReward.Text)) //!fin_dictionary.ContainsKey(finNum) && 
            {
                fin_dictionary.Add(finNum, _FinalReward.Text);
            }

            try
            {
                stepCount = int.Parse(goalstepno_dictionary[goalstepno_dictionary.Count]);
                goalType = String.Join("|", Array.ConvertAll(goaltype_dictionary.Values.ToArray(), i => i.ToString()));
                allowedClasses = String.Join("|", listClasses.SelectedItems.Cast<object>().Select(i => i.ToString()));
                questGoals = String.Join("|", Array.ConvertAll(questgoalsformatted_dictionary.Values.ToArray(), i => i.ToString()));
                goalTargetName = String.Join("|", Array.ConvertAll(goaltargetname_dictionary.Values.ToArray(), i => i.ToString()));
                goalRepeatNo = String.Join("|", Array.ConvertAll(goalrepeatno_dictionary.Values.ToArray(), i => i.ToString()));
                goalStepNo = String.Join("|", Array.ConvertAll(goalstepno_dictionary.Values.ToArray(), i => i.ToString()));
                goalTargetText = String.Join("|", Array.ConvertAll(goaltargettext_dictionary.Values.ToArray(), i => i.ToString()));
                collectItemTemplate = String.Join("|", Array.ConvertAll(colitem_dictionary.Values.ToArray(), i => i.ToString()));
                //_AdvanceText = String.Join("|", Array.ConvertAll(advtext_dictionary.Values.ToArray(), i => i.ToString())); // removed this until coded into server
                xloc = String.Join("|", Array.ConvertAll(xloc_dictionary.Values.ToArray(), i => i.ToString()));
                yloc = String.Join("|", Array.ConvertAll(yloc_dictionary.Values.ToArray(), i => i.ToString()));
                zoneId = String.Join("|", Array.ConvertAll(zoneid_dictionary.Values.ToArray(), i => i.ToString()));
                finishNPC = goaltargetname_dictionary[goaltargetname_dictionary.Count];

                if (opt_dictionary.Count > 0) // if option rewards has at least 1 entry, we will append the optional choices digit to the string
                {
                    optionalRewardItemTemplates = optChoices.ToString() + String.Join("|", Array.ConvertAll(opt_dictionary.Values.ToArray(), i => i.ToString()));
                }
                else // no entries? blank string
                {
                    optionalRewardItemTemplates = "";
                }
                finalRewardItemTemplates = String.Join("|", Array.ConvertAll(fin_dictionary.Values.ToArray(), i => i.ToString()));

                StringBuilder gtype = new StringBuilder(goalType);
                gtype.Replace("Search", "2");
                gtype.Replace("Kill", "3");
                gtype.Replace("InteractFinish", "5");
                gtype.Replace("InteractWhisper", "6");
                gtype.Replace("InteractDeliver", "7");
                gtype.Replace("Interact", "4");
                goalType = gtype.ToString();

                StringBuilder allcl = new StringBuilder(allowedClasses);
                allcl.Replace("All", ""); // added all incase one is selected, you cannot deselect
                allcl.Replace("Armsman", "2");
                allcl.Replace("Cabalist", "13");
                allcl.Replace("Cleric", "6");
                allcl.Replace("Friar", "10");
                allcl.Replace("Heretic", "33");
                allcl.Replace("Infiltrator", "9");
                allcl.Replace("Mercenary", "11");
                allcl.Replace("Minstrel", "4");
                allcl.Replace("Necromancer", "12");
                allcl.Replace("Paladin", "1");
                allcl.Replace("Reaver", "19");
                allcl.Replace("Scout", "3");
                allcl.Replace("Sorcerer", "8");
                allcl.Replace("Theurgist", "5");
                allcl.Replace("Wizard", "7");
                allcl.Replace("MaulerAlb", "60");
                allcl.Replace("Berserker", "31");
                allcl.Replace("Bonedancer", "30");
                allcl.Replace("Healer", "26");
                allcl.Replace("Hunter", "25");
                allcl.Replace("Runemaster", "29");
                allcl.Replace("Savage", "32");
                allcl.Replace("Shadowblade", "23");
                allcl.Replace("Shaman", "28");
                allcl.Replace("Skald", "24");
                allcl.Replace("Spiritmaster", "27");
                allcl.Replace("Thane", "21");
                allcl.Replace("Valkyrie", "34");
                allcl.Replace("Warlock", "59");
                allcl.Replace("Warrior", "22");
                allcl.Replace("MaulerMid", "61");
                allcl.Replace("Animist", "55");
                allcl.Replace("Bainshee", "39");
                allcl.Replace("Bard", "48");
                allcl.Replace("Blademaster", "43");
                allcl.Replace("Champion", "45");
                allcl.Replace("Druid", "47");
                allcl.Replace("Eldritch", "40");
                allcl.Replace("Enchanter", "41");
                allcl.Replace("Hero", "44");
                allcl.Replace("Mentalist", "42");
                allcl.Replace("Nightshade", "49");
                allcl.Replace("Ranger", "50");
                allcl.Replace("Valewalker", "56");
                allcl.Replace("Vampiir", "58");
                allcl.Replace("Warden", "46");
                allcl.Replace("MaulerHib", "62");
                allcl.Replace("Acolyte", "16");
                allcl.Replace("AlbionRogue", "17");
                allcl.Replace("Disciple", "20");
                allcl.Replace("Elementalist", "15");
                allcl.Replace("Fighter", "14");
                allcl.Replace("Forester", "57");
                allcl.Replace("Guardian", "52");
                allcl.Replace("Mage", "18");
                allcl.Replace("Magician", "51");
                allcl.Replace("MidgardRogue", "38");
                allcl.Replace("Mystic", "36");
                allcl.Replace("Naturalist", "53");
                allcl.Replace("Seer", "37");
                allcl.Replace("Stalker", "54");
                allcl.Replace("Viking", "35");
                allowedClasses = allcl.ToString();
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error converting data. Quest will not be completely saved!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DBDQRewardQ q = new DBDQRewardQ();
            int.TryParse(_ID.Text, out int questID);
            q.ID = questID;
            q.QuestName = string.IsNullOrWhiteSpace(_QuestName.Text) ? "new quest" : _QuestName.Text;
            q.StartNPC = string.IsNullOrWhiteSpace(_StartNPC.Text) ? "undefined" : _StartNPC.Text;
            q.StartRegionID = ushort.Parse(string.IsNullOrWhiteSpace(_StartRegionID.Text) ? "0" : _StartRegionID.Text);
            q.StoryText = string.IsNullOrWhiteSpace(_StoryText.Text) ? "" : _StoryText.Text;
            q.Summary = _Summary.Text;
            q.AcceptText = _AcceptText.Text;
            q.QuestGoals = questGoals;
            q.GoalType = goalType;
            q.GoalRepeatNo = goalRepeatNo;
            q.GoalTargetName = goalTargetName;
            q.GoalTargetText = goalTargetText;
            q.StepCount = stepCount;
            q.FinishNPC = finishNPC;
            q.AdvanceText = "";// not supported yet
            q.CollectItemTemplate = collectItemTemplate;
            q.MaxCount = byte.Parse(string.IsNullOrWhiteSpace(_MaxCount.Text) ? "1" : _MaxCount.Text);
            q.MinLevel = byte.Parse(string.IsNullOrWhiteSpace(_MinLevel.Text) ? "1" : _MinLevel.Text);
            q.MaxLevel = byte.Parse(string.IsNullOrWhiteSpace(_MaxLevel.Text) ? "50" : _MaxLevel.Text);
            q.RewardMoney = int.Parse(string.IsNullOrWhiteSpace(_RewardMoney.Text) ? "0" : _RewardMoney.Text);
            q.RewardXP = int.Parse(string.IsNullOrWhiteSpace(_RewardXP.Text) ? "0" : _RewardXP.Text);
            q.RewardCLXP = int.Parse(string.IsNullOrWhiteSpace(_RewardCLXP.Text) ? "0" : _RewardCLXP.Text);
            q.RewardRP = int.Parse(string.IsNullOrWhiteSpace(_RewardRP.Text) ? "0" : _RewardRP.Text);
            q.RewardBP = int.Parse(string.IsNullOrWhiteSpace(_RewardBP.Text) ? "0" : _RewardBP.Text);
            q.OptionalRewardItemTemplates = optionalRewardItemTemplates;
            q.FinalRewardItemTemplates = finalRewardItemTemplates;
            q.FinishText = _FinishText.Text;
            q.QuestDependency = _QuestDependency.Text; //might need to serialize....if quest has multiple dependencies
            q.AllowedClasses = allowedClasses;
            q.ClassType = _ClassType.Text;
            q.XOffset = xloc;
            q.YOffset = yloc;
            q.ZoneID = zoneId;

            try
            { // This is probabaly a bad way to do this, but i can't get the quest to save onto the same ID 
                if (!LoadedQuest)
                {
                    DatabaseManager.Database.AddObject(q);
                    LoadedQuest = true; // quest has been added to DB so can now be saved on its existing ID
                    _ID.Text = q.ID.ToString(); // set the generated ID to the ID text field so next save it has a value
                }
                else
                {
                    DatabaseManager.Database.SaveObject(q);
                }
                MessageBox.Show("Quest successfully saved!", "", MessageBoxButtons.OK);
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error saving data!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ensure the InteractFinish goaltype is the last goal set in the quest.
        private bool CheckForInteractFinishGoal()
        {
            return goaltype_dictionary[goaltype_dictionary.Count] == "InteractFinish";
        }

        // Clear the form fields, but not delete from the database
        private void questDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(@"This will clear the form. Are you sure you want to continue? (If this quest is in the database it will NOT be deleted)",
                @"Confirm Delete!!",
                MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            Clear();
            SetFieldDefaults();
        }

        // clear the dictionaries 
        private void Clear()
        {
            _quest = null;
            LoadedQuest = false;
            BindingService.ClearData(this);
            opt_dictionary.Clear();
            fin_dictionary.Clear();
            //advtext_dictionary.Clear();
            colitem_dictionary.Clear();
            goalrepeatno_dictionary.Clear();
            goaltargetname_dictionary.Clear();
            goaltargettext_dictionary.Clear();
            goaltext_dictionary.Clear();
            questgoalsformatted_dictionary.Clear();
            goaltype_dictionary.Clear();
            goalstepno_dictionary.Clear();
            xloc_dictionary.Clear();
            yloc_dictionary.Clear();
            zoneid_dictionary.Clear();
            listClasses.ClearSelected();
        }

        // set field defaults when the form is cleared
        private void SetFieldDefaults()
        {
            GoalNumber.Text = "1";
            optNumber.Text = "1";
            finNumber.Text = "1";
            OptRewardUpDown.Value = 1;
            _RewardMoney.Text = "0";
            _RewardXP.Text = "0";
            _RewardCLXP.Text = "0";
            _RewardRP.Text = "0";
            _RewardBP.Text = "0";
            _XOffset.Text = "0";
            _YOffset.Text = "0";
            _ZoneID.Text = "0";
        }

        // Keypress event args

        // prevent typing custom text into the goaltype dropdown box
        private void _GoalType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void MinLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // block ID from being changed. Auto generated
        }

        private void MaxLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void MaxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void StartRegionID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardXp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardCLXp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardRp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void RewardBp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GoalStepNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GoalRepeatNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_GoalType.SelectedIndex == 2) // can only change this number from 1 on a kill goaltype
            {
                // Verify that the pressed key isn't CTRL or any non-numeric digit
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void GoalXloc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GoalYloc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GoalZoneId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var search = new ItemSearchForm();

            search.SelectClicked += (o, args) =>
            {
                if (!(o is ItemTemplate item))
                {
                    return;
                }
                LoadItem(item.Id_nb);
            };


            search.ShowDialog(this);
        }

        private void LoadItem(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return;
            }

            ItemTemplateBox.Text = itemId;
        }

        private void optSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemTemplateBox.Text))
            {
                return;
            }
            else
            {
                _OptionalReward.Text = ItemTemplateBox.Text;
            }
        }

        private void collectSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemTemplateBox.Text))
            {
                return;
            }
            else
            {
                _CollectItemTempate.Text = ItemTemplateBox.Text;
            }
        }

        private void finSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemTemplateBox.Text))
            {
                return;
            }
            else
            {
                _FinalReward.Text = ItemTemplateBox.Text;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (OptRewardUpDown.Value > 8)
            {
                OptRewardUpDown.Value = 8;
                return;
            }
            else if (OptRewardUpDown.Value < 1)
            {
                OptRewardUpDown.Value = 1;
            }
        }

        private void SearchStartNPCBtn_Click(object sender, EventArgs e)
        {
            var mobsearch = new MobSearch();

            mobsearch.SelectNpcClicked += (o, args) => { LoadStartNPC(((Mob)o).Name, ((Mob)o).Region); };

            mobsearch.ShowDialog(this);
        }

        private void LoadStartNPC(string mobName, ushort mobRegion)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }

            _StartNPC.Text = mobName;
            _StartRegionID.Text = mobRegion.ToString();
        }

        private void _GoalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_GoalType.SelectedIndex != 2) // kill goaltype
            {
                _GoalRepeatNo.Text = "1"; // all other goaltypes have a repeat value of 1
            }
            if (_GoalType.SelectedIndex == 4) // interactFinish
            {
                if (goalstepno_dictionary.Count > 0) // at least one value  
                {
                    int.TryParse(goalstepno_dictionary.Values.Max(), out int maxValue); // find the highest stepNo
                    //var maxValue = goalstepno_dictionary.Values.Max();
                    GoalStepNo.Text = (maxValue + 1).ToString(); // and add +1 for the interactFinish stepNo
                }
                else // no previous steps
                {
                    GoalStepNo.Text = "1"; // interactFinish stepNo is 1
                }
            }
        }

        private void QuestDependencySearchBtn_Click(object sender, EventArgs e)
        {
            var search = new RewardQuestSearchForm();

            search.SelectClicked += (o, args) =>
            {
                if (!(o is DBDQRewardQ item))
                {
                    return;
                }

                _QuestDependency.Text = item.QuestName;
            };

            search.ShowDialog(this);
        }

        private void npcSearch_Click(object sender, EventArgs e)
        {
            var mobsearch = new MobSearch();

            mobsearch.SelectNpcClicked += (o, args) => { LoadGoalTargetNPC(((Mob)o).Name); };

            mobsearch.ShowDialog(this);
        }

        private void LoadGoalTargetNPC(string mobName)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }

            _GoalTargetName.Text = mobName;
        }

        //Step data forward
        private void nextGoal_Click(object sender, EventArgs e)
        {

            int goalNumber = int.Parse(GoalNumber.Text);

            if (_GoalType.Text == "" || _GoalRepeatNo.Text == "" || _GoalTargetName.Text == "" || GoalStepNo.Text == "" || _QuestGoals.Text == "")
            {
                MessageBox.Show("You cannot proceed until required fields are met!\n" +
                    "GoalType, Target Name, Step Number, Journal Text are all manditory fields!");
                return;
            }

            //GoalType.Text
            if (!goaltype_dictionary.ContainsKey(goalNumber))
            {
                goaltype_dictionary.Add(goalNumber, _GoalType.Text);
            }
            else
            {
                goaltype_dictionary.Remove(goalNumber);
                goaltype_dictionary.Add(goalNumber, _GoalType.Text);
            }
            _GoalType.Text = "";

            //GoalText.Text
            if (!goaltext_dictionary.ContainsKey(goalNumber))
            {
                goaltext_dictionary.Add(goalNumber, _QuestGoals.Text);
                questgoalsformatted_dictionary.Add(goalNumber, _QuestGoals.Text + ";" + GoalStepNo.Text); // format is like "kill 3 bandits;1
            }
            else
            {
                goaltext_dictionary.Remove(goalNumber);
                goaltext_dictionary.Add(goalNumber, _QuestGoals.Text);
                questgoalsformatted_dictionary.Remove(goalNumber);
                questgoalsformatted_dictionary.Add(goalNumber, _QuestGoals.Text + ";" + GoalStepNo.Text);
            }
            _QuestGoals.Text = "";

            //GoalTarget.Text
            if (!goaltargetname_dictionary.ContainsKey(goalNumber))
            {
                goaltargetname_dictionary.Add(goalNumber, _GoalTargetName.Text);
            }
            else
            {
                goaltargetname_dictionary.Remove(goalNumber);
                goaltargetname_dictionary.Add(goalNumber, _GoalTargetName.Text);
            }
            _GoalTargetName.Text = "";

            //CollectItem.Text
            if (!colitem_dictionary.ContainsKey(goalNumber))
            {
                colitem_dictionary.Add(goalNumber, _CollectItemTempate.Text);
            }
            else
            {
                colitem_dictionary.Remove(goalNumber);
                colitem_dictionary.Add(goalNumber, _CollectItemTempate.Text);
            }
            _CollectItemTempate.Text = "";

            //GoalStepNo.Text
            if (!goalstepno_dictionary.ContainsKey(goalNumber))
            {
                goalstepno_dictionary.Add(goalNumber, GoalStepNo.Text);
            }
            else
            {
                goalstepno_dictionary.Remove(goalNumber);
                goalstepno_dictionary.Add(goalNumber, GoalStepNo.Text);
            }
            GoalStepNo.Text = "";

            //GoalRepeatNo.Text
            if (!goalrepeatno_dictionary.ContainsKey(goalNumber))
            {
                goalrepeatno_dictionary.Add(goalNumber, _GoalRepeatNo.Text);
            }
            else
            {
                goalrepeatno_dictionary.Remove(goalNumber);
                goalrepeatno_dictionary.Add(goalNumber, _GoalRepeatNo.Text);
            }
            _GoalRepeatNo.Text = "1";

            //GoalXloc.Text
            if (!xloc_dictionary.ContainsKey(goalNumber))
            {
                xloc_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_XOffset.Text) ? "0" : _XOffset.Text);
            }
            else
            {
                xloc_dictionary.Remove(goalNumber);
                xloc_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_XOffset.Text) ? "0" : _XOffset.Text);
            }
            _XOffset.Text = "0";

            //GoalYloc.Text
            if (!yloc_dictionary.ContainsKey(goalNumber))
            {
                yloc_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_YOffset.Text) ? "0" : _YOffset.Text);
            }
            else
            {
                yloc_dictionary.Remove(goalNumber);
                yloc_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_YOffset.Text) ? "0" : _YOffset.Text);
            }
            _YOffset.Text = "0";

            //GoalZoneId.Text
            if (!zoneid_dictionary.ContainsKey(goalNumber))
            {
                zoneid_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_ZoneID.Text) ? "0" : _ZoneID.Text);
            }
            else
            {
                zoneid_dictionary.Remove(goalNumber);
                zoneid_dictionary.Add(goalNumber, string.IsNullOrWhiteSpace(_ZoneID.Text) ? "0" : _ZoneID.Text);
            }
            _ZoneID.Text = "0";

            //GoalTargetText.Text
            if (!goaltargettext_dictionary.ContainsKey(goalNumber))
            {
                goaltargettext_dictionary.Add(goalNumber, _GoalTargetText.Text);
            }
            else
            {
                goaltargettext_dictionary.Remove(goalNumber);
                goaltargettext_dictionary.Add(goalNumber, _GoalTargetText.Text);
            }
            _GoalTargetText.Text = "";

            GoalNumber.Text = (goalNumber + 1).ToString(); //increment label

            //Step forward check next step

            int goalNext = int.Parse(GoalNumber.Text);
            string goalvalue;

            //Previous step data check
            if (goaltype_dictionary.ContainsKey(goalNext))
            {
                goaltype_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalType.Text = goalvalue;
            }
            else _GoalType.Text = "";

            if (goaltext_dictionary.ContainsKey(goalNext))
            {
                goaltext_dictionary.TryGetValue(goalNext, out goalvalue);
                _QuestGoals.Text = goalvalue;
            }
            else _QuestGoals.Text = "";

            if (goaltargetname_dictionary.ContainsKey(goalNext))
            {
                goaltargetname_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalTargetName.Text = goalvalue;
            }
            else _GoalTargetName.Text = "";

            if (colitem_dictionary.ContainsKey(goalNext))
            {
                colitem_dictionary.TryGetValue(goalNext, out goalvalue);
                _CollectItemTempate.Text = goalvalue;
            }
            else _CollectItemTempate.Text = "";

            if (goalstepno_dictionary.ContainsKey(goalNext))
            {
                goalstepno_dictionary.TryGetValue(goalNext, out goalvalue);
                GoalStepNo.Text = goalvalue;
            }
            else GoalStepNo.Text = "";

            if (goalrepeatno_dictionary.ContainsKey(goalNext))
            {
                goalrepeatno_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalRepeatNo.Text = goalvalue;
            }
            else _GoalRepeatNo.Text = "";

            if (xloc_dictionary.ContainsKey(goalNext))
            {
                xloc_dictionary.TryGetValue(goalNext, out goalvalue);
                _XOffset.Text = goalvalue;
            }
            else _XOffset.Text = "0";

            if (yloc_dictionary.ContainsKey(goalNext))
            {
                yloc_dictionary.TryGetValue(goalNext, out goalvalue);
                _YOffset.Text = goalvalue;
            }
            else _YOffset.Text = "0";

            if (zoneid_dictionary.ContainsKey(goalNext))
            {
                zoneid_dictionary.TryGetValue(goalNext, out goalvalue);
                _ZoneID.Text = goalvalue;
            }
            else _ZoneID.Text = "0";

            if (goaltargettext_dictionary.ContainsKey(goalNext))
            {
                goaltargettext_dictionary.TryGetValue(goalNext, out goalvalue);
                _GoalTargetText.Text = goalvalue;
            }
            else _GoalTargetText.Text = "";
        }

        //Step data back
        private void previousGoal_Click(object sender, EventArgs e)
        {
            int goalNum = int.Parse(GoalNumber.Text);

            if (goalNum == 1) //return if already at goal 1, there is no goal 0
            {
                return;
            }

            //remove step altogether if mandatory fields are not entered
            if (_GoalRepeatNo.Text == "" || _GoalType.Text == "")
            {
                goaltype_dictionary.Remove(goalNum);
                goaltext_dictionary.Remove(goalNum);
                goaltargetname_dictionary.Remove(goalNum);
                colitem_dictionary.Remove(goalNum);
                goalstepno_dictionary.Remove(goalNum);
                goalrepeatno_dictionary.Remove(goalNum);
                xloc_dictionary.Remove(goalNum);
                yloc_dictionary.Remove(goalNum);
                zoneid_dictionary.Remove(goalNum);
                goaltargettext_dictionary.Remove(goalNum);
            }
            else
            //Needed to commit data to m_dictionary when the back button is clicked
            {
                goaltype_dictionary.Remove(goalNum);
                goaltype_dictionary.Add(goalNum, _GoalType.Text);
                goaltext_dictionary.Remove(goalNum);
                goaltext_dictionary.Add(goalNum, _QuestGoals.Text);
                questgoalsformatted_dictionary.Remove(goalNum);
                questgoalsformatted_dictionary.Add(goalNum, _QuestGoals.Text + ";" + GoalStepNo.Text); // format is like "kill 3 bandits;1
                goaltargetname_dictionary.Remove(goalNum);
                goaltargetname_dictionary.Add(goalNum, _GoalTargetName.Text);
                colitem_dictionary.Remove(goalNum);
                colitem_dictionary.Add(goalNum, _CollectItemTempate.Text);
                goalstepno_dictionary.Remove(goalNum);
                goalstepno_dictionary.Add(goalNum, GoalStepNo.Text);
                goalrepeatno_dictionary.Remove(goalNum);
                goalrepeatno_dictionary.Add(goalNum, _GoalRepeatNo.Text);
                xloc_dictionary.Remove(goalNum);

                xloc_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_XOffset.Text) ? "0" : _XOffset.Text);
                yloc_dictionary.Remove(goalNum);
                yloc_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_YOffset.Text) ? "0" : _YOffset.Text);
                zoneid_dictionary.Remove(goalNum);
                zoneid_dictionary.Add(goalNum, string.IsNullOrWhiteSpace(_ZoneID.Text) ? "0" : _ZoneID.Text);
                goaltargettext_dictionary.Remove(goalNum);
                goaltargettext_dictionary.Add(goalNum, _GoalTargetText.Text);
            }

            GoalNumber.Text = (goalNum - 1).ToString();
            string goalvalue;
            goalNum--;

            //Previous step data check
            if (goaltype_dictionary.ContainsKey(goalNum))
            {
                goaltype_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalType.Text = goalvalue;
            }
            else _GoalType.Text = "";

            if (goaltext_dictionary.ContainsKey(goalNum))
            {
                goaltext_dictionary.TryGetValue(goalNum, out goalvalue);
                _QuestGoals.Text = goalvalue;
            }
            else _QuestGoals.Text = "";

            if (goaltargetname_dictionary.ContainsKey(goalNum))
            {
                goaltargetname_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalTargetName.Text = goalvalue;
            }
            else _GoalTargetName.Text = "";

            if (colitem_dictionary.ContainsKey(goalNum))
            {
                colitem_dictionary.TryGetValue(goalNum, out goalvalue);
                _CollectItemTempate.Text = goalvalue;
            }
            else _CollectItemTempate.Text = "";

            if (goalstepno_dictionary.ContainsKey(goalNum))
            {
                goalstepno_dictionary.TryGetValue(goalNum, out goalvalue);
                GoalStepNo.Text = goalvalue;
            }
            else GoalStepNo.Text = "";

            if (goalrepeatno_dictionary.ContainsKey(goalNum))
            {
                goalrepeatno_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalRepeatNo.Text = goalvalue;
            }
            else _GoalRepeatNo.Text = "";

            if (xloc_dictionary.ContainsKey(goalNum))
            {
                xloc_dictionary.TryGetValue(goalNum, out goalvalue);
                _XOffset.Text = goalvalue;
            }
            else _XOffset.Text = "0";

            if (yloc_dictionary.ContainsKey(goalNum))
            {
                yloc_dictionary.TryGetValue(goalNum, out goalvalue);
                _YOffset.Text = goalvalue;
            }
            else _YOffset.Text = "0";

            if (zoneid_dictionary.ContainsKey(goalNum))
            {
                zoneid_dictionary.TryGetValue(goalNum, out goalvalue);
                _ZoneID.Text = goalvalue;
            }
            else _ZoneID.Text = "0";

            if (goaltargettext_dictionary.ContainsKey(goalNum))
            {
                goaltargettext_dictionary.TryGetValue(goalNum, out goalvalue);
                _GoalTargetText.Text = goalvalue;
            }
            else _GoalTargetText.Text = "";
        }

        //Final Reward forward dictionary
        private void finrewardForward_Click(object sender, EventArgs e)
        {
            if (_FinalReward.Text == "")
            {
                MessageBox.Show("You can't add nothing as a reward!", "PEBKAC");
                return;
            }

            int finNum = int.Parse(finNumber.Text);

            if (!fin_dictionary.ContainsKey(finNum)) //If the reward data is not in the dictionary...check for step 1, then add
            {
                fin_dictionary.Add(finNum, _FinalReward.Text);
            }
            else //If the reward data is in the dictionary...check if the values match and add if they don't
            {
                fin_dictionary.TryGetValue(finNum, out string finvalue);

                if (finvalue != _FinalReward.Text)
                {
                    fin_dictionary.Remove(finNum);
                    fin_dictionary.Add(finNum, _FinalReward.Text);
                }
            }

            finNumber.Text = (finNum + 1).ToString();

            //Check if next step contains data
            int finNext = int.Parse(finNumber.Text);
            if (fin_dictionary.ContainsKey(finNext))
            {
                fin_dictionary.TryGetValue(finNext, out string finvalue);
                _FinalReward.Text = finvalue;
            }
            else _FinalReward.Text = "";

        }

        //Final Reward back Dictionary
        private void finrewardBack_Click(object sender, EventArgs e)
        {
            int finNum = int.Parse(finNumber.Text);

            if (finNum == 1) //return if already at step 1, there ain't no step 0
            {
                return;
            }

            if (_FinalReward.Text != "") //Add data on back click
            {
                fin_dictionary.TryGetValue(finNum, out string finvalue);
                if (finvalue != _FinalReward.Text) //check if data is different from dictionary data
                {
                    fin_dictionary.Remove(finNum);
                    fin_dictionary.Add(finNum, _FinalReward.Text);
                }
            }
            else //There are no breaks in reward data, so a blank box cannot exist between populated blocks
            {
                if (fin_dictionary.ContainsKey(finNum + 1))
                {
                    MessageBox.Show("You must remove the items listed after the current item to remove this one!", "Error");
                    return;
                }
                else
                {
                    fin_dictionary.Remove(finNum);
                }
            }

            //Pull previous step data
            int fincheck = int.Parse(finNumber.Text);
            fincheck--;
            fin_dictionary.TryGetValue(fincheck, out string finback);
            _FinalReward.Text = finback;
            finNumber.Text = (finNum - 1).ToString(); //finally, decrement fin label
        }

        //Optional Reward forward dictionary
        private void optrewardForward_Click(object sender, EventArgs e)
        {
            if (_OptionalReward.Text == "")
            {
                MessageBox.Show("You can't add nothing as a reward!", "PEBKAC");
                return;
            }

            int optNum = int.Parse(optNumber.Text);

            if (optNum == 8)
            {
                opt_dictionary.Remove(optNum);
                opt_dictionary.Add(optNum, _OptionalReward.Text);
                MessageBox.Show("Last optional item added", "8 Optional Rewards Max");
                return;
            }

            if (!opt_dictionary.ContainsKey(optNum)) //If the reward data is not in the dictionary...check for step 1, then add
            {
                opt_dictionary.Add(optNum, _OptionalReward.Text);
            }
            else //If the reward data is in the dictionary...check if the values match and add if they don't
            {
                opt_dictionary.TryGetValue(optNum, out string optvalue);

                if (optvalue != _OptionalReward.Text)
                {
                    opt_dictionary.Remove(optNum);
                    opt_dictionary.Add(optNum, _OptionalReward.Text);
                }
            }

            optNumber.Text = (optNum + 1).ToString();

            //Check if next step contains data
            int optNext = int.Parse(optNumber.Text);
            if (opt_dictionary.ContainsKey(optNext))
            {
                opt_dictionary.TryGetValue(optNext, out string optvalue);
                _OptionalReward.Text = optvalue;
            }
            else _OptionalReward.Text = "";

        }

        //Optional Reward back dictionary
        private void optrewardBack_Click(object sender, EventArgs e)
        {
            int optNum = int.Parse(optNumber.Text);

            if (optNum == 1) //return if already at step 1, there ain't no step 0
            {
                return;
            }

            if (_OptionalReward.Text != "") //Add data on back click
            {
                opt_dictionary.TryGetValue(optNum, out string optvalue);
                if (optvalue != _OptionalReward.Text)
                {
                    opt_dictionary.Remove(optNum);
                    opt_dictionary.Add(optNum, _OptionalReward.Text);
                }
            }
            else
            {
                if (opt_dictionary.ContainsKey(optNum + 1))
                {
                    MessageBox.Show("You must remove the items listed after the current item to remove this one!", "Error");
                    return;
                }
                else
                {
                    opt_dictionary.Remove(optNum);
                }
            }

            //Pull previous step data
            int optcheck = int.Parse(optNumber.Text);
            optcheck--;
            opt_dictionary.TryGetValue(optcheck, out string optback);
            _OptionalReward.Text = optback;

            optNumber.Text = (optNum - 1).ToString(); //finally, decrement opt label
        }

        // Hover over tooltips

        private void label8_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The X co-ordinate for this quest target. Used to display red dot on players map. Use /loc command ingame for the number.", labelXloc, 10000);
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelXloc);
        }

        private void label11_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The Y co-ordinate for this quest target. Used to display red dot on players map. Use /loc command ingame for the number.", labelYloc, 10000);
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelYloc);
        }

        private void label14_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The Y co-ordinate for this quest target. Used to display red dot on players map. Use /loc command ingame for the number.", labelZoneId, 10000);
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelZoneId);
        }

        private void label10_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The step number when this quest goal is given. Multiple goals can have the same step number so they are given to the player at the same time.\n" +
                "The 'InteractFinish' goaltype must be the last goal and the last step number of EVERY quest for it to work correctly.", labelStepNo, 15000);
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelStepNo);
        }

        private void label24_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("How many times you must kill the target to complete the goal. Only used on 'kill' goaltype.", label24, 10000);
        }

        private void label24_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label24);
        }

        private void label19_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("A text window displayed when the player interacts with the goal target. Interact, InteractDeliver goaltype only.", labelGoalInteract, 10000);
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelGoalInteract);
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The text thats displayed in the players journal for this quest goal.", labelGoalText, 10000);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelGoalText);
        }

        private void label16_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("GoalTypes:\n" +
                "Search - /search an area in game to complete. \n" +
                "Kill - kill the goal target to complete. Can increase the repeatNo for this type. \n" +
                "Interact - Interact with the goal target to complete. \n" +
                "InteractDeliver - Used to deliver the 'collect item'. Interact with the goal target to complete. \n" +
                "InteractWhisper - Player must whisper the advancetext to the target to complete. \n" +
                "interactFinish - Interact with the goal target to finish the quest. This required for the last step of EVERY quest!", labelGoalType, 20000);

        }

        private void label16_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelGoalType);
        }

        private void label21_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("A dummy item that will show its model icon in the journal upon completing the goal.\n" +
                "When used with 'InteractDeliver' goaltype, the model icon will be displated immediately after receiving the goal.", labelGoalText, 15000);
        }

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelCollectItem);
        }

        private void label13_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Minimum level quest is offered to player.", label13, 10000);
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label13);
        }

        private void label35_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Maximum level quest is offered to player.", label35, 10000);
        }

        private void label35_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label35);
        }

        private void label32_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Maximum times a player can repeat the quest.", label32, 10000);
        }

        private void label32_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label32);
        }

        private void label7_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("List of classes quest is offered to. Leave blank for all or shift+click to select multiple.", label7, 10000);
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label7);
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Auto-generated ID. You can change this in the database after quest is saved or use for reference to use 'Load' button.", label1, 10000);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label1);
        }

        private void label18_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("NPC that offers the quest.", label18, 10000);
        }

        private void label18_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label18);
        }

        private void label17_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Region of the quest Start NPC.", label17, 10000);
        }

        private void label17_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label17);
        }

        private void QuestDependencySearchBtn_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Name of quest a player must complete before this quest is offered to them.", QuestDependencySearchBtn, 10000);
        }

        private void QuestDependencySearchBtn_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(QuestDependencySearchBtn);
        }

        private void classTypeLabel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("Class name of custom script thats called for this quest.", classTypeLabel, 10000);
        }

        private void classTypeLabel_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(classTypeLabel);
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The quest story presented to the player when offered the quest.", label5, 10000);
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label5);
        }

        private void summaryLabel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The summary of the quest displayed in the quest journal.", summaryLabel, 10000);
        }

        private void summaryLabel_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(summaryLabel);
        }

        private void finishTextLabel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The text displayed to the player upon completing the quest and being offered the rewards.", finishTextLabel, 10000);
        }

        private void finishTextLabel_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(finishTextLabel);
        }

        private void label34_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The items a player recieves upon completing the quest.", label34, 10000);
        }

        private void label34_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label34);
        }

        private void GoalNPCSearch_Click(object sender, EventArgs e)
        {
            var mobsearch = new MobSearch();

            mobsearch.SelectNpcClicked += (o, args) => { LoadGoalTargetNPC(((Mob)o).Name); };

            mobsearch.ShowDialog(this);
        }

        private void GoalObjectSearch_Click(object sender, EventArgs e)
        {
            var objectsearch = new ObjectSearch();

            objectsearch.SelectNpcClicked += (o, args) => { LoadGoalTargetNPC(((WorldObject)o).Name); };

            objectsearch.ShowDialog(this);
        }

        private void label33_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The optional items a player has a choice from upon completing the quest.\n" +
                "You can set multiple options in here and set the number the player can choose from the counter.", label33, 10000);
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label33);
        }

        private void label22_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("How many optional rewards the player can choose from.", label22, 10000);
        }

        private void label22_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(label22);
        }

        private void label3_MouseHover_1(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 500;
            toolTip1.Show("The current goal. This number is not important and is not the step number.", label3, 10000);
        }

        private void label3_MouseLeave_1(object sender, EventArgs e)
        {
            toolTip1.Hide(label3);
        }

        // Add classes and associated ID to a dictionary for later use
        private void PopulateClassDictionary()
        {
            allClasses.Add(2, "Armsman");
            allClasses.Add(13, "Cabalist");
            allClasses.Add(6, "Cleric");
            allClasses.Add(10, "Friar");
            allClasses.Add(33, "Heretic");
            allClasses.Add(9, "Infiltrator");
            allClasses.Add(11, "Mercenary");
            allClasses.Add(4, "Minstrel");
            allClasses.Add(12, "Necromancer");
            allClasses.Add(1, "Paladin");
            allClasses.Add(19, "Reaver");
            allClasses.Add(3, "Scout");
            allClasses.Add(8, "Sorcerer");
            allClasses.Add(5, "Theurgist");
            allClasses.Add(7, "Wizard");
            allClasses.Add(60, "MaulerAlb");
            allClasses.Add(31, "Berserker");
            allClasses.Add(30, "Bonedancer");
            allClasses.Add(26, "Healer");
            allClasses.Add(25, "Hunter");
            allClasses.Add(29, "Runemaster");
            allClasses.Add(32, "Savage");
            allClasses.Add(23, "Shadowblade");
            allClasses.Add(28, "Shaman");
            allClasses.Add(24, "Skald");
            allClasses.Add(27, "Spiritmaster");
            allClasses.Add(21, "Thane");
            allClasses.Add(34, "Valkyrie");
            allClasses.Add(59, "Warlock");
            allClasses.Add(22, "Warrior");
            allClasses.Add(61, "MaulerMid");
            allClasses.Add(55, "Animist");
            allClasses.Add(39, "Bainshee");
            allClasses.Add(48, "Bard");
            allClasses.Add(43, "Blademaster");
            allClasses.Add(45, "Champion");
            allClasses.Add(47, "Druid");
            allClasses.Add(40, "Eldritch");
            allClasses.Add(41, "Enchanter");
            allClasses.Add(44, "Hero");
            allClasses.Add(42, "Mentalist");
            allClasses.Add(49, "Nightshade");
            allClasses.Add(50, "Ranger");
            allClasses.Add(56, "Valewalker");
            allClasses.Add(58, "Vampiir");
            allClasses.Add(46, "Warden");
            allClasses.Add(62, "MaulerHib");
            allClasses.Add(16, "Acolyte");
            allClasses.Add(17, "AlbionRogue");
            allClasses.Add(20, "Disciple");
            allClasses.Add(15, "Elementalist");
            allClasses.Add(14, "Fighter");
            allClasses.Add(57, "Forester");
            allClasses.Add(52, "Guardian");
            allClasses.Add(18, "Mage");
            allClasses.Add(51, "Magician");
            allClasses.Add(38, "MidgardRogue");
            allClasses.Add(36, "Mystic");
            allClasses.Add(53, "Naturalist");
            allClasses.Add(37, "Seer");
            allClasses.Add(54, "Stalker");
            allClasses.Add(35, "Viking");
        }

        private void RewardQuestControl_Load(object sender, EventArgs e)
        {
            SetupDropdowns();
        }

        private void SetupDropdowns()
        {
            ComboboxService.BindQuestStep(_GoalType);
        }
    }

    //StringBuilder allcl = new StringBuilder(allowedClasses);
    ////allcl.Replace("", "All");
    //allcl.Replace("2", "Armsman");
    //allcl.Replace("13", "Cabalist");
    //allcl.Replace("6", "Cleric");
    //allcl.Replace("10", "Friar");
    //allcl.Replace("33", "Heretic");
    //allcl.Replace("9", "Infiltrator");
    //allcl.Replace("11", "Mercenary");
    //allcl.Replace("4", "Minstrel");
    //allcl.Replace("12", "Necromancer");
    //allcl.Replace("1", "Paladin");
    //allcl.Replace("19", "Reaver");
    //allcl.Replace("3", "Scout");
    //allcl.Replace("8", "Sorcerer");
    //allcl.Replace("5", "Theurgist");
    //allcl.Replace("7", "Wizard");
    //allcl.Replace("60", "MaulerAlb");
    //allcl.Replace("31", "Berserker");
    //allcl.Replace("30", "Bonedancer");
    //allcl.Replace("26", "Healer");
    //allcl.Replace("25", "Hunter");
    //allcl.Replace("29", "Runemaster");
    //allcl.Replace("32", "Savage");
    //allcl.Replace("23", "Shadowblade");
    //allcl.Replace("28", "Shaman");
    //allcl.Replace("24", "Skald");
    //allcl.Replace("27", "Spiritmaster");
    //allcl.Replace("21", "Thane");
    //allcl.Replace("34", "Valkyrie");
    //allcl.Replace("59", "Warlock");
    //allcl.Replace("22", "Warrior");
    //allcl.Replace("61", "MaulerMid");
    //allcl.Replace("55", "Animist");
    //allcl.Replace("39", "Bainshee");
    //allcl.Replace("48", "Bard");
    //allcl.Replace("43", "Blademaster");
    //allcl.Replace("45", "Champion");
    //allcl.Replace("47", "Druid");
    //allcl.Replace("40", "Eldritch");
    //allcl.Replace("41", "Enchanter");
    //allcl.Replace("44", "Hero");
    //allcl.Replace("42", "Mentalist");
    //allcl.Replace("49", "Nightshade");
    //allcl.Replace("50", "Ranger");
    //allcl.Replace("56", "Valewalker");
    //allcl.Replace("58", "Vampiir");
    //allcl.Replace("46", "Warden");
    //allcl.Replace("62", "MaulerHib");
    //allcl.Replace("16", "Acolyte");
    //allcl.Replace("17", "AlbionRogue");
    //allcl.Replace("20", "Disciple");
    //allcl.Replace("15", "Elementalist");
    //allcl.Replace("14", "Fighter");
    //allcl.Replace("57", "Forester");
    //allcl.Replace("52", "Guardian");
    //allcl.Replace("18", "Mage");
    //allcl.Replace("51", "Magician");
    //allcl.Replace("38", "MidgardRogue");
    //allcl.Replace("36", "Mystic");
    //allcl.Replace("53", "Naturalist");
    //allcl.Replace("37", "Seer");
    //allcl.Replace("54", "Stalker");
    //allcl.Replace("35", "Viking");
    //allowedClasses = allcl.ToString();
}
