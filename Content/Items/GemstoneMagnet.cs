using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items {
    public class GemstoneMagnet : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Increases pickup range for items while in your inventory");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.GemstoneMagnet;

        public override void SetDefaults() {
            Item.width = 26;
            Item.height = 28;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 1, silver: 50);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.TreasureMagnet)
                .AddIngredient(ItemID.Sapphire, 3)
                .AddIngredient(ItemID.Ruby, 3)
                .AddIngredient(ItemID.Emerald, 3)
                .AddTile(TileID.CrystalBall)
                .Register();
        }

        public override void UpdateInventory(Player player) {
            player.treasureMagnet = true;
        }
    }
}
