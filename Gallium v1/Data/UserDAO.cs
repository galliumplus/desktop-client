using Gallium_v1.Logique;
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
    public class UserDAO
    {
        /// <summary>
        /// Permet la connexion de l'utilisateur sur gallium
        /// </summary>
        /// <param name="identifiant"> Identifiant de connexion </param>
        /// <param name="mdp"> mot de passe de l'identifiant </param>
        /// <Author> Damien C.</Author>
        public static User ConnexionUser(string identifiant, string mdp)
        {
            User user = null;

            // Requêtes
            dbsDAO.Instance.RequeteSQL($"select identifiant, nom, prenom, password from `User` where identifiant = \"{identifiant}\" and password = \"{mdp}\";");
            dbsDAO.Reader = dbsDAO.CMD.ExecuteReader();

            // Vérifie s'il y a des résultats
            if (dbsDAO.Reader.HasRows == true)
            {
                string nom = "";
                string prenom = "";
                while (dbsDAO.Reader.Read()) // Tant qu'il lit
                {
                    nom = dbsDAO.Reader.GetString("nom");
                    prenom = dbsDAO.Reader.GetString("prenom");
                }
                user = new User(nom, prenom, identifiant);
            }
            else
            {
                MessageBox.Show("Mauvais identifiant ou mot de passe");
            }

            dbsDAO.Reader.Close();
            return user;
        }

        /// <summary>
        /// Créer l'utilisateur
        /// </summary>
        public static void CreateUser()
        {

        }


        /// <summary>
        /// Suprimme l'utilisateur
        /// </summary>
        public static void DeleteUser()
        {

        }

        /// <summary>
        /// Modifie l'utilisateur
        /// </summary>
        public static void UpdateUser()
        {

        }

        /// <summary>
        /// Lis tous les utilisateurs de la base de donnée
        /// </summary>
        public static List<User> ReadAllUser()
        {
            List<User> users = new List<User>();

            // Requête
            string requete = "Select * from User";

            // Lecture de la requête
            dbsDAO.CMD = new MySqlCommand(requete, dbsDAO.Instance.Sql);
            dbsDAO.Reader = dbsDAO.CMD.ExecuteReader();

            while (dbsDAO.Reader.Read())
            {
                users.Add(new User(dbsDAO.Reader.GetString("identifiant"), dbsDAO.Reader.GetString("nom"), dbsDAO.Reader.GetString("prenom")));
                
            }
            dbsDAO.Reader.Close();

            return users;
        }

    }
}
