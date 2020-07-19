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
	public class DialogueLine : BaseObject
	{
		private string textShown;
		private int spriteIndex;
		private string characterName;
		private ObservableCollection<SpriteViewModel> m_usedSprites;
		private ObservableCollection<int> m_spriteIds;

		public string TextShown {
			get => textShown;
			set => SetProperty(ref textShown, value);
		}

		public int SpriteIndex {
			get => spriteIndex;
			set => SetProperty(ref spriteIndex, value);
		}

		public string CharacterName {
			get => characterName;
			set => SetProperty(ref characterName, value);
		}

		public ObservableCollection<SpriteViewModel> Sprites {
			get => m_usedSprites;
			set => m_usedSprites = value;
		}

		public ObservableCollection<int> SpriteIds {
			get => m_spriteIds;
			set => m_spriteIds = value;
		}

		public DialogueLine() {
			m_usedSprites = new ObservableCollection<SpriteViewModel>();
		}
	}
}