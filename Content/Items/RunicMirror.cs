using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items {
    public class RunicMirror : ModItem {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.RunicMirror;

        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Allows you to teleport to party members, NPCs and Pylons");
        }

        public override void SetDefaults() {
            Item.useTurn = true;
            Item.width = 26;
            Item.height = 26;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateInventory(Player player) {
            player.GetModPlayer<RunicMirrorPlayer>().runicMirror = true;
        }
    }

    public class RunicMirrorPlayer : ModPlayer {
        public bool runicMirror;

        public override void ResetEffects() {
            runicMirror = false;
        }

        public override void PostUpdateMiscEffects() {
            if (Player.whoAmI != Main.myPlayer || !Main.mapFullscreen || !Main.mouseLeft || !Main.mouseLeftRelease) {
                return;
            }

            // Gets where our cursor is on the map
            PlayerInput.SetZoom_Unscaled();
            float scale = 16f / Main.mapFullscreenScale;
            float minX = Main.mapFullscreenPos.X * 16f - 10f;
            float minY = Main.mapFullscreenPos.Y * 16f - 21f;
            float mouseX = Main.mouseX - Main.screenWidth / 2;
            float mouseY = Main.mouseY - Main.screenHeight / 2;
            float cursorOnMapX = minX + mouseX * scale;
            float cursorOnMapY = minY + mouseY * scale;

            // If we clicked near a player
            for (int i = 0; i < Main.player.Length; i++) {
                var teleportPlayer = Main.player[i];
                if (teleportPlayer.whoAmI != Main.myPlayer && teleportPlayer.active && !teleportPlayer.dead && teleportPlayer.team == Player.team && !teleportPlayer.hostile) {
                    float minClickX = teleportPlayer.position.X - 14f * scale;
                    float minClickY = teleportPlayer.position.Y - 14f * scale;
                    float maxClickX = teleportPlayer.position.X + 14f * scale;
                    float maxClickY = teleportPlayer.position.Y + 14f * scale;
                    if (cursorOnMapX >= minClickX && cursorOnMapX <= maxClickX && cursorOnMapY >= minClickY && cursorOnMapY <= maxClickY) {
                        Main.mouseLeftRelease = false;
                        Main.mapFullscreen = false;
                        Player.UnityTeleport(teleportPlayer.position);
                        PlayerInput.SetZoom_Unscaled();
                        return;
                    }
                }
            }

            // If we clicked near a town npc
            for (int i = 0; i < Main.npc.Length; i++) {
                var teleportNPC = Main.npc[i];
                if (teleportNPC.active && teleportNPC.townNPC) {
                    float minClickX = teleportNPC.position.X - 14f * scale;
                    float minClickY = teleportNPC.position.Y - 14f * scale;
                    float maxClickX = teleportNPC.position.X + 14f * scale;
                    float maxClickY = teleportNPC.position.Y + 14f * scale;
                    if (cursorOnMapX >= minClickX && cursorOnMapX <= maxClickX && cursorOnMapY >= minClickY && cursorOnMapY <= maxClickY) {
                        Main.mouseLeftRelease = false;
                        Main.mapFullscreen = false;
                        Player.Teleport(teleportNPC.position + new Vector2(0f, -6f));
                        PlayerInput.SetZoom_Unscaled();
                        return;
                    }
                }
            }
        }
    }

    public class RunicMirrorGlobalNPC : GlobalNPC {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.RuneWizard;

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RunicMirror>()));
        }
    }

    public class RunicMirrorSystem : ModSystem {
        public override void Load() {
            On.Terraria.GameContent.TeleportPylonsSystem.IsPlayerNearAPylon += TeleportPylonsSystem_IsPlayerNearAPylon;
            On.Terraria.Player.InInteractionRange += Player_InInteractionRange;
        }

        public override void Unload() {
            On.Terraria.GameContent.TeleportPylonsSystem.IsPlayerNearAPylon -= TeleportPylonsSystem_IsPlayerNearAPylon;
            On.Terraria.Player.InInteractionRange -= Player_InInteractionRange;
        }

        private bool TeleportPylonsSystem_IsPlayerNearAPylon(On.Terraria.GameContent.TeleportPylonsSystem.orig_IsPlayerNearAPylon orig, Player player) {
            if (player.HasItem(ModContent.ItemType<RunicMirror>()) || player.HasItem(ModContent.ItemType<SpiralMirror>())) {
                return true;
            }

            return orig(player);
        }

        private bool Player_InInteractionRange(On.Terraria.Player.orig_InInteractionRange orig, Player self, int interactX, int interactY) {
            bool ret = orig(self, interactX, interactY);

            Tile tile = Framing.GetTileSafely(interactX, interactY);
            if (TileID.Sets.CountsAsPylon.Contains(tile.TileType) && self.HasItem(ModContent.ItemType<RunicMirror>()) || self.HasItem(ModContent.ItemType<SpiralMirror>()) && !ret) {
                return true;
            }

            return ret;
        }
    }
}