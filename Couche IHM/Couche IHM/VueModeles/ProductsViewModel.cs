using Couche_Métier;
using Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<ProductViewModel> products = new ObservableCollection<ProductViewModel>();
        private List<CategoryViewModel> categories = new List<CategoryViewModel>();
        private ProductManager productManager;
        private CategoryManager categoryManager;
        private ProductViewModel currentProduct;

        private bool showProductDetail = false;
        private bool showProduct = false;
        private bool showCategories = false;
        private string searchFilter = "";

        #endregion
        #region events
        public RelayCommand OpenProd { get; set; }
        public RelayCommand CloseCategory { get; set; }
        public RelayCommand OpenCat { get; set; }
        #endregion
        #region properties

        /// <summary>
        /// Représente toutes les catégories de produits disponibles
        /// </summary>
        public List<CategoryViewModel> Categories
        {
            get
            {
                return categories;
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
        public ObservableCollection<ProductViewModel> Products
        {
            get
            {
                ObservableCollection<ProductViewModel> produitsIHM;
                if (searchFilter == "")
                {
                    produitsIHM = products;
                }
                else
                {
                    produitsIHM = new ObservableCollection<ProductViewModel>(
                        products.ToList().FindAll(prd =>
                            prd.NomProduitIHM.ToUpper().Contains(searchFilter.ToUpper()) ||
                            prd.CategoryIHM.CurrentNameCategory.ToUpper().Contains(searchFilter.ToUpper())));
                }

                return produitsIHM;
            }
            set 
            { 
                products = value; 
            }
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
        public bool ShowCategories 
        { 
            get => showCategories;
            set
            { 
                showCategories = value; 
                NotifyPropertyChanged();
            }
        }

        #endregion

        public ProductsViewModel()
        {
            this.productManager = new ProductManager();
            this.categoryManager = new CategoryManager();
            this.OpenProd = new RelayCommand(action => OpenProductDetails((string)action));
            this.OpenCat = new RelayCommand(x => this.ShowCategories = true);
            this.CloseCategory = new RelayCommand(x =>
            {
                foreach (CategoryViewModel categoryViewModel in categories)
                {
                    categoryViewModel.ResetCategory();
                }
                this.showCategories = false;
            }
            );
            InitCategories();
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
                CategoryViewModel catProduit = this.categories.Find(x => x.CurrentNameCategory == prd.Categorie);
                this.products.Add(new ProductViewModel(prd,this.productManager,catProduit));
            }
        }

        /// <summary>
        /// Permet de récupérer la liste des catégories
        /// </summary>
        private void InitCategories()
        {
            List<string> categories = this.categoryManager.Categories;
            foreach (string cat in categories)
            {
                this.categories.Add(new CategoryViewModel(this.categoryManager,cat));
            }
        }


        private void OpenProductDetails(string action)
        {

            if (action == "NEW")
            {
                CurrentProduct = new ProductViewModel(new Product(),this.productManager, this.categories[0]);
            }
            ShowProductDetail = true;
        }

        #endregion
    }
}
