using Couche_Métier;
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
    /// Logique d'interaction pour FrameComptes.xaml
    /// </summary>
    public partial class FrameComptes : Page
    {
        private UserManager userManager;
        private bool createCompte;

        // Attributs qui gèrent si la liste est triée
        private int isSortingMail = 0;
        private int isSortingRole = 0;
        private int isSortingIdentite = 0;
        public FrameComptes(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;

            // Met à jour l'affichage
            UpdateView();
            this.buttonValidate.Content = "Valider";
            this.RoleUtilisateur.Content = MainWindow.CompteConnected.Role.ToString();
            this.NomUtilisateur.Content = MainWindow.CompteConnected.NomComplet;

            this.log = new LogToTXT();

            FieldRoleList();
        }


        /// <summary>
        /// Ajoute un nouvelle adhérent
        /// </summary>
        private void AddAdherentButton(object sender, RoutedEventArgs e)
        {
            infoUser.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Permet de mettre à jour la liste des adhérents
        /// </summary>
        private void UpdateView()
        {
            listUser.ItemsSource = null;
            listUser.ItemsSource = userManager.GetComptes();
        }


        /// <summary>
        /// Permet d'afficher les informations d'un adhérent 
        /// </summary>
        /// <param name="infoUser"> Information de l'utilisateur </param>
        private void AfficheAcompte(string mailUser)
        {
            User user = this.userManager.GetCompte(mailUser);
            if (user != null)
            {
                AfficheAcompte(user);

            }
            else
            {
                infoUser.Visibility = Visibility.Hidden;
                this.listUser.SelectedItem = null;
                this.buttonValidate.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Permet d'afficher lesi nformations d'un adhérent
        /// </summary>
        /// <param name="adhérent">adhérent à détailler</param>
        private void AfficheAcompte(User user)
        {

            this.mailuser.Text = user.Mail;
            this.nomComplet.Text = user.NomComplet;
            this.roleList.SelectedItem = user.Role;

            this.buttonValidate.Visibility = Visibility.Hidden;
            ResetWarnings();
        }


        /// <summary>
        /// Permet de sélectionner un adhérent quand l'utilisateur clique sur une ligne de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectUser(object sender, SelectionChangedEventArgs e)
        {
            User user = (User)this.listUser.SelectedItem;
            if (user != null)
            {
                AfficheAcompte(user);
                infoUser.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// Permet d'afficher un accompte en le recherchant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchAdherent(object sender, TextChangedEventArgs e)
        {
            if (this.rechercheAcompte.Text != "" && this.rechercheAcompte.Text != " ")
            {
                infoUser.Visibility = Visibility.Visible;
                AfficheAcompte(this.rechercheAcompte.Text);
            }
            else
            {
                infoUser.Visibility = Visibility.Hidden;
                this.listUser.SelectedItem = null;
                this.buttonValidate.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Permet de cacher les warnings
        /// </summary>
        private void ResetWarnings()
        {
            this.compteWarning.Visibility = Visibility.Hidden;
            this.identiteWarning.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Permet de valider les changements faits à un utilisateur
        /// </summary>
        private void ValiderChangements(object sender, RoutedEventArgs e)
        {
            ResetWarnings();

            try
            {
                // Mise à jour du nom et du prénom
                string nomAdherent;
                string prenomAdherent;
                if (this.nomComplet.Text.Contains(" "))
                {
                    string[] nomComplet = this.nomComplet.Text.Split(" ");
                    nomAdherent = nomComplet[0];
                    prenomAdherent = nomComplet[1];
                }
                else
                {
                    throw new Exception("IdentiteFormat");
                }


                // Mise à jour du mail
                string mail;
                if (string.IsNullOrEmpty(this.mailuser.Text))
                {
                    mail = this.mailuser.Text;
                }
                else
                {
                    throw new Exception("MailFormat");
                }

                // Role
                RolePerm role = (RolePerm)this.roleList.SelectedIndex;

                if (createCompte) // Ajout d'un compte
                {
                    this.userManager.CreateCompte(new User(0, nomAdherent, prenomAdherent, mail, role));
                }
                else // Mise à jour d'un compte
                {
                    this.userManager.UpdateCompte(new User(0, nomAdherent, prenomAdherent, mail, role));
                }


                // Refresh vue
                UpdateView();
                infoUser.Visibility = Visibility.Hidden;
                this.listUser.SelectedItem = null;
                this.buttonValidate.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "MailFormat":
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
        /// Permet d'afficher le bouton de validation des changements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowValidationButton(object sender, RoutedEventArgs e)
        {
            this.buttonValidate.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Remplis la ComboBox des roles existants
        /// </summary>
        private void FieldRoleList()
        {
            foreach(RolePerm role in (RolePerm[])Enum.GetValues(typeof(RolePerm)))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Name = role.ToString();
                roleList.Items.Add(item);
            }
        }

        #region tri
        /// <summary>
        /// Permet de trier les adhérents selon leur argent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortArgent(object sender, RoutedEventArgs e)
        {

            ControlTemplate template = this.listUser.Template;
            Image myImage = template.FindName("argentTri", this.listUser) as Image;

            switch (isSortingArgent)
            {
                case 0:
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Argent", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Argent", ListSortDirection.Ascending));
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Argent", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Argent", ListSortDirection.Descending));
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
            ControlTemplate template = this.listUser.Template;
            Image myImage = template.FindName("idTri", this.listUser) as Image;
            switch (isSortingId)
            {
                case 0:
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Identifiant", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Identifiant", ListSortDirection.Ascending));
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Identifiant", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Identifiant", ListSortDirection.Descending));
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
            ControlTemplate template = this.listUser.Template;
            Image myImage = template.FindName("identiteTri", this.listUser) as Image;
            switch (isSortingIdentite)
            {
                case 0:
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("NomComplet", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("NomComplet", ListSortDirection.Ascending));
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("NomComplet", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("NomComplet", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingIdentite = (isSortingIdentite + 1) % 3;
        }

        /// <summary>
        /// Permet de trier les adhérents selon leur role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortRole(object sender, RoutedEventArgs e)
        {

            ControlTemplate template = this.listUser.Template;
            Image myImage = template.FindName("roleTri", this.listUser) as Image;

            switch (isSortingRole)
            {
                case 0:
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Role", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Role", ListSortDirection.Ascending));
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Role", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Role", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingRole = (isSortingRole + 1) % 3;

        }

        /// <summary>
        /// Permet de trier les adhérents selon leur mail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortMail(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = this.listUser.Template;
            Image myImage = template.FindName("mailTri", this.listUser) as Image;
            switch (isSortingMail)
            {
                case 0:
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Mail", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Mail", ListSortDirection.Ascending));
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Mail", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Mail", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingMail = (isSortingMail + 1) % 3;
        }
        #endregion
    }
}