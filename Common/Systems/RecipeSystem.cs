using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Content.Items;

namespace YAQOLM.Common.Systems;

public class RecipeSystem : ModSystem
{
	// Recipe groups 
	public static RecipeGroup MagicMirrors;
	public static RecipeGroup HorseshoeBalloons;
	public static RecipeGroup EvilBars;
	public static RecipeGroup EvilMaterials;
	public static RecipeGroup MoonLordWeapons;
	public static RecipeGroup MoonLordItems;
	public static RecipeGroup Shellphones;
	public static RecipeGroup GoldBars;

	public override void Unload() {
		// Set stuff to null
		MagicMirrors = null;
		HorseshoeBalloons = null;
		EvilBars = null;
		EvilMaterials = null;
		MoonLordWeapons = null;
		MoonLordItems = null;
		Shellphones = null;
		GoldBars = null;
	}

	public override void AddRecipeGroups() {
		// Initialise our recipe groups
		MagicMirrors = new RecipeGroup(() => "Any Magic Mirror", ItemID.MagicMirror, ItemID.IceMirror);
		RecipeGroup.RegisterGroup("YAQOLM:MagicMirror", MagicMirrors);
		HorseshoeBalloons = new RecipeGroup(() => "Any Horseshoe Balloon", ItemID.BlueHorseshoeBalloon, ItemID.WhiteHorseshoeBalloon, ItemID.YellowHorseshoeBalloon,
			ItemID.BalloonHorseshoeFart, ItemID.BalloonHorseshoeHoney, ItemID.BalloonHorseshoeSharkron);
		RecipeGroup.RegisterGroup("YAQOLM:HorseshoeBalloon", HorseshoeBalloons);
		EvilBars = new RecipeGroup(() => "Any Evil Bar", ItemID.DemoniteBar, ItemID.CrimtaneBar);
		RecipeGroup.RegisterGroup("YAQOLM:EvilBar", EvilBars);
		EvilMaterials = new RecipeGroup(() => "Any Evil Material", ItemID.ShadowScale, ItemID.TissueSample);
		RecipeGroup.RegisterGroup("YAQOLM:EvilMaterial", EvilMaterials);
		MoonLordWeapons = new RecipeGroup(() => "Any Moon Lord weapon", ItemID.Meowmere, ItemID.Terrarian, ItemID.StarWrath, ItemID.SDMG, ItemID.LastPrism, ItemID.LunarFlareBook,
			ItemID.RainbowCrystalStaff, ItemID.MoonlordTurretStaff, ItemID.Celeb2);
		RecipeGroup.RegisterGroup("YAQOLM:MoonLordWeapon", MoonLordWeapons);
		MoonLordItems = new RecipeGroup(() => "Any Moon Lord item", ItemID.MeowmereMinecart, ItemID.PortalGun, ItemID.GravityGlobe, ItemID.SuspiciousLookingTentacle,
			ItemID.LongRainbowTrailWings);
		RecipeGroup.RegisterGroup("YAQOLM:MoonLordItem", MoonLordItems);
		Shellphones = new RecipeGroup(() => "Any Shellphone", ItemID.Shellphone, ItemID.ShellphoneDummy, ItemID.ShellphoneHell, ItemID.ShellphoneOcean, ItemID.ShellphoneSpawn);
		RecipeGroup.RegisterGroup("YAQOLM:Shellphone", Shellphones);
		GoldBars = new RecipeGroup(() => "Any Gold Bar", ItemID.GoldBar, ItemID.PlatinumBar);
		RecipeGroup.RegisterGroup("YAQOLM:GoldBar", GoldBars);
	}

