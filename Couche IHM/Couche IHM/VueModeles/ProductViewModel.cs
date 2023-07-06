

using Couche_Métier.Utilitaire;
using Modeles;

namespace Couche_IHM.VueModeles
{
    public class ProductViewModel
    {
        #region attributes
        /// <summary>
        /// Représente le modèle produit
        /// </summary>
        private Product product;

        #endregion

        #region properties
        /// <summary>
        /// Prix Adherent formatté pour l'afficher
        /// </summary>
        public string PrixAdherentIHM
        {
            get
            {
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();
                return converterFormatArgent.ConvertToString(product.PrixAdherent);
            }
        }
        /// <summary>
        /// Prix non adhérent formatté pour l'afficher
        /// </summary>
        public string PrixNonAdherentIHM
        {
            get
            {
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();
                return converterFormatArgent.ConvertToString(product.PrixNonAdherent);
            }
        }
        #endregion
        public ProductViewModel(Product product)
        {
            this.product = product;
        }
    }
}
