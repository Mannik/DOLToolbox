using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DOL.Database;

namespace DOLToolbox.Services
{
    public static class ComboboxService
    {
        public static void BindPlayerRaces(ComboBox input)
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
      

        public static async Task BindMobRaces(ComboBox input)
        {
            List<SelectItemModel> races = new List<SelectItemModel>();

            await Task.Run(() =>
            {
                races = DatabaseManager.Database.SelectAllObjects<Race>()
                    .Where(x => x.ID > 21)
                    .Select(x => new SelectItemModel
                    {
                        Id = x.ID,
                        Value = x.Name
                    })
                    .ToList();
            });

            BindData(input, races);
        }

        public static void BindRegions(ComboBox input)
        {
            var zones = new List<SelectItemModel>
            {
                new SelectItemModel(1, "Albion"),
                new SelectItemModel(2, "Albion Housing"),
                new SelectItemModel(10, "Camelot City"),
                new SelectItemModel(20, "Stonehenge Barrows"),
                new SelectItemModel(21, "Tomb of Mithra"),
                new SelectItemModel(22, "Keltoi Fogou"),
                new SelectItemModel(23, "Catacombs of Cardova"),
                new SelectItemModel(24, "Tepok's Mine"),
                new SelectItemModel(27, "Tutorial"),
                new SelectItemModel(30, "Atlantis"),
                new SelectItemModel(35, "Cetus' Pit"),
                new SelectItemModel(36, "Sobekite Eternal"),
                new SelectItemModel(37, "Temple of Twilight"),
                new SelectItemModel(40, "Halls of Ma'ati"),
                new SelectItemModel(45, "The Great Pyramid"),
                new SelectItemModel(46, "Deep Volcanus"),
                new SelectItemModel(47, "Aerus City"),
                new SelectItemModel(48, "The Unused Mine"),
                new SelectItemModel(49, "Rugnog's Haven"),
                new SelectItemModel(50, "Avalon City"),
                new SelectItemModel(51, "Avalon"),
                new SelectItemModel(58, "Underground Forest"),
                new SelectItemModel(59, "Glashtin Forge"),
                new SelectItemModel(60, "Caer Sidi"),
                new SelectItemModel(61, "Krondon"),
                new SelectItemModel(62, "Crystal Cave"),
                new SelectItemModel(63, "Roman Aqueducts"),
                new SelectItemModel(65, "Inconnu Crypt"),
                new SelectItemModel(66, "Underground Forest"),
                new SelectItemModel(67, "Deadlands of Annwn"),
                new SelectItemModel(68, "Lower Crypt"),
                new SelectItemModel(69, "Darkspire"),
                new SelectItemModel(70, "Ruins of Atlantis"),
                new SelectItemModel(71, "Ruinrar Atlantis"),
                new SelectItemModel(72, "Scrios de Atlantis"),
                new SelectItemModel(73, "Atlantis"),
                new SelectItemModel(78, "Cetus' Pit"),
                new SelectItemModel(79, "Sobekite Eternal"),
                new SelectItemModel(80, "Temple of Twilight"),
                new SelectItemModel(83, "Halls of Ma'ati"),
                new SelectItemModel(88, "The Great Pyramid"),
                new SelectItemModel(89, "Deep Volcanus"),
                new SelectItemModel(90, "Aerus City"),
                new SelectItemModel(91, "Celestius"),
                new SelectItemModel(92, "Veil Rift"),
                new SelectItemModel(93, "Shar Labyrinth"),
                new SelectItemModel(94, "Queen's Labyrinth"),
                new SelectItemModel(95, "The Frontlines"),
                new SelectItemModel(96, "Underground Forest"),
                new SelectItemModel(97, "Deadlands of Annwn"),
                new SelectItemModel(98, "Darkspire"),
                new SelectItemModel(99, "Glashtin Forge"),
                new SelectItemModel(100, "Midgard"),
                new SelectItemModel(101, "Jordheim"),
                new SelectItemModel(102, "Midgard Housing"),
                new SelectItemModel(109, "The Frontlines"),
                new SelectItemModel(123, "Volcanus Instance 1"),
                new SelectItemModel(124, "Volcanus Instance 2"),
                new SelectItemModel(125, "Spindelhalla"),
                new SelectItemModel(126, "Vendo Caverns"),
                new SelectItemModel(127, "Varulvhamn"),
                new SelectItemModel(128, "Cursed Tomb"),
                new SelectItemModel(129, "Nisse's Lair"),
                new SelectItemModel(130, "Atlantis"),
                new SelectItemModel(135, "Cetus' Pit"),
                new SelectItemModel(136, "Sobekite Eternal"),
                new SelectItemModel(137, "Temple of Twilight"),
                new SelectItemModel(140, "Halls of Ma'ati"),
                new SelectItemModel(145, "The Great Pyramid"),
                new SelectItemModel(146, "Deep Volcanus"),
                new SelectItemModel(147, "Aerus City"),
                new SelectItemModel(148, "The Frontlines"),
                new SelectItemModel(149, "Nyttheim"),
                new SelectItemModel(150, "Trollheim"),
                new SelectItemModel(151, "Aegir"),
                new SelectItemModel(160, "Tuscaran Glacier"),
                new SelectItemModel(161, "Iarnvidiur's Lair"),
                new SelectItemModel(162, "Deadlands of Annwn"),
                new SelectItemModel(163, "New Frontiers"),
                new SelectItemModel(165, "Cathal Valley"),
                new SelectItemModel(180, "Fomor City"),
                new SelectItemModel(181, "HyBrasil"),
                new SelectItemModel(188, "Darkspire"),
                new SelectItemModel(189, "Glashtin Forge"),
                new SelectItemModel(190, "Tur Suil"),
                new SelectItemModel(191, "Galladoria"),
                new SelectItemModel(192, "Le Sanctuaire Du Haut Consule"),
                new SelectItemModel(193, "Le Sanctuaire Du Haut Consule"),
                new SelectItemModel(194, "Le Sanctuaire Du Haut Consule"),
                new SelectItemModel(195, "The Otherworld"),
                new SelectItemModel(196, "The Otherworld"),
                new SelectItemModel(197, "The Otherworld"),
                new SelectItemModel(198, "Volcanus Instance 3"),
                new SelectItemModel(199, "Volcanus Instance 4"),
                new SelectItemModel(200, "Hibernia"),
                new SelectItemModel(201, "Tir na Nog"),
                new SelectItemModel(202, "Hibernia Housing"),
                new SelectItemModel(220, "Coruscating Mine"),
                new SelectItemModel(221, "Muire Tomb"),
                new SelectItemModel(222, "Spraggon Den"),
                new SelectItemModel(223, "Koalinth Tribal Caverns"),
                new SelectItemModel(224, "Treibh Caillte"),
                new SelectItemModel(226, "Abandoned Mines"),
                new SelectItemModel(227, "Abandoned Mines"),
                new SelectItemModel(228, "Abandoned Mines"),
                new SelectItemModel(229, "Burial Grounds"),
                new SelectItemModel(230, ""),
                new SelectItemModel(231, "Art Test Zone"),
                new SelectItemModel(232, "- Art Test Dungeon"),
                new SelectItemModel(233, "Summoner's Hall"),
                new SelectItemModel(234, "The Proving Grounds"),
                new SelectItemModel(235, "The Lion's Den"),
                new SelectItemModel(236, "The Hills of Claret"),
                new SelectItemModel(237, "Killaloe"),
                new SelectItemModel(238, "Thidranki"),
                new SelectItemModel(239, "Braemar"),
                new SelectItemModel(240, "Wilton"),
                new SelectItemModel(241, "Molvik"),
                new SelectItemModel(242, "Leirvik"),
                new SelectItemModel(243, "Kobold Undercity"),
                new SelectItemModel(244, "Passage of Conflict"),
                new SelectItemModel(245, "Labyrinth"),
                new SelectItemModel(247, ""),
                new SelectItemModel(249, "Darkness Falls"),
                new SelectItemModel(256, "Forgotten Mines"),
                new SelectItemModel(257, "Forgotten Mines"),
                new SelectItemModel(258, "Forgotten Mines"),
                new SelectItemModel(278, "Damp Cavern"),
                new SelectItemModel(279, "Damp Cavern"),
                new SelectItemModel(280, "Damp Cavern"),
                new SelectItemModel(281, "Forgotten Sepulchre"),
                new SelectItemModel(282, "Forgotten Sepulchre"),
                new SelectItemModel(283, "Forgotten Sepulchre"),
                new SelectItemModel(284, "The Concealed Guardhouse"),
                new SelectItemModel(285, "The Concealed Guardhouse"),
                new SelectItemModel(286, "The Concealed Guardhouse"),
                new SelectItemModel(287, "The Gossamer Grotto"),
                new SelectItemModel(288, "The Gossamer Grotto"),
                new SelectItemModel(289, "The Gossamer Grotto"),
                new SelectItemModel(290, "Underground Tunnel"),
                new SelectItemModel(291, "Underground Tunnel"),
                new SelectItemModel(292, "Underground Tunnel"),
                new SelectItemModel(293, "Burial Tomb"),
                new SelectItemModel(294, "Burial Tomb"),
                new SelectItemModel(295, "Burial Tomb"),
                new SelectItemModel(296, "Desecrated Grounds"),
                new SelectItemModel(297, "Desecrated Grounds"),
                new SelectItemModel(298, "Desecrated Grounds"),
                new SelectItemModel(300, "Damp Cavern"),
                new SelectItemModel(301, "Damp Cavern"),
                new SelectItemModel(302, "Damp Cavern"),
                new SelectItemModel(303, "Damp Cavern"),
                new SelectItemModel(304, "Damp Cavern"),
                new SelectItemModel(305, "Forgotten Sepulchre"),
                new SelectItemModel(306, "Forgotten Sepulchre"),
                new SelectItemModel(307, "Forgotten Sepulchre"),
                new SelectItemModel(308, "Forgotten Sepulchre"),
                new SelectItemModel(309, "Forgotten Sepulchre"),
                new SelectItemModel(310, "The Concealed Guardhouse"),
                new SelectItemModel(311, "The Concealed Guardhouse"),
                new SelectItemModel(312, "The Concealed Guardhouse"),
                new SelectItemModel(313, "The Concealed Guardhouse"),
                new SelectItemModel(314, "The Concealed Guardhouse"),
                new SelectItemModel(315, "The Gossamer Grotto"),
                new SelectItemModel(316, "The Gossamer Grotto"),
                new SelectItemModel(317, "The Gossamer Grotto"),
                new SelectItemModel(318, "The Gossamer Grotto"),
                new SelectItemModel(319, "The Gossamer Grotto"),
                new SelectItemModel(320, "Underground Tunnel"),
                new SelectItemModel(321, "Underground Tunnel"),
                new SelectItemModel(322, "Underground Tunnel"),
                new SelectItemModel(323, "Underground Tunnel"),
                new SelectItemModel(324, "Underground Tunnel"),
                new SelectItemModel(325, "The Forgotten Tunnels"),
                new SelectItemModel(326, "The Hidden Lair"),
                new SelectItemModel(327, "Caverns of Madness"),
                new SelectItemModel(328, "The Hidden Lair"),
                new SelectItemModel(329, "Jarlsberg's Hideout"),
                new SelectItemModel(330, "The Foothills of Albion"),
                new SelectItemModel(331, "Burial AW4"),
                new SelectItemModel(332, "Rebel Half Orc Lair"),
                new SelectItemModel(333, "The Haunted Halls"),
                new SelectItemModel(334, "The Foothills of Midgard"),
                new SelectItemModel(335, "Doiri Ban"),
                new SelectItemModel(336, "Demonic Prison"),
                new SelectItemModel(337, "Demonic Prison"),
                new SelectItemModel(338, "Demonic Prison"),
                new SelectItemModel(339, "Brimstone Caverns"),
                new SelectItemModel(340, "Halls of Helgardh"),
                new SelectItemModel(341, "Cave of Cruachan"),
                new SelectItemModel(342, "Mid Launch 2"),
                new SelectItemModel(343, "Alb Launch 2"),
                new SelectItemModel(344, "Hib Launch 2"),
                new SelectItemModel(345, "Tomte Prison"),
                new SelectItemModel(346, "Nisse's Retreat"),
                new SelectItemModel(347, "Lair of the Demon Lord"),
                new SelectItemModel(348, "The Baron's Gaol"),
                new SelectItemModel(349, "Lios's Eternal Rest"),
                new SelectItemModel(350, "Liche's Unrest"),
                new SelectItemModel(351, "Nephraal's Gaol"),
                new SelectItemModel(352, "Morfesa's Gaol"),
                new SelectItemModel(353, "Den of Bones"),
                new SelectItemModel(354, "The Lost Burrow"),
                new SelectItemModel(355, "Tethra's Stronghold"),
                new SelectItemModel(356, "Barrow of Restless Dead"),
                new SelectItemModel(357, "The Cursed Lair"),
                new SelectItemModel(358, "The Burrow"),
                new SelectItemModel(359, "Red Dagger Hideout"),
                new SelectItemModel(360, "King Eirik's Throne Room"),
                new SelectItemModel(361, "Arachnid's Labyrinth"),
                new SelectItemModel(362, "The Master's Lair"),
                new SelectItemModel(363, "Anataeus' Sanctuary"),
                new SelectItemModel(364, "Keeper Iraeda's Sanctuary"),
                new SelectItemModel(365, "Bandit Lair"),
                new SelectItemModel(366, "Gnoll Lair"),
                new SelectItemModel(367, "Vandr's Bane"),
                new SelectItemModel(368, "Past and Present"),
                new SelectItemModel(369, "Half-Orc Camp"),
                new SelectItemModel(370, "Brutal Realization"),
                new SelectItemModel(371, "History Repeats Itself"),
                new SelectItemModel(372, "Hounds of Arawn"),
                new SelectItemModel(373, "Passage of Echoes"),
                new SelectItemModel(374, "The Unrestful Tomb"),
                new SelectItemModel(375, "The Lost Passages"),
                new SelectItemModel(376, "Goblin's Cookery"),
                new SelectItemModel(377, "The Beastmaster's Den"),
                new SelectItemModel(378, "Keeper Garran's Sanctuary"),
                new SelectItemModel(379, "The Funerary Hall"),
                new SelectItemModel(380, "Lair of Doom"),
                new SelectItemModel(381, "The Arena"),
                new SelectItemModel(382, "The Funerary Hall"),
                new SelectItemModel(383, "The Funerary Hall"),
                new SelectItemModel(384, "The Ritual Hall"),
                new SelectItemModel(385, "The Betrayer's Den"),
                new SelectItemModel(386, "The Sundered Tombs"),
                new SelectItemModel(387, "The Sundered Tombs"),
                new SelectItemModel(388, "The Sundered Tombs"),
                new SelectItemModel(389, "Shafts of the Tenebrae"),
                new SelectItemModel(390, "Lair of the Exiled"),
                new SelectItemModel(391, "La Tour Des Arcanes ALB"),
                new SelectItemModel(392, "The Hall of Reawakening"),
                new SelectItemModel(393, "The Hidden Crypt"),
                new SelectItemModel(394, "King Constantine's Throne Room"),
                new SelectItemModel(395, "King Lamfhotas' Throne Room"),
                new SelectItemModel(396, "The Depths of Despair"),
                new SelectItemModel(397, "Realm of the Damned"),
                new SelectItemModel(398, "La Tour des Arcanes MID"),
                new SelectItemModel(399, "La Tour des Arcanes HIB"),
                new SelectItemModel(400, "Burial Tomb"),
                new SelectItemModel(401, "Burial Tomb"),
                new SelectItemModel(402, "Burial Tomb"),
                new SelectItemModel(403, "Burial Tomb"),
                new SelectItemModel(404, "Burial Tomb"),
                new SelectItemModel(405, "Desecrated Grounds"),
                new SelectItemModel(406, "Desecrated Grounds"),
                new SelectItemModel(407, "Desecrated Grounds"),
                new SelectItemModel(408, "Desecrated Grounds"),
                new SelectItemModel(409, "Desecrated Grounds"),
                new SelectItemModel(410, "Forgotten Mines"),
                new SelectItemModel(411, "Forgotten Mines"),
                new SelectItemModel(412, "Forgotten Mines"),
                new SelectItemModel(413, "Forgotten Mines"),
                new SelectItemModel(414, "Forgotten Mines"),
                new SelectItemModel(415, "The Funerary Hall"),
                new SelectItemModel(416, "The Funerary Hall"),
                new SelectItemModel(417, "The Funerary Hall"),
                new SelectItemModel(418, "The Funerary Hall"),
                new SelectItemModel(419, "The Funerary Hall"),
                new SelectItemModel(420, "The Sundered Tombs"),
                new SelectItemModel(421, "The Sundered Tombs"),
                new SelectItemModel(422, "The Sundered Tombs"),
                new SelectItemModel(423, "The Sundered Tombs"),
                new SelectItemModel(424, "The Sundered Tombs"),
                new SelectItemModel(425, "The Lost Wing"),
                new SelectItemModel(426, "Deliah's Sanctuary"),
                new SelectItemModel(427, "The Cursed Barrow"),
                new SelectItemModel(428, "The Cursed Barrow"),
                new SelectItemModel(429, "Snyblem's Lair"),
                new SelectItemModel(430, "The Plutonian Shore"),
                new SelectItemModel(431, "The Cursed Barrow"),
                new SelectItemModel(432, "Dismal Grotto"),
                new SelectItemModel(433, "Snarg's Grotto"),
                new SelectItemModel(434, "The Maze of Tribulation"),
                new SelectItemModel(435, "The Smelting Pot"),
                new SelectItemModel(436, "Forges of Flame"),
                new SelectItemModel(437, "Hugak's Smithy"),
                new SelectItemModel(438, "The Goblin Workshop"),
                new SelectItemModel(439, "Wolak's Crucible"),
                new SelectItemModel(440, "Marik's Workroom"),
                new SelectItemModel(441, "Dismal Grotto"),
                new SelectItemModel(442, "The Steward's Crypt"),
                new SelectItemModel(443, "Serf's Folly"),
                new SelectItemModel(444, "Dismal Grotto"),
                new SelectItemModel(445, "The Dark Caverns"),
                new SelectItemModel(446, "Blathnait's Refuge"),
                new SelectItemModel(447, "Broken Mirrors"),
                new SelectItemModel(448, "The Dark Caverns"),
                new SelectItemModel(449, "The Dark Caverns"),
                new SelectItemModel(450, "The Cursed Barrow"),
                new SelectItemModel(451, "The Cursed Barrow"),
                new SelectItemModel(452, "The Cursed Barrow"),
                new SelectItemModel(453, "The Cursed Barrow"),
                new SelectItemModel(454, "The Cursed Barrow"),
                new SelectItemModel(455, "Dismal Grotto"),
                new SelectItemModel(456, "Dismal Grotto"),
                new SelectItemModel(457, "Dismal Grotto"),
                new SelectItemModel(458, "Dismal Grotto"),
                new SelectItemModel(459, "Dismal Grotto"),
                new SelectItemModel(460, "The Accursed Caves"),
                new SelectItemModel(461, "The Accursed Caves"),
                new SelectItemModel(462, "The Accursed Caves"),
                new SelectItemModel(463, "The Accursed Caves"),
                new SelectItemModel(464, "The Accursed Caves"),
                new SelectItemModel(465, "The Dark Caverns"),
                new SelectItemModel(466, "The Dark Caverns"),
                new SelectItemModel(467, "The Dark Caverns"),
                new SelectItemModel(468, "The Dark Caverns"),
                new SelectItemModel(469, "The Dark Caverns"),
                new SelectItemModel(470, "Half Orc Command Post"),
                new SelectItemModel(471, "The Unused Mine"),
                new SelectItemModel(472, "The Unused Mine"),
                new SelectItemModel(473, "The Unused Mine"),
                new SelectItemModel(474, "The Unused Mine"),
                new SelectItemModel(475, "Rise of the Spraggons"),
                new SelectItemModel(476, "The Warrens"),
                new SelectItemModel(477, "The Accursed Caves"),
                new SelectItemModel(478, "The Accursed Caves"),
                new SelectItemModel(479, "The Ancient's Retreat"),
                new SelectItemModel(480, "The Shaman's Inner Sanctum"),
                new SelectItemModel(481, "The Accursed Caves"),
                new SelectItemModel(482, "The Unused Mine"),
                new SelectItemModel(483, "The Brawler's Den"),
                new SelectItemModel(484, "Felena's Sorrow"),
                new SelectItemModel(485, "The Unused Mine"),
                new SelectItemModel(486, "The Unused Mine"),
                new SelectItemModel(487, "The Pit of Despair"),
                new SelectItemModel(488, "The Forgotten Vein"),
                new SelectItemModel(489, "Demon's Breach"),
                new SelectItemModel(491, "Maze of Madness"),
                new SelectItemModel(492, "Shattered Lands"),
                new SelectItemModel(494, "The Gnoll's Den"),
                new SelectItemModel(495, "The Inner Sanctum"),
                new SelectItemModel(496, "The Deep"),
                new SelectItemModel(497, "Mid Launch"),
                new SelectItemModel(498, "Alb Launch"),
                new SelectItemModel(499, "Hib Launch")
            };

            BindData(input, zones);
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

        private static void BindData(ComboBox input, List<SelectItemModel> data, string nullLabel = "Undefined")
        {
            if (data.All(x => x.Id != null))
                data.Insert(0, new SelectItemModel(null, nullLabel));

            input.DataSource = new BindingSource(data, null);
            input.DisplayMember = "Value";
            input.ValueMember = "Id";
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
                new SelectItemModel(9, "Self")
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
                new SelectItemModel(4, "Harp")
            };

            BindData(input, items);
        }

