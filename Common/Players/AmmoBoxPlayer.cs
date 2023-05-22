using Terraria;
using Terraria.ModLoader;

namespace YAQOLM.Common.Players;

public class AmmoBoxPlayer : ModPlayer
{
	public bool ammoBox;

	public override void ResetEffects() {
		ammoBox = false;
	}

	public override bool CanConsumeAmmo(Item weapon, Item ammo) => ammoBox && Main.rand.NextFloat() < 0.4f;
}