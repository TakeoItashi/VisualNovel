using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Sprite : BaseObject
	{
		string m_image, m_name;
		Image m_uriImage;
		int m_posX, m_posY, m_height, m_width;

		public string Image {
			get => m_image;
			set => SetProperty(ref m_image, value);
		}

		public Image UriImage {
			get => m_uriImage;
			set => SetProperty(ref m_uriImage, value);
		}

		public string Name {
			get => m_name;
			set => SetProperty(ref m_name, value);
		}

		public int PosX {
			get => m_posX;
			set => SetProperty(ref m_posX, value);
		}
		public int PosY {
			get => m_posY;
			set => SetProperty(ref m_posY, value);
		}
		public int Height {
			get => m_height;
			set => SetProperty(ref m_height, value);
		}
		public int Width {
			get => m_width;
			set => SetProperty(ref m_width, value);
		}

		public RelayCommand MoveSpriteCommand {
			get;
			set;
		}

		public Sprite(string _image, string _name, Image _uriImage, int _posX, int _posY, int _height, int _width) {
			m_image = _image;
			m_name = _name;
			m_uriImage = _uriImage;
			m_posX = _posX;
			m_posY = _posY;
			m_height = _height;
			m_width = _width;
			MoveSpriteCommand = new RelayCommand(MoveSprite);
		}

		private void MoveSprite() {

		}
	}
}