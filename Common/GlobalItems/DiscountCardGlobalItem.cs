using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Helpers;

namespace YAQOLM.Common.GlobalItems;

public class DiscountCardGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.DiscountCard;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.DiscountCard;

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		TooltipLine newTooltip = new(Mod, "Tooltip1", Language.GetTextValue("Mods.YAQOLM.Items.DiscountCard.Tooltip"));
		tooltips.InsertTooltip(newTooltip, "Tooltip#");
	}
}