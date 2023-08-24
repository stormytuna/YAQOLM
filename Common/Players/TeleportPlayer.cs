using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Systems;
using YAQOLM.Content.Items;

namespace YAQOLM.Common.Players;

public class TeleportPlayer : ModPlayer
{
    private const int TotalTeleportTime = 90;

    private int teleportTime;
    private int teleportStyle;
    private SlotId teleportSound;

    public void StartTeleport(int teleportStyle) {
        // Checks we are using Warped Mirror and can use it
        if (teleportStyle == 2 && Player.GetModPlayer<WarpedMirrorPlayer>().deathLocation == Vector2.Zero) {
            return;
        }

        this.teleportStyle = teleportStyle;
        teleportTime = TotalTeleportTime;
        teleportSound = SoundEngine.PlaySound(SoundID.Item6, Player.Center);
    }

    public override void PostUpdate() {
        WarpedMirrorPlayer warpedPlayer = Player.GetModPlayer<WarpedMirrorPlayer>();

        teleportTime--;
        if (teleportTime < 0) {
            return;
        }

        if (Main.rand.NextBool()) {
            MakeDust(Player);
        }

        if (teleportTime == TotalTeleportTime / 2) {
            // Dust where the player starts
            for (int i = 0; i < 70; i++) {
                MakeDust(Player);
            }

            // Release grappling hooks
            Player.grappling[0] = -1;
            Player.grapCount = 0;
            for (int i = 0; i < Main.projectile.Length; i++) {
                if (Main.projectile[i].active && Main.projectile[i].owner == Player.whoAmI && Main.projectile[i].aiStyle == 7) {
                    Main.projectile[i].Kill();
                }
            }

            // Teleport the player
            if (Player.whoAmI == Main.myPlayer) {
                switch (teleportStyle) {
                    case 0:
                        Player.Spawn(PlayerSpawnContext.RecallFromItem);
                        break;
                    case 1:
                        if (Main.netMode == NetmodeID.SinglePlayer) {
                            Player.Shellphone_Spawn();
                        } else if (Main.netMode == NetmodeID.MultiplayerClient) {
                            NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 3);
                        }

                        break;
                    case 2:
                        Player.Teleport(warpedPlayer.deathLocation, -1); // style: -1 prevents vanilla from doing any teleport effects
                        warpedPlayer.canUseWarpedMirror = false;
                        break;
                    case 3:
                        Player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();
                        break;
                    case 4:
                        if (Main.netMode == NetmodeID.SinglePlayer) {
                            Player.MagicConch();
                        } else if (Main.netMode == NetmodeID.MultiplayerClient) {
                            NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 1);
                        }

                        break;
                    case 5:
                        if (Main.netMode == NetmodeID.SinglePlayer) {
                            Player.DemonConch();
                        } else if (Main.netMode == NetmodeID.MultiplayerClient) {
                            NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 2);
                        }

                        break;
                }
            }

            // Move our sound
            if (SoundEngine.TryGetActiveSound(teleportSound, out ActiveSound sound)) {
                sound.Position = Player.Center;
            }

            // Dust where the player appears
            for (int i = 0; i < 70; i++) {
                MakeDust(Player);
            }
        }
    }

    private void MakeDust(Player player) {
        Dust d;
        Vector2 vector;
        switch (teleportStyle) {
            case 0:
                d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.1f);
                d.velocity *= 0.5f;
                return;
            case 1:
                d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.1f);
                d.velocity *= 0.5f;
                return;
            case 2:
                Dust.NewDust(player.position, player.width, player.height, DustID.Demonite, 0f, 0f, 150, default, 1.1f);
                return;
            case 3:
                d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, Color.Cyan, 1.1f);
                d.velocity *= 0.5f;
                return;
            case 4:
                vector = Vector2.UnitY.RotatedBy(player.itemAnimation * MathHelper.TwoPi / 30f) * new Vector2(15f, 0f);
                d = Dust.NewDustPerfect(player.Bottom + vector, Dust.dustWater());
                d.velocity.Y *= 0f;
                d.velocity.Y -= 4.5f;
                d.velocity.X *= 1.5f;
                d.scale = 0.8f;
                d.alpha = 130;
                d.noGravity = true;
                d.fadeIn = 1.1f;
                return;
            case 5:
                vector = Vector2.UnitY.RotatedBy(player.itemAnimation * MathHelper.TwoPi / 30f) * new Vector2(15f, 0f);
                d = Dust.NewDustPerfect(player.Bottom + vector, 35);
                d.velocity.Y *= 0f;
                d.velocity.Y -= 4.5f;
                d.velocity.X *= 1.5f;
                d.scale = 0.8f;
                d.alpha = 130;
                d.noGravity = true;
                d.fadeIn = 1.1f;
                return;
        }
    }

    public override void UpdateDead() => SpiralMirrorUiSystem.Hide();
}