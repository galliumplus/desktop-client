using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Data
{
    public class FakeUserDAO : IUserDAO
    {
        private List<User> users = new List<User>()
        {
            new User(1, "Damien", "Chabret", "damienchab.p@gmail.com", RolePerm.BUREAU),
            new User(2, "Florian", "Marteau", "flo21p@gmail.com", RolePerm.CA),
            new User(3, "Lucas", "Pupats", "lucapupat@gmail.com", RolePerm.BUREAU),
            new User(4, "Prout", "Pipi", "pipicaca@gmail.com", RolePerm.CA)
        };

        public void CreateCompte(User compte)
        {
            users.Add(compte);
        }

        public User GetCompte(int id)
        {
            return null;
        }

        public List<User> GetComptes()
        {
            return users;
        }

        public void RemoveCompte(User compte)
        {
            users.Remove(compte);
        }

        public User UpdateCompte(User compte)
        {
            User newUser = GetCompte(compte.ID);
            newUser = compte;
            return newUser;
        }

        public User ConnectionUser(string indentifiant, string hashPassword)
        {
            return new User(3, "Chabret", "Damien", "damienchab.p@gmail.com", RolePerm.CA);
        }
    }
}
