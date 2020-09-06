using System;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializablePanel
	{
		public Guid BackgroundId {
			get;
			set;
		}
		public string PanelName {
			get;
			set;
		}
		public SerializableBranch[] Branches {
			get;
			set;
		}
		public SerializableCondition Condition {
			get;
			set;
		}
		public string EntryBranch {
			get;
			set;
		}
	}
}