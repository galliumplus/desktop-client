
using Couche_Métier.Manager;
using DocumentFormat.OpenXml.Drawing.Charts;
using MaterialDesignThemes.Wpf;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        private AccountManager accountManager;
        private List<StatProduitViewModel> statsProduit = new List<StatProduitViewModel>();

        private StatAccountManager statAccountManager;
        private ProductManager productManager;
        private List<StatAccountViewModel> statsAccount = new List<StatAccountViewModel>();

        private DateTime date = DateTime.Now;
       
        #endregion

        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region properties
        public int FirstPurchaseCount
        {
            get
            {
                return PodiumProduits[0].PurchaseCount;
            }
        }
        public float FirstArgent
        {
            get
            {
                return PodiumAccount[0].Argent;
            }
        }

        /// <summary>
        /// Représente la date de recherche par semaine
        /// </summary>
        public string DateSemaine
        {
            get
            {

                return $"Du {date.Day} au {date.AddDays(6).Day} {date:MMMM} {date:yyyy}";
            }
        }
        public List<StatAccountViewModel> BestAcomptes
        {
            get
            {
                CultureInfo ci = CultureInfo.CurrentCulture;
                CalendarWeekRule rule = ci.DateTimeFormat.CalendarWeekRule;
                DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
                int week = ci.Calendar.GetWeekOfYear(date, rule, firstDayOfWeek) - 1;

                List<StatAccount> statAcomptes = this.statAccountManager.GetStats(week, date.Year).OrderByDescending(x => x.Money).Take(10).ToList();
                List<StatAccountViewModel> statAcomptesVM = new List<StatAccountViewModel>();
                foreach (StatAccount stat in statAcomptes)
                {
                    if (accountManager.GetAdhérents().Find(x => x.Id == stat.Account_Id) is Account acompte)
                    {
                        statAcomptesVM.Add(new StatAccountViewModel(stat, new AccountViewModel(acompte, this.accountManager)));
                    }
                }

                return statAcomptesVM;
            }
        }

        public List<StatProduitViewModel> BestProducts
        {
            get
            {
                CultureInfo ci = CultureInfo.CurrentCulture;
                CalendarWeekRule rule = ci.DateTimeFormat.CalendarWeekRule;
                DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
                int week = ci.Calendar.GetWeekOfYear(date, rule, firstDayOfWeek) - 1;

                List<StatProduit> statProduit = this.statProduitManager.GetStats(week, date.Year).OrderByDescending(x => x.Number_sales).Take(10).ToList();
                List<StatProduitViewModel> statProductVM = new List<StatProduitViewModel>();
                foreach (StatProduit stat in statProduit)
                {
                    if (productManager.GetProducts().Find(x => x.ID == stat.Product_id) is Product productLogic)
                    {
                        statProductVM.Add(new StatProduitViewModel(stat, new ProductViewModel(productLogic, null, null, null)));
                    }
                }

                return statProductVM;
            }
        }
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
        public List<StatAccountViewModel> PodiumAccount
        {
            get
            {
                return statsAccount.OrderByDescending(x => x.Argent).Take(4).ToList();
            }
        }

        #endregion

        #region events
        public RelayCommand FindAccount { get; set;}
        public RelayCommand FindProduct { get; set;}
        public RelayCommand NextWeek { get; set; }
        public RelayCommand PreviousWeek { get; set; }
        #endregion
        #region constructor
        /// <summary>
        /// Constructeur du statistique vue modele
        /// </summary>
        public StatistiqueViewModel(ProductManager produtManager,AccountManager accountManager,StatAccountManager statAccount,StatProduitManager statProduit)
        {
            // Initialisation des objets métiers
            this.statProduitManager = statProduit;
            this.statAccountManager = statAccount;
            this.productManager = produtManager;
            this.accountManager = accountManager;

            // Initialisation des events
            this.FindProduct = new RelayCommand(product => GoToProductDetails((ProductViewModel)product));
            this.FindAccount = new RelayCommand(acompte => GoToAccountDetails((AccountViewModel)acompte));
            this.NextWeek = new RelayCommand(x => GetStatNextWeek());
            this.PreviousWeek = new RelayCommand(x => GetStatPreviousWeek());

            DayOfWeek firstDay = DayOfWeek.Monday; 
            int diff = date.DayOfWeek - firstDay;
            if (diff < 0)
            {
                diff += 7; // Ajoute 7 jours pour revenir à la semaine précédente
            }
            date = date.AddDays(-diff);

           

            // Initialisation des datas
            InitStatsProduit();
            InitStatsAccount();
            
        }
        #endregion

        #region methods
        /// <summary>
        /// Permet d'obtenir les stats du jour d'après
        /// </summary>
        private void GetStatNextWeek()
        {
            date = this.date.AddDays(7);
            NotifyPropertyChanged(nameof(this.BestAcomptes));
            NotifyPropertyChanged(nameof(this.BestProducts));
            NotifyPropertyChanged(nameof(this.DateSemaine));
        }

        /// <summary>
        /// Permet d'obtenir les stats du jour d'après
        /// </summary>
        private void GetStatPreviousWeek()
        {
            date =this.date.AddDays(-7);
            NotifyPropertyChanged(nameof(this.BestAcomptes));
            NotifyPropertyChanged(nameof(this.BestProducts));
            NotifyPropertyChanged(nameof(this.DateSemaine));
        }
        /// <summary>
        /// Permet de retrouver l'acompte en liant avec la stat et de redirigier sur ses détails
        /// </summary>
        private void GoToAccountDetails(AccountViewModel acompte)
        {
            MainWindowViewModel.Instance.Frame = Frame.FRAMEACCOUNT;
            MainWindowViewModel.Instance.AccountsViewModel.CurrentAccount = MainWindowViewModel.Instance.AccountsViewModel.Accounts.ToList().Find(x => x.Id == acompte.Id);
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
        public void AddStatAccount(StatAccount stat)
        {
            StatAccountViewModel? acompteStat = this.statsAccount.Find(x => x.AccountsViewModel.Id == stat.Account_Id);
            if (acompteStat != null)
            {
                acompteStat.Argent += stat.Money;
            }
            else
            {
                this.statsAccount.Add(new StatAccountViewModel(stat, new AccountViewModel(accountManager.GetAdhérents().Find(x => x.Id == stat.Account_Id),this.accountManager)));
            }
            NotifyPropertyChanged(nameof(this.PodiumAccount));
        }

        /// <summary>
        /// Permet d'inistaliser les stats des produits
        /// </summary>
        public void InitStatsProduit()
        {
            this.statsProduit.Clear();
            CultureInfo ci = CultureInfo.CurrentCulture;
            CalendarWeekRule rule = ci.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
            int currentWeek = ci.Calendar.GetWeekOfYear(DateTime.Now, rule, firstDayOfWeek) - 1;
            List<StatProduit> statProduit = this.statProduitManager.GetStats(currentWeek, DateTime.Now.Year);
            foreach (StatProduit stat in statProduit)
            {
                if (productManager.GetProducts().Find(x => x.ID == stat.Product_id) is Product productLogic)
                {
                    this.statsProduit.Add(new StatProduitViewModel(stat, new ProductViewModel(productLogic, null, null, null)));
                }
            }
            NotifyPropertyChanged(nameof(this.PodiumProduits));
        }


        /// <summary>
        /// Permet d'initialiser les stats des acomptes
        /// </summary>
        public void InitStatsAccount()
        {
            this.statsAccount.Clear();
            CultureInfo ci = CultureInfo.CurrentCulture;
            CalendarWeekRule rule = ci.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
            int currentWeek = ci.Calendar.GetWeekOfYear(DateTime.Now, rule, firstDayOfWeek) - 1;
            List<StatAccount> statAccount = this.statAccountManager.GetStats(currentWeek,DateTime.Now.Year);
            foreach (StatAccount stat in statAccount)
            {
                if (accountManager.GetAdhérents().Find(x => x.Id == stat.Account_Id) is Account acompte)
                {
                    this.statsAccount.Add(new StatAccountViewModel(stat, new AccountViewModel(acompte, this.accountManager)));
                }
            }
            NotifyPropertyChanged(nameof(this.PodiumAccount));
        }


        #endregion
    }
}
