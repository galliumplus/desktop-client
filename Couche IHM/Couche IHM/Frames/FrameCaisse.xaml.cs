
using Couche_IHM.CustomListView;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Utilitaire;
using Modeles;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameCaisse.xaml
    /// </summary>
    public partial class FrameCaisse : Page
    {


        /// <summary>
        /// Constructeur de la caisse
        /// </summary>
        /// <param name="adherentManager"></param>
        /// <param name="produitManager"></param>
        /// <param name="categorieManager"></param>
        public FrameCaisse()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel.Instance;

            // Initialisation des produits de la caisse
            productsSP.Children.Clear();
            foreach (CategoryViewModel category in MainWindowViewModel.Instance.ProductViewModel.Categories)
            {
                if (!category.Invisible)
                {
                    // On créer chaque vue catégorie
                    CategoryProductList categoryProductList = new CategoryProductList(category.NomCat);
                    productsSP.Children.Add(categoryProductList);
                }



            }
        }
    }
}
