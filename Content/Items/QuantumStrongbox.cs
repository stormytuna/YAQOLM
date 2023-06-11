using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items;

public class QuantumStrongbox : ModItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.QuantumStrongbox;

	public override void SetStaticDefaults() {
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		Tooltip.SetDefault("Allows easy access to your piggy bank, safe, defender's forge and void vault\nPicks up items when your inventory is full\n'Schrödinger's storage'");
	}

	public override void SetDefaults() {
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useTime = 30;
		Item.useAnimation = 30;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.PiggyBank)
			.AddIngredient(ItemID.Safe)
			.AddIngredient(ItemID.DefendersForge)
			.AddIngredient(ItemID.VoidLens)
			.AddIngredient(ItemID.SoulofFright, 3)
			.AddIngredient(ItemID.SoulofMight, 3)
			.AddIngredient(ItemID.SoulofSight, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}

	public override void UpdateInventory(Player player) {
		player.IsVoidVaultEnabled = true;
	}

	public override bool? UseItem(Player player) {
		if (player.whoAmI == Main.myPlayer) {
			QuantumStrongboxUiSystem.Show();
		}

		return true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		player.itemLocation += new Vector2(-6f * player.direction, 2f);
	}
}