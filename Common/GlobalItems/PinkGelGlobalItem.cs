using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Helpers;

namespace YAQOLM.Common.GlobalItems;

public class PinkGelGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.PinkGelIsAmmo;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.PinkGel;

	public override void SetDefaults(Item item) {
		item.ammo = AmmoID.Gel;
		item.consumable = true;
	}

	public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		if (player.PickAmmo(item, out _, out _, out _, out _, out int usedAmmoItemId, true) && usedAmmoItemId == ItemID.PinkGel) {
			damage = (int)(damage * 1.2f);
		}
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		TooltipLine newTooltip = new(Mod, "Tooltip0", Language.GetTextValue("Mods.YAQOLM.Items.PinkGel.Tooltip"));
		tooltips.InsertTooltip(newTooltip, "Material");
	}
}