using Couche_Data.Interfaces;
using GalliumPlusApi.Dao;
using Modeles;


namespace Couche_Métier.Manager
{
    /// <summary>
    /// Manager des utilisateurs gallium
    /// </summary>
    public class UserManager
    {
        #region attributes
        /// <summary>
        /// Dao pour gérer les données des users
        /// </summary>        
        private IUserDAO userDao;

        /// <summary>
        /// Liste des comptes
        /// </summary>
        private List<User> comptes;

        /// <summary>
        /// Liste des rôles
        /// </summary>
        private List<Role> roles;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du manager des comptes
        /// </summary>
        public UserManager()
        {
            this.userDao = new UserDao();
            this.comptes = this.userDao.GetComptes();
            this.roles = this.userDao.GetRoles();
        }
        #endregion

        #region methods

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
            comp.HashedPassword = compte.HashedPassword;
            comp.Nom = compte.Nom;
            comp.Prenom = compte.Prenom;
            comp.Mail = compte.Mail;
            comp.IdRole = compte.IdRole;
        }

        /// <summary>
        /// Permet de se connecter à un utilisatuer
        /// </summary>
        /// <param name="identifiant"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User? ConnectCompte(string identifiant,string password)
        {
            SessionDao dao = new();

            var result = dao.LogIn(identifiant, password);
            User? user = result?.Item1;
            Role? role = result?.Item2;

            if (role?.Name == "Adhérent")
            {
                return null;
            }
            else
            {
                return user;
            }
        }
        #endregion
    }
}
