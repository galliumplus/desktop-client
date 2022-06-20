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
    /// <Author> Damien.C </Author>
    public static class UserDAO
    {
        /// <summary>
        /// Permet la connexion de l'utilisateur sur gallium
        /// </summary>
        /// <param name="identifiant"> Identifiant de connexion </param>
        /// <param name="mdp"> mot de passe de l'identifiant </param>
        public static User ConnexionUser(string identifiant, string mdp)
        {
            User user = null;
            List<string> infoUser = null;
            string requete = $"SELECT identifiant, nom, prenom, nomrole FROM User INNER join Role on User.idRole = Role.idRole where identifiant = \"{identifiant}\" and password = \"{mdp}\";";

            // Requêtes
            dbsDAO.Instance.FetchSQL(requete);

            if (ReadUser(identifiant, mdp) != null)
            {
                infoUser = ReadUser(identifiant, mdp);
                user = new User(infoUser[0], infoUser[2], infoUser[3], infoUser[4]);
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
            dbsDAO.Instance.FetchSQL(requete);
        }

        /// <summary>
        /// Suprimme l'utilisateur
        /// </summary>
        /// <param name="identifiant"> Identifiant de l'utilisateur </param>
        /// <param name="password"> Mot de passe de l'utilisateur </param>
        public static void DeleteUser(string identifiant, string password)
        {
            string requete = $"Delete from User where identifiant = \"{identifiant}\" and password = \"{password}\"";
            dbsDAO.Instance.FetchSQL(requete);
        }

        /// <summary>
        /// Modifie l'utilisateur
        /// </summary>
        /// <param name="actualIdentifiant"> Identifiant actuel de l'utilisateur </param>
        /// <param name="actualMdp"> Mot de passe actuel de l'utilisateur</param>
        /// <param name="newIdentifiant"> Nouvelle identifiant</param>
        /// <param name="newPassword"> Nouveau mot de passe</param>
        /// <param name="newName"> Nouveau nom </param>
        /// <param name="newNickname"> Nouveau prénom</param>
        /// <param name="newIdRole"> Nouveau rôle </param>
        /// <returns> L'utilisateur modifié </returns>
        public static User UpdateUser(string actualIdentifiant, string actualMdp, string newIdentifiant, string newPassword, string newName, string newNickname, int newIdRole)
        {
            string requete = $"UPDATE User SET identifiant=\"{newIdentifiant}\",password=\"{newPassword}\", nom=\"{newName}\",prenom=\"{newNickname}\",idRole={newIdRole++} WHERE identifiant = \"{actualIdentifiant}\" and password = \"{actualMdp}\"";
            dbsDAO.Instance.FetchSQL(requete);

            User user = new User(newIdentifiant, newName, newNickname, Role.Roles[newIdRole]);
            return user;
        }

        /// <summary>
        /// lis l'utilisateur souhaité
        /// </summary>
        /// <param name="identif"> identifiant de l'utilisateur </param>
        /// <param name="mdp"> mot de passe de l'utilisateur </param>
        /// <returns> Information de l'utilisateur </returns>
        public static List<string> ReadUser(string identif, string mdp)
        {
            List<String> user = null;
            string requete = $"SELECT identifiant, password, nom, prenom, nomrole FROM User INNER join Role on User.idRole = Role.idRole where identifiant = \"{identif}\" and password = \"{mdp}\";";

            dbsDAO.Instance.FetchSQL(requete);
            dbsDAO.Reader = dbsDAO.CMD.ExecuteReader();

            // Vérifie s'il y a des résultats
            if (dbsDAO.Reader.HasRows == true)
            {
                string identifiant = "";
                string password = "";
                string nom = "";
                string prenom = "";
                string role = "";

                // Tant qu'il lit
                while (dbsDAO.Reader.Read()) 
                {
                    // Donne les résultats aux variables selon les noms de colonnes
                    identifiant = dbsDAO.Reader.GetString("identifiant");
                    password = dbsDAO.Reader.GetString("password");
                    nom = dbsDAO.Reader.GetString("nom");
                    prenom = dbsDAO.Reader.GetString("prenom");
                    role = dbsDAO.Reader.GetString("nomrole");
                }

                // Information de l'utilisateur
                user = new List<string>()
                {
                    identifiant,
                    password,
                    nom,
                    prenom,
                    role
                };
            }
            dbsDAO.Reader.Close();

            return user;
        }

        /// <summary>
        /// Lis tous les utilisateurs de la base de donnée
        /// </summary>
        /// <returns> Tous les utilisateurs </returns>
        public static List<User> ReadAllUser()
        {
            // Liste des utilisateurs
            List<User> users = new List<User>();

            // Requête
            string requete = "SELECT idUser, identifiant, nom, prenom, nomRole FROM User INNER join Role on User.idRole = Role.idRole";

            // Lecture de la requête
            dbsDAO.CMD = new MySqlCommand(requete, dbsDAO.Instance.Sql);
            dbsDAO.Reader = dbsDAO.CMD.ExecuteReader();

            // Tant qu'il lit
            while (dbsDAO.Reader.Read())
            {
                users.Add(new User(dbsDAO.Reader.GetString("identifiant"), dbsDAO.Reader.GetString("nom"), dbsDAO.Reader.GetString("prenom"), dbsDAO.Reader.GetString("nomRole")));
            }
            dbsDAO.Reader.Close();

            return users;
        }

    }
}
