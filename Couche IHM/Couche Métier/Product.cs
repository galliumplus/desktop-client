using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Couche_Métier.Utilitaire;

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

        /// <summary>
        /// Id du produit
        /// </summary>
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
                return converterFormatArgent.ConvertToString(prixAdherent );
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
                return converterFormatArgent.ConvertToString(PrixNonAdherent);
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
        /// Futur constructeur naturelle
        public Product(int id, string nomProduit, int quantite, double prixAdherent, double prixNonAdherent, string categorie)
        {
            this.id = id;
            this.nomProduit = nomProduit;
            this.quantite = quantite;
            this.prixAdherent = prixAdherent;
            this.prixNonAdherent = prixNonAdherent;
            this.categorie = categorie;
            this.PrixNonAdherent = this.prixNonAdherent;
        }

        /// <summary>
        /// Constructeur vide du produit
        /// </summary>
        public Product() { }

        /// <summary>
        /// Constructeur naturelle copiant l'objet
        /// </summary>
        /// <param name="p"> produit à copier </param>
        public Product(Product p)
        {
            this.id = p.ID;
            this.nomProduit = p.NomProduit;
            this.quantite = p.Quantite;
            this.prixAdherent = p.PrixAdherent;
            this.prixNonAdherent = p.PrixNonAdherent;
            this.categorie = p.Categorie;
        }

        public override string ToString()
        {
            return $"{this.nomProduit} {this.quantite} {this.PrixAdherent} {categorie}";
        }
    }
}
