﻿using Terraria;
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

	// Stops us holding left click after using a keybind and continuing to use the item
	private bool autoReuseHack;

	public override void ProcessTriggers(TriggersSet triggersSet) {
		if (KeybindSystem.RodOfDiscordKB.JustPressed) {
			QuickSwitchAndUse(ItemID.RodofDiscord);
		}

		int spiralMirrorType = ModContent.ItemType<SpiralMirror>();
		TeleportPlayer teleportPlayer = Player.GetModPlayer<TeleportPlayer>();
		if (KeybindSystem.SpiralMirrorHomeKB.JustPressed && Player.HasItem(spiralMirrorType)) {
			teleportPlayer.StartTeleport(0);
		}

		if (KeybindSystem.SpiralMirrorGraveKB.JustPressed && Player.HasItem(spiralMirrorType)) {
			teleportPlayer.StartTeleport(2);
		}

		if (KeybindSystem.SpiralMirrorReturnKB.JustPressed && Player.HasItem(spiralMirrorType)) {
			teleportPlayer.StartTeleport(3);
		}

		if (KeybindSystem.SpiralMirrorOceanKB.JustPressed && Player.HasItem(spiralMirrorType)) {
			teleportPlayer.StartTeleport(4);
		}

		if (KeybindSystem.SpiralMirrorHellKB.JustPressed && Player.HasItem(spiralMirrorType)) {
			teleportPlayer.StartTeleport(5);
		}
	}

	public override void UpdateAutopause() {
		if (Main.playerInventory || Main.npcChatText != "" || Main.player[Main.myPlayer].sign >= 0 || Main.ingameOptionsWindow || Main.inFancyUI) {
			ProcessTriggers(null);
		}
	}

	private void QuickSwitchAndUse(int itemType) {
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

		// Use our item at that index
		originalSelectedItem = Player.selectedItem;
		autoRevertSelectedItem = true;
		Player.selectedItem = index;
		autoReuseHack = Player.HeldItem.autoReuse;
		Player.HeldItem.autoReuse = false;
		Player.controlUseItem = true;

		Player.ItemCheck();
	}

	public override void PostUpdate() {
		if (autoRevertSelectedItem && Player.itemTime == 0 && Player.itemAnimation == 0) {
			Player.HeldItem.autoReuse = autoReuseHack;
			Player.selectedItem = originalSelectedItem;
			autoRevertSelectedItem = false;
		}
	}
}