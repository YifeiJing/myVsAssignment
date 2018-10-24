using System;
using System.Globalization;
using System.Windows.Media;

namespace Example1
{
    class PortStatusToForegroundColor : BaseValueConverter<PortStatusToForegroundColor>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string portstatus = (string)value;
            if(portstatus == "On")
            {
                return new SolidColorBrush(Color.FromRgb(0x00, 0xc5, 0x41));
            }
            else
            {
                return new SolidColorBrush(Color.FromRgb(0xff, 0x47, 0x47));
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
