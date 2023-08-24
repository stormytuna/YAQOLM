using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using YAQOLM.Common.UI.SpiralMirror;

namespace YAQOLM.Common.Systems;

[Autoload(Side = ModSide.Client)]
public class SpiralMirrorUiSystem : ModSystem
{
    public static SpiralMirrorUiSystem Instance => ModContent.GetInstance<SpiralMirrorUiSystem>();

    public UserInterface SpiralMirrorUi;
    public SpiralMirrorUiState SpiralMirrorUiState;

    public override void Load() {
        SpiralMirrorUi = new UserInterface();

        SpiralMirrorUiState = new SpiralMirrorUiState();
        SpiralMirrorUiState.Activate();
    }

    public static void Show() => Instance.SpiralMirrorUi?.SetState(Instance.SpiralMirrorUiState);

    public static void Hide() => Instance.SpiralMirrorUi?.SetState(null);

    public static void Toggle() {
        if (Instance.SpiralMirrorUi?.CurrentState is null) {
            Show();
        } else {
            Hide();
        }
    }

    private GameTime oldUiGameTime;

    public override void UpdateUI(GameTime gameTime) {
        oldUiGameTime = gameTime;

        if (SpiralMirrorUi?.CurrentState is not null) {
            SpiralMirrorUi.Update(gameTime);
        }
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        if (mouseTextIndex == -1) {
            return;
        }

        bool drawMethod() {
            if (oldUiGameTime is not null && SpiralMirrorUi?.CurrentState is not null) {
                SpiralMirrorUi.Draw(Main.spriteBatch, oldUiGameTime);
            }

            return true;
        }

        layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("YAQOLM: SpiralMirrorUi", drawMethod, InterfaceScaleType.UI));
    }
}