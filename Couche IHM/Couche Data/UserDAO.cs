
using Couche_Data;
using Couche_Métier;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gallium_v1.Data
{

    /// <summary>
    /// Interaction avec l'utilisateur sur la base de donnée
    /// </summary>
    public class UserDAO : IUserDAO
    {
        public User ConnectionUser(string indentifiant, string hashPassword)
        {
            throw new NotImplementedException();
        }

        public void CreateCompte(User compte)
        {
            dbsDAO.Instance.Fetch("");
        }

        public User GetCompte(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetComptes()
        {
            throw new NotImplementedException();
        }

        public void RemoveCompte(User compte)
        {
            throw new NotImplementedException();
        }

        public User UpdateCompte(User compte)
        {
            throw new NotImplementedException();
        }
    }

}
