
using Couche_IHM.VueModeles;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameTopStat.xaml
    /// </summary>
    public partial class FrameTopStat : Page
    {
        /// <summary>
        /// Constructeur naturelle 
        /// </summary>
        public FrameTopStat()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
        }
    }
}
