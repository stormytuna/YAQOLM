using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace YAQOLM.Common.Systems;

public static class PrefixSystem
{
	private static int BestPrefix(Item item) {
		// Items with no knockback
		if (item.knockBack <= 0f) {
			return PrefixID.Demonic;
		}

		// Tools
		if (item.pick > 0 || item.axe > 0 || item.hammer > 0) {
			return PrefixID.Light;
		}

		// Shortswords and terrarian
		if (ArraySystem.ItemsWithLegendary2.Contains(item.type)) {
			return PrefixID.Legendary2;
		}

		// Whips and melee weapons that are swung
		if ((item.CountsAsClass(DamageClass.Melee) && item.useStyle == ItemUseStyleID.Swing && !item.noMelee) || item.DamageType == DamageClass.SummonMeleeSpeed) {
			return PrefixID.Legendary;
		}

		// Other melee weapons
		if (item.CountsAsClass(DamageClass.Melee)) {
			return PrefixID.Godly;
		}

		// Other ranged weapons
		if (item.CountsAsClass(DamageClass.Ranged)) {
			return PrefixID.Unreal;
		}

		// Other magic weapons
		if (item.CountsAsClass(DamageClass.Magic)) {
			return PrefixID.Mythical;
		}

		// Other summon weapons
		if (item.CountsAsClass(DamageClass.Summon)) {
			return PrefixID.Ruthless;
		}

		// Default will just be zealous
		return PrefixID.Zealous;
	}

	public static bool ItemHasBestPrefix(Item item) => item.prefix == BestPrefix(item);

	public static void ApplyBestPrefix(ref Item item) {
		ApplyPrefix(ref item, BestPrefix(item));
	}

	public static void ApplyPrefix(ref Item item, int prefix) {
		bool favorited = item.favorited;
		item.netDefaults(item.netID);
		item.Prefix(prefix);
		item.position = Main.LocalPlayer.Center;
		item.favorited = favorited;
		PopupText.NewText(PopupTextContext.ItemReforge, item, item.stack, true);
		SoundEngine.PlaySound(SoundID.Item37);
	}
}