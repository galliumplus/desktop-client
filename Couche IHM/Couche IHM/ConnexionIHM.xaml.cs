
using Couche_Data;
using Couche_IHM.ImagesProduit;
using Modeles;
using System.Windows;



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
