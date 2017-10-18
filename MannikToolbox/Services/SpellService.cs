using DOL.Database;

namespace MannikToolbox.Services
{
    public class SpellService
    {
        public DBSpell GetSpell(string spellName)
        {
            return DatabaseManager.Database.FindObjectByKey<DBSpell>(spellName);
        }

        public void SaveSpell(DBSpell spell)
        {
            if (string.IsNullOrWhiteSpace(spell.Name))
            {
                DatabaseManager.Database.AddObject(spell);
                return;
            }

            DatabaseManager.Database.SaveObject(spell);
        }
    }
}