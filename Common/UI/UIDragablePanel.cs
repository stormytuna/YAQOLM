﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

/*
 * Copyright (c) 2022 absoluteAquarian
 * https://github.com/absoluteAquarian/SerousCommonLib/blob/v1.0.2/src/UI/UIDragablePanel.cs
 */

namespace YAQOLM.Common.UI {
	/// <summary>
	/// An object representing a panel with "page tabs" that can be moved around
	/// </summary>
	public class UIDragablePanel : UIPanel {
		#pragma warning disable CS1591

		// Stores the offset from the top left of the UIPanel while dragging.
		private Vector2 Offset { get; set; }

		public bool Dragging { get; set; }

		public readonly bool StopItemUse;

		public int UIDelay = -1;

		public event Action OnMenuClose, OnMenuReset;
		public UIPanel header;

		public UIPanel viewArea;

		public event Action OnRecalculate;

		public UIDragablePanel(bool stopItemUse) {
			StopItemUse = stopItemUse;

			SetPadding(0);

			header = new UIPanel();
			header.SetPadding(0);
			header.Width.Set(0, 1f);
			header.Height.Set(30, 0f);
			header.BackgroundColor.A = 255;
			header.OnMouseDown += Header_MouseDown;
			header.OnMouseUp += Header_MouseUp;
			Append(header);

			var closeButton = new UITextPanel<char>('X');
			closeButton.SetPadding(7);
			closeButton.Width.Set(40, 0);
			closeButton.Left.Set(-40, 1);
			closeButton.BackgroundColor.A = 255;
			closeButton.OnClick += (evt, element) => {
				SoundEngine.PlaySound(SoundID.MenuTick);
				OnMenuClose?.Invoke();
			};
			header.Append(closeButton);

			viewArea = new();
			viewArea.Top.Set(38, 0);
			viewArea.Width.Set(0, 1f);
			viewArea.Height.Set(-38, 1f);
			viewArea.BackgroundColor = Color.Transparent;
			viewArea.BorderColor = Color.Transparent;
			Append(viewArea);

			float left = 0;
		}

		public override void Recalculate(){
			base.Recalculate();

			OnRecalculate?.Invoke();
		}

		private void Header_MouseDown(UIMouseEvent evt, UIElement element) {
			base.MouseDown(evt);

			DragStart(evt);
		}

		private void Header_MouseUp(UIMouseEvent evt, UIElement element) {
			base.MouseUp(evt);

			DragEnd(evt);
		}

		private void DragStart(UIMouseEvent evt) {
			Offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			Dragging = true;
		}

		private void DragEnd(UIMouseEvent evt) {
			//A child element forced this to not move
			if(!Dragging)
				return;

			Vector2 end = evt.MousePosition;
			Dragging = false;

			Left.Set(end.X - Offset.X, 0f);
			Top.Set(end.Y - Offset.Y, 0f);

			Recalculate();
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime); // don't remove.

			if (UIDelay > 0)
				UIDelay--;

			// clicks on this UIElement dont cause the player to use current items. 
			if (ContainsPoint(Main.MouseScreen) && StopItemUse)
				Main.LocalPlayer.mouseInterface = true;

			if (Dragging) {
				Left.Set(Main.mouseX - Offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same.
				Top.Set(Main.mouseY - Offset.Y, 0f);
				Recalculate();
			}

			// Here we check if the UIDragablePanel is outside the Parent UIElement rectangle. 
			// By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
			var parentSpace = Parent.GetDimensions().ToRectangle();

			if (!GetDimensions().ToRectangle().Intersects(parentSpace)) {
				// TODO: account for negative Pixels and > 0 Percent
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);

				// Recalculate forces the UI system to do the positioning math again.
				Recalculate();
			}
		}
	}
}
