using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Systems {
    public class RecipesSystem : ModSystem {
        // Recipe groups 
        public static RecipeGroup evilBarRecipeGroup;
        public static RecipeGroup moonLordWeaponRecipeGroup;
        public static RecipeGroup moonLordItemRecipeGroup;

        public override void Unload() {
            // Set all recipe groups to null
            evilBarRecipeGroup = null;
            moonLordWeaponRecipeGroup = null;
            moonLordItemRecipeGroup = null;
            beetleArmor = null;
        }

        public override void AddRecipeGroups() {
            // Initialise our recipe groups
            evilBarRecipeGroup = new(() => "Any Evil Bar", ItemID.DemoniteBar, ItemID.CrimtaneBar);
            RecipeGroup.RegisterGroup("YAQOLM:EvilBar", evilBarRecipeGroup);
            moonLordWeaponRecipeGroup = new(() => "Any Moon Lord weapon", ItemID.Meowmere, ItemID.Terrarian, ItemID.StarWrath, ItemID.SDMG, ItemID.LastPrism, ItemID.LunarFlareBook, ItemID.RainbowCrystalStaff, ItemID.MoonlordTurretStaff, ItemID.Celeb2);
            RecipeGroup.RegisterGroup("YAQOLM:MoonLordWeapon", moonLordWeaponRecipeGroup);
            moonLordItemRecipeGroup = new(() => "Any Moon Lord item", ItemID.MeowmereMinecart, ItemID.PortalGun, ItemID.GravityGlobe, ItemID.SuspiciousLookingTentacle, ItemID.LongRainbowTrailWings);
            RecipeGroup.RegisterGroup("YAQOLM:MoonLordItem", moonLordItemRecipeGroup);
        }

        public override void AddRecipes() {
            if (ServerConfig.Instance.RodOfDiscord) {
                Recipe recipe = Recipe.Create(ItemID.RodofDiscord);
                recipe.AddIngredient(ItemID.HallowedBar, 15);
                recipe.AddIngredient(ItemID.SoulofLight, 10);
                recipe.AddIngredient(ItemID.SoulofSight, 5);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }

            if (ServerConfig.Instance.SnowGlobe) {
                Recipe recipe = Recipe.Create(ItemID.SnowGlobe);
                recipe.AddIngredient(ItemID.SnowBlock, 20);
                recipe.AddIngredient(ItemID.Glass, 20);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }

            if (ServerConfig.Instance.MoneyTrough) {
                Recipe recipe = Recipe.Create(ItemID.MoneyTrough);
                recipe.AddIngredient(ItemID.PiggyBank);
                recipe.AddRecipeGroup(evilBarRecipeGroup, 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }

            if (ServerConfig.Instance.Bait) {
                Recipe recipe = Recipe.Create(ItemID.JourneymanBait);
                recipe.AddIngredient(ItemID.ApprenticeBait, 2);
                recipe.AddTile(TileID.Solidifier);
                recipe.Register();

                recipe = Recipe.Create(ItemID.MasterBait);
                recipe.AddIngredient(ItemID.JourneymanBait, 2);
                recipe.AddTile(TileID.Solidifier);
                recipe.Register();
            }

            if (ServerConfig.Instance.BottomlessBuckets) {
                Recipe recipe = Recipe.Create(ItemID.BottomlessBucket);
                recipe.AddIngredient(ItemID.WaterBucket, 8);
                recipe.AddTile(TileID.CrystalBall);
                recipe.Register();

                recipe = Recipe.Create(ItemID.BottomlessLavaBucket);
                recipe.AddIngredient(ItemID.LavaBucket, 8);
                recipe.AddTile(TileID.CrystalBall);
                recipe.Register();

                // TODO: Add bottomless honey and shimmer buckets when 1.4.4 is here
            }

            if (ServerConfig.Instance.LuminiteSmeltingRecipes) {
                Recipe recipe = Recipe.Create(ItemID.LunarBar, 8);
                recipe.AddRecipeGroup(moonLordWeaponRecipeGroup);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();

                recipe = Recipe.Create(ItemID.LunarBar, 5);
                recipe.AddRecipeGroup(moonLordItemRecipeGroup);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }

            if (ServerConfig.Instance.BeetleArmorOnlyBeetle) {
                Recipe recipe = Recipe.Create(ItemID.BeetleHelmet);
                recipe.AddIngredient(ItemID.BeetleHusk, 6);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();

                recipe = Recipe.Create(ItemID.BeetleLeggings);
                recipe.AddIngredient(ItemID.BeetleHusk, 9);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();

                recipe = Recipe.Create(ItemID.BeetleScaleMail);
                recipe.AddIngredient(ItemID.BeetleHusk, 12);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();

                recipe = Recipe.Create(ItemID.BeetleShell);
                recipe.AddIngredient(ItemID.BeetleHusk, 12);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
        private int[] beetleArmor = new int[] {
            ItemID.BeetleHelmet,
            ItemID.BeetleLeggings,
            ItemID.BeetleScaleMail,
            ItemID.BeetleShell
        };
        public override void PostAddRecipes() {
            if (ServerConfig.Instance.BeetleArmorOnlyBeetle) {
                for (int i = 0; i < Main.recipe.Length; i++) {
                    if (beetleArmor.Contains(Main.recipe[i].createItem.type) && Main.recipe[i].requiredItem.Count > 1) {
                        Main.recipe[i].DisableRecipe();
                    }
                }
            }
        }
    }
}