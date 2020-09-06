using VisualNovelInterface.Models.Enums;

namespace VisualNovelInterface.Models.Serialization
{
	public struct SerializableDataValue
	{
		public string Name {
			get;
			set;
		}
		public DataValueTypeEnum Type {
			get;
			set;
		}
		public object Value {
			get;
			set;
		}
	}
}