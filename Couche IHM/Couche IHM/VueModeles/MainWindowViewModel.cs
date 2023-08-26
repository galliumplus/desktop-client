using Couche_Métier;
using Couche_Métier.Manager;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Couche_IHM.VueModeles
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region singleton
        private static MainWindowViewModel instance = null;
        public static MainWindowViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainWindowViewModel();
                }
                return instance;
            }
        }
        #endregion

        #region viewmodels
        public AdherentsViewModel AdherentViewModel { get => adherentViewModel; set => adherentViewModel = value; }
        public ProductsViewModel ProductViewModel { get => productViewModel; set => productViewModel = value; }
        public CaisseViewModel CaisseViewModel { get => caisseViewModel; set => caisseViewModel = value; }
        public LogsViewModel LogsViewModel { get => logsViewModel; set => logsViewModel = value; }

        public UsersViewModel UserViewModel { get => userViewModel; set => userViewModel = value; }
        public StatistiqueViewModel StatViewModel { get => statViewModel; set => statViewModel = value; }

        private AdherentsViewModel adherentViewModel;
        private ProductsViewModel productViewModel;
        private CaisseViewModel caisseViewModel;
        private LogsViewModel logsViewModel;
        private UsersViewModel userViewModel;
        private StatistiqueViewModel statViewModel;
        #endregion

        #region notify

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private UserViewModel compteConnected;
        private Frame frame = Frame.FRAMEACCUEIL;
        private LogManager logManager;
        private UserManager userManager;
        private AcompteManager acompteManager;
        private ProductManager productManager;

        #region events
        public RelayCommand GoTwitter { get; set; }
        public RelayCommand GoTwitch { get; set; }
        public RelayCommand GoInsta { get; set; }
        public RelayCommand GoDrive { get; set; }
        public RelayCommand ChangeFrame { set; get; }
        #endregion


        #region properties
        /// <summary>
        /// Compte connecté à gallium
        /// </summary>
        public UserViewModel CompteConnected
        {
            get => compteConnected;
            set => compteConnected = value;
        }

        /// <summary>
        /// Permet de créer des logs
        /// </summary>
        public LogManager LogManager
        {
            get => logManager;
            set => logManager = value;
        }

        /// <summary>
        /// Permet de gérer les comptes
        /// </summary>
        public UserManager UserManager
        {
            get => userManager;
            set => userManager = value;
        }


        /// <summary>
        /// Représente la frame actuellement affichée
        /// </summary>
        public Frame Frame
        {
            get => frame;
            set
            {
                frame = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Uri));
            }
        }

        /// <summary>
        /// L'uri de la frame
        /// </summary>
        public Uri Uri
        {
            get
            {
                return new Uri($"Frames/{frame}.xaml", UriKind.Relative);
            }
        }




        #endregion

        private MainWindowViewModel()
        {

            this.logManager = new LogManager();
            this.userManager = new UserManager();
            this.productManager = new ProductManager();
            this.acompteManager = new AcompteManager();

            this.adherentViewModel = new AdherentsViewModel(acompteManager);
            this.productViewModel = new ProductsViewModel(productManager);
            this.caisseViewModel = new CaisseViewModel();
            this.statViewModel = new StatistiqueViewModel(productManager, acompteManager);
            this.logsViewModel = new LogsViewModel(userManager, logManager);
            this.userViewModel = new UsersViewModel(this.userManager);

            // Initialisation des events
            this.ChangeFrame = new RelayCommand(fram => this.Frame = (Frame)fram);
        }

    }
}
