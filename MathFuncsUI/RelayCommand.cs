using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MathFuncsUI
{
  public class RelayCommandParameters : ICommand
  {
    #region Fields

    readonly Action<object> _execute;
    readonly Predicate<object> _canExecute;

    #endregion // Fields

    #region Constructors

    public RelayCommandParameters(Action<object> execute)
        : this(execute, null)
    {
    }

    public RelayCommandParameters(Action<object> execute, Predicate<object> canExecute)
    {
      if (execute == null)
        throw new ArgumentNullException("execute");

      _execute = execute;
      _canExecute = canExecute;
    }
    #endregion // Constructors

    #region ICommand Members

    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
      return _canExecute == null ? true : _canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
      _execute(parameter);
    }
    #endregion
  }

  public class RelayCommand : ICommand
  {
    #region private fields
    private readonly Action execute;
    private readonly Func<bool> canExecute;
    private object commandParameter;
    #endregion

    public event EventHandler CanExecuteChanged
    {
      // wire the CanExecutedChanged event only if the canExecute func
      // is defined (that improves perf when canExecute is not used)
      add
      {
        if (canExecute != null)
          CommandManager.RequerySuggested += value;
      }
      remove
      {
        if (canExecute != null)
          CommandManager.RequerySuggested -= value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the RelayCommand class
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    public RelayCommand(Action execute) : this(execute, null) { }

    /// <summary>
    /// Initializes a new instance of the RelayCommand class
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    public RelayCommand(Action execute, Func<bool> canExecute)
    {
      if (execute == null)
        throw new ArgumentNullException("execute");

      this.execute = execute;
      this.canExecute = canExecute;
    }

    /// <summary>
    /// The command parameter passed via the XAML CommandParameter property
    /// e.g. CommandParameter="{Binding ElementName=MyTextBox, Path=Text}"
    /// </summary>
    public object CommandParameter
    {
      get { return commandParameter; }
    }

    public void Execute(object parameter)
    {
      commandParameter = parameter;
      execute();
    }

    public bool CanExecute(object parameter)
    {
      commandParameter = parameter;
      return canExecute == null ? true : canExecute();
    }
  }
}
