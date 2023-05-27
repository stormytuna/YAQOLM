using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using YAQOLM.Common.Systems;

namespace YAQOLM.Common.UI;

public class QuantumStrongboxUIState : UIState
{
	private const int numRows = 4;
	private const int numColumns = 10;
	
	public int Bank { private get; set; } = 0;

	private UIText header;

	private BankItemSlot[] itemSlots;

	public override void OnInitialize() {
		UIDragablePanel panel = new(true);
		panel.Width.Set(505, 0);
		panel.Height.Set(240, 0);
		panel.HAlign = 0.4f;
		panel.VAlign = 0.45f;
		panel.OnMenuClose += QuantumStrongboxUiSystem.Hide;
		Append(panel);

		UIText header = new(HeaderText);
		header.Top.Set(5, 0f);
		header.Left.Set(40, 0f);
		panel.Append(header);
		this.header = header;

		itemSlots = new BankItemSlot[numRows * numColumns];
		for (int row = 0; row < numRows; row++) {
			for (int column = 0; column < numColumns; column++) {
				int index = (row * 10) + column;
				BankItemSlot itemSlot = new(Bank, index, ItemSlot.Context.BankItem, 0.8f);
				itemSlot.Top.Set((row * 50) + 40, 0f);
				itemSlot.Left.Set((column * 50) + 5, 0f);
				itemSlots[index] = itemSlot;
				panel.Append(itemSlot);
			}
		}

		UIImageButton previousBankButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/PreviousButton"));
		previousBankButton.Width.Set(25, 0f);
		previousBankButton.Height.Set(25, 0f);
		previousBankButton.Top.Set(5, 0f);
		previousBankButton.Left.Set(5, 0f);
		previousBankButton.OnLeftClick += OnPreviousBankButtonClick;
		panel.Append(previousBankButton);

		UIImageButton nextBankButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/NextButton"));
		nextBankButton.Width.Set(25, 0f);
		nextBankButton.Height.Set(25, 0f);
		nextBankButton.Top.Set(5, 0f);
		nextBankButton.Left.Set(220 , 0f);
		nextBankButton.OnLeftClick += OnNextBankButtonClick;
		panel.Append(nextBankButton);
	}

	private void OnNextBankButtonClick(UIMouseEvent mouseEvent, UIElement listener) {
		int nextSlot = Bank + 1;
		if (nextSlot > 3) {
			nextSlot = 0;
		}

		SetNextBank(nextSlot);
	}

	private void OnPreviousBankButtonClick(UIMouseEvent mouseEvent, UIElement listener) {
		int nextSlot = Bank - 1;
		if (nextSlot < 0) {
			nextSlot = 3;
		}
		
		SetNextBank(nextSlot);
	}

	public void SetNextBank(int bankSlot) {
		Bank = bankSlot;
		header?.SetText(HeaderText);
		foreach (BankItemSlot itemSlot in itemSlots) {
			itemSlot.BankType = bankSlot;
		}
	}

	private string HeaderText => Bank switch {
		0 => $"[i:{ItemID.PiggyBank}] {Lang.GetItemName(ItemID.PiggyBank)}",
		1 => $"[i:{ItemID.Safe}] {Lang.GetItemName(ItemID.Safe)}",
		2 => $"[i:{ItemID.DefendersForge}] {Lang.GetItemName(ItemID.DefendersForge)}",
		3 => $"[i:{ItemID.VoidVault}] {Lang.GetItemName(ItemID.VoidVault)}",
		_ => throw new ArgumentException($"Could not recognise bank with ID {Bank}")
	};
}