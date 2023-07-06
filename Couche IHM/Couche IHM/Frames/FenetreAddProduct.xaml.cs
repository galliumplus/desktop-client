using Couche_IHM.ImagesProduit;
using Couche_Métier;
using Microsoft.Win32;
using Modeles;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Fenêtre pour ajouter et modifier les produits
    /// </summary>
    public partial class FenetreAddProduct : Window
    {
        // Produit à créer ou à modifier
        private Product product;

        private ProductManager productManager;

        /// <summary>
        /// Constructeur de la fenêtre 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="catégories"></param>
        public FenetreAddProduct(Product p, List<string> catégories)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Initialisation
            this.product = p;
            this.productCategorie.ItemsSource = catégories;

            // Création d'un produit
            if(string.IsNullOrEmpty(p.NomProduit))
            {
                title.Content = "Création du produit";
            }
            // Modification d'un produit
            else
            {
                title.Content = "Modification d'un produit";

                // On met à jour les champs
                this.productCategorie.Text = product.Categorie;
                this.productStock.Text = product.Quantite.ToString();
                this.productPriceA.Text = product.PrixAdherent.ToString();
                this.productPriceNA.Text = product.PrixNonAdherent.ToString();
                this.productName.Text = product.NomProduit;
                ImageManager image = new ImageManager();
                this.productImage.Source = new BitmapImage(new Uri(image.GetImageFromProduct(this.product.NomProduit), UriKind.Absolute));
                
            }
        }

        /// <summary>
        /// Quand l'utilisateur clique sur l'image, permet de changer l'image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePicture(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fn = new OpenFileDialog();
            fn.Title = "Choisir une nouvelle image";
            fn.Filter = "Images png (*.png)|*.png| Images jpeg (*.jpeg)|*.jpeg";
            fn.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            if(fn.ShowDialog() == true)
            {
                productImage.Source = new BitmapImage(new Uri(fn.FileName, UriKind.Absolute));
            }
        }

        /// <summary>
        /// Annule les changements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelChangement(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Valide les changements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateChangement(object sender, RoutedEventArgs e)
        {
            if (IsAdherentNotNull())
            {
                product.Categorie = this.productCategorie.Text;
                product.Quantite = Convert.ToInt32(this.productStock.Text.Trim());
                product.PrixAdherent = Convert.ToDouble(this.productPriceA.Text.Trim());
                product.PrixNonAdherent = Convert.ToDouble(this.productPriceNA.Text.Trim());
                product.NomProduit = this.productName.Text;

                // Met à jour l'image
                ImageManager image = new ImageManager();
                image.CreateImageFromBlob(product.NomProduit, image.ConvertImageToBlob(this.productImage.Source.ToString()));

                this.DialogResult = true;
            }
        }

        /// <summary>
        /// Change le style de l'image quand la souris va dessus
        /// </summary>
        private void StyleImageEnter(object sender, MouseEventArgs e)
        {
            this.modifImage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Change le style de l'image quand la souris part
        /// </summary>
        private void StyleImageLeave(object sender, MouseEventArgs e)
        {
            this.modifImage.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Vérifie que le formulaire n'est pas vide
        /// </summary>
        /// <returns> true si il n'est pas vide, false si il est vide</returns>
        private bool IsAdherentNotNull()
        {
            bool notnull = true;

            // Catégorie
            if (productCategorie.SelectedValue is null)
            {
                notnull = false;
            }
            
            // Stock
            if(string.IsNullOrEmpty(productStock.Text))
            {
                notnull = false;
            }

            // Prix Adhérent
            if (string.IsNullOrEmpty(productPriceA.Text))
            {
                notnull = false;
            }

            // Prix non adhérent
            if (string.IsNullOrEmpty(productPriceNA.Text))
            {
                notnull = false;
            }

            // Nom du produit
            if (string.IsNullOrEmpty(this.productName.Text))
            {
                notnull = false;
            }

            return notnull;
        }

        /// <summary>
        /// Empêche d'écrire si c'est pas un nombre
        /// </summary>
        private void IsNumberBox(object sender, TextCompositionEventArgs e)
        {
            // Empêcher la saisie de caractères qui ne sont pas des chiffres ou une seule virgule
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ",")
            {
                e.Handled = true;
            }
            // Empêcher la saisie de virgule si elle est déjà présente
            else if (e.Text == "," && ((TextBox)sender).Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Empêche d'écrir si c'est pas un entier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsIntegerBox(object sender, TextCompositionEventArgs e)
        {
            // Empêcher la saisie de caractères qui ne sont pas des chiffres
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Empêche l'utilisateur d'entrer un espace
        /// </summary>
        private void isSpaceBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
