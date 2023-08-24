using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalBuffs;

public class SharpenedGlobalBuff : GlobalBuff
{
    public override void Update(int type, Player player, ref int buffIndex) {
        if (type != BuffID.Sharpened || !ServerConfig.Instance.BuffStationChanges) {
            return;
        }

        // Undo what vanilla does
        if (player.HeldItem.CountsAsClass(DamageClass.Melee)) {
            player.GetArmorPenetration(DamageClass.Melee) -= 12;
        }

        // Do our stuff
        player.GetDamage(DamageClass.Melee) += 0.12f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
    }

    public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare) {
        if (type == BuffID.Sharpened && ServerConfig.Instance.BuffStationChanges) {
            tip = Language.GetTextValue("Mods.YAQOLM.Buffs.Sharpened");
        }
    }
}