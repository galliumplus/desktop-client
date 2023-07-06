
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.Windows;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FenetreAddUser.xaml
    /// </summary>
    public partial class FenetreAddUser : Window
    {
        private User copyUser;

        public FenetreAddUser(User copyUser)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            FillComboBoxRole();
            this.copyUser = copyUser;

            // Si l'utilisateur vient d'être créer, change le titre
            if(string.IsNullOrEmpty(copyUser.Mail))
            {
                this.titleFenetre.Content = "Création d'un utilisateur";
            }
            else
            {
                this.titleFenetre.Content = "Modification d'un utilisateur";
                this.identifiantUser.Text = this.copyUser.Mail;
                this.nomUser.Text = this.copyUser.Nom;
                this.prenomUser.Text = this.copyUser.Prenom;
                this.roleUser.SelectedValue = this.copyUser.Role;
            }

        }

        /// <summary>
        /// Remplis la comboBox "Rôle" de chaque enum
        /// </summary>
        private void FillComboBoxRole()
        {
            foreach (RolePerm role in Enum.GetValues(typeof(RolePerm)))
            {
                this.roleUser.Items.Add(role);
            }
        }

        /// <summary>
        /// Vérifie que le formulaire n'est pas vide
        /// </summary>
        /// <returns> true si il n'est pas vide, false si il est vide</returns>
        private bool IsUserNotNull()
        {
            bool notnull = true;

            // Identifiant
            if (string.IsNullOrEmpty(identifiantUser.Text))
            {
                notnull = false;
            }

            // Nom
            if (string.IsNullOrEmpty(nomUser.Text))
            {
                notnull = false;
            }

            // Prénom
            if (string.IsNullOrEmpty(prenomUser.Text))
            {
                notnull = false;
            }

            // Rôle
            if (this.roleUser.Text is null)
            {
                notnull = false;
            }

            return notnull;
        }

        /// <summary>
        /// Vérifie que les passwords sont égaux
        /// </summary>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        private bool IsPasswordEquals(string password, string newPassword)
        {
            return password.Equals(newPassword);

        }

        /// <summary>
        /// Valide les changements
        /// </summary>
        private void ValideChange(object sender, RoutedEventArgs e)
        {
            if (IsUserNotNull())
            {
                this.copyUser.Nom = this.nomUser.Text;
                this.copyUser.Prenom = this.prenomUser.Text;
                this.copyUser.Mail = this.identifiantUser.Text;
                this.copyUser.Role = (RolePerm)this.roleUser.SelectedItem;

                // Si le mot de passe est modifié
                if (!string.IsNullOrEmpty(passwordUser.Text) || !string.IsNullOrEmpty(password2User.Text))
                {
                    // Mot de passe identique
                    if (IsPasswordEquals(passwordUser.Text, password2User.Text))
                    {
                        CryptStringToSHA256 hash = new CryptStringToSHA256();
                        this.copyUser.HashedPassword = hash.HashTo256(passwordUser.Text);
                        this.DialogResult = true;
                    }
                }
                else // Si le mot de passe n'est pas modifié
                {
                    this.DialogResult = true;
                }

               
            }
        }

        /// <summary>
        /// Annule les changemements
        /// </summary>
        private void CancelChange(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
