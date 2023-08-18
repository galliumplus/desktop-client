using Couche_IHM.VueModeles;
using Couche_Métier;
using Modeles;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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