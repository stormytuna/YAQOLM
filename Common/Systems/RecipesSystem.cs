using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Systems {
    public class RecipesSystem : ModSystem {
        // Recipe groups 
        public static RecipeGroup evilBarRecipeGroup;

        public override void Unload() {
            // Set all recipe groups to null
            evilBarRecipeGroup = null;
        }

        public override void AddRecipeGroups() {
            // Initialise our recipe groups
            evilBarRecipeGroup = new(() => "Any Evil Bar", ItemID.DemoniteBar, ItemID.CrimtaneBar);
            RecipeGroup.RegisterGroup("YAQOLM:EvilBar", evilBarRecipeGroup);
        }

        public override void AddRecipes() {
            if (ServerConfig.Instance.Recipe_RodOfDiscord) {
                Recipe recipe = Recipe.Create(ItemID.RodofDiscord);
                recipe.AddIngredient(ItemID.HallowedBar, 15);
                recipe.AddIngredient(ItemID.SoulofLight, 10);
                recipe.AddIngredient(ItemID.SoulofSight, 5);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }

            if (ServerConfig.Instance.Recipe_SnowGlobe) {
                Recipe recipe = Recipe.Create(ItemID.SnowGlobe);
                recipe.AddIngredient(ItemID.SnowBlock, 20);
                recipe.AddIngredient(ItemID.Glass, 20);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }

            if (ServerConfig.Instance.Recipe_MoneyTrough) {
                Recipe recipe = Recipe.Create(ItemID.MoneyTrough);
                recipe.AddIngredient(ItemID.PiggyBank);
                recipe.AddRecipeGroup(evilBarRecipeGroup, 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}