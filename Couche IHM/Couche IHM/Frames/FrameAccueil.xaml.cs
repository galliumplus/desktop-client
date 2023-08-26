using Couche_IHM.VueModeles;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameAccueil.xaml
    /// </summary>
    public partial class FrameAccueil : Page
    {
        public FrameAccueil()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
                
        }
    }
}
