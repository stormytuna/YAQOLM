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
    }
}