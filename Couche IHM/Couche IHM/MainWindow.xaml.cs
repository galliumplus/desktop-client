using Couche_Data;
using Couche_IHM.Frames;
using Couche_Métier;
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
        // Manager utilisateurs
        private UserManager userManager;

        public MainWindow()
        {
            InitializeComponent();
            this.adherentManager = new AdhérentManager(new FakeAdherentDao());
            this.productManager = new ProductManager(new FakeProduitsDAO());
            this.userManager = new UserManager(new FakeUserDAO());
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre de l'accueil
        /// </summary>
        private void GoToAccueil(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameAccueil.xaml", UriKind.Relative);
            this.namePage.Text = "Accueil";
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre de la caisse
        /// </summary>
        private void GoToCaisse(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameCaisse.xaml", UriKind.Relative);
            this.namePage.Text = "Caisse";
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre des adhérents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToAdhérent(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new FrameAdherent(this.adherentManager));
            this.namePage.Text = "Adhérents";
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre du stock
        /// </summary>
        private void GoToStock(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new FrameStock(this.productManager));
            this.namePage.Text = "Stock";
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre des comptes
        /// </summary>
        private void GoToCompte(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Navigate(new FrameComptes(this.userManager));
            this.namePage.Text = "Comptes";
        }
        /// <summary>
        /// Permet d'aller sur la fenêtre des statistiques
        /// </summary>
        private void GoToStatistique(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameStatistique.xaml", UriKind.Relative);
            this.namePage.Text = "Statistiques";
        }
    }
}
