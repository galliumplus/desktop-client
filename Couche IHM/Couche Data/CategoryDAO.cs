

using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{
    public class CategoryDAO : ICategoryDao
    {
        private List<Category> categories = new List<Category>();

        public CategoryDAO()
        {
            //Connection
            string connString = String.Format("server={0};port={1};user id={2};password={3};database={4};SslMode={5}", "51.178.36.43", "3306", "c2_gallium", "DfD2no5UJc_nB", "c2_gallium", "none");
            MySqlConnection mySqlConnection = new MySqlConnection(connString);
            mySqlConnection.Open();

            //Requette SQL
            string stm = "SELECT * FROM Categories ORDER BY name";
            MySqlCommand cmd = new MySqlCommand(stm, mySqlConnection);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                categories.Add(new Category(rdr.GetInt32("category_id"), rdr.GetString("name")));
            }

            mySqlConnection.Close();
        }

        public void CreateCategory(Category cat)
        {
            categories.Add(cat);
        }

        public void DeleteCategory(Category cat)
        {
            categories.Remove(cat);
        }

        public Category GetCategory(Category category)
        {
            return categories.Find(x => x.IdCat == category.IdCat);
        }

        public List<Category> ListALlCategory()
        {
            return categories;
        }

        public void UpdateCategory(Category cat)
        {
            Category category = categories.Find(x => x.IdCat == cat.IdCat);
            category.NomCategory = cat.NomCategory;

        }
    }
}
