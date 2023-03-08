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

            // Remplissage du formulaire
            this.nom.Text = adh.Nom;
            this.prenom.Text = adh.Prenom;
            this.login.Text = adh.Identifiant;
            int isAdherent = 0;
            if (adh.StillAdherent)
            {
                isAdherent = 1;
            }
            this.isadherent.Value = isAdherent;
            this.argent.Text = Convert.ToString(adh.Argent);


        }

        /// <summary>
        /// Permet de valider les changements de l'adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValideAdherent(object sender, RoutedEventArgs e)
        {
            try
            {
                this.adh.Nom = this.nom.Text;
                this.adh.Prenom =  this.prenom.Text;
                this.adh.Identifiant = this.login.Text;
                bool isAdherent = false;
                if (this.isadherent.Value == 1)
                {
                    isAdherent = true;
                }
                this.adh.StillAdherent= isAdherent;
                this.adh.Argent = Convert.ToDouble(this.argent.Text);

                DialogResult = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = false;
            }
            
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
