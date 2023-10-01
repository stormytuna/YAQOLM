using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace YAQOLM.Common.UI;

public class MouseCenteredUIPanel : UIPanel
{
	private bool centreOnMouse;

	public override void OnActivate() {
		base.OnActivate();
		centreOnMouse = true;
	}

	public override void Update(GameTime gameTime) {
		base.Update(gameTime);
		if (!centreOnMouse) {
			return;
		}

		centreOnMouse = false;
		Left.Set(Main.mouseX - Width.Pixels / 2f, 0f);
		Top.Set(Main.mouseY - Height.Pixels / 2f, 0f);
		Recalculate();
	}

	protected override void DrawChildren(SpriteBatch spriteBatch) {
		if (!centreOnMouse) {
			base.DrawChildren(spriteBatch);
		}
	}
}