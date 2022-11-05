using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace YAQOLM.Common.Players {
    public class ArmorSetPlayer : ModPlayer {
        public bool rainArmor;

        public override void ResetEffects() {
            rainArmor = false;
        }

        public override void PostUpdateEquips() {
            if (rainArmor) {
                bool isRaining = Main.raining;
                bool isExposed = Collision.CanHit(Player.Center, 1, 1, Player.Center + new Vector2(0f, -30f * 16f), 1, 1) && Player.Center.Y < Main.worldSurface * 16f;

                if (isRaining && isExposed) {
                    Player.GetDamage(DamageClass.Generic) += 0.08f;
                    Player.GetCritChance(DamageClass.Generic) += 5;
                }
            }
        }
    }
}