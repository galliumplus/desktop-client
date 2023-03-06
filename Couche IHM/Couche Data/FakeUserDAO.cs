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
        private Dictionary<int,User> users = new Dictionary<int, User>
        {
            {1, new User(1, "Chabret", "Damien", "damienchab.p@gmail.com", RolePerm.BUREAU) },
            {2,new User(2, "Marteau", "Florian", "flo21p@gmail.com", RolePerm.CA) },
            {3,new User(3, "Pupats", "Lucas", "lucapupat@gmail.com", RolePerm.BUREAU) },
            {4,new User(4, "Prout", "Pipi", "pipicaca@gmail.com", RolePerm.CA) }
        };

        public void CreateCompte(User compte)
        {
            users.Add(compte.ID,compte);
        }

        public User GetCompte(int id)
        {
            return users[id];
        }

        public Dictionary<int,User> GetComptes()
        {
            return users;
        }

        public void RemoveCompte(User compte)
        {
            users.Remove(compte.ID);
        }

        public User UpdateCompte(User compte)
        {
            User newUser = GetCompte(compte.ID);
            newUser = compte;
            return newUser;
        }

        public User ConnectionUser(string indentifiant, string hashPassword)
        {
            return new User(10, "Caca", "Pipi", "Poupou@gmail.com", RolePerm.BUREAU);
        }
    }
}
