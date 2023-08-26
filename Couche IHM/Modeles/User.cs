

namespace Modeles
{
    /// <summary>
    /// Utilisateur de l'application
    /// </summary>
    public class User
    {
        #region attributes
        private int id;
        private string nom;
        private string prenom;
        private string mail;
        private int role = 8;
        private string hashedPassword;
        #endregion

        #region constructors
        /// <summary>
        /// Constructeur naturelle
        /// </summary>
        /// <param name="nom"> nom de l'utilisateur </param>
        /// <param name="prenom"> prénom de l'utilisateur </param>
        /// <param name="mail"> mail de l'utilisateur </param>
        /// <param name="role"> role de l'utilisateur </param>
        public User(int id, string nom, string prenom, string mail, string password, int role)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.mail = mail;
            this.hashedPassword = password;
            this.role = role;
        }


        /// <summary>
        /// Constructeur vide de l'utilisateur
        /// </summary>
        public User() { }
        #endregion

        #region properties
        /// <summary>
        /// ID de l'utilisateur
        /// </summary>
        public int ID
        {
            get => id;
            set => id = value;
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
        public int IdRole 
        { 
            get => role; 
            set => role = value; 
        }

        /// <summary>
        /// Mot de passe hashé de l'utilisateur
        /// </summary>
        public string HashedPassword
        {
            get => hashedPassword;
            set => hashedPassword = value;
        }
        #endregion
    }
}
