using Terraria.ModLoader;

namespace YAQOLM.Common.Systems;

public class KeybindSystem : ModSystem
{
	public static ModKeybind RodOfDiscordKB { get; private set; }
	public static ModKeybind SpiralMirrorHomeKB { get; private set; }
	public static ModKeybind SpiralMirrorGraveKB { get; private set; }
	public static ModKeybind SpiralMirrorReturnKB { get; private set; }
	public static ModKeybind QuantumStrongboxKB { get; private set; }

	public override void Load() {
		RodOfDiscordKB = KeybindLoader.RegisterKeybind(Mod, "RodOfDiscord", "F");
		SpiralMirrorHomeKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorHome", "Y");
		SpiralMirrorGraveKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorGrave", "H");
		SpiralMirrorReturnKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorReturn", "N");
		QuantumStrongboxKB = KeybindLoader.RegisterKeybind(Mod, "QuantumStrongbox", "U");
	}

	public override void Unload() {
		RodOfDiscordKB = null;
		SpiralMirrorHomeKB = null;
		SpiralMirrorGraveKB = null;
		SpiralMirrorReturnKB = null;
		QuantumStrongboxKB = null;
	}
}