        public static void BindItemSlot(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(7, "Horse Armor"),
                new SelectItemModel(8, "Horse Barding"),
                new SelectItemModel(9, "Horse"),
                new SelectItemModel(10, "Right Hand"),
                new SelectItemModel(11, "Left Hand"),
                new SelectItemModel(12, "Two Hand"),
                new SelectItemModel(13, "Ranged"),
                new SelectItemModel(14, "First Quiver"),
                new SelectItemModel(15, "Second Quiver"),
                new SelectItemModel(16, "Third Quiver"),
                new SelectItemModel(17, "Forth Quiver"),
                new SelectItemModel(21, "Helm"),
                new SelectItemModel(22, "Hands"),
                new SelectItemModel(23, "Feet"),
                new SelectItemModel(24, "Jewelry"),
                new SelectItemModel(25, "Torso"),
                new SelectItemModel(26, "Cloak"),
                new SelectItemModel(27, "Legs"),
                new SelectItemModel(28, "Arms"),
                new SelectItemModel(29, "Neck"),                
                new SelectItemModel(11, "Shield"),
                new SelectItemModel(32, "Waist"),
                new SelectItemModel(33, "Left Wrist"),
                new SelectItemModel(34, "Right Wrist"),
                new SelectItemModel(35, "Left Ring"),
                new SelectItemModel(36, "Right Ring"),
                new SelectItemModel(37, "Mythical"),
                new SelectItemModel(40, "Inventory")
            };

