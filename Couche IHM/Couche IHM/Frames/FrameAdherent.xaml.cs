using Couche_Métier;
using Couche_Métier.Log;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

        // Exporter des adhérents
        private IExportableAdherent exportAdh;

        // Attributs qui gèrent si la liste est triée
        private int isSortingArgent = 0;
        private int isSortingId = 0;
        private int isSortingIdentite = 0;

        // Attribut qui permet de gérer les logs pour chaque opération
        private ILog log = new LogAdherentToTxt();

        /// <summary>
        /// Cosntructeur du frame adhérent
        /// </summary>
        /// <param name="adhérentManager">manager des adhérents</param>
        public FrameAdherent(AdhérentManager adhérentManager)
        {
            InitializeComponent();
            this.adhérentManager = adhérentManager;
            this.exportAdh = new ExportAdherentToExcel();
            

            // Met à jour l'affichage
            UpdateView();
            this.RoleUtilisateur.Content = MainWindow.CompteConnected.Role.ToString();
            this.NomUtilisateur.Content = MainWindow.CompteConnected.NomComplet;
            

            // Focus l'utilisateur sur la barre de recherche
            this.rechercheAcompte.Focus();


            // Si membre du ca alors parametre pas visibles
            if (MainWindow.CompteConnected.Role != RolePerm.BUREAU)
            {
                this.optionsButton.Visibility = Visibility.Hidden;
            }

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
        /// Permet d'afficher lesi nformations d'un adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à détailler</param>
        private void AfficheAcompte(Adhérent adhérent)
        {
            this.id.Text = adhérent.Identifiant;
            this.argent.Text = Convert.ToString(adhérent.ArgentIHM);
            this.name.Text = adhérent.NomCompletIHM;


            // Modification affichage
            this.buttonValidate.Visibility = Visibility.Hidden;
            this.options.Visibility = Visibility.Hidden;
            ResetWarnings();
                     
        }

        /// <summary>
        /// Permet de cacher les warnings
        /// </summary>
        private void ResetWarnings()
        {
            this.compteWarning.Visibility = Visibility.Hidden;
            this.identiteWarning.Visibility = Visibility.Hidden;
            this.argentWarning.Visibility = Visibility.Hidden;
        }

        #region operationsMetiers
        /// <summary>
        /// Permet de créer un adhérent
        /// </summary>
        /// <param name="a">Adhérent à créer</param>
        private void CreateAnAdherent(Adhérent a)
        {
            // Créer l'adhérent
            this.adhérentManager.CreateAdhérent(a);
            
            // Log l'opération
            log.registerLog(CategorieLog.CREATE, a, MainWindow.CompteConnected);
        }

        /// <summary>
        /// Permet de mettre à jour un adhérent
        /// </summary>
        /// <param name="baseAdhérent">Adhérent à modifier</param>
        /// <param name="a">Nouvel Adhérent</param>
        private void UpdateAnAdherent(Adhérent baseAdhérent, Adhérent a)
        {
            // Met à jour l'adhérent
            this.adhérentManager.UpdateAdhérent(a);

            // Log l'opération
            log.registerLog(CategorieLog.UPDATE, a, MainWindow.CompteConnected);
        }
        #endregion

        #region events
        /// <summary>
        /// Permet d'afficher un accompte en le recherchant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchAdherent(object sender, TextChangedEventArgs e)
        {
            if (this.rechercheAcompte.Text.Trim() != "")
            {
                this.listadherents.ItemsSource = this.adhérentManager.GetAdhérents(this.rechercheAcompte.Text);
            }
            else
            {
                
                this.listadherents.SelectedItem = null;
                this.UpdateView();
                infoAdherent.Visibility = Visibility.Hidden;
                this.buttonValidate.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Permet de valider les changements faits à un utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderChangements(object sender, RoutedEventArgs e)
        {
            ResetWarnings();

            Adhérent newAdher = new Adhérent();
            Adhérent baseAdhérent = (Adhérent)this.listadherents.SelectedItem;
            try
            {
                // Mise à jour du nom et du prénom
                if (this.name.Text.Contains(" "))
                {
                    string[] nomComplet = this.name.Text.Split(" ");
                    newAdher.Nom = nomComplet[0];
                    newAdher.Prenom = nomComplet[1];
                }
                else
                {
                    throw new Exception("IdentiteFormat");
                }


                // Mise à jour de l'id
                if (this.id.Text.Length == 8 && this.id.Text[0] == newAdher.Prenom.ToLower()[0] && this.id.Text[1] == newAdher.Nom.ToLower()[0])
                {
                    newAdher.Identifiant = this.id.Text;
                }
                else
                {
                    throw new Exception("IDFormat");
                }


                // Mise à jour de l'argent
                string argentFormat = this.argent.Text.Replace(".", ",");
                argentFormat = argentFormat.Replace("€", " ");
                argentFormat = argentFormat.Trim();
                if (new Regex("^[0-9]+$").IsMatch(argentFormat) || new Regex("^[0-9]+,[0-9]+$").IsMatch(argentFormat))
                {
                    newAdher.Argent = (float)Convert.ToDouble(argentFormat);
                }
                else
                {
                    throw new Exception("ArgentFormat");
                }


                // Met à jour l'adhérent
                this.UpdateAnAdherent(baseAdhérent, newAdher);


                // Refresh vue
                UpdateView();
                this.infoAdherent.Visibility = Visibility.Hidden;
                this.listadherents.SelectedItem = null;
                this.buttonValidate.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "ArgentFormat":
                        this.argentWarning.Visibility = Visibility.Visible;
                        break;
                    case "IDFormat":
                        this.compteWarning.Visibility = Visibility.Visible;
                        break;
                    case "IdentiteFormat":
                        this.identiteWarning.Visibility = Visibility.Visible;
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
            }
        }

        /// <summary>
        /// Permet d'afficher le bouton de validation des changements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowValidationButton(object sender, TextChangedEventArgs e)
        {
            this.buttonValidate.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Permet d'exporter la liste des adhérents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportAdhérent(object sender, RoutedEventArgs e)
        {
            this.exportAdh.Export(this.adhérentManager.GetAdhérents());
        }

        /// <summary>
        /// Permet d'afficher le bouton de validation des changements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowValidationButton(object sender, RoutedEventArgs e)
        {
            this.buttonValidate.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Permet de supprimer l'adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAdherent(object sender, RoutedEventArgs e)
        {
            Adhérent adhérentSelect = this.adhérentManager.GetAdhérent(this.id.Text);
            this.adhérentManager.RemoveAdhérent(adhérentSelect);
            infoAdherent.Visibility = Visibility.Hidden;

            // LOG DELETE ADHERENT
            log.registerLog(CategorieLog.DELETE, adhérentSelect, MainWindow.CompteConnected);

            UpdateView();
            this.options.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Permet de modifier un adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyAdherent(object sender, RoutedEventArgs e)
        {
            Adhérent adhérentSelect = new Adhérent((Adhérent)this.listadherents.SelectedItem);
            ModificationAcompte modifAcompteWindow = new ModificationAcompte(adhérentSelect);
            this.options.Visibility = Visibility.Hidden;
            bool? result = modifAcompteWindow.ShowDialog();

            // Si l'ajout est validé alors on met à jour la bdd et la vue
            if (result.Value == true)
            {
                this.adhérentManager.UpdateAdhérent(adhérentSelect);
                UpdateView();
                this.infoAdherent.Visibility = Visibility.Hidden;

                log.registerLog(CategorieLog.UPDATE, adhérentSelect, MainWindow.CompteConnected);
            }

            
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
                this.adhérentManager.CreateAdhérent(newAdhérent);
                UpdateView();
                log.registerLog(CategorieLog.CREATE, newAdhérent, MainWindow.CompteConnected);
            }
            
        }

        /// <summary>
        /// Permet de selectionner un adhérent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAdherent(object sender, SelectionChangedEventArgs e)
        {
            Adhérent adhérent = this.listadherents.SelectedItem as Adhérent;
            if (adhérent != null)
            {

                infoAdherent.Visibility = Visibility.Visible;
                AfficheAcompte(adhérent);

            }
        }

        /// <summary>
        /// Permet de fermer la fenêtre aves les infos adhérents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseInfoAdherent(object sender, RoutedEventArgs e)
        {
            this.infoAdherent.Visibility = Visibility.Hidden;
            this.buttonValidate.Visibility = Visibility.Hidden;
            this.listadherents.SelectedItem = null;
            this.options.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Permet de cacher les options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideOptions(object sender, RoutedEventArgs e)
        {
            this.options.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Permet d'afficher les options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOptions(object sender, RoutedEventArgs e)
        {
            this.options.Visibility = Visibility.Visible;
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
