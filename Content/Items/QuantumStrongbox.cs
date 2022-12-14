using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Gamepad;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items {
    public class QuantumStrongbox : ModItem {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.QuantumStrongbox;

        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Picks up items when your inventory is full");
        }

        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 5);

            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 30;
            Item.useAnimation = 30;
        }

        public override void AddRecipes() {
            CreateRecipe()
                    .AddIngredient(ItemID.PiggyBank)
                    .AddIngredient(ItemID.Safe)
                    .AddIngredient(ItemID.DefendersForge)
                    .AddIngredient(ItemID.VoidLens)
                    .AddIngredient(ItemID.SoulofFright, 3)
                    .AddIngredient(ItemID.SoulofMight, 3)
                    .AddIngredient(ItemID.SoulofSight, 3)
                    .AddTile(TileID.MythrilAnvil)
                    .Register();
        }

        public override void UpdateInventory(Player player) {
            player.IsVoidVaultEnabled = true;
        }

        private int mode = 0;
        private int resetMode = -1;
        public void SetAndResetMode(int newMode) {
            resetMode = mode;
            mode = newMode;
            var player = Main.LocalPlayer;
            ToggleChest(ref player, mode);
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player) {
            mode++;
            if (mode > 3) {
                mode = 0;
            }

            Item.stack++;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            var tooltipLine = tooltips.FirstOrDefault(t => t.Name == "Tooltip0");
            int index = tooltips.IndexOf(tooltipLine) + 1;
            string text = "";
            switch (mode) {
                case 0:
                    text = "Use to open your [c/f78bc1:Piggy Bank]";
                    break;
                case 1:
                    text = "Use to open your [c/606060:Safe]";
                    break;
                case 2:
                    text = "Use to open your [c/5af0ff:Defender's Forge]";
                    break;
                case 3:
                    text = "Use to open your [c/7b5fd4:Void Vault]";
                    break;
            }
            tooltips.Insert(index, new(Mod, "Tooltip1", text));
            tooltips.Insert(index + 1, new(Mod, "Tooltip2", "Right click to switch mode"));

            if (PlayerInput.Triggers.JustPressed.MouseMiddle) {
                var player = Main.LocalPlayer;
                ToggleChest(ref player, mode);
            }

        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {

                ToggleChest(ref player, mode);

                if (resetMode != -1) {
                    mode = resetMode;
                    resetMode = -1;
                }
            }

            return true;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) {
            player.itemLocation += new Vector2(-6f * player.direction, 2f);
        }

        private void ToggleChest(ref Player player, int mode) {
            int chestId = -1;
            SoundStyle? sound = null;
            switch (mode) {
                case 0:
                    sound = SoundID.Item59;
                    chestId = -2;
                    break;
                case 1:
                    sound = SoundID.Item149;
                    chestId = -3;
                    break;
                case 2:
                    sound = SoundID.Item117;
                    chestId = -4;
                    break;
                case 3:
                    sound = SoundID.Item130;
                    chestId = -5;
                    break;
            }

            if (player.chest == chestId) {
                player.chest = -1;
                SoundEngine.PlaySound(sound ?? SoundID.MenuClose);
            }
            else {
                var x = player.Center.ToTileCoordinates().X;
                var y = player.Center.ToTileCoordinates().Y;
                player.chest = chestId;
                for (int i = 0; i < 40; i++) {
                    ItemSlot.SetGlow(i, -1f, chest: true);
                }

                player.chestX = x;
                player.chestY = y;
                player.CloseSign();
                player.SetTalkNPC(-1);
                Main.npcChatCornerItem = 0;
                Main.npcChatText = "";
                Main.SetNPCShopIndex(0);

                UILinkPointNavigator.ForceMovementCooldown(120);
                if (PlayerInput.GrappleAndInteractAreShared)
                    PlayerInput.Triggers.JustPressed.Grapple = false;

                SoundEngine.PlaySound(sound ?? SoundID.MenuOpen);
            }
            Main.playerInventory = true;
            Recipe.FindRecipes();
        }
    }
}
