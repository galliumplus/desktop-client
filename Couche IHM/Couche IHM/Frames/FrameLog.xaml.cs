using Couche_IHM.VueModeles;
using Couche_Métier;
using Modeles;
using System;
using System.Collections.Generic;
using System.Windows;
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
