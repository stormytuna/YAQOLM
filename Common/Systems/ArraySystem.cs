using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Content.Items.PrefixHammers;

namespace YAQOLM.Common.Systems;

public class ArraySystem : ModSystem
{
	public static int[] SolutionIds { get; private set; } = { ItemID.GreenSolution, ItemID.PurpleSolution, ItemID.RedSolution, ItemID.BlueSolution, ItemID.DarkBlueSolution };

	public static int[] BarTypes { get; private set; } = {
		ItemID.CopperBar, ItemID.TinBar, ItemID.IronBar, ItemID.LeadBar, ItemID.SilverBar, ItemID.TungstenBar, ItemID.GoldBar, ItemID.PlatinumBar, ItemID.MeteoriteBar, ItemID.DemoniteBar,
		ItemID.CrimtaneBar, ItemID.HellstoneBar
	};

	public static int[] OreTypes { get; private set; } = {
		ItemID.CopperOre, ItemID.TinOre, ItemID.IronOre, ItemID.LeadOre, ItemID.SilverOre, ItemID.TungstenOre, ItemID.GoldOre, ItemID.PlatinumOre, ItemID.Meteorite, ItemID.DemoniteOre,
		ItemID.CrimtaneOre, ItemID.Hellstone
	};

	public static int[] BeetleArmor { get; private set; } = { ItemID.BeetleHelmet, ItemID.BeetleLeggings, ItemID.BeetleScaleMail, ItemID.BeetleShell };

	public static int[] ItemsWithLegendary2 { get; private set; } = {
		ItemID.Terrarian, ItemID.CopperShortsword, ItemID.Gladius, ItemID.GoldShortsword, ItemID.IronShortsword, ItemID.LeadShortsword, ItemID.PlatinumShortsword, ItemID.Ruler,
		ItemID.SilverShortsword, ItemID.TinShortsword, ItemID.TragicUmbrella, ItemID.TungstenShortsword, ItemID.Umbrella
	};

	public static int[] IndestructibleTiles { get; } = { TileID.DemonAltar, TileID.Dressers, TileID.LihzahrdAltar, TileID.Mannequin, TileID.Womannequin, TileID.HatRack };

	public static int[] ItemsThatPlaceTilesWithRecipes { get; private set; } = { };

	public static int[] PrefixHammers { get; } = {
		ModContent.ItemType<NebulaPrefixHammer>(), ModContent.ItemType<SolarPrefixHammer>(), ModContent.ItemType<VortexPrefixHammer>(), ModContent.ItemType<StardustPrefixHammer>()
	};

	public override void Unload() {
		SolutionIds = null;
		BarTypes = null;
		OreTypes = null;
		BeetleArmor = null;
		ItemsWithLegendary2 = null;
	}

	public override void PostAddRecipes() {
		var tilesWithRecipes = new List<int>();

		for (int i = 0; i < Main.recipe.Length; i++) {
			for (int j = 0; j < Main.recipe[i].requiredTile.Count; j++) {
				if (!tilesWithRecipes.Contains(Main.recipe[i].requiredTile[j])) {
					tilesWithRecipes.Add(Main.recipe[i].requiredTile[j]);
				}
			}
		}

		var itemsPlaceTilesWithRecipes = new List<int>();

		for (int i = 0; i < ContentSamples.ItemsByType.Count; i++) {
			if (tilesWithRecipes.Contains(ContentSamples.ItemsByType[i].createTile)) {
				itemsPlaceTilesWithRecipes.Add(i);
			}
		}

		ItemsThatPlaceTilesWithRecipes = itemsPlaceTilesWithRecipes.ToArray();
	}
}