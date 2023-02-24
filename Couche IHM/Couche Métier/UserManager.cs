using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    /// <summary>
    /// Manager des utilisateurs gallium
    /// </summary>
    public class UserManager
    {
        // Attribut représentant le DAO pour gérer les comptes 
        private IUserDAO userDao;

        // Dictionnaire stockant les comptes temporairement en tant que cache
        private Dictionary<string,User> comptes;

        /// <summary>
        /// Constructeur du manager des comptes
        /// </summary>
        /// <param name="userDao">le DAO des comptes</param>
        public UserManager(IUserDAO userDao)
        {
            this.userDao = userDao;
            this.comptes = new Dictionary<string,User>(this.userDao.GetComptes());
        }

        /// <summary>
        /// Permet de créer un compte
        /// </summary>
        /// <param name="compte">compte à créer</param>
        public void CreateCompte(User compte)
        {
            userDao.CreateCompte(compte);
            comptes.Add(compte.Mail,compte);
        }

        /// <summary>
        /// Permet d'obtenir un compte
        /// </summary>
        /// <param name="infoCompte">information du compte</param>
        /// <returns>un compte</returns>
        public User GetCompte(string infoCompte)
        {
            User user = null;
            if (comptes.ContainsKey(infoCompte))
            {
                user = comptes[infoCompte];
            }
            return user;
        }



        /// <summary>
        /// Permet d'obtenir tous les comptes
        /// </summary>
        /// <returns>tous les comptes</returns>
        public List<User> GetComptes()
        {
            return comptes.Values.ToList();
        }

        /// <summary>
        /// Permet de supprimer un compte
        /// </summary>
        /// <param name="compte">compte à supprimer</param>
        public void RemoveCompte(User compte)
        {
            userDao.RemoveCompte(compte);
            comptes.Remove(compte.Mail);
        }

        /// <summary>
        /// Permet de mettre à jour un compte
        /// </summary>
        /// <param name="compte">compte à modifier</param>
        public void UpdateCompte(User compte)
        {
            userDao.UpdateCompte(compte);
            comptes[compte.Mail] = compte;
        }
    }
}
