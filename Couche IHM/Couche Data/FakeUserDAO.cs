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
            new User("Damien", "Chabret", "damienchab.p@gmail.com", RolePerm.BUREAU),
            new User("Florian", "Marteau", "flo21p@gmail.com", RolePerm.CA),
            new User("Lucas", "Pupats", "lucapupat@gmail.com", RolePerm.BUREAU),
            new User("Prout", "Pipi", "pipicaca@gmail.com", RolePerm.CA)
        };

        public void CreateCompte(User compte)
        {
            users.Add(compte);
        }

        public User GetCompte(string mail)
        {
            User user = null;
            foreach(User u in users)
            {
                if(u.Mail == mail)
                {
                    user = u;
                }
            }
            return user;
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
            User newUser = GetCompte(compte.Mail);
            newUser = compte;
            return newUser;
        }
    }
}
