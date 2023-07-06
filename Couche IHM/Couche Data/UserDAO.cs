
using Modeles;

namespace Couche_Data
{

    /// <summary>
    /// Interaction avec l'utilisateur sur la base de donnée
    /// </summary>
    public class UserDAO : IUserDAO
    {
        public User ConnectionUser(string indentifiant, string hashPassword)
        {
            throw new NotImplementedException();
        }

        public void CreateCompte(User compte)
        {
            dbsDAO.Instance.Fetch("");
        }

        public User GetCompte(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int,User> GetComptes()
        {
            throw new NotImplementedException();
        }

        public void RemoveCompte(User compte)
        {
            throw new NotImplementedException();
        }

        public User UpdateCompte(User compte)
        {
            throw new NotImplementedException();
        }
    }

}
