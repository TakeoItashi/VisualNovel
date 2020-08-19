using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Split : ShownItem
	{
		private ObservableCollection<Option> m_options;
		private string m_name;

		public ObservableCollection<Option> Options {
			get => m_options;
			set => SetProperty(ref m_options, value);
		}
		public string Name {
			get => m_name;
			set => SetProperty(ref m_name, value) ;
		}

		public Split(string _name, ObservableCollection<Option> _options) {
			m_name = _name;
			m_options = _options;
		}
	}
}
