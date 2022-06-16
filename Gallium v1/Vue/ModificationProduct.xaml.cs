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

namespace Gallium_v1.Vue
{
    /// <summary>
    /// Logique d'interaction pour ModificationProduct.xaml
    /// </summary>
    public partial class ModificationProduct : Window
    {
        private Product produit;
        public Product Produit
        {
            get => produit;
        }

        public ModificationProduct(Product p)
        {
            InitializeComponent();
            this.produit = p;
            this.ChargeListCategorie();
            this.chargeProduct();
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
                this.produit.PrixProduitAdhérent = Convert.ToInt32(this.prixAdherent.Text);
                this.Close();
            }
        }

        /// <summary>
        /// Permet d'annuler les modifications
        /// </summary>
        /// <Author> Damien.C </Author>
        private void AnnulerModif(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Méthode qui charge les informations de l'acompte
        /// </summary>
        /// <Author> Damien.C </Author>
        private void chargeProduct()
        {
            this.produitName.Text = this.produit.NomProduit;
            this.choixCatégorie.SelectedItem = this.produit.Categorie.ToString();
            this.stock.Text = this.produit.Stock.ToString();
            this.prixAdherent.Text = this.produit.PrixProduitAdhérent.ToString();
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

       
    }
}
