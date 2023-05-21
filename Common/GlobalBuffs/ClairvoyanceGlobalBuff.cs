using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Buffs;

public class ClairvoyanceGlobalBuff : GlobalBuff
{
	public override void Update(int type, Player player, ref int buffIndex) {
		if (type != BuffID.Clairvoyance || !ServerConfig.Instance.BuffStationChanges) {
			return;
		}

		// Undo what vanilla does
		player.GetCritChance(DamageClass.Magic) -= 2;
		player.statManaMax2 -= 20;

		// Do our stuff, technically compounding on vanilla 
		player.GetDamage(DamageClass.Magic) += 0.05f;
		player.manaCost -= 0.06f;
	}

	public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare) {
		if (type == BuffID.Clairvoyance && ServerConfig.Instance.BuffStationChanges) {
			tip = Language.GetTextValue("Mods.YAQOLM.Buffs.Clairvoyance");
		}
	}
}