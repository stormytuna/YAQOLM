using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using YAQOLM.Common.Configs;
using System.Collections.Generic;
using System.Linq;

namespace YAQOLM.Common.GlobalNPCs {
    public class ShopGlobalNPC : GlobalNPC {
        public override bool InstancePerEntity => true;

        private readonly int[] solutionIds = new int[] {
            ItemID.GreenSolution,
            ItemID.PurpleSolution,
            ItemID.RedSolution,
            ItemID.BlueSolution,
            ItemID.DarkBlueSolution
        };

        public override void SetupShop(int type, Chest shop, ref int nextSlot) {
            if (type == NPCID.Steampunker && MiscConfig.Instance.SteampunkerSolutions) {
                List<Item> inventory = shop.item.ToList(); // Easier to insert into lists

                Item firstSolution = inventory.FirstOrDefault(i => i.ammo == AmmoID.Solution);
                int index = 1; // Default index is immediately after clentaminator in case we dont find one
                if (firstSolution != null) {
                    index = inventory.IndexOf(firstSolution) + 1;
                }

                for (int i = 0; i < solutionIds.Length; i++) {
                    int solutionId = solutionIds[i];
                    Item solution = inventory.FirstOrDefault(i => i.type == solutionId);
                    // Edge case - solution we found is our first solution, just continue in this case
                    if (solution == firstSolution) {
                        continue;
                    }
                    // If steampunker isnt already selling this solution, insert it
                    if (solution == null) {
                        solution = new();
                        solution.SetDefaults(solutionId);
                        inventory.Insert(index, solution);
                        index++;
                        nextSlot++;
                    } 
                    // If steampunker is selling this solution, move it
                    else {
                        inventory.Remove(solution);
                        inventory.Insert(index, solution);
                        index++;
                        // Don't increment nextSlot here since we haven't actually added anything to the shop
                    }
                }

                shop.item = inventory.ToArray();
            }
        }
    }
}