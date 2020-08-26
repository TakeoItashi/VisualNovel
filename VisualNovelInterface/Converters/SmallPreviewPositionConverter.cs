using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.Converters
{
	public class SmallPreviewPositionConverter : DependencyObject, IValueConverter
	{
		public UIElement UIParameter {
			get;
			set;
		}

		public SettingsViewModel SettingsReference {
			get => (SettingsViewModel)GetValue(SettingsReferenceProperty);
			set => SetValue(SettingsReferenceProperty, value);
		}

		public static readonly DependencyProperty SettingsReferenceProperty = DependencyProperty.Register(nameof(SettingsReference), typeof(SettingsViewModel), typeof(SmallPreviewPositionConverter),new PropertyMetadata(null));

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			double StepSizeWidth = 0;
			double StepSizeHeight = 0;
			double position = (double)value;
			if ((string)parameter == "PosX") {
				StepSizeWidth = ((Grid)UIParameter).ActualWidth / SettingsReference.WindowWidth;
				position *= StepSizeWidth;
			} else if ((string)parameter == "PosY") {
				StepSizeHeight = ((Grid)UIParameter).ActualHeight / SettingsReference.WindowHeight;
				position *= StepSizeHeight;
			}
			return position;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
