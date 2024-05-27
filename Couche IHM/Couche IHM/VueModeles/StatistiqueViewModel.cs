
using Couche_Métier.Manager;
using DocumentFormat.OpenXml.Drawing.Charts;
using MaterialDesignThemes.Wpf;
using Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<string> typeEvolutions;
        private ProductViewModel currentEvolProduct;
        private AccountViewModel currentEvolAcompte;
        private string typeEvolutionChoisi;
        private string typeRechercheStat;

        private DateTime date = DateTime.Now;
        private int anneeSelected;

       
        #endregion

        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region properties

        public List<int> AnneeList
        {
            get
            {
                return new List<int>() { DateTime.Now.Year, DateTime.Now.Year-1, DateTime.Now.Year - 2   };
            }
        }

        public List<String> TypeRechercheList
        {
            get
            {
                return new List<String>() { "Par semaine", "Par mois", "Par année" };
            }
        }
        /// <summary>
        /// Représente la date de recherche 
        /// </summary>
        public string DateIHM
        {
            get
            {
                string dateFormatted = "";
                switch (typeRechercheStat)
                {
                    case "Par semaine":
                        dateFormatted = $"Du {date.Day} au {date.AddDays(6).Day} {date:MMMM} {date:yyyy}";
                        break;
                    case "Par mois":
                        dateFormatted = $"{date:MMMM} {date:yyyy}";
                        break;
                    case "Par année":
                        dateFormatted = $"{date:yyyy}";
                        break;
                }
                return dateFormatted;
            }
        }

        /// <summary>
        /// Représente le top des acompts de la semaine choisie
        /// </summary>
        public List<StatAccountViewModel> BestAcomptes
        {
            get
            {
                

                List<StatAccount> statAcomptes = new List<StatAccount>();
                switch (typeRechercheStat)
                {
                    case "Par semaine":
                        CultureInfo ci = CultureInfo.CurrentCulture;
                        CalendarWeekRule rule = ci.DateTimeFormat.CalendarWeekRule;
                        DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
                        int week = ci.Calendar.GetWeekOfYear(date, rule, firstDayOfWeek) - 1;
                        statAcomptes = this.statAccountManager.GetStatsByWeek(week, date.Year).OrderByDescending(x => x.Money).Take(10).ToList();
                        break;
                    case "Par mois":
                        statAcomptes = this.statAccountManager.GetStatsByMonth(date.Month, date.Year).OrderByDescending(x => x.Money).Take(10).ToList();
                        break;
                    case "Par année":
                        statAcomptes = this.statAccountManager.GetStatsByYear(date.Year).OrderByDescending(x => x.Money).Take(10).ToList();
                        break;
                }

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
        /// <summary>
        /// Représente le top des produits de la semaine choisie
        /// </summary>
        public List<StatProduitViewModel> BestProducts
        {
            get
            {
                List<StatProduit> statProduit = new List<StatProduit>();
                switch (typeRechercheStat)
                {
                    case "Par semaine":
                        CultureInfo ci = CultureInfo.CurrentCulture;
                        CalendarWeekRule rule = ci.DateTimeFormat.CalendarWeekRule;
                        DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
                        int week = ci.Calendar.GetWeekOfYear(date, rule, firstDayOfWeek) - 1;
                        statProduit = this.statProduitManager.GetStatsByWeek(week, date.Year).OrderByDescending(x => x.Number_sales).Take(10).ToList();
                        break;
                    case "Par mois":
                        statProduit = this.statProduitManager.GetStatsByMonth(date.Month, date.Year).OrderByDescending(x => x.Number_sales).Take(10).ToList();
                        break;
                    case "Par année":
                        statProduit = this.statProduitManager.GetStatsByYear(date.Year).OrderByDescending(x => x.Number_sales).Take(10).ToList();
                        break;
                }
               

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


        private ObservableCollection<StatProduitViewModel> evolProducts;
        

        /// <summary>
        /// Représente le top des produits de la semaine choisie
        /// </summary>
        public ObservableCollection<StatProduitViewModel> EvolProducts
        {
            set => evolProducts = value;
            get
            {
                return evolProducts;
            }
        }

        private ObservableCollection<StatAccountViewModel> evolAcomptes;


        /// <summary>
        /// Représente le top des produits de la semaine choisie
        /// </summary>
        public ObservableCollection<StatAccountViewModel> EvolAcomptes
        {
            set => evolAcomptes = value;
            get
            {
                return evolAcomptes;
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
        public RelayCommand Next { get; set; }
        public RelayCommand Previous { get; set; }
        public string TypeRechercheStat 
        { 
            get => typeRechercheStat;
            set 
            { 
                typeRechercheStat = value;
                NotifyPropertyChanged(nameof(this.DateIHM));
                NotifyPropertyChanged(nameof(this.BestProducts));
                NotifyPropertyChanged(nameof(this.BestAcomptes));
            }
        }


        public string TitreEvolution
        {
            get
            {
                string titre = "";
                if (typeEvolutionChoisi == "Produit")
                {
                    if (currentEvolProduct != null)
                    {
                        titre = $"Vente de {currentEvolProduct.NomProduitIHM} par mois";
                    }
                }
                else
                {
                    if (currentEvolAcompte != null)
                    {
                        titre = $"Dépenses de {currentEvolAcompte.NomCompletIHM} par mois";
                    }
                }
               
                return titre;
            }
        }
        /// <summary>
        /// Représente les types d'évolutions disponibles
        /// </summary>
        public List<string> TypeEvolutions 
        { 
            get => typeEvolutions; 
            set => typeEvolutions = value; 
        }
        /// <summary>
        /// Représente le type d'évolution choisi
        /// </summary>
        public string TypeEvolutionChoisi
        {
            get => typeEvolutionChoisi;
            set 
            { 
                typeEvolutionChoisi = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(this.TitreEvolution));
            }
        }

        /// <summary>
        /// Représente le produit dont on souhaite connaitre l'évolution
        /// </summary>
        public ProductViewModel CurrentEvolProduct
        {
            get => currentEvolProduct;
            set {
                currentEvolProduct = value;
                InitStatOfProduct(currentEvolProduct.Id);
                NotifyPropertyChanged(nameof(this.TitreEvolution));
            }
        }

        public int AnneeSelected 
        { 
            get => anneeSelected;
            set
            {
                anneeSelected = value;
                if (typeEvolutionChoisi == "Produit")
                {
                    InitStatOfProduct(currentEvolProduct.Id);
                }
                else
                {
                    InitStatOfAcompte(currentEvolAcompte.Id);
                }
            }
            
        }

        public AccountViewModel CurrentEvolAccount
        {
            get => currentEvolAcompte;
            set
            {
                currentEvolAcompte = value;
                InitStatOfAcompte(currentEvolAcompte.Id);
                NotifyPropertyChanged(nameof(this.TitreEvolution));
            }
        }
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
            this.typeEvolutions = new List<string>() { "Produit", "Acompte" };
            this.typeEvolutionChoisi = typeEvolutions[0];
            

            // Initialisation des events
            this.FindProduct = new RelayCommand(product => GoToProductDetails((ProductViewModel)product));
            this.FindAccount = new RelayCommand(acompte => GoToAccountDetails((AccountViewModel)acompte));
            this.Next = new RelayCommand(x => GetStatNext());
            this.Previous = new RelayCommand(x => GetStatPrevious());

            DayOfWeek firstDay = DayOfWeek.Monday; 
            int diff = date.DayOfWeek - firstDay;
            if (diff < 0)
            {
                diff += 7;
            }
            date = date.AddDays(-diff);

           

            // Initialisation des datas
            InitStatsProduit();
            InitStatsAccount();
            this.evolProducts = new ObservableCollection<StatProduitViewModel>();
            this.evolAcomptes = new ObservableCollection<StatAccountViewModel>();
            this.TypeRechercheStat = "Par semaine";
            this.anneeSelected = this.AnneeList[0];
            
        }
        #endregion

        #region methods
        /// <summary>
        /// Permet d'obtenir les stats d'apres
        /// </summary>
        private void GetStatNext()
        {
            switch(typeRechercheStat)
            {
                case "Par semaine":
                    date = date.AddDays(7);
                    break;
                case "Par mois":
                    date = date.AddMonths(1);
                    break;
                case "Par année":
                    date = date.AddYears(1);
                    break;
                }
            NotifyPropertyChanged(nameof(this.BestAcomptes));
            NotifyPropertyChanged(nameof(this.BestProducts));
            NotifyPropertyChanged(nameof(this.DateIHM));
        }

        /// <summary>
        /// Permet d'obtenir les stats du jour d'avant
        /// </summary>
        private void GetStatPrevious()
        {
            switch (typeRechercheStat)
            {
                case "Par semaine":
                    date = date.AddDays(-7);
                    break;
                case "Par mois":
                    date = date.AddMonths(-1);
                    break;
                case "Par année":
                    date = date.AddYears(-1);
                    break;
            }

            NotifyPropertyChanged(nameof(this.BestAcomptes));
            NotifyPropertyChanged(nameof(this.BestProducts));
            NotifyPropertyChanged(nameof(this.DateIHM));
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
            List<StatProduit> statProduit = this.statProduitManager.GetStatsByWeek(currentWeek, DateTime.Now.Year);
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
        /// Permet d'initialiser les statistiques d'un produit
        /// </summary>
        /// <param name="product_id">id du produit</param>
        public void InitStatOfProduct(int product_id)
        {
            this.evolProducts.Clear();
            List<StatProduit> statProduit = this.statProduitManager.GetStatBOfProductByMonth(anneeSelected,product_id);
            foreach (StatProduit stat in statProduit)
            {
                if (productManager.GetProducts().Find(x => x.ID == stat.Product_id) is Product productLogic)
                {
                    this.evolProducts.Add(new StatProduitViewModel(stat, new ProductViewModel(productLogic, null, null, null)));
                }
            }
            NotifyPropertyChanged(nameof(this.EvolProducts));
        }

        /// <summary>
        /// Permet d'initialiser les statistiques d'un acompte
        /// </summary>
        /// <param name="acompte_id">id de l'acompte</param>
        public void InitStatOfAcompte(int acompte_id)
        {
            this.evolAcomptes.Clear();
            List<StatAccount> statAcomptes = this.statAccountManager.GetStatBOfAcompteByMonth(anneeSelected, acompte_id);
            foreach (StatAccount stat in statAcomptes)
            {
                if (accountManager.GetAdhérents().Find(x => x.Id == stat.Account_Id) is Account acompte)
                {
                    this.evolAcomptes.Add(new StatAccountViewModel(stat, new AccountViewModel(acompte, this.accountManager)));
                }
            }
            NotifyPropertyChanged(nameof(this.EvolAcomptes));
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
            List<StatAccount> statAccount = this.statAccountManager.GetStatsByWeek(currentWeek,DateTime.Now.Year);
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
