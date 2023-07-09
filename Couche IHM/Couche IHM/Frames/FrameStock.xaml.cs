
using Couche_Métier;
using Couche_Métier.Log;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameStock.xaml
    /// </summary>
    public partial class FrameStock : Page
    {
        // Manager des produits
        private ProductManager productManager;

        // Manager des catégories
        private CategoryManager categorieManager;

        // Attributs qui gèrent si la liste est triée
        private int isSortingProductName = 0;
        private int isSortingCategorie = 0;
        private int isSortingPrix = 0;
        private int isSortingQt = 0;


        /// <summary>
        /// Constructeur de la frame des stock
        /// </summary>
        /// <param name="productManager">manager des produits</param>
        /// <param name="categoryManager">manager des catégories</param>
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
            
            this.DataContext = categorieManager;

            // Tri initial


            // Si membre du ca alors parametre pas visibles
        //    if (MainWindow.CompteConnected.Role != RolePerm.BUREAU)
        //    {
        //        this.optionsButton.Visibility = Visibility.Hidden;
        //    }
        }

        
        /// <summary>
        /// Permet de mettre à jour la liste des produits
        /// </summary>
        private void UpdateView()
        {
            this.listproduits.ItemsSource = null;
            List<Product> productMetier = this.productManager.GetProducts();

            this.listproduits.ItemsSource = productMetier;
        }


        #region events

        /// <summary>
        /// Permet de fermer la fenêtre aves les infos adhérents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseInfoAdherent(object sender, RoutedEventArgs e)
        {
            this.listproduits.SelectedItem = null;
            this.productDetails.Visibility = Visibility.Hidden;
            this.buttonValidate.Visibility = Visibility.Hidden;
            this.options.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Permet de cacher les options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideOptions(object sender, RoutedEventArgs e)
        {
            this.options.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Permet d'afficher les options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOptions(object sender, RoutedEventArgs e)
        {
            this.options.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Permet de supprimer le produit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteStock(object sender, RoutedEventArgs e)
        {
            Product productSelect = this.productManager.GetProduct((this.listproduits.SelectedItem as Product).ID);
            this.productManager.RemoveProduct(productSelect);
            productDetails.Visibility = Visibility.Hidden;

            // LOG DELETE ADHERENT
            ILog log = new LogProductToTxt();
            //log.registerLog(CategorieLog.DELETE, productSelect ,MainWindow.CompteConnected);

            UpdateView();
            this.options.Visibility = Visibility.Hidden;
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
                this.productQuantite.Text = p.Quantite.ToString();
                this.productCategorie.SelectedItem = p.Categorie.ToString();
            }
        }

        /// <summary>
        /// Permet d'afficher la liste des catégories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowCategory(object sender, RoutedEventArgs e)
        {
            FenetreCategory fenetreCategory = new FenetreCategory(this.categorieManager.ListAllCategory());
            fenetreCategory.ShowDialog();
        }

        /// <summary>
        /// Permet de rechercher un produit dans la barre de recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchProduct(object sender, TextChangedEventArgs e)
        {
            if (this.rechercheProduct.Text.Trim() != "")
            {
                this.listproduits.ItemsSource = this.productManager.GetProducts(this.rechercheProduct.Text);
            }
            else
            {

                this.listproduits.SelectedItem = null;
                this.UpdateView();
                productDetails.Visibility = Visibility.Hidden;
                this.buttonValidate.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region tri
        /// <summary>
        /// Permet de trier les produits selon leur prix
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortPrix(object sender, RoutedEventArgs e)
        {

            ControlTemplate template = this.listproduits.Template;
            Image myImage = template.FindName("prixTri", this.listproduits) as Image;
            switch (isSortingPrix)
            {
                case 0:
                    this.listproduits.Items.SortDescriptions.Add(new SortDescription("PrixAdherentIHM", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listproduits.Items.SortDescriptions.Remove(new SortDescription("PrixAdherentIHM", ListSortDirection.Ascending));
                    this.listproduits.Items.SortDescriptions.Add(new SortDescription("PrixAdherentIHM", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listproduits.Items.SortDescriptions.Remove(new SortDescription("PrixAdherentIHM", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingPrix = (isSortingPrix + 1) % 3;

        }

        /// <summary>
        /// Permet de trier les produits selon leur quantite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortQuantite(object sender, RoutedEventArgs e)
        {

            ControlTemplate template = this.listproduits.Template;
            Image myImage = template.FindName("quantiteTri", this.listproduits) as Image;
            switch (isSortingQt)
            {
                case 0:
                    this.listproduits.Items.SortDescriptions.Add(new SortDescription("Quantite", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listproduits.Items.SortDescriptions.Remove(new SortDescription("Quantite", ListSortDirection.Ascending));
                    this.listproduits.Items.SortDescriptions.Add(new SortDescription("Quantite", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listproduits.Items.SortDescriptions.Remove(new SortDescription("Quantite", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingQt = (isSortingQt + 1) % 3;

        }

        /// <summary>
        /// Permet de trier les produits selon leur catégorie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortCategorie(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = this.listproduits.Template;
            Image myImage = template.FindName("categorieTri", this.listproduits) as Image;
            switch (isSortingCategorie)
            {
                case 0:
                    this.listproduits.Items.SortDescriptions.Add(new SortDescription("Categorie", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listproduits.Items.SortDescriptions.Remove(new SortDescription("Categorie", ListSortDirection.Ascending));
                    this.listproduits.Items.SortDescriptions.Add(new SortDescription("Categorie", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listproduits.Items.SortDescriptions.Remove(new SortDescription("Categorie", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingCategorie = (isSortingCategorie + 1) % 3;
        }



        /// <summary>
        /// Permet de trier les produits selon leur nom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortProductName(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = this.listproduits.Template;
            Image myImage = template.FindName("produitTri", this.listproduits) as Image;
            switch (isSortingProductName)
            {
                case 0:
                    this.listproduits.Items.SortDescriptions.Add(new SortDescription("NomProduit", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listproduits.Items.SortDescriptions.Remove(new SortDescription("NomProduit", ListSortDirection.Ascending));
                    this.listproduits.Items.SortDescriptions.Add(new SortDescription("NomProduit", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listproduits.Items.SortDescriptions.Remove(new SortDescription("NomProduit", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingProductName = (isSortingProductName + 1) % 3;
        }

        #endregion

        /// <summary>
        /// Ajoute un produit 
        /// </summary>
        private void AddAnProduct(object sender, RoutedEventArgs e)
        {
            Product newProduct = new Product();
            FenetreAddProduct p = new FenetreAddProduct(newProduct, this.categorieManager.ListAllCategory());
            bool res = p.ShowDialog().Value;

            // Si fermé
            if(res == true)
            {
                this.productManager.CreateProduct(newProduct);
                this.UpdateView();  
            }
        }

        /// <summary>
        /// Met à jour un produit
        /// </summary>
        private void UpdateAnProduct(object sender, RoutedEventArgs e)
        {
            HideOptions();
            Product baseProduit = (Product)listproduits.SelectedItem;
            Product copyProduct = new Product(baseProduit);
            FenetreAddProduct p = new FenetreAddProduct(copyProduct, this.categorieManager.ListAllCategory());
            bool res = p.ShowDialog().Value;

            // Si la modification est validée
            if (res == true)
            {
                // On met à jour le produit
                this.productManager.UpdateProduct(copyProduct);

              
                
                this.UpdateView();
            }
        }

        /// <summary>
        /// Cache les options
        /// </summary>
        private void HideOptions()
        {
            options.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Cache les options quand la souris est en dehors de la zone
        /// </summary>
        private void HideOptionsIHM(object sender, MouseEventArgs e)
        {
            HideOptions();
        }
    }
}
