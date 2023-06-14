using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace YAQOLM.Helpers;

public static class Extensions
{
	private static readonly string[] VanillaTooltipOrder = { "ItemName", "Favorite", "FavoriteDesc", "Social", "SocialDesc", "Damage", "CritChance", "Speed", "Knockback", "FishingPower", "NeedsBait", "BaitPower", "WandConsumables", "Quest", "Vanity", "Defense", "PickPower", "AxePower", "HammerPower", "TileBoost", "HealLife", "HealMana", "UseMana", "Placeable", "Ammo", "Consumable", "Material", "Tooltip#", "EtherianManaWarning", "WellFedExpert", "BuffTime", "OneDropLogo", "PrefixDamage", "PrefixSpeed", "PrefixCritChance", "PrefixUseMana", "PrefixSize", "PrefixShootSpeed", "PrefixKnockback", "PrefixAccDefense", "PrefixAccMaxMana", "PrefixAccCritChance", "PrefixAccDamage", "PrefixAccMoveSpeed", "PrefixAccMeleeSpeed", "SetBonus", "Expert", "SpecialPrice", "Price" };

	public static void InsertTooltip(this List<TooltipLine> tooltips, TooltipLine newTooltip, string afterVanillaTooltip)
	{
		int index = Array.IndexOf(VanillaTooltipOrder, afterVanillaTooltip);
		if (index < 0)
		{
			ModContent.GetInstance<YAQOLM>().Logger.Error($"Tooltip with name {afterVanillaTooltip} does not exist");
			return;
		}

		if (afterVanillaTooltip == "Tooltip#")
		{
			int tooltipIndex = tooltips.FindLastIndex(tip => tip.Name.StartsWith("Tooltip"));
			tooltips.Insert(tooltipIndex + 1, newTooltip);
			return;
		}

		string[] afterTooltips = VanillaTooltipOrder.Take(index + 1).ToArray();
		for (int i = afterTooltips.Length - 1; i >= 0; i--)
		{
			string vanillaTooltipName = afterTooltips[i];
			for (int j = 0; j < tooltips.Count; j++)
			{
				TooltipLine tooltip = tooltips[j];
				if (vanillaTooltipName == tooltip.Name)
				{
					tooltips.Insert(j + 1, newTooltip);
					return;
				}
			}
		}

		ModContent.GetInstance<YAQOLM>().Logger.Error($"Tooltip could not be inserted after {afterVanillaTooltip}");
	}

	public static void ReplaceTooltip(this List<TooltipLine> tooltips, TooltipLine newTooltip, string vanillaTooltip)
	{
		int index = Array.IndexOf(VanillaTooltipOrder, vanillaTooltip);
		if (index < 0 && !vanillaTooltip.StartsWith("Tooltip"))
		{
			ModContent.GetInstance<YAQOLM>().Logger.Error($"Tooltip with name {vanillaTooltip} does not exist");
			return;
		}

		int tooltipIndex = tooltips.FindIndex(tip => tip.Name == vanillaTooltip);
		if (tooltipIndex < 0)
		{
			string previousVanillaTooltip = vanillaTooltip.StartsWith("Tooltip") ? "Material" : VanillaTooltipOrder[index - 1];
			tooltips.InsertTooltip(newTooltip, previousVanillaTooltip);
			return;
		}

		tooltips[tooltipIndex] = newTooltip;
	}

	public static void RemoveAllHack<TSource>(this IEnumerable<TSource> source)
	{
		List<TSource> hack = (List<TSource>)source;
		hack.RemoveAll(_ => true);
	}

	public static bool NotAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => !source.Any(predicate);

	public static bool IsVanillaItem(this int itemType) => itemType < ItemID.Count;

	public static bool IsVanillaItem(this Item item) => IsVanillaItem(item.type);
	/// <summary>Checks whether the player has an item in their inventory, piggy bank, safe, defenders forge or void bag</summary>
	/// <param name="player">The player to check for</param>
	/// <param name="itemType">The type of item to search for</param>
	/// <param name="checkPiggyBank">If set to true, this will search the piggy bank</param>
	/// <param name="checkSafe">If set to true, this will search the safe</param>
	/// <param name="checkDefendersForge">If set to true, this will search the defenders forge</param>
	/// <param name="checkVoidBag">If set to true, this wil search the void bag</param>
	/// <returns>Returns true when an item is found, returns false otherwise</returns>
	public static bool TryFindItem(this Player player, int itemType, out Item item, bool checkPiggyBank = false, bool checkSafe = false, bool checkDefendersForge = false, bool checkVoidBag = true)
	{
		item = null;

		foreach (Item i in player.inventory)
		{
			if (i.type == itemType)
			{
				item = i;
				return true;
			}
		}

		foreach (Item i in player.bank.item)
		{
			if (i.type == itemType)
			{
				item = i;
				return true;
			}
		}

		foreach (Item i in player.bank2.item)
		{
			if (i.type == itemType)
			{
				item = i;
				return true;
			}
		}

		foreach (Item i in player.bank3.item)
		{
			if (i.type == itemType)
			{
				item = i;
				return true;
			}
		}

		foreach (Item i in player.bank4.item)
		{
			if (i.type == itemType)
			{
				item = i;
				return true;
			}
		}

		return false;
	}
}