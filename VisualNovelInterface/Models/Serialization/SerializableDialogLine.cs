using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializableDialogLine : SerializableShownItem
	{
		public string TextShown {
			get;
			set;
		}
		public string CharacterName {
			get;
			set;
		}
		public SerializableSpriteViewModel[] UsedSprites {
			get;
			set;
		}
	}
}
