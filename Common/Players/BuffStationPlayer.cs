using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace YAQOLM.Common.Players {
    public class BuffStationPlayer : ModPlayer {
        public bool ammoBox;

        public override void ResetEffects() {
            ammoBox = false;
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo) {
            if (ammoBox && Main.rand.NextFloat() < 0.4f) {
                return false;
            }

            return base.CanConsumeAmmo(weapon, ammo);
        }
    }
}