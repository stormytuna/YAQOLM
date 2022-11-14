using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players {
    public class TeamSavePlayer : ModPlayer {
        public override void SaveData(TagCompound tag) {
            tag["team"] = Player.team;
        }

        public override void LoadData(TagCompound tag) {
            int team = tag.GetInt("team");
            if (ServerConfig.Instance.SaveTeam) {
                Player.team = team;
                NetMessage.SendData(MessageID.PlayerTeam, -1, -1, null, Player.whoAmI);
            }
        }
    }
}
