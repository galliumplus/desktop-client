using Couche_IHM.VueModeles;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    public partial class FrameAcompte : Page
    {

        public FrameAcompte()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
        }
    }
}
