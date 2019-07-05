using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface.Models {
    public class Panel : BaseObject {

        private string backgroundImage;
        private string panelName;
        private ObservableCollection<Sprite> sprites;
        private ObservableCollection<DialogueLine> dialogueLines;

		public string BackgroundImage {
            get => backgroundImage;
            set => SetProperty(ref backgroundImage, value);
        }
        public string PanelName {
            get => panelName;
            set => panelName = value;
        }
        public ObservableCollection<Sprite> Sprites {
            get => sprites;
            set => sprites = value;
        }
        public ObservableCollection<DialogueLine> DialogueLines {
            get => dialogueLines;
            set => dialogueLines = value;
        }

		public Panel(string _newPanelName)
        {
            PanelName = _newPanelName;
            sprites = new ObservableCollection<Sprite>();
            dialogueLines = new ObservableCollection<DialogueLine>();
#if DEBUG
            dialogueLines.Add(new DialogueLine { CharacterName = $"Heinrich Meinrich {_newPanelName}", SpriteIndex=0, TextShown = "Lorem Ipsum"});
            dialogueLines.Add(new DialogueLine { CharacterName = $"dlwmvmd {_newPanelName}", SpriteIndex = 0, TextShown = "9ur409ut23409u9589023485" });
            dialogueLines.Add(new DialogueLine { CharacterName = $"39rt md {_newPanelName}", SpriteIndex = 0, TextShown = " 03r dskngkdjfgkdm" });
            dialogueLines.Add(new DialogueLine { CharacterName = $"FFFFFFFFFFFFFFFFF {_newPanelName}", SpriteIndex = 0, TextShown = "ffffffffffff" });
#endif
        }
    }
}