
using System;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Example1
{
    class MapValueConverter : BaseValueConverter<MapValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "images\\Maps\\";
            int Number = (int)value;

            if(Number == 0)
            {
                
                BitmapImage temp = new BitmapImage(new Uri(BaseDirectory + "map01.png",UriKind.Absolute));
                return temp;
            }
            else
            {
                BitmapImage temp = new BitmapImage(new Uri(BaseDirectory + "map01.png", UriKind.Absolute));
                return temp;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
