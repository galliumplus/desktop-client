using Couche_Data;
using Couche_IHM.Frames;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Modeles;
using System;
using System.Windows;

namespace Couche_IHM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Représente le manager des adhérents
        private AdhérentManager adherentManager;
        // Manager de produits
        private ProductManager productManager;
       
        // Manager des catégories
        private CategoryManager categoryManager;

        // Manager utilisateurs
        private UserManager userManager;


        public MainWindow(User user)
        {
            InitializeComponent();
            MainWindowViewModel.Instance.CompteConnected = user;
            this.adherentManager = new AdhérentManager();
            this.productManager = new ProductManager(new FakeProduitsDAO());
            this.userManager = new UserManager(new FakeUserDAO());
            this.categoryManager = new CategoryManager(new FakeCategoryDAO());

        }

        #region navigationMethods
        /// <summary>
        /// Permet d'aller sur la fenêtre de l'accueil
        /// </summary>
        private void GoToAccueil(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameAccueil.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre de la caisse
        /// </summary>
        private void GoToCaisse(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new FrameCaisse(this.adherentManager, this.productManager, this.categoryManager));
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre des adhérents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToAdhérent(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new FrameAdherent(this.adherentManager));
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre du stock
        /// </summary>
        private void GoToStock(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new FrameStock(this.productManager, this.categoryManager));
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre des comptes
        /// </summary>
        private void GoToCompte(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new FrameComptes(this.userManager));
        }
        /// <summary>
        /// Permet d'aller sur la fenêtre des statistiques
        /// </summary>
        private void GoToStatistique(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameStatistique.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Va a la frame log
        /// </summary>
        private void GoToLog(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new FrameLogs(this.userManager));
        }
        #endregion
    }
}
