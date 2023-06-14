using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Helpers;

namespace YAQOLM.Common.GlobalNPCs;

public class ShopGlobalNPC : GlobalNPC
{
	public override void ModifyShop(NPCShop shop) {
		if (shop.NpcType == NPCID.Steampunker && ServerConfig.Instance.SteampunkerSolutions) {
			foreach (NPCShop.Entry entry in shop.Entries.Where(entry => entry.Item.ammo == AmmoID.Solution && entry.Item.IsVanillaItem())) {
				entry.Conditions.RemoveAllHack();
			}
		}

		if (shop.NpcType == NPCID.DyeTrader && ServerConfig.Instance.DyeTraderSellsSusDyes) {
			Condition moonPhaseFull = new("", () => Main.GetMoonPhase() == MoonPhase.Full);
			shop.Add(ItemID.RedDye, moonPhaseFull);
			shop.Add(ItemID.OrangeDye, moonPhaseFull);
			shop.Add(ItemID.ShiftingSandsDye, moonPhaseFull);
			shop.Add(ItemID.TwilightDye, moonPhaseFull);
			shop.Add(ItemID.UnicornWispDye, moonPhaseFull, Condition.DownedPlantera);
			shop.Add(ItemID.MartianArmorDye, moonPhaseFull, Condition.DownedMartians);

			Condition moonPhaseThreeQuartersAtLeft = new("", () => Main.GetMoonPhase() == MoonPhase.ThreeQuartersAtLeft);
			shop.Add(ItemID.YellowDye, moonPhaseThreeQuartersAtLeft);
			shop.Add(ItemID.LimeDye, moonPhaseThreeQuartersAtLeft);
			shop.Add(ItemID.ReflectiveSilverDye, moonPhaseThreeQuartersAtLeft);
			shop.Add(ItemID.ShadowDye, moonPhaseThreeQuartersAtLeft);
			shop.Add(ItemID.LivingOceanDye, moonPhaseThreeQuartersAtLeft, Condition.DownedMechBossAny);
			shop.Add(ItemID.MidnightRainbowDye, moonPhaseThreeQuartersAtLeft, Condition.DownedMartians);

			Condition moonPhaseHalfAtLeft = new("", () => Main.GetMoonPhase() == MoonPhase.HalfAtLeft);
			shop.Add(ItemID.GreenDye, moonPhaseHalfAtLeft);
			shop.Add(ItemID.TealDye, moonPhaseHalfAtLeft);
			shop.Add(ItemID.ReflectiveGoldDye, moonPhaseHalfAtLeft);
			shop.Add(ItemID.ReflectiveObsidianDye, moonPhaseHalfAtLeft);
			shop.Add(ItemID.ReflectiveMetalDye, moonPhaseHalfAtLeft);
			shop.Add(ItemID.LivingFlameDye, moonPhaseHalfAtLeft, Condition.DownedMechBossAny);

			Condition moonPhaseQuarterAtLeft = new("", () => Main.GetMoonPhase() == MoonPhase.QuarterAtLeft);
			shop.Add(ItemID.CyanDye, moonPhaseQuarterAtLeft);
			shop.Add(ItemID.SkyBlueDye, moonPhaseQuarterAtLeft);
			shop.Add(ItemID.PurpleOozeDye, moonPhaseQuarterAtLeft);
			shop.Add(ItemID.ReflectiveDye, moonPhaseQuarterAtLeft);
			shop.Add(ItemID.ReflectiveCopperDye, moonPhaseQuarterAtLeft);
			shop.Add(ItemID.LivingRainbowDye, moonPhaseQuarterAtLeft, Condition.DownedMechBossAny);

			Condition moonPhaseEmpty = new("", () => Main.GetMoonPhase() == MoonPhase.Empty);
			shop.Add(ItemID.BlueDye, moonPhaseEmpty);
			shop.Add(ItemID.PurpleDye, moonPhaseEmpty);
			shop.Add(ItemID.MirageDye, moonPhaseEmpty);
			shop.Add(ItemID.NegativeDye, moonPhaseEmpty);
			shop.Add(ItemID.PhaseDye, moonPhaseEmpty);
			shop.Add(ItemID.ChlorophyteDye, moonPhaseEmpty, Condition.DownedMechBossAny);

			Condition moonPhaseQuarterAtRight = new("", () => Main.GetMoonPhase() == MoonPhase.QuarterAtRight);
			shop.Add(ItemID.VioletDye, moonPhaseQuarterAtRight);
			shop.Add(ItemID.HadesDye, moonPhaseQuarterAtRight);
			shop.Add(ItemID.BurningHadesDye, moonPhaseQuarterAtRight);
			shop.Add(ItemID.ShadowflameHadesDye, moonPhaseQuarterAtRight);
			shop.Add(ItemID.PixieDye, moonPhaseQuarterAtRight, Condition.DownedPlantera);

			Condition moonPhaseHalfAtRight = new("", () => Main.GetMoonPhase() == MoonPhase.HalfAtRight);
			shop.Add(ItemID.PinkDye, moonPhaseHalfAtRight);
			shop.Add(ItemID.GelDye, moonPhaseHalfAtRight);
			shop.Add(ItemID.MushroomDye, moonPhaseHalfAtRight);
			shop.Add(ItemID.GrimDye, moonPhaseHalfAtRight);
			shop.Add(ItemID.WispDye, moonPhaseHalfAtRight, Condition.DownedPlantera);
			shop.Add(ItemID.DevDye, moonPhaseHalfAtRight, Condition.DownedMoonLord);

			Condition moonPhaseThreeQuartersAtRight = new("", () => Main.GetMoonPhase() == MoonPhase.ThreeQuartersAtRight);
			shop.Add(ItemID.BlackDye, moonPhaseThreeQuartersAtRight);
			shop.Add(ItemID.AcidDye, moonPhaseThreeQuartersAtRight);
			shop.Add(ItemID.BlueAcidDye, moonPhaseThreeQuartersAtRight);
			shop.Add(ItemID.RedAcidDye, moonPhaseThreeQuartersAtRight);
			shop.Add(ItemID.InfernalWispDye, moonPhaseThreeQuartersAtRight, Condition.DownedPlantera);
		}

		if (type == NPCID.DD2Bartender && ServerConfig.Instance.CheaperDefendersForge) {
			Item defendersForge = shop.item.FirstOrDefault(i => i.type == ItemID.DefendersForge);
			if (defendersForge is not null) {
				defendersForge.shopCustomPrice = 15;
			}
		}
	}
}