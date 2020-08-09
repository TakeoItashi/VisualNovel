using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Split : BaseObject
	{
		private ObservableCollection<Option> m_options;

		public ObservableCollection<Option> Options {
			get => m_options;
			set => SetProperty(ref m_options, value);
		}
	}
}
