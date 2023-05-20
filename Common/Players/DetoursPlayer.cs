using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players;

public class DetoursPlayer : ModPlayer
{
	public override void Load() {
		// Subscribing in Load
		if (ServerConfig.Instance.MoreAnglerLoot || ServerConfig.Instance.AnglerResetsImmediately) {
			On_Player.GetAnglerReward += On_PlayerOnGetAnglerReward;
		}

		if (ServerConfig.Instance.BulkExtractinate) {
			On_Player.ExtractinatorUse += On_PlayerOnExtractinatorUse;
		}

		if (ServerConfig.Instance.BetterLifeFruit) {
			On_Player.ItemCheck_UseLifeFruit += On_PlayerOnItemCheck_UseLifeFruit;
		}
	}

	private void On_PlayerOnGetAnglerReward(On_Player.orig_GetAnglerReward orig, Player self, NPC angler, int questitemtype) {
		if (ServerConfig.Instance.MoreAnglerLoot) {
			orig(self, angler, questitemtype);
			self.anglerQuestsFinished++;
			orig(self, angler, questitemtype);
		} else {
			orig(self, angler, questitemtype);
		}

		if (ServerConfig.Instance.AnglerResetsImmediately) {
			Main.AnglerQuestSwap();
		}
	}

	private void On_PlayerOnExtractinatorUse(On_Player.orig_ExtractinatorUse orig, Player self, int extractType, int extractinatorblocktype) {
		while (self.HeldItem.stack > 0) {
			// Call orig
			orig(self, extractType, extractinatorblocktype);

			// Reduce our stack
			self.HeldItem.stack--;
			if (self.HeldItem.stack <= 0) {
				self.HeldItem.TurnToAir();
			}

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

	private void On_PlayerOnItemCheck_UseLifeFruit(On_Player.orig_ItemCheck_UseLifeFruit orig, Player self, Item sItem) {
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