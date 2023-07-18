using Couche_Métier;
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
    public class ProductsViewModel : INotifyPropertyChanged
    {
        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private List<ProductViewModel> products = new List<ProductViewModel>();
        private ProductManager productManager;
        private CategoryManager categoryManager;
        private ProductViewModel currentProduct;

        private bool showProductDetail = false;
        private bool showProduct = false;

        private string searchFilter = "";

        #endregion
        #region events
        public RelayCommand OpenProd { get; set; }
        #endregion
        #region properties

        /// <summary>
        /// Représente toutes les catégories de produits disponibles
        /// </summary>
        public List<string> Categories
        {
            get
            {
                return categoryManager.ListAllCategory();
            }
        }
        /// <summary>
        /// Représente l'adherent selectionné
        /// </summary>
        public ProductViewModel CurrentProduct
        {
            get => currentProduct;
            set
            {
                currentProduct = value;
                if (value != null)
                {
                    ShowProduct = true;

                }
                else
                {
                    this.ShowProduct = false;

                }
                NotifyPropertyChanged(nameof(CurrentProduct));
            }
        }

        /// <summary>
        /// Représente la liste de tous les produits ihm
        /// </summary>
        public List<ProductViewModel> Products
        {
            get
            {
                List<ProductViewModel> produitsIHM;
                if (searchFilter == "")
                {
                    produitsIHM = products;
                }
                else
                {

                    produitsIHM = products.FindAll(prd =>
                    prd.NomProduitIHM.ToUpper().Contains(searchFilter.ToUpper()) ||
                    prd.CategoryIHM.ToUpper().Contains(searchFilter.ToUpper()));
                }

                return produitsIHM;
            }
            set => products = value;
        }

        /// <summary>
        /// Permet d'ouvrir le détail du produit
        /// </summary>
        public bool ShowProduct
        {
            get => showProduct;
            set 
            {
                showProduct = value;
                NotifyPropertyChanged(nameof(ShowProduct));
            }
        }

        /// <summary>
        /// Représente la barre de recherche des produits
        /// </summary>
        public string SearchFilter
        {
            get => searchFilter;
            set 
            {
                searchFilter = value;
                NotifyPropertyChanged(nameof(Products));
                
            } 
        }

        /// <summary>
        /// Permet de montrer la popup du produit
        /// </summary>
        public bool ShowProductDetail 
        { 
            get => showProductDetail;
            set 
            { 
                showProductDetail = value; 
                NotifyPropertyChanged(nameof(ShowProductDetail));
            }
        }

        public ProductManager ProductManager { get => productManager; set => productManager = value; }

        #endregion

        public ProductsViewModel()
        {
            this.productManager = new ProductManager();
            this.categoryManager = new CategoryManager();
            this.OpenProd = new RelayCommand(x => this.ShowProductDetail = true);
            InitProducts();
        }

        #region methods
        /// <summary>
        /// Permet de récupérer la liste des adhérents
        /// </summary>
        private void InitProducts()
        {
            List<Product> produitsMetier = this.productManager.GetProducts();
            foreach (Product prd in produitsMetier)
            {
                this.products.Add(new ProductViewModel(prd,this.productManager));
            }
        }





        #endregion
    }
}
