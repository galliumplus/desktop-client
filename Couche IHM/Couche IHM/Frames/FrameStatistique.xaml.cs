using Couche_IHM.VueModeles;
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

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameChooseStat.xaml
    /// </summary>
    public partial class FrameStatistique : Page
    {
        public FrameStatistique()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
        }
    }
}
