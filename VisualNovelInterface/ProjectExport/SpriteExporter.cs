using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.Models;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.ProjectExport
{
	public class SpriteExporter {
		public readonly SpriteImage[] Sprites;
		public readonly SpriteImage[] ButtonSprites;

		public int SpriteCount {
			get => Sprites.Length;
		}

		public int ButtonSpriteCount {
			get => ButtonSprites.Length;
		}

		public SpriteExporter(List<SpriteImage> _sprites, List<SpriteImage> _buttonSprite) {
			Sprites = _sprites.ToArray();
			ButtonSprites = _buttonSprite.ToArray();
		}

		public int GetSpriteIndex(SpriteImage _sprite){
			return Array.IndexOf(Sprites, _sprite);
		}

		public int GetButtonSpriteIndex(SpriteImage _sprite){
			return Array.IndexOf(ButtonSprites, _sprite);
		}
	}
}
