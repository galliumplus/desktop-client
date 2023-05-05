
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
            this.nameCategory.Text = category;

            foreach(Product p in produits)
            {
                DetailedProduct dp = new DetailedProduct(p);
                products.Items.Add(dp);
                listProductView.Add(dp);
            }
        }        
    }
}
