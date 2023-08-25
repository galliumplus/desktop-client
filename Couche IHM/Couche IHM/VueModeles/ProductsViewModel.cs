using Couche_Métier;
using Couche_Métier.Manager;
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
        private ObservableCollection<CategoryViewModel> categories = new ObservableCollection<CategoryViewModel>();
        private ProductManager productManager;
        private CategoryManager categoryManager;
        private ProductViewModel currentProduct;

        private bool showProductDetail = false;
        private bool showModifButtons = false;
        private bool showProduct = false;
        private bool showCategories = false;
        private bool showDeleteProduct = false;
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
        public ObservableCollection<CategoryViewModel> Categories
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
                if (currentProduct != null)
                {
                    currentProduct.ResetProduct();
                }

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
                            prd.CategoryIHM.NomCat.ToUpper().Contains(searchFilter.ToUpper())));
                }

                return produitsIHM;
            }
            set 
            { 
                products = value; 
            }
        }

        /// <summary>
        /// Permet d'afficher les boutons de modification rapide
        /// </summary>
        public bool ShowModifButtons
        {
            get => showModifButtons;
            set
            {
                showModifButtons = value;
                NotifyPropertyChanged();
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

        /// <summary>
        /// Permet d'afficher la popup des catégories
        /// </summary>
        public bool ShowCategories 
        { 
            get => showCategories;
            set
            { 
                showCategories = value; 
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Permet d'afficher l'icone pour supprimer un produit
        /// </summary>
        public bool ShowDeleteProduct 
        { 
            get => showDeleteProduct;
            set 
            { 
                showDeleteProduct = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Est ce que la card actions est retournée
        /// </summary>
        public bool ShowProduct
        {
            get => showProduct;
            set
            {
                showProduct = value;
                NotifyPropertyChanged();
            }
        }

        public ProductManager ProductManager { get => productManager; set => productManager = value; }


        #endregion

        /// <summary>
        /// Constructeur du viewModel
        /// </summary>
        public ProductsViewModel(ProductManager productManager)
        {
            // Initialisation objet métier
            this.productManager = productManager;
            this.categoryManager = new CategoryManager();

            // Initialisation Events
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

            // Initialisation Data
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
                CategoryViewModel catProduit = null;
                if (prd.Categorie != -1)
                {
                    catProduit = this.categories.ToList().Find(x => x.NomCat == this.categoryManager.Categories.Find(x => x.IdCat == prd.Categorie).NomCategory);
                }
                
                this.products.Add(new ProductViewModel(prd,this.productManager,this.categoryManager,catProduit));
            }
            this.currentProduct = this.products[0];
        }


        /// <summary>
        /// Permet de récupérer la liste des catégories
        /// </summary>
        private void InitCategories()
        {
            List<Category> categories = this.categoryManager.Categories;
            foreach (Category cat in categories)
            {
                this.categories.Add(new CategoryViewModel(this.categoryManager,cat));
            }
        }

        /// <summary>
        /// Permet d'ouvrir les détails du produit
        /// </summary>
        /// <param name="action"></param>
        private void OpenProductDetails(string action)
        {

            if (action == "NEW" || currentProduct == null || currentProduct.Action == "NEW")
            {
                ShowDeleteProduct = false;
                CurrentProduct = new ProductViewModel(new Product(),this.productManager,this.categoryManager ,null);
            }
            else
            {
                ShowDeleteProduct = true;
            }
            
            ShowProductDetail = true;
        }

        /// <summary>
        /// Permet de rajouter un produit  dans la liste
        /// </summary>
        /// <param name="produit"></param>
        public void AddProduct(ProductViewModel produit)
        {
            this.products.Add(produit);
            NotifyPropertyChanged(nameof(Products));

        }

        /// <summary>
        /// Permet de supprimer un produit  dans la liste
        /// </summary>
        /// <param name="produit"></param>
        public void RemoveProduct(ProductViewModel produit)
        {
            this.products.Remove(produit);
            NotifyPropertyChanged(nameof(Products));
        }

        /// <summary>
        /// Permet de récupérer les produits
        /// </summary>
        /// <returns></returns>
        public List<ProductViewModel> GetProducts()
        {
            return this.products.ToList();
        }

        #endregion
    }
}
