using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Players;

namespace YAQOLM.Common.Buffs {
    public class YAQOLMGlobalBuff : GlobalBuff {
        public override void Update(int type, Player player, ref int buffIndex) {
            if (type == BuffID.Sharpened && ServerConfig.Instance.BuffStationChanges) {
                // Undo what vanilla does
                if (player.HeldItem.CountsAsClass(DamageClass.Melee)) {
                    player.GetArmorPenetration(DamageClass.Melee) -= 12;
                }

                // Do our stuff
                player.GetDamage(DamageClass.Melee) += 0.08f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.08f;

                return;
            }
        }

        public override void ModifyBuffTip(int type, ref string tip, ref int rare) {
            if (type == BuffID.Sharpened && ServerConfig.Instance.BuffStationChanges) {
                tip = "8% increased melee damage and melee speed";
            }
        }
    }
}