using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalItems;

public class WireGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.CheaperWire;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.Wire;

	public override void SetDefaults(Item item) => item.value = 5;
}