using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Content.Items;

public class FlowerOfTheJungle : ModItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.FlowerOfTheJungle;

	public override void SetStaticDefaults() {
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;

		NPCID.Sets.MPAllowedEnemies[NPCID.Plantera] = true;
	}

	public override void SetDefaults() {
		Item.width = 22;
		Item.height = 22;
		Item.maxStack = 20;
		Item.value = 100;
		Item.rare = ItemRarityID.Blue;
		Item.useAnimation = 30;
		Item.useTime = 30;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.consumable = true;
	}

	public override bool CanUseItem(Player player) => Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.AnyNPCs(NPCID.Plantera);

	public override bool? UseItem(Player player) {
		if (player.whoAmI == Main.myPlayer) {
			SoundEngine.PlaySound(SoundID.Roar, player.Center);

			int type = NPCID.Plantera;

			if (Main.netMode != NetmodeID.MultiplayerClient) {
				// If the player is not in multiplayer, spawn directly
				NPC.SpawnOnPlayer(player.whoAmI, type);
			} else {
				// If the player is in multiplayer, request a spawn
				// This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in this class above
				NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
			}
		}

		return true;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.JungleSpores, 5)
			.AddIngredient(ItemID.ChlorophyteBar, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}