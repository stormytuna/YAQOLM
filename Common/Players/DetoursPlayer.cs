using Terraria;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players {
    public class DetoursPlayer : ModPlayer {
        public override void Load() {
            // Subscribing in Load
            if (ServerConfig.Instance.MoreAnglerLoot) {
                On.Terraria.Player.GetAnglerReward += Player_GetAnglerReward;
            }
        }

        public override void Unload() {
            // Unsubscribing in Unload
            if (ServerConfig.Instance.MoreAnglerLoot) {
                On.Terraria.Player.GetAnglerReward -= Player_GetAnglerReward;
            }
        }

        private void Player_GetAnglerReward(On.Terraria.Player.orig_GetAnglerReward orig, Player self, NPC angler) {
            // Just gives us double loot from anglerg
            orig(self, angler);
            self.anglerQuestsFinished++;
            orig(self, angler);
        }
    }
}