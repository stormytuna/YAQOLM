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

	public override void SetStaticDefaults() {
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		Tooltip.SetDefault("Use to teleport somewhere\nAllows you to teleport to party members, NPCs and Pylons\nDisplays everything\n'Can I interest you in everything, all of the time?'");
	}

	public override void SetDefaults() {
		Item.useTurn = true;
		Item.width = 30;
		Item.height = 30;
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(gold: 5);

		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useTime = 10;
		Item.useAnimation = 10;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<WarpedMirror>())
			.AddIngredient(ModContent.ItemType<MysticMirror>())
			.AddIngredient(ModContent.ItemType<RunicMirror>())
			.AddIngredient(ItemID.CellPhone)
			.AddIngredient(ItemID.MagicConch)
			.AddIngredient(ItemID.DemonConch)
			.AddIngredient(ItemID.ChlorophyteBar, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}

	public override bool? UseItem(Player player) {
		SoundEngine.PlaySound(SoundID.MenuOpen);
		SpiralMirrorUiSystem.Toggle();

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