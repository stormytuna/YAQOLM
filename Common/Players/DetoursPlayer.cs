using System.Linq;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players;

public class DetoursPlayer : ModPlayer
{
    private bool ResetAngler { get; set; }

    public override void Load() {
        // Subscribing in Load
        if (ServerConfig.Instance.MoreAnglerLoot) {
            On_Player.GetAnglerReward += MoreAnglerLoot;
        }

        if (ServerConfig.Instance.AnglerResetsImmediately) {
            On_Player.GetAnglerReward += ResetAnglerImmediately;
        }

        if (ServerConfig.Instance.BulkExtractinate) {
            On_Player.ExtractinatorUse += BulkExtractinate;
        }

        if (ServerConfig.Instance.BetterLifeFruit) {
            On_Player.ItemCheck_UseLifeFruit += BetterLifeFruit;
        }
    }

    private void MoreAnglerLoot(On_Player.orig_GetAnglerReward orig, Player self, NPC angler, int questItemType) {
        orig(self, angler, questItemType);
        self.anglerQuestsFinished++;
        orig(self, angler, questItemType);
    }

    private void ResetAnglerImmediately(On_Player.orig_GetAnglerReward orig, Player self, NPC angler, int questItemType) {
        orig(self, angler, questItemType);
        self.GetModPlayer<DetoursPlayer>().ResetAngler = true;
    }

    public override void PostUpdate() {
        if (ResetAngler) {
            ResetAngler = false;
            Main.AnglerQuestSwap();
        }
    }

    private void BulkExtractinate(On_Player.orig_ExtractinatorUse orig, Player self, int extractType, int extractinatorBlockType) {
        int maxExtractinations = Main.item.SkipLast(101).Count(item => !item.active);

        // Prevents us eating silt when we can't make any more items
        if (maxExtractinations == 0) {
            self.HeldItem.stack++;
        }

        for (int i = 0; i < maxExtractinations; i++) {
            orig(self, extractType, extractinatorBlockType);

            // Reduce our stack
            self.HeldItem.stack--;
            if (self.HeldItem.stack <= 0) {
                self.HeldItem.TurnToAir();
                return;
            }
        }
    }

    private void BetterLifeFruit(On_Player.orig_ItemCheck_UseLifeFruit orig, Player self, Item sItem) {
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