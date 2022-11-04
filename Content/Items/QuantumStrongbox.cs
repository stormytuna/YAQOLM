using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items {
    public class QuantumStrongbox : ModItem {
        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Picks up items when your inventory is full");
        }

        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.QuantumStrongbox;

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
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                var modPlayer = player.GetModPlayer<QuantumStrongboxPlayer>();
                switch (mode) {
                    case 0:
                        SoundEngine.PlaySound(SoundID.Item59);
                        modPlayer.SetStrongboxMode = -2;
                        break;
                    case 1:
                        SoundEngine.PlaySound(SoundID.Item149);
                        modPlayer.SetStrongboxMode = -3;
                        break;
                    case 2:
                        SoundEngine.PlaySound(SoundID.Item117);
                        modPlayer.SetStrongboxMode = -4;
                        break;
                    case 3:
                        SoundEngine.PlaySound(SoundID.Item130);
                        modPlayer.SetStrongboxMode = -5;
                        break;
                }
            }

            return true;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) {
            player.itemLocation += new Vector2(-6f, 2f);
        }
    }

    public class QuantumStrongboxPlayer : ModPlayer {
        private int _strongboxMode = -1;

        public int SetStrongboxMode { set => _strongboxMode = value; }

        public override void PostUpdate() {
            if (!Main.playerInventory) {
                _strongboxMode = -1;
            }

            if (_strongboxMode != -1) {
                Recipe.FindRecipes();
                Player.CloseSign();
                Player.SetTalkNPC(-1);
                Player.chest = _strongboxMode;
                Main.npcChatCornerItem = 0;
                Main.npcChatText = "";
                Main.playerInventory = true;
                Main.stackSplit = 600;
            }
        }
    }
}