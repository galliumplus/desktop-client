
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{
    public class FakeProduitsDAO : IProductDAO
    {
        private List<Product> products = new List<Product>();

        public FakeProduitsDAO() 
        {
            //Connection
            string connString = String.Format("server={0};port={1};user id={2};password={3};database={4};SslMode={5}", "51.178.36.43", "3306", "c2_gallium", "DfD2no5UJc_nB", "c2_gallium", "none");
            MySqlConnection mySqlConnection = new MySqlConnection(connString);
            mySqlConnection.Open();

            //Requette SQL
            string stm = "SELECT * FROM Products ORDER BY name";
            MySqlCommand cmd = new MySqlCommand(stm, mySqlConnection);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<Product> products = new List<Product>();

            while (rdr.Read())
            {
                products.Add(new Product(rdr.GetInt32("product_id"), rdr.GetString("name"), rdr.GetInt32("stock"), rdr.GetFloat("price_na"), rdr.GetFloat("price_a"), rdr.GetInt32("category_id")));
            }

            mySqlConnection.Close();
            this.products = products;
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


        private string getRandomCategorie()
        {
            List<string> categories =new List<string>(){
                "BOISSON",
                "SNACKS",
                "HIDDEN",
                "PABLO",
                "test"
            };
            return categories[new Random().Next(0, categories.Count)];
        }
    }
}
