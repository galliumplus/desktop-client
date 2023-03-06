using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    /// <summary>
    /// Utilisateur de l'application
    /// </summary>
    public class User
    {
        private int id;
        private string nom;
        private string prenom;
        private string mail;
        private RolePerm role;
        private string hashedPassword;

        public int ID
        {
            get => id;
        }

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom 
        { 
            get => nom; 
            set => nom = value; 
        }

        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        public string Prenom 
        { 
            get => prenom; 
            set => prenom = value; 
        }

        /// <summary>
        /// Nom complet de l'utilisateur 
        /// </summary>
        public string NomComplet
        {
            get => nom + " " + prenom;
        }

        /// <summary>
        /// mail de l'utilisateur
        /// </summary>
        public string Mail 
        { 
            get => mail; 
            set => mail = value; 
        }

        /// <summary>
        /// Role de l'utilisateur
        /// </summary>
        public RolePerm Role 
        { 
            get => role; 
            set => role = value; 
        }

        public string HashedPassword
        {
            get => hashedPassword;
            set => hashedPassword = value;
        }

        /// <summary>
        /// Constructeur naturelle
        /// </summary>
        /// <param name="nom"> nom de l'utilisateur </param>
        /// <param name="prenom"> prénom de l'utilisateur </param>
        /// <param name="mail"> mail de l'utilisateur </param>
        /// <param name="role"> role de l'utilisateur </param>
        public User(int id, string nom, string prenom, string mail, RolePerm role)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.mail = mail;
            this.role = role;
        }

        public User(User user)
        {
            this.id = user.ID;
            this.nom = user.Nom;
            this.prenom = user.Prenom;
            this.mail = user.Mail;
            this.role = user.Role;
        }

        public User() { }
        public override string ToString()
        {
            return $"{this.mail} {this.nom} {this.prenom} {this.role}";
        }
    }
}
