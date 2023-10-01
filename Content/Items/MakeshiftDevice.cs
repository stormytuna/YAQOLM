using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Content.Tiles;

namespace YAQOLM.Content.Items;

public class MakeshiftDevice : ModItem
{
    public override void SetStaticDefaults() => CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

    public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("MagicStorage", out _) && ServerConfig.Instance.InventoryCraftingStations;

    public override void SetDefaults() {
        Item.width = 22;
        Item.height = 22;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(gold: 1);

        Item.createTile = ModContent.TileType<TotallyAPlaceableTile>();
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient(ItemID.PlatinumBar, 5)
            .AddIngredient(ItemID.Diamond)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.GoldBar, 5)
            .AddIngredient(ItemID.Diamond)
            .AddTile(TileID.Anvils)
            .Register();
    }
}