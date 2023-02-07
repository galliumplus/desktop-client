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
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre de l'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToAccueil(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameAccueil.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre de la caisse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToCaisse(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameCaisse.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre des adhérents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToAdhérent(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameAdherent.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre du stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToStock(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameStock.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Permet d'aller sur la fenêtre des comptes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToCompte(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameComptes.xaml", UriKind.Relative);
        }
        /// <summary>
        /// Permet d'aller sur la fenêtre des statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToStatistique(object sender, RoutedEventArgs e)
        {
            this.mainFrame.Source = new Uri("Frames/FrameStatistique.xaml", UriKind.Relative);
        }
    }
}
