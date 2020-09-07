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
using System.Text.RegularExpressions;
using System.Windows.Media;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace VisualNovelInterface.ViewModels
{
	public class MainViewModel : BaseObject
	{
		private Project currentProject;
		private SpriteViewModel m_selectedSprite;
		private SpriteImage m_selectedGlobalSprite;
		private SpriteImage m_selectedGlobalButtonSprite;
		private bool canExecute;
		private int selectedPanelIndex;

		private Option m_selectedOption;
		private string m_exportPath;
		private static readonly Regex m_integerRegex = new Regex("[^0-9]+");
		private Window m_parentWindow;
		private int m_selectedTabIndex;

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
				if (m_selectedSprite != null) {
					m_selectedSprite.IsSelected = true;
				}
				OnPropertyChanged(nameof(IsSpriteSelected));
			}
		}

		public SpriteImage SelectedGlobalSprite {
			get => m_selectedGlobalSprite;
			set => SetProperty(ref m_selectedGlobalSprite, value);
		}

		public SpriteImage SelectedGlobalButtonSprite {
			get => m_selectedGlobalButtonSprite;
			set => SetProperty(ref m_selectedGlobalButtonSprite, value);
		}

		public Panel SelectedPanel {
			get => currentProject.SelectedPanel;
			set {
				currentProject.SelectedPanel = value;
				if (SelectedPanel != null) {
					SelectedBranch = SelectedPanel.Branches.First();
				}
				SelectedSprite = null;
				OnPropertyChanged(nameof(SelectedPanel));
				OnPropertyChanged(nameof(SelectionPath));
			}
		}

		public Branch SelectedBranch {
			get => SelectedPanel != null ? SelectedPanel.SelectedBranch : null;
			set {
				if (SelectedPanel != null) {
					SelectedPanel.SelectedBranch = value;
				}
				if (SelectedBranch != null) {
					SelectedItem = SelectedBranch.ShownItems.First();
				}
				SelectedSprite = null;
				OnPropertyChanged(nameof(SelectedBranch));
				OnPropertyChanged(nameof(SelectionPath));
			}
		}

		public ShownItem SelectedItem {
			get => SelectedBranch != null ? SelectedBranch.SelectedItem : null;
			set {
				if (SelectedBranch != null) {
					SelectedBranch.SelectedItem = value;
					SelectedSprite = null;
					OnPropertyChanged(nameof(SelectedItem));
					OnPropertyChanged(nameof(SelectionPath));
					OnPropertyChanged(nameof(IsSelectedItemDialogLine));
					OnPropertyChanged(nameof(IsSelectedItemContinue));
					if (!(SelectedBranch.SelectedItem is Continue)) {
						SelectedOption = null;
					} else {
						if (((Continue)SelectedBranch.SelectedItem).Type == ContinueTypeEnum.Split)
							RefreshButtons();
					}
				}
			}
		}

		public Option SelectedOption {
			get => m_selectedOption;
			set => SetProperty(ref m_selectedOption, value);
		}

		public ProjectFont CurrentUsedProjectFont {
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



		public string SelectionPath {
			get {
				string itemName = "Error";
				if (SelectedItem is DialogLine) {
					itemName = ((DialogLine)SelectedItem).TextShown;
				} else if (SelectedItem is Split) {
					itemName = ((Split)SelectedItem).Name;
				}
				string Panelname = SelectedPanel != null ? SelectedPanel.PanelName : "Error";
				string Branchlname = SelectedBranch != null ? SelectedBranch.Name : "Error";
				return $"{Panelname} => {Branchlname} => {itemName}";
			}
		}

		public bool IsSelectedItemDialogLine {
			get => SelectedItem is DialogLine;
		}
		public bool IsSelectedItemContinue {
			get => SelectedItem is Continue;
		}

		public bool IsPanelSelected {
			get => SelectedPanel != null;
		}

		public bool IsBranchSelected {
			get => SelectedBranch != null;
		}

		public bool IsItemSelected {
			get => SelectedItem != null;
		}


		public bool IsItemsTabSelected {
			get => SelectedTabIndex == 2;
		}

		public bool IsSpriteSelected {
			get => SelectedSprite != null;
		}

		public double NameTextBoxWidth {
			get => LineTextBoxWidth / 6;
		}

		public double LineTextBoxWidth {
			get => currentProject.ProjectSettingsViewModel.WindowWidth - 50;
		}

		public double NameTextBoxHeight {
			get => LineTextBoxHeight / 4;
		}

		public double LineTextBoxHeight {
			get => currentProject.ProjectSettingsViewModel.WindowHeight / 4;
		}

		public int SelectedTabIndex {
			get => m_selectedTabIndex;
			set {
				SetProperty(ref m_selectedTabIndex, value);
				OnPropertyChanged(nameof(IsItemsTabSelected));
			}
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

		public RelayCommand AddNewPanelCommand {
			get;
			set;
		}

		public RelayCommand RemoveSelectedPanelCommand {
			get;
			set;
		}

		public RelayCommand AddNewBranchCommand {
			get;
			set;
		}

		public RelayCommand RemoveSelectedBranchCommand {
			get;
			set;
		}

		public RelayCommand AddNewItemCommand {
			get;
			set;
		}

		public RelayCommand RemoveSelectedItemCommand {
			get;
			set;
		}

		public RelayCommand MoveSelectedShownItemUpCommand {
			get;
			set;
		}

		public RelayCommand MoveSelectedShownItemDownCommand {
			get;
			set;
		}

		public RelayCommand<TextCompositionEventArgs> CheckInputForIntegerCommand {
			get;
			set;
		}

		public RelayCommand OpenProjectSettingsCommand {
			get;
			set;
		}

		public RelayCommand OpenFullsizePreviewCommand {
			get;
			set;
		}

		public RelayCommand RemoveSelectedSpriteCommand {
			get;
			set;
		}

		public RelayCommand RemoveSelectedGlobalSpriteCommand {
			get;
			set;
		}

		public RelayCommand AddNewGlobalButtonSpriteCommand {
			get;
			set;
		}

		public RelayCommand RemoveSelectedGlobalButtonSpriteCommand {
			get;
			set;
		}

		public RelayCommand NewProjectCommand {
			get;
			set;
		}

		public RelayCommand SaveCurrentProjectCommand {
			get;
			set;
		}

		public RelayCommand LoadCurrentProjectCommand {
			get;
			set;
		}
		#endregion

		public MainViewModel(Window _parentWindow) {
			canExecute = true;
			m_parentWindow = _parentWindow;
			currentProject = new Project();
			currentProject.FontManagerViewModel.OnNewUsedFont += NotifyOfChangedUsedFont;

			AddBackgroundImageCommand = new RelayCommand(AddBackgroundImage);
			OpenVariableManagerCommand = new RelayCommand(OpenVariableManager);
			AddNewSpriteCommand = new RelayCommand(AddNewSprite);
			AddNewOptionCommand = new RelayCommand(AddNewOption);
			RemoveSelectedOptionCommand = new RelayCommand(RemoveSelectedOption);
			DropSpriteInPanelCommand = new RelayCommand<CustomEventCommandParameter>(DropSpriteInPanel);
			OpenFontConfigWindowCommand = new RelayCommand(OpenFontConfigWindow);
			SetOutputDirectoryCommand = new RelayCommand(SetOutputDirectory);
			RunExportCommand = new RelayCommand(RunExport);
			CheckInputForIntegerCommand = new RelayCommand<TextCompositionEventArgs>(CheckInputForInteger);
			AddNewPanelCommand = new RelayCommand(AddNewPanel);
			RemoveSelectedPanelCommand = new RelayCommand(RemoveSelectedPanel);
			AddNewBranchCommand = new RelayCommand(AddNewBranch);
			RemoveSelectedBranchCommand = new RelayCommand(RemoveSelectedBranch);
			RemoveSelectedSpriteCommand = new RelayCommand(RemoveSelectedSprite);
			AddNewItemCommand = new RelayCommand(AddNewItem);
			RemoveSelectedItemCommand = new RelayCommand(RemoveSelectedItem);
			MoveSelectedShownItemUpCommand = new RelayCommand(MoveSelectedShownItemUp);
			MoveSelectedShownItemDownCommand = new RelayCommand(MoveSelectedShownItemDown);
			OpenProjectSettingsCommand = new RelayCommand(OpenProjectSettings);
			OpenFullsizePreviewCommand = new RelayCommand(OpenFullsizePreview);
			RemoveSelectedGlobalSpriteCommand = new RelayCommand(RemoveSelectedGlobalSprite);
			AddNewGlobalButtonSpriteCommand = new RelayCommand(AddNewGlobalButtonSprite);
			RemoveSelectedGlobalButtonSpriteCommand = new RelayCommand(RemoveSelectedGlobalButtonSprite);
			NewProjectCommand = new RelayCommand(NewProject);
			SaveCurrentProjectCommand = new RelayCommand(SaveCurrentProject);
			LoadCurrentProjectCommand = new RelayCommand(LoadCurrentProject);
#if DEBUG
			//GenerateTestStory();
			//OnPropertyChanged(nameof(SelectedItem));
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
				newFile.Title = "Add new background image";

				if (newFile.ShowDialog() == DialogResult.OK) {

					SelectedPanel.BackgroundImage = new SpriteImage(newFile.FileName, newFile.SafeFileName);
					CurrentProject.GlobalSprites.Add(SelectedPanel.BackgroundImage);
				}
			}
		}

		public void AddNewSprite() {

			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = Directory.GetCurrentDirectory();
				newFile.Filter = "image files (*.png; *.jpg)|*.png; *.jpg";
				newFile.Title = "Add new sprite";

				if (newFile.ShowDialog() == DialogResult.OK) {

					string path = newFile.FileName;
					SpriteImage newSVM = new SpriteImage(path, newFile.SafeFileName);
					CurrentProject.GlobalSprites.Add(newSVM);
				}
			}
		}

		public void OpenVariableManager() {
			VariableManagerWindow vm = new VariableManagerWindow();
			vm.DataContext = VariableManager;
			vm.Owner = m_parentWindow;
			vm.Show();
		}

		public void MoveSprite(SpriteViewModel _sender) {
			SelectedSprite = _sender;
		}

		public void DropSpriteInPanel(CustomEventCommandParameter _args) {
			DialogLine selectedLine = SelectedBranch.SelectedItem as DialogLine;
			if (selectedLine != null) {

				DragEventArgs dea = (DragEventArgs) _args.Args;
				string[] formats = dea.Data.GetFormats();
				SpriteImage Sprite = (SpriteImage)dea.Data.GetData(formats.First());
				FrameworkElement canvas = (Canvas)dea.OriginalSource;
				System.Windows.Point coordinates = dea.GetPosition(canvas);

				SpriteViewModel newSpriteVM = new SpriteViewModel(Sprite, (int)coordinates.X, coordinates.Y);
				bool exists = SelectedPanel.SpriteImages.ContainsKey(newSpriteVM.SpriteImage.Id);
				if (!exists) {
					SelectedPanel.SpriteImages.Add(newSpriteVM.SpriteImage.Id, newSpriteVM.SpriteImage);
				}
				newSpriteVM.OnSpriteMoveEvent += MoveSprite;
				selectedLine.Sprites.Add(newSpriteVM);
				SelectedSprite = newSpriteVM;
			}
		}

		public void AddNewOption() {
			SpriteImage newSprite = new SpriteImage("ButtonSpriteState.png", "ButtonSprite");
			Continue newContinue = new Continue(ContinueTypeEnum.Branch, SelectedPanel.Branches.First().Name);
			Option newOption = new Option("NewOption", "New Option", newSprite, newContinue);
			newOption.OnButtonSpriteChange += OpenButtonSpriteDialog;
			((Continue)SelectedItem).Split.Options.Add(newOption);
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
			fcw.Owner = m_parentWindow;
			fcw.Show();
		}

		public void OpenProjectSettings() {
			ProjectSettingsWindow psw = new ProjectSettingsWindow();
			psw.DataContext = currentProject.ProjectSettingsViewModel;
			psw.Owner = m_parentWindow;
			psw.Show();
		}

		public void OpenFullsizePreview() {
			FullsizePreviewWindow fsw = new FullsizePreviewWindow();
			fsw.DataContext = this;
			fsw.Owner = m_parentWindow;
			fsw.Show();
		}

		public bool OpenButtonSpriteDialog() {
			ChooseButtonSpirteDialog dialog = new ChooseButtonSpirteDialog();
			dialog.DataContext = this;

			if (dialog.ShowDialog() == true) {
				SelectedOption.ButtonSprite = dialog.SpriteChoice;
				return true;
			}
			return false;
		}

		public void EntryBranchChecked(Branch _branch) {

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

		public void CheckInputForInteger(TextCompositionEventArgs e) {

		}

		public void AddNewPanel() {
			CurrentProject.Panels.Add(new Panel("NewPanel", OpenButtonSpriteDialog));
			CurrentProject.Panels.First().Branches.First().SetEntryBranchEventHandler += EntryBranchChecked;
			if (CurrentProject.Panels.Count < 2) {
				OnPropertyChanged(nameof(SelectedPanel));
				OnPropertyChanged(nameof(IsPanelSelected));
			}
		}

		public void RemoveSelectedPanel() {
			if (SelectedPanel != null) {
				if (CurrentProject.Panels.Count <= 1) {
					System.Windows.Forms.MessageBox.Show("Cannot Remove Last Panel! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				} else {
					CurrentProject.Panels.Remove(SelectedPanel);
					if (CurrentProject.Panels.Count > 0) {
						SelectedPanel = CurrentProject.Panels.First();
					} else {
						SelectedPanel = null;
					}
				}
			}
		}

		public void AddNewBranch() {
			Branch newBranch = new Branch("NewBranch");
			newBranch.SetEntryBranchEventHandler += EntryBranchChecked;
			newBranch.Continue.OpenButtonSpriteDialogReference = OpenButtonSpriteDialog;
			SelectedPanel.Branches.Add(newBranch);

		}

		public void RemoveSelectedBranch() {
			if (SelectedBranch != null) {
				if (SelectedPanel.Branches.Count <= 1) {
					System.Windows.Forms.MessageBox.Show("Cannot Remove Last Branch! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				} else {
					Branch tobeRemovedBranch = SelectedBranch;
					SelectedBranch = SelectedPanel.Branches.First();
					SelectedPanel.Branches.Remove(tobeRemovedBranch);
					if (SelectedPanel.Branches.Count > 0) {
						SelectedBranch = SelectedPanel.Branches.First();
					} else {
						SelectedBranch = null;
					}
				}
			}
		}

		public void RemoveSelectedSprite() {
			if (SelectedSprite != null) {
				DialogLine line = (DialogLine)SelectedItem;
				line.Sprites.Remove(SelectedSprite);
			}
		}

		public void AddNewItem() {
			SelectedBranch.ShownItems.Add(new DialogLine() { CharacterName = "New Name", TextShown = "New Text" });
			SelectedBranch.NotifyShownItemsListChange();
		}

		public void RemoveSelectedItem() {
			if (SelectedItem != null &&
				!(SelectedItem is Split)) {
				SelectedBranch.ShownItems.Remove(SelectedItem);
				SelectedBranch.NotifyShownItemsListChange();
				if (SelectedBranch.ShownItems.Count > 0) {
					SelectedItem = SelectedBranch.ShownItems.First();
				} else {
					SelectedItem = null;
				}
			}
		}

		public void MoveSelectedShownItemUp() {
			int index = SelectedBranch.ShownItems.IndexOf(SelectedItem);
			if (index > 0) {    //Iff the Item is not the first in the list
				ShownItem item = SelectedItem;
				SelectedBranch.ShownItems.Remove(SelectedItem);
				SelectedBranch.ShownItems.Insert(index - 1, item);
				SelectedItem = SelectedBranch.ShownItems[index - 1];
			}
		}

		public void MoveSelectedShownItemDown() {
			int index = SelectedBranch.ShownItems.IndexOf(SelectedItem);
			if (index < SelectedBranch.ShownItems.Count - 1) {    //Iff the Item is not the first in the list
				ShownItem item = SelectedItem;
				SelectedBranch.ShownItems.Remove(SelectedItem);
				SelectedBranch.ShownItems.Insert(index + 1, item);
				SelectedItem = SelectedBranch.ShownItems[index + 1];
			}
		}

		public void RunExport() {

			ExportWizardDialog ewd = new ExportWizardDialog();
			ewd.DataContext = this;
			if (ewd.ShowDialog() == true) {

				Exporter exporter = new Exporter(m_exportPath, ewd.GameName);
				SpriteExporter spriteExporter = new SpriteExporter(CurrentProject.GlobalSprites.ToList(), CurrentProject.GlobalButtonSprites.ToList());
				bool result = exporter.Export(currentProject, spriteExporter, ewd.MainMenuBackgroundImage);
				if (result) {
					System.Windows.Forms.MessageBox.Show("Export successfull", "Success", MessageBoxButtons.OK);
				}
			}
		}

		public void NotifyOfChangedUsedFont() {
			OnPropertyChanged(nameof(CurrentUsedProjectFont));
		}

		public void RemoveSelectedGlobalSprite() {
			CurrentProject.GlobalSprites.Remove(SelectedGlobalSprite);
		}

		public void AddNewGlobalButtonSprite() {

			using (OpenFileDialog newFile = new OpenFileDialog()) {

				newFile.InitialDirectory = Directory.GetCurrentDirectory();
				newFile.Filter = "image files (*.png; *.jpg)|*.png; *.jpg";
				newFile.Title = "Choose new button sprite";

				if (newFile.ShowDialog() == DialogResult.OK) {

					string path = newFile.FileName;
					SpriteImage newSVM = new SpriteImage(path, newFile.SafeFileName);
					CurrentProject.GlobalButtonSprites.Add(newSVM);
				}
			}
		}

		public void RemoveSelectedGlobalButtonSprite() {
			CurrentProject.GlobalButtonSprites.Remove(SelectedGlobalButtonSprite);
		}

		public void NewProject() {
			CurrentProject.Panels = new ObservableCollection<Panel>();
			CurrentProject = new Project();
			CurrentProject.FontManagerViewModel.OnNewUsedFont += NotifyOfChangedUsedFont;
		}

		public void SaveCurrentProject() {
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Title = "Save current project";
			dialog.Filter = "Project Save (*.vnproj)|*.vnproj";
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK) {

				Project.Serialize(CurrentProject, dialog.FileName);
			}
		}

		public void LoadCurrentProject() {
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Title = "Open saved project";
			dialog.Filter = "Project Save (*.vnproj)|*.vnproj";
			if (dialog.ShowDialog() == DialogResult.OK) {

				Project.Deserialize(out currentProject, dialog.FileName, MoveSprite, OpenButtonSpriteDialog, EntryBranchChecked);
				CurrentProject.FontManagerViewModel.OnNewUsedFont += NotifyOfChangedUsedFont;
				OnPropertyChanged("");
			}
		}

#if DEBUG
		public void GenerateTestStory() {

			currentProject.Panels.Add(new Panel("NewPanel_01", OpenButtonSpriteDialog));
			//currentProject.Panels.Add(new Panel("NewPanel_02"));
			//currentProject.Panels.Add(new Panel("NewPanel_03"));
			currentProject.Panels.First().Branches.Clear(); //TODO: Why clear???

			currentProject.SelectedPanel = currentProject.Panels[0];

			string curDir = Directory.GetCurrentDirectory();
			curDir = Path.Combine(curDir, "Resources", "images");
			string fullPath = Path.Combine(curDir,"ButtonSpriteState.png");
			SpriteImage buttonSprite = new SpriteImage(fullPath, "ButtonSprite");
			fullPath = Path.Combine(curDir, "ButtonSpriteStateColor.png");
			SpriteImage buttonColorSprite = new SpriteImage(fullPath, "ButtonSpriteColor");
			fullPath = Path.Combine(curDir, "wallpaper.jpg");
			SpriteImage wallpaperSprite = new SpriteImage(fullPath, "wallpaper");
			fullPath = Path.Combine(curDir, "doge.png");
			SpriteImage dogeSprite = new SpriteImage(fullPath, "DogeSprite");
			fullPath = Path.Combine(curDir, "StickFigure.png");
			SpriteImage stickSprite = new SpriteImage(fullPath, "StickSprite");
			fullPath = Path.Combine(curDir, "GoblinMage.png");
			SpriteImage goblinSprite = new SpriteImage(fullPath, "GoblinSprite");
			CurrentProject.GlobalSprites.Add(dogeSprite);

			bool exists = SelectedPanel.SpriteImages.ContainsKey(dogeSprite.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(dogeSprite.Id, dogeSprite);
			}

			CurrentProject.GlobalSprites.Add(stickSprite);
			exists = SelectedPanel.SpriteImages.ContainsKey(stickSprite.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(stickSprite.Id, stickSprite);
			}

			CurrentProject.GlobalSprites.Add(goblinSprite);
			exists = SelectedPanel.SpriteImages.ContainsKey(goblinSprite.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(goblinSprite.Id, goblinSprite);
			}

			CurrentProject.GlobalButtonSprites.Add(buttonSprite);
			CurrentProject.GlobalButtonSprites.Add(buttonColorSprite);

			SelectedPanel.BackgroundImage = wallpaperSprite;

			SpriteViewModel dogeSVM = new SpriteViewModel(dogeSprite);
			dogeSVM.OnSpriteMoveEvent += MoveSprite;
			dogeSVM.PosX = 320;
			dogeSVM.PosY = 180;
			SpriteViewModel stickSVM = new SpriteViewModel(stickSprite);
			stickSVM.OnSpriteMoveEvent += MoveSprite;
			SpriteViewModel goblinSVM = new SpriteViewModel(goblinSprite);
			goblinSVM.OnSpriteMoveEvent += MoveSprite;
			SpriteViewModel goblinSVM2 = new SpriteViewModel(goblinSprite);
			goblinSVM2.OnSpriteMoveEvent += MoveSprite;

			Option newOption = new Option("Option1", "Doge first", buttonSprite, new Continue(ContinueTypeEnum.Branch, "Branch2"));
			newOption.OnButtonSpriteChange += OpenButtonSpriteDialog;
			Option newOption2 = new Option("Option2", "Heinrich first", buttonSprite, new Continue(ContinueTypeEnum.Branch, "Branch3"));
			newOption2.OnButtonSpriteChange += OpenButtonSpriteDialog;

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch1",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Narrator", TextShown = "This is the first string."},
															new DialogLine{ CharacterName = "", TextShown = "This is the second text shown."},
														},
														new Continue(
															ContinueTypeEnum.Split,
															new Split("Split1",
																new ObservableCollection<Option>() {
																	newOption,
																	newOption2
																}
															)
														)));

			SelectedBranch = SelectedPanel.Branches.First();
			SelectedBranch.IsEntryBranch = true;

			SpriteViewModel dogeSVM2 = new SpriteViewModel(dogeSprite);
			dogeSVM2.OnSpriteMoveEvent += MoveSprite;
			exists = SelectedPanel.SpriteImages.ContainsKey(dogeSVM2.SpriteImage.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(dogeSVM2.SpriteImage.Id, dogeSVM2.SpriteImage);
			}
			SpriteViewModel stickSVM2 = new SpriteViewModel(stickSprite);
			stickSVM2.OnSpriteMoveEvent += MoveSprite;
			exists = SelectedPanel.SpriteImages.ContainsKey(stickSVM2.SpriteImage.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(stickSVM2.SpriteImage.Id, stickSVM2.SpriteImage);
			}

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch2",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Doge", TextShown = "I Am Doge", Sprites = new ObservableCollection<SpriteViewModel>(){dogeSVM}},
															new DialogLine{ CharacterName = "Heinrich Kleinrich", TextShown = "And I am Heinrich", Sprites = new ObservableCollection<SpriteViewModel>(){dogeSVM2, stickSVM}},
														},
														new Continue(ContinueTypeEnum.Branch, "Branch4")));
			currentProject.SelectedPanel.Branches.Last().Continue.OpenButtonSpriteDialogReference = OpenButtonSpriteDialog;

			SpriteViewModel dogeSVM3 = new SpriteViewModel(dogeSprite);
			dogeSVM3.OnSpriteMoveEvent += MoveSprite;
			exists = SelectedPanel.SpriteImages.ContainsKey(dogeSVM3.SpriteImage.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(dogeSVM3.SpriteImage.Id, dogeSVM3.SpriteImage);
			}

			SpriteViewModel dogeSVM4 = new SpriteViewModel(dogeSprite);
			dogeSVM4.OnSpriteMoveEvent += MoveSprite;
			exists = SelectedPanel.SpriteImages.ContainsKey(dogeSVM4.SpriteImage.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(dogeSVM4.SpriteImage.Id, dogeSVM4.SpriteImage);
			}

			SpriteViewModel stickSVM3 = new SpriteViewModel(stickSprite);
			stickSVM3.OnSpriteMoveEvent += MoveSprite;
			exists = SelectedPanel.SpriteImages.ContainsKey(stickSVM3.SpriteImage.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(stickSVM3.SpriteImage.Id, stickSVM3.SpriteImage);
			}

			SpriteViewModel stickSVM4 = new SpriteViewModel(stickSprite);
			stickSVM4.OnSpriteMoveEvent += MoveSprite;
			exists = SelectedPanel.SpriteImages.ContainsKey(stickSVM4.SpriteImage.Id);
			if (!exists) {
				SelectedPanel.SpriteImages.Add(stickSVM4.SpriteImage.Id, stickSVM4.SpriteImage);
			}


			currentProject.SelectedPanel.Branches.Add(new Branch("Branch3",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Heinrich Kleinrich", TextShown = "I am Heinrich", Sprites = new ObservableCollection<SpriteViewModel>(){stickSVM2}},
															new DialogLine{ CharacterName = "Doge", TextShown = "And I Am Doge", Sprites = new ObservableCollection<SpriteViewModel>(){stickSVM3, dogeSVM3}},
														},
														new Continue(ContinueTypeEnum.Branch, "Branch4")));

			currentProject.SelectedPanel.Branches.Last().Continue.OpenButtonSpriteDialogReference = OpenButtonSpriteDialog;

			Option newOption3 = new Option("Option3", "Repeat Doge", buttonSprite, new Continue(ContinueTypeEnum.Branch, "Branch5"));
			newOption3.OnButtonSpriteChange += OpenButtonSpriteDialog;
			Option newOption4 = new Option("Option4", "Repeat Heinrich", buttonSprite, new Continue(ContinueTypeEnum.Branch, "Branch6"));
			newOption4.OnButtonSpriteChange += OpenButtonSpriteDialog;


			currentProject.SelectedPanel.Branches.Add(new Branch("Branch4",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "", TextShown = "This is the second split"},
														},
														new Continue(
															ContinueTypeEnum.Split,
															new Split("Split2",
																new ObservableCollection<Option>() {
																	newOption3,
																	newOption4
																}
															)
														)));
			currentProject.SelectedPanel.Branches.Last().Continue.OpenButtonSpriteDialogReference = OpenButtonSpriteDialog;

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch5",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Doge", TextShown = "Hello there", Sprites = new ObservableCollection<SpriteViewModel>(){dogeSVM4}},
														},
														new Continue(ContinueTypeEnum.Branch, "Branch7")));
			currentProject.SelectedPanel.Branches.Last().Continue.OpenButtonSpriteDialogReference = OpenButtonSpriteDialog;

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch6",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Heinrich Kleinrich", TextShown = "General Ke-dogee", Sprites = new ObservableCollection<SpriteViewModel>(){stickSVM4}},
														},
														new Continue(ContinueTypeEnum.Branch, "Branch7")));
			currentProject.SelectedPanel.Branches.Last().Continue.OpenButtonSpriteDialogReference = OpenButtonSpriteDialog;

			currentProject.SelectedPanel.Branches.Add(new Branch("Branch7",
														new ObservableCollection<ShownItem>() {
															new DialogLine{ CharacterName = "Goblin Doctor", TextShown = "Hello", Sprites = new ObservableCollection<SpriteViewModel>(){goblinSVM2}},
														},
														new Continue(ContinueTypeEnum.Panel, "Panel2")));
			currentProject.SelectedPanel.Branches.Last().Continue.OpenButtonSpriteDialogReference = OpenButtonSpriteDialog;

			SelectedPanel.EntryBranchKey = SelectedPanel.Branches.First().Name;

			for (int i = 0; i < SelectedPanel.Branches.Count; ++i) {
				SelectedPanel.Branches[i].SetEntryBranchEventHandler += EntryBranchChecked;
			}

			string currentPath = Directory.GetCurrentDirectory();
			currentPath = Path.Combine(currentPath, @"..\..\");
			var ProjectDir = Path.GetFullPath(currentPath);
			currentPath = Path.Combine(ProjectDir, @"VisualNovelInterface\Resources\fonts\");

			FontFamily newFontFam = new FontFamily(new Uri(currentPath), "PAPYRUS");
			ProjectFont newFont = new ProjectFont(){ Font = newFontFam, IsUsed = true};
			//FontFamily newFont = new FontFamily(new Uri(currentPath), "OpenSans-Regular");
			currentProject.FontManagerViewModel.Fonts.Add(newFont);
			currentProject.FontManagerViewModel.CurrentUsedFont = currentProject.FontManagerViewModel.Fonts.First();
			//GlobalSprites.Add(new SpriteImage(currentPath, "DogeSprite"));
			//currentProject.SelectedPanel.SelectedLine.Sprites.Add(new SpriteViewModel(GlobalSprites.First()));
			//currentProject.SelectedPanel.SelectedLine.Sprites.Last().OnSpriteMoveEvent += MoveSprite;
			//currentProject.SelectedPanel.SelectedLine.Sprites.Add(new SpriteViewModel(GlobalSprites.First()));
			//currentProject.SelectedPanel.SelectedLine.Sprites.Last().OnSpriteMoveEvent += MoveSprite;
		}
#endif
	}
}
