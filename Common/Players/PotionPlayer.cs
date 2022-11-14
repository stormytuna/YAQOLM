using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players {
    public class PotionPlayer : ModPlayer {
        public override void PreUpdateBuffs() {
            if (ServerConfig.Instance.UnlimitedPotions) {
                List<Item> inventories = Player.inventory.ToList();
                inventories.AddRange(Player.bank.item);
                inventories.AddRange(Player.bank2.item);
                inventories.AddRange(Player.bank3.item);
                inventories.AddRange(Player.bank4.item);

                foreach (var item in inventories) {
                    if (item.buffType > 0 && (!Main.debuff[item.buffType] || ServerConfig.Instance.UnlimitedPotionsWithDebuffs) && item.stack >= 30) {
                        Player.AddBuff(item.buffType, 2);
                    }
                }
            }
        }
    }
}
