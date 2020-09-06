using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.Models;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.ProjectExport
{
	public class SpriteExporter
	{
		public readonly Dictionary<Guid, Tuple<int, SpriteImage>> Sprites;
		public readonly Dictionary<Guid, Tuple<int, SpriteImage>> ButtonSprites;

		public readonly Dictionary<Guid, int> SpriteIndexDict;
		public readonly Dictionary<Guid, int> ButtonSpriteIndexDict;

		public readonly SpriteImage[] SpritesArray;
		public readonly SpriteImage[] ButtonSpritesArray;

		public int SpriteCount {
			get => Sprites.Count;
		}

		public int ButtonSpriteCount {
			get => ButtonSprites.Count;
		}

		public SpriteExporter(List<SpriteImage> _sprites, List<SpriteImage> _buttonSprite) {
			Sprites = new Dictionary<Guid,  Tuple<int, SpriteImage>>();
			SpriteIndexDict = new Dictionary<Guid, int>();
			SpritesArray = new SpriteImage[_sprites.Count];
			for (int i = 0; i < _sprites.Count; i++) {
				Sprites.Add(_sprites[i].Id, new Tuple<int, SpriteImage>(i, _sprites[i]));
			}
			foreach (KeyValuePair<Guid,  Tuple<int, SpriteImage>> pair in Sprites) {

				SpriteIndexDict.Add(pair.Key, pair.Value.Item1);
				SpritesArray[pair.Value.Item1] = pair.Value.Item2;
			}

			ButtonSprites = new Dictionary<Guid,  Tuple<int, SpriteImage>>();
			ButtonSpriteIndexDict = new Dictionary<Guid, int>();
			ButtonSpritesArray = new SpriteImage[_buttonSprite.Count];
			for (int i = 0; i < _buttonSprite.Count; i++) {
				ButtonSprites.Add(_buttonSprite[i].Id, new Tuple<int, SpriteImage>(i, _buttonSprite[i]));
			}

			foreach (KeyValuePair<Guid,  Tuple<int, SpriteImage>> pair in ButtonSprites) {

				ButtonSpriteIndexDict.Add(pair.Key, pair.Value.Item1);
				ButtonSpritesArray[pair.Value.Item1] = pair.Value.Item2;
			}
		}

		public int GetSpriteIndex(SpriteImage _sprite) {
			return Sprites[_sprite.Id].Item1;
		}

		public int GetButtonSpriteIndex(SpriteImage _sprite) {
			return ButtonSprites[_sprite.Id].Item1;
		}
	}
}
