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
using System.Windows.Shapes;

namespace Gallium_v1.Vue.Modification
{
    /// <summary>
    /// Logique d'interaction pour ModificationProduct.xaml
    /// </summary>
    public partial class ModificationProduct : Window
    {

        private Product oldProduct;
        private Product produit;

        /// <summary>
        /// Produit modifié
        /// </summary>
        public Product Produit
        {
            get => produit;
            private set => produit = value;
        }

        public ModificationProduct(Product p)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.oldProduct = p;
            this.produit = p;
            this.ChargeListCategorie();
            this.ChargeProduct();
            this.PositionCaretIndex();
            produitName.Focus();
        }

        /// <summary>
        /// Valide les modifications faite et de mettre à jour l'utilisateur
        /// </summary>
        /// <Author> Damien.C </Author>
        private void ValiderModif(object sender, RoutedEventArgs e)
        {
            // demande si l'utilisateur est sur de la modification
            MessageBoxResult validation = MessageBox.Show("Vous allez modifier ce produit.", "Modifier le produit ?", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (validation.Equals(MessageBoxResult.OK))
            {
                this.produit.NomProduit = this.produitName.Text;
                this.produit.Stock = Convert.ToInt32(this.stock.Text);
                this.produit.PrixProduitAdhérent = Convert.ToDouble(this.prixAdherent.Text);
                // manque catégorie
                this.Close();
            }
        }

        /// <summary>
        /// Permet d'annuler les modifications
        /// </summary>
        /// <Author> Damien.C </Author>
        private void AnnulerModif(object sender, RoutedEventArgs e)
        {
            this.Produit = oldProduct;
            this.Close();
        }

        /// <summary>
        /// Méthode qui charge les informations de l'acompte
        /// </summary>
        /// <Author> Damien.C </Author>
        private void ChargeProduct()
        {
            this.produitName.Text = this.produit.NomProduit;
            this.choixCatégorie.SelectedItem = this.produit.Categorie.ToString();
            this.stock.Text = this.produit.Stock.ToString();
            this.prixAdherent.Text = this.produit.PrixProduitAdhérent.ToString();
        }

        /// <summary>
        /// Empêche de mettre des lettres dans la textebox
        /// </summary>
        /// <Author> Damien.C </Author>
        private void NoLetterTextChanged(object sender, TextCompositionEventArgs e)
        {
            var tb = sender as TextBox;
            e.Handled = !double.TryParse(tb.Text + e.Text, out double d);
        }




        /// <summary>
        /// Ajout 1 produit 
        /// </summary>
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            produit.Stock++;
            this.stock.Text = produit.Stock.ToString();
        }

        /// <summary>
        /// Enlève un produit
        /// </summary>
        private void RemoveProduct(object sender, RoutedEventArgs e)
        {
            if (produit.Stock != 0)
            {
                produit.Stock--;
                this.stock.Text = produit.Stock.ToString();
            }
            
            
        }


        /// <summary>
        /// Charge dans la comboBox tous les élements de l'énumration Catagégorie produit 
        /// </summary>
        private void ChargeListCategorie()
        {
            foreach (Category element in Enum.GetValues(typeof(Category)))
            {
                choixCatégorie.Items.Add(element);
            }
        }

        /// <summary>
        /// Initialise position du Caret
        /// </summary>
        private void PositionCaretIndex()
        {

            produitName.CaretIndex = produitName.Text.Length;
            prixAdherent.CaretIndex = prixAdherent.Text.Length;
            stock.CaretIndex = stock.Text.Length;
        }



    }
}
