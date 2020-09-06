using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using VisualNovelInterface.Models;
using VisualNovelInterface.MVVM;
using FontFamily = System.Windows.Media.FontFamily;

namespace VisualNovelInterface.ViewModels
{
	public class FontManagerViewModel : BaseObject
	{
		private ObservableCollection<ProjectFont> m_fonts;
		private ProjectFont m_selectedFont;
		private ProjectFont m_currentUsedFont;
		private int m_fontSize;

		public delegate void SetNewUsedFontEventHandler();
		public event SetNewUsedFontEventHandler OnNewUsedFont;

		public ObservableCollection<ProjectFont> Fonts {
			get => m_fonts;
			set => SetProperty(ref m_fonts, value);
		}

		public ProjectFont SelectedFont {
			get => m_selectedFont;
			set => SetProperty(ref m_selectedFont, value);
		}

		public ProjectFont CurrentUsedFont {
			get => m_currentUsedFont;
			set {
				if (m_currentUsedFont != null) {
					m_currentUsedFont.IsUsed = false;
				}
				SetProperty(ref m_currentUsedFont, value);
				if (m_currentUsedFont != null) {
					m_currentUsedFont.IsUsed = true;
				}
			}
		}

		public bool IsFontSelected {
			get => SelectedFont != null;
		}

		public int FontSize {
			get => m_fontSize;
			set => SetProperty(ref m_fontSize, value);
		}

		[JsonIgnore]
		public RelayCommand AddNewFontCommand {
			get;
			set;
		}
		[JsonIgnore]
		public RelayCommand RemoveSelectedFontCommand {
			get;
			set;
		}
		[JsonIgnore]
		public RelayCommand<ProjectFont> SetNewUsedFontCommand {
			get;
			set;
		}

		public FontManagerViewModel(List<ProjectFont> _fonts) {
			Fonts = new ObservableCollection<ProjectFont>(_fonts);
			SelectedFont = Fonts.First();
			FontSize = 26;
			InitCommands();
		}
		public FontManagerViewModel(ProjectFont _font) {
			Fonts = new ObservableCollection<ProjectFont>() { _font };
			SelectedFont = _font;
			FontSize = 26;
			InitCommands();
		}
		public FontManagerViewModel() {
			Fonts = new ObservableCollection<ProjectFont>();
			FontSize = 26;
			InitCommands();
		}

		public void InitCommands() {
			AddNewFontCommand = new RelayCommand(AddNewFont);
			RemoveSelectedFontCommand = new RelayCommand(RemoveSelectedFont);
			SetNewUsedFontCommand = new RelayCommand<ProjectFont>(SetNewUsedFont);
		}

		private void AddNewFont() {

			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
				newFile.Filter = "TrueType-Fonts (*.ttf)|*.ttf";

				if (newFile.ShowDialog() == DialogResult.OK) {

					string path = newFile.FileName;
					ProjectFont newFont = new ProjectFont(){ Font = new FontFamily(new Uri(path), Path.GetFileNameWithoutExtension(newFile.SafeFileName)), IsUsed = false };
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

		private void SetNewUsedFont(ProjectFont _newFont) {
			CurrentUsedFont = _newFont;
			OnNewUsedFont.Invoke();
			//--> Relay Property Change Event to MainViewModel
		}
	}
}
