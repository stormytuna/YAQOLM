using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM {
    public class YAQOLM : Mod {
        public override void Load() {
            AddToggle("Mods.YAQOLM.Config.WarpedMirror", "Warped Mirror", ModContent.ItemType<_CONFIG_WarpedMirror>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.MysticMirror", "Mystic Mirror", ModContent.ItemType<_CONFIG_MysticMirror>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.RunicMirror", "Runic Mirror", ModContent.ItemType<_CONFIG_RunicMirror>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.SpiralMirror", "Spiral Mirror", ModContent.ItemType<_CONFIG_SpiralMirror>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.GemstoneMagnet", "Gemstone Magnet", ModContent.ItemType<_CONFIG_GemstoneMagnet>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.MagnificentMagnet", "Magnificent Magnet", ModContent.ItemType<_CONFIG_MagnificentMagnet>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.QuantumStrongbox", "Quantum Strongbox", ModContent.ItemType<_CONFIG_QuantumStrongbox>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.GoldenHorseshoeBalloon", "Golden Horseshoe Balloon", ModContent.ItemType<_CONFIG_GoldenHorseshoeBalloon>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.FlowerOfTheJungle", "Flower of the Jungle", ModContent.ItemType<_CONFIG_FlowerOfTheJungle>(), "ffffff");
            AddToggle("Mods.YAQOLM.Config.PrefixHammers", "Prefix Hammers", ModContent.ItemType<_CONFIG_PrefixHammers>(), "ffffff");
        }

        private void AddToggle(string toggle, string name, int item, string color) {
            ModTranslation text = LocalizationLoader.CreateTranslation(toggle);
            text.SetDefault($"[i:{item}] [c/{color}:{name}]");
            LocalizationLoader.AddTranslation(text);
        }
    }
}