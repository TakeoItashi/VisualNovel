using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VisualNovelInterface.Models.Args;
using VisualNovelInterface.Models.Enums;
using VisualNovelInterface.MVVM;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using VisualNovelInterface.Models;

namespace VisualNovelInterface.ViewModels
{
	public class VariableManagerViewModel : BaseObject
	{
		private ObservableCollection<DataValue> m_variables;
		private DataValue m_selectedValue;
		private static readonly Regex m_integerRegex = new Regex("[^0-9]+");
		private static readonly Regex m_decimalRegex = new Regex(@"^(\d+\.\d+)$");

		public DataValue SelectedValue {
			get => m_selectedValue;
			set {
				SetProperty(ref m_selectedValue, value);
				OnPropertyChanged(nameof(IsValueSelected));
			}
		}
		public ObservableCollection<DataValue> Variables {
			get => m_variables;
			set => SetProperty(ref m_variables, value);
		}

		public bool IsValueSelected {
			get => SelectedValue != null;
		}

		public RelayCommand<TextCompositionEventArgs> CheckInputForIntegerCommand {
			get;
			set;
		}

		public RelayCommand<TextBoxEventArgs> CheckInputForDecimalCommand {
			get;
			set;
		}

		public RelayCommand<TextBoxEventArgs> ChechForDecimalInTextCommand {
			get;
			set;
		}

		public RelayCommand AddNewVariableCommand {
			get;
			set;
		}

		public RelayCommand DeleteSelectedVariableCommand {
			get;
			set;
		}


		public VariableManagerViewModel() {
			bool canExecute = true;
			CheckInputForIntegerCommand = new RelayCommand<TextCompositionEventArgs>(CheckInputForInteger);
			CheckInputForDecimalCommand = new RelayCommand<TextBoxEventArgs>(CheckInputForDecimal);
			ChechForDecimalInTextCommand = new RelayCommand<TextBoxEventArgs>(ChechForDecimalInText);
			AddNewVariableCommand = new RelayCommand(AddNewVariable);
			DeleteSelectedVariableCommand = new RelayCommand(DeleteSelectedVariable);
#if DEBUG
			m_variables = new ObservableCollection<DataValue> {
				new DataValue("testBool", new Tuple<DataValueTypeEnum, object>(DataValueTypeEnum.trigger, true)),
				new DataValue("testDecimal", new Tuple<DataValueTypeEnum, object>(DataValueTypeEnum.decimalCCPlus, 12.8)),
				new DataValue("testString", new Tuple<DataValueTypeEnum, object>(DataValueTypeEnum.text, "test test"))
			};
#endif
		}


		public void CheckInputForInteger(TextCompositionEventArgs e) {
			e.Handled = IsInteger(e.Text);
		}

		public void CheckInputForDecimal(TextBoxEventArgs e) {
			//e.Handled = IsDecimal(e.Text);

			Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
			TextBox testBox = (e.sender as TextBox);
			string testText = testBox.Text;
			if (testBox.SelectedText.Length > 0) {
				testText = testText.Replace(testBox.SelectedText, e.args.Text);
			}
			string newString = testText.Insert(testBox.SelectionStart, e.args.Text);
			var test = string.Format("{0:0.0##########################}", testText);
			e.args.Handled = !regex.IsMatch(newString);
		}

		public void ChechForDecimalInText(TextBoxEventArgs e) {
			//e.Handled = IsDecimal(e.Text);
			string text = (e.sender as TextBox).Text;
			if (!text.Contains('.')) {
				text += ".0";
			} else if (text.Last() == '.') {
				text += '0';
			}
			(e.sender as TextBox).Text = text;
		}

		public static bool IsInteger(string text) {

			return m_integerRegex.IsMatch(text);
		}
		private static bool IsDecimal(string text) {

			return m_decimalRegex.IsMatch(text);
		}

		private void AddNewVariable() {
			m_variables.Add(new DataValue("NewVariable", new Tuple<DataValueTypeEnum, object>(DataValueTypeEnum.trigger, false)));
		}

		private void DeleteSelectedVariable() {
			m_variables.Remove(SelectedValue);
		}
	}
}
