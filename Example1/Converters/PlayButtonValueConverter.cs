using System;
using System.Globalization;
using System.Windows;

namespace Example1
{
    class PlayButtonValueConverter : BaseValueConverter<PlayButtonValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (bool)value;
            if(temp)
            {
                return Application.Current.FindResource("kComplete");
            }
            else
            {
                return Application.Current.FindResource("kStart");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
