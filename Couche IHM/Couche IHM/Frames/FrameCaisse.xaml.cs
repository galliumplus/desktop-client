using Couche_Métier;
using Couche_Métier.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameCaisse.xaml
    /// </summary>
    public partial class FrameCaisse : Page
    {
        private List<Product> orderedItem = new List<Product>();
        private int quantityO = 0;
        private double priceO = 0.00;

        public int QuantityO { get => quantityO; set => quantityO = value; }
        public string PriceAdher { get => priceO + "€"; }
        public string PriceNanAdher { get => priceO + (0.20 * quantityO) + "€"; }

        // Liste des managers
        private AdhérentManager adherentManager;
        private ProductManager produitManager;
        private CategoryManager categorieManager;

        public FrameCaisse(AdhérentManager adherentManager, ProductManager produitManager, CategoryManager categorieManager)
        {
            InitializeComponent();
            this.adherentManager = adherentManager;
            this.produitManager = produitManager;
            this.categorieManager = categorieManager;


            productHandler.ItemsSource = produitManager.GetProducts();
            Order.ItemsSource = orderedItem;

            string[] moyenPayement = { "Acompte", "Paypal", "Carte" };
            listeMoyenPayement.ItemsSource = moyenPayement;
        }

        /// <summary>
        /// Permet de retirer un produit du panier
        /// </summary>
        private void RemoveProduct(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            int i = 0;
            while (i < orderedItem.Count)
            {
                if (orderedItem[i].NomProduit == gd.Tag)
                {
                    priceO -= orderedItem[i].PrixAdherent;
                    orderedItem.RemoveAt(i);
                    quantityO--;
                    i = orderedItem.Count;
                }
                i++;
            }
            this.QuantityOrdered.Content = Convert.ToString(QuantityO);
            if (quantityO == 0) priceO = 0.00;
            if (this.AdherCheck.IsChecked == false) this.Price.Content = PriceAdher;
            else this.Price.Content = PriceNanAdher;

            UpdateListProduitsOrder();
        }

        /// <summary>
        /// Ajoute un produit
        /// </summary
        private void AddProduct(object sender, MouseButtonEventArgs e)
        {
            Grid gd = sender as Grid;
            Label lab = gd.Children[1] as Label;
            string prodName = (string)lab.Content;

            foreach (Product p in produitManager.GetProducts())
            {
                if (p.NomProduit == prodName)
                {
                    orderedItem.Add(p);
                    quantityO++;
                    priceO += p.PrixAdherent;
                }
            }
            this.QuantityOrdered.Content = Convert.ToString(QuantityO);
            if (this.AdherCheck.IsChecked == false) this.Price.Content = PriceAdher;
            else this.Price.Content = PriceNanAdher;
            UpdateListProduitsOrder();
        }

        /// <summary>
        /// Met à jour la liste des produits
        /// </summary>
        private void UpdateListProduitsOrder()
        {
            this.Order.ItemsSource = null;
            this.Order.ItemsSource = orderedItem;
        }

        /// <summary>
        /// Change prix Adherent
        /// </summary>
        private void AdherCheck_Click(object sender, RoutedEventArgs e)
        {
            this.Price.Content = PriceNanAdher;
            if (this.AdherCheck.IsChecked == false) 
                this.Price.Content = PriceAdher;
        }

        #region BouttonIHM
        /// <summary>
        /// Change background derrière image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="color"></param>
        private void SetBackground(object sender, SolidColorBrush color)
        {
            Grid gd = sender as Grid;
            gd.Background = color;
        }

        private void Mouse_Enter(object sender, MouseEventArgs e)
        {
            this.SetBackground(sender, Brushes.Gray);
        }

        private void Mouse_Leave(object sender, MouseEventArgs e)
        {
            this.SetBackground(sender, Brushes.Transparent);
        }
        private void OrderItem_Enter(object sender, MouseEventArgs e)
        {
            this.SetBackground(sender, Brushes.Gray);
        }

        private void OrderItem_Leave(object sender, MouseEventArgs e)
        {
            this.SetBackground(sender, (SolidColorBrush)new BrushConverter().ConvertFrom("#302F2F"));
        }
        private void Buy_MouseEnter(object sender, MouseEventArgs e)
        {
            this.SetBackground(sender, Brushes.Gray);
        }

        private void Buy_MouseLeave(object sender, MouseEventArgs e)
        {
            this.SetBackground(sender, Brushes.Black);
        }
        #endregion
    }
}
