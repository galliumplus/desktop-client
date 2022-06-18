using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    public static class Adherents
    {
        
        private static List<Acompte> acomptes = new List<Acompte>();
        /// <summary>
        /// Liste des utilisateurs
        /// </summary>
        public static List<Acompte> Acomptes
        { 
            get 
            { 
                return acomptes; 
            } 
        }


        /// <summary>
        /// Permet d'ajouter un utilisateur
        /// </summary>
        /// <param name="nom"></param>
        public static void ajoutAcompte(String nom,string prenom,string compte,double balance,string password)
        {
            acomptes.Add(new Acompte(nom,prenom,compte,balance,password));
        }

        /// <summary>
        /// Permet de retrouver un produit à partir de son nom
        /// </summary>
        /// <param name="nomProduit"></param>
        /// <returns></returns>
        public static Acompte findAcompte(string nomUser)
        {
            Acompte u = null;
            foreach (Acompte p in acomptes)
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
        public static void removeAcompte(Acompte user)
        {
            Acomptes.Remove(user);
        }

        public static string calculPlusGrosAcompte()
        {
            Acompte user = null;

            foreach(Acompte p in acomptes)
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
