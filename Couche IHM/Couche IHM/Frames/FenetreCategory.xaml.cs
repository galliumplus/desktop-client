using Couche_Data;
using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FenetreCategory.xaml
    /// </summary>
    public partial class FenetreCategory : Window
    {
        public FenetreCategory(List<string> listCategory)
        {
            InitializeComponent();
            this.listCategory.ItemsSource = listCategory;
        }
    }
}
