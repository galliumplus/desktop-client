using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Log;
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
        /// <param name="adhérentManager">manager des adhérents</param>
        public FrameAdherent()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;
        }
    }
}
