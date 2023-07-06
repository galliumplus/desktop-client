
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
        public User UpdateCompte(User compte);

        /// <summary>
        /// Récupère un compte
        /// </summary>
        public User GetCompte(int id);

        /// <summary>
        /// Récupère tous les comptes 
        /// </summary>
        public Dictionary<int,User> GetComptes();

        /// <summary>
        /// Connection d'un utilisateur à gallium
        /// </summary>
        /// <param name="indentifiant"> identifiant de l'utilisateur </param>
        /// <param name="hashPassword"> mot de passe hashé de l'utilisateur </param>
        /// <returns> un utilisateur</returns>
        public User ConnectionUser(string indentifiant, string hashPassword);
    }
}
