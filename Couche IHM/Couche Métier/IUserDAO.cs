using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
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
        public User GetCompte(string mail);

        /// <summary>
        /// Récupère tous les comptes 
        /// </summary>
        public List<User> GetComptes();
    }
}
