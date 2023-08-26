using Couche_IHM.VueModeles;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameAdherent.xaml
    /// </summary>
    public partial class FrameAdherent : Page
    {
        /// <summary>
        /// Cosntructeur du frame adhérent
        /// </summary>
        public FrameAdherent()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
        }
    }
}
