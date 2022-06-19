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
            string requete = $"SELECT identifiant, nom, prenom, nomrole FROM User INNER join Role on User.idRole = Role.idRole where identifiant = \"{identifiant}\" and password = \"{mdp}\";";

            // Requêtes
            dbsDAO.Instance.RequeteSQL(requete);
            dbsDAO.Reader = dbsDAO.CMD.ExecuteReader();

            // Vérifie s'il y a des résultats
            if (dbsDAO.Reader.HasRows == true)
            {
                string nom = "";
                string prenom = "";
                string role = "";
                while (dbsDAO.Reader.Read()) // Tant qu'il lit
                {
                    nom = dbsDAO.Reader.GetString("nom");
                    prenom = dbsDAO.Reader.GetString("prenom");
                    role = dbsDAO.Reader.GetString("nomrole");
                    
                }
                user = new User(nom, prenom, identifiant, role);
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
        public static void CreateUser(string identifiant, string password, string nom, string prenom, int idRole)
        {
            string requete = $"Insert into User values(null, \"{identifiant}\", \"{password}\", \"{nom}\", \"{prenom}\",{idRole})";
            dbsDAO.Instance.RequeteSQL(requete);
        }


        /// <summary>
        /// Suprimme l'utilisateur
        /// </summary>
        public static void DeleteUser(string identifiant)
        {
            string requete = $"Delete from User where identifiant = \"{identifiant}\"";
            dbsDAO.Instance.RequeteSQL(requete);
        }

        /// <summary>
        /// Modifie l'utilisateur
        /// </summary>
        public static User UpdateUser(string actualIdentifiant, string actualMdp, string newidentifiant, string password, string nom, string prénom, int idRole)
        {
            string requete = $"UPDATE User SET identifiant=\"{newidentifiant}\",nom=\"{nom}\",prenom=\"{prénom}\",idRole={idRole++} WHERE identifiant = \"{actualIdentifiant}\" and password = \"{actualMdp}\"";
            dbsDAO.Instance.RequeteSQL(requete);

            User user = new User(nom, prénom, newidentifiant, Role.Roles[idRole]);
            return user;
        }

        /// <summary>
        /// Lis tous les utilisateurs de la base de donnée
        /// </summary>
        public static List<User> ReadAllUser()
        {
            List<User> users = new List<User>();

            // Requête
            string requete = "SELECT idUser, identifiant, nom, prenom, nomRole FROM User INNER join Role on User.idRole = Role.idRole";

            // Lecture de la requête
            dbsDAO.CMD = new MySqlCommand(requete, dbsDAO.Instance.Sql);
            dbsDAO.Reader = dbsDAO.CMD.ExecuteReader();

            while (dbsDAO.Reader.Read())
            {
                users.Add(new User(dbsDAO.Reader.GetString("nom"), dbsDAO.Reader.GetString("prenom"),dbsDAO.Reader.GetString("identifiant"), dbsDAO.Reader.GetString("nomRole")));
                
            }
            dbsDAO.Reader.Close();

            return users;
        }

    }
}
