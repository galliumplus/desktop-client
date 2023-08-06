using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Couche_IHM.BindingRules
{
    public class PodiumIndexToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int podiumIndex = (int)value;

            switch (podiumIndex)
            {
                case 0:
                    return "Numeric1";
                case 1:
                    return "Numeric2";
                case 2:
                    return "Numeric3";
                default:
                    return "Numeric4"; // Pour les autres positions, ajustez en conséquence
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
