using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VisualNovelInterface.MVVM;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.Models
{
	public class DialogLine : ShownItem
	{
		private string textShown;
		private string characterName;
		private ObservableCollection<SpriteViewModel> m_usedSprites;

		public string TextShown {
			get => textShown;
			set => SetProperty(ref textShown, value);
		}

		public string CharacterName {
			get => characterName;
			set => SetProperty(ref characterName, value);
		}

		public string RenderedCharacterName {
			get => characterName + ':';
		}

		public ObservableCollection<SpriteViewModel> Sprites {
			get => m_usedSprites;
			set => m_usedSprites = value;
		}

		public DialogLine() {
			m_usedSprites = new ObservableCollection<SpriteViewModel>();
		}
	}
}