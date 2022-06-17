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
        /// <summary>
        /// Permet la connexion de l'utilisateur sur gallium
        /// </summary>
        /// <param name="identifiant"> Identifiant de connexion </param>
        /// <param name="mdp"> mot de passe de l'identifiant </param>
        public static User ConnexionUser(string identifiant, string mdp)
        {
            User user = null;

            // Requêtes
            dbsDAO.Instance.RequeteSQL($"select identifiantUser, nomUser, prenomUser, motdepasseUser from `UserTest` where identifiantUser = \"{identifiant}\" and motdepasseUser = \"{mdp}\";");
            dbsDAO.Reader = dbsDAO.Instance.CMD.ExecuteReader();

            // Vérifie s'il y a des résultats
            if (dbsDAO.Reader.HasRows == true)
            {
                string nom = "";
                string prenom = "";
                while (dbsDAO.Reader.Read()) // Tant qu'il lit
                {
                    nom = dbsDAO.Reader.GetString("nomUser");
                    prenom = dbsDAO.Reader.GetString("prenomUser");
                }
                user = new User(nom, prenom);
            }
            else
            {
                MessageBox.Show("Mauvais identifiant ou mot de passe");
            }
                
            
            
            
            return user;
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
