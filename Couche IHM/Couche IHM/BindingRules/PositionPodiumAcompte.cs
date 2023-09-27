using Couche_IHM.VueModeles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Couche_IHM.BindingRules
{
    internal class PositionPodiumAcompte : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string identfiant)
            {
                int position = MainWindowViewModel.Instance.StatViewModel.PodiumAcompte.FindIndex(p => p.AdherentViewModel.IdentifiantIHM == identfiant) + 1;
                return position.ToString();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
