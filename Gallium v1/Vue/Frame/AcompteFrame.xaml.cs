using Gallium_v1.Logique;
using Gallium_v1;
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
    /// Logique d'interaction pour AcompteFrame.xaml
    /// </summary>
    public partial class AcompteFrame : Page
    {
        public AcompteFrame()
        {
            InitializeComponent();
            acomptelist.ItemsSource = Adherent.Users;
            this.acomptelist.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Balance", System.ComponentModel.ListSortDirection.Descending));
        }

        /// <summary>
        /// Permet de sélectionner un produit en cliquant dessus
        /// </summary>
        private void SelectItem(object sender, SelectionChangedEventArgs e)
        {
            ListBox l = sender as ListBox;
            User u = l.SelectedItem as User;
            if (u != null)
            {
                this.compte.Text = u.Compte;
                this.balance.Text = Convert.ToString(u.Balance) + "€";
                this.infouser.Text = u.Nom;
                infoUser.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Permet de rechercher un utilisateur
        /// </summary>
        private void Search(object sender, TextChangedEventArgs e)
        {
            if(this.rechercheAcompte.Text !="" && this.rechercheAcompte.Text != " ")
            {
                infoUser.Visibility = Visibility.Visible;
                AfficheUser(this.rechercheAcompte.Text);
            }
            else
            {
                infoUser.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Permet de modifier un utilisateur
        /// </summary>
        private void ModifUser(object sender, RoutedEventArgs e)
        {
            User u = this.acomptelist.SelectedItem as User;

            if (u == null)
            {
                u = Adherent.findUser(this.rechercheAcompte.Text);
            }

            // Fenetre de modification en mode modale
            ModificationUser mod = new ModificationUser(u);
            mod.ShowDialog();
        }

        /// <summary>
        /// Permet de supprimer un utilisateur
        /// </summary>
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            // Utilisateur 
            User u = this.acomptelist.SelectedItem as User;

            if (u == null)
            {
                u = Adherent.findUser(this.rechercheAcompte.Text);
            }

            // Message pour vérifier l'envie de supprimer 
            MessageBoxResult result = MessageBox.Show("Êtes-vous sur de vouloir supprimer cet utilisateur ?", $"Supression de {u.Nom}", MessageBoxButton.YesNo);
                
            if (result == MessageBoxResult.Yes)
            {
                // Suprimme l'utilisateur
                //Adherent.removeUser(u);
                Adherent.removeUser(u);
                this.acomptelist.UnselectAll();
                this.acomptelist.ItemsSource = null;
                this.acomptelist.ItemsSource = Adherent.Users;
                infoUser.Visibility = Visibility.Hidden;
            }
        }


        /// <summary>
        /// Permet d'afficher les informations d'un user
        /// </summary>
        /// <param name="nomUser"> Nom de l'utilisateur </param>
        private void AfficheUser(string nomUser)
        {
            User user = Adherent.findUser(nomUser);
            if (user != null)
            {
                this.compte.Text = user.Compte;
                this.balance.Text = Convert.ToString(user.Balance);
                this.infouser.Text = user.Nom;

            }
            else
            {
                infoUser.Visibility = Visibility.Hidden;
            }
        }
    }
}
