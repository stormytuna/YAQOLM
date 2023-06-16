using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace YAQOLM.Common.UI.SpiralMirror;

public class SpiralMirrorButton : BetterUIImageButton
{
	private readonly Asset<Texture2D> faceTexture;

	public SpiralMirrorButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorButton")) {
		SetHoverImage(ModContent.Request<Texture2D>("YAQOLM/Assets/UI/SpiralMirrorButton_Hover"));
		this.faceTexture = faceTexture;
	}

	protected override void DrawSelf(SpriteBatch spriteBatch) {
		base.DrawSelf(spriteBatch);

		CalculatedStyle dimensions = GetDimensions();
		spriteBatch.Draw(faceTexture.Value, dimensions.Position(), Color.White * (IsMouseHovering ? VisibilityActive : VisibilityInactive));
	}
}