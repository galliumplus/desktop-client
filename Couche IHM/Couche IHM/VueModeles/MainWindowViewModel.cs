using Couche_Métier;
using Couche_Métier.Manager;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Couche_IHM.VueModeles
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region singleton
        private static MainWindowViewModel instance = null;

        public static MainWindowViewModel Instance => instance ?? throw new NullReferenceException("Le MainWindowViewModel doit d'abord être instancié par la fenêtre.");

        public static MainWindowViewModel GetInstanceFor(MainWindow mainWindow)
        {
            if (instance == null)
            {
                instance = new MainWindowViewModel(mainWindow);
            }
            return instance;
        }
        #endregion

        #region viewmodels
        public AcomptesViewModel AdherentViewModel { get => adherentViewModel; set => adherentViewModel = value; }
        public ProductsViewModel ProductViewModel { get => productViewModel; set => productViewModel = value; }
        public PartenariatViewModel PartenariatViewModel { get => partenariatViewModel; set => partenariatViewModel = value; }
        public CaisseViewModel CaisseViewModel { get => caisseViewModel; set => caisseViewModel = value; }
        public LogsViewModel LogsViewModel { get => logsViewModel; set => logsViewModel = value; }

        public UsersViewModel UserViewModel { get => userViewModel; set => userViewModel = value; }
        public StatistiqueViewModel StatViewModel { get => statViewModel; set => statViewModel = value; }

        private AcomptesViewModel adherentViewModel;
        private ProductsViewModel productViewModel;
        private PartenariatViewModel partenariatViewModel;
        private CaisseViewModel caisseViewModel;
        private LogsViewModel logsViewModel;
        private UsersViewModel userViewModel;
        private StatistiqueViewModel statViewModel;
        private MainWindow mainWindow;
        #endregion

        #region notify

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private UserViewModel compteConnected;
        private Frame frame = Frame.FRAMEACCUEIL;
        private LogManager logManager;
        private UserManager userManager;
        private AcompteManager acompteManager;
        private ProductManager productManager;
        private StatAcompteManager statAcompteManager;
        private StatProduitManager statProduitManager;
        #endregion

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


        public MainWindow MainWindow => mainWindow;

        #endregion

        /// <summary>
        /// Constructeur du mainwindow vue modele
        /// </summary>
        private MainWindowViewModel(MainWindow mainWindow)
        {
            this.logManager = new LogManager();
            this.userManager = new UserManager();
            this.productManager = new ProductManager();
            this.acompteManager = new AcompteManager();
            this.statAcompteManager = new StatAcompteManager();
            this.statProduitManager = new StatProduitManager();
            this.adherentViewModel = new AcomptesViewModel(acompteManager);
            this.productViewModel = new ProductsViewModel(productManager);
            this.caisseViewModel = new CaisseViewModel(this.statAcompteManager, this.statProduitManager);
            this.statViewModel = new StatistiqueViewModel(productManager, acompteManager, statAcompteManager, statProduitManager);
            this.logsViewModel = new LogsViewModel(userManager, logManager);
            this.userViewModel = new UsersViewModel(this.userManager);
            this.partenariatViewModel = new PartenariatViewModel();
            this.mainWindow = mainWindow;

            // Initialisation des events
            this.ChangeFrame = new RelayCommand(fram => this.Frame = (Frame)fram);
        }
    }
}
