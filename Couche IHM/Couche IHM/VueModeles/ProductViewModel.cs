

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
        /// Permet de savoir si le produit est disponible
        /// </summary>
        public bool isDisponible
        {
            get
            {
                return (product.Quantite > 0);
            }
        }

        /// <summary>
        /// Renvoie la quantite du produit
        /// </summary>
        public int Quantite
        {
            get
            {
                return product.Quantite;
            }
        }

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string NomProduit
        {
            get => product.NomProduit;
            set => product.NomProduit = value;
        }

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

        /// <summary>
        /// Categorie du produit
        /// </summary>
        public string Categorie
        {
            get => product.Categorie;
            set => product.Categorie = value;
        }


        #endregion
        public ProductViewModel(Product product)
        {
            this.product = product;
        }
    }
}
