using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items;

public class MysticMirror : ModItem
{
    public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.MysticMirror;

    public override void SetStaticDefaults() => CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

    public override void SetDefaults() {
        Item.useTurn = true;
        Item.width = 24;
        Item.height = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useTime = 90;
        Item.UseSound = SoundID.Item6;
        Item.useAnimation = 90;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(gold: 2);
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

public class MysticMirrorGlobalNPC : GlobalNPC
{
    public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.Wizard;

    public override void ModifyShop(NPCShop shop) => shop.Add<MysticMirror>(Condition.DownedMechBossAny);
}