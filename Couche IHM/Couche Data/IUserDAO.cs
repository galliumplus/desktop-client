
using Modeles;

namespace Couche_Data
{
    /// <summary>
    /// Compte DAO
    /// </summary>
    public interface IUserDAO
    {
        /// <summary>
        /// Créer un compte
        /// </summary>
        public void CreateCompte(User compte);

        /// <summary>
        /// Enlève un compte
        /// </summary>
        public void RemoveCompte(User compte);

        /// <summary>
        /// Met à jour un compte
        /// </summary>
        public void UpdateCompte(User compte);


        /// <summary>
        /// Récupère tous les comptes 
        /// </summary>
        public List<User> GetComptes();

        /// <summary>
        /// Récupère tous les comptes 
        /// </summary>
        public List<Role> GetRoles();

        /// <summary>
        /// Connection d'un utilisateur à gallium
        /// </summary>
        /// <param name="indentifiant"> identifiant de l'utilisateur </param>
        /// <param name="hashPassword"> mot de passe hashé de l'utilisateur </param>
        /// <returns> un utilisateur</returns>
        public User ConnectionUser(string indentifiant, string hashPassword);
    }
}
