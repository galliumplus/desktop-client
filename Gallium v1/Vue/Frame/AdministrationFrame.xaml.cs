using Gallium_v1.Data;
using Gallium_v1.Logique;
using Gallium_v1.Vue.Modification;
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

namespace Gallium_v1.Vue.Frame
{
    /// <summary>
    /// Logique d'interaction pour AdministrationFrame.xaml
    /// </summary>
    public partial class AdministrationFrame : Page
    {
        public AdministrationFrame()
        {
            InitializeComponent();
            UpdateListUsers();
            
            //this.userList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Role", System.ComponentModel.ListSortDirection.Descending));
        }

        /// <summary>
        /// Permet de sélectionner un utilisateur en cliquant dessus
        /// </summary>
        private void SelectItem(object sender, SelectionChangedEventArgs e)
        {
            ListBox l = sender as ListBox;
            User u = l.SelectedItem as User;
            if (u != null)
            {
                this.ChargeUser(u);
                InfoUser.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Permet de rechercher un utilisateur
        /// </summary>
        private void Search(object sender, TextChangedEventArgs e)
        {
            if (this.rechercheUser.Text != "")
            {
                InfoUser.Visibility = Visibility.Visible;
                AfficheUser(this.rechercheUser.Text);
            }
            else
            {
                InfoUser.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Permet d'afficher les informations d'un user
        /// </summary>
        /// <param name="nomUser"> Nom de l'utilisateur </param>
        private void AfficheUser(string nomUser)
        {
            User user = ListUser.findUser(nomUser);
            if (user != null)
            {
                this.ChargeUser(user);

            }
            else
            {
                InfoUser.Visibility = Visibility.Hidden;
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            // Initialise le produit
            User u = this.userList.SelectedItem as User;

            if (u == null)
            {
                u = ListUser.findUser(this.rechercheUser.Text);
            }

            // Message demandant si vous voulez vraiment supprimer le produit
            MessageBoxResult result = MessageBox.Show("Êtes-vous sur de vouloir supprimer cet utiliateur ?", $"Supression de {u.PrenomUser}", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ValidationUser validation = new ValidationUser(u,"Delete");
                validation.ShowDialog();
                if (validation.Réel == true)
                {
                    MessageBox.Show("Utilisateur supprimé !", $"Supression de {u.PrenomUser}", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                this.userList.UnselectAll();
                this.UpdateListUsers();
                InfoUser.Visibility = Visibility.Hidden;
            }

        }

        private void ModifUser(object sender, RoutedEventArgs e)
        {
            // Initialise le produit
            User u = this.userList.SelectedItem as User;

            if (u == null)
            {
                u = ListUser.findUser(this.rechercheUser.Text);
            }

            // Fenetre de modification en mode modale
            ModificationUser modificationUser = new ModificationUser(u);
            modificationUser.ShowDialog();

            // Modification de l'utilisateur
            
            this.userList.SelectedItem = modificationUser.User;
            this.UpdateListUsers();
            
        }


        /// <summary>
        /// Met à jour la liste des stocks
        /// </summary>
        private void UpdateListUsers()
        {
            this.userList.ItemsSource = null;
            ListUser.UsersList = UserDAO.ReadAllUser();
            userList.ItemsSource = ListUser.UsersList;
        }

        private void ChargeUser(User user)
        {
            this.nomUser.Text = user.NomUser;
            this.prénomUser.Text = user.PrenomUser;
            this.identifiantUser.Text = user.IdentifiantUser;
            this.roleUser.Text = user.RangUser.ToString();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            AjoutUser ajoutUser = new AjoutUser();
            ajoutUser.ShowDialog();
            UpdateListUsers();
        }
    }
}
