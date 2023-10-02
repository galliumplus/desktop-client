
using Couche_Métier.Manager;
using Couche_Métier.Utilitaire;
using MaterialDesignThemes.Wpf;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Couche_IHM.VueModeles
{
    public class CaisseViewModel : INotifyPropertyChanged
    {
        #region attributes
        private ObservableDictionary<ProductViewModel,int> productOrder = new ObservableDictionary<ProductViewModel,int>();
        private string currentPaiement;
        private bool showPayAcompte = false;
        private bool showPayPaypal = false;
        private bool showPayLiquide = false;
        private bool showPayBanque = false;
        private bool isAdherent = true;
        private string prixIhm;
        private AcompteViewModel adherentPayer = null;
        private StatProduitManager statProduitManager;
        private StatAcompteManager statAcompteManager;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur de caisse vue modele
        /// </summary>
        public CaisseViewModel(StatAcompteManager statAcompte,StatProduitManager statProduit)
        {
            // Initialisation objets métier
            this.statProduitManager = statProduit;
            this.statAcompteManager = statAcompte;

            // Initialisation events
            this.AddProd = new RelayCommand(prodIHM => AddProduct(prodIHM));
            this.RemoveProd = new RelayCommand(prodIHM => RemoveProduct(prodIHM));
            this.ShowPay = new RelayCommand(x => PreviewPayArticles());
            this.CancelPay = new RelayCommand(x => 
            {
                this.ShowPayPaypal = false;
                this.ShowPayBanque = false;
                this.ShowPayAcompte = false;
            }
            );
            this.Pay = new RelayCommand(tuple => PayArticles(tuple));
            this.ClearProd = new RelayCommand(x =>
            {
                this.ProductOrder.Clear();
                this.NotifyPropertyChanged(nameof(this.PriceAdherIHM));

                this.NotifyPropertyChanged(nameof(this.PriceNonAdherIHM));
                });
            this.CurrentPaiement = Paiements[0];
        }
        #endregion

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region events

        public RelayCommand ClearProd { get; set; }
        public RelayCommand AddProd { get; set; }
        public RelayCommand RemoveProd { get; set; }
        public RelayCommand CancelPay { get; set; }
        public RelayCommand Pay { get; set; }   
        public RelayCommand ShowPay { get; set; }
        #endregion

        #region properties

        /// <summary>
        /// Liste des moyens de paiements
        /// </summary>
        public List<string> Paiements
        {
            get
            {
                return new List<string>()
                { "Acompte", "Paypal", "Carte", "Liquide" };
            }
        }

        /// <summary>
        /// Renvoie le prix total du panier pour les adhérents
        /// </summary>
        public float PriceAdher
        {
            get
            {
                float prixTotal = 0.00f;
                foreach (ProductViewModel product in productOrder.Keys)
                {
                  prixTotal += (float)ConverterFormatArgent.ConvertToDouble(product.PrixAdherentIHM) * productOrder[product];
                }
                return prixTotal;
            }
        }
        /// <summary>
        /// Prix adhérent formatté
        /// </summary>
        public string PriceAdherIHM
        {
            get
            {
                return $"{ConverterFormatArgent.ConvertToString(PriceAdher)}";
            }
        }
        /// <summary>
        /// Prix non adhérent formatté
        /// </summary>
        public string PriceNonAdherIHM
        {
            get
            {
                return $"({ConverterFormatArgent.ConvertToString(PriceNanAdher)})";
            }
        }
        /// <summary>
        /// Renvoie le prix total du panier pour les non adhérents
        /// </summary>
        public float PriceNanAdher
        {
            get
            {
                float prixTotal = 0.00f;
                foreach (ProductViewModel product in productOrder.Keys)
                {
                    prixTotal += (float)ConverterFormatArgent.ConvertToDouble(product.PrixNonAdherentIHM) * productOrder[product];
                }
                return prixTotal;
            }

        }

        /// <summary>
        /// Représente les produits du panier
        /// </summary>
        public ObservableDictionary<ProductViewModel, int> ProductOrder
        { 
            get => productOrder; 
            set => productOrder = value; 
        }

        /// <summary>
        /// Représente le moyen de paiement sélectionné
        /// </summary>
        public string CurrentPaiement 
        { 
            get => currentPaiement;
            set 
            { 
                currentPaiement = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Représente l'adhérent qui va payer
        /// </summary>
        public AcompteViewModel AdherentPayer
        {
            get => adherentPayer;
            set 
            {
                adherentPayer = value;
                NotifyPropertyChanged();
            } 
        }

        /// <summary>
        /// Permet d'afficher la sélection d'acompte
        /// </summary>
        public bool ShowPayAcompte 
        { 
            get => showPayAcompte;
            set
            {
                showPayAcompte = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Est ce que l'acheteur est adhérent
        /// </summary>
        public bool IsAdherent
        {
            get => isAdherent;
            set
            {
                isAdherent = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowPayPaypal 
        { 
            get => showPayPaypal;
            set 
            { 
                showPayPaypal = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowPayBanque
        {
            get => showPayBanque;
            set 
            { 
                showPayBanque = value;
                NotifyPropertyChanged();
            }
        }

        public string PrixIHM
        {
            get { return prixIhm; }
            set 
            { 
                prixIhm = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowPayLiquide 
        { 
            get => showPayLiquide;
            set 
            { 
                showPayLiquide = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region methods

        /// <summary>
        /// Permet de payer les articles
        /// </summary>
        private async void PayArticles(object tuple = null)
        {
            string messageLog = $"Achat par {currentPaiement} ";

            // Paiement par acompte
            try
            {
                if (tuple is Tuple<AcompteViewModel, bool> values)
                {
                    AcompteViewModel acompte = values.Item1;
                    bool isAdherentCheckboxChecked = values.Item2;

                    float argent = ConverterFormatArgent.ConvertToDouble(acompte.ArgentIHM);
                    float prix;
                    if (isAdherentCheckboxChecked)
                    {
                        prix = this.PriceAdher;
                    }
                    else
                    {
                        prix = this.PriceNanAdher;
                    }

                    if (argent - prix < 0)
                    {
                        throw new Exception("Pas assez d'argent sur l'acompte");
                    }

                    _ = Task.Run(() =>
                    {
                        StatAcompte stat = new StatAcompte(0, DateTime.Now, prix, acompte.Id);
                        MainWindowViewModel.Instance.StatViewModel.AddStatAcompte(stat);
                        statAcompteManager.CreateStat(stat);
                    });

                    string prixFormatted = ConverterFormatArgent.ConvertToString(prix);
                    acompte.ArgentIHM = ConverterFormatArgent.ConvertToString(argent - prix);
                    messageLog += $"{acompte.IdentifiantIHM} ";
                    messageLog += $"({prixFormatted}) : ";
                    acompte.UpdateAcompte(false);

                }
                else
                {
                    if (isAdherent)
                    {
                        messageLog += $"({this.PriceAdherIHM}) : ";
                    }
                    else
                    {
                        messageLog += $"{this.PriceNonAdherIHM} : ";
                    }
                }

                
                foreach (ProductViewModel product in productOrder.Keys)
                {
                    messageLog += product.NomProduitIHM + ", ";
                }

                // Gérer les stats 
                ObservableDictionary<ProductViewModel, int> productOrder2 = new ObservableDictionary<ProductViewModel, int>(productOrder);
                _ = Task.Run(() =>
                    {
                        foreach (ProductViewModel product in productOrder2.Keys)
                        {
                            StatProduit stat = new StatProduit(0, DateTime.Now, productOrder2[product], product.Id);
                            MainWindowViewModel.Instance.StatViewModel.AddStatProduit(stat);
                            statProduitManager.CreateStat(stat);

                            product.QuantiteIHM -= productOrder2[product];
                            product.UpdateProduct(false);
                        }
                    });

                // Log l'action
                Log log = new Log(DateTime.Now, 5, messageLog, MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                _ = Task.Run(() => MainWindowViewModel.Instance.LogManager.CreateLog(log));
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));

                this.ProductOrder.Clear();
                NotifyPropertyChanged(nameof(PriceAdherIHM));
                NotifyPropertyChanged(nameof(PriceNonAdherIHM));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


            // Notifier la vue
            this.ShowPayAcompte = false;
            this.ShowPayPaypal = false;
            this.ShowPayLiquide = false;
            this.ShowPayBanque = false;


        }

        /// <summary>
        /// Permet de préparer le paiement des articles
        /// </summary>
        private void PreviewPayArticles()
        {
            if (this.PriceAdher != 0 && this.PriceNanAdher != 0)
            {
                switch (currentPaiement)
                {
                    case "Acompte":
                        this.ShowPayAcompte = true;
                        break;
                    case "Paypal":
                        PrixIHM = "Montant : " + (this.isAdherent ? this.PriceAdherIHM : this.PriceNonAdherIHM);
                        this.ShowPayPaypal = true;
                        break;
                    case "Carte":
                        PrixIHM = "Montant : " + (this.isAdherent ? this.PriceAdherIHM : this.PriceNonAdherIHM);
                        this.ShowPayBanque = true;
                        break;
                    case "Liquide":
                        PrixIHM = "Montant : " + (this.isAdherent ? this.PriceAdherIHM : this.PriceNonAdherIHM);
                        this.ShowPayLiquide=true;
                        break;
                }
            }
            
        }

        /// <summary>
        /// Ajoute un produit au panier
        /// </summary
        public void AddProduct(object product)
        {

            ProductViewModel produitIHM = (ProductViewModel)product;

            if (this.productOrder.ContainsKey(produitIHM))
            {
                if (produitIHM.QuantiteIHM - productOrder[produitIHM] > 0)
                {
                    this.productOrder[produitIHM]++;
                    
                }
                
            }
            else
            {
                this.productOrder[produitIHM] = 1;
            }

            NotifyPropertyChanged(nameof(PriceAdherIHM));
            NotifyPropertyChanged(nameof(PriceNonAdherIHM));
        }

        /// <summary>
        /// Permet d'enlever un produit
        /// </summary>
        private void RemoveProduct(object product)
        {
            ProductViewModel produitIHM = (ProductViewModel)product;
            if (this.productOrder[produitIHM] == 1)
            {
                this.productOrder.Remove(produitIHM);
            }
            else
            {
                this.productOrder[produitIHM]--;
            }

            NotifyPropertyChanged(nameof(PriceAdherIHM));
            NotifyPropertyChanged(nameof(PriceNonAdherIHM));
        }



        #endregion
    }
}
