using System.Linq;
using On.Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players;

public class DetoursPlayer : ModPlayer
{
	public override void Load()
	{
		// Subscribing in Load
		if (ServerConfig.Instance.MoreAnglerLoot || ServerConfig.Instance.AnglerResetsImmediately)
		{
			On_Player.GetAnglerReward += On_PlayerOnGetAnglerReward;
		}

		if (ServerConfig.Instance.BulkExtractinate)
		{
			On_Player.ExtractinatorUse += On_PlayerOnExtractinatorUse;
		}

		if (ServerConfig.Instance.BetterLifeFruit)
		{
			On_Player.ItemCheck_UseLifeFruit += On_PlayerOnItemCheck_UseLifeFruit;
		}
	}

	private void Player_GetAnglerReward(Player.orig_GetAnglerReward orig, Terraria.Player self, NPC angler)
	{
		// Just gives us double loot from angler
		if (ServerConfig.Instance.MoreAnglerLoot)
		{
			orig(self, angler, questitemtype);
			self.anglerQuestsFinished++;
			orig(self, angler, questitemtype);
		}
		else
		{
			orig(self, angler, questitemtype);
		}

		if (ServerConfig.Instance.AnglerResetsImmediately)
		{
			Main.AnglerQuestSwap();
		}
	}

	private void Player_ExtractinatorUse(Player.orig_ExtractinatorUse orig, Terraria.Player self, int extractType)
	{
		int maxExtractinations = Main.item.SkipLast(101).Count(item => !item.active);

		// Prevents us eating silt when we can't make any more items
		if (maxExtractinations == 0)
		{
			self.HeldItem.stack++;
		}

		for (int i = 0; i < maxExtractinations; i++)
		{
			// Call orig
			orig(self, extractType, extractinatorblocktype);

			// Reduce our stack
			self.HeldItem.stack--;
			if (self.HeldItem.stack <= 0)
			{
				self.HeldItem.TurnToAir();
				return;
			}
		}
	}

	private void On_PlayerOnItemCheck_UseLifeFruit(On_Player.orig_ItemCheck_UseLifeFruit orig, Player self, Item sItem)
	{
		if (sItem.type == ItemID.LifeFruit && self.itemAnimation > 0 && self.statLifeMax >= 400 && self.statLifeMax < 500 && self.ItemTimeIsZero)
		{
			//self.ApplyItemTime(sItem);
			self.statLifeMax += 5;
			self.statLifeMax2 += 5;
			self.statLife += 5;
			if (self.statLifeMax > 500)
			{
				self.statLifeMax = 500;
			}

			if (Main.myPlayer == self.whoAmI)
			{
				self.HealEffect(5);
			}

			AchievementsHelper.HandleSpecialEvent(self, 2);
		}

		orig(self, sItem);
	}
}