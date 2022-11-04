using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using YAQOLM.Common.Configs;

namespace YAQOLM.Common.Players {
    public class ConsumableCraftingStationsPlayer : ModPlayer {
        private List<string> consumedCraftingStations = new();
        public override void SaveData(TagCompound tag) {
            if (consumedCraftingStations == null) {
                consumedCraftingStations = new();
            }

            if (consumedCraftingStations.Count != 0) {
                tag["consumedCraftingStations"] = consumedCraftingStations;
            }
        }

        public override void LoadData(TagCompound tag) {
            consumedCraftingStations = new();

            if (tag.ContainsKey("consumedCraftingStations")) {
                consumedCraftingStations = tag.GetList<string>("consumedCraftingStations").ToList();
            }
        }

        public string GetFullNameFromItem(Item item) {
            string name = ItemID.Search.GetName(item.type);
            string mod = "Terraria";
            if (item.ModItem != null) {
                mod = item.ModItem.Mod.Name;
            }
            string fullName = $"{mod}:{name}";

            return fullName;
        }

        public Item GetItemFromFullName(string name) {
            int separater = name.IndexOf(":");
            string item = name.Substring(separater + 1);
            if (ItemID.Search.TryGetId(item, out int type)) {
                return ContentSamples.ItemsByType[type];
            }

            return null;
        }

        public bool HasConsumedItem(Item item) => consumedCraftingStations.Contains(GetFullNameFromItem(item)) && ServerConfig.Instance.InventoryCraftingStations;

        public void ConsumeItem(Item item) => consumedCraftingStations.Add(GetFullNameFromItem(item));

        public List<int> ConsumedItemTiles() {
            List<int> tiles = new();

            foreach (string fullName in consumedCraftingStations) {
                Item item = GetItemFromFullName(fullName);
                if (item != null) {
                    if (!tiles.Contains(item.createTile)) {
                        tiles.Add(item.createTile);
                    }
                }
            }

            return tiles;
        }
    }

    public class ConsumableCraftingStationsGlobalTile : GlobalTile {
        public override int[] AdjTiles(int type) {
            if (ServerConfig.Instance.InventoryCraftingStations) {
                foreach (int entry in Main.LocalPlayer.GetModPlayer<ConsumableCraftingStationsPlayer>().ConsumedItemTiles()) {
                    Main.LocalPlayer.adjTile[entry] = true;
                    Main.LocalPlayer.oldAdjTile[entry] = true;
                }
            }

            return base.AdjTiles(type);
        }
    }
}