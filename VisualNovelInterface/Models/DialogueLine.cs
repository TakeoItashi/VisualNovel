using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models
{
    public class DialogueLine : BaseObject
    {
        public string textShown;
        public int spriteIndex;
        public string characterName;

        public string TextShown {
            get => textShown;
            set => SetProperty(ref textShown, value);
        }
        public int SpriteIndex {
            get => spriteIndex;
            set => SetProperty(ref spriteIndex, value);
        }
        public string CharacterName {
            get => characterName;
            set => SetProperty(ref characterName, value);
        }
    }
}