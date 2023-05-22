using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Players;
using YAQOLM.Common.Systems;
using YAQOLM.Helpers;

namespace YAQOLM.Common.GlobalItems;

public class ConsumableCraftingsStationGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.InventoryCraftingStations;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => ArraySystem.ItemsThatPlaceTilesWithRecipes.Contains(entity.type);

	public override bool CanRightClick(Item item) => true;

	public override void RightClick(Item item, Player player) => player.GetModPlayer<ConsumableCraftingStationsPlayer>().ConsumeItem(item);

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		var modPlayer = Main.LocalPlayer.GetModPlayer<ConsumableCraftingStationsPlayer>();

		string tooltipText = modPlayer.HasConsumedItem(item) ? Language.GetTextValue("Mods.YAQOLM.Misc.Consumed") : Language.GetTextValue("Mods.YAQOLM.Misc.NotConsumed");
		TooltipLine newTooltip = new(Mod, "CraftingStationConsumed", tooltipText);
		tooltips.InsertTooltip(newTooltip, "Material");
	}
}