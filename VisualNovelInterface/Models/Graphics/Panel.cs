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

        private SpriteImage backgroundImage;
        private string panelName;
		private DialogLine m_selectedDialogLine;
        private ObservableCollection<Branch> m_branches;
        private Condition m_condition;
        private ObservableCollection<SpriteImage> m_spriteImages;
        private string m_EntryBranchKey;
        private Branch m_selectedBranch;

		public SpriteImage BackgroundImage {
            get => backgroundImage;
            set => SetProperty(ref backgroundImage, value);
        }

        public string PanelName {
            get => panelName;
            set => panelName = value;
        }

		public DialogLine SelectedLine {
			get => m_selectedDialogLine;
			set => SetProperty(ref m_selectedDialogLine, value);
		}

        public ObservableCollection<Branch> Branches {
            get => m_branches;
            set => SetProperty(ref m_branches, value);
        }

        public Condition Condition {
            get => m_condition;
            set => SetProperty(ref m_condition, value);
        }

        public Dictionary<Guid, SpriteImage> SpriteImages {
            get;
            set;
        }

        public string EntryBranchKey {
            get => m_EntryBranchKey;
            set => SetProperty(ref m_EntryBranchKey, value);
        }

        public Branch SelectedBranch {
            get => m_selectedBranch;
            set => SetProperty(ref m_selectedBranch, value);
        }


        public Panel(string _newPanelName, Option.ButtonSpriteChange _buttonSpriteChangeDelegate)
        {
            PanelName = _newPanelName;

            Branches = new ObservableCollection<Branch>();
            Branches.Add(new Branch("Branch1"));
            Branches.First().Continue.OpenButtonSpriteDialogReference = _buttonSpriteChangeDelegate;
            Branches.First().IsEntryBranch = true;
            EntryBranchKey = Branches.First().Name;
            SpriteImages = new Dictionary<Guid, SpriteImage>();
            SelectedBranch = Branches.First();
        }
    }
}