            BindData(input, items);
        }

        public static void BindNpcEquipmentSlot(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(10, "Right Hand"),
                new SelectItemModel(11, "Left Hand"),
                new SelectItemModel(12, "Two Hand"),
                new SelectItemModel(13, "Ranged"),
                new SelectItemModel(21, "Helm"),
                new SelectItemModel(22, "Hands"),
                new SelectItemModel(23, "Feet"),
                new SelectItemModel(25, "Torso"),
                new SelectItemModel(26, "Cloak"),
                new SelectItemModel(27, "Legs"),
                new SelectItemModel(28, "Arms")
            };

            BindData(input, items);
        }

        public static void BindColors(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "White"),
                new SelectItemModel(1, "Old Red"),
                new SelectItemModel(2, "Old Green"),
                new SelectItemModel(3, "Old Blue"),
                new SelectItemModel(4, "Old Yellow"),
                new SelectItemModel(5, "Old Purple"),
                new SelectItemModel(6, "Gray"),
                new SelectItemModel(7, "Old Turquoise"),
                new SelectItemModel(8, "Leather Yellow"),
                new SelectItemModel(9, "Leather Red"),
                new SelectItemModel(10, "Leather Green"),
                new SelectItemModel(11, "Leather Orange"),
                new SelectItemModel(12, "Leather Violet"),
                new SelectItemModel(13, "Leather Forest Green"),
                new SelectItemModel(14, "Leather Blue"),
                new SelectItemModel(15, "Leather Purple"),
                new SelectItemModel(16, "Bronze"),
                new SelectItemModel(17, "Iron"),
                new SelectItemModel(18, "Steel"),
                new SelectItemModel(19, "Alloy"),
                new SelectItemModel(20, "Fine Alloy"),
                new SelectItemModel(21, "Mithril"),
                new SelectItemModel(22, "Asterite"),
                new SelectItemModel(23, "Eog"),
                new SelectItemModel(24, "Xenium"),
                new SelectItemModel(25, "Vaanum"),
                new SelectItemModel(26, "Adamantium"),
                new SelectItemModel(27, "Red Cloth"),
                new SelectItemModel(28, "Orange Cloth"),
                new SelectItemModel(29, "Yellow-Orange Cloth"),
                new SelectItemModel(30, "Yellow Cloth"),
                new SelectItemModel(31, "Yellow-Green Cloth"),
                new SelectItemModel(32, "Green Cloth"),
                new SelectItemModel(33, "Blue-Green Cloth"),
                new SelectItemModel(34, "Turquoise Cloth"),
                new SelectItemModel(35, "Light Blue Cloth"),
                new SelectItemModel(36, "Blue Cloth"),
                new SelectItemModel(37, "Blue-Violet Cloth"),
                new SelectItemModel(38, "Violet Cloth"),
                new SelectItemModel(39, "Bright Violet Cloth"),
                new SelectItemModel(40, "Purple Cloth"),
                new SelectItemModel(41, "Bright Purple Cloth"),
                new SelectItemModel(42, "Purple-Red Cloth"),
                new SelectItemModel(43, "Black Cloth"),
                new SelectItemModel(44, "Brown Cloth"),
                new SelectItemModel(45, "Blue Metal"),
                new SelectItemModel(46, "Green Metal"),
                new SelectItemModel(47, "Yellow Metal"),
                new SelectItemModel(48, "Gold Metal"),
                new SelectItemModel(49, "Red Metal"),
                new SelectItemModel(50, "Purple Metal"),
                new SelectItemModel(51, "Blue 1"),
                new SelectItemModel(52, "Blue 2"),
                new SelectItemModel(53, "Blue 3"),
                new SelectItemModel(54, "Blue 4"),
                new SelectItemModel(55, "Turquoise 1"),
                new SelectItemModel(56, "Turquoise 2"),
                new SelectItemModel(57, "Turquoise 3"),
                new SelectItemModel(58, "Teal 1"),
                new SelectItemModel(59, "Teal 2"),
                new SelectItemModel(60, "Teal 3"),
                new SelectItemModel(61, "Brown 1"),
                new SelectItemModel(62, "Brown 2"),
                new SelectItemModel(63, "Brown 3"),
                new SelectItemModel(64, "Red 1"),
                new SelectItemModel(65, "Red 2"),
                new SelectItemModel(66, "Red 3"),
                new SelectItemModel(67, "Red 4"),
                new SelectItemModel(68, "Green 1"),
                new SelectItemModel(69, "Green 2"),
                new SelectItemModel(70, "Green 3"),
                new SelectItemModel(71, "Green 4"),
                new SelectItemModel(72, "Gray 1"),
                new SelectItemModel(73, "Gray 2"),
                new SelectItemModel(74, "Gray 3"),
                new SelectItemModel(75, "Orange 1"),
                new SelectItemModel(76, "Orange 2"),
                new SelectItemModel(77, "Orange 3"),
                new SelectItemModel(78, "Purple 1"),
                new SelectItemModel(79, "Purple 2"),
                new SelectItemModel(80, "Purple 3"),
                new SelectItemModel(81, "Yellow 1"),
                new SelectItemModel(82, "Yellow 2"),
                new SelectItemModel(83, "Yelow 3"),
                new SelectItemModel(84, "violet"),
                new SelectItemModel(85, "mauve"),
                new SelectItemModel(86, "Blue 4"),
                new SelectItemModel(87, "Purple 4"),
                new SelectItemModel(100, "Ship Red"),
                new SelectItemModel(101, "Ship Red 2"),
                new SelectItemModel(102, "Ship Orange"),
                new SelectItemModel(103, "Ship Orange 2"),
                new SelectItemModel(104, "Orange 3"),
                new SelectItemModel(105, "Ship Yellow"),
                new SelectItemModel(106, "Ship Lime Green"),
                new SelectItemModel(107, "Ship Green"),
                new SelectItemModel(108, "Ship Green 2"),
                new SelectItemModel(109, "Ship Turquiose"),
                new SelectItemModel(110, "Ship Turqiose 2"),
                new SelectItemModel(111, "Ship Blue"),
                new SelectItemModel(112, "Ship Blue 2"),
                new SelectItemModel(113, "Ship Blue 3"),
                new SelectItemModel(114, "Ship Purple"),
                new SelectItemModel(115, "Ship Purple 2"),
                new SelectItemModel(116, "Ship Purple 3"),
                new SelectItemModel(117, "Ship Pink"),
                new SelectItemModel(118, "Ship Charcoal"),
                new SelectItemModel(119, "Ship Charcoal 2"),
                new SelectItemModel(120, "red - crafter only"),
                new SelectItemModel(121, "plum - crafter only"),
                new SelectItemModel(122, "purple - crafter only"),
                new SelectItemModel(123, "dark purple - crafter only"),
                new SelectItemModel(124, "dusky purple - crafter only"),
                new SelectItemModel(125, "light gold - crafter only"),
                new SelectItemModel(126, "dark gold - crafter only"),
                new SelectItemModel(127, "dirty orange - crafter only"),
                new SelectItemModel(128, "dark tan - crafter only"),
                new SelectItemModel(129, "brown - crafter only"),
                new SelectItemModel(130, "light green - crafter only"),
                new SelectItemModel(131, "olive green - crafter only"),
                new SelectItemModel(132, "cornflower blue - crafter only"),
                new SelectItemModel(133, "light gray - crafter only"),
                new SelectItemModel(134, "hot pink - crafter only"),
                new SelectItemModel(135, "dusky rose - crafter only"),
                new SelectItemModel(136, "sage green - crafter only"),
                new SelectItemModel(137, "lime green - crafter only"),
                new SelectItemModel(138, "gray teal - crafter only"),
                new SelectItemModel(139, "gray blue - crafter only"),
                new SelectItemModel(140, "olive gray - crafter only"),
                new SelectItemModel(141, "Navy Blue - crafter only"),
                new SelectItemModel(142, "Forest Green - crafter only"),
                new SelectItemModel(143, "Burgundy - crafter only")
            };

            BindData(input, items);
        }

        public static void BindObjectType(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "generic (item)"),
                new SelectItemModel(1, "generic (weapon)"),
                new SelectItemModel(2, "crushing (weapon)"),
                new SelectItemModel(3, "slashing (weapon)"),
                new SelectItemModel(4, "thrusting (weapon)"),
                new SelectItemModel(5, "fired (weapon)"),
                new SelectItemModel(6, "twohanded (weapon)"),
                new SelectItemModel(7, "polearm (weapon)"),
                new SelectItemModel(8, "staff (weapon)"),
                new SelectItemModel(9, "longbow (weapon)"),
                new SelectItemModel(10, "crossbow (weapon)"),
                new SelectItemModel(11, "sword (weapon)"),
                new SelectItemModel(12, "hammer (weapon)"),
                new SelectItemModel(13, "axe (weapon)"),
                new SelectItemModel(14, "spear (weapon)"),
                new SelectItemModel(15, "composite bow (weapon)"),
                new SelectItemModel(16, "thrown (weapon)"),
                new SelectItemModel(17, "left axe (weapon)"),
                new SelectItemModel(18, "recurve bow (weapon)"),
                new SelectItemModel(19, "blades (weapon)"),
                new SelectItemModel(20, "blunt (weapon)"),
                new SelectItemModel(21, "piercing (weapon)"),
                new SelectItemModel(22, "large (weapon)"),
                new SelectItemModel(23, "celtic spear (weapon)"),
                new SelectItemModel(24, "flexible (weapon)"),
                new SelectItemModel(25, "hand to hand (weapon)"),
                new SelectItemModel(26, "scythe (weapon)"),
                new SelectItemModel(27, "fist wraps (weapon)"),
                new SelectItemModel(28, "mauler staff (weapon)"),
                new SelectItemModel(31, "generic (armor)"),
                new SelectItemModel(32, "cloth (armor)"),
                new SelectItemModel(33, "leather (armor)"),
                new SelectItemModel(34, "studded leather (armor)"),
                new SelectItemModel(35, "chain (armor)"),
                new SelectItemModel(36, "plate (armor)"),
                new SelectItemModel(37, "reinforced (armor)"),
                new SelectItemModel(38, "scale (armor)"),
                new SelectItemModel(41, "magical (item)"),
                new SelectItemModel(42, "shield (armor)"),
                new SelectItemModel(43, "arrow (item)"),
                new SelectItemModel(44, "bolt (item)"),
                new SelectItemModel(45, "instrument (item)"),
                new SelectItemModel(46, "poison (item)"),
                new SelectItemModel(47, "alchemy tincture"),
                new SelectItemModel(48, "spellcrafting gem"),
                new SelectItemModel(49, "garden object"),
                new SelectItemModel(50, "house wall object"),
                new SelectItemModel(51, "house floor object"),
                new SelectItemModel(53, "house npc"),
                new SelectItemModel(54, "house vault"),
                new SelectItemModel(55, "house crafting object"),
                new SelectItemModel(68, "house bindstone")
            };

            BindData(input, items);
        }

        public static void BindBonusCatagory(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(1, "Stats"),
                new SelectItemModel(2, "Stat Cap"),
                new SelectItemModel(3, "Resists"),
                new SelectItemModel(4, "Resist Cap"),
                new SelectItemModel(5, "Skills"),
                new SelectItemModel(6, "Focus"),
                new SelectItemModel(7, "Trials of Atlantis"),
                new SelectItemModel(8, "Other"),
                new SelectItemModel(9, "Mythical")
            };

            BindData(input, items, "All");
        }

        public static void BindItemExtension(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "0"),
                new SelectItemModel(1, "1"),
                new SelectItemModel(2, "2"),
                new SelectItemModel(3, "3"),
                new SelectItemModel(4, "4"),
                new SelectItemModel(5, "5"),
                new SelectItemModel(6, "6"),
                new SelectItemModel(7, "7"),
                new SelectItemModel(8, "8")
            };

            BindData(input, items);
        }

        public static void BindItemEffect(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(1, "univ longsword propane-style flame"),
                new SelectItemModel(2, "univ longsword regular flame"),
                new SelectItemModel(3, "univ longsword orange flame"),
                new SelectItemModel(4, "univ longsword rising flame"),
                new SelectItemModel(5, "univ longsword flame w/smoke"),
                new SelectItemModel(6, "univ longsword flame w/sparks"),
                new SelectItemModel(7, "univ longsword hot glow"),
                new SelectItemModel(8, "univ longsword hot aura"),
                new SelectItemModel(9, "univ longsword blue aura"),
                new SelectItemModel(10, "univ longsword hot blue glow"),
                new SelectItemModel(11, "univ longsword hot gold glow"),
                new SelectItemModel(12, "univ longsword hot red glow"),
                new SelectItemModel(13, "univ longsword red aura"),
                new SelectItemModel(14, "univ longsword cold aura w/sparkles"),
                new SelectItemModel(15, "univ longsword cold aura w/vapor"),
                new SelectItemModel(16, "longsword hilt wavering blue beam"),
                new SelectItemModel(17, "longsword hilt wavering green beam"),
                new SelectItemModel(18, "longsword hilt wavering red beam"),
                new SelectItemModel(19, "longsword hilt red/blue beam"),
                new SelectItemModel(20, "longsword hilt purple beam"),
                new SelectItemModel(21, "univ 2h/gr sword yellow flames"),
                new SelectItemModel(22, "univ 2h/gr sword orange flames"),
                new SelectItemModel(23, "univ 2h/gr sword fire w/smoke"),
                new SelectItemModel(24, "univ 2h/gr sword fire w/sparks"),
                new SelectItemModel(25, "univ 2h/gr blue glow w/sparkles"),
                new SelectItemModel(26, "univ 2h/gr blue aura w/cold vapor"),
                new SelectItemModel(27, "univ 2h/gr icy blue glow"),
                new SelectItemModel(28, "univ 2h/gr red aura"),
                new SelectItemModel(29, "univ 2h/gr strong crimson glow"),
                new SelectItemModel(30, "univ 2h/gr white core red glow"),
                new SelectItemModel(31, "univ 2h/gr silvery/white glow"),
                new SelectItemModel(32, "univ 2h/gr gold/yellow glow"),
                new SelectItemModel(33, "univ 2h/gr hot green glow"),
                new SelectItemModel(34, "univ 1h hammer red aura"),
                new SelectItemModel(35, "univ 1h hammer fiery glow"),
                new SelectItemModel(36, "univ 1h hammer more intense fiery glow"),
                new SelectItemModel(37, "univ 1h hammer flaming"),
                new SelectItemModel(38, "univ 1h hammer torchlike flaming"),
                new SelectItemModel(39, "univ 1h hammer silvery glow"),
                new SelectItemModel(40, "univ 1h hammer purple glow"),
                new SelectItemModel(41, "univ 1h hammer blue aura"),
                new SelectItemModel(42, "univ 1h hammer blue glow"),
                new SelectItemModel(43, "univ 1h hammer arcs from head to handle"),
                new SelectItemModel(44, "univ 1h crush arcing halo"),
                new SelectItemModel(45, "univ 1h crush center arcing"),
                new SelectItemModel(46, "univ 1h crush smaller arcing halo"),
                new SelectItemModel(47, "univ 1h crush hot orange core glow"),
                new SelectItemModel(48, "univ 1h crush orange aura"),
                new SelectItemModel(49, "univ 1h crush subtle aura with sparks"),
                new SelectItemModel(50, "univ 1h crush yellow flame"),
                new SelectItemModel(51, "univ 1h crush mana flame"),
                new SelectItemModel(52, "univ 1h crush hot green glow"),
                new SelectItemModel(53, "univ 1h crush hot red glow"),
                new SelectItemModel(54, "univ 1h crush hot purple glow"),
                new SelectItemModel(55, "univ 1h crush cold vapor"),
                new SelectItemModel(56, "univ 1h axe basic flame"),
                new SelectItemModel(57, "univ 1h axe orange flame"),
                new SelectItemModel(58, "univ 1h axe slow orange flame w/sparks"),
                new SelectItemModel(59, "univ 1h axe fiery/trailing flame"),
                new SelectItemModel(60, "univ 1h axe cold vapor"),
                new SelectItemModel(61, "univ 1h axe blue aura with twinkles"),
                new SelectItemModel(62, "univ 1h axe hot green glow"),
                new SelectItemModel(63, "univ 1h axe hot blue glow"),
                new SelectItemModel(64, "univ 1h axe hot cyan glow"),
                new SelectItemModel(65, "univ 1h axe hot purple glow"),
                new SelectItemModel(66, "univ 1h axe blue->purple->orange glow"),
                new SelectItemModel(67, "univ shortsword propane flame"),
                new SelectItemModel(68, "univ shortsword orange flame with sparks"),
                new SelectItemModel(69, "univ shortsword blue aura with twinkles"),
                new SelectItemModel(70, "univ shortsword green cloud with bubbles"),
                new SelectItemModel(71, "univ shortsword red aura with blood bubbles"),
                new SelectItemModel(72, "univ shortsword evil green glow"),
                new SelectItemModel(73, "univ shortsword black glow"),
                new SelectItemModel(74, "Midgard battlespear cold with twinkles"),
                new SelectItemModel(75, "Midgard battlespear evil green aura"),
                new SelectItemModel(76, "Midgard battlespear evil red aura"),
                new SelectItemModel(77, "Midgard battlespear flaming"),
                new SelectItemModel(78, "Midgard battlespear hot gold glow"),
                new SelectItemModel(79, "Midgard battlespear hot fire glow"),
                new SelectItemModel(80, "Midgard battlespear red aura"),
                new SelectItemModel(81, "Midgard lugged spear blue glow"),
                new SelectItemModel(82, "Midgard lugged spear hot blue glow"),
                new SelectItemModel(83, "Midgard lugged spear cold with twinkles"),
                new SelectItemModel(84, "Midgard lugged spear electric arcing"),
                new SelectItemModel(85, "Midgard lugged spear hot yellow flame"),
                new SelectItemModel(86, "Midgard lugged spear orange flame w/ sparks"),
                new SelectItemModel(87, "Midgard lugged spear orange to purple flame"),
                new SelectItemModel(88, "Midgard lugged spear hot purple flame"),
                new SelectItemModel(89, "Midgard lugged spear silvery glow"),
                new SelectItemModel(90, "Briton quarterstaff blue glow"),
                new SelectItemModel(91, "Briton quarterstaff blue glow w/ twinkles"),
                new SelectItemModel(92, "Briton quarterstaff gold glow"),
                new SelectItemModel(93, "Briton quarterstaff gold glow w/ twinkles"),
                new SelectItemModel(94, "Briton quarterstaff faint red glow"),
                new SelectItemModel(95, "Relic pad effect"),
                new SelectItemModel(96, "Admorenth's Staff Effect (Admorenth ONLY)"),
                new SelectItemModel(98, "Ice Fist 1"),
                new SelectItemModel(99, "Fire Fist 2"),
                new SelectItemModel(100, "Fire Fist 1"),
                new SelectItemModel(101, "Force/Ripple Fist"),
                new SelectItemModel(102, "Smoke Fist"),
                new SelectItemModel(103, "Energy Fist 1"),
                new SelectItemModel(104, "Energy Fist 2"),
                new SelectItemModel(105, "ADR_DSL_Hib_flex_thrust_sfx"),
                new SelectItemModel(106, "ADR_DSL_Hib_flex_slash_sfx"),
                new SelectItemModel(107, "ADR_DSL_Hib_flex_blunt_sfx"),
                new SelectItemModel(108, "ADR_DSL_Hib_bow_short_sfx"),
                new SelectItemModel(109, "ADR_DSL_Hib_bow_long_sfx"),
                new SelectItemModel(110, "ADR_DSL_Hib_bow_cross_sfx"),
                new SelectItemModel(111, "ADR_DSL_Hib_2h_sword_thrust_sfx"),
                new SelectItemModel(112, "ADR_DSL_Hib_2h_sword_slash_sfx"),
                new SelectItemModel(113, "ADR_DSL_Hib_2h_hammer_blunt_sfx"),
                new SelectItemModel(114, "ADR_DSL_Hib_2h_axe_sfx"),
                new SelectItemModel(115, "ADR_DSL_Hib_1h_sword_thrust_offhand_sfx"),
                new SelectItemModel(116, "ADR_DSL_Hib_1h_sword_thrust_mainhand_sfx"),
                new SelectItemModel(117, "ADR_DSL_Hib_1h_sword_slash_offhand_sfx"),
                new SelectItemModel(118, "ADR_DSL_Hib_1h_sword_slash_mainhand_sfx"),
                new SelectItemModel(119, "ADR_DSL_Hib_1h_hammer_blunt_offhand_sfx"),
                new SelectItemModel(120, "ADR_DSL_Hib_1h_hammer_blunt_mainhand_sfx"),
                new SelectItemModel(121, "ADR_DSL_Hib_1h_axe_offhand_sfx"),
                new SelectItemModel(122, "ADR_DSL_Hib_1h_axe_mainhand_sfx"),
                new SelectItemModel(123, "ADR_DSL_Hib_Shield_Small_sfx"),
                new SelectItemModel(124, "ADR_DSL_Hib_Shield_Medium_sfx"),
                new SelectItemModel(125, "ADR_DSL_Hib_Shield_Large_sfx"),
                new SelectItemModel(126, "ADR_DSL_Hib_scythe_sfx"),
                new SelectItemModel(127, "ADR_DSL_Hib_quiver_sfx"),
                new SelectItemModel(128, "ADR_DSL_Hib_quarterstaff_sfx"),
                new SelectItemModel(129, "ADR_DSL_Hib_pole-arm_spear_thrust_sfx"),
                new SelectItemModel(130, "ADR_DSL_Hib_pole-arm_spear_slash_sfx"),
                new SelectItemModel(131, "ADR_DSL_Hib_pole-arm_blunt_sfx"),
                new SelectItemModel(132, "ADR_DSL_Hib_magicstaff_sfx"),
                new SelectItemModel(133, "ADR_DSL_Hib_harp_sfx"),
                new SelectItemModel(134, "ADR_DSL_Hib_H2h_thrust_offhand_sfx"),
                new SelectItemModel(135, "ADR_DSL_Hib_H2h_thrust_mainhand_sfx"),
                new SelectItemModel(136, "ADR_DSL_Hib_H2h_slash_offhand_sfx"),
                new SelectItemModel(137, "ADR_DSL_Hib_H2h_slash_mainhand_sfx"),
                new SelectItemModel(138, "ADR_DSL_Hib_H2h_blunt_offhand_sfx"),
                new SelectItemModel(139, "ADR_DSL_Hib_H2h_blunt_mainhand_sfx"),
                new SelectItemModel(140, "ADR_DSL_Mid_Flex_thrust_sfx"),
                new SelectItemModel(141, "ADR_DSL_Mid_H2h_blunt_mainhand_sfx"),
                new SelectItemModel(142, "ADR_DSL_Mid_H2h_blunt_offhand_sfx"),
                new SelectItemModel(143, "ADR_DSL_Mid_H2h_slash_mainhand_sfx"),
                new SelectItemModel(144, "ADR_DSL_Mid_H2h_slash_offhand_sfx"),
                new SelectItemModel(145, "ADR_DSL_Mid_H2h_thrust_mainhand_sfx"),
                new SelectItemModel(146, "ADR_DSL_Mid_H2h_thrust_offhand_sfx"),
                new SelectItemModel(147, "ADR_DSL_Mid_harp_sfx"),
                new SelectItemModel(148, "ADR_DSL_Mid_magicstaff_sfx"),
                new SelectItemModel(149, "ADR_DSL_Mid_pole-arm_blunt_sfx"),
                new SelectItemModel(150, "ADR_DSL_Mid_pole-arm_spear_slash_sfx"),
                new SelectItemModel(151, "ADR_DSL_Mid_pole-arm_spear_thrust_sfx"),
                new SelectItemModel(152, "ADR_DSL_Mid_quarterstaff_sfx"),
                new SelectItemModel(153, "ADR_DSL_Mid_quiver_sfx"),
                new SelectItemModel(154, "ADR_DSL_Mid_scythe_sfx"),
                new SelectItemModel(155, "ADR_DSL_Mid_Shield_Large_sfx"),
                new SelectItemModel(156, "ADR_DSL_Mid_Shield_Medium_sfx"),
                new SelectItemModel(157, "ADR_DSL_Mid_Shield_Small_sfx"),
                new SelectItemModel(158, "ADR_DSL_Mid_1h_axe_mainhand_sfx"),
                new SelectItemModel(159, "ADR_DSL_Mid_1h_axe_offhand_sfx"),
                new SelectItemModel(160, "ADR_DSL_Mid_1h_hammer_blunt_mainhand_sfx"),
                new SelectItemModel(161, "ADR_DSL_Mid_1h_hammer_blunt_offhand_sfx"),
                new SelectItemModel(162, "ADR_DSL_Mid_1h_sword_slash_mainhand_sfx"),
                new SelectItemModel(163, "ADR_DSL_Mid_1h_sword_slash_offhand_sfx"),
                new SelectItemModel(164, "ADR_DSL_Mid_1h_sword_thrust_mainhand_sfx"),
                new SelectItemModel(165, "ADR_DSL_Mid_1h_sword_thrust_offhand_sfx"),
                new SelectItemModel(166, "ADR_DSL_Mid_2h_axe_sfx"),
                new SelectItemModel(167, "ADR_DSL_Mid_2h_hammer_blunt_sfx"),
                new SelectItemModel(168, "ADR_DSL_Mid_2h_sword_slash_sfx"),
                new SelectItemModel(169, "ADR_DSL_Mid_2h_sword_thrust_sfx"),
                new SelectItemModel(170, "ADR_DSL_Mid_bow_cross_sfx"),
                new SelectItemModel(171, "ADR_DSL_Mid_bow_long_sfx"),
                new SelectItemModel(172, "ADR_DSL_Mid_bow_short_sfx"),
                new SelectItemModel(173, "ADR_DSL_Mid_Flex_blunt_sfx"),
                new SelectItemModel(174, "ADR_DSL_Mid_Flex_slash_sfx"),
                new SelectItemModel(175, "ADR_DSL_Alb_1h_sword_slash_offhand_sfx"),
                new SelectItemModel(176, "ADR_DSL_Alb_1h_sword_slash_mainhand_sfx"),
                new SelectItemModel(177, "ADR_DSL_Alb_1h_hammer_blunt_offhand_sfx"),
                new SelectItemModel(178, "ADR_DSL_Alb_1h_hammer_blunt_mainhand_sfx"),
                new SelectItemModel(179, "ADR_DSL_Alb_1h_axe_offhand_sfx"),
                new SelectItemModel(180, "ADR_DSL_Alb_1h_axe_mainhand_sfx"),
                new SelectItemModel(181, "ADR_DSL_Alb_Shield_Small_sfx"),
                new SelectItemModel(182, "ADR_DSL_Alb_Shield_Medium_sfx"),
                new SelectItemModel(183, "ADR_DSL_Alb_Shield_Large_sfx"),
                new SelectItemModel(184, "ADR_DSL_Alb_scythe_sfx"),
                new SelectItemModel(185, "ADR_DSL_Alb_quiver_sfx"),
                new SelectItemModel(186, "ADR_DSL_Alb_quarterstaff_sfx"),
                new SelectItemModel(187, "ADR_DSL_Alb_pole-arm_spear_thrust_sfx"),
                new SelectItemModel(188, "ADR_DSL_Alb_pole-arm_spear_slash_sfx"),
                new SelectItemModel(189, "ADR_DSL_Alb_pole-arm_blunt_sfx"),
                new SelectItemModel(190, "ADR_DSL_Alb_magicstaff_sfx"),
                new SelectItemModel(191, "ADR_DSL_Alb_harp_sfx"),
                new SelectItemModel(192, "ADR_DSL_Alb_H2h_thrust_offhand_sfx"),
                new SelectItemModel(193, "ADR_DSL_Alb_H2h_thrust_mainhand_sfx"),
                new SelectItemModel(194, "ADR_DSL_Alb_H2h_slash_offhand_sfx"),
                new SelectItemModel(195, "ADR_DSL_Alb_H2h_slash_mainhand_sfx"),
                new SelectItemModel(196, "ADR_DSL_Alb_H2h_blunt_offhand_sfx"),
                new SelectItemModel(197, "ADR_DSL_Alb_H2h_blunt_mainhand_sfx"),
                new SelectItemModel(198, "ADR_DSL_Alb_Flex_thrust_sfx"),
                new SelectItemModel(199, "ADR_DSL_Alb_Flex_slash_sfx"),
                new SelectItemModel(200, "ADR_DSL_Alb_Flex_blunt_sfx"),
                new SelectItemModel(201, "ADR_DSL_Alb_bow_short_sfx"),
                new SelectItemModel(202, "ADR_DSL_Alb_bow_long_sfx"),
                new SelectItemModel(203, "ADR_DSL_Alb_bow_cross_sfx"),
                new SelectItemModel(204, "ADR_DSL_Alb_2h_sword_thrust_sfx"),
                new SelectItemModel(205, "ADR_DSL_Alb_2h_sword_slash_sfx"),
                new SelectItemModel(206, "ADR_DSL_Alb_2h_hammer_blunt_sfx"),
                new SelectItemModel(207, "ADR_DSL_Alb_2h_axe_sfx"),
                new SelectItemModel(208, "ADR_DSL_Alb_1h_sword_thrust_offhand_sfx"),
                new SelectItemModel(209, "ADR_DSL_Alb_1h_sword_thrust_mainhand_sfx"),
                new SelectItemModel(256, "Paladin Epic Two Handed Slash"),
                new SelectItemModel(257, "Hero Epic Blunt"),
                new SelectItemModel(258, "Warrior Epic One Handed Axe"),
                new SelectItemModel(259, "Hero_Piercing_2H"),
                new SelectItemModel(260, "Hero_Spear_2H"),
                new SelectItemModel(261, "Hero_Spear_1H"),
                new SelectItemModel(262, "Hero_Piercing_1H"),
                new SelectItemModel(263, "Hero_Blunt_2H"),
                new SelectItemModel(264, "Hero_Blade_2H"),
                new SelectItemModel(265, "Hero_Blade_1H"),
                new SelectItemModel(266, "Reaver_Sword_1H"),
                new SelectItemModel(267, "palladin_Slash_1h"),
                new SelectItemModel(268, "palladin_Thrust_2H"),
                new SelectItemModel(269, "palladin_Thrust_1H"),
                new SelectItemModel(270, "Reaver_Thrust_1H"),
                new SelectItemModel(271, "Reaver_Crush_1H"),
                new SelectItemModel(272, "Warrior_Sword_1H"),
                new SelectItemModel(273, "Warrior_Hammer_2H"),
                new SelectItemModel(274, "Warrior_Hammer_1H"),
                new SelectItemModel(275, "Warrior_Axe_2H"),
                new SelectItemModel(276, "Warrior_Sword_2H"),
                new SelectItemModel(277, "Reaver_Flex_1H"),
                new SelectItemModel(278, "palladin_Crush_2H"),
                new SelectItemModel(279, "palladin_Crush_1H"),
                new SelectItemModel(280, "Armsman_Thrust_2H"),
                new SelectItemModel(281, "Armsman_Crush_1H"),
                new SelectItemModel(282, "Armsman_Crush_2H"),
                new SelectItemModel(283, "Armsman_Pole_S"),
                new SelectItemModel(284, "Armsman_Sword_1H"),
                new SelectItemModel(285, "Armsman_Sword_2H"),
                new SelectItemModel(286, "Armsman_Thrust_1H"),
                new SelectItemModel(287, "Mercenary_T"),
                new SelectItemModel(288, "Mercenary_C"),
                new SelectItemModel(289, "Mercenary_S"),
                new SelectItemModel(290, "minstrel_T"),
                new SelectItemModel(291, "minstrel_S"),
                new SelectItemModel(292, "minstrel_H"),
                new SelectItemModel(293, "infiltrator_T"),
                new SelectItemModel(294, "Infiltrator_S"),
                new SelectItemModel(295, "sfx_epic_wpn_Armsman_Pole_C"),
                new SelectItemModel(296, "Heretic_flexS_1H"),
                new SelectItemModel(297, "Heretic_crush_1H"),
                new SelectItemModel(298, "Heretic_flexC_1H"),
                new SelectItemModel(299, "sfx_epic_wpn_cleric_C"),
                new SelectItemModel(300, "Scout_Bow"),
                new SelectItemModel(301, "Scout_T"),
                new SelectItemModel(302, "Scout_S"),
                new SelectItemModel(303, "sfx_epic_wpn_skald_axe_1h"),
                new SelectItemModel(304, "sfx_epic_wpn_skald_hammer_1h"),
                new SelectItemModel(305, "sfx_epic_wpn_skald_sword_1h"),
                new SelectItemModel(306, "sfx_epic_wpn_skald_hammer_2h"),
                new SelectItemModel(307, "sfx_epic_wpn_skald_axe_2h"),
                new SelectItemModel(308, "sfx_epic_wpn_skald_sword_2h"),
                new SelectItemModel(309, "sfx_epic_Druid_Crush_1H"),
                new SelectItemModel(310, "sfx_epic_Druid_Blade_1H"),
                new SelectItemModel(311, "sfx_epic_wpn_savage_sword_1h"),
                new SelectItemModel(312, "sfx_epic_wpn_savage_sword_2h"),
                new SelectItemModel(313, "sfx_epic_wpn_savage_axe_1h"),
                new SelectItemModel(314, "sfx_epic_Warden_Blade_1H"),
                new SelectItemModel(315, "sfx_epic_Warden_Crush_1H"),
                new SelectItemModel(316, "sfx_epic_Friar_Staff&Crush"),
                new SelectItemModel(317, "sfx_epic_Necromancer_Staff"),
                new SelectItemModel(318, "sfx_epic_wpn_savage_axe_2h"),
                new SelectItemModel(319, "sfx_epic_wpn_savage_hth"),
                new SelectItemModel(320, "sfx_epic_wpn_savage_hammer_1h"),
                new SelectItemModel(321, "sfx_epic_wpn_savage_hammer_2h"),
                new SelectItemModel(322, "sfx_epic_wpn_valkyrie_sword_1h"),
                new SelectItemModel(323, "sfx_epic_wpn_valkyrie_sword_2h"),
                new SelectItemModel(324, "sfx_epic_Ranger_Bow"),
                new SelectItemModel(325, "sfx_epic_Ranger_Pierce_1H"),
                new SelectItemModel(326, "sfx_epic_Ranger_Slash_1H"),
                new SelectItemModel(327, "sfx_epic_wpn_valkyrie_spear"),
                new SelectItemModel(328, "sfx_epic_wpn_Sorcerer_St"),
                new SelectItemModel(329, "sfx_epic_wpn_hunter_sword_2h"),
                new SelectItemModel(330, "sfx_epic_wpn_hunter_sword_1H"),
                new SelectItemModel(331, "sfx_epic_wpn_hunter_spear"),
                new SelectItemModel(332, "sfx_epic_wpn_hunter_bow"),
                new SelectItemModel(333, "sfx_epic_Blademaster_Pierce_1H"),
                new SelectItemModel(334, "sfx_epic_Blademaster_Slash_1H"),
                new SelectItemModel(335, "sfx_epic_Blademaster_Crush_1H"),
                new SelectItemModel(336, "sfx_epic_wpn_thane_sword_2h"),
                new SelectItemModel(337, "sfx_epic_wpn_thane_sword_1h"),
                new SelectItemModel(338, "sfx_epic_wpn_thane_axe_2h"),
                new SelectItemModel(339, "sfx_epic_wpn_thane_axe_1h"),
                new SelectItemModel(340, "sfx_epic_Bainshee_Staff"),
                new SelectItemModel(341, "sfx_epic_Animist_Staff"),
                new SelectItemModel(342, "sfx_epic_Eldritch_Staff"),
                new SelectItemModel(343, "sfx_epic_Enchanter_Staff"),
                new SelectItemModel(344, "sfx_epic_wpn_thane_hammer_2h"),
                new SelectItemModel(345, "sfx_epic_wpn_thane_hammer_1h"),
                new SelectItemModel(346, "sfx_epic_wpn_shadowblade_sword_2h"),
                new SelectItemModel(347, "sfx_epic_wpn_shadowblade_sword_1h"),
                new SelectItemModel(348, "sfx_epic_Mentalist_Staff"),
                new SelectItemModel(349, "sfx_epic_Valewalker_Sy"),
                new SelectItemModel(350, "sfx_epic_wpn_shadowblade_axe_1h"),
                new SelectItemModel(351, "sfx_epic_wpn_shadowblade_axe_2h"),
                new SelectItemModel(352, "sfx_epic_Healer_Crush_2H"),
                new SelectItemModel(353, "sfx_epic_Healer_Crush_1H"),
                new SelectItemModel(354, "sfx_epic_Spiritmaster_Staff"),
                new SelectItemModel(355, "sfx_epic_wpn_Cabalist_st"),
                new SelectItemModel(356, "sfx_epic_wpn_Theurgist_St"),
                new SelectItemModel(357, "sfx_epic_wpn_Vampiir_T"),
                new SelectItemModel(358, "sfx_epic_wpn_Wizard_St"),
                new SelectItemModel(359, "sfx_epic_wpn_Runemaster_St"),
                new SelectItemModel(360, "sfx_epic_Shaman_Crush_1H"),
                new SelectItemModel(361, "sfx_epic_Bonedancer_Staff"),
                new SelectItemModel(362, "sfx_epic_Warlock_Staff"),
                new SelectItemModel(363, "sfx_epic_Shaman_Crush_2H"),
                new SelectItemModel(364, "sfx_epic_wpn_Champion_Slash_2h"),
                new SelectItemModel(365, "sfx_epic_wpn_Champion_Slash_1h"),
                new SelectItemModel(366, "sfx_epic_wpn_Champion_Pierce_1h"),
                new SelectItemModel(367, "sfx_epic_wpn_Champion_Crush_2h"),
                new SelectItemModel(368, "sfx_epic_wpn_Champion_Crush_1h"),
                new SelectItemModel(369, "sfx_epic_wpn_nightshade_T"),
                new SelectItemModel(370, "sfx_epic_wpn_nightshade_S"),
                new SelectItemModel(371, "sfx_epic_wpn_berserker_axe_2h"),
                new SelectItemModel(372, "sfx_epic_wpn_berserker_axe_1h"),
                new SelectItemModel(373, "sfx_epic_wpn_berserker_sword_2h"),
                new SelectItemModel(374, "sfx_epic_wpn_berserker_sword_1h"),
                new SelectItemModel(375, "sfx_epic_wpn_berserker_hammer_2h"),
                new SelectItemModel(376, "sfx_epic_wpn_berserker_hammer_1h"),
                new SelectItemModel(377, "sfx_epic_wpn_Bard_Blade_1H"),
                new SelectItemModel(378, "sfx_epic_wpn_Bard_Crush_1H"),
                new SelectItemModel(379, "sfx_epic_wpn_Bard_Harp"),
                new SelectItemModel(380, "sfx_AlbMauler_Staff01"),
                new SelectItemModel(381, "sfx_HibMauler_Staff01"),
                new SelectItemModel(382, "sfx_MidMauler_Staff01"),
                new SelectItemModel(383, "sfx_MidMauler_Punch01"),
                new SelectItemModel(384, "sfx_HibMauler_Punch01"),
                new SelectItemModel(385, "sfx_AlbMauler_Punch01")
            };

            BindData(input, items);
        }

        public static void BindBonusStats(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.Stat)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }

        public static void BindBonusStatCap(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.StatCap)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }
        public static void BindBonusResists(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.Resist)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }
        public static void BindBonusResistCap(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.ResistCap)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }

        public static void BindBonusSkills(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.Skill)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }

        public static void BindBonusFocus(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.Focus)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }

        public static void BindBonusToa(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.Toa)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }

        public static void BindBonusMythical(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.Mythical)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }

        public static void BindBonusOther(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Where(x => x.Type == ItemBonusService.BonusType.Other)
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }

        public static void BindBonusAll(ComboBox input)
        {
            var items = ItemBonusService.Bonuses
                .Select(x => new SelectItemModel(x.Id, x.Value))
                .ToList();

            BindData(input, items);
        }

        public class SelectItemModel
        {
            public SelectItemModel()
            {
            }

            public SelectItemModel(int? id, string value)
            {
                Id = id;
                Value = value;
            }

            public int? Id { get; set; }
            public string Value { get; set; }
        }

        public static void BindQuestType(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "Standard"),
                new SelectItemModel(1, "Collection"),
                new SelectItemModel(2, "AutoStart"),
                new SelectItemModel(3, "KillComplete"),
                new SelectItemModel(4, "InteractComplete"),
                new SelectItemModel(5, "SearchStart"),
                new SelectItemModel(200, "RewardQuest")
            };

            BindData(input, items);
        }

        public static void BindQuestStep(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(0, "Kill"),
                new SelectItemModel(1, "KillFinish"),
                new SelectItemModel(2, "Deliver"),
                new SelectItemModel(3, "DeliverFinish"),
                new SelectItemModel(4, "Interact"),
                new SelectItemModel(5, "InteractFinish"),
                new SelectItemModel(6, "Whisper"),
                new SelectItemModel(7, "WhisperFinish"),
                new SelectItemModel(8, "Search"),
                new SelectItemModel(9, "SearchFinish"),
                new SelectItemModel(10, "Collect"),
                new SelectItemModel(11, "CollectFinish")
            };

            BindData(input, items);
        }

        public static void BindQuestGoal(ComboBox input)
        {
            var items = new List<SelectItemModel>
            {
                new SelectItemModel(2, "Search"),
                new SelectItemModel(3, "Kill"),
                new SelectItemModel(4, "Interact"),
                new SelectItemModel(5, "InteractFinish"),
                new SelectItemModel(6, "InteractWhisper"),
                new SelectItemModel(7, "InteractDeliver")
            };

            BindData(input, items);
        }

    }
}