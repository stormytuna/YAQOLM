using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items {
    public class SpiralMirror : ModItem {
        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Use to teleport somewhere\nRight click to switch mode\nAllows you to teleport to party members, NPCs and Pylons");
        }

        public override void SetDefaults() {
            Item.useTurn = true;
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 5);

            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 90;
            Item.useAnimation = 90;
            Item.UseSound = SoundID.Item6;
        }

        public override bool CanUseItem(Player player) => _mode == 1 ? player.GetModPlayer<WarpedMirrorPlayer>().deathLocation != Vector2.Zero : true;

        public override void AddRecipes() {
            if (ServerConfig.Instance.SpiralMirror) {
                CreateRecipe()
                    .AddIngredient(ModContent.ItemType<WarpedMirror>())
                    .AddIngredient(ModContent.ItemType<MysticMirror>())
                    .AddIngredient(ModContent.ItemType<RunicMirror>())
                    .AddIngredient(ItemID.ChlorophyteBar, 3)
                    .AddTile(TileID.MythrilAnvil)
                    .Register();
            }
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player) {
            _mode++;
            if (_mode > 2) {
                _mode = 0;
            }

            Item.stack++;
        }

        private int _mode = 0;
        private int _resetMode = -1;

        public void SetAndResetMode(int newMode) {
            _resetMode = _mode;
            _mode = newMode;
        }

        private void MakeDust(Player player) {
            Dust d = null;
            switch (_mode) {
                case 0:
                    d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.1f);
                    d.velocity *= 0.5f;
                    return;
                case 1:
                    Dust.NewDust(player.position, player.width, player.height, DustID.Demonite, 0f, 0f, 150, default, 1.1f);
                    return;
                case 2:
                    d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, Color.Cyan, 1.1f);
                    d.velocity *= 0.5f;
                    return;
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) {
            var warpedPlayer = player.GetModPlayer<WarpedMirrorPlayer>();

            // Make some dust each frame
            if (Main.rand.NextBool()) {
                MakeDust(player);
            }

            // Setup item time correctly
            if (player.ItemTimeIsZero) {
                player.ApplyItemTime(Item);
            }

            // Exactly halfway through
            if (player.itemTime == player.itemTimeMax / 2) {
                // Dust where the player starts
                for (int i = 0; i < 70; i++) {
                    MakeDust(player);
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
                switch (_mode) {
                    case 0:
                        player.Spawn(PlayerSpawnContext.RecallFromItem);
                        break;
                    case 1:
                        player.Teleport(warpedPlayer.deathLocation, -1); // style: -1 prevents vanilla from doing any teleport effects
                        warpedPlayer.deathLocation = Vector2.Zero;
                        break;
                    case 2:
                        player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();
                        break;
                }

                // Dust where the player appears
                for (int i = 0; i < 70; i++) {
                    MakeDust(player);
                }

                // Reset mode if we need to
                if (_resetMode != -1) {
                    _mode = _resetMode;
                    _resetMode = -1;
                }
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            var tooltip = tooltips.FirstOrDefault(t => t.Name == "Tooltip0");
            switch (_mode) {
                case 0:
                    tooltip.Text = "Gaze into the mirror to return home";
                    break;
                case 1:
                    tooltip.Text = "Gaze into the mirror to return to where you last died";
                    break;
                case 2:
                    tooltip.Text = "Gaze into the mirror to return home and create a portal";
                    break;
            }
        }

        public override void UpdateInventory(Player player) {
            player.GetModPlayer<RunicMirrorPlayer>().runicMirror = true;
        }
    }
}