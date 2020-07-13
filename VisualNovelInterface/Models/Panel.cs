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
        private ObservableCollection<DialogueLine> dialogueLines;
		private DialogueLine m_selectedDialogLine;

		public string BackgroundImage {
            get => backgroundImage;
            set => SetProperty(ref backgroundImage, value);
        }

        public string PanelName {
            get => panelName;
            set => panelName = value;
        }

        public ObservableCollection<DialogueLine> DialogueLines {
            get => dialogueLines;
            set => dialogueLines = value;
        }

		public DialogueLine SelectedLine {
			get => m_selectedDialogLine;
			set => SetProperty(ref m_selectedDialogLine, value);
		}

		public Panel(string _newPanelName)
        {
            PanelName = _newPanelName;

            dialogueLines = new ObservableCollection<DialogueLine>();
        }
    }
}