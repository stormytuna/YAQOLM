using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace YAQOLM.Common.Configs {
    public class ServerConfig : ModConfig {
        public static ServerConfig Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [SeparatePage]
        [Header("Recipes")]

        [Label("[i:1326] Rod of Discord")]
        [Tooltip("15 Hallowed Bar\n10 Soul of Light\n5 Soul of Sight")]
        [DefaultValue(true)]
        public bool Recipe_RodOfDiscord { get; set; }

        [Label("[i:602] Snow Globe")]
        [Tooltip("20 Snow Block\n20 Glass")]
        [DefaultValue(true)]
        public bool Recipe_SnowGlobe { get; set; }

        [Label("[i:3213] Money Trough")]
        [Tooltip("1 Piggy Bank\n6 Demonite/Crimtane Bar")]
        [DefaultValue(true)]
        public bool Recipe_MoneyTrough { get; set; }

        [Label("[i:2676] Bait")]
        [Tooltip("2 Apprentice Bait -> 1 Journeyman Bait\n2 Journeyman Bait -> 1 Master Bait")]
        [DefaultValue(true)]
        public bool Recipe_Bait { get; set; }
    }
}