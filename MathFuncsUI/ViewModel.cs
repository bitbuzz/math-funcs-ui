using System;
using System.ComponentModel;
using System.Windows.Input;
using MathFuncInterop;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows;

namespace MathFuncsUI
{
  internal class ViewModel : INotifyPropertyChanged
  {
    private bool _isJustCalculatedExpression = false;
    private bool _isScrollCalculatorFieldEnabled = true;
    private string _calculatorField;
    private double _previousAnswer = 0;
    private int _calculatorFieldSelectionStart = 0;
    private ICalculator _calculator = null;
    private IScientificCalculator _scientificCalculator = null;
    private CalculatorFactory _calculatorFactory = new CalculatorFactory();

    private char[] _delimiterChars = { '(', ')', '+', '-', '*', '/', '=' };
    public static readonly string ValueDecimal = ".";
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
    public static readonly string Sin = "sin";
    public static readonly string Cos = "cos";
    public static readonly string ValueAdd = "+";
    public static readonly string ValueSubtract = "-";
    public static readonly string ValueMultiply = "*";
    public static readonly string ValueDivide = "/";
    public static readonly string ValuePower = "^";
    public static readonly string ValueEqual = "=";
    public static readonly string ValueOpenParenthesis = "(";
    public static readonly string ValueClosedParenthesis = ")";

    public ViewModel()
    {
      _calculator = _calculatorFactory.CreateCalculator();
      _scientificCalculator = ((IScientificCalculator)_calculator);
      CalculatorField = "1. Type open dirs" + Environment.NewLine + "2. Then ='s to open dev directories.";
    }

    #region Properties

    public string CalculatorField
    {
      get { return _calculatorField; }
      set
      {
        _calculatorField = value;
        OnPropertyChanged("CalculatorField");
      }
    }

    public int CalculatorFieldSelectionStart
    {
      get { return _calculatorFieldSelectionStart; }
      set
      {
        _calculatorFieldSelectionStart = value;
        OnPropertyChanged("CalculatorFieldSelectionStart");
      }
    }

    public bool IsScrollCalculatorFieldEnabled
    {
      get { return _isScrollCalculatorFieldEnabled; }
    }

    #endregion

    #region Methods

    private void ClearCalculatorField()
    {
      CalculatorField = string.Empty;
    }

    private void EvaluateExpression(string expression)
    {
      if (string.IsNullOrEmpty(expression) || string.IsNullOrWhiteSpace(expression))
        return;

      if (expression == "open dirs")
      {
        OpenDevelopmentDirectories();
        return;
      }

      string currentExpression = string.Empty;
      const char CR = '\r'; // or (char)13

      if (expression.Contains(CR))
      {
        currentExpression = GetLastLines(expression, 1).FirstOrDefault();
      }
      else
      {
        currentExpression = expression;
      }

      if (string.IsNullOrEmpty(currentExpression))
        return;

      // DO WORK...

      CalculatorField = _scientificCalculator.Subtract(1, 2).ToString() + Environment.NewLine;
      CalculatorField += _scientificCalculator.Add(1, 2).ToString() + Environment.NewLine;
      string test = "test_";
      _scientificCalculator.AppendInputToRandNumber(ref test);
      CalculatorField += test + Environment.NewLine;
      double answer = _scientificCalculator.RaiseToPower(5, 3);
      CalculatorField += answer.ToString() + Environment.NewLine;
      answer = _scientificCalculator.Sin(5.0);
      CalculatorField += answer.ToString() + Environment.NewLine;

      // Scientific
      //
      //string sinPrefix = "sin";
      //string cosPrefix = "cos";
      //string tanPrefix = "tan";
      //if (currentExpression.StartsWith(sinPrefix))
      //{
      //  string test = "n";
      //  _scientificCalculator.AppendInputToRandNumber(ref test);
      //  var tmp = currentExpression.Remove(0, sinPrefix.Length);
      //  var dbl = Convert.ToDouble(tmp);
      // double answer = _scientificCalculator.Sin(dbl);
      //  CalculatorField += answer.ToString() + Environment.NewLine;
      //  return;
      //}
      //else if (currentExpression.StartsWith(cosPrefix))
      //{
      //  var tmp = currentExpression.Remove(0, cosPrefix.Length);
      //  var dbl = Convert.ToDouble(tmp);
      //  CalculatorField += _scientificCalculator.Cos(dbl).ToString() + Environment.NewLine;
      //  return;
      //}
      //else if (currentExpression.StartsWith(tanPrefix))
      //{
      //  var tmp = currentExpression.Remove(0, tanPrefix.Length);
      //  var dbl = Convert.ToDouble(tmp);
      //  CalculatorField += _scientificCalculator.Tan(dbl).ToString() + Environment.NewLine;
      //  return;
      //}
      //string output = "";
      //_scientificCalculator.AppendStrings("Generate a", "random number", ref output);

      // Calculator
      //
      //CalculatorField = _scientificCalculator.Add(1, 2).ToString() + Environment.NewLine;

      // Interop:
      //
      //CalculatorField += RunInteropFunctions();

      // Basic Calculations:
      //
      //DataTable dataTable = new DataTable();
      //var answer = Convert.ToDouble(dataTable.Compute(currentExpression, string.Empty).ToString());
      //_previousAnswer = answer;
      //_isScrollCalculatorFieldEnabled = true;
      //CalculatorField = expression + Environment.NewLine + answer + Environment.NewLine;
      //_isScrollCalculatorFieldEnabled = false;

      // END WORK...

      _isJustCalculatedExpression = true;
    }

