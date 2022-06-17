using Gallium_v1.Logique;
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
    public class UserDAO
    {
        private dbsDAO dao = dbsDAO.Instance;
        
        /// <summary>
        /// Permet la connexion de l'utilisateur sur gallium
        /// </summary>
        /// <param name="identifiant"> Identifiant de connexion </param>
        /// <param name="mdp"> mot de passe de l'identifiant </param>
        public static Acompte ConnexionUser(string identifiant, string mdp)
        {
            Acompte user = null;
            try
            {
                dbsDAO.Instance.FetchSQL($"select identifiantUser, motdepasseUser from user where identifiantUser = \"{identifiant}\" and motdepasseUser = \"{mdp}\")");
                dbsDAO.Reader = dbsDAO.Instance.CMD.ExecuteReader();
                if(dbsDAO.Reader.HasRows == true)
                {
                    user = new User(
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CreateUser()
        {

        }

        public static void DeleteUser()
        {

        }

        public static void UpdateUser()
        {

        }

    }
}
