using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace YAQOLM.Common.Configs {
    public class ServerConfig : ModConfig {
        public static ServerConfig Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

        /* ================ */
        /*   NEW CONTENT    */
        /* ================ */

        [Header("New Content")]

        [Label("$Mods.YAQOLM.Config.WarpedMirror")]
        [Tooltip("Returns you to where you died")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool WarpedMirror { get; set; }

        [Label("$Mods.YAQOLM.Config.MysticMirror")]
        [Tooltip("Acts as a return potion")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool MysticMirror { get; set; }

        [Label("$Mods.YAQOLM.Config.RunicMirror")]
        [Tooltip("Allows you to teleport to party members, NPCs and Pylons")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool RunicMirror { get; set; }

        [Label("$Mods.YAQOLM.Config.SpiralMirror")]
        [Tooltip("A combination of all the above mirrors")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool SpiralMirror { get; set; }

        [Label("$Mods.YAQOLM.Config.GemstoneMagnet")]
        [Tooltip("A Treasure Magnet upgrade that works in your inventory")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool GemstoneMagnet { get; set; }

        [Label("$Mods.YAQOLM.Config.MagnificentMagnet")]
        [Tooltip("A Gemstone Magnet upgrade that allows you to pull all items to you")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool MagnificentMagnet { get; set; }

        [Label("$Mods.YAQOLM.Config.QuantumStrongbox")]
        [Tooltip("Combines all 4 player inventories into one item")]
        [ReloadRequired]
        [DefaultValue(false)]
        public bool QuantumStrongbox { get; set; }

        [Label("$Mods.YAQOLM.Config.GoldenHorseshoeBalloon")]
        [Tooltip("Crafted from and into every horseshoe balloon")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool GoldenHorseshoeBalloon { get; set; }

        [Label("$Mods.YAQOLM.Config.FlowerOfTheJungle")]
        [Tooltip("Summons Plantera")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool FlowerOfTheJungle { get; set; }

        [Label("$Mods.YAQOLM.Config.PrefixHammers")]
        [Tooltip("Right click to apply a given prefix to your held item")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool PrefixHammers { get; set; }


        /* ================ */
        /*     RECIPES      */
        /* ================ */

        [Header("Recipes")]

        [Label("[i:1326] Rod of Discord")]
        [Tooltip("15 Hallowed Bar\n10 Soul of Light\n5 Soul of Sight")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool RodOfDiscord { get; set; }

        [Label("[i:602] Snow Globe")]
        [Tooltip("20 Snow Block\n20 Glass")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool SnowGlobe { get; set; }

        [Label("[i:3213] Money Trough")]
        [Tooltip("1 Piggy Bank\n6 Demonite/Crimtane Bar")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool MoneyTrough { get; set; }

        [Label("[i:2676] Bait")]
        [Tooltip("2 Apprentice Bait -> 1 Journeyman Bait\n2 Journeyman Bait -> 1 Master Bait")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool Bait { get; set; }

        [Label("[i:3031] Bottomless Buckets")]
        [Tooltip("8 of any fluid bucket -> 1 of that fluids bottomless bucket")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool BottomlessBuckets { get; set; }

        [Label("[i:50] Magic Mirror")]
        [Tooltip("10 Iron/Lead Bar\n5 Glass")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool MagicMirror { get; set; }

        [Label("[i:3467] Moon Lord drops to Luminite Bars")]
        [Tooltip("Any weapon -> 8 Luminite Bar\nAnything else -> 5 Luminite Bar")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool LuminiteSmeltingRecipes { get; set; }

        [Label("[i:2201] Beetle Armor doesn't require Turtle Armor")]
        [Tooltip("The number of Beetle Husks required is increased to compensate")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool BeetleArmorOnlyBeetle { get; set; }

        [Label("[i:20] Pre Hardmode bars are cheaper at a Hardmode forge")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool CheaperOre { get; set; }

        /* ================ */
        /*      ITEMS       */
        /* ================ */

        [Header("Items")]

        [Label("[i:729] Wood Greaves have 1 defense")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool WoodGreavesDefense { get; set; }

        [Label("[i:37] Goggles grant Night Owl")]
        [DefaultValue(true)]
        public bool GogglesGiveNightVision { get; set; }

        [Label("[i:1136] Rain Armor set bonus")]
        [Tooltip("8% increased damage and 5% increased critical chance when exposed to rain")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool RainArmorSetBonus { get; set; }

        [Label("[i:780] Cheaper solutions")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool CheaperSolutions { get; set; }

        [Label("[i:530] Cheaper wire")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool CheaperWire { get; set; }

        [Label("[i:3111] Pink Gel is gel ammo")]
        [Tooltip("Deals 20% more damage than regular gel")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool PinkGelIsAmmo { get; set; }

        [Label("[i:854] Discount Card works from inventory and banks")]
        [DefaultValue(true)]
        public bool DiscountCard { get; set; }

        [Label("[i:1291] Life Fruits increase max life by 10")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool BetterLifeFruit { get; set; }

        /* ================ */
        /*       NPCS       */
        /* ================ */

        [Header("NPCs and Drops")]

        [Label("[i:780] Steampunker sells all vanilla solutions")]
        [Tooltip("Can't sell modded solutions here to avoid progression breaking with modded biomes")]
        [DefaultValue(true)]
        public bool SteampunkerSolutions { get; set; }

        [Label("[i:2873] Dye Trader sells Strange Plant dyes")]
        [Tooltip("Depending on moon phase to not spam out the shop")]
        [DefaultValue(true)]
        public bool DyeTraderSellsSusDyes { get; set; }

        [Label("[i:2294] More angler quest loot")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool MoreAnglerLoot { get; set; }

        [Label("[i:2294] Angler quest resets immediately")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool AnglerResetsImmediately { get; set; }

        [Label("[i:1309] King Slime drops Slime Staff")]
        [Tooltip("20% chance in Classic, 25% chance in Expert")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool KingSlimeDropsSlimeStaff { get; set; }

        [Label("[i:3212] Sharks drop Sharktooth Necklace")]
        [Tooltip("4% chance")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool SharksDropSharktoothNecklace { get; set; }

        [Label("[i:3332] One from options drops become some from options")]
        [Tooltip("For every 4 items, one more will drop. 4 items still drops 1, while 5 will drop 2, 9 drops 3 etc etc")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool OneFromOptionsToFewFromOptions { get; set; }

        /* ================ */
        /*       MISC       */
        /* ================ */

        [Header("Misc")]

        [Label("[i:3198] Buff station changes")]
        [Tooltip("Buff stations provide 10% increased damage and a class specific buff")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool BuffStationChanges { get; set; }

        [Label("[i:997] Bulk extratination")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool BulkExtractinate { get; set; }

        [Label("[i:73] Killing bosses makes NPCs happier")]
        [Tooltip("0.92 price multiplier for Evil boss, Wall of Flesh and Plantera\n0.9 price multiplier for Moon Lord")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool BossesIncreaseHappiness { get; set; }

        [Label("[i:166] Bombs explode tiles you could mine")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool BetterBombTiles { get; set; }

        [Label("[i:166] Bombs explode walls even when unexposed")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool BetterBombWalls { get; set; }

        [Label("[i:36] Consume crafting stations for permanent access")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool InventoryCraftingStations { get; set; }

        [Label("[i:29] Super quick respawn")]
        [Tooltip("Respawn time will be 2 seconds unless there's a currently active invasion or boss")]
        [DefaultValue(true)]
        public bool SuperQuickRespawn { get; set; }

        [Label("[i:292] Having 30 potions grants you that buff")]
        [DefaultValue(false)]
        public bool UnlimitedPotions { get; set; }

        [Label("[i:2266] Gain buffs from potions that grant debuffs as well")]
        [DefaultValue(false)]
        public bool UnlimitedPotionsWithDebuffs { get; set; }

        [Label("[i:1969] Save team when leaving and rejoining")]
        [DefaultValue(true)]
        public bool SaveTeam { get; set; }
    }
}