﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOL.Database;
using DOLToolbox.Forms;
using DOLToolbox.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOLToolbox.Controls;
using EODModelViewer;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace DOLToolbox.Controls
{
    public partial class DataQuestControl : UserControl
    {
        private readonly DataQuestService _questService = new DataQuestService();

        // using this to determine if quest can be saved back to original ID
        private bool LoadedQuest { get; set; } = false;

        public DataQuestControl()
        {
            InitializeComponent();

            opt_dictionary = new Dictionary<int, string>();
            fin_dictionary = new Dictionary<int, string>();
            advtext_dictionary = new Dictionary<int, string>();
            colitem_dictionary = new Dictionary<int, string>();
            money_dictionary = new Dictionary<int, string>();
            xp_dictionary = new Dictionary<int, string>();
            clxp_dictionary = new Dictionary<int, string>();
            rp_dictionary = new Dictionary<int, string>();
            bp_dictionary = new Dictionary<int, string>();
            srctext_dictionary = new Dictionary<int, string>();
            srcname_dictionary = new Dictionary<int, string>();
            stepitem_dictionary = new Dictionary<int, string>();
            steptext_dictionary = new Dictionary<int, string>();
            trgtname_dictionary = new Dictionary<int, string>();
            trgttext_dictionary = new Dictionary<int, string>();
            steptype_dictionary = new Dictionary<int, string>();
            allClasses = new Dictionary<int, string>();
            quest_errors = new List<string>();

            PopulateClassDictionary();
        }
        #region variables
        private DBDataQuest _quest;

        string _SourceName, _SourceText, _StepType, _StepText, _StepItemTemplates, _AdvanceText, _TargetName, _TargetText;
        string _CollectItemTemplate, _RewardMoney, _RewardXP, _RewardCLXP, _RewardRp, _RewardBp, _OptionalRewardItemTemplates;
        string _FinalRewardItemTemplates, _QuestDependency, _AllowedClasses;

        public Dictionary<int, string> opt_dictionary;
        public Dictionary<int, string> fin_dictionary;
        public Dictionary<int, string> advtext_dictionary;
        public Dictionary<int, string> colitem_dictionary;
        public Dictionary<int, string> money_dictionary;
        public Dictionary<int, string> xp_dictionary;
        public Dictionary<int, string> clxp_dictionary;
        public Dictionary<int, string> rp_dictionary;
        public Dictionary<int, string> bp_dictionary;
        public Dictionary<int, string> srctext_dictionary;
        public Dictionary<int, string> srcname_dictionary;
        public Dictionary<int, string> stepitem_dictionary;
        public Dictionary<int, string> steptext_dictionary;
        public Dictionary<int, string> trgtname_dictionary;
        public Dictionary<int, string> trgttext_dictionary;
        public Dictionary<int, string> steptype_dictionary;
        // quest restrictions
        private Dictionary<int, string> allClasses;
        public List<string> quest_errors;

        #endregion

        #region buttonclicks
        private void SearchStartNPC_Click(object sender, EventArgs e)
        {
            var mobsearch = new MobSearch();

            mobsearch.SelectNpcClicked += (o, args) => { LoadStartNPC(((Mob)o).Name, ((Mob)o).Region); };

            mobsearch.ShowDialog(this);
        }

        private void SearchStartObject_Click(object sender, EventArgs e)
        {
            var objectsearch = new ObjectSearch();

            objectsearch.SelectNpcClicked += (o, args) => { LoadStartNPC(((WorldObject)o).Name, ((WorldObject)o).Region); };

            objectsearch.ShowDialog(this);
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

        // convert database string entries to dictionary for quest step usage
        private void StringToDictionary(string[] str, Dictionary<int, string> dict)
        {
            if (str == null)
            {
                return;
            }

            for (int i = 0; i < str.Length; i++)
            {
                dict.Add(i, str[i]);
            }
        }
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

        private void finrewardForward_Click_1(object sender, EventArgs e)
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

        private void finrewardBack_Click_1(object sender, EventArgs e)
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

        private void finSelect_Click(object sender, EventArgs e)
        {
            _FinalReward.Text = ItemTemplateBox.Text;
        }

        private void optSelect_Click(object sender, EventArgs e)
        {
            _OptionalReward.Text = ItemTemplateBox.Text;
        }

        private void itemSelect_Click(object sender, EventArgs e)
        {
            StepItem.Text = ItemTemplateBox.Text;
        }

        private void npcSearch_Click(object sender, EventArgs e)
        {

            var mobsearch = new MobSearch();

            mobsearch.SelectNpcClicked += (o, args) => { LoadTargetNPC(((Mob)o).Name, ((Mob)o).Region); };

            mobsearch.ShowDialog(this);
        }

        private void questSearch_Click(object sender, EventArgs e)
        {
            var search = new DataQuestSearchForm();

            search.SelectClicked += async (o, args) =>
            {
                if (!(o is DBDataQuest item))
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
            stepNumber.Text = "1";
            cleanDictionaries();
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


        /// <summary>
        /// convert database format "kill bandit;2|talk to NPC;3" to usable format
        /// </summary>        
        private void DeserializeData(DBDataQuest quest)
        {
            if (_quest == null)
            {
                return;
            }

            try
            {

                // Step type 
                StringBuilder stype = new StringBuilder(quest.StepType);
                // Start with long wording first to avoid any bad replace 
                stype.Replace("200", "RewardQuest");
                stype.Replace("11", "CollectFinish");
                stype.Replace("10", "Collect");
                stype.Replace("0", "Kill");
                stype.Replace("1", "KillFinish");
                stype.Replace("2", "Deliver");
                stype.Replace("3", "DeliverFinish");
                stype.Replace("4", "Interact");
                stype.Replace("5", "InteractFinish");
                stype.Replace("6", "Whisper");
                stype.Replace("7", "WhisperFinish");
                stype.Replace("8", "Search");
                stype.Replace("9", "SearchFinish");
                _StepType = stype.ToString();
                if (_StepType != null && !_StepType.Equals(""))
                {
                    string[] splitStepType = _StepType.Split(new string[] { "|" }, StringSplitOptions.None);
                    steptype_dictionary.Clear();
                    StringToDictionary(splitStepType, steptype_dictionary);
                    StepType.Text = steptype_dictionary[0];
                }

                // Source name 
                _SourceName = quest.SourceName;
                if (_SourceName != null && !_SourceName.Equals(""))
                {
                    string[] splitSourceName = _SourceName.Split(new string[] { "|" }, StringSplitOptions.None);
                    srcname_dictionary.Clear();
                    StringToDictionary(splitSourceName, srcname_dictionary);
                    SourceName.Text = srcname_dictionary[0];
                }

                // Target name 
                _TargetName = quest.TargetName;
                if (_TargetName != null && !_TargetName.Equals(""))
                {
                    string[] splitTargetName = _TargetName.Split(new string[] { "|" }, StringSplitOptions.None);
                    trgtname_dictionary.Clear();
                    StringToDictionary(splitTargetName, trgtname_dictionary);
                    TargetName.Text = trgtname_dictionary[0];
                }

                // Source text 
                _SourceText = quest.SourceText;
                if (_SourceText != null && !_SourceText.Equals(""))
                {
                    string[] splitSourceText = _SourceText.Split(new string[] { "|" }, StringSplitOptions.None);
                    srctext_dictionary.Clear();
                    StringToDictionary(splitSourceText, srctext_dictionary);
                    SourceText.Text = srctext_dictionary[0];
                }

                // Step text 
                _StepText = quest.StepText;
                if (_StepText != null && !_StepText.Equals(""))
                {
                    string[] splitStepText = _StepText.Split(new string[] { "|" }, StringSplitOptions.None);
                    steptext_dictionary.Clear();
                    StringToDictionary(splitStepText, steptext_dictionary);
                    StepText.Text = steptext_dictionary[0];
                }

                // Advance text 
                _AdvanceText = quest.AdvanceText;
                if (_AdvanceText != null && !_AdvanceText.Equals(""))
                {
                    string[] splitAdvanceText = _AdvanceText.Split(new string[] { "|" }, StringSplitOptions.None);
                    advtext_dictionary.Clear();
                    StringToDictionary(splitAdvanceText, advtext_dictionary);
                    AdvanceText.Text = advtext_dictionary[0];
                }

                // Target text 
                _TargetText = quest.TargetText;
                if (_TargetText != null && !_TargetText.Equals(""))
                {
                    string[] splitTargetText = _TargetText.Split(new string[] { "|" }, StringSplitOptions.None);
                    trgttext_dictionary.Clear();
                    StringToDictionary(splitTargetText, trgttext_dictionary);
                    TargetText.Text = trgttext_dictionary[0];
                }

                // Step item templates
                _StepItemTemplates = quest.StepItemTemplates;
                if (_StepItemTemplates != null && !_StepItemTemplates.Equals(""))
                {
                    string[] splitStepItemTemplates = _StepItemTemplates.Split(new string[] { "|" }, StringSplitOptions.None);
                    stepitem_dictionary.Clear();
                    StringToDictionary(splitStepItemTemplates, stepitem_dictionary);
                    StepItem.Text = stepitem_dictionary[0];
                }

                // Collect item templates 
                _CollectItemTemplate = quest.CollectItemTemplate;
                if (_CollectItemTemplate != null && !_CollectItemTemplate.Equals(""))
                {
                    string[] splitCollectItemTemplate = _CollectItemTemplate.Split(new string[] { "|" }, StringSplitOptions.None);
                    colitem_dictionary.Clear();
                    StringToDictionary(splitCollectItemTemplate, colitem_dictionary);
                    CollectItem.Text = colitem_dictionary[0];
                }

                // Option reward 
                _OptionalRewardItemTemplates = quest.OptionalRewardItemTemplates;
                if (_OptionalRewardItemTemplates != null && !_OptionalRewardItemTemplates.Equals(""))
                {
                    string[] splitOptionalRewardItemTemplates = _OptionalRewardItemTemplates.Split(new string[] { "|" }, StringSplitOptions.None);
                    opt_dictionary.Clear();
                    StringToDictionary(splitOptionalRewardItemTemplates, opt_dictionary);
                    _OptionalReward.Text = opt_dictionary[0];
                }

                // Final reward 
                _FinalRewardItemTemplates = quest.FinalRewardItemTemplates;
                if (_FinalRewardItemTemplates != null && !_FinalRewardItemTemplates.Equals(""))
                {
                    string[] splitFinalRewardItemTemplates = _FinalRewardItemTemplates.Split(new string[] { "|" }, StringSplitOptions.None);
                    fin_dictionary.Clear();
                    StringToDictionary(splitFinalRewardItemTemplates, fin_dictionary);
                    _FinalReward.Text = splitFinalRewardItemTemplates[0];
                }

                // Money 
                _RewardMoney = quest.RewardMoney;
                if (_RewardMoney != null && !_RewardMoney.Equals(""))
                {
                    string[] splitRewardLoney = _RewardMoney.Split(new string[] { "|" }, StringSplitOptions.None);
                    money_dictionary.Clear();
                    StringToDictionary(splitRewardLoney, money_dictionary);
                    RewardMoney.Text = money_dictionary[0];
                }

                // XP 
                _RewardXP = quest.RewardXP;
                if (_RewardXP != null && !_RewardXP.Equals(""))
                {
                    string[] splitRewardXP = _RewardXP.Split(new string[] { "|" }, StringSplitOptions.None);
                    xp_dictionary.Clear();
                    StringToDictionary(splitRewardXP, xp_dictionary);
                    RewardXp.Text = xp_dictionary[0];
                }

                // CLXP
                _RewardCLXP = quest.RewardCLXP;
                if (_RewardCLXP != null && !_RewardCLXP.Equals(""))
                {
                    string[] splitRewardCLXP = _RewardCLXP.Split(new string[] { "|" }, StringSplitOptions.None);
                    clxp_dictionary.Clear();
                    StringToDictionary(splitRewardCLXP, clxp_dictionary);
                    RewardCLXp.Text = clxp_dictionary[0];
                }

                // RP
                _RewardRp = quest.RewardRP;
                if (_RewardRp != null && !_RewardRp.Equals(""))
                {
                    string[] splitRewardRP = _RewardRp.Split(new string[] { "|" }, StringSplitOptions.None);
                    rp_dictionary.Clear();
                    StringToDictionary(splitRewardRP, rp_dictionary);
                    RewardRp.Text = rp_dictionary[0];
                }

                // BP
                _RewardBp = quest.RewardBP;
                if (_RewardBp != null && !_RewardBp.Equals(""))
                {
                    string[] splitRewardBP = _RewardBp.Split(new string[] { "|" }, StringSplitOptions.None);
                    bp_dictionary.Clear();
                    StringToDictionary(splitRewardBP, bp_dictionary);
                    RewardBp.Text = bp_dictionary[0];
                }

                // Allowed classes
                _AllowedClasses = quest.AllowedClasses;

                string[] splitAllowedClasses;
                if (!string.IsNullOrWhiteSpace(_AllowedClasses))
                {
                    splitAllowedClasses = _AllowedClasses.Split(new string[] { "|" }, StringSplitOptions.None);

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
        private void button2_Click(object sender, EventArgs e)
        {
            var objectsearch = new ObjectSearch();

            objectsearch.SelectNpcClicked += (o, args) => { LoadTargetNPC(((WorldObject)o).Name, ((WorldObject)o).Region); };

            objectsearch.ShowDialog(this);
        }

        private void questDelete_Click(object sender, EventArgs e)
        {
            if (_quest == null)
            {
                return;
            }

            var confirmResult = MessageBox.Show(@"This will clear the form. Are you sure you want to continue? (If this quest is in the database it will NOT be deleted)",
                @"Confirm Delete!!",
                MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            LoadedQuest = false;

            stepNumber.Text = "1";
            cleanDictionaries();
            Clear();
        }

        private void cleanDictionaries()
        {
            opt_dictionary.Clear();
            fin_dictionary.Clear();
            advtext_dictionary.Clear();
            colitem_dictionary.Clear();
            money_dictionary.Clear();
            xp_dictionary.Clear();
            clxp_dictionary.Clear();
            rp_dictionary.Clear();
            bp_dictionary.Clear();
            srctext_dictionary.Clear();
            srcname_dictionary.Clear();
            stepitem_dictionary.Clear();
            steptext_dictionary.Clear();
            trgtname_dictionary.Clear();
            trgttext_dictionary.Clear();
            steptype_dictionary.Clear();
        }

        private void collectSelect_Click(object sender, EventArgs e)
        {
            CollectItem.Text = ItemTemplateBox.Text;
        }
        //Step data forward
        private void stepForward_Click(object sender, EventArgs e)
        {

            int stepNum = int.Parse(stepNumber.Text);

            if (StepType.Text == "" || TargetName.Text == "")
            {
                MessageBox.Show("You cannot proceed until you have selected a step type and target!");
                return;
            }

            #region Step forward data entry

            //AdvanceText.Text
            if (!advtext_dictionary.ContainsKey(stepNum - 1))
            {
                advtext_dictionary.Add(stepNum - 1, AdvanceText.Text);
            }
            else
            {
                advtext_dictionary.Remove(stepNum - 1);
                advtext_dictionary.Add(stepNum - 1, AdvanceText.Text);
            }
            AdvanceText.Text = "";

            //CollectItem.Text
            if (!colitem_dictionary.ContainsKey(stepNum - 1))
            {
                colitem_dictionary.Add(stepNum - 1, CollectItem.Text);
            }
            else
            {
                colitem_dictionary.Remove(stepNum - 1);
                colitem_dictionary.Add(stepNum - 1, CollectItem.Text);
            }
            CollectItem.Text = "";

            //RewardMoney.Text
            if (!money_dictionary.ContainsKey(stepNum - 1))
            {
                money_dictionary.Add(stepNum - 1, RewardMoney.Text);
            }
            else
            {
                money_dictionary.Remove(stepNum - 1);
                money_dictionary.Add(stepNum - 1, RewardMoney.Text);
            }
            RewardMoney.Text = "";

            //RewardXp.Text
            if (!xp_dictionary.ContainsKey(stepNum - 1))
            {
                xp_dictionary.Add(stepNum - 1, RewardXp.Text);
            }
            else
            {
                xp_dictionary.Remove(stepNum - 1);
                xp_dictionary.Add(stepNum - 1, RewardXp.Text);
            }
            RewardXp.Text = "";

            //RewardCLXp.Text
            if (!clxp_dictionary.ContainsKey(stepNum - 1))
            {
                clxp_dictionary.Add(stepNum - 1, RewardCLXp.Text);
            }
            else
            {
                clxp_dictionary.Remove(stepNum - 1);
                clxp_dictionary.Add(stepNum - 1, RewardCLXp.Text);
            }
            RewardCLXp.Text = "";

            //RewardRp.Text
            if (!rp_dictionary.ContainsKey(stepNum - 1))
            {
                rp_dictionary.Add(stepNum - 1, RewardRp.Text);
            }
            else
            {
                rp_dictionary.Remove(stepNum - 1);
                rp_dictionary.Add(stepNum - 1, RewardRp.Text);
            }
            RewardRp.Text = "";

            //RewardBp.Text
            if (!bp_dictionary.ContainsKey(stepNum - 1))
            {
                bp_dictionary.Add(stepNum - 1, RewardBp.Text);
            }
            else
            {
                bp_dictionary.Remove(stepNum - 1);
                bp_dictionary.Add(stepNum - 1, RewardBp.Text);
            }
            RewardBp.Text = "";

            //SourceText.Text
            if (!srctext_dictionary.ContainsKey(stepNum - 1))
            {
                srctext_dictionary.Add(stepNum - 1, SourceText.Text);
            }
            else
            {
                srctext_dictionary.Remove(stepNum - 1);
                srctext_dictionary.Add(stepNum - 1, SourceText.Text);
            }
            SourceText.Text = "";

            //SourceName.Text
            if (!srcname_dictionary.ContainsKey(stepNum - 1))
            {
                srcname_dictionary.Add(stepNum - 1, SourceName.Text);
            }
            else
            {
                srcname_dictionary.Remove(stepNum - 1);
                srcname_dictionary.Add(stepNum - 1, SourceName.Text);
            }
            SourceText.Text = "";

            //StepItem.Text
            if (!stepitem_dictionary.ContainsKey(stepNum - 1))
            {
                stepitem_dictionary.Add(stepNum - 1, StepItem.Text);
            }
            else
            {
                stepitem_dictionary.Remove(stepNum - 1);
                stepitem_dictionary.Add(stepNum - 1, StepItem.Text);
            }
            StepItem.Text = "";

            //StepText.Text
            if (!steptext_dictionary.ContainsKey(stepNum - 1))
            {
                steptext_dictionary.Add(stepNum - 1, StepText.Text);
            }
            else
            {
                steptext_dictionary.Remove(stepNum - 1);
                steptext_dictionary.Add(stepNum - 1, StepText.Text);
            }
            StepText.Text = "";

            //TargetName.Text
            if (!trgtname_dictionary.ContainsKey(stepNum - 1))
            {
                trgtname_dictionary.Add(stepNum - 1, TargetName.Text);
            }
            else
            {
                trgtname_dictionary.Remove(stepNum - 1);
                trgtname_dictionary.Add(stepNum - 1, TargetName.Text);
            }
            TargetName.Text = "";

            //TargetText.Text
            if (!trgttext_dictionary.ContainsKey(stepNum - 1))
            {
                trgttext_dictionary.Add(stepNum - 1, TargetText.Text);
            }
            else
            {
                trgttext_dictionary.Remove(stepNum - 1);
                trgttext_dictionary.Add(stepNum - 1, TargetText.Text);
            }
            TargetText.Text = "";

            //StepType.Text
            if (!steptype_dictionary.ContainsKey(stepNum - 1))
            {
                steptype_dictionary.Add(stepNum - 1, StepType.Text);
            }
            else
            {
                steptype_dictionary.Remove(stepNum - 1);
                steptype_dictionary.Add(stepNum - 1, StepType.Text);
            }
            StepType.Text = "";

            #endregion

            stepNumber.Text = (stepNum + 1).ToString(); //increment label

            #region Step forward check next step

            int stepNext = int.Parse(stepNumber.Text);
            string stepvalue;

            if (advtext_dictionary.ContainsKey(stepNext - 1))
            {
                advtext_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                AdvanceText.Text = stepvalue;
            }
            else AdvanceText.Text = "";

            if (colitem_dictionary.ContainsKey(stepNext - 1))
            {
                colitem_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                CollectItem.Text = stepvalue;
            }
            else CollectItem.Text = "";

            if (money_dictionary.ContainsKey(stepNext - 1))
            {
                money_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                RewardMoney.Text = stepvalue;
            }
            else RewardMoney.Text = "0";

            if (xp_dictionary.ContainsKey(stepNext - 1))
            {
                xp_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                RewardXp.Text = stepvalue;
            }
            else RewardXp.Text = "0";

            if (clxp_dictionary.ContainsKey(stepNext - 1))
            {
                clxp_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                RewardCLXp.Text = stepvalue;
            }
            else RewardCLXp.Text = "0";

            if (rp_dictionary.ContainsKey(stepNext - 1))
            {
                rp_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                RewardRp.Text = stepvalue;
            }
            else RewardRp.Text = "0";

            if (bp_dictionary.ContainsKey(stepNext - 1))
            {
                bp_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                RewardBp.Text = stepvalue;
            }
            else RewardBp.Text = "0";

            if (srctext_dictionary.ContainsKey(stepNext - 1))
            {
                srctext_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                SourceText.Text = stepvalue;
            }
            if (srcname_dictionary.ContainsKey(stepNext - 1))
            {
                srcname_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                SourceName.Text = stepvalue;
            }
            else SourceText.Text = "";

            if (stepitem_dictionary.ContainsKey(stepNext - 1))
            {
                stepitem_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                StepItem.Text = stepvalue;
            }
            else StepItem.Text = "";

            if (steptext_dictionary.ContainsKey(stepNext - 1))
            {
                steptext_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                StepText.Text = stepvalue;
            }
            else StepText.Text = "";

            if (trgtname_dictionary.ContainsKey(stepNext - 1))
            {
                trgtname_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                TargetName.Text = stepvalue;
            }
            else TargetName.Text = "";

            if (trgttext_dictionary.ContainsKey(stepNext - 1))
            {
                trgttext_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                TargetText.Text = stepvalue;
            }
            else TargetText.Text = "";

            if (steptype_dictionary.ContainsKey(stepNext - 1))
            {
                steptype_dictionary.TryGetValue(stepNext - 1, out stepvalue);
                StepType.Text = stepvalue;
            }
            else StepType.Text = "";

            #endregion
        }

        //Step data back
        private void stepBack_Click(object sender, EventArgs e)
        {
            int stepNum = int.Parse(stepNumber.Text);

            if (stepNum == 1) //return if already at step 1, there ain't no step 0
            {
                return;
            }

            //remove step altogether if mandatory fields are not entered
            if (TargetName.Text == "" || StepType.Text == "")
            {
                advtext_dictionary.Remove(stepNum - 1);
                colitem_dictionary.Remove(stepNum - 1);
                money_dictionary.Remove(stepNum - 1);
                xp_dictionary.Remove(stepNum - 1);
                clxp_dictionary.Remove(stepNum - 1);
                rp_dictionary.Remove(stepNum - 1);
                bp_dictionary.Remove(stepNum - 1);
                srctext_dictionary.Remove(stepNum - 1);
                srcname_dictionary.Remove(stepNum - 1);
                stepitem_dictionary.Remove(stepNum - 1);
                steptext_dictionary.Remove(stepNum - 1);
                trgtname_dictionary.Remove(stepNum - 1);
                trgttext_dictionary.Remove(stepNum - 1);
                steptype_dictionary.Remove(stepNum - 1);
            }
            else
            //Needed to commit data to m_dictionary when the back button is clicked
            {
                advtext_dictionary.Remove(stepNum - 1);
                advtext_dictionary.Add(stepNum - 1, AdvanceText.Text);
                colitem_dictionary.Remove(stepNum - 1);
                colitem_dictionary.Add(stepNum - 1, CollectItem.Text);
                money_dictionary.Remove(stepNum - 1);
                money_dictionary.Add(stepNum - 1, RewardMoney.Text);
                xp_dictionary.Remove(stepNum - 1);
                xp_dictionary.Add(stepNum - 1, RewardXp.Text);
                clxp_dictionary.Remove(stepNum - 1);
                clxp_dictionary.Add(stepNum - 1, RewardCLXp.Text);
                rp_dictionary.Remove(stepNum - 1);
                rp_dictionary.Add(stepNum - 1, RewardRp.Text);
                bp_dictionary.Remove(stepNum - 1);
                bp_dictionary.Add(stepNum - 1, RewardBp.Text);
                srctext_dictionary.Remove(stepNum - 1);
                srctext_dictionary.Add(stepNum - 1, SourceText.Text);
                srcname_dictionary.Remove(stepNum - 1);
                srcname_dictionary.Add(stepNum - 1, SourceName.Text);
                stepitem_dictionary.Remove(stepNum - 1);
                stepitem_dictionary.Add(stepNum - 1, StepItem.Text);
                steptext_dictionary.Remove(stepNum - 1);
                steptext_dictionary.Add(stepNum - 1, StepText.Text);
                trgtname_dictionary.Remove(stepNum - 1);
                trgtname_dictionary.Add(stepNum - 1, TargetName.Text);
                trgttext_dictionary.Remove(stepNum - 1);
                trgttext_dictionary.Add(stepNum - 1, TargetText.Text);
                steptype_dictionary.Remove(stepNum - 1);
                steptype_dictionary.Add(stepNum - 1, StepType.Text);
            }


            stepNumber.Text = (stepNum - 1).ToString();
            string stepvalue;
            stepNum--;

            //Previous step data check
            if (advtext_dictionary.ContainsKey(stepNum - 1))
            {
                advtext_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                AdvanceText.Text = stepvalue;
            }
            else AdvanceText.Text = "";

            if (colitem_dictionary.ContainsKey(stepNum - 1))
            {
                colitem_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                CollectItem.Text = stepvalue;
            }
            else CollectItem.Text = "";

            if (money_dictionary.ContainsKey(stepNum - 1))
            {
                money_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                RewardMoney.Text = stepvalue;
            }
            else RewardMoney.Text = "0";

            if (xp_dictionary.ContainsKey(stepNum - 1))
            {
                xp_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                RewardXp.Text = stepvalue;
            }
            else RewardXp.Text = "0";

            if (clxp_dictionary.ContainsKey(stepNum - 1))
            {
                clxp_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                RewardCLXp.Text = stepvalue;
            }
            else RewardCLXp.Text = "0";

            if (rp_dictionary.ContainsKey(stepNum - 1))
            {
                rp_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                RewardRp.Text = stepvalue;
            }
            else RewardRp.Text = "0";

            if (bp_dictionary.ContainsKey(stepNum - 1))
            {
                bp_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                RewardBp.Text = stepvalue;
            }
            else RewardBp.Text = "0";

            if (srctext_dictionary.ContainsKey(stepNum - 1))
            {
                srctext_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                SourceText.Text = stepvalue;
            }
            else SourceText.Text = "";

            if (srcname_dictionary.ContainsKey(stepNum - 1))
            {
                srcname_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                SourceName.Text = stepvalue;
            }
            else SourceName.Text = "";

            if (stepitem_dictionary.ContainsKey(stepNum - 1))
            {
                stepitem_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                StepItem.Text = stepvalue;

            }
            else StepItem.Text = "";

            if (steptext_dictionary.ContainsKey(stepNum - 1))
            {
                steptext_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                StepText.Text = stepvalue;
            }
            else StepText.Text = "";

            if (trgtname_dictionary.ContainsKey(stepNum - 1))
            {
                trgtname_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                TargetName.Text = stepvalue;
            }
            else TargetName.Text = "";

            if (trgttext_dictionary.ContainsKey(stepNum - 1))
            {
                trgttext_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                TargetText.Text = stepvalue;
            }
            else TargetText.Text = "";

            if (steptype_dictionary.ContainsKey(stepNum - 1)) //wtf...enum can get bent
            {
                steptype_dictionary.TryGetValue(stepNum - 1, out stepvalue);
                StepType.Text = stepvalue;
            }
            else StepType.Text = "";
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
                string finvalue;
                fin_dictionary.TryGetValue(finNum, out finvalue);

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
                string finvalue;
                fin_dictionary.TryGetValue(finNext, out finvalue);
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
                string finvalue;
                fin_dictionary.TryGetValue(finNum, out finvalue);
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
            string finback;
            int fincheck = int.Parse(finNumber.Text);
            fincheck--;
            fin_dictionary.TryGetValue(fincheck, out finback);
            _FinalReward.Text = finback;

            finNumber.Text = (finNum - 1).ToString(); //finally, decrement fin label
        }
        #endregion

        #region SaveQuestButton
        private void questSave_Click(object sender, EventArgs e)
        {
            int stepNum = int.Parse(stepNumber.Text);
            int optNum = int.Parse(optNumber.Text);
            int finNum = int.Parse(finNumber.Text);
            
            // Refresh step data to the dictionary if the forward/back button has not been pressed 
            advtext_dictionary.Remove(stepNum - 1);
            advtext_dictionary.Add(stepNum - 1, AdvanceText.Text);
            colitem_dictionary.Remove(stepNum - 1);
            colitem_dictionary.Add(stepNum - 1, CollectItem.Text);
            money_dictionary.Remove(stepNum - 1);
            money_dictionary.Add(stepNum - 1, RewardMoney.Text);
            xp_dictionary.Remove(stepNum - 1);
            xp_dictionary.Add(stepNum - 1, RewardXp.Text);
            clxp_dictionary.Remove(stepNum - 1);
            clxp_dictionary.Add(stepNum - 1, RewardCLXp.Text);
            rp_dictionary.Remove(stepNum - 1);
            rp_dictionary.Add(stepNum - 1, RewardRp.Text);
            bp_dictionary.Remove(stepNum - 1);
            bp_dictionary.Add(stepNum - 1, RewardBp.Text);
            srctext_dictionary.Remove(stepNum - 1);
            srctext_dictionary.Add(stepNum - 1, SourceText.Text);
            srcname_dictionary.Remove(stepNum - 1);
            srcname_dictionary.Add(stepNum - 1, SourceName.Text);
            stepitem_dictionary.Remove(stepNum - 1);
            stepitem_dictionary.Add(stepNum - 1, StepItem.Text);
            steptext_dictionary.Remove(stepNum - 1);
            steptext_dictionary.Add(stepNum - 1, StepText.Text);
            trgtname_dictionary.Remove(stepNum - 1);
            trgtname_dictionary.Add(stepNum - 1, TargetName.Text);
            trgttext_dictionary.Remove(stepNum - 1);
            trgttext_dictionary.Add(stepNum - 1, TargetText.Text);
            steptype_dictionary.Remove(stepNum - 1);
            steptype_dictionary.Add(stepNum - 1, StepType.Text);

            opt_dictionary.Remove(optNum - 1); // do this incase it was edited without pressing forward/back
            if (!string.IsNullOrWhiteSpace(_OptionalReward.Text)) 
            {
                opt_dictionary.Add(optNum - 1, _OptionalReward.Text);
            }
            fin_dictionary.Remove(finNum - 1); // do this incase it was edited without pressing forward/back
            if (!string.IsNullOrWhiteSpace(_FinalReward.Text))
            {
                fin_dictionary.Add(finNum - 1, _FinalReward.Text);
            }

            try
            {
                #region String conversions                
                _OptionalRewardItemTemplates = String.Join("|", Array.ConvertAll(opt_dictionary.Values.ToArray(), i => i.ToString()));
                _FinalRewardItemTemplates = String.Join("|", Array.ConvertAll(fin_dictionary.Values.ToArray(), i => i.ToString()));
                _AdvanceText = String.Join("|", Array.ConvertAll(advtext_dictionary.Values.ToArray(), i => i.ToString()));
                _CollectItemTemplate = String.Join("|", Array.ConvertAll(colitem_dictionary.Values.ToArray(), i => i.ToString()));
                _RewardMoney = String.Join("|", Array.ConvertAll(money_dictionary.Values.ToArray(), i => i.ToString()));
                _RewardXP = String.Join("|", Array.ConvertAll(xp_dictionary.Values.ToArray(), i => i.ToString()));
                _RewardCLXP = String.Join("|", Array.ConvertAll(clxp_dictionary.Values.ToArray(), i => i.ToString()));
                _RewardRp = String.Join("|", Array.ConvertAll(rp_dictionary.Values.ToArray(), i => i.ToString()));
                _RewardBp = String.Join("|", Array.ConvertAll(bp_dictionary.Values.ToArray(), i => i.ToString()));
                _SourceText = String.Join("|", Array.ConvertAll(srctext_dictionary.Values.ToArray(), i => i.ToString()));
                _SourceName = String.Join("|", Array.ConvertAll(srcname_dictionary.Values.ToArray(), i => i.ToString()));
                _StepItemTemplates = String.Join("|", Array.ConvertAll(stepitem_dictionary.Values.ToArray(), i => i.ToString()));
                _StepText = String.Join("|", Array.ConvertAll(steptext_dictionary.Values.ToArray(), i => i.ToString()));
                _TargetName = String.Join("|", Array.ConvertAll(trgtname_dictionary.Values.ToArray(), i => i.ToString()));
                _TargetText = String.Join("|", Array.ConvertAll(trgttext_dictionary.Values.ToArray(), i => i.ToString()));
                _StepType = String.Join("|", Array.ConvertAll(steptype_dictionary.Values.ToArray(), i => i.ToString()));
                _AllowedClasses = String.Join("|", listClasses.SelectedItems.Cast<object>().Select(i => i.ToString()));

                //eStepType string replace values:
                StringBuilder stype = new StringBuilder(_StepType);
                // Start with long wording first to avoid any bad replace 
                stype.Replace("KillFinish", "1");
                stype.Replace("DeliverFinish", "3");
                stype.Replace("InteractFinish", "5");
                stype.Replace("WhisperFinish", "7");
                stype.Replace("SearchFinish", "9");
                stype.Replace("CollectFinish", "11");
                stype.Replace("Kill", "0");
                stype.Replace("Deliver", "2");
                stype.Replace("Interact", "4");
                stype.Replace("Whisper", "6");
                stype.Replace("Search", "8");
                stype.Replace("Collect", "10");
                stype.Replace("RewardQuest", "200");
                _StepType = stype.ToString();

                StringBuilder allcl = new StringBuilder(_AllowedClasses);
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
                allcl.Replace("Healer", "36");
                allcl.Replace("Hunter", "35");
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
                _AllowedClasses = allcl.ToString();
                #endregion

            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DBDataQuest q = new DBDataQuest();

            // Before saving check for missing or wrong information in the quest 
            if (CheckQuestOK() == false)
            {
                MessageBox.Show("Quest data is invalid, please fix the below fields : \n\n" + String.Join("\n", quest_errors), 
                    "Error on quest", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }

            // Save can be done : retrieve all values from Form for DB insertion 
            if (!_ID.Text.Equals(""))
            {
                q.ID = int.Parse(_ID.Text);
            }
            q.Name = _Name.Text;
            ComboboxService.SelectItemModel item = (ComboboxService.SelectItemModel) _StartType.Items[_StartType.SelectedIndex];
            q.StartType = Convert.ToByte(item.Id);
            q.StartName = _StartName.Text;
            q.StartRegionID = ushort.Parse(_StartRegionID.Text);
            q.AcceptText = _AcceptText.Text;
            q.Description = _Description.Text;
            q.SourceText = _SourceText; //serialized
            q.SourceName = _SourceName;
            q.StepType = _StepType; //serialized
            q.StepText = _StepText; //serialized
            q.StepItemTemplates = _StepItemTemplates; //serialized
            q.AdvanceText = _AdvanceText; //serialized
            q.TargetName = _TargetName; //serialized
            q.TargetText = _TargetText; //serialized
            q.CollectItemTemplate = _CollectItemTemplate; //serialized
            q.MaxCount = byte.Parse(_MaxCount.Text);
            q.MinLevel = byte.Parse(_MinLevel.Text);
            q.MaxLevel = byte.Parse(_MaxLevel.Text);
            q.RewardMoney = _RewardMoney; //serialized
            q.RewardXP = _RewardXP; //serialized
            q.RewardCLXP = _RewardCLXP; //serialized
            q.RewardRP = _RewardRp; //serialized
            q.RewardBP = _RewardBp; //serialized
            q.OptionalRewardItemTemplates = _OptionalRewardItemTemplates;
            q.FinalRewardItemTemplates = _FinalRewardItemTemplates;
            q.FinishText = _FinishText.Text;
            q.QuestDependency = _QuestDependency; //might need to serialize....if quest has multiple dependencies
            q.AllowedClasses = _AllowedClasses; //serialized
            q.ClassType = _ClassType.Text;

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
        #endregion

        #region Methods

        private bool CheckQuestOK()
        {
            bool error_raised = false;
            quest_errors.Clear();

            ComboboxService.SelectItemModel item = (ComboboxService.SelectItemModel)_StartType.Items[_StartType.SelectedIndex];

            // Quest type undefined
            if (item.Id == null)
            {
                error_raised = true;
                quest_errors.Add("Start type is invalid");
            }

            // Quest name 
            if (_Name.Text.Equals(""))
            {
                error_raised = true;
                quest_errors.Add("Quest name must be defined");
            }

            // Min or Max level
            if (_MinLevel.Text.Equals(""))
            {
                error_raised = true;
                quest_errors.Add("Min level must be defined");
            }
            if (_MaxLevel.Text.Equals(""))
            {
                error_raised = true;
                quest_errors.Add("Max level must be defined");
            }
            if (_MaxCount.Text.Equals(""))
            {
                error_raised = true;
                quest_errors.Add("Max count must be defined");
            }

            // Start NPC 
            if (_StartName.Text.Equals(""))
            {
                error_raised = true;
                quest_errors.Add("Start NPC name must be defined");
            }
            if (_StartRegionID.Text.Equals(""))
            {
                error_raised = true;
                quest_errors.Add("Start NPC region id must be defined");
            }


            // Overall status 
            if (error_raised == true)
                return false;

            return true;
        }

        private void SetupDropdowns()
        {
            ComboboxService.BindQuestType(_StartType);
            ComboboxService.BindQuestStep(StepType);

        }


        private void Clear()
        {
            _quest = null;
            BindingService.ClearData(this);
        }



        private void SyncDataQuest()
        {
            BindingService.SyncData(_quest, this);
        }

        private void DataQuestControl_Load_1(object sender, EventArgs e)
        {
            SetupDropdowns();
        }
        private void LoadTargetNPC(string mobName, ushort mobRegion)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }
            TargetName.Text = mobName + ";" + mobRegion.ToString();

        }
        private void LoadItem(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return;
            }

            ItemTemplateBox.Text = itemId;
        }

        private void LoadStartNPC(string mobName, ushort mobRegion)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }

            _StartName.Text = mobName;
            _StartRegionID.Text = mobRegion.ToString();
        }
        #endregion

    }
}