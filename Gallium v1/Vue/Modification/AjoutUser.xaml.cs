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
    /// Logique d'interaction pour AjoutUser.xaml
    /// </summary>
    public partial class AjoutUser : Window
    {
        public AjoutUser()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            ChargeListRole();
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

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (identifiant.Text != "" && nom.Text != "" && prénom.Text != "" && mdp1.Password != "" && mdp1.Password == mdp2.Password)
            {
                error.Content = "";
                int role = roleUser.SelectedIndex + 1;
                UserDAO.CreateUser(identifiant.Text, mdp1.Password, nom.Text, prénom.Text, role);
            }
            else if (mdp1.Password != mdp2.Password)
            {
                error.Content = "Mdp 1 et mdp2 pas pareil";
            }

            this.Close();
            
        }
    }
}
