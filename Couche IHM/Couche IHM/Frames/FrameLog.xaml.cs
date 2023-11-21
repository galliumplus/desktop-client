using Couche_IHM.VueModeles;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameLogs.xaml
    /// </summary>
    public partial class FrameLog : Page
    {
        public FrameLog()
        {          
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
        }
    }
}
