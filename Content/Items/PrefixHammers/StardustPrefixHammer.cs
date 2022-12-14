using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items.PrefixHammers {
    public class StardustPrefixHammer : ModItem {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.PrefixHammers;

        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Right click with an accessory in your cursor to apply Menacing to it\nOr, right click an accessory with this in your cursor");
        }

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
            return Main.mouseItem.accessory && Main.mouseItem.prefix != PrefixID.Menacing;
        }

        public override void RightClick(Player player) {
            PrefixSystem.ApplyPrefix(ref Main.mouseItem, PrefixID.Menacing);

            // Dusty dust
            for (int i = 0; i < 40; i++) {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.CoralTorch, 0f, 0f, 150, default, 1.3f);
                d.noLight = true;
                d.velocity *= 10f;
                d.noGravity = true;
            }
        }
    }

    public class StardustPrefixHammerGlobalItem : GlobalItem {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.accessory;

        public override bool CanRightClick(Item item) {
            if (Main.mouseItem.type == ModContent.ItemType<StardustPrefixHammer>()) {
                return item.prefix != PrefixID.Menacing;
            }

            return base.CanRightClick(item);
        }

        public override void RightClick(Item item, Player player) {
            if (Main.mouseItem.type != ModContent.ItemType<StardustPrefixHammer>()) {
                return;
            }

            PrefixSystem.ApplyPrefix(ref item, PrefixID.Menacing);
            item.stack++;
            Main.mouseItem.stack--;

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