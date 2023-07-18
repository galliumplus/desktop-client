
using Couche_IHM.CustomListView;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Log;
using Couche_Métier.Utilitaire;
using Modeles;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameCaisse.xaml
    /// </summary>
    public partial class FrameCaisse : Page
    {


        /// <summary>
        /// Constructeur de la caisse
        /// </summary>
        /// <param name="adherentManager"></param>
        /// <param name="produitManager"></param>
        /// <param name="categorieManager"></param>
        public FrameCaisse()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel.Instance;

            // Initialisation des produits de la caisse
            productsSP.Children.Clear();
            foreach (string category in MainWindowViewModel.Instance.ProductViewModel.Categories)
            {
                if (category != "HIDDEN")
                {
                    // On créer chaque vue catégorie
                    CategoryProductList categoryProductList = new CategoryProductList(category);
                    productsSP.Children.Add(categoryProductList);
                }



            }
        }



        #region BouttonIHM

        /// <summary>
        /// Valide le paiment 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PayCaisse(object sender, RoutedEventArgs e)
        {
            /**
            bool result = false;
            Adhérent adhérentSelectionne = null;
            // Si Achat acompte
            if(listeMoyenPayement.SelectedItem != null)
            {
                switch (listeMoyenPayement.SelectedItem.ToString())
                {
                    case "Acompte":
                        PaiementAcompteWindow fn = new PaiementAcompteWindow(this.adherentManager.GetAdhérents(),this.PriceAdher,this.PriceNanAdher);
                        result = fn.ShowDialog().Value;
                        if (result)
                        {
                            adhérentSelectionne = fn.AdhérentSelectionné;
                        }
                        break;
                    case "Liquide":
                        break;
                    case "Carte":
                        break;
                    case "Paypal":
                        break;
                }

            }

            // Si vente reussite
            if (result == true)
            {
                // On enleve les produits du stock
                for (int i = 0; i < orderedItem.Count; i++)
                {
                    orderedItem[i].Key.Quantite -= orderedItem[i].Value;
                }
                this.orderedItem.Clear();
                UpdateListProduitsOrder();
                ConverterFormatArgent converterFormat = new ConverterFormatArgent();
                this.PriceA.Content = converterFormat.ConvertToString(this.PriceAdher);
                this.PriceNA.Content = "(" + converterFormat.ConvertToString(this.PriceNanAdher) + ")";
                this.adherentManager.UpdateAdhérent(adhérentSelectionne);
                //this.log.registerLog(CategorieLog.VENTE, adhérentSelectionne, MainWindow.CompteConnected);
            }**/
        }

        #endregion


    }
}
