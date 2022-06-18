using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    public class ListUser
    {

        private static List<User> usersList = new List<User>();
        public static List<User> UsersList { get { return usersList; } set { usersList = value; } }


        public static void AjouteUser(string nom, string prénom, string identifiant, Rang role)
        {
            usersList.Add(new User(nom, prénom, identifiant));
        }

        /// <summary>
        /// Permet de retrouver un produit à partir de son nom
        /// </summary>
        /// <param name="nomProduit"></param>
        /// <returns></returns>
        public static User findUser(string nomUser)
        {
            User u = null;
            foreach (User user in usersList)
            {
                if (user.NomUser.ToUpper().Contains(nomUser.ToUpper()) || nomUser.ToUpper() == user.PrenomUser.ToUpper())
                {
                    u = user;
                    break;
                }
                else if (user.IdentifiantUser.ToUpper().Contains(nomUser.ToUpper()))
                {
                    u = user;
                    break;
                }
            }
            return u;
        }

        public static void RemoveUser(User user)
        {
            usersList.Remove(user);
        }

    }
}
