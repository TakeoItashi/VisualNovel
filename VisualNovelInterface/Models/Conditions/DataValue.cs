using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.Models.Enums;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
	public class DataValue : BaseObject
	{
		private string m_name;
		private Tuple<DataValueTypeEnum, object> m_valueTuple;

		public string Name {
			get => m_name;
			set => SetProperty(ref m_name, value);
		}

		public DataValueTypeEnum ValueType {
			get => m_valueTuple.Item1;
			set {
				switch (value) {
					case DataValueTypeEnum.trigger:
						SetProperty(ref m_valueTuple, new Tuple<DataValueTypeEnum, object>(DataValueTypeEnum.trigger, false));
						break;
					case DataValueTypeEnum.variable:
						SetProperty(ref m_valueTuple, new Tuple<DataValueTypeEnum, object>(DataValueTypeEnum.variable, 0));
						break;
					case DataValueTypeEnum.decimalCCPlus:
						SetProperty(ref m_valueTuple, new Tuple<DataValueTypeEnum, object>(DataValueTypeEnum.decimalCCPlus, 0.0f));
						break;
					case DataValueTypeEnum.text:
						SetProperty(ref m_valueTuple, new Tuple<DataValueTypeEnum, object>(DataValueTypeEnum.text, "New Text"));
						break;
					default:
						throw new Exception("Unexpected DataValueTypeEnum. Value was: " + value);
				}
				OnPropertyChanged(nameof(ValueType));
				OnPropertyChanged(nameof(Value));
			}
		}

		public object Value {
			get => m_valueTuple.Item2;
			set =>SetProperty(ref m_valueTuple, new Tuple<DataValueTypeEnum, object>(m_valueTuple.Item1, value));
		}

		public DataValue(string _name, Tuple<DataValueTypeEnum, object> _dataValue) {
			m_name = _name;
			m_valueTuple = _dataValue;
		}
	}
}
