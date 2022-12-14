using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items {
    public class WarpedMirror : ModItem {
        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Gaze into the mirror to return to where you last died");
        }

        public override void SetDefaults() {
            Item.useTurn = true;
            Item.width = 30;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 90;
            Item.UseSound = SoundID.Item6;
            Item.useAnimation = 90;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
        }

        public override void AddRecipes() {
            if (ServerConfig.Instance.WarpedMirror) {
                CreateRecipe()
                .AddRecipeGroup(RecipeSystem.magicMirrorRecipeGroup)
                .AddRecipeGroup(RecipeSystem.evilMaterialRecipeGroup, 8)
                .AddRecipeGroup(RecipeSystem.evilBarRecipeGroup, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
            }
        }

        public override bool CanUseItem(Player player) {
            return player.GetModPlayer<WarpedMirrorPlayer>().canUseWarpedMirror;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) {
            var modPlayer = player.GetModPlayer<WarpedMirrorPlayer>();

            // Make dust each frame
            if (Main.rand.NextBool()) {
                Dust.NewDust(player.position, player.width, player.height, DustID.Demonite, 0f, 0f, 150, default, 1.1f);
            }

            // Set up itemTime correctly
            if (player.ItemTimeIsZero) {
                player.ApplyItemTime(Item);
            }

            if (player.itemTime == player.itemTimeMax / 2) {
                // Dust where the player starts
                for (int i = 0; i < 70; i++) {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Demonite, 0f, 0f, 150, default, 1.5f);
                }

                // Release grappling hooks
                player.grappling[0] = -1;
                player.grapCount = 0;
                for (int i = 0; i < Main.projectile.Length; i++) {
                    if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].aiStyle == 7) {
                        Main.projectile[i].Kill();
                    }
                }

                // Teleport the player
                player.Teleport(modPlayer.deathLocation, -1); // style: -1 prevents vanilla from doing any teleport effects
                modPlayer.canUseWarpedMirror = false;

                // Dust where the player appears
                for (int i = 0; i < 70; i++) {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Demonite, 0f, 0f, 150, default, 1.5f);
                }
            }
        }
    }

    public class WarpedMirrorPlayer : ModPlayer {
        public bool canUseWarpedMirror = false;
        public Vector2 deathLocation;

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            canUseWarpedMirror = true;
            deathLocation = Player.position;
        }
    }
}