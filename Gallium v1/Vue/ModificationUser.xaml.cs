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


        public ModificationUser(User u)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.user = u;
            this.chargeUser();
            

            
            
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

        /// <summary>
        /// Méthode qui charge les informations de l'acompte
        /// </summary>
        private void chargeUser()
        {
            this.acompteUser.Text = this.user.Compte;
            this.nomUser.Text = this.user.Nom;
            this.prénomUser.Text = this.user.Prenom;
            this.balanceUser.Text = this.user.BalanceString;
        }
    }
}
