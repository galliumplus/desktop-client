
using Couche_IHM.VueModeles;
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
        #region attributes
        private List<DetailedProduct> listProductView = new List<DetailedProduct>();
        private string category;
        #endregion

        #region properties
        public string Category { get { return category; } set => category = value; }
        public List<DetailedProduct> ListProductView { get => listProductView; }

        #endregion

        /// <summary>
        /// Constructeur de la classe CategoryProductList
        /// </summary>
        /// <param name="category"></param>
        /// <param name="produits"></param>
        public CategoryProductList(string category)
        {
            InitializeComponent();
            DataContext = this;
            this.Category = category;
            List<ProductViewModel> produits = MainWindowViewModel.Instance.ProductViewModel.Products.FindAll(prod => prod.CategoryIHM == category );  
            foreach (ProductViewModel p in produits)
            {
                DetailedProduct dp = new DetailedProduct(p);
                listProductView.Add(dp);
            }
        }        
    }
}
