using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Players;

namespace YAQOLM.Common.GlobalItems {
    // Just using a generic global item class here since there's a few little things we need to do
    public class YAQOLMGlobalItem : GlobalItem {
        public override void SetDefaults(Item item) {
            if (item.type == ItemID.WoodGreaves && ServerConfig.Instance.WoodGreavesDefense) {
                item.defense = 1;
                return;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (item.type == ItemID.Goggles && ServerConfig.Instance.GogglesGiveNightVision) {
                var tip = tooltips.FindLast(t => t.Mod == "Terraria");
                tip.Text += "\nGrants Night Owl";
            }
        }

        public override void UpdateEquip(Item item, Player player) {
            if (item.type == ItemID.Goggles && ServerConfig.Instance.GogglesGiveNightVision) {
                player.AddBuff(BuffID.NightOwl, 2);
            }
        }

        public override string IsArmorSet(Item head, Item body, Item legs) {
            if (head.type == ItemID.RainHat && body.type == ItemID.RainCoat && ServerConfig.Instance.RainArmorSetBonus) {
                return "RainArmor";
            }

            return base.IsArmorSet(head, body, legs);
        }

        public override void UpdateArmorSet(Player player, string set) {
            if (set == "RainArmor") {
                player.setBonus = "8% increased damage and 5% increased critical strike chance";
                player.GetModPlayer<ArmorSetPlayer>().rainArmor = true;
            }
        }
    }
}