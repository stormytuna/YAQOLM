using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using YAQOLM.Common.Configs;
using YAQOLM.Common.Players;

namespace YAQOLM.Content.Tiles {
    public class TotallyAPlaceableTile : ModTile {
        // Wow I actually just dont need to add anything here that's funy
    }

    public class TotallyAPlaceableTileGlobalTile : GlobalTile {
        public override int[] AdjTiles(int type) {
            if (type == ModContent.TileType<TotallyAPlaceableTile>()) {
                List<int> tiles = new();
                if (ServerConfig.Instance.InventoryCraftingStations) {
                    foreach (int entry in Main.LocalPlayer.GetModPlayer<ConsumableCraftingStationsPlayer>().ConsumedItemTiles()) {
                        tiles.Add(entry);
                    }
                }
                return tiles.ToArray();
            }

            return base.AdjTiles(type);
        }
    }
}