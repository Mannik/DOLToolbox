using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        private static void BindData(ComboBox input, List<SelectItemModel> data)
        {
            if (data.All(x => x.Id != null))
                data.Insert(0, new SelectItemModel(null, "Undefined"));

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
    }
}