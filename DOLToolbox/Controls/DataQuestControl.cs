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
    public partial class DataQuestControl : UserControl
    {
        private readonly DataQuestService _questService = new DataQuestService();

        // using this to determine if quest can be saved back to original ID
        private bool LoadedQuest { get; set; }

        public DataQuestControl()
        {
            InitializeComponent();
            PopulateClassDictionary();
        }

        private DBDataQuest _quest;

        private string _sourceName, _sourceText, _stepType, _stepText, _stepItemTemplates, _advanceText, _targetName, _targetText;
        private string _collectItemTemplate, _rewardMoney, _rewardXp, _rewardClXp, _rewardRp, _rewardBp, _optionalRewardItemTemplates;
        private string _finalRewardItemTemplates, _allowedClasses;

        private readonly Dictionary<int, string> _optDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _finDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _advTextDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _colItemDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _moneyDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _xpDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _clXpDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _rpDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _bpDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _srcTextDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _srcNameDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _stepItemDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _stepTextDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _trgNameDictionary = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _trgTextDictionary = new Dictionary<int, string>();

        private readonly Dictionary<int, string> _stepTypeDictionary = new Dictionary<int, string>();
        // quest restrictions
        private readonly Dictionary<int, string> _allClasses = new Dictionary<int, string>();
        private readonly List<string> _questErrors = new List<string>();

        private void SearchStartNPC_Click(object sender, EventArgs e)
        {
            var mobSearch = new MobSearch();
            mobSearch.SelectNpcClicked += (o, args) => { LoadStartNpc(((Mob)o).Name, ((Mob)o).Region); };
            mobSearch.ShowDialog(this);
        }

        private void SearchStartObject_Click(object sender, EventArgs e)
        {
            var objectSearch = new ObjectSearch();
            objectSearch.SelectNpcClicked += (o, args) => { LoadStartNpc(((WorldObject)o).Name, ((WorldObject)o).Region); };
            objectSearch.ShowDialog(this);
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

        private void OptRewardForward_Click(object sender, EventArgs e)
        {
            if (_OptionalReward.Text == "")
            {
                MessageBox.Show(@"You can't add nothing as a reward!", @"PEBKAC");
                return;
            }

            int optNum = int.Parse(optNumber.Text);
            var optNumDic = optNum - 1;

            if (optNum == 8)
            {
                _optDictionary.Remove(optNumDic);
                _optDictionary.Add(optNumDic, _OptionalReward.Text);
                MessageBox.Show(@"Last optional item added", @"8 Optional Rewards Max");
                return;
            }

            if (!_optDictionary.ContainsKey(optNumDic)) //If the reward data is not in the dictionary...check for step 1, then add
            {
                _optDictionary.Add(optNumDic, _OptionalReward.Text);
            }
            else //If the reward data is in the dictionary...check if the values match and add if they don't
            {
                _optDictionary.TryGetValue(optNumDic, out var optValue);

                if (optValue != _OptionalReward.Text)
                {
                    _optDictionary.Remove(optNumDic);
                    _optDictionary.Add(optNumDic, _OptionalReward.Text);
                }
            }

            optNumber.Text = (optNum + 1).ToString();

            //Check if next step contains data
            var optNext = int.Parse(optNumber.Text);
            var optNextDic = optNext - 1;
            if (_optDictionary.ContainsKey(optNextDic))
            {
                _optDictionary.TryGetValue(optNextDic, out var optValue);
                _OptionalReward.Text = optValue;
            }
            else _OptionalReward.Text = "";
        }

        // convert database string entries to dictionary for quest step usage
        private static void StringToDictionary(string[] str, Dictionary<int, string> dict)
        {
            if (str == null)
            {
                return;
            }

            for (var i = 0; i < str.Length; i++)
            {
                dict.Add(i, str[i]);
            }
        }
        private void OptRewardBack_Click(object sender, EventArgs e)
        {
            int optNum = int.Parse(optNumber.Text);
            var optNumDic = optNum - 1;

            if (optNum == 1) //return if already at step 1, there ain't no step 0
            {
                return;
            }

            if (_OptionalReward.Text != "") //Add data on back click
            {
                _optDictionary.TryGetValue(optNumDic, out var optValue);
                if (optValue != _OptionalReward.Text)
                {
                    _optDictionary.Remove(optNumDic);
                    _optDictionary.Add(optNumDic, _OptionalReward.Text);
                }
            }
            else
            {
                if (_optDictionary.ContainsKey(optNumDic + 1))
                {
                    MessageBox.Show(@"You must remove the items listed after the current item to remove this one!", @"Error");
                    return;
                }
                else
                {
                    _optDictionary.Remove(optNumDic);
                }
            }

            //Pull previous step data
            var optCheck = int.Parse(optNumber.Text);
            optCheck--;
            var optCheckDic = optCheck - 1;
            _optDictionary.TryGetValue(optCheckDic, out var optBack);
            _OptionalReward.Text = optBack;

            optNumber.Text = (optNum - 1).ToString(); //finally, decrement opt label
        }

        private void FinRewardForward_Click_1(object sender, EventArgs e)
        {
            if (_FinalReward.Text == "")
            {
                MessageBox.Show(@"You can't add nothing as a reward!", @"PEBKAC");
                return;
            }

            var finNum = int.Parse(finNumber.Text);
            var finNumDic = finNum - 1;

            if (!_finDictionary.ContainsKey(finNumDic)) //If the reward data is not in the dictionary...check for step 1, then add
            {
                _finDictionary.Add(finNumDic, _FinalReward.Text);
            }
            else //If the reward data is in the dictionary...check if the values match and add if they don't
            {
                _finDictionary.TryGetValue(finNumDic, out var finValue);

                if (finValue != _FinalReward.Text)
                {
                    _finDictionary.Remove(finNumDic);
                    _finDictionary.Add(finNumDic, _FinalReward.Text);
                }
            }

            finNumber.Text = (finNum + 1).ToString();

            //Check if next step contains data
            var finNext = int.Parse(finNumber.Text);
            var finNextDic = finNext - 1;
            if (_finDictionary.ContainsKey(finNextDic))
            {
                _finDictionary.TryGetValue(finNextDic, out var finValue);
                _FinalReward.Text = finValue;
            }
            else _FinalReward.Text = "";
        }

        private void FinRewardBack_Click_1(object sender, EventArgs e)
        {
            var finNum = int.Parse(finNumber.Text);
            var finNumDic = finNum - 1;

            if (finNum == 1) //return if already at step 1, there ain't no step 0
            {
                return;
            }

            if (_FinalReward.Text != "") //Add data on back click
            {
                _finDictionary.TryGetValue(finNumDic, out var finValue);
                if (finValue != _FinalReward.Text) //check if data is different from dictionary data
                {
                    _finDictionary.Remove(finNumDic);
                    _finDictionary.Add(finNumDic, _FinalReward.Text);
                }
            }
            else //There are no breaks in reward data, so a blank box cannot exist between populated blocks
            {
                if (_finDictionary.ContainsKey(finNumDic + 1))
                {
                    MessageBox.Show(@"You must remove the items listed after the current item to remove this one!", @"Error");
                    return;
                }

                _finDictionary.Remove(finNumDic);
            }

            //Pull previous step data
            var finCheck = int.Parse(finNumber.Text);
            finCheck--;
            var finCheckDic = finCheck - 1;
            _finDictionary.TryGetValue(finCheckDic, out var finBack);
            _FinalReward.Text = finBack;
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
            var mobSearch = new MobSearch();
            mobSearch.SelectNpcClicked += (o, args) => { LoadTargetNpc(((Mob)o).Name, ((Mob)o).Region); };
            mobSearch.ShowDialog(this);
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
            stepNumber.Text = @"1";
            CleanDictionaries();
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
            _allClasses.Add(2, "Armsman");
            _allClasses.Add(13, "Cabalist");
            _allClasses.Add(6, "Cleric");
            _allClasses.Add(10, "Friar");
            _allClasses.Add(33, "Heretic");
            _allClasses.Add(9, "Infiltrator");
            _allClasses.Add(11, "Mercenary");
            _allClasses.Add(4, "Minstrel");
            _allClasses.Add(12, "Necromancer");
            _allClasses.Add(1, "Paladin");
            _allClasses.Add(19, "Reaver");
            _allClasses.Add(3, "Scout");
            _allClasses.Add(8, "Sorcerer");
            _allClasses.Add(5, "Theurgist");
            _allClasses.Add(7, "Wizard");
            _allClasses.Add(60, "MaulerAlb");
            _allClasses.Add(31, "Berserker");
            _allClasses.Add(30, "Bonedancer");
            _allClasses.Add(26, "Healer");
            _allClasses.Add(25, "Hunter");
            _allClasses.Add(29, "Runemaster");
            _allClasses.Add(32, "Savage");
            _allClasses.Add(23, "Shadowblade");
            _allClasses.Add(28, "Shaman");
            _allClasses.Add(24, "Skald");
            _allClasses.Add(27, "Spiritmaster");
            _allClasses.Add(21, "Thane");
            _allClasses.Add(34, "Valkyrie");
            _allClasses.Add(59, "Warlock");
            _allClasses.Add(22, "Warrior");
            _allClasses.Add(61, "MaulerMid");
            _allClasses.Add(55, "Animist");
            _allClasses.Add(39, "Bainshee");
            _allClasses.Add(48, "Bard");
            _allClasses.Add(43, "Blademaster");
            _allClasses.Add(45, "Champion");
            _allClasses.Add(47, "Druid");
            _allClasses.Add(40, "Eldritch");
            _allClasses.Add(41, "Enchanter");
            _allClasses.Add(44, "Hero");
            _allClasses.Add(42, "Mentalist");
            _allClasses.Add(49, "Nightshade");
            _allClasses.Add(50, "Ranger");
            _allClasses.Add(56, "Valewalker");
            _allClasses.Add(58, "Vampiir");
            _allClasses.Add(46, "Warden");
            _allClasses.Add(62, "MaulerHib");
            _allClasses.Add(16, "Acolyte");
            _allClasses.Add(17, "AlbionRogue");
            _allClasses.Add(20, "Disciple");
            _allClasses.Add(15, "Elementalist");
            _allClasses.Add(14, "Fighter");
            _allClasses.Add(57, "Forester");
            _allClasses.Add(52, "Guardian");
            _allClasses.Add(18, "Mage");
            _allClasses.Add(51, "Magician");
            _allClasses.Add(38, "MidgardRogue");
            _allClasses.Add(36, "Mystic");
            _allClasses.Add(53, "Naturalist");
            _allClasses.Add(37, "Seer");
            _allClasses.Add(54, "Stalker");
            _allClasses.Add(35, "Viking");
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

            void PrepareInput(string input, Dictionary<int, string> dictionary, Control control)
            {
                if (input == null || input.Equals(""))
                {
                    return;
                }

                var splitStepType = input.Split(new[] { "|" }, StringSplitOptions.None);
                dictionary.Clear();
                StringToDictionary(splitStepType, dictionary);
                control.Text = dictionary[0];
            }

            try
            {
                // Step type
                var builder = new StringBuilder(quest.StepType);
                // Start with long wording first to avoid any bad replace
                builder.Replace("200", "RewardQuest");
                builder.Replace("11", "CollectFinish");
                builder.Replace("10", "Collect");
                builder.Replace("0", "Kill");
                builder.Replace("1", "KillFinish");
                builder.Replace("2", "Deliver");
                builder.Replace("3", "DeliverFinish");
                builder.Replace("4", "Interact");
                builder.Replace("5", "InteractFinish");
                builder.Replace("6", "Whisper");
                builder.Replace("7", "WhisperFinish");
                builder.Replace("8", "Search");
                builder.Replace("9", "SearchFinish");
                _stepType = builder.ToString();

                PrepareInput(_stepType, _stepTypeDictionary, StepType);

                // Source name
                _sourceName = quest.SourceName;
                PrepareInput(_sourceName, _srcNameDictionary, SourceName);

                // Target name
                _targetName = quest.TargetName;
                PrepareInput(_targetName, _trgNameDictionary, TargetName);

                // Source text
                _sourceText = quest.SourceText;
                PrepareInput(_sourceText, _srcTextDictionary, SourceText);

                // Step text
                _stepText = quest.StepText;
                PrepareInput(_stepText, _stepTextDictionary, StepText);

                // Advance text
                _advanceText = quest.AdvanceText;
                PrepareInput(_advanceText, _advTextDictionary, AdvanceText);

                // Target text
                _targetText = quest.TargetText;
                PrepareInput(_targetText, _trgTextDictionary, TargetText);

                // Step item templates
                _stepItemTemplates = quest.StepItemTemplates;
                PrepareInput(_stepItemTemplates, _stepItemDictionary, StepItem);

                // Collect item templates
                _collectItemTemplate = quest.CollectItemTemplate;
                PrepareInput(_collectItemTemplate, _colItemDictionary, CollectItem);

                // Option reward
                _optionalRewardItemTemplates = quest.OptionalRewardItemTemplates;
                PrepareInput(_optionalRewardItemTemplates, _optDictionary, _OptionalReward);

                // Final reward
                _finalRewardItemTemplates = quest.FinalRewardItemTemplates;
                PrepareInput(_finalRewardItemTemplates, _finDictionary, _FinalReward);

                // Money
                _rewardMoney = quest.RewardMoney;
                PrepareInput(_rewardMoney, _moneyDictionary, RewardMoney);

                // XP
                _rewardXp = quest.RewardXP;
                PrepareInput(_rewardXp, _xpDictionary, RewardXp);

                // ClXp
                _rewardClXp = quest.RewardCLXP;
                PrepareInput(_rewardClXp, _clXpDictionary, RewardCLXp);

                // RP
                _rewardRp = quest.RewardRP;
                PrepareInput(_rewardRp, _rpDictionary, RewardRp);

                // BP
                _rewardBp = quest.RewardBP;
                PrepareInput(_rewardBp, _bpDictionary, RewardBp);

                // Allowed classes
                _allowedClasses = quest.AllowedClasses;

                if (!string.IsNullOrWhiteSpace(_allowedClasses))
                {
                    var splitAllowedClasses = _allowedClasses.Split(new[] { "|" }, StringSplitOptions.None);

                    for (var i = 0; i < splitAllowedClasses.Length; i++)
                    {
                        int.TryParse(splitAllowedClasses[i], out int result);
                        splitAllowedClasses[i] = _allClasses[result];
                    }

                    foreach (var allowedClass in splitAllowedClasses)
                    {
                        for (var j = 0; j < listClasses.Items.Count; j++)
                        {
                            var cls = listClasses.Items[j].ToString();
                            if (cls != allowedClass) continue;
                            listClasses.SetSelected(j, true);
                        }
                    }
                }

                LoadedQuest = true;
            }
            catch (Exception g)
            {
                MessageBox.Show(
                    g.Message,
                    @"Error while deserializing data! Quest was not loaded completely - Errors in database format.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var objectSearch = new ObjectSearch();
            objectSearch.SelectNpcClicked += (o, args) => { LoadTargetNpc(((WorldObject)o).Name, ((WorldObject)o).Region); };
            objectSearch.ShowDialog(this);
        }

        private void questDelete_Click(object sender, EventArgs e)
        {
            if (_quest == null)
            {
                return;
            }

            var confirmResult = MessageBox.Show(
                @"This will clear the form. Are you sure you want to continue? (If this quest is in the database it will NOT be deleted)",
                @"Confirm Delete!!",
                MessageBoxButtons.YesNo
            );

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            LoadedQuest = false;

            stepNumber.Text = @"1";
            CleanDictionaries();
            Clear();
        }

        private void CleanDictionaries()
        {
            _optDictionary.Clear();
            _finDictionary.Clear();
            _advTextDictionary.Clear();
            _colItemDictionary.Clear();
            _moneyDictionary.Clear();
            _xpDictionary.Clear();
            _clXpDictionary.Clear();
            _rpDictionary.Clear();
            _bpDictionary.Clear();
            _srcTextDictionary.Clear();
            _srcNameDictionary.Clear();
            _stepItemDictionary.Clear();
            _stepTextDictionary.Clear();
            _trgNameDictionary.Clear();
            _trgTextDictionary.Clear();
            _stepTypeDictionary.Clear();
        }

        private void collectSelect_Click(object sender, EventArgs e)
        {
            CollectItem.Text = ItemTemplateBox.Text;
        }
        //Step data forward
        private void stepForward_Click(object sender, EventArgs e)
        {
            var stepNum = int.Parse(stepNumber.Text);

            if (StepType.Text == "" || TargetName.Text == "")
            {
                MessageBox.Show(@"You cannot proceed until you have selected a step type and target!");
                return;
            }

            RefreshDictionaries(stepNum);

            AdvanceText.Text = "";
            CollectItem.Text = "";
            RewardMoney.Text = "";
            RewardXp.Text = "";
            RewardCLXp.Text = "";
            RewardRp.Text = "";
            RewardBp.Text = "";
            SourceText.Text = "";
            SourceText.Text = "";
            StepItem.Text = "";
            StepText.Text = "";
            TargetName.Text = "";
            TargetText.Text = "";
            StepType.Text = "";

            stepNumber.Text = (stepNum + 1).ToString(); //increment label

            var stepNext = int.Parse(stepNumber.Text);
            SetStepValues(stepNext);
        }

        //Step data back
        private void stepBack_Click(object sender, EventArgs e)
        {
            var stepNum = int.Parse(stepNumber.Text);
            if (stepNum == 1) //return if already at step 1, there ain't no step 0
            {
                return;
            }

            //remove step altogether if mandatory fields are not entered
            if (TargetName.Text == "" || StepType.Text == "")
            {
                _advTextDictionary.Remove(stepNum - 1);
                _colItemDictionary.Remove(stepNum - 1);
                _moneyDictionary.Remove(stepNum - 1);
                _xpDictionary.Remove(stepNum - 1);
                _clXpDictionary.Remove(stepNum - 1);
                _rpDictionary.Remove(stepNum - 1);
                _bpDictionary.Remove(stepNum - 1);
                _srcTextDictionary.Remove(stepNum - 1);
                _srcNameDictionary.Remove(stepNum - 1);
                _stepItemDictionary.Remove(stepNum - 1);
                _stepTextDictionary.Remove(stepNum - 1);
                _trgNameDictionary.Remove(stepNum - 1);
                _trgTextDictionary.Remove(stepNum - 1);
                _stepTypeDictionary.Remove(stepNum - 1);
            }
            else
            //Needed to commit data to m_dictionary when the back button is clicked
            {
                RefreshDictionaries(stepNum);
            }

            stepNumber.Text = (stepNum - 1).ToString();
            stepNum--;

            //Previous step data check
            SetStepValues(stepNum);
        }

        private void SetStepValues(int stepNum)
        {
            SetStepValue(stepNum, _advTextDictionary, AdvanceText);
            SetStepValue(stepNum, _colItemDictionary, CollectItem);
            SetStepValue(stepNum, _moneyDictionary, RewardMoney, defaultEmpty: false);
            SetStepValue(stepNum, _xpDictionary, RewardXp, defaultEmpty: false);
            SetStepValue(stepNum, _clXpDictionary, RewardCLXp, defaultEmpty: false);
            SetStepValue(stepNum, _rpDictionary, RewardRp, defaultEmpty: false);
            SetStepValue(stepNum, _bpDictionary, RewardBp, defaultEmpty: false);
            SetStepValue(stepNum, _srcTextDictionary, SourceText);
            SetStepValue(stepNum, _srcNameDictionary, SourceName);
            SetStepValue(stepNum, _stepItemDictionary, StepItem);
            SetStepValue(stepNum, _stepTextDictionary, StepText);
            SetStepValue(stepNum, _trgNameDictionary, TargetName);
            SetStepValue(stepNum, _trgTextDictionary, TargetText);
            SetStepValue(stepNum, _stepTypeDictionary, StepType);
        }

        private static void SetStepValue(int stepNum, Dictionary<int, string> dictionary, Control textBox, bool defaultEmpty = true)
        {
            textBox.Text = dictionary.TryGetValue(stepNum - 1, out var value) ? value : (defaultEmpty ? "" : "0");
        }

        private void questSave_Click(object sender, EventArgs e)
        {
            var stepNum = int.Parse(stepNumber.Text);
            var optNum = int.Parse(optNumber.Text);
            var finNum = int.Parse(finNumber.Text);

            // Refresh step data to the dictionary if the forward/back button has not been pressed
            RefreshDictionaries(stepNum);

            _optDictionary.Remove(optNum - 1); // do this in case it was edited without pressing forward/back
            if (!string.IsNullOrWhiteSpace(_OptionalReward.Text))
            {
                _optDictionary.Add(optNum - 1, _OptionalReward.Text);
            }

            _finDictionary.Remove(finNum - 1); // do this in case it was edited without pressing forward/back
            if (!string.IsNullOrWhiteSpace(_FinalReward.Text))
            {
                _finDictionary.Add(finNum - 1, _FinalReward.Text);
            }

            try
            {
                _optionalRewardItemTemplates = string.Join("|", Array.ConvertAll(_optDictionary.Values.ToArray(), i => i.ToString()));
                _finalRewardItemTemplates = string.Join("|", Array.ConvertAll(_finDictionary.Values.ToArray(), i => i.ToString()));
                _advanceText = string.Join("|", Array.ConvertAll(_advTextDictionary.Values.ToArray(), i => i.ToString()));
                _collectItemTemplate = string.Join("|", Array.ConvertAll(_colItemDictionary.Values.ToArray(), i => i.ToString()));
                _rewardMoney = string.Join("|", Array.ConvertAll(_moneyDictionary.Values.ToArray(), i => i.ToString()));
                _rewardXp = string.Join("|", Array.ConvertAll(_xpDictionary.Values.ToArray(), i => i.ToString()));
                _rewardClXp = string.Join("|", Array.ConvertAll(_clXpDictionary.Values.ToArray(), i => i.ToString()));
                _rewardRp = string.Join("|", Array.ConvertAll(_rpDictionary.Values.ToArray(), i => i.ToString()));
                _rewardBp = string.Join("|", Array.ConvertAll(_bpDictionary.Values.ToArray(), i => i.ToString()));
                _sourceText = string.Join("|", Array.ConvertAll(_srcTextDictionary.Values.ToArray(), i => i.ToString()));
                _sourceName = string.Join("|", Array.ConvertAll(_srcNameDictionary.Values.ToArray(), i => i.ToString()));
                _stepItemTemplates = string.Join("|", Array.ConvertAll(_stepItemDictionary.Values.ToArray(), i => i.ToString()));
                _stepText = string.Join("|", Array.ConvertAll(_stepTextDictionary.Values.ToArray(), i => i.ToString()));
                _targetName = string.Join("|", Array.ConvertAll(_trgNameDictionary.Values.ToArray(), i => i.ToString()));
                _targetText = string.Join("|", Array.ConvertAll(_trgTextDictionary.Values.ToArray(), i => i.ToString()));
                _stepType = string.Join("|", Array.ConvertAll(_stepTypeDictionary.Values.ToArray(), i => i.ToString()));
                _allowedClasses = string.Join("|", listClasses.SelectedItems.Cast<object>().Select(i => i.ToString()));

                //eStepType string replace values:
                StringBuilder stype = new StringBuilder(_stepType);
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
                _stepType = stype.ToString();

                StringBuilder allcl = new StringBuilder(_allowedClasses);
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
                _allowedClasses = allcl.ToString();
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DBDataQuest q = new DBDataQuest();

            // Before saving check for missing or wrong information in the quest
            if (CheckQuestOk() == false)
            {
                MessageBox.Show(
                    $"Quest data is invalid, please fix the below fields : \n\n{string.Join("\n", _questErrors)}",
                    @"Error on quest",
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

            var item = (ComboboxService.SelectItemModel) _StartType.Items[_StartType.SelectedIndex];
            q.StartType = Convert.ToByte(item.Id);
            q.StartName = _StartName.Text;
            q.StartRegionID = ushort.Parse(_StartRegionID.Text);
            q.AcceptText = _AcceptText.Text;
            q.Description = _Description.Text;
            q.SourceText = _sourceText; //serialized
            q.SourceName = _sourceName;
            q.StepType = _stepType; //serialized
            q.StepText = _stepText; //serialized
            q.StepItemTemplates = _stepItemTemplates; //serialized
            q.AdvanceText = _advanceText; //serialized
            q.TargetName = _targetName; //serialized
            q.TargetText = _targetText; //serialized
            q.CollectItemTemplate = _collectItemTemplate; //serialized
            q.MaxCount = byte.Parse(_MaxCount.Text);
            q.MinLevel = byte.Parse(_MinLevel.Text);
            q.MaxLevel = byte.Parse(_MaxLevel.Text);
            q.RewardMoney = _rewardMoney; //serialized
            q.RewardXP = _rewardXp; //serialized
            q.RewardCLXP = _rewardClXp; //serialized
            q.RewardRP = _rewardRp; //serialized
            q.RewardBP = _rewardBp; //serialized
            q.OptionalRewardItemTemplates = _optionalRewardItemTemplates;
            q.FinalRewardItemTemplates = _finalRewardItemTemplates;
            q.FinishText = _FinishText.Text;
            q.QuestDependency = _QuestDependency.Text; //might need to serialize....if quest has multiple dependencies
            q.AllowedClasses = _allowedClasses; //serialized
            q.ClassType = _ClassType.Text;

            try
            { // This is probably a bad way to do this, but i can't get the quest to save onto the same ID
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
                MessageBox.Show(@"Quest successfully saved!", "", MessageBoxButtons.OK);
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message, @"Error saving data!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDictionaries(int stepNum)
        {
            _advTextDictionary[stepNum -1] = AdvanceText.Text;
            _colItemDictionary[stepNum -1] = CollectItem.Text;
            _moneyDictionary[stepNum -1] = RewardMoney.Text;
            _xpDictionary[stepNum -1] = RewardXp.Text;
            _clXpDictionary[stepNum -1] = RewardCLXp.Text;
            _rpDictionary[stepNum -1] = RewardRp.Text;
            _bpDictionary[stepNum -1] = RewardBp.Text;
            _srcTextDictionary[stepNum -1] = SourceText.Text;
            _srcNameDictionary[stepNum -1] = SourceName.Text;
            _stepItemDictionary[stepNum -1] = StepItem.Text;
            _stepTextDictionary[stepNum -1] = StepText.Text;
            _trgNameDictionary[stepNum -1] = TargetName.Text;
            _trgTextDictionary[stepNum -1] = TargetText.Text;
            _stepTypeDictionary[stepNum - 1] = StepType.Text;
        }

        private bool CheckQuestOk()
        {
            var errorRaised = false;
            _questErrors.Clear();

            var item = (ComboboxService.SelectItemModel)_StartType.Items[_StartType.SelectedIndex];

            void ValidateString(Control input, string message)
            {
                if (!string.IsNullOrWhiteSpace(input.Text))
                {
                    return;
                }

                errorRaised = true;
                _questErrors.Add($"{message} must be defined");
            }

            // Quest type undefined
            if (item.Id == null)
            {
                errorRaised = true;
                _questErrors.Add("Start type is invalid");
            }

            // Quest name
            ValidateString(_Name, "Quest name");
            ValidateString(_MinLevel, "Min level");
            ValidateString(_MaxLevel, "Max level");
            ValidateString(_MaxCount, "Max count");
            ValidateString(_StartName, "Start NPC name");
            ValidateString(_StartRegionID, "Start NPC region id");

            return !errorRaised;
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

        private void DataQuestControl_Load_1(object sender, EventArgs e)
        {
            SetupDropdowns();
        }

        private void LoadTargetNpc(string mobName, ushort mobRegion)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }
            TargetName.Text = $@"{mobName};{mobRegion}";

        }

        private void LoadItem(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return;
            }

            ItemTemplateBox.Text = itemId;
        }

        private void LoadStartNpc(string mobName, ushort mobRegion)
        {
            if (string.IsNullOrWhiteSpace(mobName))
            {
                return;
            }

            _StartName.Text = mobName;
            _StartRegionID.Text = mobRegion.ToString();
        }
    }
}