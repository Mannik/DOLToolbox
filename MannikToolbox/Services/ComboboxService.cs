using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DOL.Database;

namespace MannikToolbox.Services
{
    public static class ComboboxService
    {
       public static void BindRaces(ComboBox input)
        {
            var races = new List<SelectItemModel>
            {
                new SelectItemModel(0, "Unknown"),
                new SelectItemModel(1, "Briton"),
                new SelectItemModel(2, "Avalonian"),
                new SelectItemModel(3, "Highlander"),
                new SelectItemModel(4, "Saracen"),
                new SelectItemModel(5, "Norseman"),
                new SelectItemModel(6, "Troll"),
                new SelectItemModel(7, "Dwarf"),
                new SelectItemModel(8, "Kobold"),
                new SelectItemModel(9, "Celt"),
                new SelectItemModel(10, "Firbolg"),
                new SelectItemModel(11, "Elf"),
                new SelectItemModel(12, "Lurikeen"),
                new SelectItemModel(13, "Inconnu"),
                new SelectItemModel(14, "Valkyn"),
                new SelectItemModel(15, "Sylvan"),
                new SelectItemModel(15, "HalfOgre"),
                new SelectItemModel(15, "Frostalf"),
                new SelectItemModel(17, "Shar"),
                new SelectItemModel(19, "AlbionMinotaur"),
                new SelectItemModel(20, "MidgardMinotaur"),
                new SelectItemModel(21, "HiberniaMinotaur"),
                new SelectItemModel(19, "Korazh"),
                new SelectItemModel(20, "Deifrang"),
                new SelectItemModel(21, "Graoch")
            };
            BindData(input, races);
        }
        
        public static void BindRegions(ComboBox input)
        {
            var items = DatabaseManager.Database.SelectAllObjects<DBRegions>()
                .OrderBy(x => x.Description)
                .Select(x => new SelectItemModel
                {
                    Id = x.RegionID,
                    Value = x.Description
                })
                .ToList();

            BindData(input, items);
        }

        public static void BindRealms(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "None"),
                new SelectItemModel(1, "Albion"),
                new SelectItemModel(2, "Midgard"),
                new SelectItemModel(3, "Hibernia"),
                new SelectItemModel(6, "Door")
            };

            BindData(input, items);
        }

        public static void BindGenders(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "Neutral"),
                new SelectItemModel(1, "Male"),
                new SelectItemModel(2, "Female")
            };

            BindData(input, items);
        }

        public static void BindWeaponDamageTypes(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "Elemental"),
                new SelectItemModel(1, "Crush"),
                new SelectItemModel(2, "Slash"),
                new SelectItemModel(3, "Thrust"),
                new SelectItemModel(10, "Body"),
                new SelectItemModel(11, "Cold"),
                new SelectItemModel(12, "Energy"),
                new SelectItemModel(13, "Heat"),
                new SelectItemModel(14, "Matter"),
                new SelectItemModel(15, "Spirit")
            };

            BindData(input, items);
        }

        public static void BindBodyTypes(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "None"),
                new SelectItemModel(1, "Animal"),
                new SelectItemModel(2, "Demon"),
                new SelectItemModel(3, "Dragon"),
                new SelectItemModel(4, "Elemental"),
                new SelectItemModel(5, "Giant"),
                new SelectItemModel(6, "Humanoid"),
                new SelectItemModel(7, "Insect"),
                new SelectItemModel(8, "Magical"),
                new SelectItemModel(9, "Reptile"),
                new SelectItemModel(10, "Plant"),
                new SelectItemModel(11, "Undead")
            };

            BindData(input, items);
        }

        private static void BindData(ComboBox input, List<SelectItemModel> data)
        {
            if (data.All(x => x.Id != null))
            {
                data.Insert(0, new SelectItemModel(null, "Undefined"));
            }

            input.DataSource = new BindingSource(data, null);
            input.DisplayMember = "Value";
            input.ValueMember = "Id";
        }

        public class SelectItemModel
        {
            public SelectItemModel() { }

            public SelectItemModel(int? id, string value)
            {
                Id = id;
                Value = value;
            }

            public int? Id { get; set; }
            public string Value { get; set; }
        }

        public static void BindTargets(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "Area"),
                new SelectItemModel(1, "Cone"),
                new SelectItemModel(2, "Controlled"),
                new SelectItemModel(3, "Corpse"),
                new SelectItemModel(4, "Enemy"),
                new SelectItemModel(5, "Group"),
                new SelectItemModel(6, "KeepComponent"),
                new SelectItemModel(7, "Pet"),
                new SelectItemModel(8, "Realm"),
                new SelectItemModel(9, "Self"),
            };

            BindData(input, items);
        }

        public static void BindInstrumentRequirements(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "None"),
                new SelectItemModel(1, "Drum"),
                new SelectItemModel(2, "Lute"),
                new SelectItemModel(3, "Flute"),
                new SelectItemModel(4, "Harp"),

            };

            BindData(input, items);
        }
    }
}
