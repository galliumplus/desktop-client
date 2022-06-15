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

        private string acompte;
        private string nom;
        private string prenom;
        private double balance;
        private string mdp;


        public ModificationUser(User u)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.user = u;
            this.nom = this.user.Nom.Split(' ')[0];
            this.prenom = this.user.Nom.Split(' ')[1];
            this.acompte = this.user.Compte;
            this.balance = this.user.Balance;
            
        }

        /// <summary>
        /// Permet de valider les modifications faite
        /// </summary>
        private void ValiderModif(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Permet d'annuler les modifications
        /// </summary>
        private void AnnulerModif(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
