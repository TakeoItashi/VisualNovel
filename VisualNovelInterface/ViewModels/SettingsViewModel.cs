using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.ViewModels
{
	public class SettingsViewModel : BaseObject
	{
		private int m_windowWidth;
		private int m_windowHeight;
		private byte m_textBoxRed;
		private byte m_textBoxGreen;
		private byte m_textBoxBlue;
		private byte m_textBoxAlpha;

		public int WindowWidth {
			get => m_windowWidth;
			set => SetProperty(ref m_windowWidth, value);
		}
		public int WindowHeight {
			get => m_windowHeight;
			set => SetProperty(ref m_windowHeight, value);
		}
		public byte TextBoxRed {
			get => m_textBoxRed;
			set {
				SetProperty(ref m_textBoxRed, value);
				OnPropertyChanged(nameof(TextBoxColorBrush));
			}
		}
		public byte TextBoxGreen {
			get => m_textBoxGreen;
			set {
				SetProperty(ref m_textBoxGreen, value);
				OnPropertyChanged(nameof(TextBoxColorBrush));
			}
		}
		public byte TextBoxBlue {
			get => m_textBoxBlue;
			set {
				SetProperty(ref m_textBoxBlue, value);
				OnPropertyChanged(nameof(TextBoxColorBrush));
			}
		}
		public byte TextBoxAlpha {
			get => m_textBoxAlpha;
			set {
				SetProperty(ref m_textBoxAlpha, value);
				OnPropertyChanged(nameof(TextBoxColorBrush));
			}
		}

		public SolidColorBrush TextBoxColorBrush {
			get => new SolidColorBrush(Color.FromArgb(TextBoxAlpha, TextBoxRed, TextBoxGreen, TextBoxBlue));
		}

		public SettingsViewModel(int _windowWidth = 1280, int _windowHeight = 720, byte _textBoxRed = 0, byte _textBoxGreen = 0, byte _textBoxBlue = 255, byte _textBoxAlpha = 255) {
			WindowWidth = _windowWidth;
			WindowHeight = _windowHeight;
			TextBoxRed = _textBoxRed;
			TextBoxGreen = _textBoxGreen;
			TextBoxBlue = _textBoxBlue;
			TextBoxAlpha = _textBoxAlpha;
		}
	}
}
