
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{

    /// <summary>
    /// Interaction avec l'utilisateur sur la base de donnée
    /// </summary>
    public class UserDAO : IUserDAO
    {

        private List<User> users = new List<User>();
        private List<Role> roles = new List<Role>();
        public UserDAO()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM users ORDER BY firstname";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                users.Add(new User(rdr.GetInt32("user_id"), rdr.GetString("lastname"), rdr.GetString("firstname"),rdr.GetString("email"),rdr.GetString("password"),rdr.GetInt16("grade_id")));
            }

            //Requette SQL
            string stm2 = "SELECT * FROM grades";
            MySqlCommand cmd2 = new MySqlCommand(stm2, dbsDAO.Instance.Sql);
            cmd2.Prepare();
            rdr.Close();
            //lecture de la requette
            MySqlDataReader rdr2 = cmd2.ExecuteReader();

            while (rdr2.Read())
            {
                roles.Add(new Role(rdr2.GetInt32("grade_id"), rdr2.GetString("name")));
            }
            rdr2.Close();
            dbsDAO.Instance.CloseDatabase();

        }

        public void CreateCompte(User compte)
        {
            users.Add(compte);
        }

        public List<User> GetComptes()
        {
            return users;
        }
        public List<Role> GetRoles()
        {
            return roles;
        }

        public void RemoveCompte(User compte)
        {
            users.Remove(compte);
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
