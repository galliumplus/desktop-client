using Couche_Data;
using Modeles;


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
        private Dictionary<int,User> comptes;

        /// <summary>
        /// Constructeur du manager des comptes
        /// </summary>
        /// <param name="userDao">le DAO des comptes</param>
        public UserManager(IUserDAO userDao)
        {
            this.userDao = userDao;
            this.comptes = new Dictionary<int,User>(this.userDao.GetComptes());
        }

        /// <summary>
        /// Permet de créer un compte
        /// </summary>
        /// <param name="compte">compte à créer</param>
        public void CreateCompte(User compte)
        {
            userDao.CreateCompte(compte);
            comptes.Add(compte.ID,compte);
        }

        /// <summary>
        /// Permet d'obtenir un compte
        /// </summary>
        /// <param name="infoCompte">information du compte</param>
        /// <returns>un compte</returns>
        public User GetCompte(string infoCompte)
        {
            User user = null;

            foreach(User compte in comptes.Values)
            {
                if(compte.Mail == infoCompte)
                {
                    user = compte;
                }
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
            comptes.Remove(compte.ID);
        }

        /// <summary>
        /// Permet de mettre à jour un compte
        /// </summary>
        /// <param name="compte">compte à modifier</param>
        public void UpdateCompte(User compte)
        {
            userDao.UpdateCompte(compte);
            comptes[compte.ID] = compte;
        }
    }
}
