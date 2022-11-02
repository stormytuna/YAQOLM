using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items.PrefixHammers {
    public class SolarPrefixHammer : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Right click to apply the best prefix to your currently held weapon");
        }

        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.PrefixHammers;

        public override void SetDefaults() {
            Item.width = 26;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Cyan;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentSolar, 1)
                .AddIngredient(ItemID.FragmentVortex, 1)
                .AddIngredient(ItemID.FragmentNebula, 1)
                .AddIngredient(ItemID.FragmentStardust, 1)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool CanRightClick() {
            return Main.LocalPlayer.HeldItem.damage > 0 && !PrefixSystem.ItemHasBestPrefix(Main.LocalPlayer.HeldItem);
        }

        public override void RightClick(Player player) {
            PrefixSystem.ApplyBestPrefix(ref player.inventory[player.selectedItem]);
        }
    }
}