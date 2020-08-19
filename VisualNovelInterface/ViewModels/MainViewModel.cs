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
using VisualNovelInterface.Views;
using VisualNovelInterface.Models.Enums;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using VisualNovelInterface.Models.Args.SenderEventArgs;
using System.Windows;
using System.IO;
using DragEventArgs = System.Windows.DragEventArgs;
using System.Windows.Controls;
using Image = System.Drawing.Image;
using FontFamily = System.Windows.Media.FontFamily;
using VisualNovelInterface.ProjectExport;

namespace VisualNovelInterface.ViewModels
{
	public class MainViewModel : BaseObject
	{
		private Project currentProject;
		private SpriteViewModel m_selectedSprite;
		private bool canExecute;
		private int selectedPanelIndex;
		private ObservableCollection<SpriteImage> m_globalSprites;
		private ObservableCollection<SpriteImage> m_globalButtonSprites;
		private Option m_selectedOption;
		private string m_exportPath;

		public delegate void ReorganizeOptionButtons(ObservableCollection<Option> m_options);
		public delegate void AutoPositionSprites(ObservableCollection<SpriteViewModel> m_options);
		public event ReorganizeOptionButtons OnReorganizeButtons;
		public event AutoPositionSprites OnAutoPositionSprites;

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
				SelectedBranch = SelectedPanel.Branches.First();
				OnPropertyChanged(nameof(SelectedPanel));
				OnPropertyChanged(nameof(SelectionPath));
			}
		}

		public Branch SelectedBranch {
			get => SelectedPanel.SelectedBranch;
			set {
				SelectedPanel.SelectedBranch = value;
				SelectedItem = SelectedBranch.ShownItems.First();
				OnPropertyChanged(nameof(SelectedBranch));
				OnPropertyChanged(nameof(SelectionPath));
			}
		}

		public ShownItem SelectedItem {
			get => SelectedBranch.SelectedItem;
			set {
				SelectedBranch.SelectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));
				OnPropertyChanged(nameof(SelectionPath));
				OnPropertyChanged(nameof(IsSelectedItemDialogLine));
				OnPropertyChanged(nameof(IsSelectedItemContinue));
				if (!(SelectedBranch.SelectedItem is Continue)) {
					SelectedOption = null;
				} else {
					if(((Continue)SelectedBranch.SelectedItem).Type == ContinueTypeEnum.Split)
					RefreshButtons();
				}
			}
		}

		public Option SelectedOption {
			get => m_selectedOption;
			set => SetProperty(ref m_selectedOption, value);
		}

		public FontFamily SelectedFont {
			get => FontManager.CurrentUsedFont;
			set => FontManager.CurrentUsedFont = value;
		}

		public int SelectedPanelIndex {
			get => selectedPanelIndex;
			set => SetProperty(ref selectedPanelIndex, value);
		}

		public VariableManagerViewModel VariableManager {
			get => CurrentProject.VariableManagerViewModel;
			set => CurrentProject.VariableManagerViewModel = value;
		}

		public FontManagerViewModel FontManager {
			get => CurrentProject.FontManagerViewModel;
			set => CurrentProject.FontManagerViewModel = value;
		}

		public ObservableCollection<DataValue> Variables {
			get => VariableManager.Variables;
			set => VariableManager.Variables = value;
		}

		public ObservableCollection<SpriteImage> GlobalSprites {
			get => m_globalSprites;
			set => m_globalSprites = value;
		}

		public ObservableCollection<SpriteImage> GlobalButtonSprites {
			get => m_globalButtonSprites;
			set => m_globalButtonSprites = value;
		}

		public string SelectionPath {
			get {
				string itemName = "Error";
				if (SelectedItem is DialogLine) {
					itemName = ((DialogLine)SelectedItem).TextShown;
				} else if (SelectedItem is Split) {
					itemName = ((Split)SelectedItem).Name;
				}
				return $"{SelectedPanel.PanelName} => {SelectedBranch.Name} => {itemName}";
			}
		}

		public bool IsSelectedItemDialogLine {
			get => SelectedItem is DialogLine;
		}
		public bool IsSelectedItemContinue {
			get => SelectedItem is Continue;
		}

		#endregion

		#region Commands
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

		public RelayCommand AddNewOptionCommand {
			get;
			set;
		}

		public RelayCommand RemoveSelectedOptionCommand {
			get;
			set;
		}

		public RelayCommand OpenFontConfigWindowCommand {
			get;
			set;
		}

		public RelayCommand SetOutputDirectoryCommand {
			get;
			set;
		}

		public RelayCommand RunExportCommand {
			get;
			set;
		}

		#endregion

		public MainViewModel() {
			canExecute = true;
			currentProject = new Project();
			GlobalSprites = new ObservableCollection<SpriteImage>();
			GlobalButtonSprites = new ObservableCollection<SpriteImage>();
			AddBackgroundImageCommand = new RelayCommand(AddBackgroundImage);
			OpenVariableManagerCommand = new RelayCommand(OpenVariableManager);
			AddNewSpriteCommand = new RelayCommand(AddNewSprite);
			AddNewOptionCommand = new RelayCommand(AddNewOption);
			RemoveSelectedOptionCommand = new RelayCommand(RemoveSelectedOption);
			DropSpriteInPanelCommand = new RelayCommand<CustomEventCommandParameter>(DropSpriteInPanel);
			OpenFontConfigWindowCommand = new RelayCommand(OpenFontConfigWindow);
			SetOutputDirectoryCommand = new RelayCommand(SetOutputDirectory);
			RunExportCommand = new RelayCommand(RunExport);
			
#if DEBUG
			GenerateTestStory();
			OnPropertyChanged(nameof(SelectedItem));
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

		public void RefreshButtons() {
			OnReorganizeButtons.Invoke(((Continue)SelectedItem).Split.Options);
		}

		public void TriggerAutoPositionSprites() {
			OnAutoPositionSprites.Invoke(((DialogLine)SelectedItem).Sprites);
		}

		public void AddBackgroundImage() {
			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = Directory.GetCurrentDirectory();
				newFile.Filter = "image files (*.png; *.jpg)|*.png; *.jpg";

				if (newFile.ShowDialog() == DialogResult.OK) {

					string path = newFile.FileName;
					Bitmap newImage = (Bitmap)Image.FromFile(path);
					SelectedPanel.BackgroundImage = new SpriteImage(newFile.FileName, newFile.SafeFileName);
					GlobalSprites.Add(SelectedPanel.BackgroundImage);
				}
			}
		}

		public void AddNewSprite() {

			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = Directory.GetCurrentDirectory();
				newFile.Filter = "image files (*.png; *.jpg)|*.png; *.jpg";

				if (newFile.ShowDialog() == DialogResult.OK) {

					string path = newFile.FileName;
					SpriteImage newSVM = new SpriteImage(path, newFile.SafeFileName);
					GlobalSprites.Add(newSVM);
				}
			}
		}

		public void OpenVariableManager() {
			VariableManagerWindow vm = new VariableManagerWindow();
			vm.DataContext = VariableManager;
			vm.Show();
		}

		public void MoveSprite(SpriteViewModel _sender) {
			SelectedSprite = _sender;
		}

		public void DropSpriteInPanel(CustomEventCommandParameter _args) {
			DragEventArgs dea = (DragEventArgs) _args.Args;
			string[] formats = dea.Data.GetFormats();
			SpriteImage Sprite = (SpriteImage)dea.Data.GetData(formats.First());
			FrameworkElement canvas = (Canvas)dea.OriginalSource;
			System.Windows.Point coordinates = dea.GetPosition(canvas);

			currentProject.SelectedPanel.SelectedLine.Sprites.Add(new SpriteViewModel(Sprite, (int)coordinates.X, coordinates.Y));
			currentProject.SelectedPanel.SelectedLine.Sprites.Last().OnSpriteMoveEvent += MoveSprite;
			SelectedSprite = currentProject.SelectedPanel.SelectedLine.Sprites.Last();
		}

		public void AddNewOption() {
			SpriteImage newSprite = new SpriteImage("ButtonSpriteState.png", "ButtonSprite");
			Continue newContinue = new Continue(ContinueTypeEnum.Branch, SelectedPanel.Branches.First().Name);
			((Continue)SelectedItem).Split.Options.Add(new Option("NewOption", "New Option", newSprite, newContinue));
			RefreshButtons();
		}

		public void RemoveSelectedOption() {
			if (SelectedOption != null) {
				if (((Continue)SelectedItem).Split.Options.Count < 2) {
					System.Windows.MessageBox.Show("A split cannot have less than 1 option!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				RefreshButtons();
				((Continue)SelectedItem).Split.Options.Remove(SelectedOption);
			}
		}

		public void OpenFontConfigWindow() {
			FontConfigWindow fcw = new FontConfigWindow();
			fcw.DataContext = FontManager;
			fcw.Show();
		}

		public void SetEntryBranch(Branch _branch) {

			SelectedPanel.Branches.Single(x => x.Name == SelectedPanel.EntryBranchKey).IsEntryBranch = false;
			_branch.IsEntryBranch = true;
			SelectedPanel.EntryBranchKey = _branch.Name;
		}

		public void SetOutputDirectory() {
			using (FolderBrowserDialog dialog = new FolderBrowserDialog()) {
				dialog.RootFolder = Environment.SpecialFolder.MyComputer;
				dialog.SelectedPath = Directory.GetCurrentDirectory();
				dialog.Description = "Choose an output directory";
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK) {
					m_exportPath = dialog.SelectedPath;
				}
			}
		}

		public void RunExport() {

			Exporter exporter = new Exporter(m_exportPath);
			SpriteExporter spriteExporter = new SpriteExporter(m_globalSprites.ToList(), m_globalButtonSprites.ToList());
			bool result = exporter.Export(currentProject, spriteExporter);
			if (result) {
				System.Windows.Forms.MessageBox.Show("Export successfull", "Success", MessageBoxButtons.OK);
			} else {
				System.Windows.Forms.MessageBox.Show("Export failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void GenerateTestStory() {

			currentProject.Panels.Add(new Panel("NewPanel_01"));
			//currentProject.Panels.Add(new Panel("NewPanel_02"));
			//currentProject.Panels.Add(new Panel("NewPanel_03"));
			currentProject.Panels.First().Branches.Clear();	//TODO: Why clear???

			currentProject.SelectedPanel = currentProject.Panels[0];

			string curDir = Directory.GetCurrentDirectory();
			string fullPath = Path.Combine(curDir,"ButtonSpriteState.png");
			SpriteImage buttonSprite = new SpriteImage(fullPath, "ButtonSprite");
			SpriteImage dogeSprite = new SpriteImage("doge.png", "DogeSprite");
			SpriteImage stickSprite = new SpriteImage("StickFigure.png", "StickSprite");
			SpriteImage goblinSprite = new SpriteImage("GoblinMage.png", "GoblinSprite");
			GlobalSprites.Add(dogeSprite);
			GlobalSprites.Add(stickSprite);
			GlobalSprites.Add(goblinSprite);
			GlobalButtonSprites.Add(buttonSprite);
			SpriteViewModel dogeSVM = new SpriteViewModel(dogeSprite);
			SpriteViewModel stickSVM = new SpriteViewModel(stickSprite);
			SpriteViewModel goblinSVM = new SpriteViewModel(goblinSprite);

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch1",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Narrator", TextShown = "This is the first string."},
															new DialogLine{ CharacterName = "", TextShown = "This is the second text shown."},
														},
														new Continue(
															ContinueTypeEnum.Split,
															new Split("Split1",
																new ObservableCollection<Option>() {
																	new Option("Option1", "Doge first", buttonSprite, new Continue(ContinueTypeEnum.Branch, "Branch 2")),
																	new Option("Option2", "Heinrich first", buttonSprite, new Continue(ContinueTypeEnum.Branch, "Branch 3"))
																}
															)
														)));

			SelectedBranch = SelectedPanel.Branches.First();

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch2",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Doge", TextShown = "I Am Doge", Sprites = new ObservableCollection<SpriteViewModel>(){dogeSVM}},
															new DialogLine{ CharacterName = "Heinrich Kleinrich", TextShown = "And I am Heinrich", Sprites = new ObservableCollection<SpriteViewModel>(){dogeSVM, stickSVM}},
														},
														new Continue(ContinueTypeEnum.Branch, "Branch 4")));

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch3",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Heinrich Kleinrich", TextShown = "I am Heinrich", Sprites = new ObservableCollection<SpriteViewModel>(){stickSVM}},
															new DialogLine{ CharacterName = "Doge", TextShown = "And I Am Doge", Sprites = new ObservableCollection<SpriteViewModel>(){stickSVM, dogeSVM}},
														},
														new Continue(ContinueTypeEnum.Branch, "Branch 4")));

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch4",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "", TextShown = "This is the second split"},
														},
														new Continue(
															ContinueTypeEnum.Split,
															new Split("Split2",
																new ObservableCollection<Option>() {
																	new Option("Option3", "Repeat Doge", buttonSprite, new Continue(ContinueTypeEnum.Branch, "Branch 5")),
																	new Option("Option4", "Repeat Heinrich", buttonSprite, new Continue(ContinueTypeEnum.Branch, "Branch 6"))
																}
															)
														)));

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch5",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Doge", TextShown = "Hello there", Sprites = new ObservableCollection<SpriteViewModel>(){dogeSVM}},
														},
														new Continue(ContinueTypeEnum.Branch, "Branch 7")));

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch6",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Heinrich Kleinrich", TextShown = "General Ke-dogee", Sprites = new ObservableCollection<SpriteViewModel>(){stickSVM}},
														},
														new Continue(ContinueTypeEnum.Branch, "Branch 7")));

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch7",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Goblin Doctor", TextShown = "Hello", Sprites = new ObservableCollection<SpriteViewModel>(){goblinSVM}},
														},
														new Continue(ContinueTypeEnum.Panel, "Panel 2")));
			foreach (Branch branch in SelectedPanel.Branches) {

				branch.OnEntryBranchChange += SetEntryBranch;
			}

			SelectedPanel.EntryBranchKey = SelectedPanel.Branches.First().Name;
			SelectedBranch.IsEntryBranch = true;
			SelectedBranch.CallEntryBranchTrigger();
			
			string currentPath = Directory.GetCurrentDirectory();
			currentPath = Path.Combine(currentPath, @"..\..\");
			var ProjectDir = Path.GetFullPath(currentPath);
			currentPath = Path.Combine(ProjectDir, @"VisualNovelInterface\Resources\fonts\");

			FontFamily newFont = new FontFamily(new Uri(currentPath), "PAPYRUS");
			//FontFamily newFont = new FontFamily(new Uri(currentPath), "OpenSans-Regular");
			currentProject.FontManagerViewModel.Fonts.Add(newFont);
			currentProject.FontManagerViewModel.CurrentUsedFont = currentProject.FontManagerViewModel.Fonts.First();
			//GlobalSprites.Add(new SpriteImage(currentPath, "DogeSprite"));
			//currentProject.SelectedPanel.SelectedLine.Sprites.Add(new SpriteViewModel(GlobalSprites.First()));
			//currentProject.SelectedPanel.SelectedLine.Sprites.Last().OnSpriteMoveEvent += MoveSprite;
			//currentProject.SelectedPanel.SelectedLine.Sprites.Add(new SpriteViewModel(GlobalSprites.First()));
			//currentProject.SelectedPanel.SelectedLine.Sprites.Last().OnSpriteMoveEvent += MoveSprite;
		}
	}
}
