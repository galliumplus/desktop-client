using Couche_IHM.ImagesProduit;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Couche_IHM.CustomListView
{
    /// <summary>
    /// Logique d'interaction pour DetailedProduct.xaml
    /// </summary>
    public partial class DetailedProduct : UserControl
    {
        private Product product;
        public Product Product { get => product; set { product = value; } }

        /// <summary>
        /// Constructeur de la classe DetailedProduct
        /// </summary>
        /// <param name="p"></param>
        public DetailedProduct(Product p)
        {
            InitializeComponent();
            DataContext = p;
            product = p;
            ImageManager image = new ImageManager();
            this.image.Source = new BitmapImage(new Uri(image.GetImageFromProduct(p.NomProduit),UriKind.Absolute));
        }
    }
}
