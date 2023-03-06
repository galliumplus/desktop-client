using Couche_Métier;
using Couche_Métier.Utilitaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        public Product Product { get { return product; } }

        /// <summary>
        /// Permet d'obtenir la couleur selon si le produit est encore en stock ou pas
        /// </summary>
        public bool isDisponible
        {
            get
            {
                return (product.Quantite > 0);
            }
        }
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
        public int QuantiteProduitPanier
        {
            get => nombreProduit;
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
        /// Produit ahdérent
        /// </summary>
        public double PrixProduitAdherentAffichage
        {
            get => product.PrixAdherent;
        }

        /// <summary>
        /// Produit non adhérent
        /// </summary>
        public double PrixProduitNonAdherentAffichage
        {
            get => product.PrixNonAdherent;
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
        /// Prix Adherent formatté pour l'afficher
        /// </summary>
        public string PrixAdherentIHM
        {
            get
            {
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();
                return converterFormatArgent.ConvertFormat(product.PrixAdherent);
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
                return converterFormatArgent.ConvertFormat(product.PrixNonAdherent);
            }
        }

        /// <summary>
        /// Permet d'obtenir la catégorie du produit
        /// </summary>
        public string Categorie
        {
            get => product.Categorie;
        }

        public ProduitIHM(Product produit) 
        {
            this.product = produit;
            this.QuantiteProduitPanier = 1;
        }


        public ProduitIHM(ProduitIHM produitIHM)
        {
            this.product = new Product(produitIHM.Product);
            this.QuantiteProduitPanier = produitIHM.QuantiteProduitPanier;
            this.ImageProduit = produitIHM.ImageProduit;
        }
    }
}
