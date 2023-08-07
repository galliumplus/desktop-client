using Couche_Métier.Utilitaire;
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
        private AdherentViewModel adherentPayer = null;
        public CaisseViewModel()
        {
            this.AddProd = new RelayCommand(prodIHM => AddProduct(prodIHM));
            this.RemoveProd = new RelayCommand(prodIHM => RemoveProduct(prodIHM));
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


        #endregion

        #region methods

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
