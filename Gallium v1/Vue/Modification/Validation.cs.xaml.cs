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
        private bool réel;
        public bool Réel
        {
            get => réel;
        }

        public Validation(User user)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.user = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // si l'utiliateur modifié à le même identifiant et même mdp qu'entrée alors modification possible, sinon niet
        }
    }
}
