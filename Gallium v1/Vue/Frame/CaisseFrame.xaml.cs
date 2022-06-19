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
        List<Product> orderedItem = new List<Product>(); 
        public CaisseFrame()
        {
            InitializeComponent();

            productHandler.ItemsSource = Stock.StockProduits;
            Order.ItemsSource = orderedItem;
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

            for(int i = 0; i < orderedItem.Count; i++)
            {
                if (orderedItem[i].NomProduit == (string)gd.Tag)
                {
                    orderedItem.RemoveAt(i);
                }
            }

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
                }
            }
            UpdateListProduitsOrder();
        }

        private void UpdateListProduitsOrder()
        {
            this.Order.ItemsSource = null;
            this.Order.ItemsSource = orderedItem;
        }
    }
}
