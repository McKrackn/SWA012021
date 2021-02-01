using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace exam.Converter
{
    public class StringToBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (string)value.ToString();
            if (temp != null && temp.Equals("normal"))
            {
                return new SolidColorBrush(Colors.Green);
            }
            else if (temp != null && temp.Equals("warning"))
            {
                return new SolidColorBrush(Colors.Yellow);

            }
            else
            {
                return new SolidColorBrush(Colors.Red);

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) //Gibt eine NotImplementedException(); zurück
        {
            throw new NotImplementedException();
        }
    }
}
