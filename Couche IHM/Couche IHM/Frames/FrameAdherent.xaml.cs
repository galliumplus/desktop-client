using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Log;
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameAdherent.xaml
    /// </summary>
    public partial class FrameAdherent : Page
    {


        // Exporter des adhérents
        private IExportableAdherent exportAdh;

        // Attributs qui gèrent si la liste est triée
        private int isSortingArgent = 0;
        private int isSortingId = 0;
        private int isSortingIdentite = 0;


        /// <summary>
        /// Cosntructeur du frame adhérent
        /// </summary>
        /// <param name="adhérentManager">manager des adhérents</param>
        public FrameAdherent()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
            this.exportAdh = new ExportAdherentToExcel();
        }

        



        #region operationsMetiers
        /// <summary>
        /// Permet de créer un adhérent
        /// </summary>
        /// <param name="a">Adhérent à créer</param>
        private void CreateAnAdherent(Adhérent a)
        {
            // Créer l'adhérent
            //this.adhérentManager.CreateAdhérent(a);
            
            // Log l'opération
            //log.registerLog(CategorieLog.CREATE, a, MainWindow.CompteConnected);
        }

        /// <summary>
        /// Permet de mettre à jour un adhérent
        /// </summary>
        /// <param name="baseAdhérent">Adhérent à modifier</param>
        /// <param name="a">Nouvel Adhérent</param>
        private void UpdateAnAdherent(Adhérent baseAdhérent, Adhérent a)
        {
            // Met à jour l'adhérent
            //this.adhérentManager.UpdateAdhérent(a);

            // Log l'opération
            //log.registerLog(CategorieLog.UPDATE, a, MainWindow.CompteConnected);
        }
        #endregion

        #region events




        /// <summary>
        /// Permet d'exporter la liste des adhérents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportAdhérent(object sender, RoutedEventArgs e)
        {
           // this.exportAdh.Export(this.adhérentManager.GetAdhérents());
        }


        /// <summary>
        /// Permet de supprimer l'adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAdherent(object sender, RoutedEventArgs e)
        {
            //Adhérent adhérentSelect = this.adhérentManager.GetAdhérent(this.id.Text);
            //this.adhérentManager.RemoveAdhérent(adhérentSelect);

            // LOG DELETE ADHERENT
            //log.registerLog(CategorieLog.DELETE, adhérentSelect, MainWindow.CompteConnected);

        }


        /// <summary>
        /// Permet d'ajouter un adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAdherentButton(object sender, RoutedEventArgs e)
        {
            Adhérent newAdhérent = new Adhérent();
            ModificationAcompte modifAcompteWindow = new ModificationAcompte(newAdhérent);
            bool? result = modifAcompteWindow.ShowDialog();

            // Si l'ajout est validé alors on met à jour la bdd
            if (result.Value == true)
            {
                //this.adhérentManager.CreateAdhérent(newAdhérent);
                //log.registerLog(CategorieLog.CREATE, newAdhérent, MainWindow.CompteConnected);
            }
            
        }


        #endregion

        #region TRI
        /// <summary>
        /// Permet de trier les adhérents selon leur argent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortArgent(object sender, RoutedEventArgs e)
        {

            ControlTemplate template = this.listadherents.Template;
            Image myImage = template.FindName("argentTri", this.listadherents) as Image;

            switch (isSortingArgent)
            {
                case 0:
                    this.listadherents.Items.SortDescriptions.Add(new SortDescription("Argent", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listadherents.Items.SortDescriptions.Remove(new SortDescription("Argent", ListSortDirection.Ascending));
                    this.listadherents.Items.SortDescriptions.Add(new SortDescription("Argent", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listadherents.Items.SortDescriptions.Remove(new SortDescription("Argent", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingArgent = (isSortingArgent + 1) % 3;

        }

        /// <summary>
        /// Permet de trier les adhérents selon leur id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortId(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = this.listadherents.Template;
            Image myImage = template.FindName("idTri", this.listadherents) as Image;
            switch (isSortingId)
            {
                case 0:
                    this.listadherents.Items.SortDescriptions.Add(new SortDescription("Identifiant", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listadherents.Items.SortDescriptions.Remove(new SortDescription("Identifiant", ListSortDirection.Ascending));
                    this.listadherents.Items.SortDescriptions.Add(new SortDescription("Identifiant", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listadherents.Items.SortDescriptions.Remove(new SortDescription("Identifiant", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingId = (isSortingId + 1) % 3;
        }
        


        /// <summary>
        /// Permet de trier les adhérents selon leur identité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortIdentite(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = this.listadherents.Template;
            Image myImage = template.FindName("identiteTri", this.listadherents) as Image;
            switch (isSortingIdentite)
            {
                case 0:
                    this.listadherents.Items.SortDescriptions.Add(new SortDescription("NomCompletIHM", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listadherents.Items.SortDescriptions.Remove(new SortDescription("NomCompletIHM", ListSortDirection.Ascending));
                    this.listadherents.Items.SortDescriptions.Add(new SortDescription("NomCompletIHM", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listadherents.Items.SortDescriptions.Remove(new SortDescription("NomCompletIHM", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingIdentite = (isSortingIdentite + 1) % 3;
        }



        #endregion

       
    }
}
