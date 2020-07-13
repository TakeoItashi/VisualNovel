using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VisualNovelInterface.Resources.HelperClasses
{
	/// <summary>
	/// Helps with calculating different values for several shapes
	/// </summary>
	public class ShapesHelper
	{
		/// <summary>
		/// Konstante für inner border
		/// </summary>
		public const int InnerBorder = 10;

		/// <summary>
		/// Konstante Hilfsgridabstand X
		/// </summary>
		public const int GridDistanceX = 10;

		/// <summary>
		/// Konstante Hilfsgridabstand Y
		/// </summary>
		public const int GridDistanceY = 10;

		/// <summary>
		/// Konstante Hilfsgridabstand X=Y
		/// </summary>
		public const int GridDistance = 10;

		/// <summary>
		/// Calculates the closest grid position for the given coordinates to snap to.
		/// </summary>
		/// <param name="_coords">The real coordinates of the object.</param>
		/// <param name="_gridSize">The size of the grid used.</param>
		/// <returns>The new position of the given coordinates, adjusted to the grid.</returns>
		public static Point SnapToGrid(Point _coords, double _gridSize) {
			// If it's less than half the grid size, snap left/up
			// (by subtracting the remainder),
			// otherwise move it the remaining distance of the grid size right/down
			// (by adding the remaining distance to the next grid point).
			if (_coords.X % _gridSize < _gridSize / 2) {
				_coords.X = _coords.X - (_coords.X % _gridSize);
			} else {
				_coords.X = _coords.X + (_gridSize - (_coords.X % _gridSize));
			}

			if (_coords.Y % _gridSize < _gridSize / 2) {
				_coords.Y = _coords.Y - (_coords.Y % _gridSize);
			} else {
				_coords.Y = _coords.Y + (_gridSize - (_coords.Y % _gridSize));
			}

			return _coords;
		}

		/// <summary>
		/// Calculates the closest grid position on a single axis for the given coordinates to snap to.
		/// </summary>
		/// <param name="coord">The real coordinates of the object on the axis.</param>
		/// <param name="gridSize">The size of the grid used.</param>
		/// <returns>The new coordinate on the axis, adjusted to the grid.</returns>
		public static double SnapToGrid(double coord, double gridSize) {
			// If it's less than half the grid size, snap left/up
			// (by subtracting the remainder),
			// otherwise move it the remaining distance of the grid size right/down
			// (by adding the remaining distance to the next grid point).
			if (coord % gridSize < gridSize / 2) {
				coord = coord - (coord % gridSize);
			} else {
				coord = coord + (gridSize - (coord % gridSize));
			}

			return coord;
		}
	}
}
