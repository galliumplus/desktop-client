using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM
{
    /// <summary>
    /// Produit IHM
    /// </summary>
    public class ProduitIHM
    {
        private Product product;
        private int nombreProduit;
        private string pathImage;

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string NomProduit
        {
            get => product.NomProduit;
        }

        /// <summary>
        /// Quantité produits présent dans la caisse
        /// </summary>
        public dynamic QuantiteProduitPanier
        {
            get => "x" + nombreProduit;
            set => nombreProduit = value;
        }

        /// <summary>
        /// Quantite du produit
        /// Get => String
        /// Set => Entier
        /// </summary>
        public int QuantiteProduit
        {
            get => product.Quantite;
            set => product.Quantite = value;
        }

        /// <summary>
        /// Image du produits
        /// </summary>
        public string ImageProduit
        {
            get => pathImage;
            set => pathImage = value;
        }

        /// <summary>
        /// Produit ahdérent
        /// </summary>
        public double PrixProduitAdherentAffichage
        {
            get => product.PrixAdherent;
        }

        /// <summary>
        /// Produit non adhérent
        /// </summary>
        public string PrixProduitNonAdherentAffichage
        {
            get => product.PrixNonAdherentIHM;
        }

        public ProduitIHM(Product produit) 
        {
            this.product = produit;
            this.QuantiteProduitPanier = 1;
        }

    }
}
