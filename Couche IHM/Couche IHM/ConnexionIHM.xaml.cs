using Couche_Data;
using Couche_IHM.ImagesProduit;
using Couche_Métier;
using Microsoft.Win32;
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
        private IUserDAO userDAO = new FakeUserDAO();

        public ConnexionIHM()
        {
            InitializeComponent();
            ImageManager blobConverter = new ImageManager();
            
        }

        /// <summary>
        /// Permet de se connecter à son compte et de créer la mainWindows
        /// </summary>
        private void ConnectToAccount(object sender, RoutedEventArgs e)
        {
            // Vérifie la connexion d'un utilisateur
            User userConnection = userDAO.ConnectionUser(identifiantBox.Text, passwordBox.Password);

            // Vérifie si l'utilisateur réussi à se connecter
            if (userConnection != null)
            {
                MainWindow mainWindow = new MainWindow(userConnection);
                mainWindow.Show();
                this.Close();
            }
            
        }
    }
}
