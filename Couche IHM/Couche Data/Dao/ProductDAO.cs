
using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data.Dao
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
                int categoryId = rdr.IsDBNull(rdr.GetOrdinal("category_id")) ? -1 : rdr.GetInt32("category_id");
                products.Add(new Product(rdr.GetInt32("product_id"), rdr.GetString("name"), rdr.GetInt32("stock"), rdr.GetFloat("price_na"), rdr.GetFloat("price_a"), categoryId));
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

            string connString = String.Format("server={0};port={1};user id={2};password={3};database={4};SslMode={5}", "51.178.36.43", "3306", "c2_gallium", "DfD2no5UJc_nB", "c2_etismash", "none");
            MySqlConnection sql = new MySqlConnection(connString);
            sql.Open();

            //Requette SQL
            string prixA = product.PrixAdherent.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string prixNA = product.PrixNonAdherent.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string stm = $"UPDATE products SET name = '{product.NomProduit}', stock = {product.Quantite}, price_a = {prixA}, price_na = {prixNA}, category_id = {product.Categorie} WHERE product_id = {product.ID}";
            MySqlCommand cmd = new MySqlCommand(stm, sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();

            sql.Close();
            
        }

    }
}
