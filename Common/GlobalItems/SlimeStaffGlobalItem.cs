using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalItems;

public class SlimeStaffGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.KingSlimeDropsSlimeStaff;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.KingSlimeBossBag;

	public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
		itemLoot.Add(ItemDropRule.Common(ItemID.SlimeStaff, 4));
	}
}