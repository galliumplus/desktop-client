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

namespace Couche_IHM
{
    /// <summary>
    /// Logique d'interaction pour ConnexionIHM.xaml
    /// </summary>
    public partial class ConnexionIHM : Window
    {
        public ConnexionIHM()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Permet de se connecter à son compte et de créer la mainWindows
        /// </summary>
        private void ConnectToAccount(object sender, RoutedEventArgs e)
        {
            bool isConneted = true;

            // Vérifie si l'utilisateur réussi à se connecter
            if (isConneted)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            
        }
    }
}
