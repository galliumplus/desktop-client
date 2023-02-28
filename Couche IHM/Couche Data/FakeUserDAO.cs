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
        private Dictionary<string,User> users = new Dictionary<string, User>
        {
            {"damienchab.p@gmail.com", new User(1, "Damien", "Chabret", "damienchab.p@gmail.com", RolePerm.BUREAU) },
            {"flo21p@gmail.com",new User(2, "Florian", "Marteau", "flo21p@gmail.com", RolePerm.CA) },
            {"lucapupat@gmail.com",new User(3, "Lucas", "Pupats", "lucapupat@gmail.com", RolePerm.BUREAU) },
            {"pipicaca@gmail.com",new User(4, "Prout", "Pipi", "pipicaca@gmail.com", RolePerm.CA) }
        };

        public void CreateCompte(User compte)
        {
            users.Add(compte.Mail,compte);
        }

        public User GetCompte(string mail)
        {
            return users[mail];
        }

        public Dictionary<string,User> GetComptes()
        {
            return users;
        }

        public void RemoveCompte(User compte)
        {
            users.Remove(compte.Mail);
        }

        public User UpdateCompte(User compte)
        {
            User newUser = GetCompte(compte.Mail);
            newUser = compte;
            return newUser;
        }

        public User ConnectionUser(string indentifiant, string hashPassword)
        {
            return new User(3, "Chabret", "Damien", "damienchab.p@gmail.com", RolePerm.BUREAU);
        }
    }
}
