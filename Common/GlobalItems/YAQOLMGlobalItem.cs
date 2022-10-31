using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
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

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
            if (item.type == ItemID.KingSlimeBossBag && ServerConfig.Instance.KingSlimeDropsSlimeStaff) {
                itemLoot.Add(ItemDropRule.Common(ItemID.SlimeStaff, 4));
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (item.type == ItemID.Goggles && ServerConfig.Instance.GogglesGiveNightVision) {
                var tip = tooltips.FindLast(t => t.Mod == "Terraria");
                tip.Text += "\nGrants Night Owl";
                return;
            }

            if (item.type == ItemID.SharpeningStation && ServerConfig.Instance.BuffStationChanges) {
                var tip = tooltips.FirstOrDefault(t => t.Mod == "Terraria" && t.Name == "Tooltip0");
                tip.Text = "Increases melee damage and melee swing speed by 12%";
            }

            if (item.type == ItemID.AmmoBox && ServerConfig.Instance.BuffStationChanges) {
                var tip = tooltips.FirstOrDefault(t => t.Mod == "Terraria" && t.Name == "Tooltip0");
                tip.Text = "Increases ranged damage by 15% and reduces ammo usage by 40%";
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