	public override void AddRecipes() {
		if (ServerConfig.Instance.RodOfDiscord) {
			Recipe recipe = Recipe.Create(ItemID.RodofDiscord);
			recipe.AddIngredient(ItemID.HallowedBar, 15);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		if (ServerConfig.Instance.SnowGlobe) {
			Recipe recipe = Recipe.Create(ItemID.SnowGlobe);
			recipe.AddIngredient(ItemID.SnowBlock, 20);
			recipe.AddIngredient(ItemID.Glass, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		if (ServerConfig.Instance.MoneyTrough) {
			Recipe recipe = Recipe.Create(ItemID.MoneyTrough);
			recipe.AddIngredient(ItemID.PiggyBank);
			recipe.AddRecipeGroup(EvilBars, 6);
			recipe.AddRecipeGroup(EvilMaterials, 5);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}

		if (ServerConfig.Instance.Bait) {
			Recipe recipe = Recipe.Create(ItemID.JourneymanBait);
			recipe.AddIngredient(ItemID.ApprenticeBait, 2);
			recipe.AddTile(TileID.Solidifier);
			recipe.Register();

			recipe = Recipe.Create(ItemID.MasterBait);
			recipe.AddIngredient(ItemID.JourneymanBait, 2);
			recipe.AddTile(TileID.Solidifier);
			recipe.Register();
		}

		if (ServerConfig.Instance.BottomlessBuckets) {
			Recipe recipe = Recipe.Create(ItemID.BottomlessBucket);
			recipe.AddIngredient(ItemID.WaterBucket, 8);
			recipe.AddTile(TileID.CrystalBall);
			recipe.Register();

			recipe = Recipe.Create(ItemID.BottomlessLavaBucket);
			recipe.AddIngredient(ItemID.LavaBucket, 8);
			recipe.AddTile(TileID.CrystalBall);
			recipe.Register();

			recipe = Recipe.Create(ItemID.BottomlessHoneyBucket);
			recipe.AddIngredient(ItemID.HoneyBucket, 8);
			recipe.AddTile(TileID.CrystalBall);
			recipe.Register();
		}

		if (ServerConfig.Instance.LuminiteSmeltingRecipes) {
			Recipe recipe = Recipe.Create(ItemID.LunarBar, 8);
			recipe.AddRecipeGroup(MoonLordWeapons);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();

			recipe = Recipe.Create(ItemID.LunarBar, 5);
			recipe.AddRecipeGroup(MoonLordItems);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}

		if (ServerConfig.Instance.BeetleArmorOnlyBeetle) {
			Recipe recipe = Recipe.Create(ItemID.BeetleHelmet);
			recipe.AddIngredient(ItemID.BeetleHusk, 6);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();

			recipe = Recipe.Create(ItemID.BeetleLeggings);
			recipe.AddIngredient(ItemID.BeetleHusk, 9);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();

			recipe = Recipe.Create(ItemID.BeetleScaleMail);
			recipe.AddIngredient(ItemID.BeetleHusk, 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();

			recipe = Recipe.Create(ItemID.BeetleShell);
			recipe.AddIngredient(ItemID.BeetleHusk, 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		if (ServerConfig.Instance.CheaperOre) {
			Recipe recipe = Recipe.Create(ItemID.CopperBar);
			recipe.AddIngredient(ItemID.CopperOre, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.TinBar);
			recipe.AddIngredient(ItemID.TinOre, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.IronBar);
			recipe.AddIngredient(ItemID.IronOre, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.LeadBar);
			recipe.AddIngredient(ItemID.LeadOre, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.SilverBar);
			recipe.AddIngredient(ItemID.SilverOre, 3);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.TungstenBar);
			recipe.AddIngredient(ItemID.TungstenOre, 3);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.GoldBar);
			recipe.AddIngredient(ItemID.GoldOre, 3);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.PlatinumBar);
			recipe.AddIngredient(ItemID.PlatinumOre, 3);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.MeteoriteBar);
			recipe.AddIngredient(ItemID.Meteorite, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.DemoniteBar);
			recipe.AddIngredient(ItemID.DemoniteOre, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.CrimtaneBar);
			recipe.AddIngredient(ItemID.CrimtaneOre, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();

			recipe = Recipe.Create(ItemID.HellstoneBar);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddIngredient(ItemID.Obsidian);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
		}

		if (ServerConfig.Instance.GoldenHorseshoeBalloon) {
			Recipe recipe = Recipe.Create(ItemID.BlueHorseshoeBalloon);
			recipe.AddIngredient(ModContent.ItemType<GoldenHorseshoeBalloon>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();

			recipe = Recipe.Create(ItemID.WhiteHorseshoeBalloon);
			recipe.AddIngredient(ModContent.ItemType<GoldenHorseshoeBalloon>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();

			recipe = Recipe.Create(ItemID.YellowHorseshoeBalloon);
			recipe.AddIngredient(ModContent.ItemType<GoldenHorseshoeBalloon>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();

			recipe = Recipe.Create(ItemID.BalloonHorseshoeFart);
			recipe.AddIngredient(ModContent.ItemType<GoldenHorseshoeBalloon>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();

			recipe = Recipe.Create(ItemID.BalloonHorseshoeHoney);
			recipe.AddIngredient(ModContent.ItemType<GoldenHorseshoeBalloon>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();

			recipe = Recipe.Create(ItemID.BalloonHorseshoeSharkron);
			recipe.AddIngredient(ModContent.ItemType<GoldenHorseshoeBalloon>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}

		if (ServerConfig.Instance.Cascade) {
			Recipe recipe = Recipe.Create(ItemID.Cascade);
			recipe.AddIngredient(ItemID.HellstoneBar, 15);
			recipe.AddIngredient(ItemID.WoodYoyo);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

		if (ServerConfig.Instance.Extractinator) {
			Recipe recipe = Recipe.Create(ItemID.Extractinator);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 25);
			recipe.AddRecipeGroup(GoldBars, 10);
			recipe.AddIngredient(ItemID.Diamond);
			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.Register();
		}
	}

	public override void PostAddRecipes() {
		if (ServerConfig.Instance.CheaperOre) {
			for (int i = 0; i < Main.recipe.Length; i++) {
				Recipe recipe = Main.recipe[i];
				if (ArraySystem.BarTypes.Contains(recipe.createItem.type) && !recipe.requiredTile.Contains(TileID.AdamantiteForge) && ArraySystem.OreTypes.Contains(recipe.requiredItem[0].type)) {
					recipe.AddCondition(new Condition("Mods.YAQOLM.Conditions.HardmodeForge", () => !Main.LocalPlayer.adjTile[TileID.AdamantiteForge]));
				}
			}
		}

		if (ServerConfig.Instance.BeetleArmorOnlyBeetle) {
			for (int i = 0; i < Main.recipe.Length; i++) {
				if (ArraySystem.BeetleArmor.Contains(Main.recipe[i].createItem.type) && Main.recipe[i].requiredItem.Count > 1) {
					Main.recipe[i].DisableRecipe();
				}
			}
		}
	}
}