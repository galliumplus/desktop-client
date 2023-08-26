using Couche_IHM.VueModeles;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameComptes.xaml
    /// </summary>
    public partial class FrameComptes : Page
    {
        public FrameComptes()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
        }
    }
}