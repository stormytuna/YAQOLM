using Terraria.ID;
using Terraria.ModLoader;

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

        private static int[] itemsWithLegendary2 = new int[] {
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

        public static int[] SolutionIds { get => _solutionIds; }

        public static int[] BarTypes { get => _barTypes; }

        public static int[] OreTypes { get => _oreTypes; }

        public static int[] BeetleArmor { get => _beetleArmor; }

        public static int[] ItemsWithLegendary2 { get => itemsWithLegendary2; }

        public override void Unload() {
            _solutionIds = null;
            _barTypes = null;
            _oreTypes = null;
            _beetleArmor = null;
            itemsWithLegendary2 = null;
        }
    }
}