using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items.PrefixHammers {
    public class VortexPrefixHammer : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Right click with an accessory in your cursor to apply Lucky to it\nOr, right click an accessory with this in your cursor");
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
            return Main.mouseItem.accessory && Main.mouseItem.prefix != PrefixID.Lucky;
        }

        public override void RightClick(Player player) {
            PrefixSystem.ApplyPrefix(ref Main.mouseItem, PrefixID.Lucky);

            // Dusty dust
            for (int i = 0; i < 40; i++) {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.CoralTorch, 0f, 0f, 150, Color.Green, 1.3f);
                d.noLight = true;
                d.velocity *= 10f;
                d.noGravity = true;
            }
        }
    }

    public class VortexPrefixHammerGlobalItem : GlobalItem {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.accessory;

        public override bool CanRightClick(Item item) {
            if (Main.mouseItem.type == ModContent.ItemType<VortexPrefixHammer>()) {
                return item.prefix != PrefixID.Lucky;
            }

            return base.CanRightClick(item);
        }

        public override void RightClick(Item item, Player player) {
            if (Main.mouseItem.type != ModContent.ItemType<VortexPrefixHammer>()) {
                return;
            }

            PrefixSystem.ApplyPrefix(ref item, PrefixID.Lucky);
            item.stack++;
            Main.mouseItem.stack--;

            // Dusty dust
            for (int i = 0; i < 40; i++) {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.HallowedTorch, 0f, 0f, 150, default, 1.3f);
                d.noLight = true;
                d.velocity *= 10f;
                d.noGravity = true;
            }
        }
    }
}