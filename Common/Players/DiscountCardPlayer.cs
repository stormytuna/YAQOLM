using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players;

public class DiscountCardPlayer : ModPlayer
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.DiscountCard;

	public override void UpdateEquips() {
		if (Player.HasItemInInventories(ItemID.DiscountCard, out _, true, true, true)) {
			Player.discountAvailable = true;
		}
	}
}