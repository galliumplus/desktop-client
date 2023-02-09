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

        public FrameStock(ProductManager productManager)
        {
            InitializeComponent();
            this.productDetails.Visibility = Visibility.Hidden; // Cache détails du produit

            // Récupère le manager 
            this.productManager = productManager;

            // Remplis listView
            this.listStock.ItemsSource = this.productManager.Products;

            // Créer les headers de la listView
            ListViewStockHeader();
        }

        /// <summary>
        /// Affiche les header selon les proprietés du Produit
        /// </summary>
        private void ListViewStockHeader()
        {
            foreach(PropertyInfo p in typeof(Product).GetProperties())
            {
                GridViewColumn column = new GridViewColumn();
                column.Header = p.Name;

                column.DisplayMemberBinding = new Binding(p.Name);
                this.GridViewStockControl.Columns.Add(column);
            }
        }

        /// <summary>
        /// Charge détails du produit selectionné
        /// </summary>
        private void ShowProductDetails(object sender, SelectionChangedEventArgs e)
        {
            this.productDetails.Visibility = Visibility.Visible;
            Product p = (Product)this.listStock.SelectedItem;
            if(p != null)
            {
                this.productName.Text = p.NomProduit;
                this.productPrice.Text = p.PrixAdherent.ToString();
                this.productQuantity.Text = p.Quantite.ToString();
                this.productCategory.Text = p.Categorie;
            }
        }

       
    }
}
