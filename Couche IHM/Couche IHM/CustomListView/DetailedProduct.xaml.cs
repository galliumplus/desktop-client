using Couche_IHM.ImagesProduit;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Modeles;
using System;

using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Couche_IHM.CustomListView
{
    /// <summary>
    /// Logique d'interaction pour DetailedProduct.xaml
    /// </summary>
    public partial class DetailedProduct : UserControl
    {
        private ProductViewModel product;
        public ProductViewModel Product { get => product; set { product = value; } }

        /// <summary>
        /// Constructeur de la classe DetailedProduct
        /// </summary>
        /// <param name="p"></param>
        public DetailedProduct(ProductViewModel p)
        {
            InitializeComponent();
            DataContext = p;
            product = p;
        }



        private void AddProduct(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindowViewModel.Instance.CaisseViewModel.AddProduct(this.product);
        }
    }
}
