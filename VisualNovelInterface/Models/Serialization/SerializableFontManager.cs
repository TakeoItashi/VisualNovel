using System;
using System.Windows.Media;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializableFontManager
	{
		public int FontSize {
			get;
			set;
		}
		public Tuple<string, string>[] EncodedFontName_FontFiles {
			get;
			set;
		}
		public FontFamily CurrentUsedFont {
			get;
			set;
		}
	}
}