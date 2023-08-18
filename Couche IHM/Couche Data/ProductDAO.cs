
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{
    /// <summary>
    /// Interfaction avec les produits de la base de donnée
    /// </summary>
    public class ProductDAO : IProductDAO
    {
        private List<Product> products = new List<Product>();

        public ProductDAO()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM Products ORDER BY name";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                products.Add(new Product(rdr.GetInt32("product_id"), rdr.GetString("name"), rdr.GetInt32("stock"), rdr.GetFloat("price_na"), rdr.GetFloat("price_a"), rdr.GetInt32("category_id")));
            }

            dbsDAO.Instance.CloseDatabase();
        }

        public void CreateProduct(Product product)
        {
            this.products.Add(product);
        }

        public Product? GetProduct(int id)
        {
            return this.products.Find(x => id == x.ID);
        }

        /// <summary>
        /// Récupère tous les produits d'une catégorie
        /// </summary>
        /// <returns> liste de produits </returns>
        public List<Product> GetProductsByCategory(Category category)
        {
            return this.products.FindAll(x => category.IdCat == x.Categorie);
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public void RemoveProduct(Product product)
        {
            this.products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            Product p = GetProduct(product.ID);
            p.NomProduit = product.NomProduit;
            p.PrixNonAdherent = product.PrixNonAdherent;
            p.PrixAdherent = product.PrixAdherent;
            p.Quantite = product.Quantite;
            p.Categorie = product.Categorie;
        }

    }
}
