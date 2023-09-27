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
    internal class PositionPodiumProduit : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string nomProduit)
            {
                int position = MainWindowViewModel.Instance.StatViewModel.PodiumProduits.FindIndex(p => p.ProductViewModel.NomProduitIHM == nomProduit) + 1;
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
