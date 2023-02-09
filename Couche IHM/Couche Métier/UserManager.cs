using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    /// <summary>
    /// Manager des utilisateurs gallium
    /// </summary>
    public class UserManager
    {
        private IUserDAO userDao;
        private List<User> comptes;

        public List<User> Comptes
        {
            get => comptes;
            set => comptes = value;
        }

        public UserManager(IUserDAO userDao)
        {
            this.userDao = userDao;
            this.comptes = new List<User>();
            this.comptes = this.userDao.GetComptes();
        }

        public void CreateCompte(User compte)
        {
            userDao.CreateCompte(compte);
            comptes.Add(compte);
        }

        public User GetCompte(string mail)
        {
            User user = null;
            foreach(User u in comptes)
            {
                if (u.Mail == mail)
                {
                    user = u;
                }
            }
            return user;
        }

        public List<User> GetComptes()
        {
            return comptes;
        }

        public void RemoveCompte(User compte)
        {
            userDao.RemoveCompte(compte);
            comptes.Remove(compte);
        }

        public User UpdateCompte(User compte)
        {
            throw new NotImplementedException();
        }
    }
}
