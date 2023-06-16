using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using YAQOLM.Common.UI.QuantumStrongbox;

namespace YAQOLM.Common.Systems;

[Autoload(Side = ModSide.Client)]
public class QuantumStrongboxUiSystem : ModSystem
{
	public static QuantumStrongboxUiSystem Instance => ModContent.GetInstance<QuantumStrongboxUiSystem>();

	public UserInterface QuantumStrongboxUi;
	public QuantumStrongboxUIState QuantumStrongboxUiState;

	public override void Load() {
		QuantumStrongboxUi = new UserInterface();

		QuantumStrongboxUiState = new QuantumStrongboxUIState();
		QuantumStrongboxUiState.Activate();
	}

	public static void Show() {
		Instance.QuantumStrongboxUi?.SetState(Instance.QuantumStrongboxUiState);
	}

	public static void Hide() {
		Instance.QuantumStrongboxUi?.SetState(null);
	}

	private GameTime oldUiGameTime;

	public override void UpdateUI(GameTime gameTime) {
		oldUiGameTime = gameTime;

		if (QuantumStrongboxUi?.CurrentState is not null) {
			QuantumStrongboxUi.Update(gameTime);
		}
	}

	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
		int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
		if (mouseTextIndex == -1) {
			return;
		}

		GameInterfaceDrawMethod drawMethod = () => {
			if (oldUiGameTime is not null && QuantumStrongboxUi?.CurrentState is not null) {
				QuantumStrongboxUi.Draw(Main.spriteBatch, oldUiGameTime);
			}

			return true;
		};

		layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("YAQOLM: QuantumLockboxUi", drawMethod, InterfaceScaleType.UI));
	}
}