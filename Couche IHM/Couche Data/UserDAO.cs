
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
                users.Add(new User(rdr.GetInt32("user_id"), rdr.GetString("lastname"), rdr.GetString("firstname"),rdr.GetString("email"),rdr.GetInt16("grade_id")));
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
            User newUser = users.Find(x => x.ID == compte.ID);
            newUser = compte;
        }

        public User ConnectionUser(string indentifiant, string hashPassword)
        {
            return new User(10, "Caca", "Pipi", "Poupou@gmail.com", 1);
        }
    }

}
