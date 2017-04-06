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
			CalculatorField = "abc";
		}

		private char[] _delimiterChars = { '(', ')', '+', '-', '*', '/', '=' };
		private ICalculator _calculator = null;
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

		public string CalculatorField
		{
			get { return _calculatorField; }
			set
			{
				_calculatorField = value;
				OnPropertyChanged("CalculatorField");
			}
		}
		private string _calculatorField;

		private void ClearCalculatorField()
		{
			CalculatorField = string.Empty;
		}

		public void EvaluateExpression(string expression)
		{
			string result = string.Empty;

			_calculator.Add(1.74, 2.27);
			var add = _calculator.GetAnswer();
			result += "Add(): " + add.ToString() + Environment.NewLine;

			var addOne = ((IScientificCalculator)_calculator).AddOne(147);
			result += "AddOne(): " + addOne.ToString() + Environment.NewLine;

			string tester = "hello";
			_calculator.GetString(ref tester);
			result += "GetString(): " + tester + Environment.NewLine;

			string raiseToPower = _calculator.RaiseToPower (2.0, 10.0).ToString();
			result += "RaiseToPower(): " + (_calculator.GetAnswer().ToString()) + Environment.NewLine;

			MessageBox.Show(result); 
		}

		private void UpdateCalculatorField(string value)
		{
			CalculatorField += value;
		}

		#region Interop Functions

		public string ReturnValue
		{
			get { return returnValue; }
			set
			{
				returnValue = value;
				OnPropertyChanged("ReturnValue");
			}
		}
		private string returnValue;

		private void Run()
		{
			ReturnValue = RunInteropFunctions();
			Console.WriteLine(ReturnValue);

			var factory = new CalculatorFactory();
			var pCalculator = factory.CreateCalculator();

			double inputValue = 4;
			double outputValue = pCalculator.RaiseToPower(inputValue, 2);
			Console.WriteLine("pScientificCalculator.SquareRoot(1024): " + outputValue.ToString());

			double currentAnswer = pCalculator.GetAnswer();

			var pScientificCalculator = pCalculator as IScientificCalculator;
			Console.WriteLine("pCalculator.Add(2,3): " + pCalculator.Add(2, 3).ToString());

			currentAnswer = pCalculator.GetAnswer();

			IScientificCalculator sc = pCalculator as IScientificCalculator;
			currentAnswer = sc.RaiseToPower(16, 2);
			currentAnswer = pCalculator.GetAnswer();
			currentAnswer = sc.AddOne(20);

			factory = null;
		}

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

		private RelayCommand _doSomethingCommand;
		public ICommand DoSomethingCommand
		{
			get
			{
				if (_doSomethingCommand == null)
					_doSomethingCommand = new RelayCommand(
									() => Run(), () => CanDoSomething());
				return _doSomethingCommand;
			}
		}
		private bool CanDoSomething()
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
