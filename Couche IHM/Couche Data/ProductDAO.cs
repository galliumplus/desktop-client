using Gallium_v1.Logique;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Data
{
    /// <summary>
    /// Interfaction avec les produits de la base de donnée
    /// </summary>
    public static class ProductDAO
    {

        /// <summary>
        /// Créer un produit
        /// </summary>
        /// <returns> le produit créer </returns>
        public static void CreateProduct(string nomProduit, double prixProduitAdhérent, string pathImage, string category, int stock)
        {
            string requete = $"Insert into Produit values(null, \"{nomProduit}\", \"{prixProduitAdhérent}\", \"{pathImage}\", \"{category}\",{stock})";
            dbsDAO.Instance.FetchSQL(requete);
        }

        /// <summary>
        /// Suprimme un produit
        /// </summary>
        public static void DeleteProduct()
        {
            
        }

        /// <summary>
        /// Modifie un produit
        /// </summary>
        /// <returns> le produit modifié </returns>
        public static Product UpdateProduct()
        {
            Product produit = null;



            return produit;
        }

        /// <summary>
        /// Lis le produit voulu
        /// </summary>
        /// <returns> le produit voulu </returns>
        public static List<string> ReadProduct(string identif, string mdp)
        {
            List<String> produit = null;

            string requete = $"SELECT identifiant, password, nom, prenom, nomrole FROM User INNER join Role on User.idRole = Role.idRole where identifiant = \"{identif}\" and password = \"{mdp}\";";

            dbsDAO.Instance.FetchSQL(requete);
            dbsDAO.Reader = dbsDAO.CMD.ExecuteReader();

            // Vérifie s'il y a des résultats
            if (dbsDAO.Reader.HasRows == true)
            {
                string nomProduit = "";
                double prix = 0.00;
                string lienimage = "";
                string category = "";
                int stock = 0;

                // Tant qu'il lit
                while (dbsDAO.Reader.Read())
                {
                    // Donne les résultats aux variables selon les noms de colonnes
                    nomProduit = dbsDAO.Reader.GetString("nomProduit");
                    prix = dbsDAO.Reader.GetDouble("prix");
                    lienimage = dbsDAO.Reader.GetString("lienimage");
                    category = dbsDAO.Reader.GetString("nomCategorie");
                    stock = dbsDAO.Reader.GetInt32("stock");
                }
                produit = new List<string>()
                {
                    nomProduit,
                    Convert.ToString(prix),
                    lienimage,
                    category,
                    Convert.ToString(stock)
                };
            }
            dbsDAO.Reader.Close();

            return produit;
        }

        /// <summary>
        /// Lis tous les produits disponibles
        /// </summary>
        /// <returns> La liste de tous les produits existants </returns>
        public static List<Product> ReadAllProduct()
        {
            List<Product> produits = new List<Product>();

            // Requête
            string requete = "SELECT idProduit, nomProduit, prix, lienimage, stock, nomCategorie FROM Produit INNER join Categorie on Produit.idCategorie = Categorie.idCategorie";

            // Lecture de la requête
            dbsDAO.CMD = new MySqlCommand(requete, dbsDAO.Instance.Sql);
            dbsDAO.Reader = dbsDAO.CMD.ExecuteReader();

            // Tant qu'il lit
            while (dbsDAO.Reader.Read())
            {
                produits.Add(new Product(dbsDAO.Reader.GetString("nomProduit"), dbsDAO.Reader.GetDouble("prix"), dbsDAO.Reader.GetString("lienimage"), (Category)Enum.Parse(typeof(Category), dbsDAO.Reader.GetString("nomCategorie")), dbsDAO.Reader.GetInt32("stock")));
            }
            dbsDAO.Reader.Close();

            return produits;
        }
    }
}
