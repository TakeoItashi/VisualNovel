using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class Condition : BaseObject
	{
		private ObservableCollection<DataValue> m_dataValues;
		private string m_alternativePanelKey;

		public ObservableCollection<DataValue> DataValues {
			get => m_dataValues;
			set => SetProperty(ref m_dataValues, value);
		}
		public string AlternativePanelKey {
			get => m_alternativePanelKey;
			set => m_alternativePanelKey = value;
		}
	}
}
