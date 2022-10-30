using System.Collections.Generic;
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
                return;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (item.type == ItemID.Goggles && ItemConfig.Instance.GogglesGiveNightVision) {
                var tip = tooltips.FindLast(t => t.Mod == "Terraria");
                tip.Text += "\nGrants Night Owl";
            }
        }

        public override void UpdateEquip(Item item, Player player) {
            if (item.type == ItemID.Goggles && ItemConfig.Instance.GogglesGiveNightVision) {
                player.AddBuff(BuffID.NightOwl, 2);
            }
        }
    }
}