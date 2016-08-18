using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace pixiv_sekai
{
    class DimensionSubtractMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (double)value - 2.0*System.Convert.ToDouble((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (double)value + 2.0*System.Convert.ToDouble((string)parameter);
        }
    }
}
