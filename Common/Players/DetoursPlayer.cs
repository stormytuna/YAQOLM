using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players {
    public class DetoursPlayer : ModPlayer {
        public override void Load() {
            // Subscribing in Load
            if (ServerConfig.Instance.MoreAnglerLoot || ServerConfig.Instance.AnglerResetsImmediately) {
                On.Terraria.Player.GetAnglerReward += Player_GetAnglerReward;
            }
            if (ServerConfig.Instance.BulkExtractinate) {
                On.Terraria.Player.ExtractinatorUse += Player_ExtractinatorUse;
            }
            if (ServerConfig.Instance.BetterLifeFruit) {
                On.Terraria.Player.ItemCheck_UseLifeFruit += Player_ItemCheck_UseLifeFruit;
            }
        }

        public override void Unload() {
            // Unsubscribing in Unload
            if (ServerConfig.Instance.MoreAnglerLoot || ServerConfig.Instance.AnglerResetsImmediately) {
                On.Terraria.Player.GetAnglerReward -= Player_GetAnglerReward;
            }
            if (ServerConfig.Instance.BulkExtractinate) {
                On.Terraria.Player.ExtractinatorUse -= Player_ExtractinatorUse;
            }
            if (ServerConfig.Instance.BetterLifeFruit) {
                On.Terraria.Player.ItemCheck_UseLifeFruit -= Player_ItemCheck_UseLifeFruit;
            }
        }

        private void Player_GetAnglerReward(On.Terraria.Player.orig_GetAnglerReward orig, Player self, NPC angler) {
            // Just gives us double loot from angler
            if (ServerConfig.Instance.MoreAnglerLoot) {
                orig(self, angler);
                self.anglerQuestsFinished++;
                orig(self, angler);
            }
            else {
                orig(self, angler);
            }

            if (ServerConfig.Instance.AnglerResetsImmediately) {
                Main.AnglerQuestSwap();
            }
        }

        private void Player_ExtractinatorUse(On.Terraria.Player.orig_ExtractinatorUse orig, Player self, int extractType) {
            // Extractinate one by one, removing from the stack as we do
            // If Main.item gets 3/4 full, we should stop
            while (self.HeldItem.stack > 0) {
                // Call orig
                orig(self, extractType);

                // Reduce our stack
                self.HeldItem.stack--;
                if (self.HeldItem.stack <= 0)
                    self.HeldItem.TurnToAir();

                // Check if we should break our loop
                int numItems = 0;
                for (int i = 0; i < Main.item.Length; i++) {
                    if (Main.item[i].active) {
                        numItems++;
                    }
                }
                if (numItems > 300) {
                    break;
                }
            }
        }

        private void Player_ItemCheck_UseLifeFruit(On.Terraria.Player.orig_ItemCheck_UseLifeFruit orig, Player self, Item sItem) {
            if (sItem.type == ItemID.LifeFruit && self.itemAnimation > 0 && self.statLifeMax >= 400 && self.statLifeMax < 500 && self.ItemTimeIsZero) {
                //self.ApplyItemTime(sItem);
                self.statLifeMax += 5;
                self.statLifeMax2 += 5;
                self.statLife += 5;
                if (self.statLifeMax > 500) {
                    self.statLifeMax = 500;
                }
                if (Main.myPlayer == self.whoAmI) {
                    self.HealEffect(5);
                }
                AchievementsHelper.HandleSpecialEvent(self, 2);
            }

            orig(self, sItem);
        }
    }
}