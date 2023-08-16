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
        /// Liste des adhérents
        /// </summary>
        public List<PodiumProduit> PodiumProduits
        {
            get
            {
                List<PodiumProduit> podAdherents = new List<PodiumProduit>();
                List<ProductViewModel> prds = products.OrderByDescending(a => a.PurchaseCount).Take(3).ToList();
                for (int i = 0; i < 3; i++)
                {
                    podAdherents.Add(new PodiumProduit(prds[i], i));
                }
                return podAdherents;
            }
        }

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
            Random random = new Random();
            foreach (Product prd in produitsMetier)
            {
                int r = random.Next(0, 100);
                CategoryViewModel catProduit = this.categories.Find(x => x.CurrentNameCategory == this.categoryManager.Categories.Find(x => x.IdCat == prd.Categorie).NomCategory);
                this.products.Add(new ProductViewModel(prd,this.productManager,this.categoryManager,catProduit,r));
            }
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


        private void OpenProductDetails(string action)
        {

            if (action == "NEW")
            {
                ShowDeleteProduct = false;
                CurrentProduct = new ProductViewModel(new Product(),this.productManager,this.categoryManager ,null,0);
            }
            else
            {
                ShowDeleteProduct = true;
            }
            
            ShowProductDetail = true;
        }

        #endregion
    }
}
