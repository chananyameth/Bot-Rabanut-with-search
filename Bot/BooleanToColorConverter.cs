using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Bot
{
	public class BooleanToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (value is bool && (bool)value) ?
				new SolidColorBrush(Color.FromArgb(255, 5, 97, 98)) :
				new SolidColorBrush(Color.FromArgb(255, 38, 45, 49));
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new Exception("Not Implemented");
		}
	}
}
