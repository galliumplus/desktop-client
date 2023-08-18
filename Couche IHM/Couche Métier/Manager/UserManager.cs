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
        private List<User> comptes;

        private List<Role> roles;

        /// <summary>
        /// Constructeur du manager des comptes
        /// </summary>
        /// <param name="userDao">le DAO des comptes</param>
        public UserManager()
        {
            this.userDao = new UserDAO();
            this.comptes = this.userDao.GetComptes();
            this.roles = this.userDao.GetRoles();
        }

        /// <summary>
        /// Permet de créer un compte
        /// </summary>
        /// <param name="compte">compte à créer</param>
        public void CreateCompte(User compte)
        {
            userDao.CreateCompte(compte);
            comptes.Add(compte);
        }



        /// <summary>
        /// Permet d'obtenir tous les comptes
        /// </summary>
        /// <returns>tous les comptes</returns>
        public List<User> GetComptes()
        {
            return comptes;
        }

        // <summary>
        /// Permet d'obtenir tous les comptes
        /// </summary>
        /// <returns>tous les comptes</returns>
        public List<Role> GetRoles()
        {
            return roles;
        }

        /// <summary>
        /// Permet de supprimer un compte
        /// </summary>
        /// <param name="compte">compte à supprimer</param>
        public void RemoveCompte(User compte)
        {
            userDao.RemoveCompte(compte);
            comptes.Remove(compte);
        }

        /// <summary>
        /// Permet de mettre à jour un compte
        /// </summary>
        /// <param name="compte">compte à modifier</param>
        public void UpdateCompte(User compte)
        {
            userDao.UpdateCompte(compte);
            User comp =comptes.Find(x => x.ID == compte.ID);
            comp.Nom = compte.Nom;
            comp.Prenom = compte.Prenom;
            comp.Mail = compte.Mail;
            comp.IdRole = compte.IdRole;
        }
    }
}
