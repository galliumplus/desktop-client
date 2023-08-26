
using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data.Dao
{

    public class UserDAO : IUserDAO
    {

        public void CreateCompte(User compte)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"INSERT INTO users VALUES (0,'{compte.Prenom}','{compte.Nom}','{compte.Mail}','{compte.HashedPassword}',{compte.IdRole})";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();
            using (MySqlCommand selectCommand = new MySqlCommand("SELECT LAST_INSERT_ID() AS nouvel_id", dbsDAO.Instance.Sql))
            {
                int nouvelId = Convert.ToInt32(selectCommand.ExecuteScalar());
                compte.ID = nouvelId;

            }
            dbsDAO.Instance.CloseDatabase();
        }

        public List<User> GetComptes()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM users ORDER BY firstname";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (rdr.Read())
            {
                users.Add(new User(rdr.GetInt32("user_id"), rdr.GetString("lastname"), rdr.GetString("firstname"), rdr.GetString("email"), rdr.GetString("password"), rdr.GetInt16("grade_id")));
            }


            dbsDAO.Instance.CloseDatabase();
            return users;
        }
        public List<Role> GetRoles()
        {
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm2 = "SELECT * FROM grades";
            MySqlCommand cmd2 = new MySqlCommand(stm2, dbsDAO.Instance.Sql);
            cmd2.Prepare();

            //lecture de la requette
            MySqlDataReader rdr2 = cmd2.ExecuteReader();
            List<Role> roles = new List<Role>();
            while (rdr2.Read())
            {
                roles.Add(new Role(rdr2.GetInt32("grade_id"), rdr2.GetString("name")));
            }
            rdr2.Close();
            dbsDAO.Instance.CloseDatabase();
            return roles;
        }

        public void RemoveCompte(User compte)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"DELETE FROM users WHERE user_id = {compte.ID}";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            dbsDAO.Instance.CloseDatabase();
        }
    

        public void UpdateCompte(User compte)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"UPDATE users SET firstname = '{compte.Prenom}',lastname = '{compte.Nom}',email = '{compte.Mail}',password = '{compte.HashedPassword}',grade_id = {compte.IdRole} WHERE user_id = {compte.ID}";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();
                
            cmd.ExecuteNonQuery();

            dbsDAO.Instance.CloseDatabase();
        }

      
    }

}
