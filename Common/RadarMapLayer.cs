using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.UI;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common;

public class RadarMapLayer : ModMapLayer
{
	private bool NPCHasHeadTexture(NPC npc) {
		bool hasRegularNPCHead = NPC.TypeToDefaultHeadIndex(npc.type) != -1;
		bool hasBossNPCHead = npc.GetBossHeadTextureIndex() != -1;
		return hasBossNPCHead || hasRegularNPCHead;
	}

	private Asset<Texture2D> _radarMapLayerNPCHostile;
	private Asset<Texture2D> RadarMapLayerNPCHostile => _radarMapLayerNPCHostile ??= ModContent.Request<Texture2D>("YAQOLM/Assets/UI/RadarMapLayerNPCHostile");
	private Asset<Texture2D> _radarMapLayerNPCFriendly;
	private Asset<Texture2D> RadarMapLayerNPCFriendly => _radarMapLayerNPCFriendly ??= ModContent.Request<Texture2D>("YAQOLM/Assets/UI/RadarMapLayerNPCFriendly");

	public override void Draw(ref MapOverlayDrawContext context, ref string text) {
		if (!Main.LocalPlayer.accThirdEye || Main.LocalPlayer.hideInfo[InfoDisplay.Radar.Type] || !ServerConfig.Instance.RadarEnemyMap) {
			return;
		}

		foreach (NPC npc in Main.npc.SkipLast(1)) {
			if (!npc.active || !npc.WithinRange(Main.LocalPlayer.Center, 2000f) || NPCHasHeadTexture(npc)) {
				continue;
			}

			Asset<Texture2D> texture = npc.CountsAsACritter || npc.isLikeATownNPC ? RadarMapLayerNPCFriendly : RadarMapLayerNPCHostile;
			context.Draw(texture.Value, npc.Center.ToTileCoordinates().ToVector2(), Color.White * 0.8f, new SpriteFrame(1, 1, 0, 0), 1f, 1f, Alignment.Center);
		}
	}
}