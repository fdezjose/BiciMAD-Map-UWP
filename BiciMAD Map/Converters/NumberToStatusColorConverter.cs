using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BiciMAD_Map.Converters
{
    class NumberToStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int number = (int)value;

            if (number < 5) // Red. Low availability.
            {
                return "#E74C3C";
            }
            else
            {
                return "White";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
