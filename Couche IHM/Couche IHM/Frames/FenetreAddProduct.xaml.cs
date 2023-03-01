using Couche_Métier;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FenetreAddProduct.xaml
    /// </summary>
    public partial class FenetreAddProduct : Window
    {
        private ProduitIHM copyProduct;

        private ProductManager productManager;

        public FenetreAddProduct(ProduitIHM p, List<string> catégories)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Initialisation
            this.copyProduct = p;
            this.productCategorie.ItemsSource = catégories;

            // Si l'objet vient d'être créer
            if(string.IsNullOrEmpty(p.Product.Categorie))
            {
                title.Content = "Création du produit";
            }
            else // Si l'objet existe déjà
            {
                title.Content = "Modification d'un produit";
                this.productCategorie.Text = copyProduct.Product.Categorie;
                this.productStock.Text = copyProduct.Product.Quantite.ToString();
                this.productPriceA.Text = copyProduct.Product.PrixAdherent.ToString();
                this.productPriceNA.Text = copyProduct.Product.PrixNonAdherent.ToString();
                this.productName.Text = copyProduct.Product.NomProduit;
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
                copyProduct.Product.Categorie = this.productCategorie.Text;
                copyProduct.Product.Quantite = Convert.ToInt32(this.productStock.Text.Trim());
                copyProduct.Product.PrixAdherent = Convert.ToDouble(this.productPriceA.Text.Trim());
                copyProduct.Product.PrixNonAdherent = Convert.ToDouble(this.productPriceNA.Text.Trim());
                copyProduct.Product.NomProduit = this.productName.Text;
                copyProduct.ImageProduit = this.productImage.Source.ToString(); // Path de l'image

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
