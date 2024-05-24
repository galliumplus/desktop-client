using Couche_Data;
using Couche_IHM.Frames;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Manager;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Couche_IHM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DiscordSocketClient _client;
        private static List<ProductViewModel> products;
        private static SocketUser god;


        /// <summary>
        /// Constructeur de la mainwindow
        /// </summary>
        public MainWindow(Account user)
        {
            InitializeComponent();
            var mwvm = MainWindowViewModel.GetInstanceFor(this);
            AccountManager accountManager = MainWindowViewModel.Instance.AccountManager;
            mwvm.CompteConnected = new AccountViewModel(user, accountManager);
            DataContext = MainWindowViewModel.Instance;

            products = mwvm.ProductViewModel.Products.ToList();

            if (DevelopmentInfo.isDevelopment)
            {
                Menu.Background = new SolidColorBrush(Colors.DarkRed);
                buttonAccount.Background = new SolidColorBrush(Colors.DarkRed);
                buttonAccueil.Background = new SolidColorBrush(Colors.DarkRed);
                buttonCaisse.Background = new SolidColorBrush(Colors.DarkRed);
                buttonComptes.Background = new SolidColorBrush(Colors.DarkRed);
                buttonStock.Background = new SolidColorBrush(Colors.DarkRed);
            }
            DiscordBot discordBot = new DiscordBot(mwvm);
        }




        /// <summary>
        /// Permet de se déconnecter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnect(object sender, RoutedEventArgs e)
        {
            this.AskToDisconnect();
        }

        public void AskToDisconnect()
        {
            ConnexionIHM connexionIHM = new ConnexionIHM();
            connexionIHM.Show();
            this.Close();
        }
    }
}
