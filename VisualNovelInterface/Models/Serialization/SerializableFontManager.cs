using System.Windows.Media;

namespace VisualNovelInterface.Models.Serialization
{
	public class SerializableFontManager
	{
		public int FontSize {
			get;
			set;
		}
		public FontFamily[] Fonts {
			get;
			set;
		}
		public FontFamily CurrentUsedFont {
			get;
			set;
		}
	}
}