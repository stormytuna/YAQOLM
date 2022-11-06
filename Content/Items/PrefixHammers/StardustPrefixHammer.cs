using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items.PrefixHammers {
    public class StardustPrefixHammer : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Right click to apply Menacing to your currently held accessory");
        }

        public override void SetDefaults() {
            Item.width = 26;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Cyan;
        }

        public override void AddRecipes() {
            if (ServerConfig.Instance.PrefixHammers) {
                CreateRecipe()
                    .AddIngredient(ItemID.FragmentSolar, 1)
                    .AddIngredient(ItemID.FragmentVortex, 1)
                    .AddIngredient(ItemID.FragmentNebula, 1)
                    .AddIngredient(ItemID.FragmentStardust, 1)
                    .AddTile(TileID.LunarCraftingStation)
                    .Register();
            }
        }

        public override bool CanRightClick() {
            return Main.LocalPlayer.HeldItem.accessory && Main.LocalPlayer.HeldItem.prefix != PrefixID.Menacing;
        }

        public override void RightClick(Player player) {
            PrefixSystem.ApplyPrefix(ref player.inventory[player.selectedItem], PrefixID.Menacing);

            // Dusty dust
            for (int i = 0; i < 40; i++) {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.CoralTorch, 0f, 0f, 150, default, 1.3f);
                d.noLight = true;
                d.velocity *= 10f;
                d.noGravity = true;
            }
        }
    }
}