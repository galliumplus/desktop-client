
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{
    /// <summary>
    /// Interfaction avec les produits de la base de donnée
    /// </summary>
    public class ProductDAO : IProductDAO
    {

        public void CreateProduct(Product product)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"INSERT INTO products VALUES (0,'{product.NomProduit}',{product.Quantite},{product.PrixAdherent},{product.PrixNonAdherent},{product.Categorie})";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();
            

            using (MySqlCommand selectCommand = new MySqlCommand("SELECT LAST_INSERT_ID() AS nouvel_id", dbsDAO.Instance.Sql))
            {
                int nouvelId = Convert.ToInt32(selectCommand.ExecuteScalar());
                product.ID = nouvelId;
            }
            dbsDAO.Instance.CloseDatabase();

        }

        public List<Product> GetProducts()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM products ORDER BY name";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<Product> products = new List<Product>();
            while (rdr.Read())
            {
                products.Add(new Product(rdr.GetInt32("product_id"), rdr.GetString("name"), rdr.GetInt32("stock"), rdr.GetFloat("price_na"), rdr.GetFloat("price_a"), rdr.GetInt32("category_id")));
            }
            rdr.Close();
            dbsDAO.Instance.CloseDatabase();
            return products;
        }

        public void RemoveProduct(Product product)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"DELETE FROM products WHERE product_id = {product.ID}";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();

            dbsDAO.Instance.CloseDatabase();
        }

        public void UpdateProduct(Product product)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"UPDATE products SET name = '{product.NomProduit}', stock = {product.Quantite}, price_a = {product.PrixAdherent}, price_na = {product.PrixNonAdherent}, category_id = {product.Categorie})";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();

            dbsDAO.Instance.CloseDatabase();
            
        }

    }
}
