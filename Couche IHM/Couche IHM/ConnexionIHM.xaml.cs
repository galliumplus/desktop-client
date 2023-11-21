
using Couche_IHM.ImagesProduit;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Manager;
using MaterialDesignThemes.Wpf;
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
        #region attributes
        private AccountManager accountManager;
        private LogManager logManager;
        private string identifiant = "";
        private string password = "";
        #endregion

        public ConnexionIHM()
        {
            InitializeComponent();
            DataContext = this;
            this.messageQueue = new SnackbarMessageQueue(new TimeSpan(0, 0, 2));
            ImageManager.VerifyFiles();
        }

        public string Identifiant { get => identifiant; set => identifiant = value; }
        public string Password { get => password; set => password = value; }
        public SnackbarMessageQueue MessageQueue { get => messageQueue; set => messageQueue = value; }

        private SnackbarMessageQueue messageQueue;

        /// <summary>
        /// Permet de se connecter à son compte et de créer la mainWindows
        /// </summary>
        private void ConnectToAccount(object sender, RoutedEventArgs e)
        {      
            if (password == "" || identifiant == "")
            {
                messageQueue.Enqueue("Vous devez remplir les deux champs !");
            }
            else
            {
                try
                {
                    Account? user = AccountManager.ConnectCompte(identifiant, password);
                    if (user != null)
                    {
                        if (user.RoleId == 1)
                        {
                            DevelopmentInfo.isDevelopment = true;
                        }

                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();
                        this.Close();

                        
                        logManager = MainWindowViewModel.Instance.LogManager;
                        Log log = new Log(DateTime.Now, 1, $"Connexion de {user.Prenom} {user.Nom}", $"{user.Prenom} {user.Nom}");
                        logManager.CreateLog(log);
                        MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
                    }
                    else
                    {
                        messageQueue.Enqueue("Mauvais mot de passe");
                    }
                }
                catch(Exception ex)
                {
                    messageQueue?.Enqueue("Vous n'êtes pas connecté à Internet");
                }                
            }
        }
    }
}
