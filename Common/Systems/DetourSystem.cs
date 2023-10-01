using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Systems;

public class DetourSystem : ModSystem
{
	public override void Load() {
		if (ServerConfig.Instance.BetterBombTiles) {
			On_Projectile.CanExplodeTile += Projectile_CanExplodeTile;
		}

		if (ServerConfig.Instance.BetterBombWalls) {
			On_Projectile.ExplodeTiles += Projectile_ExplodeTiles;
		}

		if (ServerConfig.Instance.BossesIncreaseHappiness) {
			On_ShopHelper.GetShoppingSettings += ShopHelper_GetShoppingSettings;
		}
	}

	private bool Projectile_CanExplodeTile(On_Projectile.orig_CanExplodeTile orig, Projectile self, int x, int y) {
		bool canExplode = orig(self, x, y);

		if (!canExplode) {
			// Check we absolutely cannot explode this tile
			int type = Main.tile[x, y].TileType;
			Player player = Main.player[self.owner];
			if (Main.tileDungeon[type] || TileID.Sets.BasicChest[type] || ArraySystem.IndestructibleTiles.Contains(type)) {
				return canExplode;
			}

			// Check if our player could mine this
			if (player.HasEnoughPickPowerToHurtTile(x, y)) {
				canExplode = true;
			}
		}

		return canExplode;
	}

	private void Projectile_ExplodeTiles(On_Projectile.orig_ExplodeTiles orig, Projectile self, Vector2 compareSpot, int radius, int minI, int maxI, int minJ, int maxJ, bool wallSplode) => orig(self, compareSpot, radius, minI, maxI, minJ, maxJ, true);

	private ShoppingSettings ShopHelper_GetShoppingSettings(On_ShopHelper.orig_GetShoppingSettings orig, ShopHelper self, Player player, NPC npc) {
		ShoppingSettings settings = orig(self, player, npc);

		if (NPC.downedBoss2) {
			settings.PriceAdjustment *= 0.92f;
		}

		if (Main.hardMode) {
			settings.PriceAdjustment *= 0.92f;
		}

		if (NPC.downedPlantBoss) {
			settings.PriceAdjustment *= 0.92f;
		}

		if (NPC.downedMoonlord) {
			settings.PriceAdjustment *= 0.9f;
		}

		return settings;
	}
}