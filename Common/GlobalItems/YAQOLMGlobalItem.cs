using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalItems {
    // Just using a generic global item class here since there's a few little things we need to do
    public class YAQOLMGlobalItem : GlobalItem {
        public override void SetDefaults(Item item) {
            if (item.type == ItemID.WoodGreaves && ItemConfig.Instance.WoodGreavesDefense) {
                item.defense = 1;
            }
        }
    }
}