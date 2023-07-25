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

        private AdherentsViewModel adherentViewModel;
        private ProductsViewModel productViewModel;
        private CaisseViewModel caisseViewModel;
        #endregion

        #region notify

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private User compteConnected;
        private Frame frame = Frame.FRAMEACCUEIL;

        public RelayCommand ChangeFrame { set; get; }
        #region properties
        /// <summary>
        /// Compte connecté à gallium
        /// </summary>
        public User CompteConnected
        {
            get => compteConnected;
            set => compteConnected = value;
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
            this.adherentViewModel = new AdherentsViewModel();
            this.productViewModel = new ProductsViewModel();
            this.caisseViewModel = new CaisseViewModel();
            this.ChangeFrame = new RelayCommand(fram => this.Frame = (Frame)fram);
        }
    }
}
