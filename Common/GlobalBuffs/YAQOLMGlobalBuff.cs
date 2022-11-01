using Terraria;
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
                player.GetDamage(DamageClass.Melee) += 0.12f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.12f;

                return;
            }

            if (type == BuffID.AmmoBox && ServerConfig.Instance.BuffStationChanges) {
                // Undo what vanilla does
                player.ammoBox = false;

                // Do our stuff
                player.GetDamage(DamageClass.Ranged) += 0.15f;
                player.GetModPlayer<BuffStationPlayer>().ammoBox = true;

                return;
            }

            if (type == BuffID.Clairvoyance && ServerConfig.Instance.BuffStationChanges) {
                // Undo what vanilla does
                player.GetCritChance(DamageClass.Magic) -= 2;
                player.statManaMax2 -= 20;

                // Do our stuff, technically compounding on vanilla 
                player.GetDamage(DamageClass.Magic) += 0.05f;
                player.manaCost -= 0.06f;

                return;
            }
        }

        public override void ModifyBuffTip(int type, ref string tip, ref int rare) {
            if (type == BuffID.Sharpened && ServerConfig.Instance.BuffStationChanges) {
                tip = "12% increased melee damage and melee speed";
                return;
            }

            if (type == BuffID.AmmoBox && ServerConfig.Instance.BuffStationChanges) {
                tip = "15% increased ranged damage and 40% chance to not consume ammo";
                return;
            }

            if (type == BuffID.Clairvoyance && ServerConfig.Instance.BuffStationChanges) {
                tip = "10% increased magic damage and 8% reduced mana usage";
                return;
            }
        }
    }
}