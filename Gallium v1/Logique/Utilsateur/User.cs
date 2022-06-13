using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Logique.IHM
{
    public class UserIhm
    {
        private string nom;

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom
        {
            get => nom;
        }

        public UserIhm(String nom)
        {
            this.nom = nom;
        }



    }
}
