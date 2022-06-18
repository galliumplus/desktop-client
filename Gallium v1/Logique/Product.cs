using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Gallium_v1.Logique
{
    public class Product
    {
        private string nomProduit;
        private double prixProduitAdhérent;
        private string imageProduit;
        private Category categorie;
        private int stock;

        // Prix du produit pour les non adhérents
        public double prixProduitNonAdhérent { get => prixProduitAdhérent + 0.2; }

        // Prix en format string
        public string prixString 
        {
            get
            {
                string ret = "" + Math.Round(prixProduitAdhérent, 2);
                // 1 => 1,00
                if (new Regex("^[0-9]+$").IsMatch(ret))
                {
                    ret += ",00";
                }

                // 1,2 => 1,20
                if (new Regex("^[0-9]+,[0-9]$").IsMatch(ret))
                {
                    ret += "0";
                }

                ret += " €";
                return ret;
            }
        }
        // Nom du produit
        public string NomProduit { get => nomProduit; set => nomProduit = value; }

        // Prix du produit pour les adhérents
        public double PrixProduitAdhérent { get => prixProduitAdhérent; set => prixProduitAdhérent = value; }

        // Categorie du produit
        public Category Categorie { get => categorie; set => categorie = value; }

        // Stock restant du produit
        public int Stock { get => stock; set => stock = value; }
        public string ImageProduit { get => imageProduit; set => imageProduit = value; }


        /// <summary>
        /// Constructuer de la classe Product
        /// </summary>
        /// <param name="nomProduit"> Nom du Produit </param>
        /// <param name="prixProduitAdhérent"> Prix pour les adhérents</param>
        /// <param name="pathImage"> Chemin vers l'image du produit</param>
        /// <param name="category"> Categorie du produit </param>
        public Product(string nomProduit, double prixProduitAdhérent , string pathImage,Category category,int stock)
        {
            this.nomProduit = nomProduit;
            this.prixProduitAdhérent = prixProduitAdhérent;
            this.imageProduit = pathImage;
            this.categorie = category;
            this.Stock = stock;
        }

        



    }
}
