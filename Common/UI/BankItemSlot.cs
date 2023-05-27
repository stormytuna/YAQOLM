using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace YAQOLM.Common.UI;

public class BankItemSlot : EnhancedItemSlot
{
	public int BankType;

	private Item[] Bank => BankType switch {
		0 => Main.LocalPlayer.bank.item,
		1 => Main.LocalPlayer.bank2.item,
		2 => Main.LocalPlayer.bank3.item,
		3 => Main.LocalPlayer.bank4.item,
		_ => throw new ArgumentException($"Bank type {BankType} not recognised")
	};
	
	public BankItemSlot(int bankType, int slot, int context = ItemSlot.Context.InventoryItem, float scale = 1) : base(slot, context, scale) {
		BankType = bankType;

		OnItemChanged += item => {
			Bank[slot] = StoredItem;
		};
	}

	public override void Update(GameTime gameTime) {
		base.Update(gameTime);

		if (Bank[slot].IsNotSameTypePrefixAndStack(StoredItem)) {
			SetItem(Bank[slot]);
		}
	}
}