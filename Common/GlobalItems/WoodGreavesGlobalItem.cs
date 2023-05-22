using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalItems;

public class WoodGreavesGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.WoodGreavesDefense;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.WoodGreaves;

	public override void SetDefaults(Item item) {
		item.defense = 1;
	}
}