using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.VariablesImport
{
	public class VariablesManager : BaseObject
	{
		private List<DataValue> m_variables;

		private static VariablesManager m_instance;
		public static VariablesManager Instance {
			get {
				if (m_instance == null)
					m_instance = new VariablesManager();
				return m_instance;
			}
		}

		public VariablesManager()
		{

		}

		public List<DataValue> Variables {
			get => m_variables;
			set => SetProperty(ref m_variables, value);
		}
	}
}
