using System;
using System.Globalization;
using System.Windows.Media;

namespace Example1
{
    class StringToRGBValueConverter : BaseValueConverter<StringToRGBValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom($"#{(string)value}"));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
