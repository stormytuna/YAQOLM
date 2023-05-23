using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items;

public class GoldenHorseshoeBalloon : ModItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.GoldenHorseshoeBalloon;

	public override void SetStaticDefaults() {
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		// Tooltip.SetDefault("Doesn't allow the holder to double jump\nDoesn't increase jump height or negate fall damage");
	}

	public override void SetDefaults() {
		Item.width = 28;
		Item.height = 48;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(gold: 3);
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddRecipeGroup(RecipeSystem.HorseshoeBalloons)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips) {
		TooltipLine line = tooltips.FirstOrDefault(t => t.Mod == "Terraria" && t.Name == "Material");
		line.Text = "Not equipable\nCan't be worn in vanity slots\nMaterial";
	}
}