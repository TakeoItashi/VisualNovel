using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface.Models {
    public class Project {
        private string settings;
        private ObservableCollection<Panel> panels;

        public string Settings {
            get => settings;
            set => settings = value;
        }

        public ObservableCollection<Panel> Panels {
            get => panels;
            set => panels = value;
        }

        public Project()
        {
            panels = new ObservableCollection<Panel>();
#if DEBUG
            panels.Add(new Panel("NewPanel_01"));
            panels.Add(new Panel("NewPanel_02"));
            panels.Add(new Panel("NewPanel_03"));
#endif
        }
    }
}