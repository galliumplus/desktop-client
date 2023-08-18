

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
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM Categories ORDER BY name";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                categories.Add(new Category(rdr.GetInt32("category_id"), rdr.GetString("name"), rdr.GetBoolean("activated")));
            }

            dbsDAO.Instance.CloseDatabase();


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

            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"UPDATE Categories SET name ='{cat.NomCategory}',activated={Convert.ToInt16(cat.Visible)} WHERE category_id = {cat.IdCat}";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();
            cmd.ExecuteNonQuery();



            dbsDAO.Instance.CloseDatabase();

        }
    }
}
