using Couche_Data;
using Couche_IHM.Frames;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Manager;
using Modeles;
using System;
using System.Windows;

namespace Couche_IHM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Constructeur de la mainwindow
        /// </summary>
        /// <param name="user"></param>
        public MainWindow(User user,LogManager logManager,UserManager userManager)
        {
            InitializeComponent();
            MainWindowViewModel.Instance.CompteConnected = new UserViewModel(user,userManager);
            DataContext = MainWindowViewModel.Instance;
        }


    }
}
