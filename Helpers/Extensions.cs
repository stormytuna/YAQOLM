using Terraria;

namespace YAQOLM.Helpers;

public static class Extensions
{
	/// <summary>Checks whether the player has an item in their inventory, piggy bank, safe, defenders forge or void bag</summary>
	/// <param name="player">The player to check for</param>
	/// <param name="itemType">The type of item to search for</param>
	/// <param name="checkPiggyBank">If set to true, this will search the piggy bank</param>
	/// <param name="checkSafe">If set to true, this will search the safe</param>
	/// <param name="checkDefendersForge">If set to true, this will search the defenders forge</param>
	/// <param name="checkVoidBag">If set to true, this wil search the void bag</param>
	/// <returns>Returns true when an item is found, returns false otherwise</returns>
	public static bool TryFindItem(this Player player, int itemType, out Item item, bool checkPiggyBank = false, bool checkSafe = false, bool checkDefendersForge = false, bool checkVoidBag = true) {
		item = null;

		foreach (Item i in player.inventory) {
			if (i.type == itemType) {
				item = i;
				return true;
			}
		}

		foreach (Item i in player.bank.item) {
			if (i.type == itemType) {
				item = i;
				return true;
			}
		}

		foreach (Item i in player.bank2.item) {
			if (i.type == itemType) {
				item = i;
				return true;
			}
		}

		foreach (Item i in player.bank3.item) {
			if (i.type == itemType) {
				item = i;
				return true;
			}
		}

		foreach (Item i in player.bank4.item) {
			if (i.type == itemType) {
				item = i;
				return true;
			}
		}

		return false;
	}
}