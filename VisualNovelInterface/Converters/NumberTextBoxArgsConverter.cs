using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using VisualNovelInterface.Models.Args;

namespace VisualNovelInterface.Converters
{
	public class NumberTextBoxArgsConverter : IEventArgsConverter
	{

		public object Convert(object value, object parameter) {
			return new TextBoxEventArgs(parameter, value as TextCompositionEventArgs);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
