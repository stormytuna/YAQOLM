using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace YAQOLM.Common.Configs {
    [Label("Recipe Configs")]
    public class ServerConfig : ModConfig {
        public static ServerConfig Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;


        /* ================ */
        /*     RECIPES      */
        /* ================ */

        [Header("Recipes")]

        [Label("[i:1326] Rod of Discord")]
        [Tooltip("15 Hallowed Bar\n10 Soul of Light\n5 Soul of Sight")]
        [DefaultValue(true)]
        public bool RodOfDiscord { get; set; }

        [Label("[i:602] Snow Globe")]
        [Tooltip("20 Snow Block\n20 Glass")]
        [DefaultValue(true)]
        public bool SnowGlobe { get; set; }

        [Label("[i:3213] Money Trough")]
        [Tooltip("1 Piggy Bank\n6 Demonite/Crimtane Bar")]
        [DefaultValue(true)]
        public bool MoneyTrough { get; set; }

        [Label("[i:2676] Bait")]
        [Tooltip("2 Apprentice Bait -> 1 Journeyman Bait\n2 Journeyman Bait -> 1 Master Bait")]
        [DefaultValue(true)]
        public bool Bait { get; set; }

        [Label("[i:3031] Bottomless Buckets")]
        [Tooltip("8 of any fluid bucket -> 1 of that fluids bottomless bucket")]
        [DefaultValue(true)]
        public bool BottomlessBuckets { get; set; }

        [Label("[i:3031] Moon Lord drops to Luminite Bars")]
        [Tooltip("Any weapon -> 8 Luminite Bar\nAnything else -> 5 Luminite Bar")]
        [DefaultValue(true)]
        public bool LuminiteSmeltingRecipes { get; set; }


        /* ================ */
        /*      DROPS       */
        /* ================ */

        [Header("Drops")]

        [Label("[i:1309] King Slime drops Slime Staff")]
        [Tooltip("20% chance in Classic, 25% chance in expert")]
        [DefaultValue(true)]
        public bool KingSlimeDropsSlimeStaff { get; set; }


        /* ================ */
        /*      ITEMS       */
        /* ================ */

        [Header("Items")]

        [Label("[i:729] Wood Greaves have 1 defense")]
        [DefaultValue(true)]
        public bool WoodGreavesDefense { get; set; }

        [Label("[i:37] Goggles grant Night Owl")]
        [DefaultValue(true)]
        public bool GogglesGiveNightVision { get; set; }

        [Label("[i:1136] Rain Armor set bonus")]
        [Tooltip("8% increased damage and 5% increased critical chance when exposed to rain")]
        [DefaultValue(true)]
        public bool RainArmorSetBonus { get; set; }


        /* ================ */
        /*       MISC       */
        /* ================ */

        [Header("Misc")]

        [Label("[i:780] Steampunker sells all vanilla solutions")]
        [Tooltip("Can't sell modded solutions here to avoid progression breaking with modded biomes")]
        [DefaultValue(true)]
        public bool SteampunkerSolutions { get; set; }

        [Label("[i:2294] More angler quest loot")]
        [Tooltip("Can't sell modded solutions here to avoid progression breaking with modded biomes")]
        [DefaultValue(true)]
        public bool MoreAnglerLoot { get; set; }
    }
}