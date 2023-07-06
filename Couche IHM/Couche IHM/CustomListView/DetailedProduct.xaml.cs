using Couche_IHM.ImagesProduit;
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
