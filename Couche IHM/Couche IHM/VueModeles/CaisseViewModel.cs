
using Couche_Métier.Manager;
using Couche_Métier.Utilitaire;
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

namespace Couche_IHM.VueModeles
{
    public class CaisseViewModel : INotifyPropertyChanged
    {

        private ConverterFormatArgent convertFormatArgent = new ConverterFormatArgent();
        private ObservableDictionary<ProductViewModel,int> productOrder = new ObservableDictionary<ProductViewModel,int>();
        private string currentPaiement;
        private bool showPayAcompte = false;
        private bool isAdherent = true;
        private AdherentViewModel adherentPayer = null;
        private StatProduitManager statProduitManager;
        private StatAcompteManager statAcompteManager;
        public CaisseViewModel()
        {
            // Initialisation objets métier
            this.statProduitManager = new StatProduitManager();
            this.statAcompteManager = new StatAcompteManager();

            // Initialisation events
            this.AddProd = new RelayCommand(prodIHM => AddProduct(prodIHM));
            this.RemoveProd = new RelayCommand(prodIHM => RemoveProduct(prodIHM));
            this.ShowPay = new RelayCommand(x => PreviewPayArticles());
            this.CancelPay = new RelayCommand(x => this.ShowPayAcompte = false);
            this.Pay = new RelayCommand(acompte => PayArticles((AdherentViewModel)acompte));
            this.ClearProd = new RelayCommand(x =>
            {
                this.ProductOrder.Clear();
                this.NotifyPropertyChanged(nameof(this.PriceAdherIHM));

                this.NotifyPropertyChanged(nameof(this.PriceNonAdherIHM));
                });
            this.CurrentPaiement = Paiements[0];
        }


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
                { "Acompte", "Paypal", "Carte" };
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
                  prixTotal += (float)convertFormatArgent.ConvertToDouble(product.PrixAdherentIHM) * productOrder[product];
                }
                return prixTotal;
            }
        }

        public string PriceAdherIHM
        {
            get
            {
                return $"{convertFormatArgent.ConvertToString(PriceAdher)}";
            }
        }

        public string PriceNonAdherIHM
        {
            get
            {
                return $"({convertFormatArgent.ConvertToString(PriceNanAdher)})";
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
                    prixTotal += (float)convertFormatArgent.ConvertToDouble(product.PrixNonAdherentIHM) * productOrder[product];
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
        public AdherentViewModel AdherentPayer
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

        public bool IsAdherent
        {
            get => isAdherent;
            set
            {
                isAdherent = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region methods

        /// <summary>
        /// Permet de payer les articles
        /// </summary>
        private async void PayArticles(AdherentViewModel acompte = null)
        {
            string messageLog = $"Achat par {currentPaiement} ";

            // Gérer les stats 
            ObservableDictionary<ProductViewModel, int> productOrder2 = new ObservableDictionary<ProductViewModel, int>(productOrder);
            Task.Run(() =>
            {
                foreach (ProductViewModel product in productOrder2.Keys)
                {
                    StatProduit stat = new StatProduit(0, DateTime.Now, productOrder2[product], product.Id);
                    MainWindowViewModel.Instance.StatViewModel.AddStatProduit(stat);
                    statProduitManager.CreateStat(stat);
                }
            });

            // Si paiement par acompte
            if (acompte != null)
            {
                float argent = this.convertFormatArgent.ConvertToDouble(acompte.ArgentIHM);
                float prix;
                if (acompte.IsAdherentIHM)
                {
                    prix = this.PriceAdher;
                }
                else
                {
                    prix = this.PriceNanAdher;
                }

                Task.Run(() =>
                {
                    StatAcompte stat = new StatAcompte(0, DateTime.Now,prix ,acompte.Id);
                    MainWindowViewModel.Instance.StatViewModel.AddStatAcompte(stat);
                    statAcompteManager.CreateStat(stat);
                });

                acompte.ArgentIHM = this.convertFormatArgent.ConvertToString(argent- prix);
                messageLog += $"({prix}) : ";
                acompte.UpdateAdherent(false);
                this.ShowPayAcompte = false;
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



            // Changer la data
            foreach (ProductViewModel product in productOrder.Keys)
            {
                product.QuantiteIHM -= productOrder[product];
                messageLog += product.NomProduitIHM +", ";
                product.UpdateProduct(false);
            }
            
            
            // Log l'action
            Log log = new Log(0, DateTime.Now, 5, messageLog, MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            Task.Run (() => MainWindowViewModel.Instance.LogManager.CreateLog(log));



            // Notifier la vue
            this.ProductOrder.Clear();
            NotifyPropertyChanged(nameof(PriceAdherIHM));
            NotifyPropertyChanged(nameof(PriceNonAdherIHM));
            MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));


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
                        PayArticles();
                        break;
                    case "Carte":
                        PayArticles();
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
