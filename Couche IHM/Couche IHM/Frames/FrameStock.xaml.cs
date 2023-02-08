using Couche_Data;
using Couche_Métier;
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
    /// Logique d'interaction pour FrameStock.xaml
    /// </summary>
    public partial class FrameStock : Page
    {
        // Manager de produits
        private ProductManager productManager;

        public FrameStock(ProductManager productManager)
        {
            InitializeComponent();
            
            this.productManager = productManager;

            // Remplis listView
            this.listStock.ItemsSource = this.productManager.Products;

            // ListView groupement de donnée = https://wpf-tutorial.com/fr/79/le-controle-listview/listview-et-groupement-de-donnees/
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.listStock.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("CATEGORIE");
            view.GroupDescriptions.Add(groupDescription);


            // Je suis trop con
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
    }
}
