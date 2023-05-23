using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items;

public class SpiralMirror : ModItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.SpiralMirror;

	public override void SetStaticDefaults() {
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		// Tooltip.SetDefault("Use to teleport somewhere\nRight click to switch mode\nAllows you to teleport to party members, NPCs and Pylons\nDisplays everything\n'Can I interest you in everything, all of the time?'");
	}

	public override void SetDefaults() {
		Item.useTurn = true;
		Item.width = 30;
		Item.height = 30;
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(gold: 5);

		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useTime = 90;
		Item.useAnimation = 90;
		Item.UseSound = SoundID.Item6;
	}

	public override bool CanUseItem(Player player) => _mode == 2 ? player.GetModPlayer<WarpedMirrorPlayer>().canUseWarpedMirror : true;

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<WarpedMirror>())
			.AddIngredient(ModContent.ItemType<MysticMirror>())
			.AddIngredient(ModContent.ItemType<RunicMirror>())
			.AddIngredient(ItemID.CellPhone)
			.AddIngredient(ItemID.MagicConch)
			.AddIngredient(ItemID.DemonConch)
			.AddIngredient(ItemID.ChlorophyteBar, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}

	public override bool CanRightClick() => true;

	public override void RightClick(Player player) {
		_mode++;
		if (_mode > 5) {
			_mode = 0;
		}

		Item.stack++;
	}

	private int _mode;
	private int _resetMode = -1;

	public void SetAndResetMode(int newMode) {
		_resetMode = _mode;
		_mode = newMode;
	}

	private void MakeDust(Player player) {
		Dust d;
		Vector2 vector;
		switch (_mode) {
			case 0:
				d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.1f);
				d.velocity *= 0.5f;
				return;
			case 1:
				Color dustColor = Main.rand.Next(4) switch {
					0 or 1 => new Color(100, 255, 100),
					2 => Color.Yellow,
					3 => Color.White,
					_ => Color.Green
				};
				
				d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.LastPrism);
				d.noGravity = true;
				d.color = dustColor;
				d.velocity *= 2f;
				d.scale = 0.8f + Main.rand.NextFloat() * 0.6f;
				return;
			case 2:
				Dust.NewDust(player.position, player.width, player.height, DustID.Demonite, 0f, 0f, 150, default, 1.1f);
				return;
			case 3:
				d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, Color.Cyan, 1.1f);
				d.velocity *= 0.5f;
				return;
			case 4:
				vector = Vector2.UnitY.RotatedBy(player.itemAnimation * MathHelper.TwoPi / 30f) * new Vector2(15f, 0f);
				d = Dust.NewDustPerfect(player.Bottom + vector, Dust.dustWater());
				d.velocity.Y *= 0f;
				d.velocity.Y -= 4.5f;
				d.velocity.X *= 1.5f;
				d.scale = 0.8f;
				d.alpha = 130;
				d.noGravity = true;
				d.fadeIn = 1.1f;
				return;
			case 5:
				vector = Vector2.UnitY.RotatedBy(player.itemAnimation * MathHelper.TwoPi / 30f) * new Vector2(15f, 0f);
				d = Dust.NewDustPerfect(player.Bottom + vector, 35);
				d.velocity.Y *= 0f;
				d.velocity.Y -= 4.5f;
				d.velocity.X *= 1.5f;
				d.scale = 0.8f;
				d.alpha = 130;
				d.noGravity = true;
				d.fadeIn = 1.1f;
				return;
		}
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		WarpedMirrorPlayer warpedPlayer = player.GetModPlayer<WarpedMirrorPlayer>();

		// Make some dust each frame
		if (Main.rand.NextBool()) {
			MakeDust(player);
		}

		// Setup item time correctly
		if (player.ItemTimeIsZero) {
			player.ApplyItemTime(Item);
		}

		// Exactly halfway through
		if (player.itemTime == player.itemTimeMax / 2) {
			// Dust where the player starts
			for (int i = 0; i < 70; i++) {
				MakeDust(player);
			}

			// Release grappling hooks
			player.grappling[0] = -1;
			player.grapCount = 0;
			for (int i = 0; i < Main.projectile.Length; i++) {
				if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].aiStyle == 7) {
					Main.projectile[i].Kill();
				}
			}

			// Teleport the player
			switch (_mode) {
				case 0:
					player.Spawn(PlayerSpawnContext.RecallFromItem);
					break;
				case 1:
					if (Main.netMode == NetmodeID.SinglePlayer) {
						player.Shellphone_Spawn();
					} else if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer) {
						NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 3);
					}

					break;
				case 2:
					player.Teleport(warpedPlayer.deathLocation, -1); // style: -1 prevents vanilla from doing any teleport effects
					warpedPlayer.canUseWarpedMirror = false;
					break;
				case 3:
					player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();
					break;
				case 4:
					if (Main.netMode == NetmodeID.SinglePlayer) {
						player.MagicConch();
					} else if (Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == player.whoAmI) {
						NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 1);
					}

					break;
				case 5:
					if (Main.netMode == NetmodeID.SinglePlayer) {
						player.DemonConch();
					} else if (Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == player.whoAmI) {
						NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 2);
					}

					break;
			}

			// Dust where the player appears
			for (int i = 0; i < 70; i++) {
				MakeDust(player);
			}

			// Reset mode if we need to
			if (_resetMode != -1) {
				_mode = _resetMode;
				_resetMode = -1;
			}
		}
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips) {
		TooltipLine tooltip = tooltips.FirstOrDefault(t => t.Name == "Tooltip0");
		float colorMult = Main.mouseTextColor / 255f;

		switch (_mode) {
			case 0:
				Color homeColor = new Color(51, 153, 255) * colorMult;
				tooltip.Text = Language.GetTextValue("Mods.YAQOLM.Items.SpiralMirror.TooltipHome").FormatWith(homeColor.Hex3());
				break;
			case 1:
				Color spawnColor = new Color(0, 204, 0) * colorMult;
				tooltip.Text = Language.GetTextValue("Mods.YAQOLM.Items.SpiralMirror.TooltipSpawn").FormatWith(spawnColor.Hex3());
				break;
			case 2:
				Color graveColor = new Color(255, 153, 51) * colorMult;
				tooltip.Text = Language.GetTextValue("Mods.YAQOLM.Items.SpiralMirror.TooltipGrave").FormatWith(graveColor.Hex3());
				break;
			case 3:
				Color returnColor = new Color(204, 0, 204) * colorMult;
				tooltip.Text = Language.GetTextValue("Mods.YAQOLM.Items.SpiralMirror.TooltipReturn").FormatWith(returnColor.Hex3());
				break;
			case 4:
				Color oceanColor = new Color(51, 255, 255) * colorMult;
				tooltip.Text = Language.GetTextValue("Mods.YAQOLM.Items.SpiralMirror.TooltipOcean").FormatWith(oceanColor.Hex3());
				break;
			case 5:
				Color hellColor = new Color(204, 0, 0) * colorMult;
				tooltip.Text = Language.GetTextValue("Mods.YAQOLM.Items.SpiralMirror.TooltipHell").FormatWith(hellColor.Hex3());
				break;
		}
	}

	public override void UpdateInventory(Player player) {
		player.GetModPlayer<RunicMirrorPlayer>().runicMirror = true;

		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
		player.accFishFinder = true;
		player.accDreamCatcher = true;
		player.accCritterGuide = true;
		player.accOreFinder = true;
		player.accStopwatch = true;
		player.accCalendar = true;
		player.accJarOfSouls = true;
		player.accThirdEye = true;
		player.accWeatherRadio = true;
	}
}