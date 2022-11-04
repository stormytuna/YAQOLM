﻿using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items {
    public class MagnificentMagnet : ModItem {
        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Increases pickup range for items while in your inventory\nUse to pull all items in the world to you");
        }

        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.MagnificentMagnet;

        public override void SetDefaults() {
            Item.width = 26;
            Item.height = 28;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(gold: 3);

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<GemstoneMagnet>())
                .AddIngredient(ItemID.SoulofSight, 3)
                .AddIngredient(ItemID.SoulofMight, 3)
                .AddIngredient(ItemID.SoulofFright, 3)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        private bool _disabled = false;
        public bool Disabled { get => _disabled; }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            // Get index
            var lastTip = tooltips.FirstOrDefault(t => t.Name == "Tooltip1");
            int index = tooltips.IndexOf(lastTip) + 1;

            // Make TooltipLine
            string text = _disabled ? "[c/ff0000:Disabled:] Right click to enable" : "[c/00ff00:Enabled:] Right click to disable";
            TooltipLine line = new(Mod, "EnabledDisabled", text);

            // insert
            tooltips.Insert(index, line);
        }

        public override bool CanRightClick() => true;

        public override void OnConsumeItem(Player player) => Item.stack++;

        public override void RightClick(Player player) {
            _disabled = !_disabled;
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                for (int i = 0; i < Main.item.Length; i++) {
                    Main.item[i].position = player.Center;
                }
            }

            return true;
        }
    }

    // Again, like GemstoneMagnet, doing inventory checks in a player so we can use it in void bag
    public class MagnificentMagnetPlayer : ModPlayer {
        public override void PostUpdateMiscEffects() {
            if (Player.HasItemInInventories(ModContent.ItemType<MagnificentMagnet>(), out Item item)) {
                var magnet = item.ModItem as MagnificentMagnet;
                if (!magnet.Disabled) {
                    Player.treasureMagnet = true;
                }
            }
        }
    }
}
