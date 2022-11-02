using Terraria.ModLoader;
using YAQOLM.Content.Items;

namespace YAQOLM {
    public class YAQOLM : Mod {
        public override void Load() {
            AddToggle("Mods.YAQOLM.Config.WarpedMirror", "Warped Mirror", ModContent.ItemType<WarpedMirror>(), "ffffff");
        }

        private void AddToggle(string toggle, string name, int item, string color) {
            ModTranslation text = LocalizationLoader.CreateTranslation(toggle);
            text.SetDefault($"[i:{item}] [c/{color}:{name}]");
            LocalizationLoader.AddTranslation(text);
        }
    }
}