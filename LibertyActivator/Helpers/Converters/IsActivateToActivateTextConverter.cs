using System;
using System.Globalization;
using System.Windows.Data;

namespace LibertyActivator.Helpers.Converters
{
	public class IsActivateToActivateTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool flag == true)
			{
				return "Активация...";
			}
			return "Активировать";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
