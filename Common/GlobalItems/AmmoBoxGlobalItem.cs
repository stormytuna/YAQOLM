using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Helpers;

namespace YAQOLM.Common.GlobalItems;

public class AmmoBoxGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.BuffStationChanges;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.AmmoBox;

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		TooltipLine newTooltip = new(Mod, "Tooltip0", Language.GetTextValue("Mods.YAQOLM.Items.AmmoBox.Tooltip"));
		tooltips.ReplaceTooltip(newTooltip, "Tooltip0");
	}
}