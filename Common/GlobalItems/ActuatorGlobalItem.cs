using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalItems;

public class ActuatorGlobalItem : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.CheaperActuators;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.Actuator;

	public override void SetDefaults(Item item) => item.value = 20;
}