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
        public string IdentifiantUser
        { 
            get => identifiant; 
            set => identifiant = value; 
        }
        

        private string nom;
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string NomUser 
        { 
            get => nom; 
            set => nom = value; 
        }


        private string prenom;
        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        public string PrenomUser 
        { 
            get => prenom; 
            set => prenom = value; 
        }

        private string rang;
        /// <summary>
        /// Rang de l'utilisateur
        /// </summary>
        public string RangUser
        {
            get => rang;
            set => rang = value;
        }

        /// <summary>
        /// Constructeur de l'utilisateur
        /// </summary>
        /// <param name="nom"> nom de l'utilisateur </param>
        /// <param name="prenom"> prénom de l'utilisateur </param>
        public User(string nom, string prenom, string identifiant, string role) // manque un rôle
        {
            this.nom = nom;
            this.prenom = prenom;
            this.identifiant = identifiant;
            this.rang = role;

        }

    }
}
