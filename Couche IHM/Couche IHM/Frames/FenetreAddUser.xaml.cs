using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

            // Mot de passe
            if (string.IsNullOrEmpty(passwordUser.Text))
            {
                notnull = false;
            }

            // Mot de passe2
            if (string.IsNullOrEmpty(this.password2User.Text))
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
        /// Empêche l'utilisateur d'entrer un espace
        /// </summary>
        private void isSpaceBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
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
                this.copyUser.HashedPassword = passwordUser.Text; // ======================> PENSEZ A CRYPTER
                this.DialogResult = true;
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
