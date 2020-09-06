using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializableBranch
	{
		public string Name {
			get;
			set;
		}
		public SerializableDialogLine[] Items {
			get;
			set;
		}
		public SerializableContinue Continue {
			get;
			set;
		}
	}
}