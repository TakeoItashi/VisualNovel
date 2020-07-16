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
using System.Runtime.InteropServices;
using VisualNovelInterface.VariablesImport;
using VisualNovelInterface.Views;
using VisualNovelInterface.Models.Enums;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using VisualNovelInterface.Models.Args.SenderEventArgs;
using System.Windows;
using System.IO;

namespace VisualNovelInterface.ViewModels
{
	public class MainViewModel : BaseObject
	{
		private DialogueLine selectedLine;
		private Panel selectedPanel;
		private Project currentProject;
		private SpriteViewModel m_selectedSprite;
		private bool canExecute;
		private int selectedPanelIndex;
		private ObservableCollection<SpriteViewModel> m_globalSprites;


		#region Properties
		public Project CurrentProject {
			get => currentProject;
			set => SetProperty(ref currentProject, value);
		}

		public SpriteViewModel SelectedSprite {
			get => m_selectedSprite;
			set {
				if (m_selectedSprite != null) {
					m_selectedSprite.IsSelected = false;
				}
				SetProperty(ref m_selectedSprite, value);
				m_selectedSprite.IsSelected = true;
			}
		}

		public Panel SelectedPanel {
			get => currentProject.SelectedPanel;
			set {
				currentProject.SelectedPanel = value;
				SelectedLine = SelectedPanel.DialogueLines.First();
			}
		}
		public DialogueLine SelectedLine {
			get => SelectedPanel.SelectedLine;
			set => SelectedPanel.SelectedLine = value;
		}
		public RelayCommand AddBackgroundImageCommand {
			get;
			set;
		}
		public RelayCommand OpenVariableManagerCommand {
			get;
			set;
		}

		public RelayCommand AddNewSpriteCommand {
			get;
			set;
		}

		public RelayCommand<CustomEventCommandParameter> DropSpriteInPanelCommand {
			get;
			set;
		}

		public int SelectedPanelIndex {
			get => selectedPanelIndex;
			set => SetProperty(ref selectedPanelIndex, value);
		}

		public VariableManagerViewModel VariableManager {
			get => CurrentProject.VariableManagerViewModel;
			set => CurrentProject.VariableManagerViewModel = value;
		}

		public ObservableCollection<DataValue> Variables {
			get => VariableManager.Variables;
			set => VariableManager.Variables = value;
		}

		public ObservableCollection<SpriteViewModel> GlobalSprites {
			get => m_globalSprites;
			set => m_globalSprites = value;
		}
		#endregion


		public MainViewModel() {
			canExecute = true;
			currentProject = new Project();
			GlobalSprites = new ObservableCollection<SpriteViewModel>();
			AddBackgroundImageCommand = new RelayCommand(AddBackgroundImage);
			OpenVariableManagerCommand = new RelayCommand(OpenVariableManager);
			AddNewSpriteCommand = new RelayCommand(AddNewSprite);
			DropSpriteInPanelCommand = new RelayCommand<CustomEventCommandParameter>(DropSpriteInPanel);
#if DEBUG

			currentProject.Panels.Add(new Panel("NewPanel_01"));
			currentProject.Panels.Add(new Panel("NewPanel_02"));
			currentProject.Panels.Add(new Panel("NewPanel_03"));

			currentProject.SelectedPanel = currentProject.Panels[0];

			currentProject.SelectedPanel.DialogueLines.Add(new DialogueLine { CharacterName = $"Heinrich Meinrich", SpriteIndex = 0, TextShown = "Lorem Ipsum" });
			currentProject.SelectedPanel.DialogueLines.Add(new DialogueLine { CharacterName = $"dlwmvmd", SpriteIndex = 0, TextShown = "9ur409ut23409u9589023485" });
			currentProject.SelectedPanel.DialogueLines.Add(new DialogueLine { CharacterName = $"39rt md", SpriteIndex = 0, TextShown = " 03r dskngkdjfgkdm" });
			currentProject.SelectedPanel.DialogueLines.Add(new DialogueLine { CharacterName = $"FFFFFFFFFFFFFFFFF", SpriteIndex = 0, TextShown = "ffffffffffff" });

			currentProject.SelectedPanel.SelectedLine = currentProject.SelectedPanel.DialogueLines[0];

			string currentPath = Directory.GetCurrentDirectory();
			currentPath = Path.Combine(currentPath, @"..\..\");
			var ProjectDir = Path.GetFullPath(currentPath);
			currentPath = Path.Combine(ProjectDir, @"VisualNovelInterface\Resources\doge.png");

			currentProject.SelectedPanel.SelectedLine.Sprites.Add(new SpriteViewModel(currentPath, "DogeSprite1", 0, 0, 100, 100));
			GlobalSprites.Add(currentProject.SelectedPanel.SelectedLine.Sprites.Last());
			currentProject.SelectedPanel.SelectedLine.Sprites.Last().OnSpriteMoveEvent += MoveSprite;
			currentProject.SelectedPanel.SelectedLine.Sprites.Add(new SpriteViewModel(currentPath, "DogeSprite2", 100, 100, 100, 100));
			currentProject.SelectedPanel.SelectedLine.Sprites.Last().OnSpriteMoveEvent += MoveSprite;
#endif

			//IntPtr handle = DLLImporter.CreateDataValue_bool("newTrigger", true);
			//Console.WriteLine($"The Adress is: 0x{handle.ToString("X16")}");
			//bool value = DLLImporter.ReadDataValue_bool(handle);
			//Console.WriteLine($"The Value after start is: {value}");
			//DLLImporter.SetDataValue_bool(handle, false);
			//Console.WriteLine("The Value of the handle was set differently.");
			//bool value2 = DLLImporter.ReadDataValue_bool(handle);
			//Console.WriteLine($"The new Value is {value2}");
			//DLLImporter.FreeDataValue(handle);
			//Console.WriteLine("The new Value is was now set free");
			//DLLImporter.CreateDataValue_int("newVariable", 1, 3);
			//DLLImporter.CreateDataValue_float("newVariable", 1, 3.0f);
			//DLLImporter.CreateDataValue_string("newDecimal", 2, "dfvds");


		}

		public void AddBackgroundImage() {
			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
				newFile.Filter = "image files (*.png; *.jpg)|*.png; *.jpg";

				if (newFile.ShowDialog() == DialogResult.OK) {

					string path = newFile.FileName;
					Bitmap newImage = (Bitmap)Image.FromFile(path);
					SelectedPanel.BackgroundImage = newFile.FileName;
				}
			}
		}

		public void AddNewSprite() {

			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
				newFile.Filter = "image files (*.png; *.jpg)|*.png; *.jpg";

				if (newFile.ShowDialog() == DialogResult.OK) {

					string path = newFile.FileName;
					SpriteViewModel newSVM = new SpriteViewModel(path, newFile.SafeFileName, 0, 0, 100, 100);
					GlobalSprites.Add(newSVM);
				}
			}
		}

		public void OpenVariableManager() {
			VariableManagerWindow vm = new VariableManagerWindow();
			vm.DataContext = VariableManager;
			vm.Show();
		}

		public void OnSelectionChanged(object obj) {
			Console.Write("");
		}

		public void MoveSprite(SpriteViewModel _sender) {
			SelectedSprite = _sender;
		}

		public void DropSpriteInPanel(CustomEventCommandParameter _args) {
		
		}
	}
}
