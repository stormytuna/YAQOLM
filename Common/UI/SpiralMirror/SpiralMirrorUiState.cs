using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using YAQOLM.Common.Players;
using YAQOLM.Common.Systems;

namespace YAQOLM.Common.UI.SpiralMirror;

public class SpiralMirrorUiState : UIState
{
	private MouseCenteredUIPanel mainPanel;

	public override void OnInitialize() {
		mainPanel = new MouseCenteredUIPanel();
		mainPanel.BackgroundColor *= 0f;
		mainPanel.BorderColor *= 0f;
		mainPanel.Width.Set(200f, 0f);
		mainPanel.Height.Set(200f, 0f);
		Append(mainPanel);

		SpiralMirrorButton homeButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorHome"));
		homeButton.Width.Set(40f, 0f);
		homeButton.Height.Set(40f, 0f);
		homeButton.HAlign = 0.5f;
		homeButton.VAlign = 0.1f;
		homeButton.OnClick += HomeButtonOnOnClick;
		homeButton.Tooltip = "Teleport home";
		mainPanel.Append(homeButton);

		SpiralMirrorButton graveButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorGrave"));
		graveButton.Width.Set(40f, 0f);
		graveButton.Height.Set(40f, 0f);
		graveButton.HAlign = 0.1f;
		graveButton.VAlign = 0.45f;
		graveButton.OnClick += GraveButtonOnOnClick;
		graveButton.Tooltip = "Teleport to where you last died";
		mainPanel.Append(graveButton);

		SpiralMirrorButton returnButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorReturn"));
		returnButton.Width.Set(40f, 0f);
		returnButton.Height.Set(40f, 0f);
		returnButton.HAlign = 0.9f;
		returnButton.VAlign = 0.45f;
		returnButton.OnClick += ReturnButtonOnOnClick;
		returnButton.Tooltip = "Teleport home and leave a portal";
		mainPanel.Append(returnButton);

		SpiralMirrorButton oceanButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorOcean"));
		oceanButton.Width.Set(40f, 0f);
		oceanButton.Height.Set(40f, 0f);
		oceanButton.HAlign = 0.25f;
		oceanButton.VAlign = 0.9f;
		oceanButton.OnClick += OceanButtonOnOnClick;
		oceanButton.Tooltip = "Teleport to the ocean";
		mainPanel.Append(oceanButton);

		SpiralMirrorButton hellButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorHell"));
		hellButton.Width.Set(40f, 0f);
		hellButton.Height.Set(40f, 0f);
		hellButton.HAlign = 0.75f;
		hellButton.VAlign = 0.9f;
		hellButton.OnClick += HellButtonOnOnClick;
		hellButton.Tooltip = "Teleport to the underworld";
		mainPanel.Append(hellButton);
	}

	private void HomeButtonOnOnClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(0);
	private void GraveButtonOnOnClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(2);
	private void ReturnButtonOnOnClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(3);
	private void OceanButtonOnOnClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(4);
	private void HellButtonOnOnClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(5);

	private void SpiralButtonClick(int teleportStyle) {
		TeleportPlayer teleportPlayer = Main.LocalPlayer.GetModPlayer<TeleportPlayer>();
		teleportPlayer.StartTeleport(teleportStyle);
		SpiralMirrorUiSystem.Hide();
	}
}