using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class ProjectFont : BaseObject
	{
		private FontFamily m_font;
		private bool m_isUsed;

		public FontFamily Font {
			get => m_font;
			set => SetProperty(ref m_font, value);
		}

		public bool IsUsed {
			get => m_isUsed;
			set => SetProperty(ref m_isUsed, value);
		}
	}
}
