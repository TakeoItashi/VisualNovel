using VisualNovelInterface.Models.Enums;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializableContinue
	{
		public ContinueTypeEnum Type {
			get;
			set;
		}
		public string ContinueKey {
			get;
			set;
		}
		public SerializableSplit Split {
			get;
			set;
		}
	}
}