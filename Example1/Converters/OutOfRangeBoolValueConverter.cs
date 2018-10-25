using System;
using System.Globalization;
using System.Windows;

namespace Example1
{
    class OutOfRangeBoolValueConverter : BaseValueConverter<OutOfRangeBoolValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsOut = (bool)value;
            if(IsOut)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
