
using Couche_IHM.ImagesProduit;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Manager;
using Couche_Métier.Utilitaire;
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

        private string identifiant;
        private string password;

        public ConnexionIHM()
        {
            InitializeComponent();
            DataContext = this;
            userManager = MainWindowViewModel.Instance.UserManager;
            logManager = MainWindowViewModel.Instance.LogManager;
        }

        public string Identifiant { get => identifiant; set => identifiant = value; }
        public string Password { get => password; set => password = value; }

        /// <summary>
        /// Permet de se connecter à son compte et de créer la mainWindows
        /// </summary>
        private void ConnectToAccount(object sender, RoutedEventArgs e)
        {
            User? user = this.userManager.ConnectCompte(identifiant, password);
            if (user != null)
            {
                Log log = new Log(0, DateTime.Now, 1, $"Connexion de {user.Prenom} {user.Nom}", $"{user.Prenom} {user.Nom}");
                logManager.CreateLog(log);
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
                MainWindow mainWindow = new MainWindow(user, logManager, userManager);
                mainWindow.Show();
                this.Close();
            }

            
        }
    }
}
