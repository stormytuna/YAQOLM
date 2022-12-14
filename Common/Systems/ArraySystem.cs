using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YAQOLM.Content.Items.PrefixHammers;

namespace YAQOLM.Common.Systems {
    public class ArraySystem : ModSystem {
        private static int[] _solutionIds = new int[] {
            ItemID.GreenSolution,
            ItemID.PurpleSolution,
            ItemID.RedSolution,
            ItemID.BlueSolution,
            ItemID.DarkBlueSolution
        };

        private static int[] _barTypes = new int[] {
            ItemID.CopperBar,
            ItemID.TinBar,
            ItemID.IronBar,
            ItemID.LeadBar,
            ItemID.SilverBar,
            ItemID.TungstenBar,
            ItemID.GoldBar,
            ItemID.PlatinumBar,
            ItemID.MeteoriteBar,
            ItemID.DemoniteBar,
            ItemID.CrimtaneBar,
            ItemID.HellstoneBar
        };

        private static int[] _oreTypes = new int[] {
            ItemID.CopperOre,
            ItemID.TinOre,
            ItemID.IronOre,
            ItemID.LeadOre,
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ItemID.Hellstone
        };

        private static int[] _beetleArmor = new int[] {
            ItemID.BeetleHelmet,
            ItemID.BeetleLeggings,
            ItemID.BeetleScaleMail,
            ItemID.BeetleShell
        };

        private static int[] _itemsWithLegendary2 = new int[] {
            ItemID.Terrarian,
            ItemID.CopperShortsword,
            ItemID.Gladius,
            ItemID.GoldShortsword,
            ItemID.IronShortsword,
            ItemID.LeadShortsword,
            ItemID.PlatinumShortsword,
            ItemID.Ruler,
            ItemID.SilverShortsword,
            ItemID.TinShortsword,
            ItemID.TragicUmbrella,
            ItemID.TungstenShortsword,
            ItemID.Umbrella
        };

        private static int[] _indestructibleTiles = new int[] {
            TileID.DemonAltar,
            TileID.Dressers,
            TileID.LihzahrdAltar,
            TileID.Mannequin,
            TileID.Womannequin,
            TileID.HatRack
        };

        private static int[] _itemsThatPlaceTilesWithRecipes = new int[] {

        };

        private static int[] _prefixHammers = new int[] {
            ModContent.ItemType<NebulaPrefixHammer>(),
            ModContent.ItemType<SolarPrefixHammer>(),
            ModContent.ItemType<VortexPrefixHammer>(),
            ModContent.ItemType<StardustPrefixHammer>()
        };

        public static int[] SolutionIds { get => _solutionIds; }

        public static int[] BarTypes { get => _barTypes; }

        public static int[] OreTypes { get => _oreTypes; }

        public static int[] BeetleArmor { get => _beetleArmor; }

        public static int[] ItemsWithLegendary2 { get => _itemsWithLegendary2; }

        public static int[] IndestructibleTiles { get => _indestructibleTiles; }

        public static int[] ItemsThatPlaceTilesWithRecipes { get => _itemsThatPlaceTilesWithRecipes; }

        public static int[] PrefixHammers { get => _prefixHammers; }
        public override void Unload() {
            _solutionIds = null;
            _barTypes = null;
            _oreTypes = null;
            _beetleArmor = null;
            _itemsWithLegendary2 = null;
        }

        public override void PostAddRecipes() {
            List<int> tilesWithRecipes = new List<int>();

            for (int i = 0; i < Main.recipe.Length; i++) {
                for (int j = 0; j < Main.recipe[i].requiredTile.Count; j++) {
                    if (!tilesWithRecipes.Contains(Main.recipe[i].requiredTile[j])) {
                        tilesWithRecipes.Add(Main.recipe[i].requiredTile[j]);
                    }
                }
            }

            List<int> itemsPlaceTilesWithRecipes = new List<int>();

            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++) {
                if (tilesWithRecipes.Contains(ContentSamples.ItemsByType[i].createTile)) {
                    itemsPlaceTilesWithRecipes.Add(i);
                }
            }

            _itemsThatPlaceTilesWithRecipes = itemsPlaceTilesWithRecipes.ToArray();
        }
    }
}