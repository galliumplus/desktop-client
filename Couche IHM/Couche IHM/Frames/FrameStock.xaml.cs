
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Log;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
