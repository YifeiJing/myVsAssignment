
using System;
using System.Globalization;

namespace Example1
{
    class PauseTickerValueConverter : BaseValueConverter<PauseTickerValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int Number = (int)value / 5;
            

            return string.Format("{0:d2}", Number);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
