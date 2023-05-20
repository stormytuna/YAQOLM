using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Systems;
using YAQOLM.Content.Items;

namespace YAQOLM.Common.Players;

public class KeybindPlayer : ModPlayer
{
	private int originalSelectedItem;
	private bool autoRevertSelectedItem;

	public override void ProcessTriggers(TriggersSet triggersSet) {
		if (KeybindSystem.RodOfDiscordKB.JustPressed) {
			QuickSwitchAndUse(ItemID.RodofDiscord);
		}

		if (KeybindSystem.SpiralMirrorHomeKB.JustPressed) {
			QuickSwitchAndUse(ModContent.ItemType<SpiralMirror>(), 0);
		}

		if (KeybindSystem.SpiralMirrorGraveKB.JustPressed) {
			QuickSwitchAndUse(ModContent.ItemType<SpiralMirror>(), 1);
		}

		if (KeybindSystem.SpiralMirrorReturnKB.JustPressed) {
			QuickSwitchAndUse(ModContent.ItemType<SpiralMirror>(), 2);
		}

		if (KeybindSystem.QuantumStrongboxPiggyBankKB.JustPressed) {
			QuickSwitchAndUse(ModContent.ItemType<QuantumStrongbox>(), 0);
		}

		if (KeybindSystem.QuantumStrongboxSafeKB.JustPressed) {
			QuickSwitchAndUse(ModContent.ItemType<QuantumStrongbox>(), 1);
		}

		if (KeybindSystem.QuantumStrongboxDefendersForgeKB.JustPressed) {
			QuickSwitchAndUse(ModContent.ItemType<QuantumStrongbox>(), 2);
		}

		if (KeybindSystem.QuantumStrongboxVoidVaultKB.JustPressed) {
			QuickSwitchAndUse(ModContent.ItemType<QuantumStrongbox>(), 3);
		}
	}

	public override void UpdateAutopause() {
		if (Main.playerInventory || Main.npcChatText != "" || Main.player[Main.myPlayer].sign >= 0 || Main.ingameOptionsWindow || Main.inFancyUI) {
			ProcessTriggers(null);
		}
	}

	private void QuickSwitchAndUse(int itemType, int extraData = -1) {
		// Guard clause
		if (Player.itemTime != 0 || Player.itemAnimation != 0) {
			return;
		}

		// Find our items index
		int index = -1;
		for (int i = 0; i < Player.inventory.Length; i++) {
			if (Player.inventory[i].type == itemType) {
				index = i;
				break;
			}
		}

		// Check we actually found an index
		if (index == -1) {
			return;
		}

		if (itemType == ModContent.ItemType<QuantumStrongbox>()) {
			QuantumStrongbox modItem = Player.inventory[index].ModItem as QuantumStrongbox;
			modItem.SetAndResetMode(extraData);
			return;
		}

		// Use our item at that index
		originalSelectedItem = Player.selectedItem;
		autoRevertSelectedItem = true;
		Player.selectedItem = index;
		Player.controlUseItem = true;

		if (extraData != -1) {
			if (itemType == ModContent.ItemType<SpiralMirror>()) {
				SpiralMirror modItem = Player.inventory[index].ModItem as SpiralMirror;
				modItem.SetAndResetMode(extraData);
			}
		}

		Player.ItemCheck();
	}

	public override void PostUpdate() {
		if (autoRevertSelectedItem && Player.itemTime == 0 && Player.itemAnimation == 0) {
			Player.selectedItem = originalSelectedItem;
			autoRevertSelectedItem = false;
		}
	}
}