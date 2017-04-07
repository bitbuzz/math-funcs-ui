using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MathFuncInterop;
using System.Data;

namespace MathFuncsUI
{
	public class ViewModel : INotifyPropertyChanged
	{
		public ViewModel()
		{
			_calculator = _calculatorFactory.CreateCalculator();
			_scientificCalculator = ((IScientificCalculator)_calculator);
		}

		private char[] _delimiterChars = { '(', ')', '+', '-', '*', '/', '=' };
		private string _calculatorField;
		private ICalculator _calculator = null;
		private IScientificCalculator _scientificCalculator = null;
		private CalculatorFactory _calculatorFactory = new CalculatorFactory();
		
		public static readonly string Value0 = "0";
		public static readonly string Value1 = "1";
		public static readonly string Value2 = "2";
		public static readonly string Value3 = "3";
		public static readonly string Value4 = "4";
		public static readonly string Value5 = "5";
		public static readonly string Value6 = "6";
		public static readonly string Value7 = "7";
		public static readonly string Value8 = "8";
		public static readonly string Value9 = "9";
		public static readonly string ValueX = "x";
		public static readonly string ValueY = "y";
		public static readonly string ValueAdd = "+";
		public static readonly string ValueSubtract = "-";
		public static readonly string ValueMultiply = "*";
		public static readonly string ValueDivide = "/";
		public static readonly string ValuePower = "^";
		public static readonly string ValueEqual = "=";
		public static readonly string ValueOpenParenthesis = "(";
		public static readonly string ValueClosedParenthesis = ")";

		public string CalculatorField
		{
			get { return _calculatorField; }
			set
			{
				_calculatorField = value;
				OnPropertyChanged("CalculatorField");
			}
		}
	
		private void ClearCalculatorField()
		{
			CalculatorField = string.Empty;
		}

		public void EvaluateExpression(string expression)
		{
			string result = string.Empty;

			_calculator.Add(1.252, 2.111);
			var add = _calculator.GetAnswer();
			result += "Add(): " + add.ToString() + Environment.NewLine;

			var addOne = ((IScientificCalculator)_calculator).AddOne(1);
			result += "AddOne(): " + addOne.ToString() + Environment.NewLine;

			string tester = "Testing BSTR";
			_calculator.GetString(ref tester);
			result += tester + Environment.NewLine;

			string raiseToPower = _scientificCalculator.RaiseToPower (2.0, 10.0).ToString();
			result += "RaiseToPower(): " + (_calculator.GetAnswer().ToString()) + Environment.NewLine;

			CalculatorField = result;
		}

		private void UpdateCalculatorField(string value)
		{
			CalculatorField += value;
		}

		#region Interop Functions

		public string RunInteropFunctions()
		{
			Random rand = new Random();
			double number1 = rand.Next(1, 10);
			double number2 = rand.Next(1, 10);

			string output = string.Empty;
			double val = -1;

			val = MathFunctionsInterop.AddNumbers(number1, number2);
			output = "Add " + number1.ToString() + " + " + number2.ToString() + " = " + val.ToString();

			val = MathFunctionsInterop.SubtractNumbers(number1, number2);
			output += Environment.NewLine + "Subtract " + number1.ToString() + " - " + number2.ToString() + " = " + val.ToString();

			val = MathFunctionsInterop.MultiplyNumbers(number1, number2);
			output += Environment.NewLine + "Multiply " + number1.ToString() + " * " + number2.ToString() + " = " + val.ToString();

			val = MathFunctionsInterop.DivideNumbers(number1, number2);
			output += Environment.NewLine + "Divide " + number1.ToString() + " / " + number2.ToString() + " = " + val.ToString();

			return output;
		}
		
		#endregion
		
		#region ICommands

		private RelayCommand _clearCalculatorFieldCommand;
		public ICommand ClearCalculatorFieldCommand
		{
			get
			{
				if (_clearCalculatorFieldCommand == null)
					_clearCalculatorFieldCommand = new RelayCommand(
						() => ClearCalculatorField(), () => CanClearCalculatorFieldCommand());
				return _clearCalculatorFieldCommand;
			}
		}
		private bool CanClearCalculatorFieldCommand()
		{
			return true;
		}

		private RelayCommand _submitCalculatorFieldCommand;
		public ICommand SubmitCalculatorFieldCommand
		{
			get
			{
				if (_submitCalculatorFieldCommand == null)
					_submitCalculatorFieldCommand = new RelayCommand(
						() => EvaluateExpression(CalculatorField), () => CanSubmitCalculatorFieldCommand());
				return _submitCalculatorFieldCommand;
			}
		}
		private bool CanSubmitCalculatorFieldCommand()
		{
			if (string.IsNullOrEmpty(CalculatorField) || string.IsNullOrWhiteSpace(CalculatorField))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		private RelayCommandParameters _updateCalculatorFieldCommand;
		public ICommand UpdateCalculatorFieldCommand
		{
			get
			{
				if (_updateCalculatorFieldCommand == null)
					_updateCalculatorFieldCommand = new RelayCommandParameters(param => UpdateCalculatorField((string)param));
				return _updateCalculatorFieldCommand;
			}
		}
		private bool CanUpdateCalculatorFieldCommand()
		{
			return true;
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
