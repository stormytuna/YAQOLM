using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Helpers;

namespace YAQOLM.Common.Players;

public class DiscountCardPlayer : ModPlayer
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.DiscountCard;

	public override void UpdateEquips()
	{
		if (Player.TryFindItem(ItemID.DiscountCard, out _, true, true, true))
		{
			Player.discount = true;
		}
	}
}