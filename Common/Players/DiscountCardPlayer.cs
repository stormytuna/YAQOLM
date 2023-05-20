using Terraria.ID;
using Terraria.ModLoader;

namespace YAQOLM.Common.Players;

public class DiscountCardPlayer : ModPlayer
{
	public override void UpdateEquips() {
		if (Player.HasItemInInventories(ItemID.DiscountCard, out _, true, true, true)) {
			Player.discountAvailable = true;
		}
	}
}