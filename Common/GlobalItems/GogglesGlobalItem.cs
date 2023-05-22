using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Helpers;

namespace YAQOLM.Common.GlobalItems;

public class GogglesGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.GogglesGiveNightVision;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.Goggles;

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		TooltipLine newTooltip = new(Mod, "Tooltip0", Language.GetTextValue("Mods.YAQOLM.Items.Goggles.Tooltip"));
		tooltips.InsertTooltip(newTooltip, "Material");
	}

	public override void UpdateEquip(Item item, Player player) {
		player.AddBuff(BuffID.NightOwl, 2);
	}
}