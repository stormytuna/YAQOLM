using Terraria.ModLoader;

namespace YAQOLM.Common.Configs;

public abstract class ConfigItem : ModItem
{
	public override void SetStaticDefaults() {
		Tooltip.SetDefault("WARNING: This item is not real!\nIf you're seeing this through a cheating mod, just ignore it\nThis item simply allows the icon in the config to display, even when this item is unloaded");
	}
}

public class _CONFIG_WarpedMirror : ConfigItem { }

public class _CONFIG_MysticMirror : ConfigItem { }

public class _CONFIG_RunicMirror : ConfigItem { }

public class _CONFIG_SpiralMirror : ConfigItem { }

public class _CONFIG_GemstoneMagnet : ConfigItem { }

public class _CONFIG_MagnificentMagnet : ConfigItem { }

public class _CONFIG_QuantumStrongbox : ConfigItem { }

public class _CONFIG_GoldenHorseshoeBalloon : ConfigItem { }

public class _CONFIG_FlowerOfTheJungle : ConfigItem { }

public class _CONFIG_PrefixHammers : ConfigItem { }