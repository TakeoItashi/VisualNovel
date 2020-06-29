using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VisualNovelInterface.Models.Args
{
	public class TextBoxEventArgs : EventArgs
	{
		public object sender {
			get;
			set;
		}
		public TextCompositionEventArgs args {
			get;
			set;
		}

		public TextBoxEventArgs(object _sender, TextCompositionEventArgs _args) {
			sender = _sender;
			args = _args;
		}
	}
}
