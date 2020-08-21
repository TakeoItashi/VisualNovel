using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualNovelInterface.Models;
using VisualNovelInterface.ViewModels;
using static VisualNovelInterface.ViewModels.MainViewModel;

namespace VisualNovelInterface
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() {
		}

		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
			TextBlock item = sender as TextBlock;

			if (item != null && e.LeftButton == MouseButtonState.Pressed) {
				DragDrop.DoDragDrop(item,
									item.DataContext,
									DragDropEffects.Copy);
			}
		}

		private void ArrangeButtons(ObservableCollection<Option> _options) {
			double Height = Option_Preview.ActualHeight;
			double Width = Option_Preview.ActualWidth;

			double ItemsSpace = Height / (_options.Count +1);
			for (int i = 1; i <= Option_Preview.Items.Count; ++i) {

				Option currentOption = Option_Preview.Items[i-1] as Option;
				currentOption.Height = ItemsSpace;
				currentOption.Width = Width / 3;
				currentOption.PosX = (Width / 2) - currentOption.Width;
				currentOption.PosY = ItemsSpace * i;
			}
		}

		private void Refresh_Button_Click(object sender, RoutedEventArgs e) {
			((MainViewModel)DataContext).RefreshButtons();
		}

		private void Auto_Position_Button_Click(object sender, RoutedEventArgs e) {
			((MainViewModel)DataContext).RefreshButtons();
		}

		private void AutoPositionSprites(ObservableCollection<SpriteViewModel> _sprites) {
			
		}
	}
}
