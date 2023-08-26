
using Modeles;

namespace Couche_Data.Interfaces
{

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

    }
}
