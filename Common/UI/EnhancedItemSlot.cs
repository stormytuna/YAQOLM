using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

/*
 * Copyright (c) 2022 absoluteAquarian
 * https://github.com/absoluteAquarian/SerousCommonLib/blob/v1.0.2/src/UI/EnhancedItemSlot.cs
 */

namespace YAQOLM.Common.UI;

/// <summary>
///     This delegate is used by <see cref="EnhancedItemSlot" /> to indicate whether the item currently on the mouse is valid
/// </summary>
/// <param name="mouseItem">Shortcut of <see cref="Main.mouseItem" /></param>
public delegate bool IsItemValidForSlotDelegate(Item mouseItem);

/// <summary>
///     This delegate is used by <see cref="EnhancedItemSlot" /> when its bound item instance has changed
/// </summary>
/// <param name="newItem">The new state of the bound item</param>
public delegate void OnItemSlotItemChangedDelegate(Item newItem);

/// <summary>
///     An enhanced version of <see cref="ItemSlot" /> containing various functions used when inserting items, removing items, etc.
/// </summary>
public class EnhancedItemSlot : UIElement
{
    /// <summary>
    ///     The <see cref="ItemSlot.Context" /> to draw this item slot with
    /// </summary>
    public int Context { get; set; }

    /// <summary>
    ///     The scale to draw this item slot at
    /// </summary>
    public float Scale { get; }

    /// <summary>
    ///     The public property used to retrieve the item in this item slot.
    ///     By default, this property simply retrieves the <see cref="Item" /> instance bound to this item slot
    /// </summary>
    public virtual Item StoredItem => storedItem;

    /// <summary>
    ///     The <see cref="Item" /> instance bound to this item slot
    /// </summary>
    protected Item storedItem;

	private Item storedItemBeforeHandle;

    /// <summary>
    ///     Whether this item slot's bound item's type, stack and/or prefix have been changed
    /// </summary>
    public bool ItemChanged {
		get {
			Item item = storedItem;
			return item != null && storedItemBeforeHandle != null && item.IsNotSameTypePrefixAndStack(storedItemBeforeHandle);
		}
	}

    /// <summary>
    ///     Whether this item slot's bound item's type has changed
    /// </summary>
    public bool ItemTypeChanged => (storedItem?.type ?? -1) != (storedItemBeforeHandle?.type ?? -2);

    /// <summary>
    ///     A function indicating whether the item on the player's mouse can be inserted into this item slot or can be swapped with this item slot's bound item
    /// </summary>
    public IsItemValidForSlotDelegate ValidItemFunc;

    /// <summary>
    ///     An event that is invoked whenever the bound item in this item slot has changed
    /// </summary>
    public event OnItemSlotItemChangedDelegate OnItemChanged;

    /// <summary>
    ///     Whether this item slot should ignore left and right click actions.  Defaults to <see langword="false" />
    /// </summary>
    public bool IgnoreClicks { get; set; }

    /// <summary>
    ///     Whether this item slot should not run its item handling logic the next time it is attempted to be executed.  Defaults to <see langword="false" />
    /// </summary>
    public bool IgnoreNextHandleAction { get; set; }

    /// <summary>
    ///     An integer that can be used for easily tying this item slot to an inventory of items.  This type does not use it directly
    /// </summary>
    public readonly int slot;

	private readonly Item[] dummy = new Item[11];

	public EnhancedItemSlot(int slot, int context = ItemSlot.Context.InventoryItem, float scale = 1f) {
		this.slot = slot;
		Context = context;
		Scale = scale;

		storedItem = new Item();
		storedItem.SetDefaults();

		Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
		Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
	}

	protected override void DrawSelf(SpriteBatch spriteBatch) {
		float oldScale = Main.inventoryScale;
		Main.inventoryScale = Scale;
		Rectangle rectangle = GetDimensions().ToRectangle();

		//Lazy hardcoding lol
		if (!IgnoreNextHandleAction && ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface) {
			Main.LocalPlayer.mouseInterface = true;

			if (Parent is UIDragablePanel panel2) {
				panel2.Dragging = false;
			}

			if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem)) {
				bool oldLeft = Main.mouseLeft;
				bool oldLeftRelease = Main.mouseLeftRelease;
				bool oldRight = Main.mouseRight;
				bool oldRightRelease = Main.mouseRightRelease;

				if (IgnoreClicks) {
					Main.mouseLeft = Main.mouseLeftRelease = Main.mouseRight = Main.mouseRightRelease = false;
				}

				// Handle handles all the click and hover actions based on the context.
				Item item = StoredItem;
				storedItemBeforeHandle = item.Clone();
				dummy[10] = item;
				ItemSlot.Handle(dummy, Context, 10);
				storedItem = dummy[10];

				if (ItemChanged || ItemTypeChanged) {
					OnItemChanged?.Invoke(storedItemBeforeHandle);
				}

				Main.mouseLeft = oldLeft;
				Main.mouseLeftRelease = oldLeftRelease;
				Main.mouseRight = oldRight;
				Main.mouseRightRelease = oldRightRelease;
			}
		}

		IgnoreNextHandleAction = false;

		// Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
		dummy[10] = StoredItem;
		ItemSlot.Draw(spriteBatch, dummy, Context, 10, rectangle.TopLeft());

		Main.inventoryScale = oldScale;
	}

	public void SetItem(Item item, bool clone = false) => storedItem = clone ? item.Clone() : item;

	public void SetItem(int itemType, int stack = 1) {
		storedItem.SetDefaults(itemType);
		storedItem.stack = stack;
	}
}