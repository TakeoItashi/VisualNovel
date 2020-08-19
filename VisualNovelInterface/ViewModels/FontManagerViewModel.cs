using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using VisualNovelInterface.MVVM;
using FontFamily = System.Windows.Media.FontFamily;

namespace VisualNovelInterface.ViewModels
{
	public class FontManagerViewModel : BaseObject
	{
		private ObservableCollection<FontFamily> m_fonts;
		private FontFamily m_selectedFont;
		private FontFamily m_currentUsedFont;

		public ObservableCollection<FontFamily> Fonts {
			get => m_fonts;
			set => SetProperty(ref m_fonts, value);
		}

		public FontFamily SelectedFont {
			get => m_selectedFont;
			set => SetProperty(ref m_selectedFont, value);
		}

		public FontFamily CurrentUsedFont {
			get => m_currentUsedFont;
			set => SetProperty(ref m_currentUsedFont, value);
		}

		public bool IsFontSelected {
			get => SelectedFont != null;
		}

		public RelayCommand AddNewFontCommand {
			get;
			set;
		}

		public RelayCommand RemoveSelectedFontCommand {
			get;
			set;
		}

		public FontManagerViewModel(List<FontFamily> _fonts) {
			Fonts = new ObservableCollection<FontFamily>(_fonts);
			SelectedFont = Fonts.First();
			InitCommands();
		}
		public FontManagerViewModel(FontFamily _font) {
			Fonts = new ObservableCollection<FontFamily>() { _font };
			SelectedFont = _font;
			InitCommands();
		}
		public FontManagerViewModel() {
			Fonts = new ObservableCollection<FontFamily>();
			InitCommands();
		}

		public void InitCommands() {
			AddNewFontCommand = new RelayCommand(AddNewFont);
			RemoveSelectedFontCommand = new RelayCommand(RemoveSelectedFont);
		}

		private void AddNewFont() {
		
			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
				newFile.Filter = "TrueType-Fonts (*.ttf)|*.ttf";

				if (newFile.ShowDialog() == DialogResult.OK) {

					string path = newFile.FileName;
					FontFamily newFont = new FontFamily(new Uri(path), Path.GetFileNameWithoutExtension(newFile.SafeFileName));
					Fonts.Add(newFont);
				}
			}
		}

		private void RemoveSelectedFont() {
			if (SelectedFont != CurrentUsedFont) {
				Fonts.Remove(SelectedFont);
			} else {
				System.Windows.MessageBox.Show("Cannot remove currently used font!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
