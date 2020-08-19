using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Option : BaseObject
	{
		private string m_name;
		private string m_shownText;
		private SpriteImage m_buttonSprite;
		private Continue m_continue;

		private double m_height;
		private double m_width;
		private double m_posX;
		private double m_posY;

		public string Name {
			get => m_name;
			set => SetProperty(ref m_name, value);
		}

		public string ShownText {
			get => m_shownText;
			set => SetProperty(ref m_shownText, value);
		}

		public SpriteImage ButtonSprite {
			get => m_buttonSprite;
			set => SetProperty(ref m_buttonSprite, value);
		}

		public CroppedBitmap NormalButtonSprite {
			get => new CroppedBitmap(ButtonSprite.BitmapImage, new Int32Rect(0, 0, ButtonSprite.BitmapImage.PixelWidth/2, ButtonSprite.BitmapImage.PixelHeight/2));
		}

		public Continue Continue {
			get => m_continue;
			set => SetProperty(ref m_continue, value);
		}

		public double Height {
			get => m_height;
			set => SetProperty(ref m_height, value);
		}

		public double Width {
			get => m_width;
			set => SetProperty(ref m_width, value);
		}

		public double PosX {
			get => m_posX;
			set => SetProperty(ref m_posX, value);
		}

		public double PosY {
			get => m_posY;
			set => SetProperty(ref m_posY, value);
		}

		public Option(string _name, string _shownText, SpriteImage _buttonSprite, Continue _continue, Rect _rect = default) {
			m_name = _name;
			m_shownText = _shownText;
			m_buttonSprite = _buttonSprite;
			m_continue = _continue;
			Height = _rect.Height;
			Width = _rect.Width;
			PosX = _rect.X;
			PosY = _rect.Y;
		}
	}
}
