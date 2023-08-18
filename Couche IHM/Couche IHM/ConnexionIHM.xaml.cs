
using Couche_IHM.ImagesProduit;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Manager;
using Modeles;
using System;
using System.Windows;



namespace Couche_IHM
{
    /// <summary>
    /// Logique d'interaction pour ConnexionIHM.xaml
    /// </summary>
    public partial class ConnexionIHM : Window
    {
        /// <summary>
        /// Permet de gérer les utilisateurs
        /// </summary>
        private UserManager userManager;

        /// <summary>
        /// Permet de générer les logs
        /// </summary>
        private LogManager logManager;

        public ConnexionIHM()
        {
            InitializeComponent();
            userManager = MainWindowViewModel.Instance.UserManager;
            logManager = MainWindowViewModel.Instance.LogManager;
        }

        /// <summary>
        /// Permet de se connecter à son compte et de créer la mainWindows
        /// </summary>
        private void ConnectToAccount(object sender, RoutedEventArgs e)
        {

            // Vérifie la connexion d'un utilisateur
            User userConnection = userManager.GetComptes()[0];

            // Vérifie si l'utilisateur réussi à se connecter
            if (userConnection != null)
            {
                Log log = new Log(0, DateTime.Now.ToString("g"), 1, $"Connexion de {userConnection.Nom} {userConnection.Prenom}", $"{userConnection.Nom} {userConnection.Prenom}");
                logManager.CreateLog(log) ;
                MainWindowViewModel.Instance.LogsViewModel.Logs.Insert(0, new LogViewModel(log));
                MainWindow mainWindow = new MainWindow(userConnection,logManager,userManager);
                mainWindow.Show();
                this.Close();
            }
            
        }
    }
}
