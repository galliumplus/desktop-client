using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    public static class Adherents
    {
        
        private static List<Acompte> users = new List<Acompte>();
        /// <summary>
        /// Liste des utilisateurs
        /// </summary>
        public static List<Acompte> Users 
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
            users.Add(new Acompte(nom,prenom,compte,balance,password));
        }

        /// <summary>
        /// Permet de retrouver un produit à partir de son nom
        /// </summary>
        /// <param name="nomProduit"></param>
        /// <returns></returns>
        public static Acompte findUser(string nomUser)
        {
            Acompte u = null;
            foreach (Acompte p in users)
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

        /// <summary>
        /// Permet de supprimer un utilisateur
        /// </summary>
        /// <param name="user"></param>
        public static void removeUser(Acompte user)
        {
            Users.Remove(user);
        }

        public static string calculPlusGrosAcompte()
        {
            Acompte user = null;

            foreach(Acompte p in users)
            {
                if (user == null || p.Balance > user.Balance )
                {
                    user = p;
                }
            }
            return $"{user.Nom} {user.Prenom} : {user.Balance}€";
        }
    }
}
