using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Players;

namespace YAQOLM.Common.GlobalBuffs;

public class AmmoBoxGlobalBuff : GlobalBuff
{
	public override void Update(int type, Player player, ref int buffIndex) {
		if (type != BuffID.AmmoBox || !ServerConfig.Instance.BuffStationChanges) {
			return;
		}

		// Undo what vanilla does
		player.ammoBox = false;

		// Do our stuff
		player.GetDamage(DamageClass.Ranged) += 0.15f;
		player.GetModPlayer<AmmoBoxPlayer>().ammoBox = true;
	}

	public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare) {
		if (type == BuffID.AmmoBox && ServerConfig.Instance.BuffStationChanges) {
			tip = Language.GetTextValue("Mods.YAQOLM.Buffs.AmmoBox");
		}
	}
}