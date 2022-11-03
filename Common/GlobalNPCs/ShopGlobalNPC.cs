using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Common.GlobalNPCs {
    public class ShopGlobalNPC : GlobalNPC {
        public override bool InstancePerEntity => true;

        public override void SetupShop(int type, Chest shop, ref int nextSlot) {
            if (type == NPCID.Steampunker && ServerConfig.Instance.SteampunkerSolutions) {
                List<Item> inventory = shop.item.ToList(); // Easier to insert into lists

                Item firstSolution = inventory.FirstOrDefault(i => i.ammo == AmmoID.Solution);
                int index = 1; // Default index is immediately after clentaminator in case we dont find one
                if (firstSolution != null) {
                    index = inventory.IndexOf(firstSolution) + 1;
                }

                for (int i = 0; i < ArraySystem.SolutionIds.Length; i++) {
                    int solutionId = ArraySystem.SolutionIds[i];
                    Item solution = inventory.FirstOrDefault(i => i.type == solutionId);
                    // Edge case - solution we found is our first solution, just continue in this case
                    if (solution == firstSolution) {
                        continue;
                    }
                    // If steampunker isnt already selling this solution, insert it
                    if (solution == null) {
                        inventory.Insert(index, new(solutionId));
                        inventory[index].isAShopItem = true;
                        index++;
                        nextSlot++;
                    }
                    // If steampunker is selling this solution, move it
                    else {
                        inventory.Remove(solution);
                        inventory.Insert(index, solution);
                        index++;
                        // Don't increment nextSlot here since we haven't actually added anything to the shop
                    }
                }

                shop.item = inventory.ToArray();
                return;
            }

            if (type == NPCID.DyeTrader && ServerConfig.Instance.DyeTraderSellsSusDyes) {
                #region Dyes

                switch (Main.GetMoonPhase()) {
                    case MoonPhase.Full:
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.RedDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.OrangeDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ShiftingSandsDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.TwilightDye, ItemID.BrownDye, 1);
                        if (NPC.downedPlantBoss) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.UnicornWispDye, ItemID.BrownDye, 1); }
                        if (NPC.downedMartians) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.MartianArmorDye, ItemID.BrownDye, 1); }
                        return;
                    case MoonPhase.ThreeQuartersAtLeft:
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.YellowDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.LimeDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ReflectiveSilverDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ShadowDye, ItemID.BrownDye, 1);
                        if (NPC.downedMechBossAny) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.LivingOceanDye, ItemID.BrownDye, 1); }
                        if (NPC.downedMartians) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.MidnightRainbowDye, ItemID.BrownDye, 1); }
                        return;
                    case MoonPhase.HalfAtLeft:
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.GreenDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.TealDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ReflectiveGoldDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ReflectiveObsidianDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ReflectiveMetalDye, ItemID.BrownDye, 1);
                        if (NPC.downedMechBossAny) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.LivingFlameDye, ItemID.BrownDye, 1); }
                        return;
                    case MoonPhase.QuarterAtLeft:
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.CyanDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.SkyBlueDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.PurpleOozeDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ReflectiveDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ReflectiveCopperDye, ItemID.BrownDye, 1);
                        if (NPC.downedMechBossAny) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.LivingRainbowDye, ItemID.BrownDye, 1); }
                        return;
                    case MoonPhase.Empty:
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.BlueDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.PurpleDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.MirageDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.NegativeDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.PhaseDye, ItemID.BrownDye, 1);
                        if (NPC.downedMechBossAny) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ChlorophyteDye, ItemID.BrownDye, 1); }
                        return;
                    case MoonPhase.QuarterAtRight:
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.VioletDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.HadesDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.BurningHadesDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.ShadowflameHadesDye, ItemID.BrownDye, 1);
                        if (NPC.downedPlantBoss) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.PixieDye, ItemID.BrownDye, 1); }
                        return;
                    case MoonPhase.HalfAtRight:
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.PinkDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.GelDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.MushroomDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.GrimDye, ItemID.BrownDye, 1);
                        if (NPC.downedPlantBoss) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.WispDye, ItemID.BrownDye, 1); }
                        if (NPC.downedMoonlord) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.DevDye, ItemID.BrownDye, 1); }
                        return;
                    case MoonPhase.ThreeQuartersAtRight:
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.BlackDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.AcidDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.BlueAcidDye, ItemID.BrownDye, 1);
                        stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.RedAcidDye, ItemID.BrownDye, 1);
                        if (NPC.downedPlantBoss) { stormytunaUtils.AddToShop(ref shop, ref nextSlot, ItemID.InfernalWispDye, ItemID.BrownDye, 1); }
                        return;
                }

                #endregion
            }
        }
    }
}