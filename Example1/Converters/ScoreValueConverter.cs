using System;
using System.Globalization;

namespace Example1
{
    class ScoreValueConverter : BaseValueConverter<ScoreValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var score = (int)value;
            return string.Format("Score: {0}", score);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
