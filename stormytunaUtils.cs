using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ThreatOfPrecipitation {
    public static class stormytunaUtils {

        /// <summary>Gets a list of NPCs within the range of that position</summary>
        /// <param name="position">The position, should be the center of the search and usually the center of another entity</param>
        /// <param name="range">The range measured in units, 1 tile is 16 units</param>
        /// <param name="careAboutLineOfSight">Whether the function should check Collision.CanHit</param>
        /// <param name="careAboutCanBeChased">Whether the function should check npc.chaseable</param>
        /// <param name="excludedNPCs">The whoAmI fields of any NPCs that are excluded from the search</param>
        /// <returns>A list of NPCs within range of the position</returns>
        public static List<NPC> GetNearbyEnemies(Vector2 position, float range, bool careAboutLineOfSight, bool careAboutCanBeChased, List<int> excludedNPCs = null) {
            List<NPC> npcs = new List<NPC>();
            float rangeSquared = range * range;
            if (excludedNPCs == null)
                excludedNPCs = new List<int>();

            for (int i = 0; i < Main.npc.Length; i++) {
                NPC npc = Main.npc[i];

                if (!npc.active || npc.CountsAsACritter || npc.friendly || !npc.immortal || excludedNPCs.Contains(npc.whoAmI)) {
                    continue;
                }

                float distanceSquared = Vector2.DistanceSquared(position, npc.Center);
                bool canSee = careAboutLineOfSight ? Collision.CanHit(position, 1, 1, npc.position, npc.width, npc.height) : true;
                bool canBeChased = careAboutCanBeChased ? npc.chaseable : true;
                if (distanceSquared <= rangeSquared && canSee && canBeChased) {
                    npcs.Add(npc);
                }
            }

            return npcs;
        }

        /// <summary>Gets the closest hostile NPC within the range of that position</summary>
        /// <param name="position">The position, should be the center of the search and usually the center of another entity</param>
        /// <param name="range">The range measured in units, 1 tile is 16 units</param>
        /// <param name="careAboutLineOfSight">Whether the function should check Collision.CanHit</param>
        /// <param name="careAboutCanBeChased">Whether the function should check npc.chaseable</param>
        /// <param name="excludedNPCs">The whoAmI fields of any NPCs that are excluded from the search</param>
        /// <returns>Returns the closest NPC. Returns null if no NPC is found</returns>
        public static NPC GetClosestEnemy(Vector2 position, float range, bool careAboutLineOfSight, bool careAboutCanBeChased, List<int> excludedNPCs = null) {
            NPC closestNPC = null;
            float rangeSquared = range * range;
            if (excludedNPCs == null)
                excludedNPCs = new List<int>();

            for (int i = 0; i < Main.npc.Length; i++) {
                NPC npc = Main.npc[i];

                if (!npc.active || npc.CountsAsACritter || npc.friendly || !npc.immortal || excludedNPCs.Contains(npc.whoAmI)) {
                    continue;
                }

                float distanceSquared = Vector2.DistanceSquared(position, npc.Center);
                bool canSee = careAboutLineOfSight ? Collision.CanHit(position, 1, 1, npc.position, npc.width, npc.height) : true;
                bool canBeChased = careAboutCanBeChased ? npc.chaseable : true;
                if (distanceSquared < rangeSquared && canSee && canBeChased) {
                    closestNPC = npc;
                    rangeSquared = distanceSquared;
                }
            }

            return closestNPC;
        }

        /// <summary>Gets a list of players within the range of that position</summary>
        /// <param name="position">The position, should be the center of the search and usually the center of another entity</param>
        /// <param name="range">The range measured in units, 1 tile is 16 units</param>
        /// <param name="careAboutLineOfSight">Whether the function should check Collision.CanHit</param>
        /// <param name="team">The team the player should match. 0 means team doesn't matter</param>
        /// <param name="excludedPlayers">The whoAmI fields of any players that are excluded from the search</param>
        /// <returns>A list of players within range of the position</returns>
        public static List<Player> GetNearbyPlayers(Vector2 position, float range, bool careAboutLineOfSight, int team = 0, List<int> excludedPlayers = null) {
            List<Player> players = new List<Player>();
            float rangeSquared = range * range;
            if (excludedPlayers == null)
                excludedPlayers = new List<int>();

            for (int i = 0; i < Main.player.Length; i++) {
                Player player = Main.player[i];

                if (!player.active || player.dead || (player.team != team && team != 0) || excludedPlayers.Contains(player.whoAmI)) {
                    continue;
                }

                float distanceSquared = Vector2.DistanceSquared(position, player.Center);
                bool canSee = careAboutLineOfSight ? Collision.CanHit(position, 1, 1, player.position, player.width, player.height) : true;
                if (distanceSquared <= rangeSquared && canSee) {
                    players.Add(player);
                }
            }

            return players;
        }

        /// <summary>Homing via rotating a projectiles velocity towards its target.\nThis overload searches for the closest enemy</summary>
        /// <param name="currentVelocity">The projectiles current velocity</param>
        /// <param name="startPosition">The position, should be the center of the projectile</param>
        /// <param name="range">The range measured in units, 1 tile is 16 units</param>
        /// <param name="careAboutLineOfSight">Whether the function should check Collision.CanHit</param>
        /// <param name="rotationMax">The max amount of degrees the velocity can be rotated, make sure to use degrees as this function coverts it to radians</param>
        /// <param name="excludeNPCs">The whoAmI fields of any NPCs that are excluded from the search</param>
        /// <returns>Returns velocity rotated towards the found NPC. Returns the original velocity if no NPC is found</returns>
        public static Vector2 RotateVelocityHoming(Vector2 currentVelocity, Vector2 startPosition, float range, bool careAboutLineOfSight, float rotationMax, List<int> excludeNPCs = null) {
            if (excludeNPCs == null)
                excludeNPCs = new List<int>();

            NPC closestNPC = GetClosestEnemy(startPosition, range, careAboutLineOfSight, true, excludeNPCs);

            if (closestNPC == null)
                return currentVelocity;

            return RotateVelocityHoming(currentVelocity, startPosition, closestNPC.Center, rotationMax);
        }

        /// <summary>Homing via rotating a projectiles velocity towards its target.</summary>
        /// <param name="currentVelocity">The projectiles current velocity</param>
        /// <param name="startPosition">The start position, should be the center of the projectile</param>
        /// <param name="targetPosition">The target position, should be the center of the target</param>
        /// <param name="rotationMax">The max amount of degrees the velocity can be rotated, make sure to use degrees as this function coverts it to radians</param>
        /// <returns>Returns velocity rotated towards the given position</returns>
        public static Vector2 RotateVelocityHoming(Vector2 currentVelocity, Vector2 startPosition, Vector2 targetPosition, float rotationMax) {
            rotationMax = MathHelper.ToRadians(rotationMax);
            float rotTarget = Utils.ToRotation(targetPosition - startPosition);
            float rotCurrent = Utils.ToRotation(currentVelocity);
            return Utils.RotatedBy(currentVelocity, MathHelper.WrapAngle(MathHelper.WrapAngle(Utils.AngleTowards(rotCurrent, rotTarget, rotationMax)) - Utils.ToRotation(currentVelocity)));
        }

        /// <summary>Checks within a rotation is within the range of another rotation</summary>
        /// <param name="rotation">The start rotation, measured in radians</param>
        /// <param name="rotationTestAgainst">The rotation to test against, measured in radians</param>
        /// <param name="range">The range around rotation to test if rotationTestAgainst is within, checks either side with the raw range (ie it isn't halved), measured in radians</param>
        /// <returns>Returns true if rotationTestAgainst is within range of rotation. Returns false otherwise</returns>
        public static bool RotationIsWithinRange(float rotation, float rotationTestAgainst, float range) {
            float absDif = MathF.Abs(rotation - rotationTestAgainst);
            return absDif < range;
        }

        /// <summary>Spherically interpolates between the start and end</summary>
        /// <param name="start">The starting value, will return this when amount == 0</param>
        /// <param name="end">The ending value, will return this when amount == 1</param>
        /// <param name="amount">The amount to slerp by</param>
        /// <returns>Returns the spherical interpolation between start and end</returns>
        public static float Slerp(float start, float end, float amount) {
            if (amount == 0f)
                return start;
            if (amount == 1f)
                return end;

            // Calculated using a rotating vector2
            Vector2 vector = new Vector2(1f, 0f);
            vector = vector.RotatedBy(amount * MathHelper.PiOver2);
            float slerpedAmount = vector.Y;

            return MathHelper.Lerp(start, end, slerpedAmount);
        }

        /// <summary>Ease in interpolation between the start and end</summary>
        /// <param name="start">The starting value, will return this when amount == 0</param>
        /// <param name="end">The ending value, will return this when amount == 1</param>
        /// <param name="amount">The amount to lerp by</param>
        /// <param name="exponent">The exponent of the easing curve to use, larger values cause more easing</param>
        /// <returns>Returns the ease in interpolation between start and end</returns>
        public static float EaseIn(float start, float end, float amount, int exponent) {
            if (amount == 0f)
                return start;
            if (amount == 1f)
                return end;

            float amountExp = MathF.Pow(amount, exponent);
            float flipExp = 1 - amountExp;

            return MathHelper.Lerp(start, end, flipExp);
        }

        /// <summary>Ease out interpolation between the start and end</summary>
        /// <param name="start">The starting value, will return this when amount == 0</param>
        /// <param name="end">The ending value, will return this when amount == 1</param>
        /// <param name="amount">The amount to lerp by</param>
        /// <param name="exponent">The exponent of the easing curve to use, larger values cause more easing</param>
        /// <returns>Returns the ease out interpolation between start and end</returns>
        public static float EaseOut(float start, float end, float amount, int exponent) {
            if (amount == 0f)
                return start;
            if (amount == 1f)
                return end;

            float flip = 1 - amount;
            float flipExp = MathF.Pow(flip, exponent);
            float reFlip = 1 - flipExp;

            return MathHelper.Lerp(start, end, reFlip);
        }

        /// <summary>Sets the magnitude of the vector to the given magnitude</summary>
        /// <param name="vector">The vector to be changed</param>
        /// <param name="magnitude">The new magnitude of the vector</param>
        public static void SetMagnitude(this Vector2 vector, float magnitude) {
            vector.Normalize();
            vector *= magnitude;
        }

        /// <summary>Converts an integer coin value to a string</summary>
        /// <param name="coinValue">The coin value</param>
        /// <param name="useIcons">Whether the string should use coin icons or coin names</param>
        /// <param name="useColors">Whether the string should colour the number of coins</param>
        /// <returns>Returns a string representing the coin value</returns>
        public static string CoinValueToString(int coinValue, bool useIcons, bool useColors) {
            // Code copied from vanilla - PopupText.ValueToName()
            int platinumCoins = 0;
            int goldCoins = 0;
            int silverCoins = 0;
            int copperCoins = 0;
            while (coinValue > 0) {
                if (coinValue >= 1000000) {
                    coinValue -= 1000000;
                    platinumCoins++;
                }
                else if (coinValue >= 10000) {
                    coinValue -= 10000;
                    goldCoins++;
                }
                else if (coinValue >= 100) {
                    coinValue -= 100;
                    silverCoins++;
                }
                else if (coinValue >= 1) {
                    coinValue--;
                    copperCoins++;
                }
            }

            string text = "";
            if (!useIcons) {
                if (!useColors) {
                    if (platinumCoins > 0)
                        text = text + platinumCoins + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));

                    if (goldCoins > 0)
                        text = text + goldCoins + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));

                    if (silverCoins > 0)
                        text = text + silverCoins + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));

                    if (copperCoins > 0)
                        text = text + copperCoins + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
                }
                else {
                    if (platinumCoins > 0)
                        text = text + $"[c/DCDCC6:{platinumCoins}]" + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));

                    if (goldCoins > 0)
                        text = text + $"[c/E0C95C:{goldCoins}]" + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));

                    if (silverCoins > 0)
                        text = text + $"[c/B5C0C1:{silverCoins}]" + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));

                    if (copperCoins > 0)
                        text = text + $"[c/F68A60:{copperCoins}]" + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
                }
            }
            else {
                if (!useColors) {
                    if (platinumCoins > 0)
                        text = text + platinumCoins + "[i:74] ";

                    if (goldCoins > 0)
                        text = text + goldCoins + "[i:73] ";

                    if (silverCoins > 0)
                        text = text + silverCoins + "[i:72] ";

                    if (copperCoins > 0)
                        text = text + copperCoins + "[i:71] ";
                }
                else {
                    if (platinumCoins > 0)
                        text = text + $"[c/DCDCC6:{platinumCoins}]" + "[i:74] ";

                    if (goldCoins > 0)
                        text = text + $"[c/E0C95C:{goldCoins}]" + "[i:73] ";

                    if (silverCoins > 0)
                        text = text + $"[c/B5C0C1:{silverCoins}]" + "[i:72] ";

                    if (copperCoins > 0)
                        text = text + $"[c/F68A60:{copperCoins}]" + "[i:71] ";
                }
            }

            if (text.Length > 1)
                text = text.Substring(0, text.Length - 1);

            return text;
        }

        /// <summary>Adds an item to a shop</summary>
        /// <param name="shop">The <c>shop</c> parameter of <c>SetupShop</c></param>
        /// <param name="nextSlot">The <c>nextSlot</c> parameter of <c>SetupShop</c>, no need to increment this yourself as this function does that if needed</param>
        /// <param name="itemType">The type of the item being inserted</param>
        /// <param name="itemAfterType">The type of an item already in the shop that an item is inserted after</param>
        /// <param name="defaultIndex">The default index if an item with type <c>itemAfterType</c> isn't found</param>
        public static void AddToShop(ref Chest shop, ref int nextSlot, int itemType, int itemAfterType, int defaultIndex) {
            Predicate<Item> pred = delegate (Item i) { return i.type == itemAfterType; };
            AddToShop(ref shop, ref nextSlot, itemType, pred, defaultIndex);
        }

        /// <summary>Adds an item to a shop</summary>
        /// <param name="shop">The <c>shop</c> parameter of <c>SetupShop</c></param>
        /// <param name="nextSlot">The <c>nextSlot</c> parameter of <c>SetupShop</c>, no need to increment this yourself as this function does that if needed</param>
        /// <param name="itemType">The type of the item being inserted</param>
        /// <param name="after">The predicate an Item must match for the added item to be inserted after it</param>
        /// <param name="defaultIndex">The default index if an item with type <c>itemAfterType</c> isn't found</param>
        public static void AddToShop(ref Chest shop, ref int nextSlot, int itemType, Predicate<Item> after, int defaultIndex) {
            // Get our inventory as a list
            List<Item> inventory = shop.item.ToList();

            // Get our correct index
            Item itemAfter = inventory.FirstOrDefault(i => after.Invoke(i));
            int index = defaultIndex;
            if (itemAfter != null)
                index = inventory.IndexOf(itemAfter) + 1;

            // Check itemType isn't already in our shop
            Item item = inventory.FirstOrDefault(i => i.type == itemType);
            if (item != null) {
                // Move it if it is
                inventory.Remove(item);
                inventory.Insert(index, item);

                // Reassign our item array as ToList doesnt provide a reference
                shop.item = inventory.ToArray();

                return;
            }

            // Add our item since it isn't here
            item = ContentSamples.ItemsByType[itemType];
            Main.LocalPlayer.GetItemExpectedPrice(item, out int _, out int price);
            price = (int)((float)price * Main.LocalPlayer.currentShoppingSettings.PriceAdjustment);
            item.shopCustomPrice = price;
            item.value = price;
            inventory.Insert(index, item);
            nextSlot++;

            shop.item = inventory.ToArray();
        }
    }
}