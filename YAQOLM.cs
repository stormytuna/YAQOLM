using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM;

public class YAQOLM : Mod
{
	public override void Load() {
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.WarpedMirror", "Warped Mirror", ModContent.ItemType<_CONFIG_WarpedMirror>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.MysticMirror", "Mystic Mirror", ModContent.ItemType<_CONFIG_MysticMirror>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.RunicMirror", "Runic Mirror", ModContent.ItemType<_CONFIG_RunicMirror>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.SpiralMirror", "Spiral Mirror", ModContent.ItemType<_CONFIG_SpiralMirror>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.GemstoneMagnet", "Gemstone Magnet", ModContent.ItemType<_CONFIG_GemstoneMagnet>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.MagnificentMagnet", "Magnificent Magnet", ModContent.ItemType<_CONFIG_MagnificentMagnet>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.QuantumStrongbox", "Quantum Strongbox", ModContent.ItemType<_CONFIG_QuantumStrongbox>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.GoldenHorseshoeBalloon", "Golden Horseshoe Balloon", ModContent.ItemType<_CONFIG_GoldenHorseshoeBalloon>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.FlowerOfTheJungle", "Flower of the Jungle", ModContent.ItemType<_CONFIG_FlowerOfTheJungle>(), "ffffff");
		AddToggle("Mods.YAQOLM.Configs.ServerConfig.PrefixHammers", "Prefix Hammers", ModContent.ItemType<_CONFIG_PrefixHammers>(), "ffffff");
	}

	private void AddToggle(string toggle, string name, int item, string color) => Language.GetOrRegister(toggle, () => $"[i:{item}] [c/{color}:{name}]");
}