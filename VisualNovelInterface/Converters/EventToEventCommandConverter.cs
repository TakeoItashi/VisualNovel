using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using VisualNovelInterface.Models.Args.SenderEventArgs;

namespace VisualNovelInterface.Converters
{
	public class EventToEventCommandConverter : IEventArgsConverter
	{

		public object Convert(object value, object parameter) {
			CustomEventCommandParameter para = new CustomEventCommandParameter() {
				Sender = parameter,
				Args = value
			};
			return para;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
