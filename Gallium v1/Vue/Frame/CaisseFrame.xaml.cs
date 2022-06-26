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
    /// Logique d'interaction pour CaisseFrame.xaml
    /// </summary>
    public partial class CaisseFrame : Page
    {
        private List<Product> orderedItem = new List<Product>();
        private int quantityO =0;
        private double priceO =0.00;

        public int QuantityO { get => quantityO; set => quantityO = value; }
        public string PriceAdher { get => priceO+"€";}
        public string PriceNanAdher { get => priceO - (0.20 * quantityO) + "€"; }

        public CaisseFrame()
        {
            InitializeComponent();

            productHandler.ItemsSource = Stock.StockProduits;
            Order.ItemsSource = orderedItem;

            string[] moyenPayement = { "Acompte", "Paypal", "Carte" };
            listeMoyenPayement.ItemsSource = moyenPayement;
        }

        private void Mouse_Enter(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            gd.Background = Brushes.Gray;
        }

        private void Mouse_Leave(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            gd.Background = Brushes.Transparent;
        }
        private void OrderItem_Enter(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            gd.Background = Brushes.Gray;
        }

        private void OrderItem_Leave(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            gd.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#302F2F");
        }
        private void Buy_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            gd.Background = Brushes.Gray;
        }

        private void Buy_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            gd.Background = Brushes.Black;
        }

        private void Drop_Item_Order(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            int i = 0;
            while(i < orderedItem.Count)
            {
                if (orderedItem[i].NomProduit == (string)gd.Tag)
                {
                    priceO -= orderedItem[i].PrixProduitAdhérent;
                    orderedItem.RemoveAt(i);
                    quantityO--;
                    i=orderedItem.Count;
                }
                i++;
            }
            this.QuantityOrdered.Content = Convert.ToString(QuantityO);
            if (this.AdherCheck.IsChecked == false) this.Price.Content = PriceAdher;
            else this.Price.Content = PriceNanAdher;
            UpdateListProduitsOrder();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid gd = sender as Grid;
            Label lab = gd.Children[1] as Label;
            string prodName = (string)lab.Content;

            foreach (Product p in Stock.StockProduits)
            {
                if (p.NomProduit == prodName)
                {
                    orderedItem.Add(p);
                    quantityO++;
                    priceO += p.PrixProduitAdhérent;
                }
            }
            this.QuantityOrdered.Content = Convert.ToString(QuantityO);
            if (this.AdherCheck.IsChecked == false) this.Price.Content = PriceAdher;
            else this.Price.Content = PriceNanAdher;
            UpdateListProduitsOrder();
        }

        private void UpdateListProduitsOrder()
        {
            this.Order.ItemsSource = null;
            this.Order.ItemsSource = orderedItem;
        }

        private void AdherCheck_Click(object sender, RoutedEventArgs e)
        {
            if (this.AdherCheck.IsChecked == false) this.Price.Content = PriceAdher;
            else this.Price.Content = PriceNanAdher;
        }

       
    }
}
