using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Continue : BaseObject
	{
		private ContinueTypeEnum m_type;
		private string m_continueKey;
		private Split m_split;

		public ContinueTypeEnum Type {
			get => m_type;
			set => SetProperty(ref m_type, value);
		}

		public string ContinueKey {
			get => m_continueKey;
			set => SetProperty(ref m_continueKey, value);
		}

		public Split Split {
			get => m_split;
			set => SetProperty(ref m_split, value);
		}
	}
}
