using System;
using System.Globalization;

namespace Example1
{
    class PortStatusValueConverter : BaseValueConverter<PortStatusValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool portstatus = (bool)value;
            if(portstatus)
            {
                return string.Format("On");
            }
            else
            {
                return string.Format("Off");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
