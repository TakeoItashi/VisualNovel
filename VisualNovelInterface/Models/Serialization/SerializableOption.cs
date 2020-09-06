using System;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializableOption
	{
		public string Name {
			get;
			set;
		}
		public string ShownText {
			get;
			set;
		}
		public Guid ButtonSpriteId {
			get;
			set;
		}
		public SerializableContinue Continue {
			get;
			set;
		}
		public double Height {
			get;
			set;
		}
		public double Width {
			get;
			set;
		}
		public double PosX {
			get;
			set;
		}
		public double PosY {
			get;
			set;
		}
	}
}