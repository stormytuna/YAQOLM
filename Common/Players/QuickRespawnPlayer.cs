using System;
using Terraria;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players {
    public class QuickRespawnPlayer : ModPlayer {
        public override void UpdateDead() {
            if (ServerConfig.Instance.SuperQuickRespawn) {
                bool noBossOrInvasion = Main.invasionType <= 0;

                for (int i = 0; i < Main.npc.Length; i++) {
                    if (Main.npc[i].boss) {
                        noBossOrInvasion = false;
                    }
                }

                if (noBossOrInvasion) {
                    Player.respawnTimer = Math.Clamp(Player.respawnTimer, 0, 2 * 60);
                }
            }
        }
    }
}