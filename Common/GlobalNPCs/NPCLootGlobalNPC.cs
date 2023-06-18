using System;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.GlobalNPCs;

public class NPCLootGlobalNPC : GlobalNPC
{
	private static readonly int[] LunarPillars = { NPCID.LunarTowerSolar, NPCID.LunarTowerVortex, NPCID.LunarTowerNebula, NPCID.LunarTowerStardust };

	private static void MakeLunarPillarDropRulesBetter(DropOneByOne drop) {
		PropertyInfo parametersProperty = typeof(DropOneByOne).GetProperty("parameters", BindingFlags.Public | BindingFlags.Instance);
		DropOneByOne.Parameters newParameters = new() {
			MinimumItemDropsCount = 80,
			MaximumItemDropsCount = 100,
			ChanceNumerator = 1,
			ChanceDenominator = 1,
			MinimumStackPerChunkBase = 1,
			MaximumStackPerChunkBase = 1,
			BonusMinDropsPerChunkPerPlayer = 1,
			BonusMaxDropsPerChunkPerPlayer = 1
		};
		parametersProperty.SetValue(drop, newParameters);
	}

	public static void OneFromOptionsToFewFromOptions(Action<IItemDropRule> setRuleFunc, IItemDropRule rule) {
		if (rule is OneFromOptionsDropRule oneFromOptionsRule && oneFromOptionsRule.dropIds.Length > 4) {
			int amountDropped = (int)MathF.Ceiling(oneFromOptionsRule.dropIds.Length / 4f);
			FewFromOptionsDropRule newRule = new(amountDropped, oneFromOptionsRule.chanceDenominator, oneFromOptionsRule.chanceNumerator, oneFromOptionsRule.dropIds);
			newRule.ChainedRules.AddRange(oneFromOptionsRule.ChainedRules);
			setRuleFunc(newRule);
		}

		if (rule is OneFromRulesRule oneFromRulesRule && oneFromRulesRule.options.Length > 4) {
			int amountDropped = (int)MathF.Ceiling(oneFromRulesRule.options.Length / 4f);
			FewFromRulesRule newRule = new(amountDropped, oneFromRulesRule.chanceNumerator, oneFromRulesRule.options);
			newRule.chanceDenominator = oneFromRulesRule.chanceDenominator;
			newRule.ChainedRules.AddRange(oneFromRulesRule.ChainedRules);
			setRuleFunc(newRule);
		}

		if (rule is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsNoLuckRule && oneFromOptionsNoLuckRule.dropIds.Length > 4) {
			int amountDropped = (int)MathF.Ceiling(oneFromOptionsNoLuckRule.dropIds.Length / 4f);
			FewFromOptionsNotScaledWithLuckDropRule newRule = new(amountDropped, oneFromOptionsNoLuckRule.chanceDenominator, oneFromOptionsNoLuckRule.chanceNumerator, oneFromOptionsNoLuckRule.dropIds);
			newRule.ChainedRules.AddRange(oneFromOptionsNoLuckRule.ChainedRules);
			setRuleFunc(newRule);
		}

		for (int index = 0; index < rule.ChainedRules.Count; index++) {
			IItemDropRuleChainAttempt chain = rule.ChainedRules[index];
			if (chain is Chains.TryIfSucceeded succeeded) {
				OneFromOptionsToFewFromOptions(newRule => {
					rule.ChainedRules.Remove(chain);
					rule.ChainedRules.Add(new Chains.TryIfSucceeded(newRule));
				}, succeeded.RuleToChain);
			}
		}
	}

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
			foreach (IItemDropRule rule in npcLoot.Get()) {
				OneFromOptionsToFewFromOptions(newRule => {
					npcLoot.Remove(rule);
					npcLoot.Add(newRule);
				}, rule);
			}
		}

		if (LunarPillars.Contains(npc.type) && ServerConfig.Instance.LunarPillarRework) {
			foreach (IItemDropRule rule in npcLoot.Get()) {
				if (rule is DropBasedOnExpertMode expertRule) {
					if (expertRule.ruleForNormalMode is DropOneByOne normalDrop) {
						MakeLunarPillarDropRulesBetter(normalDrop);
					}

					if (expertRule.ruleForExpertMode is DropOneByOne expertDrop) {
						MakeLunarPillarDropRulesBetter(expertDrop);
					}
				}
			}
		}
	}
}