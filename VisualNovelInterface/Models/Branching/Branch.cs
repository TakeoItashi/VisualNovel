using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.Models.ShownItems;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Branch : BaseObject
	{
		private string m_name;
		private ObservableCollection<ShownItem> m_shownItems;
		private Continue m_continue;

		public string Name {
			get => m_name;
			set => SetProperty(ref m_name, value);
		}

		public ObservableCollection<ShownItem> ShownItems {
			get => m_shownItems;
			set => SetProperty(ref m_shownItems, value);
		}

		public Continue Continue {
			get => m_continue;
			set => SetProperty(ref m_continue, value);
		}

		public Branch(string _name, ObservableCollection<ShownItem> _shownItems, Continue _continue) {
			m_name = _name;
			m_shownItems = _shownItems;
			m_continue = _continue;
		}
	}
}
