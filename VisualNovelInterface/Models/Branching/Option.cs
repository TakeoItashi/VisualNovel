using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Option : BaseObject
	{
		private string m_name;
		private string m_shownText;
		private SpriteImage m_buttonSprite;

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
	}
}
