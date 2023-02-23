using Couche_Métier;
using Couche_Métier.Log;
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
        private ILog log;

        // Attributs qui gèrent si la liste est triée
        private int isSortingMail = 0;
        private int isSortingRole = 0;
        private int isSortingIdentite = 0;
        public FrameComptes(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.listUser.ItemsSource = userManager.Comptes;
            this.log = new LogToTXT();

            infoUser.Visibility = Visibility.Hidden;
            FieldRoleList();
        }

        /// <summary>
        /// Créer un utilisateur
        /// </summary>
        /// <param name="u"></param>
        private void CreateAnUser(User u)
        {
            this.userManager.CreateCompte(u);
            log.registerLog(CategorieLog.CREATE_USER, $"Création de l'utilisateur {u.NomComplet}", MainWindow.CompteConnected);
        }

        /// <summary>
        /// Met à jour un utilisateur
        /// </summary>
        /// <param name="baseUser"> base utilisateur </param>
        /// <param name="u"> utilisateur update </param>
        private void UpdateAnUser(User baseUser, User u)
        {
            this.userManager.UpdateCompte(u);

            // LOG
            string message = "Mise à jour de l'utilisateur " + baseUser.NomComplet + ":";
            // Si changement de nom
            if(baseUser.Nom != u.Nom)
            {
                message += $"/Changement du nom {baseUser.Nom} en {u.Nom}";
            }

            // Si changement de prénom
            if(baseUser.Prenom != u.Prenom)
            {
                message += $"/Changement du nom {baseUser.Prenom} en {u.Prenom}";
            } 
            
            // Si changement de mail
            if(baseUser.Mail != u.Mail)
            {
                message += $"/Changement du nom {baseUser.Mail} en {u.Nom}";
            }

            log.registerLog(CategorieLog.UPDATE_USER, message, MainWindow.CompteConnected);
        }

        /// <summary>
        /// Permet de mettre à jour la liste des utilisateurs
        /// </summary>
        private void UpdateView()
        {
            listUser.ItemsSource = null;
            listUser.ItemsSource = userManager.GetComptes();
        }


        /// <summary>
        /// Permet d'afficher les informations d'un utilisateur
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
        /// Permet d'afficher les informations d'un utilisateur
        /// </summary>
        /// <param name="user">utilisateur à détailler</param>
        private void AfficheAcompte(User user)
        {
            this.mailuser.Text = user.Mail;
            this.nomComplet.Text = user.NomComplet;
            this.roleList.SelectedValue = user.Role.ToString();
            
            this.buttonValidate.Visibility = Visibility.Hidden;
            ResetWarnings();
        }

        /// <summary>
        /// Cache les warnings
        /// </summary>
        private void ResetWarnings()
        {
            this.compteWarning.Visibility = Visibility.Hidden;
            this.identiteWarning.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Remplis la ComboBox des roles existants
        /// </summary>
        private void FieldRoleList()
        {
            foreach (RolePerm role in (RolePerm[])Enum.GetValues(typeof(RolePerm)))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Name = role.ToString();
                roleList.Items.Add(item.Name);
            }
        }

        #region events
        /// <summary>
        /// Permet d'afficher un utilisateur en le recherchant
        /// </summary>
        private void SearchUser(object sender, TextChangedEventArgs e)
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
        /// Ajoute un nouvelle utilisateur
        /// </summary>
        private void AddUserButton(object sender, RoutedEventArgs e)
        {
            infoUser.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Permet de sélectionner un compte quand l'utilisateur clique sur une ligne de la liste
        /// </summary>
        private void SelectUsers(object sender, SelectionChangedEventArgs e)
        {
            User user = (User)this.listUser.SelectedItem;
            if (user != null)
            {
                AfficheAcompte(user);
                infoUser.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Permet de valider les changements faits à un utilisateur
        /// </summary>
        private void ValiderChangements(object sender, RoutedEventArgs e)
        {
            ResetWarnings();

            try
            {
                User baseUser = (User)this.listUser.SelectedItem;

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
                    CreateAnUser(new User(0, nomAdherent, prenomAdherent, mail, role));
                }
                else // Mise à jour d'un compte
                {
                    UpdateAnUser(baseUser,new User(0, nomAdherent, prenomAdherent, mail, role));
                   
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
        private void ShowValidationButton(object sender, TextChangedEventArgs e)
        {
            this.buttonValidate.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Permet d'afficher le bouton de validation des changements
        /// </summary>
        private void ShowValidationButton(object sender, RoutedEventArgs e)
        {
            this.buttonValidate.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Permet de cacher les options
        /// </summary>
        private void HideOptions(object sender, RoutedEventArgs e)
        {
            this.options.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Permet d'afficher les options
        /// </summary>
        private void ShowOptions(object sender, RoutedEventArgs e)
        {
            this.options.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Permet de supprimer l'utilisateur
        /// </summary>
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            User userSelected = this.userManager.GetCompte(this.mailuser.Text);
            this.userManager.RemoveCompte(userSelected);
            infoUser.Visibility = Visibility.Hidden;

            // LOG DELETE ADHERENT
            log.registerLog(CategorieLog.DELETE_USER, $"Supression d'un utilisateur [{userSelected.NomComplet}]", MainWindow.CompteConnected);

            UpdateView();
            this.options.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Permet de selectionner un utilisateur
        /// </summary>
        private void SelectUser(object sender, SelectionChangedEventArgs e)
        {
            User users = (User)this.listUser.SelectedItem;
            if (users != null)
            {
                infoUser.Visibility = Visibility.Visible;
                AfficheAcompte(users);
            }
            this.createCompte = false;

        }

        /// <summary>
        /// Permet de fermer la fenêtre aves les infos utilisateurs
        /// </summary>
        private void CloseInfoAdherent(object sender, RoutedEventArgs e)
        {
            this.infoUser.Visibility = Visibility.Hidden;
            this.buttonValidate.Visibility = Visibility.Hidden;
            this.listUser.SelectedItem = null;
            this.options.Visibility = Visibility.Hidden;
        }

        #endregion

        #region tri
        /// <summary>
        /// Permet de trier les utilisateurs selon leur identité
        /// </summary>
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