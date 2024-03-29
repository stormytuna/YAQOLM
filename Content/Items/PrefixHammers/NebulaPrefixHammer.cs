using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Systems;

namespace YAQOLM.Content.Items.PrefixHammers;

public class NebulaPrefixHammer : ModItem
{
	public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.PrefixHammers;

	public override void SetDefaults() {
		Item.width = 26;
		Item.height = 24;
		Item.maxStack = 99;
		Item.rare = ItemRarityID.Cyan;
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.FragmentSolar)
			.AddIngredient(ItemID.FragmentVortex)
			.AddIngredient(ItemID.FragmentNebula)
			.AddIngredient(ItemID.FragmentStardust)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
	}

	public override bool CanRightClick() => Main.mouseItem.accessory && Main.LocalPlayer.HeldItem.prefix != PrefixID.Warding;

	public override void RightClick(Player player) {
		PrefixSystem.ApplyPrefix(ref Main.mouseItem, PrefixID.Warding);

		// Dusty dust
		for (int i = 0; i < 40; i++) {
			Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.HallowedTorch, 0f, 0f, 150, default, 1.3f);
			d.noLight = true;
			d.velocity *= 10f;
			d.noGravity = true;
		}
	}
}

public class NebulaPrefixHammerGlobalItem : GlobalItem
{
	public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.accessory;

	public override bool CanRightClick(Item item) =>
		Main.mouseItem.type == ModContent.ItemType<NebulaPrefixHammer>() && Main.mouseItem.stack > 0
			? item.prefix != PrefixID.Warding
			: base.CanRightClick(item);

	public override void RightClick(Item item, Player player) {
		if (Main.mouseItem.type != ModContent.ItemType<NebulaPrefixHammer>()) {
			return;
		}

		PrefixSystem.ApplyPrefix(ref item, PrefixID.Warding);
		item.stack++;
		Main.mouseItem.stack--;

		// Dusty dust
		for (int i = 0; i < 40; i++) {
			Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.HallowedTorch, 0f, 0f, 150, default, 1.3f);
			d.noLight = true;
			d.velocity *= 10f;
			d.noGravity = true;
		}
	}
}