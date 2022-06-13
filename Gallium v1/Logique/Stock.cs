using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    public static class Stock
    {
        
        private static List<Product> stockProduits = new List<Product>();

        /// <summary>
        /// Liste des produits
        /// </summary>
        public static List<Product> StockProduits 
        { 
            get => stockProduits;
        }


        /// <summary>
        /// Permet de récupérer le stock d'un produit à partir de son nom
        /// </summary>
        /// <param name="nomProduit"></param>
        /// <returns></returns>
        public static int stockProduit(string nomProduit)
        {
            return findProduit(nomProduit).Stock;
        }

        /// <summary>
        /// Permet d'ajouter un produit
        /// </summary>
        /// <param name="nomProduit"> Nom du produit </param>
        /// <param name="prixProduitAdhérent"> Prix du produit pour les adhérents </param>
        /// <param name="pathImage"> Chemin vers l'image </param>
        /// <param name="category"> Categorie du produit </param>
        /// <param name="stock"> Stock restant du produit </param>
        public static void ajoutProduit(string nomProduit, double prixProduitAdhérent, string pathImage, Category category, int stock)
        {
            stockProduits.Add(new Product(nomProduit, prixProduitAdhérent, pathImage, category, stock));
        }

        /// <summary>
        /// Permet de retrouver un produit à partir de son nom
        /// </summary>
        /// <param name="nomProduit"></param>
        /// <returns></returns>
        public static Product findProduit(string nomProduit)
        {
            Product product = null;
            foreach ( Product p in stockProduits)
            {
                if (p.NomProduit.ToUpper() == nomProduit.ToUpper())
                {
                    product = p;
                    break;
                }
            }
            return product;
        }

        public static void removeProduit(Product produit)
        {
            StockProduits.Remove(produit);
        }

    }
}
