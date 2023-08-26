
using Couche_IHM.VueModeles;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameStock.xaml
    /// </summary>
    public partial class FrameStock : Page
    {
        /// <summary>
        /// Constructeur de la frame des stock
        /// </summary>
        public FrameStock()
        {
            InitializeComponent();       
            this.DataContext = MainWindowViewModel.Instance;

        }
    }
}
