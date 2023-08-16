using Couche_Métier;
using Couche_Métier.Log;
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
    public class CategoryViewModel : INotifyPropertyChanged
    {

        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private CategoryManager categoryManager;
        private string currentNameCategory;
        private Category nameCategory;

        private bool invisible;
        #endregion

        #region properties

        
        /// <summary>
        /// Représente le nom de la catégorie modifiable
        /// </summary>
        public string NameCategory 
        { 
            get => nameCategory.NomCategory;
            set 
            { 
                nameCategory.NomCategory = value;
                NotifyPropertyChanged(nameof(NameCategory));
            }
        }

        /// <summary>
        /// Représente le modèle du nom de la catégorie
        /// </summary>
        public string CurrentNameCategory 
        { 
            get => currentNameCategory;
            set 
            { 
                currentNameCategory = value; 
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Est ce que la catégory est visible sur la caisse
        /// </summary>
        public bool Invisible 
        { 
            get => invisible;
            set 
            { 
                invisible = value;
                NotifyPropertyChanged();
            }

        }

        #endregion

        #region events
        public RelayCommand UpdateCat { get; set; }
        public RelayCommand DeleteCat { get; set; }

        public RelayCommand ActivateCat { get; set; }
        #endregion
        public CategoryViewModel(CategoryManager categoryManager,Category category)
        {
            this.categoryManager = categoryManager;
            this.currentNameCategory = category.NomCategory;
            this.nameCategory = category;
            this.invisible = !category.Visible;

            // Initialisation des events
            this.UpdateCat = new RelayCommand(x => this.UpdateCategory());
            this.DeleteCat = new RelayCommand(x => this.DeleteCategory());
            this.ActivateCat = new RelayCommand(x => this.ActivateCategory());

        }

        #region methods

        /// <summary>
        /// Permet de rendre visible ou invisible une catégory
        /// </summary>
        public void ActivateCategory()
        {
            // Mise à jour data
            this.categoryManager.UpdateCategory(nameCategory);


            // Log l'action
            //this.log.registerLog(CategorieLog.UPDATE, this.currentNameCategory, MainWindowViewModel.Instance.CompteConnected);


        }

        /// <summary>
        /// Permet de mettre à jour une category
        /// </summary>
        public void UpdateCategory()
        {   
            // Mise à jour data
            this.categoryManager.UpdateCategory(nameCategory);
            this.CurrentNameCategory=nameCategory.NomCategory;


            // Log l'action
            //this.log.registerLog(CategorieLog.UPDATE, this.currentNameCategory, MainWindowViewModel.Instance.CompteConnected);

            
        }



        /// <summary>
        /// Permet de reset les propriétés de la catégorie
        /// </summary>
        public void ResetCategory()
        {
            // Mise à jour data
            this.NameCategory = this.currentNameCategory;


            // Log l'action
            //this.log.registerLog(CategorieLog.UPDATE, this.currentNameCategory, MainWindowViewModel.Instance.CompteConnected);

            MainWindowViewModel.Instance.ProductViewModel.ShowCategories = false;
        }

        /// <summary>
        /// Permet de supprimer une catégorie
        /// </summary>
        public void DeleteCategory()
        {
            // Mise à jour data
            this.categoryManager.DeleteCategory(nameCategory);
            foreach (ProductViewModel prod in MainWindowViewModel.Instance.ProductViewModel.Products.ToList().FindAll(x => x.CategoryIHM == this))
            {
                prod.DeleteCatNotify();
            }
            
            // Notifier la vue
            MainWindowViewModel.Instance.ProductViewModel.Categories.Remove(this);

        }

        public override bool Equals(object? obj)
        {
            return obj is CategoryViewModel model &&
                   currentNameCategory == model.currentNameCategory;
        }
        #endregion


    }
}
