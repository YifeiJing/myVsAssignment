using System;
using System.Globalization;

namespace Example1
{
    class ReceMessageValueConverter : BaseValueConverter<ReceMessageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string message = (string)value;
            if(message == DebugCommands.Debug)
            {
                return string.Format("Debugging");
            }
            else if(message == DebugCommands.MoveServo)
            {
                return string.Format("Moving the servo");
            }
            else if(message == DebugCommands.ReadDistanceSensor)
            {
                return string.Format("Reading distance sensor");
            }
            else if(message == DebugCommands.ReadRGBSensor)
            {
                return string.Format("Reading rgb sensor");
            }
            else
            {
                return string.Format("Ready");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
