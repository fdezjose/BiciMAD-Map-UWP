using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BiciMAD_Map.Converters
{
    class StationToStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int light = (int)value;

            switch (light)
            {
                case 0:
                    return "#7FBA00"; // Green status
                case 1:
                    return "#F25022"; // Red status
                case 2:
                    return "#FFB900"; // Yellow status
                default:
                    return "#737373"; // Gray status
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
