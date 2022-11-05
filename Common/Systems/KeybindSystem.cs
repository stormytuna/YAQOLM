using Terraria.ModLoader;

namespace YAQOLM.Common.Systems {
    public class KeybindSystem : ModSystem {
        public static ModKeybind RodOfDiscordKB { get; private set; }
        public static ModKeybind SpiralMirrorHomeKB { get; private set; }
        public static ModKeybind SpiralMirrorGraveKB { get; private set; }
        public static ModKeybind SpiralMirrorReturnKB { get; private set; }
        public static ModKeybind QuantumStrongboxPiggyBankKB { get; private set; }
        public static ModKeybind QuantumStrongboxSafeKB { get; private set; }
        public static ModKeybind QuantumStrongboxDefendersForgeKB { get; private set; }
        public static ModKeybind QuantumStrongboxVoidVaultKB { get; private set; }

        public override void Load() {
            RodOfDiscordKB = KeybindLoader.RegisterKeybind(Mod, "RodOfDiscord", "F");
            SpiralMirrorHomeKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorHome", "Y");
            SpiralMirrorGraveKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorGrave", "H");
            SpiralMirrorReturnKB = KeybindLoader.RegisterKeybind(Mod, "SpiralMirrorReturn", "N");
            QuantumStrongboxPiggyBankKB = KeybindLoader.RegisterKeybind(Mod, "QuantumStrongboxPiggyBank", "U");
            QuantumStrongboxSafeKB = KeybindLoader.RegisterKeybind(Mod, "QuantumStrongboxSafe", "I");
            QuantumStrongboxDefendersForgeKB = KeybindLoader.RegisterKeybind(Mod, "QuantumStrongboxDefendersForge", "J");
            QuantumStrongboxVoidVaultKB = KeybindLoader.RegisterKeybind(Mod, "QuantumStrongboxVoidVault", "K");
        }

        public override void Unload() {
            RodOfDiscordKB = null;
            SpiralMirrorHomeKB = null;
            SpiralMirrorGraveKB = null;
            SpiralMirrorReturnKB = null;
            QuantumStrongboxPiggyBankKB = null;
            QuantumStrongboxSafeKB = null;
            QuantumStrongboxDefendersForgeKB = null;
            QuantumStrongboxVoidVaultKB = null;
        }
    }
}
