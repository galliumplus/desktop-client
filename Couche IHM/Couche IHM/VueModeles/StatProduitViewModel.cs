using Modeles;

namespace Couche_IHM.VueModeles
{
    public class StatProduitViewModel
    {
        #region attributes
        private ProductViewModel productViewModel;
        private int purchaseCount;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du stat produit vue modele
        /// </summary>
        public StatProduitViewModel(StatProduit stat,ProductViewModel product)
        {
            this.purchaseCount = stat.Number_sales;
            this.productViewModel = product;
        }
        #endregion

        #region properties
        /// <summary>
        /// Produit acheté
        /// </summary>
        public ProductViewModel ProductViewModel { get => productViewModel; set => productViewModel = value; }
        /// <summary>
        /// Nombre de fois que le produit a été acheté
        /// </summary>
        public int PurchaseCount { get => purchaseCount; set => purchaseCount = value; }
        #endregion
    }
}
