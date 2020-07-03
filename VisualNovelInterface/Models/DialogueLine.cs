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

namespace VisualNovelInterface.Models
{
	public class DialogueLine : BaseObject
	{
		private string textShown;
		private int spriteIndex;
		private string characterName;
		private ObservableCollection<Sprite> m_usedSprites;

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

		public ObservableCollection<Sprite> Sprites {
			get => m_usedSprites;
			set => m_usedSprites = value;
		}

		public DialogueLine() {
			m_usedSprites = new ObservableCollection<Sprite>();

			string path = @"F:\Users\Tom Appel\Desktop\Studium\VisualNovel\VisualNovelInterface\Resources\doge.png";
			Image newUriImage = new Image() { Source = new BitmapImage(new Uri(path)) };
			m_usedSprites.Add(new Sprite(path, "DogeSprite1", newUriImage, 0, 0, 100, 100));
			m_usedSprites.Add(new Sprite(path, "DogeSprite2", newUriImage, 100, 100, 100, 100));
		}
	}
}