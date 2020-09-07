using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using VisualNovelInterface.Models;

namespace VisualNovelInterface.Views
{
	/// <summary>
	/// Interaction logic for ExportWizardDialog.xaml
	/// </summary>
	public partial class ExportWizardDialog : Window
	{
		public string GameName {
			get;
			set;
		}

		public SpriteImage MainMenuBackgroundImage {
			get;
			set;
		}

		public ExportWizardDialog() {
			InitializeComponent();
			GameNameTextbox.Text = "VisualNovel";
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e) {
			MainMenuBackgroundImage = (SpriteImage)MainMenuBackgroundList.SelectedItem;
			if (GameNameTextbox.Text == "") {
				GameName = "VisualNovel";
			} else {
				GameName = GameNameTextbox.Text;
			}
			DialogResult = true;
		}
	}
}
