using System;
using System.Diagnostics;
using System.Globalization;

namespace Example1
{
    class PageValueConverter : BaseValueConverter<PageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((PageTypes)value)
            {
                case PageTypes.LoginPage: return new LoginPage();
                default: Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
