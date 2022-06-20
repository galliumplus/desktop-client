using Gallium_v1.Data;
using Gallium_v1.Logique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gallium_v1.Vue.Modification
{
    /// <summary>
    /// Fenetre pour valider la modification
    /// </summary>
    /// <Author> Damien.C </Author>
    public partial class ValidationUser : Window
    {
        // Information utilisateur
        User user;
        string pasword;
        string modif;

        private bool réel;
        /// <summary>
        /// Permet de savoir si l'utilisateur a été modifié ou non
        /// </summary>
        public bool Réel
        {
            get => réel;
        }

        /// <summary>
        /// Constructeur pour confirmer la supression du personnage
        /// </summary>
        /// <param name="user"></param>
        /// <param name="modif"></param>
        public ValidationUser(User user, string modif)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.user = user;
            this.modif = modif;
        }

        /// <summary>
        /// Constructeur pour confirmer la modification du personnage
        /// </summary>
        /// <param name="user"> Utilisateur en cours de moficiation </param>
        /// <param name="newPwd"> Noueau mot de passe de l'utilisateur</param>
        /// <param name="modif"> Type de moficiation </param>
        public ValidationUser(User user, string newPwd, string modif)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.user = user;
            this.pasword = newPwd;
            this.modif = modif;
        }
        

        /// <summary>
        /// Modifie l'utilisateur si il a le bon mot de passe et identifiant
        /// </summary>
        private void VérificationUser(object sender, RoutedEventArgs e)
        {
            this.réel = false;
            string actualIdentifiant = identifiant.Text;
            string actualPassword = mdp.Password;
            // Si le mot de passe est vide alors garder l'actuel
            if (pasword == "") pasword = mdp.Password;

            // Si l'utilisateur & mdp est bon faire modif & si ce sont les mêmes utilisateur
            if (UserDAO.ReadUser(actualIdentifiant, actualPassword)[0] == user.IdentifiantUser && UserDAO.ReadUser(actualIdentifiant, actualPassword) != null)
            {
                switch (modif)
                {
                    case "Update":
                        // Modifie l'utilisateur
                        user = UserDAO.UpdateUser(actualIdentifiant, actualPassword, user.IdentifiantUser, pasword, user.NomUser, user.PrenomUser, Role.RoleValue(user.RangUser) + 1);
                        this.réel = true;
                        this.Close();
                        break;
                    case "Delete":
                        UserDAO.DeleteUser(user.IdentifiantUser, actualPassword);
                        this.réel = true;
                        this.Close();
                        break;
                        
                }                
            }
            else
            {
                MessageBox.Show("Mauvais mot de passe ou mauvais identifiant");
            }
        }

        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        private void Annuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Lance la méthode pour se connecter quand on appuie sur une touche 
        /// </summary>
        private void EntreeDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.VérificationUser(sender, e);
            }
        }
    }
}
