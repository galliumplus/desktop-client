
using Couche_Métier.Manager;
using MaterialDesignThemes.Wpf;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Couche_IHM.VueModeles
{
    public class StatistiqueViewModel : INotifyPropertyChanged
    {

        #region attributes
        private StatProduitManager statProduitManager;
        private AcompteManager acompteManager;
        private List<StatProduitViewModel> statsProduit = new List<StatProduitViewModel>();

        private StatAcompteManager statAcompteManager;
        private ProductManager productManager;
        private List<StatAcompteViewModel> statsAcompte = new List<StatAcompteViewModel>();
        #endregion

        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region properties

        /// <summary>
        /// Podium des trois meilleurs produits
        /// </summary>
        public List<StatProduitViewModel> PodiumProduits
        {
            get
            {
                return statsProduit.OrderByDescending(x => x.PurchaseCount).Take(4).ToList();
            }
        }

        /// <summary>
        /// Podium des trois meilleurs acomptes
        /// </summary>
        public List<StatAcompteViewModel> PodiumAcompte
        {
            get
            {
                return statsAcompte.OrderByDescending(x => x.Argent).Take(4).ToList();
            }
        }

        #endregion

        #region events
        public RelayCommand FindAcompte { get; set;}
        public RelayCommand FindProduct { get; set;}
        #endregion
        #region constructor
        /// <summary>
        /// Constructeur du statistique vue modele
        /// </summary>
        public StatistiqueViewModel(ProductManager produtManager,AcompteManager acompteManager,StatAcompteManager statAcompte,StatProduitManager statProduit)
        {
            // Initialisation des objets métiers
            this.statProduitManager = statProduit;
            this.statAcompteManager = statAcompte;
            this.productManager = produtManager;
            this.acompteManager = acompteManager;

            // Initialisation des events
            this.FindProduct = new RelayCommand(product => GoToProductDetails((ProductViewModel)product));
            this.FindAcompte = new RelayCommand(acompte => GoToAcompteDetails((AcompteViewModel)acompte));

            // Initialisation des datas
            InitStatsProduit();
            InitStatsAcompte();
            
        }
        #endregion

        #region methods

        /// <summary>
        /// Permet de retrouver l'acompte en liant avec la stat et de redirigier sur ses détails
        /// </summary>
        private void GoToAcompteDetails(AcompteViewModel acompte)
        {
            MainWindowViewModel.Instance.Frame = Frame.FRAMEACOMPTE;
            MainWindowViewModel.Instance.AdherentViewModel.CurrentAcompte = MainWindowViewModel.Instance.AdherentViewModel.Acomptes.ToList().Find(x => x.Id == acompte.Id);
        }
        /// <summary>
        /// Permet de retrouver le produit en liant avec la stat et de redirigier sur ses détails
        /// </summary>
        private void GoToProductDetails(ProductViewModel product)
        {
            MainWindowViewModel.Instance.Frame = Frame.FRAMESTOCK;
            MainWindowViewModel.Instance.ProductViewModel.CurrentProduct = MainWindowViewModel.Instance.ProductViewModel.GetProducts().Find(x => x.Id == product.Id);
        }

        /// <summary>
        /// Permet d'ajouter une stat de produit
        /// </summary>
        public void AddStatProduit(StatProduit stat)
        {
            StatProduitViewModel? produitStat = this.statsProduit.Find(x => x.ProductViewModel.Id == stat.Product_id);
            if( produitStat != null)
            {
                produitStat.PurchaseCount += stat.Number_sales;
            }
            else
            {
                this.statsProduit.Add(new StatProduitViewModel(stat, new ProductViewModel(productManager.GetProducts().Find(x => x.ID == stat.Product_id),null,null,null)));
            }
            NotifyPropertyChanged(nameof(this.PodiumProduits));
        }

        /// <summary>
        /// Permet d'ajouter une stat d'acompte
        /// </summary>
        public void AddStatAcompte(StatAcompte stat)
        {
            StatAcompteViewModel? acompteStat = this.statsAcompte.Find(x => x.AdherentViewModel.Id == stat.Acompte_Id);
            if (acompteStat != null)
            {
                acompteStat.Argent += stat.Money;
            }
            else
            {
                this.statsAcompte.Add(new StatAcompteViewModel(stat, new AcompteViewModel(acompteManager.GetAdhérents().Find(x => x.Id == stat.Acompte_Id),null)));
            }
            NotifyPropertyChanged(nameof(this.PodiumAcompte));
        }

        /// <summary>
        /// Permet d'inistaliser les stats des produits
        /// </summary>
        public void InitStatsProduit()
        {
            this.statsProduit.Clear();
            List<StatProduit> statProduit = this.statProduitManager.GetStats();
            foreach (StatProduit stat in statProduit)
            {
                Product productLogic = productManager.GetProducts().Find(x => x.ID == stat.Product_id);
                this.statsProduit.Add(new StatProduitViewModel(stat, new ProductViewModel(productLogic, null, null, null)));
            }
            NotifyPropertyChanged(nameof(this.PodiumProduits));
        }


        /// <summary>
        /// Permet d'initialiser les stats des acomptes
        /// </summary>
        public void InitStatsAcompte()
        {
            this.statsAcompte.Clear();
            List<StatAcompte> statAcompte = this.statAcompteManager.GetStats();
            foreach (StatAcompte stat in statAcompte)
            {
                if (acompteManager.GetAdhérents().Find(x => x.Id == stat.Acompte_Id) is Acompte acompte)
                {
                    this.statsAcompte.Add(new StatAcompteViewModel(stat, new AcompteViewModel(acompte, null)));
                }
            }
            NotifyPropertyChanged(nameof(this.PodiumAcompte));
        }
        #endregion
    }
}
