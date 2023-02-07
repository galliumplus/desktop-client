using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    /// <summary>
    /// Produit
    /// </summary>
    public class Product
    {
        private string nomProduit;
        private int quantite;
        private float prixAdherent;
        private string categorie;

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string NomProduit 
        { 
            get => nomProduit; 
            set => nomProduit = value; 
        }

        /// <summary>
        /// Quantité du produit
        /// </summary>
        public int Quantite 
        { 
            get => quantite; 
            set => quantite = value; 
        }

        /// <summary>
        /// Prix adhérent
        /// </summary>
        public float PrixAdherent 
        { 
            get => prixAdherent; 
            set => prixAdherent = value; 
        }

        /// <summary>
        /// Prix non adhérent (+20 centimes)
        /// </summary>
        public float PrixNonAdherent
        {
            get => prixAdherent + 0.20f;
        }

        /// <summary>
        /// Categorie du produit
        /// </summary>
        public string Categorie 
        { 
            get => categorie; 
            set => categorie = value; 
        }

        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="nomProduit"> nom du produit </param>
        /// <param name="quantite"> quantite du produit </param>
        /// <param name="prixAdherent"> prix adherent </param>
        /// <param name="categorie"> categorie du produit </param>
        public Product(string nomProduit, int quantite, float prixAdherent, string categorie)
        {
            this.nomProduit = nomProduit;
            this.quantite = quantite;
            this.prixAdherent = prixAdherent;
            this.categorie = categorie;
        }

       
    }
}
