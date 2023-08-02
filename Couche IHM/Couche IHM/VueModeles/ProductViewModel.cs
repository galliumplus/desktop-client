

using Couche_IHM.ImagesProduit;
using Couche_Métier;
using Couche_Métier.Log;
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace Couche_IHM.VueModeles
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        #region attributes
        /// <summary>
        /// Représente le modèle produit
        /// </summary>
        private Product product;
        private ImageManager imageManager;
        private ConverterFormatArgent formatArgent;
        private ProductManager productManager;
        private ILog logProduct;
        private int quantiteIHM;
        private string nomProduitIHM;
        private CategoryViewModel categoryIHM;
        private string prixAdherentIHM;
        private string prixNonAdherentIHM;
        #endregion

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region events
        public RelayCommand ResetProd { get; set; }
        public RelayCommand UpdateProd { get; set; }
        #endregion

        #region properties

        /// <summary>
        /// Permet de savoir si le produit est disponible
        /// </summary>
        public bool isDisponible
        {
            get
            {
                return (product.Quantite > 0);
            }
        }

        /// <summary>
        /// Renvoie la quantite du produit
        /// </summary>
        public int QuantiteIHM
        {
            get
            {
                return quantiteIHM;
            }
            set => quantiteIHM = value;
        }

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string NomProduitIHM
        {
            get => nomProduitIHM;
            set => nomProduitIHM = value;
        }

        /// <summary>
        /// Prix Adherent formatté pour l'afficher
        /// </summary>
        public string PrixAdherentIHM
        {
            get => prixAdherentIHM;
            set => prixAdherentIHM = value;
        }
        /// <summary>
        /// Prix non adhérent formatté pour l'afficher
        /// </summary>
        public string PrixNonAdherentIHM
        {
            get => prixNonAdherentIHM;
            set => prixNonAdherentIHM = value;
        }

        /// <summary>
        /// Categorie du produit
        /// </summary>
        public CategoryViewModel CategoryIHM
        {
            get => categoryIHM;
            set { categoryIHM = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Image du produit
        /// </summary>
        public BitmapImage ImageProduct 
        {
            get
            { 
                return new BitmapImage(new Uri(imageManager.GetImageFromProduct(this.NomProduitIHM), UriKind.Absolute));
            }
            
        }

        #endregion

        public ProductViewModel(Product product,ProductManager productManager,CategoryViewModel categoryProduit)
        {
            // Initialisation du modele
            this.product = product;

            // Initialisation des objets metiers
            this.imageManager = new ImageManager();
            this.formatArgent = new ConverterFormatArgent();
            this.productManager = productManager;
            this.logProduct = new LogProductToTxt();

            // Initialisation des attributsIHM
            this.categoryIHM = categoryProduit;
            this.quantiteIHM = product.Quantite;
            this.nomProduitIHM = product.NomProduit;
            this.prixNonAdherentIHM = formatArgent.ConvertToString(product.PrixNonAdherent);
            this.prixAdherentIHM = formatArgent.ConvertToString(product.PrixAdherent);

            // Initialisation des events
            this.ResetProd = new RelayCommand(x => ResetProduct());
            this.UpdateProd = new RelayCommand(x => CreateProduct());
        }

        #region methods

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void UpdateProduct()
        {
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Changer la data
            this.product.Quantite = this.quantiteIHM;
            this.product.NomProduit = this.nomProduitIHM;
            this.product.Categorie = this.categoryIHM.CurrentNameCategory;
            this.product.PrixAdherent = formatArgent.ConvertToDouble(this.prixAdherentIHM);
            this.product.PrixNonAdherent = formatArgent.ConvertToDouble(this.prixNonAdherentIHM);
            this.productManager.UpdateProduct(this.product);


            // Notifier la vue
            NotifyPropertyChanged(nameof(NomProduitIHM));
            NotifyPropertyChanged(nameof(QuantiteIHM));
            NotifyPropertyChanged(nameof(CategoryIHM));
            NotifyPropertyChanged(nameof(isDisponible));

            // Log l'action
            this.logProduct.registerLog(CategorieLog.UPDATE, this.product, MainWindowViewModel.Instance.CompteConnected);

            MainWindowViewModel.Instance.ProductViewModel.ShowProductDetail = false;
        }

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void CreateProduct()
        {
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Changer la data
            this.product.Quantite = this.quantiteIHM;
            this.product.NomProduit = this.nomProduitIHM;
            this.product.Categorie = this.categoryIHM.CurrentNameCategory;
            this.product.PrixAdherent = formatArgent.ConvertToDouble(this.prixAdherentIHM);
            this.product.PrixNonAdherent = formatArgent.ConvertToDouble(this.prixNonAdherentIHM);
            this.productManager.CreateProduct(this.product);


            // Notifier la vue
            MainWindowViewModel.Instance.ProductViewModel.Products.Add(this);
            NotifyPropertyChanged(nameof(NomProduitIHM));
            NotifyPropertyChanged(nameof(QuantiteIHM));
            NotifyPropertyChanged(nameof(CategoryIHM));
            NotifyPropertyChanged(nameof(isDisponible));

            // Log l'action
            this.logProduct.registerLog(CategorieLog.CREATE, this.product, MainWindowViewModel.Instance.CompteConnected);


            MainWindowViewModel.Instance.ProductViewModel.ShowProductDetail = false;
        }

        /// <summary>
        /// Permet de reset les propriétés de l'adhérent
        /// </summary>
        public void ResetProduct()
        {
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Initialisation propriétés
            if(this.categoryIHM != null)
            {
                this.categoryIHM.NameCategory = product.Categorie;
            }
            
            this.quantiteIHM = product.Quantite;
            this.nomProduitIHM = product.NomProduit;
            this.prixNonAdherentIHM = formatArgent.ConvertToString(product.PrixNonAdherent);
            this.prixAdherentIHM = formatArgent.ConvertToString(product.PrixAdherent);

            // Notifier la vue
            NotifyPropertyChanged(nameof(CategoryIHM));
            NotifyPropertyChanged(nameof(QuantiteIHM));
            NotifyPropertyChanged(nameof(NomProduitIHM));
            NotifyPropertyChanged(nameof(PrixNonAdherentIHM));
            NotifyPropertyChanged(nameof(PrixAdherentIHM));

            MainWindowViewModel.Instance.ProductViewModel.ShowProductDetail = false;
        }

        public override bool Equals(object? obj)
        {
            return obj is ProductViewModel model &&
                   nomProduitIHM == model.nomProduitIHM;
        }


        #endregion
    }
}
