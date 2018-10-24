using System;
using System.Globalization;

namespace Example1
{
    class StatusValueConverter : BaseValueConverter<StatusValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
