using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items {
    public class MysticMirror : ModItem {
        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Gaze into the mirror to return home and create a portal\nUse portal to return when you are done");
        }

        public override void SetDefaults() {
            Item.useTurn = true;
            Item.width = 28;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 90;
            Item.UseSound = SoundID.Item6;
            Item.useAnimation = 90;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void AddRecipes() {
            if (ServerConfig.Instance.MysticMirror) {
                CreateRecipe()
                    .AddRecipeGroup(RecipeSystem.magicMirrorRecipeGroup)
                    .AddIngredient(ItemID.SoulofLight, 8)
                    .AddIngredient(ItemID.SoulofNight, 8)
                    .AddTile(TileID.MythrilAnvil)
                    .Register();
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) {
            // Make dust each frame
            if (Main.rand.NextBool()) {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, Color.Cyan, 1.1f);
                d.velocity *= 0.5f;
            }

            // Set up itemTime correctly
            if (player.ItemTimeIsZero) {
                player.ApplyItemTime(Item);
            }

            if (player.itemTime == player.itemTimeMax / 2) {
                // Dust where the player starts
                for (int i = 0; i < 70; i++) {
                    Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, Color.Cyan, 1.5f);
                    d.velocity *= 0.5f;
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
                player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();

                // Dust where the player appears
                for (int i = 0; i < 70; i++) {
                    Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, Color.Cyan, 1.5f);
                    d.velocity *= 0.5f;
                }
            }
        }
    }
}