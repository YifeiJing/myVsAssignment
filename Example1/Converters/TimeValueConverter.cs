
using System;
using System.Globalization;

namespace Example1
{
    class TimeValueConverter : BaseValueConverter<TimeValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int Number = (int)value / 5;
            int Miniutes = Number / 60;
            int Seconds = Number % 60;

            return string.Format("{0:d2},{1:d2}", Miniutes, Seconds);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
