
using Couche_IHM.CustomListView;
using Couche_IHM.VueModeles;
using System.Windows.Controls;


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
        public FrameCaisse()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel.Instance;

            // Initialisation des produits de la caisse
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
