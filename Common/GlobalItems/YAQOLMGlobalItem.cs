using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Players;
using YAQOLM.Common.Systems;

namespace YAQOLM.Common.GlobalItems {
    // Just using a generic global item class here since there's a few little things we need to do
    public class YAQOLMGlobalItem : GlobalItem {
        public override void SetDefaults(Item item) {
            if (item.type == ItemID.WoodGreaves && ServerConfig.Instance.WoodGreavesDefense) {
                item.defense = 1;
                return;
            }

            if (item.ammo == AmmoID.Solution && ServerConfig.Instance.CheaperSolutions) {
                item.value = 5;
                return;
            }

            if (item.type == ItemID.Wire && ServerConfig.Instance.CheaperWire) {
                item.value = 5;
                return;
            }

            if (item.type == ItemID.PinkGel && ServerConfig.Instance.PinkGelIsAmmo) {
                item.ammo = AmmoID.Gel;
                item.consumable = true;
                return;
            }
        }

        public override bool CanRightClick(Item item) {
            if (ArraySystem.ItemsThatPlaceTilesWithRecipes.Contains(item.type) && !Main.LocalPlayer.GetModPlayer<ConsumableCraftingStationsPlayer>().HasConsumedItem(item) && ServerConfig.Instance.InventoryCraftingStations) {
                return true;
            }

            return base.CanRightClick(item);
        }

        public override void RightClick(Item item, Player player) {
            if (ArraySystem.ItemsThatPlaceTilesWithRecipes.Contains(item.type) && !player.GetModPlayer<ConsumableCraftingStationsPlayer>().HasConsumedItem(item)) {
                player.GetModPlayer<ConsumableCraftingStationsPlayer>().ConsumeItem(item);
            }
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
            if (item.type == ItemID.KingSlimeBossBag && ServerConfig.Instance.KingSlimeDropsSlimeStaff) {
                itemLoot.Add(ItemDropRule.Common(ItemID.SlimeStaff, 4));
            }
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            if (player.PickAmmo(item, out _, out _, out _, out _, out int usedAmmoItemId, true)) {
                if (usedAmmoItemId == ItemID.PinkGel) {
                    damage = (int)((float)damage * 1.2f);
                }
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
                tip.Text = "Right click to increase melee damage and melee swing speed by 12%";
                return;
            }

            if (item.type == ItemID.AmmoBox && ServerConfig.Instance.BuffStationChanges) {
                var tip = tooltips.FirstOrDefault(t => t.Mod == "Terraria" && t.Name == "Tooltip0");
                tip.Text = "Right click to increase ranged damage by 15% and reduce ammo usage by 40%";
                return;
            }

            if (item.type == ItemID.CrystalBall && ServerConfig.Instance.BuffStationChanges) {
                var tip = tooltips.LastOrDefault(t => t.Mod == "Terraria");
                tip.Text += "\nRight click to increase magic damage by 10% and reduce mana usage by 8%";
                return;
            }

            if (item.type == ItemID.BewitchingTable && ServerConfig.Instance.BuffStationChanges) {
                var tip = tooltips.FirstOrDefault(t => t.Mod == "Terraria" && t.Name == "Tooltip0");
                tip.Text = "Right click to increase number of minions and sentries and increase minion damage by 15%";
                return;
            }

            if (item.type == ItemID.PinkGel && ServerConfig.Instance.PinkGelIsAmmo) {
                var tip = tooltips.FirstOrDefault(t => t.Mod == "Terraria" && t.Name == "Tooltip0");
                string temp = tip.Text;
                tip.Text = "When used as ammo, your weapon deals 20% more damage\n" + temp;
                return;
            }

            if (item.type == ItemID.DiscountCard && ServerConfig.Instance.DiscountCard) {
                var tip = tooltips.FirstOrDefault(t => t.Mod == "Terraria" && t.Name == "Tooltip0");
                tip.Text += "\nWorks while in your inventory or banks";
                return;
            }

            if (Main.LocalPlayer.GetModPlayer<ConsumableCraftingStationsPlayer>().HasConsumedItem(item) && ServerConfig.Instance.InventoryCraftingStations) {
                var tip = tooltips.LastOrDefault(t => t.Mod == "Terraria");
                int index = tooltips.IndexOf(tip) + 1;
                TooltipLine line = new(Mod, "CraftingStationConsumed", "[c/00FF00:Consumed]");
                tooltips.Insert(index, line);
            }
            else if (ArraySystem.ItemsThatPlaceTilesWithRecipes.Contains(item.type) && ServerConfig.Instance.InventoryCraftingStations) {
                var tip = tooltips.LastOrDefault(t => t.Mod == "Terraria");
                int index = tooltips.IndexOf(tip) + 1;
                TooltipLine line = new(Mod, "CraftingStationConsumed", $"[c/FF0000:Not consumed:] Right click to consume and unlock permanent access to {item.Name}");
                tooltips.Insert(index, line);
            }

            if (item.type == ItemID.LifeFruit && ServerConfig.Instance.BetterLifeFruit) {
                var tip = tooltips.FirstOrDefault(t => t.Name == "Tooltip0");
                tip.Text = "Permanently increases maximum life by 10";
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