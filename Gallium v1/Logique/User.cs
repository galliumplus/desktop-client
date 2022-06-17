using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    public class User
    {
        private string identifiant;
        /// <summary>
        /// Identifiant de l'utilisateur
        /// </summary>
        public string Identifiant
        { 
            get => identifiant; 
            set => identifiant = value; 
        }
        

        private string nom;
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom { get => nom; set => nom = value; }

        private string prenom;
        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        public string Prenom 
        { 
            get => prenom; 
            set => prenom = value; 
        }

        /// <summary>
        /// Constructeur de l'utilisateur
        /// </summary>
        /// <param name="nom"> nom de l'utilisateur </param>
        /// <param name="prenom"> prénom de l'utilisateur </param>
        public User(string nom, string prenom) // manque un rôle
        {
            this.nom = nom;
            this.prenom = prenom;

        }

    }
}
