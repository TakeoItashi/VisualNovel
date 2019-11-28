using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.VariablesImport
{
	public class VariableManagerViewModel : BaseObject
	{
		private List<DataValue> m_variables;

		public VariableManagerViewModel()
		{
			m_variables = new List<DataValue> {
				new DataValue("testBool", new Tuple<DataValueType, object>(DataValueType.trigger, true)),
				new DataValue("testInt", new Tuple<DataValueType, object>(DataValueType.variable, 24)),
				new DataValue("testString", new Tuple<DataValueType, object>(DataValueType.text, "test test"))
			};
		}

		public List<DataValue> Variables {
			get => m_variables;
			set => SetProperty(ref m_variables, value);
		}
	}
}
