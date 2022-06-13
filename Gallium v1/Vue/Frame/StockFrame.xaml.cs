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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gallium_v1.Vue.Frame
{
    /// <summary>
    /// Logique d'interaction pour StockFrame.xaml
    /// </summary>
    public partial class StockFrame : Page
    {
        public StockFrame()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.rechercheProduit.IsFocused == true)
                {
                    afficheStock(this.rechercheProduit.Text);
                }
            }

        }

        /// <summary>
        /// Permet d'afficher les informations d'un user
        /// </summary>
        /// <param name="nomUser"></param>
        private void afficheStock(string stock)
        {
            Product produit = Stock.findProduit(stock);
        }
    }
}
