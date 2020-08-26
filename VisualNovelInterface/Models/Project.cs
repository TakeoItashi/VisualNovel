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
		private ObservableCollection<Panel> m_panels;
		private VariableManagerViewModel m_variableManagerViewModel;
		private FontManagerViewModel m_fontManagerViewModel;
		private SettingsViewModel m_projectSettingsViewModel;

		private Panel m_selectedPanel;

		public ObservableCollection<Panel> Panels {
			get => m_panels;
			set => m_panels = value;
		}

		public VariableManagerViewModel VariableManagerViewModel {
			get => m_variableManagerViewModel;
			set => m_variableManagerViewModel = value;
		}

		public Panel SelectedPanel {
			get => m_selectedPanel;
			set => SetProperty(ref m_selectedPanel, value);
		}
		public FontManagerViewModel FontManagerViewModel {
			get => m_fontManagerViewModel;
			set => SetProperty(ref m_fontManagerViewModel, value);
		}
		public SettingsViewModel ProjectSettingsViewModel {
			get => m_projectSettingsViewModel;
			set => SetProperty(ref m_projectSettingsViewModel, value);
		}

		public Project()
        {
            m_panels = new ObservableCollection<Panel>();
			m_variableManagerViewModel = new VariableManagerViewModel();
			m_fontManagerViewModel = new FontManagerViewModel();
			ProjectSettingsViewModel = new SettingsViewModel();
        }
    }
}