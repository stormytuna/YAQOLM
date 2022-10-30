using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace YAQOLM.Common.Configs {
    public class ServerConfig : ModConfig {
        public static ServerConfig Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [SeparatePage]
        [Header("Recipes")]

        [Label("Rod of Discord")]
        [Tooltip("15 [i:1225] 10 [i:520] 3 [i:549]")]
        [DefaultValue(true)]
        public bool Recipe_RodOfDiscord { get; set; }
    }
}