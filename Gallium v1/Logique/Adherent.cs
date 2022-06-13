using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    public static class Adherent
    {
        
        private static List<User> users = new List<User>();
        /// <summary>
        /// Liste des utilisateurs
        /// </summary>
        public static List<User> Users 
        { 
            get 
            { 
                return users; 
            } 
        }


        /// <summary>
        /// Permet d'ajouter un utilisateur
        /// </summary>
        /// <param name="nom"></param>
        public static void ajoutUser(String nom,string prenom,string compte,double balance,string password)
        {
            users.Add(new User(nom,prenom,compte,balance,password));
        }

        /// <summary>
        /// Permet de retrouver un produit à partir de son nom
        /// </summary>
        /// <param name="nomProduit"></param>
        /// <returns></returns>
        public static User findUser(string nomUser)
        {
            User u = null;
            foreach (User p in users)
            {
                if (p.Nom.ToUpper().Contains(nomUser.ToUpper()) || nomUser.ToUpper() == p.Compte.ToUpper())
                {
                    u = p;
                    break;
                }
                else if (p.Compte.ToUpper().Contains(nomUser.ToUpper()))
                {
                    u = p;
                    break;
                }
            }
            return u;
        }

        public static void removeUser(User user)
        {
            Users.Remove(user);
        }
    }
}
