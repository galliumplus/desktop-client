using Couche_Data;
using Couche_IHM.Frames;
using Couche_Métier;
using Couche_Métier.Log;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        CategoryManager categoryManager;

        // Manager utilisateurs
        private UserManager userManager;
        private static User compteConnected;
        /// <summary>
        /// Compte connecté à gallium
        /// </summary>
        public static User CompteConnected
        {
            get => compteConnected;
        }


        public MainWindow(User user)
        {
            InitializeComponent();
            this.adherentManager = new AdhérentManager(new FakeAdherentDao());
            this.productManager = new ProductManager(new FakeProduitsDAO());
            this.userManager = new UserManager(new FakeUserDAO());
            this.categoryManager = new CategoryManager(new FakeCategoryDAO());

            // Enregistre la personne qui s'est connecté
            compteConnected = user;
        }

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
    }
}
