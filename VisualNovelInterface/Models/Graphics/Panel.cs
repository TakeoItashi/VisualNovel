using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using VisualNovelInterface.MVVM;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.Models {
    public class Panel : BaseObject {

        private string backgroundImage;
        private string panelName;
        private ObservableCollection<DialogLine> dialogueLines;
		private DialogLine m_selectedDialogLine;
        private ObservableCollection<Branch> m_branches;
        private Condition m_condition;
        private ObservableCollection<SpriteImage> m_spriteImages;
        private string m_EntryBranchKey;

		public string BackgroundImage {
            get => backgroundImage;
            set => SetProperty(ref backgroundImage, value);
        }

        public string PanelName {
            get => panelName;
            set => panelName = value;
        }

        public ObservableCollection<DialogLine> DialogueLines {
            get => dialogueLines;
            set => dialogueLines = value;
        }

		public DialogLine SelectedLine {
			get => m_selectedDialogLine;
			set => SetProperty(ref m_selectedDialogLine, value);
		}
        public ObservableCollection<Branch> Branches {
            get => m_branches;
            set => m_branches = value;
        }
        public Condition Condition {
            get => m_condition;
            set => SetProperty(ref m_condition, value);
        }
        public ObservableCollection<SpriteImage> SpriteImages {
            get => m_spriteImages;
            set => m_spriteImages = value;
        }
        public string EntryBranchKey {
            get => m_EntryBranchKey;
            set => SetProperty(ref m_EntryBranchKey, value);
        }

        public Panel(string _newPanelName)
        {
            PanelName = _newPanelName;

            dialogueLines = new ObservableCollection<DialogLine>();
        }
    }
}