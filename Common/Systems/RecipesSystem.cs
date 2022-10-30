using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Systems {
    public class RecipesSystem : ModSystem {
        // Recipe groups 

        public override void Unload() {
            // Set all recipe groups to null
        }

        public override void AddRecipeGroups() {
            // Initialise our recipe groups
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
        }
    }
}