    private static List<string> GetLastLines(string str, int count)
    {
      // Get last lines.
      List<string> lines = new List<string>();
      Match match = Regex.Match(str, "^.*$", RegexOptions.Multiline | RegexOptions.RightToLeft);

      while (match.Success && lines.Count < count)
      {
        lines.Insert(0, match.Value);
        match = match.NextMatch();
      }
      return lines;
    }

    private void OpenDevelopmentDirectories()
    {
      Process.Start(@"D:\Git\managed-unmanaged-code\MathFuncsUI\MathFuncsUI");
      Process.Start(@"D:\Git\managed-unmanaged-code\MathFuncDll\MathFuncInterop\bin\Debug");
      Process.Start(@"D:\Git\managed-unmanaged-code\MathFuncDll\MathFuncInterop");
      Process.Start(@"D:\Git\managed-unmanaged-code\MathFuncDll\Debug");
      Process.Start(@"D:\Git\managed-unmanaged-code\MathFuncDll\MathFuncATL");
      Process.Start(@"D:\Git\managed-unmanaged-code\MathFuncATL\MathFuncATL");
      Process.Start(@"D:\Git\managed-unmanaged-code\MathFuncATL\Debug");
    }

    private void UpdateCalculatorField(string value)
    {
      if (_isJustCalculatedExpression)
      {
        int number;
        if (int.TryParse(value, out number) == false)
        {
          value = _previousAnswer + value;
        }
        _isJustCalculatedExpression = false;
      }
      CalculatorField += value;
    }

    #endregion

    #region Interop Functions

    public string RunInteropFunctions()
    {
      Random rand = new Random();
      double number1 = rand.Next(1, 10);
      double number2 = rand.Next(1, 10);

      string output = string.Empty;
      double val = -1;

      val = MathFunctionsInterop.AddNumbers(number1, number2);
      output = Environment.NewLine + "Add " + number1.ToString() + " + " + number2.ToString() + " = " + val.ToString() + Environment.NewLine;

      val = MathFunctionsInterop.SubtractNumbers(number1, number2);
      output += "Subtract " + number1.ToString() + " - " + number2.ToString() + " = " + val.ToString() + Environment.NewLine;

      val = MathFunctionsInterop.MultiplyNumbers(number1, number2);
      output += "Multiply " + number1.ToString() + " * " + number2.ToString() + " = " + val.ToString() + Environment.NewLine;

      val = MathFunctionsInterop.DivideNumbers(number1, number2);
      output += "Divide " + number1.ToString() + " / " + number2.ToString() + " = " + val.ToString();

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
            () => { try { EvaluateExpression(CalculatorField); } catch (Exception ex) { MessageBox.Show(ex.Message + ex.StackTrace, "Calculator", MessageBoxButton.OK, MessageBoxImage.Warning); } },
            () => CanSubmitCalculatorFieldCommand());
        return _submitCalculatorFieldCommand;
      }
    }
    private bool CanSubmitCalculatorFieldCommand()
    {
      return true;
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
