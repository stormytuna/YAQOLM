using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalNPCs {
    public class NPCLootGlobalNPC : GlobalNPC {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (npc.type == NPCID.KingSlime && ServerConfig.Instance.KingSlimeDropsSlimeStaff) {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.LegacyHack_IsBossAndNotExpert(), ItemID.SlimeStaff, 5));
                return;
            }

            if (npc.type == NPCID.Shark && ServerConfig.Instance.SharksDropSharktoothNecklace) {
                npcLoot.Add(ItemDropRule.Common(ItemID.SharkToothNecklace, 25));
                return;
            }

            if (ServerConfig.Instance.OneFromOptionsToFewFromOptions) {
                foreach (var rule in npcLoot.Get()) {
                    if (rule is OneFromOptionsDropRule drop && drop.dropIds.Length > 5) {
                        int amount = (int)MathF.Ceiling((float)drop.dropIds.Length / 4f);
                        FewFromOptionsDropRule newRule = new(amount, drop.chanceDenominator, drop.chanceNumerator, drop.dropIds);
                        newRule.ChainedRules.AddRange(drop.ChainedRules);

                        npcLoot.Remove(rule);
                        npcLoot.Add(newRule);
                    }
                }
            }
        }
    }
}