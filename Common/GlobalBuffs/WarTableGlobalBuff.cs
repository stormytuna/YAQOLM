using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalBuffs;

public class WarTableGlobalBuff : GlobalBuff
{
	public override void Update(int type, Player player, ref int buffIndex) {
		if (type != BuffID.WarTable || !ServerConfig.Instance.BuffStationChanges) {
			return;
		}

		// Do our stuff, technically compounding on vanilla
		player.GetDamage(DamageClass.Summon) += 0.05f;
	}

	public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare) {
		if (type == BuffID.WarTable && ServerConfig.Instance.BuffStationChanges) {
			tip = Language.GetTextValue("Mods.YAQOLM.Buffs.WarTable");
		}
	}
}