using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Players;

namespace YAQOLM.Common.GlobalItems;

public class RainArmorGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.RainArmorSetBonus;

	public override string IsArmorSet(Item head, Item body, Item legs) {
		if (head.type == ItemID.RainHat && body.type == ItemID.RainCoat) {
			return "RainArmor";
		}

		return base.IsArmorSet(head, body, legs);
	}

	public override void UpdateArmorSet(Player player, string set) {
		if (set != "RainArmor") {
			return;
		}

		player.setBonus = Language.GetTextValue("Mods.YAQOLM.SetBonuses.RainArmor");
		player.GetCritChance(DamageClass.Generic) += 0.05f;
		player.GetDamage(DamageClass.Generic) += 0.08f;
	}
}