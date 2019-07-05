using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisualNovelInterface.Models;
using VisualNovelInterface.MVVM;
using System.Windows.Forms;
using System.Drawing;
using Panel = VisualNovelInterface.Models.Panel;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.Windows.Media.Imaging;

namespace VisualNovelInterface
{
	public class TestViewModel : BaseObject
	{
		private DialogueLine DialogueLine;
		private DialogueLine selectedLine;
		private Panel selectedPanel;
		private Project currentProject;
		private ICommand addBackgroundImageCommand;
		private bool canExecute;
		private int selectedPanelIndex;
		private string testImage;

		#region Properties
		public string StringProperty {
			get => DialogueLine.textShown;
			set => SetProperty(ref selectedLine.textShown, value);
		}

		public string NameProperty {
			get => DialogueLine.characterName;
			set => SetProperty(ref selectedLine.characterName, value);
		}

		public Project CurrentProject {
			get => currentProject;
			set => SetProperty(ref currentProject, value);
		}
		public Panel SelectedPanel {
			get => selectedPanel;
			set {
				SetProperty(ref selectedPanel, value);
				SelectedLine = SelectedPanel.DialogueLines.First();
			}
		}
		public DialogueLine SelectedLine {
			get => selectedLine;
			set => SetProperty(ref selectedLine, value);
		}
		public ICommand AddBackgroundImageCommand {
			get => addBackgroundImageCommand;
			set => SetProperty(ref addBackgroundImageCommand, value);
		}
		public int SelectedPanelIndex {
			get => selectedPanelIndex;
			set => SetProperty(ref selectedPanelIndex, value);
		}
		public string TestImage {
			get => testImage;
			set => SetProperty(ref testImage, value);
		}
		#endregion

		public TestViewModel()
		{
			canExecute = true;
			currentProject = new Project();
			DialogueLine = new DialogueLine();
			SelectedLine = new DialogueLine();
			AddBackgroundImageCommand = new RelayCommand(AddBackgroundImage, parameter => canExecute);
		}

		public void AddBackgroundImage(object obj)
		{
			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
				newFile.Filter = "image files (*.png; *.jpg)|*.png; *.jpg";

				if (newFile.ShowDialog() == DialogResult.OK) {

					SelectedPanel.BackgroundImage = newFile.FileName;
				}
			}
		}
	}
}