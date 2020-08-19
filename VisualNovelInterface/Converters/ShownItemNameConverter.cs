using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VisualNovelInterface.Models;

namespace VisualNovelInterface.Converters
{
	public class ShownItemNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is DialogLine) {
				return ((DialogLine)value).TextShown;
			} else if (value is Continue) {
				return "Continue";
			} else {
				return "ERROR";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
