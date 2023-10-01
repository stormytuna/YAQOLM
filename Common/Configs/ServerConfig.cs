using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace YAQOLM.Common.Configs;

public class ServerConfig : ModConfig
{
	public static ServerConfig Instance;

	public override ConfigScope Mode => ConfigScope.ServerSide;

	/* ================ */
	/*   NEW CONTENT    */
	/* ================ */

	[Header("NewContent")]
	[ReloadRequired]
	[DefaultValue(true)]
	public bool WarpedMirror { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool MysticMirror { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool RunicMirror { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool SpiralMirror { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool GemstoneMagnet { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool MagnificentMagnet { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool QuantumStrongbox { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool GoldenHorseshoeBalloon { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool FlowerOfTheJungle { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool PrefixHammers { get; set; }

	/* ================ */
	/*     RECIPES      */
	/* ================ */

	[Header("Recipes")]
	[ReloadRequired]
	[DefaultValue(true)]
	public bool RodOfDiscord { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool SnowGlobe { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool MoneyTrough { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool Bait { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool BottomlessBuckets { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool MagicMirror { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool Cascade { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool Extractinator { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool LuminiteSmeltingRecipes { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool BeetleArmorOnlyBeetle { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool CheaperOre { get; set; }

	/* ================ */
	/*      ITEMS       */
	/* ================ */

	[Header("Items")]
	[ReloadRequired]
	[DefaultValue(true)]
	public bool WoodGreavesDefense { get; set; }

	[DefaultValue(true)]
	public bool GogglesGiveNightVision { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool RainArmorSetBonus { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool CheaperSolutions { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool CheaperWire { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool CheaperActuators { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool CheaperDefendersForge { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool PinkGelIsAmmo { get; set; }

	[DefaultValue(true)]
	public bool DiscountCard { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool BetterLifeFruit { get; set; }

	/* ================ */
	/*       NPCS       */
	/* ================ */

	[Header("NPCsAndDrops")]
	[DefaultValue(true)]
	public bool LunarPillarRework { get; set; }

	[DefaultValue(true)]
	public bool SteampunkerSolutions { get; set; }

	[DefaultValue(true)]
	public bool DyeTraderSellsSusDyes { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool TravellingMerchantKeepsGreyZapinator { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool MoreAnglerLoot { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool AnglerResetsImmediately { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool KingSlimeDropsSlimeStaff { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool SharksDropSharktoothNecklace { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool OneFromOptionsToFewFromOptions { get; set; }

	/* ================ */
	/*       MISC       */
	/* ================ */

	[Header("Misc")]
	[ReloadRequired]
	[DefaultValue(true)]
	public bool BuffStationChanges { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool BulkExtractinate { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool BossesIncreaseHappiness { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool BetterBombTiles { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool BetterBombWalls { get; set; }

	[ReloadRequired]
	[DefaultValue(true)]
	public bool InventoryCraftingStations { get; set; }

	[DefaultValue(true)]
	public bool SuperQuickRespawn { get; set; }

	[DefaultValue(true)]
	public bool RadarEnemyMap { get; set; }

	[DefaultValue(false)]
	public bool UnlimitedPotions { get; set; }

	[DefaultValue(false)]
	public bool UnlimitedPotionsWithDebuffs { get; set; }
}