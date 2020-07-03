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

namespace VisualNovelInterface.ViewModels
{
	public class MainViewModel : BaseObject
	{
		private DialogueLine selectedLine;
		private Panel selectedPanel;
		private Project currentProject;
		private bool canExecute;
		private int selectedPanelIndex;
		//private ObservableCollection<Sprite> m_sprites;


		#region Properties
		public Project CurrentProject {
			get => currentProject;
			set => SetProperty(ref currentProject, value);
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
		public ICommand AddBackgroundImageCommand {
			get;
			set;
		}
		public ICommand OpenVariableManagerCommand {
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

		public string TestImage {
			get => @"F:\Users\Tom Appel\Desktop\Studium\VisualNovel\VisualNovelInterface\Resources\doge.png";
		}
		#endregion


		public MainViewModel()
		{
			canExecute = true;
			currentProject = new Project();
			//SelectedLine = new DialogueLine();
			AddBackgroundImageCommand = new RelayCommand(AddBackgroundImage);
			OpenVariableManagerCommand = new RelayCommand(OpenVariableManager);

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

		public void AddBackgroundImage()
		{
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

		public void OpenVariableManager()
		{
			VariableManagerWindow vm = new VariableManagerWindow();
			vm.DataContext = VariableManager;
			vm.Show();
		}

		public void OnSelectionChanged(object obj)
		{
			Console.Write("");
		}
	}
}
