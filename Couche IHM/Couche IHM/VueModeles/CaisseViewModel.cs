
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class CaisseViewModel : INotifyPropertyChanged
    {

        private ConverterFormatArgent convertFormatArgent = new ConverterFormatArgent();
        private ObservableDictionary<ProductViewModel,int> productOrder = new ObservableDictionary<ProductViewModel,int>();
        private string currentPaiement;
        private bool showPayAcompte = false;
        private AdherentViewModel adherentPayer = null;
        public CaisseViewModel()
        {
            this.AddProd = new RelayCommand(prodIHM => AddProduct(prodIHM));
            this.RemoveProd = new RelayCommand(prodIHM => RemoveProduct(prodIHM));
            this.ShowPay = new RelayCommand(x => PreviewPayArticles());
            this.CancelPay = new RelayCommand(x => this.ShowPayAcompte = false);
            this.Pay = new RelayCommand(acompte => PayArticles((AdherentViewModel)acompte));
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


        #endregion

        #region methods

        /// <summary>
        /// Permet de payer les articles
        /// </summary>
        private void PayArticles(AdherentViewModel acompte = null)
        {
            string messageLog = $"Achat par {currentPaiement} ";

            // Si paiement par acompte
            if (acompte != null)
            {
                float argent = this.convertFormatArgent.ConvertToDouble(acompte.ArgentIHM);
                if (acompte.IsAdherentIHM)
                {
                    argent -= this.PriceAdher;
                    acompte.ArgentIHM = this.convertFormatArgent.ConvertToString(argent);
                    messageLog += $"({this.PriceAdherIHM}) de ";
                }
                else
                {
                    argent -= this.PriceNanAdher;
                    acompte.ArgentIHM = this.convertFormatArgent.ConvertToString(argent);
                    messageLog += $"({this.PriceNanAdher}) de ";
                }
                acompte.UpdateAdherent(false);
                this.ShowPayAcompte = false;
            }

            // Changer la data
            foreach (ProductViewModel product in productOrder.Keys)
            {
                product.QuantiteIHM -= productOrder[product];
                messageLog += product.NomProduitIHM +", ";
                product.UpdateProduct(false);
            }

            // Log l'action
            Log log = new Log(0, DateTime.Now.ToString("g"), 5, messageLog, MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            MainWindowViewModel.Instance.LogManager.CreateLog(log);

            // Notifier la vue
            this.ProductOrder.Clear();
            NotifyPropertyChanged(nameof(PriceAdherIHM));
            NotifyPropertyChanged(nameof(PriceNonAdherIHM));
            MainWindowViewModel.Instance.LogsViewModel.Logs.Insert(0, new LogViewModel(log));
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
