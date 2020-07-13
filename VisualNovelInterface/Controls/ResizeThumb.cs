using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.Controls
{
	public class ResizeThumb : Thumb
	{
		private const int minWidth = 30;

		public ResizeThumb() {
			DragDelta += new DragDeltaEventHandler(ResizeThumb_DragDelta);
		}

		public void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e) {
			switch (Tag as string) {
				case "TopLeft":
					DragDelta_TopLeft(sender, e);
					break;
				case "TopRight":
					DragDelta_TopRight(sender, e);
					break;
				case "BottomLeft":
					DragDelta_BottomLeft(sender, e);
					break;
				case "BottomRight":
					DragDelta_BottomRight(sender, e);
					break;
			}
			e.Handled = true;
		}

		#region Funktionen für unterschidliche resize fälle

		/// <summary>
		/// Behandelt rezizing für bottom left thumb
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="args">EventArgs</param>
		public void DragDelta_BottomLeft(object sender, DragDeltaEventArgs args) {
			SpriteViewModel SpriteReference = this.DataContext as SpriteViewModel;
			Thumb hitThumb = sender as Thumb;

			if (SpriteReference == null || hitThumb == null) {
				return;
			}

			//TODO: Optional Grid mode, maybe when ctrl is pressed
			//double vchange = ShapesHelper.SnapToGrid(args.VerticalChange, ShapesHelper.GridDistance);
			//double hchange = ShapesHelper.SnapToGrid(args.HorizontalChange, ShapesHelper.GridDistance);
			double vchange = args.VerticalChange;
			double hchange = args.HorizontalChange;
			// Change the size by the amount the user drags the mouse, as long as it's larger
			// than the width or height of an adorner, respectively.

			double width_old = SpriteReference.Width;
			double width_new = Math.Max(SpriteReference.Width - hchange, hitThumb.DesiredSize.Width);
			double left_old = SpriteReference.PosX;

			if (width_new < minWidth)
				width_new = minWidth;

			double newPosY =  left_old - (width_new - width_old);
			double newHeight = Math.Max(vchange + SpriteReference.Height, hitThumb.DesiredSize.Height);

			if (newHeight > minWidth) {
				SpriteReference.Height = newHeight;
			}

			if (newPosY > 0) {
				SpriteReference.Width = width_new;
				SpriteReference.PosX = newPosY;
			}
		}

		/// <summary>
		/// Behandelt rezizing für bottom right thumb
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="args">EventArgs</param>
		public void DragDelta_BottomRight(object sender, DragDeltaEventArgs args) {
			SpriteViewModel NodeReference = this.DataContext as SpriteViewModel;
			Thumb hitThumb = sender as Thumb;

			if (NodeReference == null || hitThumb == null) {
				return;
			}

			//TODO: Optional Grid mode, maybe when ctrl is pressed
			//double vchange = ShapesHelper.SnapToGrid(args.VerticalChange, ShapesHelper.GridDistance);
			//double hchange = ShapesHelper.SnapToGrid(args.HorizontalChange, ShapesHelper.GridDistance);
			double vchange = args.VerticalChange;
			double hchange = args.HorizontalChange;

			// Ensure that the Width and Height are properly initialized after the resize.

			// Change the size by the amount the user drags the mouse, as long as it's larger
			// than the width or height of an adorner, respectively.
			NodeReference.Width = Math.Max(NodeReference.Width + hchange, hitThumb.DesiredSize.Width);
			NodeReference.Height = Math.Max(vchange + NodeReference.Height, hitThumb.DesiredSize.Height);

			if (NodeReference.Width < minWidth)
				NodeReference.Width = minWidth;

			if (NodeReference.Height < minWidth)
				NodeReference.Height = minWidth;

		}

		/// <summary>
		/// Behandelt rezizing für top left thumb
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="args">EventArgs</param>
		public void DragDelta_TopLeft(object sender, DragDeltaEventArgs args) {
			SpriteViewModel NodeReference = this.DataContext as SpriteViewModel;
			Thumb hitThumb = sender as Thumb;

			if (NodeReference == null || hitThumb == null) {
				return;
			}

			// Ensure that the Width and Height are properly initialized after the resize.
			////EnforceSize(ditem);

			//TODO: Optional Grid mode, maybe when ctrl is pressed
			//double vchange = ShapesHelper.SnapToGrid(args.VerticalChange, ShapesHelper.GridDistance);
			//double hchange = ShapesHelper.SnapToGrid(args.HorizontalChange, ShapesHelper.GridDistance);
			double vchange = args.VerticalChange;
			double hchange = args.HorizontalChange;

			// Change the size by the amount the user drags the mouse, as long as it's larger
			// than the width or height of an adorner, respectively.

			double width_old = NodeReference.Width;
			double width_new = Math.Max(NodeReference.Width - hchange, hitThumb.DesiredSize.Width);
			double left_old = NodeReference.PosX;

			if (width_new < minWidth)
				width_new = minWidth;

			double newPosX = left_old - (width_new - width_old);

			if (newPosX > 0) {

				NodeReference.Width = width_new;
				NodeReference.PosX = newPosX;
			}

			double height_old = NodeReference.Height;
			double height_new = Math.Max(NodeReference.Height - vchange, hitThumb.DesiredSize.Height);
			double top_old = NodeReference.PosY;

			if (height_new < minWidth)
				height_new = minWidth;

			double newPosY = top_old - (height_new - height_old);

			if (newPosY > 0) {

				NodeReference.Height = height_new;
				NodeReference.PosY = newPosY;
			}
		}

		/// <summary>
		/// Behandelt rezizing für top right thumb
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="args">EventArgs</param>
		public void DragDelta_TopRight(object sender, DragDeltaEventArgs args) {
			SpriteViewModel NodeReference = this.DataContext as SpriteViewModel;
			Thumb hitThumb = sender as Thumb;

			if (NodeReference == null || hitThumb == null) {
				return;
			}

			//TODO: Optional Grid mode, maybe when ctrl is pressed
			//double vchange = ShapesHelper.SnapToGrid(args.VerticalChange, ShapesHelper.GridDistance);
			//double hchange = ShapesHelper.SnapToGrid(args.HorizontalChange, ShapesHelper.GridDistance);
			double vchange = args.VerticalChange;
			double hchange = args.HorizontalChange;

			// Change the size by the amount the user drags the mouse, as long as it's larger
			// than the width or height of an adorner, respectively.

			double height_old = NodeReference.Height;
			double height_new = Math.Max(NodeReference.Height - vchange, hitThumb.DesiredSize.Height);
			double top_old = NodeReference.PosY;
			double newWidth = Math.Max(NodeReference.Width + hchange, hitThumb.DesiredSize.Width);

			if (height_new < minWidth)
				height_new = minWidth;

			if (newWidth > minWidth) {

				NodeReference.Width = newWidth;
			}

			double newPosY = top_old - (height_new - height_old);

			if (newPosY > 0) {
				NodeReference.Height = height_new;
				NodeReference.PosY = newPosY;
			}
		}
		#endregion Funktionen für unterschidliche resize fälle
	}
}
