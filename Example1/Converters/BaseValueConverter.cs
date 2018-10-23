using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Example1
{
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T: class, new()
    {
        #region Private members

        /// <summary>
        /// Creat converter
        /// </summary>
        private T mConverter = null;

        #endregion

        #region Public members

        /// <summary>
        /// Return the value or the default value
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        }

        #endregion

        #region Value converter Methods
        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Back convert
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion
    }
}
