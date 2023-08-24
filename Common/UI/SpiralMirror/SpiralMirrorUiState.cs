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
		homeButton.OnLeftClick += HomeButtonOnLeftClick;
		homeButton.Tooltip = "Teleport home";
		mainPanel.Append(homeButton);

        SpiralMirrorButton spawnButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorSpawn"));
        spawnButton.Width.Set(40f, 0f);
        spawnButton.Height.Set(40f, 0f);
        spawnButton.HAlign = 0.65f;
        spawnButton.VAlign = 0.35f;
        spawnButton.OnLeftClick += SpawnButtonOnLeftClick;
        spawnButton.Tooltip = "Teleport home";
        mainPanel.Append(spawnButton);

        SpiralMirrorButton graveButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorGrave"));
		graveButton.Width.Set(40f, 0f);
		graveButton.Height.Set(40f, 0f);
		graveButton.HAlign = 0.35f;
		graveButton.VAlign = 0.35f;
		graveButton.OnLeftClick += GraveButtonOnLeftClick;
		graveButton.Tooltip = "Teleport to where you last died";
		mainPanel.Append(graveButton);

		SpiralMirrorButton returnButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorReturn"));
		returnButton.Width.Set(40f, 0f);
		returnButton.Height.Set(40f, 0f);
		returnButton.HAlign = 0.65f;
		returnButton.VAlign = 0.65f;
		returnButton.OnLeftClick += ReturnButtonOnLeftClick;
		returnButton.Tooltip = "Teleport home and leave a portal";
		mainPanel.Append(returnButton);

		SpiralMirrorButton oceanButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorOcean"));
		oceanButton.Width.Set(40f, 0f);
		oceanButton.Height.Set(40f, 0f);
		oceanButton.HAlign = 0.35f;
		oceanButton.VAlign = 0.65f;
		oceanButton.OnLeftClick += OceanButtonOnLeftClick;
		oceanButton.Tooltip = "Teleport to the ocean";
		mainPanel.Append(oceanButton);

		SpiralMirrorButton hellButton = new(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorHell"));
		hellButton.Width.Set(40f, 0f);
		hellButton.Height.Set(40f, 0f);
		hellButton.HAlign = 0.5f;
		hellButton.VAlign = 0.9f;
		hellButton.OnLeftClick += HellButtonOnLeftClick;
		hellButton.Tooltip = "Teleport to the underworld";
		mainPanel.Append(hellButton);
	}

    private void HomeButtonOnLeftClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(0);
	private void SpawnButtonOnLeftClick(UIMouseEvent evt, UIElement listeningElement) => SpiralButtonClick(1);
	private void GraveButtonOnLeftClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(2);
	private void ReturnButtonOnLeftClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(3);
	private void OceanButtonOnLeftClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(4);
	private void HellButtonOnLeftClick(UIMouseEvent evt, UIElement listeningelement) => SpiralButtonClick(5);

	private void SpiralButtonClick(int teleportStyle) {
		TeleportPlayer teleportPlayer = Main.LocalPlayer.GetModPlayer<TeleportPlayer>();
		teleportPlayer.StartTeleport(teleportStyle);
		SpiralMirrorUiSystem.Hide();
	}
}