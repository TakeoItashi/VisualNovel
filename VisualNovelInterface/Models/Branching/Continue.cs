using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.Models.Enums;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Continue : ShownItem
	{
		private ContinueTypeEnum m_type;
		private string m_continueKey;
		private Split m_split;

		public ContinueTypeEnum Type {
			get => m_type;
			set {
				if (m_type == ContinueTypeEnum.Split) {
					ContinueKey = null;
				}
				SetProperty(ref m_type, value);
			}
		}

		public string ContinueKey {
			get => m_continueKey;
			set => SetProperty(ref m_continueKey, value);
		}

		public Split Split {
			get => m_split;
			set => SetProperty(ref m_split, value);
		}


		public Continue(ContinueTypeEnum _type, Split _split) {
			m_type = _type;
			m_split = _split;
		}

		public Continue(ContinueTypeEnum _type, string _continueKey) {
			m_type = _type;
			m_continueKey = _continueKey;
		}
	}
}
