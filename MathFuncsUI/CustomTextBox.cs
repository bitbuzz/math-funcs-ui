using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MathFuncsUI
{
	public class CustomTextBox : TextBox
	{
		public static readonly DependencyProperty BindableSelectionStartProperty =
				DependencyProperty.Register(
				"BindableSelectionStart",
				typeof(int),
				typeof(CustomTextBox),
				new PropertyMetadata(OnBindableSelectionStartChanged));

		public static readonly DependencyProperty BindableSelectionLengthProperty =
				DependencyProperty.Register(
				"BindableSelectionLength",
				typeof(int),
				typeof(CustomTextBox),
				new PropertyMetadata(OnBindableSelectionLengthChanged));

		private bool changeFromUI;

		public CustomTextBox() : base()
		{
			SelectionChanged += OnSelectionChanged;
		}

		public int BindableSelectionStart
		{
			get
			{
				return (int)GetValue(BindableSelectionStartProperty);
			}

			set
			{
				SetValue(BindableSelectionStartProperty, value);
			}
		}

		public int BindableSelectionLength
		{
			get
			{
				return (int)GetValue(BindableSelectionLengthProperty);
			}

			set
			{
				SetValue(BindableSelectionLengthProperty, value);
			}
		}

		private static void OnBindableSelectionStartChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var textBox = dependencyObject as CustomTextBox;

			if (!textBox.changeFromUI)
			{
				int newValue = (int)args.NewValue;
				textBox.SelectionStart = newValue;
			}
			else
			{
				textBox.changeFromUI = false;
			}
		}

		private static void OnBindableSelectionLengthChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var textBox = dependencyObject as CustomTextBox;

			if (!textBox.changeFromUI)
			{
				int newValue = (int)args.NewValue;
				textBox.SelectionLength = newValue;
			}
			else
			{
				textBox.changeFromUI = false;
			}
		}

		private void OnSelectionChanged(object sender, RoutedEventArgs e)
		{
			if (BindableSelectionStart != SelectionStart)
			{ 
				changeFromUI = true;
				BindableSelectionStart = SelectionStart;
			}

			if (BindableSelectionLength != SelectionLength)
			{
				changeFromUI = true;
				BindableSelectionLength = SelectionLength;
			}
		}
	}
}
