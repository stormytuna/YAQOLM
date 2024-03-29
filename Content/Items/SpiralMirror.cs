using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items;

public class SpiralMirror : ModItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.SpiralMirror;

	public override void SetStaticDefaults() => CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

	public override void SetDefaults() {
		Item.useTurn = true;
		Item.width = 30;
		Item.height = 30;
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(gold: 5);

		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useTime = 25;
		Item.useAnimation = 25;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<WarpedMirror>())
			.AddIngredient(ModContent.ItemType<MysticMirror>())
			.AddIngredient(ModContent.ItemType<RunicMirror>())
			.AddRecipeGroup(RecipeSystem.Shellphones)
			.AddIngredient(ItemID.ChlorophyteBar, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}

	public override bool? UseItem(Player player) {
		if (player.whoAmI == Main.myPlayer) {
			SoundEngine.PlaySound(SoundID.MenuOpen);
			SpiralMirrorUiSystem.Toggle();
		}

		return true;
	}

	public override void UpdateInventory(Player player) {
		player.GetModPlayer<RunicMirrorPlayer>().runicMirror = true;

		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
		player.accFishFinder = true;
		player.accDreamCatcher = true;
		player.accCritterGuide = true;
		player.accOreFinder = true;
		player.accStopwatch = true;
		player.accCalendar = true;
		player.accJarOfSouls = true;
		player.accThirdEye = true;
		player.accWeatherRadio = true;
	}
}