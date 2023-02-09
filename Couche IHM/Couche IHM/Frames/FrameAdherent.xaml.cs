using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameAdherent.xaml
    /// </summary>
    public partial class FrameAdherent : Page
    {
        // Représente le manager des adhérents
        private AdhérentManager adhérentManager;

        

        /// <summary>
        /// Cosntructeur du frame adhérent
        /// </summary>
        /// <param name="adhérentManager">manager des adhérents</param>
        public FrameAdherent(AdhérentManager adhérentManager)
        {
            InitializeComponent();
            this.adhérentManager = adhérentManager;

            // Met à jour l'affichage
            UpdateView();

            this.rechercheAcompte.Focus();


        }


        /// <summary>
        /// Permet de mettre à jour la liste des adhérents
        /// </summary>
        private void UpdateView()
        {
            listadherents.ItemsSource = null;
            listadherents.ItemsSource = adhérentManager.GetAdhérents();
        }


        /// <summary>
        /// Permet d'afficher les informations d'un adhérent 
        /// </summary>
        /// <param name="infoUser"> Information de l'utilisateur </param>
        private void AfficheAcompte(string infoUser)
        {
            Adhérent adhérent = this.adhérentManager.GetAdhérent(infoUser);
            if (adhérent != null)
            {
                this.id.Text = adhérent.Id;
                this.argent.Text = Convert.ToString(adhérent.ArgentIHM);
                this.name.Text = adhérent.NomCompletIHM;

            }
            else
            {
                infoAdherent.Visibility = Visibility.Hidden;
                this.listadherents.SelectedItem = null;
            }
        }


        /// <summary>
        /// Permet de sélectionner un adhérent quand l'utilisateur clique sur une ligne de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAdherent(object sender, SelectionChangedEventArgs e)
        {
            Adhérent adhérent =  (Adhérent)this.listadherents.SelectedItem;
            if (adhérent != null)
            {
                this.id.Text = adhérent.Id;
                this.argent.Text = Convert.ToString(adhérent.ArgentIHM);
                this.name.Text = adhérent.NomCompletIHM;
                infoAdherent.Visibility = Visibility.Visible;
                
            }
         
            
        }


        /// <summary>
        /// Permet d'afficher un accompte en le recherchant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchAdherent(object sender, TextChangedEventArgs e)
        {
            if(this.rechercheAcompte.Text != "" && this.rechercheAcompte.Text != " ")
            {
                infoAdherent.Visibility = Visibility.Visible;
                AfficheAcompte(this.rechercheAcompte.Text);
            }
            else
            {
                infoAdherent.Visibility = Visibility.Hidden;
                this.listadherents.SelectedItem = null;
            }
        }




    }
}
