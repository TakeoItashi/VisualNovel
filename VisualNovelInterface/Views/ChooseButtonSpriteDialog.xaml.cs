using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VisualNovelInterface.Models;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Views
{
	/// <summary>
	/// Interaction logic for ChooseButtonSpirteDialog.xaml
	/// </summary>
	public partial class ChooseButtonSpirteDialog : Window
	{

		public SpriteImage SpriteChoice {
			get;
			set;
		}

		public ChooseButtonSpirteDialog() {
			InitializeComponent();
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e) {
			SpriteChoice = (SpriteImage)ButtonSpriteList.SelectedItem;
			DialogResult = true;
		}
	}
}
