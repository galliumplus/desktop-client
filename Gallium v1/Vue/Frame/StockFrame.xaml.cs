using Gallium_v1.Logique;
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
    /// Logique d'interaction pour StockFrame.xaml
    /// </summary>
    public partial class StockFrame : Page
    {
        public StockFrame()
        {
            InitializeComponent();
            


            stocklist.ItemsSource = Stock.StockProduits;
            this.stocklist.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Stock", System.ComponentModel.ListSortDirection.Descending));
        }



        /// <summary>
        /// Permet d'afficher les informations d'un user
        /// </summary>
        /// <param name="nomUser"></param>
        private void afficheStock(string stock)
        {
            Product produit = Stock.findProduit(stock);
            if (produit != null)
            {
                this.stock.Text = Convert.ToString(produit.Stock);
                this.infoproduit.Text = produit.NomProduit;
                this.prix.Text = Convert.ToString(produit.PrixProduitAdhérent);
            }
            else
            {
                InfoProduct.Visibility = Visibility.Hidden;
            }
            
        }

        /// <summary>
        /// Permet de sélectionner un produit en cliquant dessus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectItem(object sender, SelectionChangedEventArgs e)
        {
            ListBox l = sender as ListBox;
            Product u = l.SelectedItem as Product;
            if (u != null)
            {
                this.stock.Text = Convert.ToString(u.Stock);
                this.infoproduit.Text = u.NomProduit;
                this.prix.Text = Convert.ToString(u.PrixProduitAdhérent) + " €";
                InfoProduct.Visibility = Visibility.Visible;
            }
            else
            {
                InfoProduct.Visibility = Visibility.Hidden;
            }


        }

        /// <summary>
        /// Permet de rechercher un produit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search(object sender, TextChangedEventArgs e)
        {
            if (this.rechercheProduit.Text != "" && this.rechercheProduit.Text != " ")
            {
                InfoProduct.Visibility = Visibility.Visible;
                afficheStock(this.rechercheProduit.Text);
            }
            
        }


        private void DeleteProduct(object sender, RoutedEventArgs e)
        {

            Product p = this.stocklist.SelectedItem as Product;

            if (p == null)
            {
                p = Stock.findProduit(this.rechercheProduit.Text);
            }

            if (p != null)
            {
                // Message demandant si vous voulez vraiment supprimer le produit
                MessageBoxResult result = MessageBox.Show("Êtes-vous sur de vouloir supprimer ce produit ?", $"Supression de {p.NomProduit}", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Stock.removeProduit(p);
                    this.stocklist.UnselectAll();
                    this.stocklist.ItemsSource = null;
                    this.stocklist.ItemsSource = Stock.StockProduits;
                    InfoProduct.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// Supprime le produit de l'acompte et de la liste
        /// </summary>
        /// <param name="u"> produit </param>
        private void DeleteProductFromStocklist(Product p)
        {
            
        }

    }
}
