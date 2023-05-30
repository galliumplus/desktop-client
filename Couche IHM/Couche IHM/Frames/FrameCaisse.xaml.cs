using Couche_Data;
using Couche_IHM.CustomListView;
using Couche_Métier;
using Couche_Métier.Log;
using Couche_Métier.Utilitaire;
using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComboBox = System.Windows.Controls.ComboBox;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameCaisse.xaml
    /// </summary>
    public partial class FrameCaisse : Page
    {
        // Produits IHM
        private List<Product> produits = new List<Product>();
        private ObservableCollection<KeyValuePair<Product, int>> orderedItem = new ObservableCollection<KeyValuePair<Product, int>>();


        /// <summary>
        /// Renvoie le prix total du panier pour les adhérents
        /// </summary>
        public float PriceAdher
        {
            get
            {
                float prixTotal = 0.00f;
                foreach (KeyValuePair<Product, int> product in orderedItem)
                {
                    prixTotal += (float)product.Key.PrixAdherent * product.Value;
                }
                return prixTotal;
            }
        }
        /// <summary>
        /// Renvoie le prix total du panier pour les non adhérents
        /// </summary>
        public float PriceNanAdher
        {
            get
            {
                float prixTotal = 0.00f;
                foreach (KeyValuePair<Product, int> product in orderedItem)
                {
                    prixTotal += (float)product.Key.PrixNonAdherent * product.Value;
                }
                return prixTotal;
            }

        }

        // Liste des managers
        private AdhérentManager adherentManager;
        private ProductManager produitManager;
        private CategoryManager categorieManager;

        private ILog log = new LogVenteToTxt();

        /// <summary>
        /// Constructeur de la caisse
        /// </summary>
        /// <param name="adherentManager"></param>
        /// <param name="produitManager"></param>
        /// <param name="categorieManager"></param>
        public FrameCaisse(AdhérentManager adherentManager, ProductManager produitManager, CategoryManager categorieManager)
        {
            InitializeComponent();
            this.adherentManager = adherentManager;
            this.produitManager = produitManager;
            this.categorieManager = categorieManager;

            // Initialisation des moyens de paiement
            string[] moyenPayement = { "Acompte", "Paypal", "Carte" };
            listeMoyenPayement.ItemsSource = moyenPayement;

            UpdateListProduitsOrder();
        }



        /// <summary>
        /// Permet de retirer un produit du panier 
        /// </summary>
        private void RemoveProduct(object sender, MouseButtonEventArgs e)
        {
            // On retrouve le produit
            System.Windows.Controls.Image buttonRemove = (System.Windows.Controls.Image)sender;
            int index = doesProductExist(buttonRemove.Tag.ToString());

            // Mise à jour de la quantité
            orderedItem[index] = new KeyValuePair<Product, int>(orderedItem[index].Key, orderedItem[index].Value - 1);

            // Si quantité inférieur ou égale à 0 alors on supprime le produit
            if (orderedItem[index].Value <= 0)
            {
                orderedItem.Remove(orderedItem[index]);
            }

            ConverterFormatArgent converterFormat = new ConverterFormatArgent();
            this.PriceA.Content = converterFormat.ConvertToString(this.PriceAdher);
            this.PriceNA.Content = "(" + converterFormat.ConvertToString(this.PriceNanAdher) + ")";
        }

        /// <summary>
        /// Ajoute un produit au panier
        /// </summary
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            // Récupère le produit séléctionné 
            System.Windows.Controls.Button gd = (System.Windows.Controls.Button)sender;
            Product productSelected = ((StackPanel)gd.Content).DataContext as Product;

            // On regarde s'il est déjà dans le panier
            int index = doesProductExist(productSelected.NomProduit);
            if (index != -1)
            {
                // On ajoute que si le produit ne va pas manquer
                if (productSelected.Quantite >= orderedItem[index].Value + 1)
                {
                    orderedItem[index] = new KeyValuePair<Product, int>(productSelected, orderedItem[index].Value + 1);
                }
                
            }
            else
            {
                orderedItem.Add(new(productSelected, 1));
            }
            ConverterFormatArgent converterFormat = new ConverterFormatArgent();
            this.PriceA.Content = converterFormat.ConvertToString(this.PriceAdher);
            this.PriceNA.Content = "(" +  converterFormat.ConvertToString(this.PriceNanAdher) + ")";
        }

        /// <summary>
        /// Permet de savoir si le produit a déjà été commandé
        /// </summary>
        /// <returns>-1 s'il existe pas , l'index sinon</returns>
        private int doesProductExist(string productName)
        {
            int itemFound = -1;
            int compteur = 0;
            foreach (KeyValuePair<Product, int> item in orderedItem)
            {   
                if (item.Key.NomProduit == productName)
                {
                    itemFound = compteur;
                    break;
                }
                compteur++;
            }
            return itemFound;
        }
        /// <summary>
        /// Met à jour la liste des produits
        /// </summary>
        private void UpdateListProduitsOrder()
        {

            // Initialisation des produits de la caisse
            productsSP.Children.Clear();
            foreach (string category in categorieManager.Categories)
            {
                // On récupère les produits de chaque catégorie
                List<Product> products = produitManager.GetProductsByCategory(category);

                // On créer chaque vue catégorie
                CategoryProductList categoryProductList = new CategoryProductList(category, products);
                productsSP.Children.Add(categoryProductList);

                // On créer des listener sur chaque items
                foreach (DetailedProduct p in categoryProductList.ListProductView)
                {
                    p.boutonDp.Click += AddProduct;
                }
            }

            // Initialisation du panier
            Order.ItemsSource = null;
            Order.ItemsSource = orderedItem;

        }


        #region BouttonIHM

        /// <summary>
        /// Valide le paiment 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PayCaisse(object sender, RoutedEventArgs e)
        {
            bool result = false;
            Adhérent adhérentSelectionne = null;
            // Si Achat acompte
            if(listeMoyenPayement.SelectedItem != null)
            {
                switch (listeMoyenPayement.SelectedItem.ToString())
                {
                    case "Acompte":
                        PaiementAcompteWindow fn = new PaiementAcompteWindow(this.adherentManager.GetAdhérents(),this.PriceAdher,this.PriceNanAdher);
                        result = fn.ShowDialog().Value;
                        if (result)
                        {
                            adhérentSelectionne = fn.AdhérentSelectionné;
                        }
                        break;
                    case "Liquide":
                        break;
                    case "Carte":
                        break;
                    case "Paypal":
                        break;
                }

            }

            // Si vente reussite
            if (result == true)
            {
                // On enleve les produits du stock
                for (int i = 0; i < orderedItem.Count; i++)
                {
                    orderedItem[i].Key.Quantite -= orderedItem[i].Value;
                }
                this.orderedItem.Clear();
                UpdateListProduitsOrder();
                ConverterFormatArgent converterFormat = new ConverterFormatArgent();
                this.PriceA.Content = converterFormat.ConvertToString(this.PriceAdher);
                this.PriceNA.Content = "(" + converterFormat.ConvertToString(this.PriceNanAdher) + ")";
                this.adherentManager.UpdateAdhérent(adhérentSelectionne);
                this.log.registerLog(CategorieLog.VENTE, adhérentSelectionne, MainWindow.CompteConnected);
            }
        }

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
