using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalItems;

public class SolutionGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.CheaperSolutions;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.ammo == AmmoID.Solution;

	public override void SetDefaults(Item item) {
		item.value = 5;
	}
}