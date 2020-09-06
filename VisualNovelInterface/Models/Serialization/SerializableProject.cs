using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializableProject
	{
		public SerializablePanel[] Panels {
			get;
			set;
		}
		public SerializableVariableManager VariableManager {
			get;
			set;
		}
		public SerializableFontManager FontManager {
			get;
			set;
		}
		public SerializableSettings Settings {
			get;
			set;
		}
		public SerializableSprite[] Sprites {
			get;
			set;
		}
		public SerializableSprite[] ButtonSprites {
			get;
			set;
		}
	}
}
