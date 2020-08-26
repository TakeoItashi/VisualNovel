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
using VisualNovelInterface.Controls;
using VisualNovelInterface.Models;
using VisualNovelInterface.ViewModels;
using static VisualNovelInterface.ViewModels.MainViewModel;
using Panel = VisualNovelInterface.Models.Panel;

namespace VisualNovelInterface
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ListViewDragDropManager<Panel> PanelList;
		private ListViewDragDropManager<Branch> BranchList;
		private ListViewDragDropManager<DialogLine> ItemList;
		private MainViewModel mvm;

		public MainWindow() {
			InitializeComponent();
			mvm = new MainViewModel(this);
			DataContext = mvm;
			mvm.OnReorganizeButtons += ArrangeButtons;
			mvm.OnAutoPositionSprites += AutoPositionSprites;
			PanelListView.ItemsSource = mvm.CurrentProject.Panels;
			Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e) {

			// Give the PanelListView an ObservableCollection of Task 
			// as a data source.  Note, the PanelListViewDragManager MUST
			// be bound to an ObservableCollection, where the collection's
			// type parameter matches the PanelListViewDragManager's type
			// parameter (in this case, both have a type parameter of Task).

			// This is all that you need to do, in order to use the PanelListViewDragManager.
			PanelList = new ListViewDragDropManager<Panel>(PanelListView);
			BranchList = new ListViewDragDropManager<Branch>(BranchListView);
			//ItemList = new ListViewDragDropManager<DialogLine>(ItemsListView);

			// Show and hide the drag adorner.
			// this.PanelList.ShowDragAdorner = true;

			// Change the opacity of the drag adorner.
			//this.PanelList.DragAdornerOpacity = this.sldDragOpacity.Value; 

			// Apply or remove the item container style, which responds to changes
			// in the attached properties of PanelListViewItemDragState.
			PanelListView.ItemContainerStyle = FindResource("ItemContStyle") as Style;
			BranchListView.ItemContainerStyle = FindResource("ItemContStyle") as Style;
			ItemsListView.ItemContainerStyle = FindResource("ItemContStyle") as Style;


			// Hook up events on both PanelListViews to that we can drag-drop
			// items between them.
			PanelListView.DragEnter += OnListViewDragEnter;
			BranchListView.DragEnter += OnListViewDragEnter;
			//ItemsListView.DragEnter += OnListViewDragEnter;
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
				currentOption.PosX = (Width / 2) - (currentOption.Width/2);
				currentOption.PosY = (ItemsSpace * i) - (currentOption.Height/2);
			}
			UpdateLayout();
		}

		private void Refresh_Button_Click(object sender, RoutedEventArgs e) {
			mvm.RefreshButtons();
		}

		private void Auto_Position_Button_Click(object sender, RoutedEventArgs e) {
			mvm.TriggerAutoPositionSprites();
		}

		private void AutoPositionSprites(ObservableCollection<SpriteViewModel> _sprites) {

			double Height = Sprite_Preview.ActualHeight;
			double Width = Sprite_Preview.ActualWidth;

			double ItemsSpace = Width / (_sprites.Count +1);
			for (int i = 1; i <= _sprites.Count; ++i) {

				SpriteViewModel currentSprite = _sprites[i-1];

				currentSprite.PosY = (Height / 2) - (currentSprite.Height /2);
				currentSprite.PosX = (ItemsSpace * i) - (currentSprite.Width / 2);
			}
		}

		#region Drag & Drop
		// Handles the DragEnter event for both ListViews.
		void OnListViewDragEnter(object sender, DragEventArgs e) {
			e.Effects = DragDropEffects.Move;
			Console.WriteLine(e.Effects);
		}
		#endregion
	}
}
