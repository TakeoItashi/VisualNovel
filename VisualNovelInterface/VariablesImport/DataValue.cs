using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface.VariablesImport
{
	public class DataValue
	{
		public string Name {
			get;
		}

		public DataValue(string _name, Tuple<DataValueType, object> _dataValue)
		{
			Name = _name;
			Value = _dataValue;
		}

		public Tuple<DataValueType, object> Value {
			get;
		}
	}
}
