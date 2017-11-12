using System.Collections.Generic;
using DOL.Database;

namespace DOLToolbox.Models
{
    public class LootTemplateModel
    {
        public MobXLootTemplate MobXLootTemplate { get; set; }
        public List<LootTemplate> LootTemplates { get; set; }
    }
}