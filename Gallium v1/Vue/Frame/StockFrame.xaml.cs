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
            Stock.ajoutProduit("Cafe",35,"test",Category.BOISSON,100);
            Stock.ajoutProduit("Chips", 35, "test", Category.SNACKS, 10);
            Stock.ajoutProduit("Monster", 35, "test", Category.BOISSON, 1);
            Stock.ajoutProduit("Kit Kat", 35, "test", Category.SNACKS, 35);


            stocklist.ItemsSource = Stock.StockProduits;
            this.stocklist.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Stock", System.ComponentModel.ListSortDirection.Descending));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.rechercheProduit.IsFocused == true  && this.rechercheProduit.Text != "")
                {
                    InfoProduct.Visibility = Visibility.Visible;
                    afficheStock(this.rechercheProduit.Text);
                }
                else
                {
                    InfoProduct.Visibility = Visibility.Hidden;
                }
            }

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
            this.stock.Text = Convert.ToString(u.Stock);
            this.infoproduit.Text = u.NomProduit;
            this.prix.Text = Convert.ToString(u.PrixProduitAdhérent);
            InfoProduct.Visibility = Visibility.Visible;

        }
    }
}
