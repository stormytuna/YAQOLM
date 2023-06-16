using Terraria.ModLoader;

namespace YAQOLM.Common.Systems;

public class KeybindSystem : ModSystem
{
	public static ModKeybind RodOfDiscordKB { get; private set; }
	public static ModKeybind SpiralMirrorHomeKB { get; private set; }
	public static ModKeybind SpiralMirrorGraveKB { get; private set; }
	public static ModKeybind SpiralMirrorReturnKB { get; private set; }
	public static ModKeybind SpiralMirrorOceanKB { get; private set; }
	public static ModKeybind SpiralMirrorHellKB { get; private set; }

	public override void Load() {
		RodOfDiscordKB = KeybindLoader.RegisterKeybind(Mod, "RodOfDiscord", "F");
		SpiralMirrorHomeKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorHome", "Y");
		SpiralMirrorGraveKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorGrave", "H");
		SpiralMirrorReturnKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorReturn", "N");
		SpiralMirrorOceanKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorOcean", "M");
		SpiralMirrorHellKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorHell", "J");
	}

	public override void Unload() {
		RodOfDiscordKB = null;
		SpiralMirrorHomeKB = null;
		SpiralMirrorGraveKB = null;
		SpiralMirrorReturnKB = null;
		SpiralMirrorOceanKB = null;
		SpiralMirrorHellKB = null;
	}
}