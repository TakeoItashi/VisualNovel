using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;
using VisualNovelInterface.ViewModels;

namespace VisualNovelInterface.Models {
	public class Project : BaseObject {
		private string m_settings;
		private ObservableCollection<Panel> m_panels;
		private VariableManagerViewModel m_variableManagerViewModel;

		private Panel m_selectedPanel;

		public string Settings {
			get => m_settings;
			set => m_settings = value;
		}

		public ObservableCollection<Panel> Panels {
			get => m_panels;
			set => m_panels = value;
		}

		public VariableManagerViewModel VariableManagerViewModel{
			get => m_variableManagerViewModel;
			set => m_variableManagerViewModel = value;
		}

		public Panel SelectedPanel {
			get => m_selectedPanel;
			set => SetProperty(ref m_selectedPanel, value);
		}

        public Project()
        {
            m_panels = new ObservableCollection<Panel>();
			m_variableManagerViewModel = new VariableManagerViewModel();
#if DEBUG
            m_panels.Add(new Panel("NewPanel_01"));
            m_panels.Add(new Panel("NewPanel_02"));
            m_panels.Add(new Panel("NewPanel_03"));
#endif
        }
    }
}