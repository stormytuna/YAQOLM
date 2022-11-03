using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Systems {
    public class DetourSystem : ModSystem {
        public override void Load() {
            if (ServerConfig.Instance.BetterBombTiles) {
                On.Terraria.Projectile.CanExplodeTile += Projectile_CanExplodeTile;
            }
        }

        public override void Unload() {
            if (ServerConfig.Instance.BetterBombTiles) {
                On.Terraria.Projectile.CanExplodeTile -= Projectile_CanExplodeTile;
            }
        }

        private bool Projectile_CanExplodeTile(On.Terraria.Projectile.orig_CanExplodeTile orig, Terraria.Projectile self, int x, int y) {
            bool canExplode = orig(self, x, y);

            if (!canExplode) {
                // Check we absolutely cannot explode this tile
                int type = Main.tile[x, y].TileType;
                var player = Main.player[self.owner];
                if (Main.tileDungeon[type] || TileID.Sets.BasicChest[type] || ArraySystem.IndestructibleTiles.Contains(type)) {
                    return canExplode;
                }

                // Check if our player could mine this
                if (player.HasEnoughPickPowerToHurtTile(x, y)) {
                    canExplode = true;
                }
            }

            return canExplode;
        }
}