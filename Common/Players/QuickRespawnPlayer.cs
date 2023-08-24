using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Helpers;

namespace YAQOLM.Common.Players;

public class QuickRespawnPlayer : ModPlayer
{
    public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.SuperQuickRespawn;

    public override void UpdateDead() {
        bool noInvasion = Main.invasionType == InvasionID.None;
        bool noBoss = Main.npc.NotAny(npc => npc.boss);

        if (noInvasion && noBoss) {
            Player.respawnTimer = Math.Clamp(Player.respawnTimer, 0, 2 * 60);
        }
    }
}