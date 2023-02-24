using Couche_Data;
using Couche_Métier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    /// Logique d'interaction pour FrameStock.xaml
    /// </summary>
    public partial class FrameStock : Page
    {
        // Manager de produits
        private ProductManager productManager;
        private CategoryManager categorieManager;
       
        public FrameStock(ProductManager productManager, CategoryManager categoryManager)
        {
            InitializeComponent();
            this.productDetails.Visibility = Visibility.Hidden; // Cache détails du produit

            // Récupère les managers
            this.productManager = productManager;
            this.categorieManager = categoryManager;

            // Met à jour l'affichage
            UpdateView();
            this.buttonValidate.Content = "Valider";
            this.RoleUtilisateur.Content = MainWindow.CompteConnected.Role.ToString();
            this.NomUtilisateur.Content = MainWindow.CompteConnected.NomComplet;
        }

        /// <summary>
        /// Permet de mettre à jour la liste des produits
        /// </summary>
        private void UpdateView()
        {
            this.listproduits.ItemsSource = null;
            this.listproduits.ItemsSource = this.productManager.GetProducts();
        }

        /// <summary>
        /// Charge détails du produit selectionné
        /// </summary>
        private void ShowProductDetails(object sender, SelectionChangedEventArgs e)
        {
            this.productDetails.Visibility = Visibility.Visible;
            Product p = (Product)this.listproduits.SelectedItem;
            if(p != null)
            {
                this.productName.Text = p.NomProduit;
                this.productPrice.Text = p.PrixAdherent.ToString();
                this.productQuantite.Text = p.Quantite.ToString();
                this.productCategorie.Text = p.Categorie;
            }
        }

        private void ShowCategory(object sender, RoutedEventArgs e)
        {
            FenetreCategory fenetreCategory = new FenetreCategory(this.categorieManager.ListAllCategory());
            fenetreCategory.ShowDialog();
        }
    }
}
