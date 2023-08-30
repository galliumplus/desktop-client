using Couche_IHM.VueModeles;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Couche_IHM.VueModeles
{
    public class AcompteAndCheckboxConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is AcompteViewModel acompte && values[1] is bool isChecked)
            {
                return new Tuple<AcompteViewModel, bool>(acompte, isChecked);
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}