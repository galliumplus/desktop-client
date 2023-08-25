

using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{
    public class CategoryDAO : ICategoryDao
    {


        public void CreateCategory(Category cat)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"INSERT INTO categories VALUES(0,'{cat.NomCategory}',{Convert.ToInt16(cat.Visible)})";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();


            cmd.ExecuteNonQuery();

            using (MySqlCommand selectCommand = new MySqlCommand("SELECT LAST_INSERT_ID() AS nouvel_id", dbsDAO.Instance.Sql))
            {
                int nouvelId = Convert.ToInt32(selectCommand.ExecuteScalar());
                cat.IdCat = nouvelId;
            }

            dbsDAO.Instance.CloseDatabase();
        }

        public void DeleteCategory(Category cat)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"DELETE FROM categories where category_id = {cat.IdCat}";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();

         
            dbsDAO.Instance.CloseDatabase();
        }

       

        public List<Category> ListALlCategory()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM categories ORDER BY name";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Category> categories = new List<Category>(); 
            while (rdr.Read())
            {
                categories.Add(new Category(rdr.GetInt32("category_id"), rdr.GetString("name"), rdr.GetBoolean("activated")));
            }
            rdr.Close();
            dbsDAO.Instance.CloseDatabase();
            return categories;
        }

        public void UpdateCategory(Category cat)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"UPDATE categories SET name ='{cat.NomCategory}',activated={Convert.ToInt16(cat.Visible)} WHERE category_id = {cat.IdCat}";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();
            cmd.ExecuteNonQuery();



            dbsDAO.Instance.CloseDatabase();

        }
    }
}
