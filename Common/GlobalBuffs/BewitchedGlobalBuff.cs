using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Buffs;

public class BewitchedGlobalBuff : GlobalBuff
{
	public override void Update(int type, Player player, ref int buffIndex) {
		if (type != BuffID.Bewitched || !ServerConfig.Instance.BuffStationChanges) {
			return;
		}

		// Do our stuff, technically compounding on vanilla 
		player.GetDamage(DamageClass.Summon) += 0.1f;
		player.maxTurrets += 1;
	}

	public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare) {
		if (type == BuffID.Bewitched && ServerConfig.Instance.BuffStationChanges) {
			tip = Language.GetTextValue("Mods.YAQOLM.Buffs.Bewitched");
		}
	}
}