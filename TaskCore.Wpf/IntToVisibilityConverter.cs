using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TaskCore.Wpf
{
	/// <summary>
	/// Convert int value to xaml visibility value
	/// </summary>
	public class IntToVisibilityConverter : IValueConverter
	{
		private int val;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string reverse = (string)parameter;
			val = (int)value;
			if (reverse == "reverse")
			{
				return ((val) == 0) ? Visibility.Visible : Visibility.Collapsed;
			}
			else
			{
				return ((val) != 0) ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}