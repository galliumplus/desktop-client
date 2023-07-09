using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class MainWindowViewModel
    {
        #region singleton
        private static MainWindowViewModel instance;
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
        public ProductViewModel ProductViewModel { get => productViewModel; set => productViewModel = value; }
        

        private AdherentsViewModel adherentViewModel;
        private ProductViewModel productViewModel;
        #endregion


        private User compteConnected;

        #region properties
        /// <summary>
        /// Compte connecté à gallium
        /// </summary>
        public User CompteConnected
        {
            get => compteConnected;
            set => compteConnected = value;
        }

        #endregion

        public MainWindowViewModel()
        {
            this.adherentViewModel = new AdherentsViewModel();
            //this.productViewModel = new productViewModel;
        }
    }
}
