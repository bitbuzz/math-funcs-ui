using System.Windows.Controls;

namespace MathFuncsUI
{
  /// <summary>
  /// Interaction logic for ScientificCalculatorControl.xaml
  /// </summary>
  public partial class ScientificCalculatorControl : UserControl
  {
    public ScientificCalculatorControl()
    {
      InitializeComponent();
    }

    private void TextBoxCalculatorField_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (((ViewModel)DataContext).IsScrollCalculatorFieldEnabled)
      {
        TextBoxCalculatorField.SelectionStart = TextBoxCalculatorField.Text.Length;
        TextBoxCalculatorField.CaretIndex = TextBoxCalculatorField.Text.Length;
        TextBoxCalculatorField.ScrollToEnd();
      }
    }
  }
}
