﻿using Microsoft.Win32;
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
		private List<DataValue> m_variables;

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
		public List<DataValue> Variables {
			get => m_variables;
			set => SetProperty(ref m_variables, value);
		}
		#endregion


		public TestViewModel()
		{
			canExecute = true;
			currentProject = new Project();
			DialogueLine = new DialogueLine();
			SelectedLine = new DialogueLine();
			AddBackgroundImageCommand = new RelayCommand(AddBackgroundImage, parameter => canExecute);
			Variables = new List<DataValue>();

			IntPtr handle = DLLImporter.CreateDataValue_bool("newTrigger", true);
			Console.WriteLine($"The Adress is: 0x{handle.ToString("X16")}");
			bool value = DLLImporter.ReadDataValue_bool(handle);
			Console.WriteLine($"The Value after start is: {value}");
			DLLImporter.SetDataValue_bool(handle, false);
			Console.WriteLine("The Value of the handle was set differently.");
			bool value2 = DLLImporter.ReadDataValue_bool(handle);
			Console.WriteLine($"The new Value is {value2}");
			DLLImporter.FreeDataValue(handle);
			Console.WriteLine("The new Value is was now set free");
			DataValue newValue = new DataValue("TestBool", new Tuple<DataValueType, object>(DataValueType.trigger, true));
			Variables.Add(newValue);
			newValue = new DataValue("TestInt", new Tuple<DataValueType, object>(DataValueType.variable, 20));
			Variables.Add(newValue);
			//DLLImporter.CreateDataValue_int("newVariable", 1, 3);
			//DLLImporter.CreateDataValue_float("newVariable", 1, 3.0f);
			//DLLImporter.CreateDataValue_string("newDecimal", 2, "dfvds");
		}

		public void AddBackgroundImage(object obj)
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

		public void OnSelectionChanged(object obj)
		{
			Console.Write("");
		}
	}
}