using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace YAQOLM.Common.Configs {
    [Label("Recipe Configs")]
    public class RecipeConfig : ModConfig {
        public static RecipeConfig Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

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
    }

    [Label("Miscellaneous Configs")]
    public class MiscConfig : ModConfig {
        public static MiscConfig Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Misc")]

        [Label("[i:780] Steampunker sells all vanilla solutions")]
        [Tooltip("Can't sell modded solutions here to avoid progression breaking with modded biomes")]
        [DefaultValue(true)]
        public bool SteampunkerSolutions { get; set; }
    }
}