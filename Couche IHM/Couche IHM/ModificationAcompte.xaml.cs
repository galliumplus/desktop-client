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
using System.Windows.Shapes;

namespace Couche_IHM
{
    /// <summary>
    /// Logique d'interaction pour ModificationAcompte.xaml
    /// </summary>
    public partial class ModificationAcompte : Window
    {
        // représente l'adhérent modifié
        private Adhérent adh;
        public Adhérent Adh { get => adh; set => adh = value; }

        public ModificationAcompte(Adhérent adhérent)
        {
            InitializeComponent();
            this.adh = adhérent;
            DataContext = Adh   ;
            
        }

        /// <summary>
        /// Permet de valider les changements de l'adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValideAdherent(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Permet d'annuler les changements de l'adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelAdherent(object sender, RoutedEventArgs e)
        {
            DialogResult=false;
        }
    }
}
