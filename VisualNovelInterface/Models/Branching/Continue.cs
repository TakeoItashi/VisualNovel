using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		public Option.ButtonSpriteChange OpenButtonSpriteDialogReference;

		public ContinueTypeEnum Type {
			get => m_type;
			set {
				if (value == ContinueTypeEnum.Split) {
					ContinueKey = null;
					Option newOption1 = new Option("Option1", "Option1", new SpriteImage(), new Continue(ContinueTypeEnum.Branch, "Branch5"));
					newOption1.OnButtonSpriteChange += OpenButtonSpriteDialogReference;
					Option newOption2 = new Option("Option2", "Option2", new SpriteImage(), new Continue(ContinueTypeEnum.Branch, "Branch6"));
					newOption2.OnButtonSpriteChange += OpenButtonSpriteDialogReference;
					Split = new Split("newSplit", new ObservableCollection<Option>() { newOption1, newOption2 });
				} else if (value == ContinueTypeEnum.Branch ||
						   value == ContinueTypeEnum.Panel) {
					Split = null;
					ContinueKey = "";
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

		public Continue() {
		}
	}
}
