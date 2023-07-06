using Couche_Métier;
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.Windows;

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
            this.formation.Text = adh.Formation;
            this.login.Text = adh.Identifiant;
            int isAdherent = 0;
            if (adh.StillAdherent)
            {
                isAdherent = 1;
            }
            this.isadherent.Value = isAdherent;
            //this.argent.Text = adh.ArgentIHM;


        }

        /// <summary>
        /// Permet de valider les changements de l'adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValideAdherent(object sender, RoutedEventArgs e)
        {
            // Permet de convertir l'argent
            ConverterFormatArgent converterArgent = new ConverterFormatArgent();

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
                this.adh.Argent = converterArgent.ConvertToDouble(this.argent.Text);
                this.adh.Formation = this.formation.Text;

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
