using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Branch : BaseObject
	{
		private string m_name;
		private ObservableCollection<ShownItem> m_shownItems;
		private Continue m_continue;
		private ShownItem m_selectedItem;
		private bool m_isEntryBranch;

		public delegate void SetEntryBranchEvent(Branch _branch);
		public event SetEntryBranchEvent SetEntryBranchEventHandler;

		public string Name {
			get => m_name;
			set => SetProperty(ref m_name, value);
		}

		public ObservableCollection<ShownItem> ShownItems {
			get => m_shownItems;
			set => SetProperty(ref m_shownItems, value);
		}

		public ObservableCollection<ShownItem> ShownItemsList {
			get => new ObservableCollection<ShownItem>(m_shownItems) { m_continue };
		}

		public Continue Continue {
			get => m_continue;
			set => SetProperty(ref m_continue, value);
		}
		public ShownItem SelectedItem {
			get => m_selectedItem;
			set => SetProperty(ref m_selectedItem, value);
		}
		public bool IsEntryBranch {
			get => m_isEntryBranch;
			set => SetProperty(ref m_isEntryBranch, value);
		}

		public RelayCommand SetToEntryBranchCommand {
			get;
			set;
		}

		public Branch(string _name, ObservableCollection<ShownItem> _shownItems, Continue _continue = null) {
			m_name = _name;
			m_shownItems = _shownItems;
			m_continue = _continue;
			m_selectedItem = m_shownItems.First();
			SetToEntryBranchCommand = new RelayCommand(SetToEntryBranch);
		}

		public void SetToEntryBranch() {
		
			SetEntryBranchEventHandler.Invoke(this);
		}
	}
}
