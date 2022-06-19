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
    /// Logique d'interaction pour ModificationUser.xaml
    /// </summary>
    public partial class ModificationUser : Window
    {
        private User oldUser; // Mémorise les informations de base de l'utilisateur en cas de modification qui soit annulés
        private User user;

        /// <summary>
        /// Utilisateur 
        /// </summary>
        public User User { get => user; set => user = value; }

        public ModificationUser(User u)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            // Initialise l'utilisateur 
            this.oldUser = u;
            this.user = u;

            ChargeListRole();
            ChargeUser();
            PositionCaretIndex();


        }

        /// <summary>
        /// Charge dans la comboBox tous les élements de l'énumration rang utilisatuer
        /// </summary>
        private void ChargeListRole()
        {
            foreach (String element in Role.Roles)
            {
                roleUser.Items.Add(element);
            }
        }

        /// <summary>
        /// Initialise position du Caret
        /// </summary>
        private void PositionCaretIndex()
        {

            identifiantUser.CaretIndex = identifiantUser.Text.Length;
            nomUser.CaretIndex = nomUser.Text.Length;
            prénomUser.CaretIndex = prénomUser.Text.Length;
        }

        private void ValiderModif(object sender, RoutedEventArgs e)
        {
            user.IdentifiantUser = identifiantUser.Text;
            user.PrenomUser = prénomUser.Text;
            // manque rang
            user.NomUser = nomUser.Text;

            Validation modif = new Validation(user, mdpUser.Password);
            this.Hide();
            modif.ShowDialog();
            this.Show();
            if (modif.Réel == true)
            {
                this.Close();
                MessageBox.Show("L'utilisateur à été modifié !", "Utilisateur modifié", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        private void AnnulerModif(object sender, RoutedEventArgs e)
        {
            this.User = this.oldUser;
            this.Close();
        }

        private void ChargeUser()
        {
            identifiantUser.Text = user.IdentifiantUser;
            nomUser.Text = user.NomUser;
            prénomUser.Text = user.PrenomUser;
            roleUser.SelectedIndex = Role.RoleValue(user.RangUser);
        }

    }
}
