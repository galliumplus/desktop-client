using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Couche_Métier
{
    /// <summary>
    /// Produit
    /// </summary>
    public class Product
    {
        private int id;
        private string nomProduit;
        private int quantite;
        private double prixAdherent;
        private double prixNonAdherent;
        private string categorie;

        public int ID
        {
            get { return id; }
        }

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
        public double PrixAdherent 
        { 
            get => prixAdherent; 
            set => prixAdherent = value; 
        }

        /// <summary>
        /// Prix Adherent formatté pour l'afficher
        /// </summary>
        public string PrixAdherentIHM {
            get
            {
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();
                return converterFormatArgent.ConvertFormat(prixAdherent );
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
                return converterFormatArgent.ConvertFormat(PrixNonAdherent);
            }
        }

        /// <summary>
        /// Categorie du produit
        /// </summary>
        public string Categorie 
        { 
            get => categorie; 
            set => categorie = value; 
        }
        public double PrixNonAdherent { get => prixNonAdherent; set => prixNonAdherent = value; }

        /// <summary>
        /// Constructeur naturel
        /// </summary>
        /// <param name="nomProduit"> nom du produit </param>
        /// <param name="quantite"> quantite du produit </param>
        /// <param name="prixAdherent"> prix adherent </param>
        /// <param name="categorie"> categorie du produit </param>
        public Product(string nomProduit, int quantite, double prixAdherent, string categorie)
        {
            this.nomProduit = nomProduit;
            this.quantite = quantite;
            this.prixAdherent = prixAdherent;
            this.categorie = categorie;
            this.PrixNonAdherent = Math.Round(PrixAdherent + 0.20,2);
        }

        public override string ToString()
        {
            return $"{this.nomProduit} {this.quantite} {this.PrixAdherent} {categorie}";
        }


    }
}
