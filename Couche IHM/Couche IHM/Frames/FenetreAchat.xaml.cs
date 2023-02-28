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

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FenetreAchat.xaml
    /// </summary>
    public partial class FenetreAchat : Window
    {
        private List<Adhérent> adhérents;

        public FenetreAchat(List<Adhérent> adhérents)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.adhérents = adhérents;
            FillListAcompte();
        }

        /// <summary>
        /// Remplis la comboBox
        /// </summary>
        private void FillListAcompte()
        {
            foreach (Adhérent ad in adhérents)
            {
                this.listAcompte.Items.Add(ad.Identifiant);
            }
        }
        /// <summary>
        /// Met à jour la liste des acomptes 
        /// </summary>
        private void UpdateListAcompte(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Valide achat
        /// </summary>
        private void buys_Click(object sender, RoutedEventArgs e)
        {
            Adhérent ad = null;
            if(this.listAcompte.SelectedIndex != null)
            {
                ad = adhérents.Find(x => x.Identifiant == this.listAcompte.SelectedIndex.ToString());
            };

            
           

            
        }

        /// <summary>
        /// Valide
        /// </summary>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
