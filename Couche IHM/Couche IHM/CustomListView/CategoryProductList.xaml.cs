
using Modeles;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Couche_IHM.CustomListView
{
    /// <summary>
    /// Logique d'interaction pour CategoryProductList.xaml
    /// </summary>
    public partial class CategoryProductList : UserControl
    {
        private List<DetailedProduct> listProductView = new List<DetailedProduct>();
        public List<DetailedProduct> ListProductView { get => listProductView; }

        /// <summary>
        /// Constructeur de la classe CategoryProductList
        /// </summary>
        /// <param name="category"></param>
        /// <param name="produits"></param>
        public CategoryProductList(string category,List<Product> produits)
        {
            InitializeComponent();
            this.nameCategory.Content = category;

            foreach(Product p in produits)
            {
                DetailedProduct dp = new DetailedProduct(p);
                products.Items.Add(dp);
                listProductView.Add(dp);
            }
        }        
    }
}
