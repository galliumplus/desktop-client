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

namespace Gallium_v1.Vue
{
    /// <summary>
    /// Logique d'interaction pour ModificationUser.xaml
    /// </summary>
    public partial class ModificationUser : Window
    {
        private User user;

        /// <summary>
        /// Utilisateur en cours de modification
        /// </summary>
        public User User 
        { 
            get => user; 
    
        }

        public ModificationUser(User u)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.user = u;
            this.chargeUser();
            acompteUser.Focus();

        }

        /// <summary>
        /// Valide les modifications faite et de mettre à jour l'utilisateur
        /// </summary>
        /// <Author> Damien.C </Author>
        private void ValiderModif(object sender, RoutedEventArgs e)
        {
            // demande si l'utilisateur est sur de la modification
            MessageBoxResult validation = MessageBox.Show("Vous allez modifier cet utilisateur.", "Modifier l'utilisateur ?", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            
            if (validation.Equals(MessageBoxResult.OK))
            {
                double balance = Convert.ToDouble(this.balanceUser.Text);
                user.Compte = this.acompteUser.Text;
                user.Nom = this.nomUser.Text;
                user.Prenom = this.prénomUser.Text;
                user.Balance = balance;
                this.Close();
            }
        }

        /// <summary>
        /// Permet d'annuler les modifications
        /// </summary>
        /// <Author> Damien.C </Author>
        private void AnnulerModif(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Méthode qui charge les informations de l'acompte
        /// </summary>
        /// <Author> Damien.C </Author>
        private void chargeUser()
        {
            this.acompteUser.Text = this.user.Compte;
            this.nomUser.Text = this.user.Nom;
            this.prénomUser.Text = this.user.Prenom;
            this.balanceUser.Text = this.user.Balance.ToString();
        }

    }
}
