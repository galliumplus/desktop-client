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
    /// Logique d'interaction pour Validation.xaml
    /// </summary>
    public partial class Validation : Window
    {
        User user;
        string pasword;
        private bool réel;
        public bool Réel
        {
            get => réel;
        }

        public Validation(User user, string pwd)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.user = user;
            this.pasword = pwd;
        }

        /// <summary>
        /// Vérifie que l'utilisateur est le bon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VérificationUser(object sender, RoutedEventArgs e)
        {
            // si l'utiliateur modifié à le même identifiant et même mdp qu'entrée alors modification possible, sinon niet
            string actualIdentifiant = identifiant.Text;
            string actualPassword = mdp.Password;
            if (pasword == "") pasword = mdp.Password;

            // Si l'utilisateur et le mdp est bon
            if (UserDAO.ReadUser(actualIdentifiant, actualPassword) != null)
            {
                user = UserDAO.UpdateUser(actualIdentifiant, actualPassword, user.IdentifiantUser, pasword , user.NomUser, user.PrenomUser, Role.RoleValue(user.RangUser) + 1);
                this.réel = true;
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Mauvais mot de passe ou mauvais identifiant");
            }
        }

        private void Annuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
            
    }
}
